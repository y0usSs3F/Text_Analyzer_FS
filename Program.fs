open System
open System.IO
open System.Windows.Forms
open System.Drawing

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
    getWordFrequency inputText
    |> Array.sortByDescending snd
    |> Array.take 5

// Text readability ---> (average sentence length)
let getReadability (inputText: string) =
    let sentences = getSentenceCount inputText
    let words = getWordCount inputText
    if sentences > 0 then float words / float sentences
    else 0.0

// GUI
let buildTextAnalysisApp () =
    let form = new Form(Text = "Text Analyzer", Width = 1000, Height = 800, StartPosition = FormStartPosition.CenterScreen)

    let inputTextBox = new TextBox(Multiline = true, Dock = DockStyle.Bottom, Height = 150, Font = new Font("Arial", 12.0f), ScrollBars = ScrollBars.Vertical)
    form.Controls.Add(inputTextBox)

    let buttonPanel = new FlowLayoutPanel(Dock = DockStyle.Right, Width = 150, AutoSize = true)
    buttonPanel.FlowDirection <- FlowDirection.LeftToRight
    form.Controls.Add(buttonPanel)

    let loadButton = new Button(Text = "load File", Width = 120, Height = 40, BackColor = Color.LightPink)
    let analyzeButton = new Button(Text = "Make Analysis", Width = 120, Height = 40, BackColor = Color.LightGreen)
    let saveButton = new Button(Text = "Save Results", Width = 120, Height = 40, BackColor = Color.LightYellow)
    let clearButton = new Button(Text = "Clear", Width = 120, Height = 40, BackColor = Color.Orange)

    buttonPanel.Controls.AddRange([| loadButton; saveButton; analyzeButton; clearButton |])

    let resultTextBox = new RichTextBox(
        Multiline = true,
        ReadOnly = true,
        Dock = DockStyle.Fill,
        Font = new Font("Courier New", 12.0f, FontStyle.Bold),
        ScrollBars = RichTextBoxScrollBars.Vertical,
        BackColor = Color.LightCyan
    )
    form.Controls.Add(resultTextBox)

    let appendFormattedText (text: string) (color: Color) (fontSize: float32) =
        resultTextBox.SelectionColor <- color
        resultTextBox.SelectionFont <- new Font("Verdana", fontSize, FontStyle.Bold)
        resultTextBox.SelectionAlignment <- HorizontalAlignment.Center
        resultTextBox.AppendText(text + "\n")

    analyzeButton.Click.Add(fun _ ->
        let inputText = inputTextBox.Text
        if String.IsNullOrWhiteSpace inputText then
            resultTextBox.Text <- "Please enter some text or load a file."
        else
            let wordCount = getWordCount inputText
            let sentenceCount = getSentenceCount inputText
            let paragraphCount = getParagraphCount inputText
            let avgSentenceLength = getReadability inputText
            let wordFreq = getWordFrequency inputText
            let mostFrequentWords = getMostFrequentWords inputText

            let frequencyText =
                wordFreq
                |> Array.map (fun (word, freq) -> sprintf "%s: %d" word freq)
                |> String.concat Environment.NewLine

            let mostFrequentText =
                mostFrequentWords
                |> Array.map (fun (word, freq) -> sprintf "%s: %d" word freq)
                |> String.concat Environment.NewLine

            appendFormattedText "\nAnalysis Results\n" Color.Red 20.0f
            appendFormattedText (sprintf "Words: %d" wordCount) Color.Black 13.0f
            appendFormattedText (sprintf "Sentences: %d" sentenceCount) Color.Black 13.0f
            appendFormattedText (sprintf "Paragraphs: %d" paragraphCount) Color.Black 13.0f
            appendFormattedText (sprintf "Average Sentence Length: %.2f" avgSentenceLength) Color.Black 13.0f
            appendFormattedText "\nMost Frequent Words\n" Color.Red 20.0f
            appendFormattedText mostFrequentText Color.Black 13.0f
            appendFormattedText "\nWord Frequency\n" Color.Red 20.0f
            appendFormattedText frequencyText Color.Black 13.0f
    )

    loadButton.Click.Add(fun _ ->
        let openFileDialog = new OpenFileDialog(Filter = "TXT files|*.txt")
        if openFileDialog.ShowDialog() = DialogResult.OK then
            let filePath = openFileDialog.FileName
            inputTextBox.Text <- File.ReadAllText(filePath)
    )

    clearButton.Click.Add(fun _ ->
        inputTextBox.Text <- ""
        resultTextBox.Text <- ""
    )

    saveButton.Click.Add(fun _ ->
        if String.IsNullOrWhiteSpace resultTextBox.Text then
            MessageBox.Show("No results to save. Please analyze some text first.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
        else
            let saveFileDialog = new SaveFileDialog(Filter = "Text Files (.txt)|.txt")
            if saveFileDialog.ShowDialog() = DialogResult.OK then
                let filePath = saveFileDialog.FileName
                File.WriteAllText(filePath, resultTextBox.Text)
                MessageBox.Show("Results saved successfully!", "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
    )

    Application.Run(form)

[<STAThread>]
[<EntryPoint>]
let main _ =
    buildTextAnalysisApp()
    0