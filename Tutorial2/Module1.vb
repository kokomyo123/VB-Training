Module Module1

    Sub Main()
        Dim year As Integer
        Console.Write("Input year : ")
        year = Convert.ToInt32(Console.ReadLine())
        GetCenturyAndCheckLeapYear(year)
        Console.ReadLine()
    End Sub

    Public Sub GetCenturyAndCheckLeapYear(ByVal year As Integer)
        Dim century As Integer

        If year Mod 400 = 0 OrElse year >= 1000 AndAlso year Mod 4 = 0 AndAlso year Mod 100 <> 0 Then
            century = (year / 100) + 1
            Console.WriteLine(century & "," & 1)
        Else
            century = (year / 100) + 1
            Console.WriteLine(century & "," & (-1))
        End If
    End Sub

End Module