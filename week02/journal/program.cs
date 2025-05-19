using System;
using System.Collections.Generic;

// This program meets all requirements for the journal assignment.
// Creative additions include:
// - Tracking mood for each entry
// - Saving and loading custom-formatted entries
// - Randomized prompts from a list of 5+ options

class Program
{
    static List<string> prompts = new List<string>
    {
        "Who was the most interesting person you interacted with today?",
        "What was the best part of your day?",
        "How did you see the hand of the Lord in your life today?",
        "What was the strongest emotion you felt today?",
        "If you had to do one thing today, what would it be?",
        "What did you learn about yourself today?",
        "What challenge did you overcome today?"
    };

    static void Main()
    {
        Journal journal = new Journal();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = GetRandomPrompt();
                    Console.WriteLine("\nPrompt: " + prompt);
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    Console.Write("How are you feeling (mood): ");
                    string mood = Console.ReadLine();

                    Entry entry = new Entry(prompt, response, mood);
                    journal.AddEntry(entry);
                    break;

                case "2":
                    journal.DisplayAll();
                    break;

                case "3":
                    Console.Write("Enter filename to save (e.g., journal.txt): ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    Console.WriteLine("Journal saved successfully.");
                    break;

                case "4":
                    Console.Write("Enter filename to load (e.g., journal.txt): ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    Console.WriteLine("Journal loaded successfully.");
                    break;

                case "5":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}
