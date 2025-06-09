using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                BreathingActivity activity = new BreathingActivity();
                activity.Run();
            }
            else if (choice == "2")
            {
                ReflectionActivity activity = new ReflectionActivity();
                activity.Run();
            }
            else if (choice == "3")
            {
                ListingActivity activity = new ListingActivity();
                activity.Run();
            }
            else if (choice == "4")
            {
                break;
            }
        }
    }
}

// Clase base
class MindfulnessActivity
{
    protected int duration;

    public void DisplayStartMessage(string name, string description)
    {
        Console.Clear();
        Console.WriteLine($"Starting: {name}");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready...");
        PauseAnimation(3);
    }

    public void DisplayEndMessage(string name)
    {
        Console.WriteLine("Well done!");
        PauseAnimation(2);
        Console.WriteLine($"You have completed the {name} for {duration} seconds.");
        PauseAnimation(3);
    }

    protected void PauseAnimation(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Actividad de Respiración
class BreathingActivity : MindfulnessActivity
{
    public void Run()
    {
        DisplayStartMessage("Breathing Activity", 
            "This activity will help you relax by guiding you through deep breathing.\nClear your mind and focus on your breath.");

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            PauseAnimation(3);
            Console.WriteLine("Breathe out...");
            PauseAnimation(3);
        }

        DisplayEndMessage("Breathing Activity");
    }
}

// Actividad de Reflexión
class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public void Run()
    {
        DisplayStartMessage("Reflection Activity",
            "This activity will help you reflect on moments of strength and resilience.\nRecognize your power and how to apply it to your life.");

        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        PauseAnimation(3);

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine(questions[rand.Next(questions.Count)]);
            PauseAnimation(5);
        }

        DisplayEndMessage("Reflection Activity");
    }
}

// Actividad de Listado
class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are your personal strengths?",
        "Who have you helped this week?",
        "When have you felt the Spirit this month?",
        "Who are some of your personal heroes?"
    };

    public void Run()
    {
        DisplayStartMessage("Listing Activity",
            "This activity will help you reflect on the good things in your life by listing as many items as you can in a certain area.");

        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        PauseAnimation(5);

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("List item: ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                items.Add(input);
            }
        }

        Console.WriteLine($"You listed {items.Count} items!");
        DisplayEndMessage("Listing Activity");
    }
}
