
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
'
' * Use MiniMax and alpha-beta pruning for tic-tac-toe game.
' * The challenge part is to give the correct huristics for the current node.
' * The short version from wikipedia is more compact but requires to return different scores for terminal node.
' * So, it is better to write longer code, but it saves the time of debugging.
' * The terminal node includes: leaf node(no children), and the nodes that winner is determined.
' * Hint: Store the min/max state in the node makes the minimax code easier to understand/implement.
' * 
' * Huristic value of a note: 
' * There are 8 rows. 
' * For each row, if there are both X and O, then the score for the row is 0.
' *      If the whole row is empty, then the score is 1.
' *      If there is only one X, then the score is 10.
' *      If there are two X, then the score is 100.
' *      If there are 3 X, then the score is 1000.
' *      For player O, the score is negative.
' *      PlayerX tries to maximize the score.
' *      PlayerO tries to minimize the score.
' * 
' * Conclusion:
' * PlayerX makes the first move.
' * PlayerO can make defensive moves to force a tie.
' * If PlayerX makes the first move at the corner, then the playerO has to take the center.
' * If PlayerX makes the first move at the center, then the playerO has take the coners.
' * If PlayerX makes the first move at the top edge, then the playerO cannot take at the side edges or unconnected corners.
' * 
' * It is mcuh easier to understand and implement it in two functions: Min and Max
' * 

Namespace TicTacToe
	Enum enuGridEntry As Byte
		Empty
		PlayerX
		PlayerO
	End Enum

	NotInheritable Class Board
		Private m_enuValues As enuGridEntry()
		Private m_intScore As Integer
		Private m_blnTurnForPlayerX As Boolean

		Public Property RecursiveScore() As Integer

			Get
				Return m_intRecursiveScore
			End Get

			Private Set(intValue As Integer)
				m_intRecursiveScore = intValue
			End Set

		End Property
		Private m_intRecursiveScore As Integer

		Public Property GameOver() As Boolean

			Get
				Return m_blnGameOver
			End Get

			Private Set(blnValue As Boolean)
				m_blnGameOver = blnValue
			End Set

		End Property
		Private m_blnGameOver As Boolean

		Public Sub New(enuValues As enuGridEntry(), blnTurnForPlayerX As Boolean)
			m_blnTurnForPlayerX = blnTurnForPlayerX
			m_enuValues = enuValues
			ComputeScore()
		End Sub

		Public Overrides Function ToString() As String

			Dim clsReturnString As New StringBuilder()
			Dim intRow As Integer = 0
			Dim intColumn As Integer = 0

			For intRow = 0 To 2
				For intColumn = 0 To 2
					Dim enuGrid As enuGridEntry = m_enuValues(intRow * 3 + intColumn)
					Dim chrPlayerLetter As Char = "-"
					If enuGrid = enuGridEntry.PlayerX Then
						chrPlayerLetter = "X"
					ElseIf enuGrid = enuGridEntry.PlayerO Then
						chrPlayerLetter = "O"
					End If
					clsReturnString.Append(chrPlayerLetter)
				Next
				clsReturnString.Append(vbCrLf)
			Next
			clsReturnString.AppendFormat("score={0},ret={1},{2}", m_intScore, RecursiveScore, m_blnTurnForPlayerX)
			Return clsReturnString.ToString()
		End Function

		Public Function GetChildAtPosition(intX As Integer, intY As Integer) As Board
			Dim intIndex As Integer = intX + intY * 3
			Dim enuNewValues As enuGridEntry() = DirectCast(m_enuValues.Clone(), enuGridEntry())

			If m_enuValues(intIndex) <> enuGridEntry.Empty Then
				Throw New Exception(String.Format("invalid index [{0},{1}] is taken by {2}", x, y, m_enuValues(i)))
			End If

			enuNewValues(intIndex) = If(m_blnTurnForPlayerX, enuGridEntry.PlayerX, enuGridEntry.PlayerO)
			Return New Board(enuNewValues, Not m_blnTurnForPlayerX)
		End Function

		Public Function IsTerminalNode() As Boolean
			Dim blnResult As Boolean

			If GameOver Then
				blnResult = True
			End If
			'if all entries are set, then it is a leaf node
			For Each v As enuGridEntry In m_enuValues
				If v = enuGridEntry.Empty Then
					blnResult = False
				End If
			Next
			blnResult = True

			Return blnResult

		End Function

		Public Function GetChildren() As IEnumerable(Of Board)
			For intIndex As Integer = 0 To m_enuValues.Length - 1
				If m_enuValues(intIndex) = enuGridEntry.Empty Then
					Dim newValues As enuGridEntry() = DirectCast(m_enuValues.Clone(), enuGridEntry())
					newValues(intIndex) = If(m_blnTurnForPlayerX, enuGridEntry.PlayerX, enuGridEntry.PlayerO)
					Return New Board(newValues, Not m_blnTurnForPlayerX)
				End If
			Next
		End Function

		'http://en.wikipedia.org/wiki/Alpha-beta_pruning
		Public Function MiniMaxShortVersion(intDepth As Integer, intAlpha As Integer, intBeta As Integer, ByRef clsChildWithMax As Board) As Integer
			clsChildWithMax = Nothing
			If intDepth = 0 OrElse IsTerminalNode() Then
				'When it is turn for PlayO, we need to find the minimum score.
				RecursiveScore = m_intScore
				Return If(m_blnTurnForPlayerX, m_intScore, -m_intScore)
			End If

			For Each clsChild As Board In GetChildren()
				Dim clsDummy As Board
				Dim intScore As Integer = -clsChild.MiniMaxShortVersion(intDepth - 1, -intBeta, -intAlpha, clsDummy)
				If intAlpha < intScore Then
					intAlpha = intScore
					clsChildWithMax = clsChild
					If intAlpha >= intBeta Then
						Exit For
					End If
				End If
			Next

			RecursiveScore = intAlpha
			Return intAlpha
		End Function

		'http://www.ocf.berkeley.edu/~yosenl/extras/alphabeta/alphabeta.html
		Public Function MiniMax(intDepth As Integer, blnNeedMax As Boolean, intAlpha As Integer, intBeta As Integer, ByRef clsChildWithMax As Board) As Integer
			clsChildWithMax = Nothing
			System.Diagnostics.Debug.Assert(m_blnTurnForPlayerX = blnNeedMax)
			If intDepth = 0 OrElse IsTerminalNode() Then
				RecursiveScore = m_intScore
				Return m_intScore
			End If

			For Each clsChild As Board In GetChildren()
				Dim intDummy As Board
				Dim intScore As Integer = clsChild.MiniMax(intDepth - 1, Not blnNeedMax, intAlpha, intBeta, intDummy)
				If Not blnNeedMax Then
					If intBeta > intScore Then
						intBeta = intScore
						clsChildWithMax = clsChild
						If intAlpha >= intBeta Then
							Exit For
						End If
					End If
				Else
					If intAlpha < intScore Then
						intAlpha = intScore
						clsChildWithMax = clsChild
						If intAlpha >= intBeta Then
							Exit For
						End If
					End If
				End If
			Next

			RecursiveScore = If(blnNeedMax, intAlpha, intBeta)
			Return RecursiveScore
		End Function

		Public Function FindNextMove(intDepth As Integer) As Board
			Dim clsBoardLongVersion As Board = Nothing
			Dim clsBoardShortVersion As Board = Nothing
			MiniMaxShortVersion(intDepth, Integer.MinValue + 1, Integer.MaxValue - 1, clsBoardShortVersion)
			MiniMax(intDepth, m_blnTurnForPlayerX, Integer.MinValue + 1, Integer.MaxValue - 1, clsBoardLongVersion)

			'compare the two versions of MiniMax give the same results
			If Not IsSameBoard(clsBoardLongVersion, clsBoardShortVersion, True) Then
				Console.WriteLine("ret={0}" & vbLf & ",!= ret1={1}," & vbLf & "cur={2}", clsBoardLongVersion, clsBoardShortVersion, Me)
				Throw New Exception("Two MinMax functions don't match.")
			End If
			Return ret
		End Function

		Private Function GetScoreForOneLine(enuValues As enuGridEntry()) As Integer
			Dim intCountX As Integer = 0, intCountO As Integer = 0
			For Each enuValue As enuGridEntry In enuValues
				If enuValue = enuGridEntry.PlayerX Then
					intCountX += 1
				ElseIf enuValue = enuGridEntry.PlayerO Then
					intCountO += 1
				End If
			Next

			If intCountO = 3 OrElse intCountX = 3 Then
				GameOver = True
			End If

			'The player who has turn should have more advantage.
			'What we should have done
			Dim intAdvantage As Integer = 1
			If intCountO = 0 Then
				If m_blnTurnForPlayerX Then
					intAdvantage = 3
				End If
				Return CInt(Math.Truncate(System.Math.Pow(10, countX))) * intAdvantage
			ElseIf intCountX = 0 Then
				If Not m_blnTurnForPlayerX Then
					intAdvantage = 3
				End If
				Return -CInt(Math.Truncate(System.Math.Pow(10, countO))) * intAdvantage
			End If
			Return 0
		End Function

		Private Sub ComputeScore()
			Dim intTotal As Integer = 0
			Dim intLines As Integer(,) = {{0, 1, 2}, {3, 4, 5}, {6, 7, 8}, {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, _
				{0, 4, 8}, {2, 4, 6}}

			For intIndex As Integer = intLines.GetLowerBound(0) To intLines.GetUpperBound(0)
				intTotal += GetScoreForOneLine(New enuGridEntry() {m_enuValues(intLines(intIndex, 0)), m_enuValues(intLines(intIndex, 1)), m_enuValues(intLines(intIndex, 2))})
			Next
			m_intScore = intTotal
		End Sub

		Public Function TransformBoard(clsTransform As Transform) As Board
			Dim enuValues As enuGridEntry() = Enumerable.Repeat(enuGridEntry.Empty, 9).ToArray()
			For intIndex As Integer = 0 To 8
				Dim clsPoint As New Point(intIndex Mod 3, intIndex \ 3)
				clsPoint = clsTransform.ActOn(clsPoint)
				Dim intPointIndex As Integer = clsPoint.x + clsPoint.y * 3
				System.Diagnostics.Debug.Assert(enuValues(intPointIndex) = enuGridEntry.Empty)
				enuValues(intPointIndex) = Me.m_enuValues(intIndex)
			Next
			Return New Board(enuValues, m_blnTurnForPlayerX)
		End Function

		Private Shared Function IsSameBoard(clsA As Board, clsB As Board, blnCompareRecursiveScore As Boolean) As Boolean
			If intPointIndex Is clsB Then
				Return True
			End If
			If intPointIndex Is Nothing OrElse clsB Is Nothing Then
				Return False
			End If
			For intIndex As Integer = 0 To intPointIndex.m_enuValues.Length - 1
				If intPointIndex.m_enuValues(intIndex) <> clsB.m_enuValues(intIndex) Then
					Return False
				End If
			Next

			If intPointIndex.m_intScore <> clsB.m_intScore Then
				Return False
			End If

			If blnCompareRecursiveScore AndAlso Math.Abs(intPointIndex.RecursiveScore) <> Math.Abs(clsB.RecursiveScore) Then
				Return False
			End If

			Return True
		End Function

		Public Shared Function IsSimilarBoard(clsA As Board, clsB As Board) As Boolean
			If IsSameBoard(clsA, clsB, False) Then
				Return True
			End If

			For Each clsTransform As Transform In Transform.s_transforms
				Dim newB As Board = clsB.TransformBoard(clsTransform)
				If IsSameBoard(clsA, newB, False) Then
					Return True
				End If
			Next
			Return False
		End Function
	End Class

	Structure Point
		Public intX As Integer
		Public intY As Integer
		Public Sub New(intX0 As Integer, intY0 As Integer)
			intX = intX0
			intY = intY0
		End Sub
	End Structure
	''''''''''''''''''''''''''''''''''
	' Stopped here
	''''''''''''''''''''''''''''''''''
	Class Transform
		Const Size As Integer = 3
		Private Delegate Function TransformFunc(p As Point) As Point
		Public Shared Function Rotate90Degree(p As Point) As Point
			'012 -> x->y, y->size-x
			'012
			Return New Point(Size - p.y - 1, p.x)
		End Function
		Public Shared Function MirrorX(p As Point) As Point
			'012 -> 210
			Return New Point(Size - p.x - 1, p.y)
		End Function
		Public Shared Function MirrorY(p As Point) As Point
			Return New Point(p.x, Size - p.y - 1)
		End Function

		Private actions As New List(Of TransformFunc)()
		Public Function ActOn(p As Point) As Point
			For Each f As TransformFunc In actions
				If f IsNot Nothing Then
					p = f(p)
				End If
			Next

			Return p
		End Function

		Private Sub New(op As TransformFunc, ops As TransformFunc())
			If op IsNot Nothing Then
				actions.Add(op)
			End If
			If ops IsNot Nothing AndAlso ops.Length > 0 Then
				actions.AddRange(ops)
			End If
		End Sub
		Public Shared s_transforms As New List(Of Transform)()
		Shared Sub New()
			For i As Integer = 0 To 3
				Dim ops As TransformFunc() = Enumerable.Repeat(Of TransformFunc)(AddressOf Rotate90Degree, i).ToArray()
				s_transforms.Add(New Transform(Nothing, ops))
				s_transforms.Add(New Transform(AddressOf MirrorX, ops))
				s_transforms.Add(New Transform(AddressOf MirrorY, ops))
			Next
		End Sub
	End Class

	Class TicTacToeGame
		Public Property Current() As Board
			Get
				Return m_Current
			End Get
			Private Set(value As Board)
				m_Current = Value
			End Set
		End Property
		Private m_Current As Board
		Private init As Board

		Public Sub New()
			Dim values As enuGridEntry() = Enumerable.Repeat(enuGridEntry.Empty, 9).ToArray()
			init = New Board(values, True)
			Current = init
		End Sub

		Public Sub ComputerMakeMove(depth As Integer)
			Dim [next] As Board = Current.FindNextMove(depth)
			If [next] IsNot Nothing Then
				Current = [next]
			End If
		End Sub

		Public Function GetInitNode() As Board
			Return init
		End Function

		Public Sub GetNextMoveFromUser()
			If Current.IsTerminalNode() Then
				Return
			End If

			While True
				Try
					Console.WriteLine("Current Node is" & vbLf & "{0}" & vbLf & " Please type in x:[0-2]", Current)
					Dim x As Integer = Integer.Parse(Console.ReadLine())
					Console.WriteLine("Please type in y:[0-2]")
					Dim y As Integer = Integer.Parse(Console.ReadLine())
					Console.WriteLine("x={0},y={1}", x, y)
					Current = Current.GetChildAtPosition(x, y)
					Console.WriteLine(Current)
					Return
				Catch e As Exception
					Console.WriteLine(e.Message)
				End Try
			End While
		End Sub
	End Class

	Class Program
		Private Shared Sub Main(args As String())
			Dim game As New TicTacToeGame()
			Console.WriteLine("Winning positions for playerO:")
			Dim history As New List(Of Board)()
			Dim q As New Queue(Of Board)()
			q.Enqueue(game.GetInitNode())
			Dim total As Integer = 0
			While q.Count > 0
				Dim b As Board = q.Dequeue()
				Dim [next] As Board = b.FindNextMove(9)
				If Math.Abs(b.RecursiveScore) >= 200 AndAlso [next] IsNot Nothing Then
					If b.RecursiveScore < 0 AndAlso Not [next].GameOver AndAlso history.Find(Function(x) Board.IsSimilarBoard(x, b)) Is Nothing Then
						history.Add(b)
						Console.WriteLine("[{0}] Winner is {1}:" & vbLf & "{2}, next move is:" & vbLf & "{3}", total, If(b.RecursiveScore < 0, "PlayerO", "PlayerX"), b, [next])
						total += 1
					End If
				Else
					For Each c As Board In b.GetChildren()
						q.Enqueue(c)
					Next
				End If
			End While

			Dim [stop] As Boolean = False
			While Not [stop]
				Dim userFirst As Boolean = False
				game = New TicTacToeGame()
				Console.WriteLine("User play against computer, Do you place the first step?[y/n]")
				If Console.ReadLine().StartsWith("y", StringComparison.InvariantCultureIgnoreCase) Then
					userFirst = True
				End If

				Dim depth As Integer = 8
				Console.WriteLine("Please select level:[1..8]. 1 is easiet, 8 is hardest")
				Integer.TryParse(Console.ReadLine(), depth)

				Console.WriteLine("{0} play first, level={1}", If(userFirst, "User", "Computer"), depth)

				While Not game.Current.IsTerminalNode()
					If userFirst Then
						game.GetNextMoveFromUser()
						game.ComputerMakeMove(depth)
					Else
						game.ComputerMakeMove(depth)
						game.GetNextMoveFromUser()
					End If
				End While
				Console.WriteLine("The final result is " & vbLf & Convert.ToString(game.Current))
				If game.Current.RecursiveScore < -200 Then
					Console.WriteLine("PlayerO has won.")
				ElseIf game.Current.RecursiveScore > 200 Then
					Console.WriteLine("PlayerX has won.")
				Else
					Console.WriteLine("It is a tie.")
				End If

				Console.WriteLine("Try again?[y/n]")
				If Not Console.ReadLine().StartsWith("y", StringComparison.InvariantCultureIgnoreCase) Then
					[stop] = True
				End If
			End While

			Console.WriteLine("bye")
		End Sub
	End Class
End Namespace