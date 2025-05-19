using System;

public class Entry
{
    public string Date { get; private set; }
    public string Prompt { get; private set; }
    public string Response { get; private set; }
    public string Mood { get; private set; }

    public Entry(string prompt, string response, string mood)
    {
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        Prompt = prompt;
        Response = response;
        Mood = mood;
    }

    public string ToSaveString()
    {
        return $"{Date}|{Prompt}|{Response}|{Mood}";
    }

    public static Entry FromSaveString(string line)
    {
        string[] parts = line.Split('|');
        if (parts.Length == 4)
        {
            Entry entry = new Entry(parts[1], parts[2], parts[3]);
            entry.Date = parts[0]; // overwrite current date with loaded date
            return entry;
        }
        return null;
    }

    public void Display()
    {
        Console.WriteLine($"[{Date}] ({Mood})\nPrompt: {Prompt}\nResponse: {Response}\n");
    }
}
