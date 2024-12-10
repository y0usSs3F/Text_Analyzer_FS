open System

// Number of words
let getWordCount (inputText: string) =
    let words = inputText.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    printfn "The number of words is: %d" words.Length

// Number of sentences
let getSentenceCount (inputText: string) =
    let sentences = inputText.Split([|'.'; '!'; '?'|], StringSplitOptions.RemoveEmptyEntries)
    printfn "The number of sentences is: %d" sentences.Length

// Number of paragraphs
let getParagraphCount (inputText: string) =
    let paragraphs = inputText.Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries)
    printfn "The number of paragraphs is: %d" paragraphs.Length

// Calculate word frequency
let getWordFrequency (inputText: string) =
    let words = inputText.Split([|' '; '\t'; '\n'; '\r'; '.'; ','; ';'; ':'; '!'; '?'|], StringSplitOptions.RemoveEmptyEntries)
    let frequency =
        words
        |> Array.map (fun w -> w.ToLowerInvariant())
        |> Array.groupBy id
        |> Array.map (fun (word, occurrences) -> word, occurrences.Length)
    printfn "Word frequencies:"
    frequency |> Array.iter (fun (word, count) -> printfn "%s: %d" word count)

[<EntryPoint>]   // Entry Point that the program will start from.
let main argv =
    printf "Please enter some text: "
    let inputText = Console.ReadLine()
    match inputText with
    | null | "" ->
        printfn "No input provided."
    | _ ->
        getWordCount inputText              // number of the words
        getSentenceCount inputText          // number of the sentences
        getParagraphCount inputText         // lenght of the paragraphs
        getWordFrequency inputText          // Frequency of the words
                
    0
