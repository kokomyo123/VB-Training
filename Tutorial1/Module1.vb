Module Module1

    Sub Main()
        Dim i, j, r As Integer
        r = 6

        For i = 0 To r

            For j = 1 To r - i
                Console.Write(" ")
            Next

            For j = 1 To 2 * i - 1
                Console.Write("*")
            Next

            Console.Write(vbLf)
        Next

        For i = r - 1 To 0 Step -1

            For j = 1 To r - i
                Console.Write(" ")
            Next

            For j = 1 To 2 * i - 1
                Console.Write("*")
            Next

            Console.Write(vbLf)
        Next

        Console.ReadLine()
    End Sub

End Module