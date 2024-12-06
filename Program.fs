open System

let getInputAndDisplayLength (inputText: string) =
    printfn "The length of the input text is: %d" inputText.Length

let getWordCount (inputText: string) =
    let words = inputText.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    printfn "The number of words is: %d" words.Length

let getSentenceCount (inputText: string) =
    let sentences = inputText.Split([|'.'; '!'; '?'|], StringSplitOptions.RemoveEmptyEntries)
    printfn "The number of sentences is: %d" sentences.Length

[<EntryPoint>]  
let main argv =
    printf "Please enter some text: "
    let inputText = Console.ReadLine()
    match inputText with
    | null | "" ->
        printfn "No input provided."
    | _ ->
        getInputAndDisplayLength inputText 
        getWordCount inputText
        getSentenceCount inputText

    0
