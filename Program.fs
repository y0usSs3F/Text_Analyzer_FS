﻿open System

// Character length
let getInputAndDisplayLength (inputText: string) =
    printfn "The length of the input text is: %d" inputText.Length

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
        getParagraphCount inputText         // lenght of the paragraphs
        
    0
