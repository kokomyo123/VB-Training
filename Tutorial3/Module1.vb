Module Module1
    Dim first, second, result As Integer
    Dim opt As String

    Sub Main()

        Console.Write("Pls write Console Calculator in C# as like below:" & vbLf)
        Console.Write("---------------------------------------------------------")
        Console.Write(vbLf & vbLf)
        Console.WriteLine("Type a number,then press Enter")
        first = Convert.ToInt32(Console.ReadLine())
        Console.Write(vbLf & vbLf)
        Console.WriteLine("Type another number,then press Enter ")
        second = Convert.ToInt32(Console.ReadLine())
        Console.Write(vbLf & vbLf)
        Console.WriteLine("Choose an option from the following list:")
        Console.WriteLine("a-Add" & vbLf & "s-Subtract" & vbLf & "m-Multiply" & vbLf & "d-Divide")
        Console.Write("Your option?")
        opt = Console.ReadLine()
        Calculate(opt)

        Console.ReadLine()
    End Sub

    Public Sub Calculate(ByVal num As String)
        Select Case num
            Case "a"
                result = first + second
                Console.WriteLine("Your result:{0} + {1} = {2}", first, second, result)
            Case "s"
                result = first - second
                Console.WriteLine("Your result:{0} - {1} = {2}", first, second, result)
            Case "m"
                result = first * second
                Console.WriteLine("Your result:{0} x {1} = {2}", first, second, result)
            Case "d"
                result = first / second
                Console.WriteLine("Your result:{0} ÷ {1} = {2}", first, second, result)
            Case Else
                Console.WriteLine("Please choose only from opiton")
        End Select
    End Sub

End Module