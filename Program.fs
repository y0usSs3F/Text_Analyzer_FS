open System

let getInputAndDisplayLength (inputText: string) =
    printfn "The length of the input text is: %d" inputText.Length


[<EntryPoint>]  
let main argv =
    printf "Please enter some text: "
    let inputText = Console.ReadLine()
    match inputText with
    | null | "" ->
        printfn "No input provided."
    | _ ->
        getInputAndDisplayLength inputText 
    0