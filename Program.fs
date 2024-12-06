open System

let getInputAndDisplayLength (inputText: string) =
    printfn "The length of the input text is: %d" inputText.Length

let getWordCount (inputText: string) =
    let words = inputText.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    printfn "The number of words is: %d" words.Length

let getSentenceCount (inputText: string) =
    let sentences = inputText.Split([|'.'; '!'; '?'|], StringSplitOptions.RemoveEmptyEntries)
    printfn "The number of sentences is: %d" sentences.Length

[<EntryPoint>]   // Entry Point that the program will start from.
let main argv =
    printf "Please enter some text: "
    let inputText = Console.ReadLine()
    match inputText with
    | null | "" ->
        printfn "No input provided."
    | _ ->
        getInputAndDisplayLength inputText  // lenght of the characters
        getWordCount inputText              // number of the words
        getSentenceCount inputText          // number of the sentences

    0
