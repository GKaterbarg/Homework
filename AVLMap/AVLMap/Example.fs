module Example
    
    open MyMap
    open System
    open System.Diagnostics

    let mutable myDictionary = MyMap.Map<_, _> [|(2, "b"); (3, "c"); (4, "d")|]
    myDictionary <- myDictionary.Add 1 "a"
    myDictionary <- myDictionary.Remove 4
    printfn "%A" (myDictionary.Item(1))
    for i in myDictionary do printfn "%A" i

    Console.ReadLine() |> ignore
