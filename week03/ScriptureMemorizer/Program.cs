using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public bool IsHidden() => _isHidden;

    public void Hide() => _isHidden = true;

    public string GetDisplayText() =>
        _isHidden ? new string('_', _text.Length) : _text;
}

class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = verse;
        EndVerse = null;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public string GetDisplayText()
    {
        return EndVerse.HasValue
            ? $"{Book} {Chapter}:{StartVerse}-{EndVerse}"
            : $"{Book} {Chapter}:{StartVerse}";
    }
}

class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWords(int count)
    {
        var visibleWords = _words.Where(w => !w.IsHidden()).ToList();
        Random rand = new Random();

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = rand.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden() => _words.All(w => w.IsHidden());

    public string GetDisplayText()
    {
        string text = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()} - {text}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        SafeClear();

        var scriptures = LoadScripturesFromFile("scriptures.txt");

        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found.");
            return;
        }

        Random rand = new Random();
        Scripture scripture = scriptures[rand.Next(scriptures.Count)];
while (true)
{
    SafeClear();
    Console.WriteLine(scripture.GetDisplayText());
    Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit:");
    string? input = Console.ReadLine();


    if ( input != null && input.ToLower() == "quit")
        break;

    if (scripture.AllWordsHidden())
    {
        Console.WriteLine("\nAll words are now hidden!");
        break;
    }

    scripture.HideRandomWords(3);
}


        SafeClear();
        Console.WriteLine("Final Scripture:");
        Console.WriteLine(scripture.GetDisplayText());
    }

    static void SafeClear()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException) { }
    }

    static List<Scripture> LoadScripturesFromFile(string filename)
    {
        var scriptures = new List<Scripture>();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found: " + filename);
            return scriptures;
        }

        string[] lines = File.ReadAllLines(filename);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split('|');
            if (parts.Length != 2) continue;

            string referenceText = parts[0].Trim();
            string scriptureText = parts[1].Trim();

            string[] refParts = referenceText.Split(' ');
            string book = string.Join(" ", refParts.Take(refParts.Length - 1));
            string chapterAndVerses = refParts[refParts.Length - 1];
            string[] chapterVerse = chapterAndVerses.Split(':');
            int chapter = int.Parse(chapterVerse[0]);

            Reference reference;
            if (chapterVerse[1].Contains('-'))
            {
                string[] verses = chapterVerse[1].Split('-');
                reference = new Reference(book, chapter, int.Parse(verses[0]), int.Parse(verses[1]));
            }
            else
            {
                reference = new Reference(book, chapter, int.Parse(chapterVerse[1]));
            }

            scriptures.Add(new Scripture(reference, scriptureText));
        }

        return scriptures;
    }
}
