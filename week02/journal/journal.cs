using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries to display.");
        }
        else
        {
            foreach (Entry entry in entries)
            {
                entry.Display();
            }
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in entries)
            {
                writer.WriteLine(entry.ToSaveString());
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            Entry entry = Entry.FromSaveString(line);
            if (entry != null)
            {
                entries.Add(entry);
            }
        }
    }
}
