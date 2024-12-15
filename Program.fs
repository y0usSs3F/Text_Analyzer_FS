open System

// Helper to split text by delimiters
let splitText (inputText: string) (delimiters: char[]) =
    inputText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)

// Number of words
let getWordCount (inputText: string) =
    let words = splitText inputText [|' '; '\t'; '\n'; '\r'|]
    words.Length

// Number of sentences
let getSentenceCount (inputText: string) =
    let sentences = splitText inputText [|'.'; '!'; '?'|]
    sentences.Length

// Number of paragraphs
let getParagraphCount (inputText: string) =
    let paragraphs = splitText inputText [|'\n'|]
    paragraphs.Length

// Calculate word frequency
let getWordFrequency (inputText: string) =
    let words = splitText inputText [|' '; '\t'; '\n'; '\r'; '.'; ','; ';'; ':'; '!'; '?'|]
    words
    |> Array.map (fun w -> w.ToLowerInvariant())
    |> Array.groupBy id
    |> Array.map (fun (word, occurrences) -> word, occurrences.Length)

// Most frequent words
let getMostFrequentWords (inputText: string) =
    let frequency = 
        getWordFrequency inputText
        |> Array.sortByDescending snd
    let count = min 5 frequency.Length
    frequency |> Array.take count

// Text readability (average sentence length)
let getReadability (inputText: string) =
    let sentences = getSentenceCount inputText
    let words = getWordCount inputText
    if sentences > 0 then Some(float words / float sentences)
    else None

[<EntryPoint>]  // Entry Point that the program will start from.
let main argv =
    printf "Please enter some text: "
    let inputText = Console.ReadLine()
    match inputText with
    | null | "" ->
        printfn "No input provided. Please provide some text."
    | _ ->
        printfn "Word Count: %d" (getWordCount inputText)
        printfn "Sentence Count: %d" (getSentenceCount inputText)
        printfn "Paragraph Count: %d" (getParagraphCount inputText)
        printfn "Word Frequencies:"
        getWordFrequency inputText |> Array.iter (fun (word, count) -> printfn "%s: %d" word count)
        printfn "Most Frequent Words:"
        getMostFrequentWords inputText |> Array.iter (fun (word, count) -> printfn "%s: %d" word count)
        match getReadability inputText with
        | Some avg -> printfn "Average Sentence Length: %.2f words" avg
        | None -> printfn "Readability cannot be calculated (no sentences)."
    0
