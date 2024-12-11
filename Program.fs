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

// Calculate frequency of most words have been used
let getMostFrequentWords (inputText: string) =
    let words = inputText.Split([|' '; '\t'; '\n'; '\r'; '.'; ','; ';'; ':'; '!'; '?'|], StringSplitOptions.RemoveEmptyEntries)
    let frequency =
        words
        |> Array.map (fun w -> w.ToLowerInvariant())
        |> Array.groupBy id
        |> Array.map (fun (word, occurrences) -> word, occurrences.Length)
        |> Array.sortByDescending snd
    printfn "Most frequently used words:"
    frequency |> Array.take 5 |> Array.iter (fun (word, count) -> printfn "%s: %d" word count)

// Calculate Text Readability ----> (average sentence length)
let getReadability (inputText: string) =
    let sentences = inputText.Split([|'.'; '!'; '?'|], StringSplitOptions.RemoveEmptyEntries)
    let words = inputText.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    let avgSentenceLength = float words.Length / float sentences.Length
    printfn "Average sentence length: %.2f words" avgSentenceLength

[<EntryPoint>]   // Entry Point that the program will start from.
let main argv =
    printf "Please enter some text: "
    let inputText = Console.ReadLine()
    match inputText with
    | null | "" ->
        printfn "No input provided, please provide an input."
    | _ ->
        getWordCount inputText              // number of the words
        getSentenceCount inputText          // number of the sentences
        getParagraphCount inputText         // lenght of the paragraphs
        getWordFrequency inputText          // Frequency of the words
        getMostFrequentWords inputText      // Frequency of most words used
        getReadability inputText            // Readability such as (avg. sentence length)
    0
