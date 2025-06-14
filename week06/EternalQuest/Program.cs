using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int Points { get; protected set; }
    public bool IsComplete { get; set; }  // <-- permitir set público para deserializar

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsComplete = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string Serialize();

    public static Goal? Deserialize(string line)
    {
        try
        {
            var parts = line.Split('|');
            if (parts.Length < 5) return null;

            string type = parts[0];
            string name = parts[1];
            string desc = parts[2];
            int pts = int.Parse(parts[3]);
            bool complete = bool.Parse(parts[4]);

            switch (type)
            {
                case "SimpleGoal":
                    var sg = new SimpleGoal(name, desc, pts);
                    sg.IsComplete = complete;
                    return sg;

                case "EternalGoal":
                    var eg = new EternalGoal(name, desc, pts);
                    eg.IsComplete = complete;
                    return eg;

                case "ChecklistGoal":
                    if (parts.Length < 8) return null;
                    int currentCount = int.Parse(parts[5]);
                    int targetCount = int.Parse(parts[6]);
                    int bonusPoints = int.Parse(parts[7]);
                    var cg = new ChecklistGoal(name, desc, pts, targetCount, bonusPoints);
                    cg.CurrentCount = currentCount;
                    cg.IsComplete = complete;
                    return cg;

                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            return Points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        return IsComplete ? $"[X] {Name} (Simple Goal)" : $"[ ] {Name} (Simple Goal)";
    }

    public override string Serialize()
    {
        return $"SimpleGoal|{Name}|{Description}|{Points}|{IsComplete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent()
    {
        return Points;
    }

    public override string GetStatus()
    {
        return $"[∞] {Name} (Eternal Goal)";
    }

    public override string Serialize()
    {
        return $"EternalGoal|{Name}|{Description}|{Points}|{IsComplete}";
    }
}

class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CurrentCount { get; set; }
    public int BonusPoints { get; private set; }

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
        CurrentCount = 0;
    }

    public override int RecordEvent()
    {
        if (IsComplete) return 0;

        CurrentCount++;
        int earnedPoints = Points;

        if (CurrentCount >= TargetCount)
        {
            IsComplete = true;
            earnedPoints += BonusPoints;
        }
        return earnedPoints;
    }

    public override string GetStatus()
    {
        return IsComplete
            ? $"[X] {Name} (Checklist Goal) Completed {CurrentCount}/{TargetCount} times - Bonus earned"
            : $"[ ] {Name} (Checklist Goal) Completed {CurrentCount}/{TargetCount} times";
    }

    public override string Serialize()
    {
        return $"ChecklistGoal|{Name}|{Description}|{Points}|{IsComplete}|{CurrentCount}|{TargetCount}|{BonusPoints}";
    }
}

class User
{
    public string UserName { get; private set; }
    private List<Goal> goals = new();
    public int TotalPoints { get; set; }
    public int Level => TotalPoints / 1000;

    public User(string userName)
    {
        UserName = userName;
        TotalPoints = 0;
    }

    public void AddGoal(Goal goal)
    {
        if (goal != null)
            goals.Add(goal);
    }

    public int RecordGoalEvent(int goalIndex)
    {
        if (goalIndex < 0 || goalIndex >= goals.Count)
            throw new ArgumentOutOfRangeException("Invalid goal number.");

        int pts = goals[goalIndex].RecordEvent();
        TotalPoints += pts;
        return pts;
    }

    public void ShowGoals()
    {
        Console.WriteLine($"\nUser: {UserName}");
        Console.WriteLine("Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
        Console.WriteLine($"Total Points: {TotalPoints} | Level: {Level}");
    }

    public void Save(string filename)
    {
        using StreamWriter sw = new(filename);
        sw.WriteLine($"{UserName}|{TotalPoints}");
        foreach (var g in goals)
        {
            sw.WriteLine(g.Serialize());
        }
    }

    public static User? Load(string filename)
    {
        try
        {
            if (!File.Exists(filename)) return null;
            var lines = File.ReadAllLines(filename);
            if (lines.Length < 1) return null;

            var header = lines[0].Split('|');
            if (header.Length < 2) return null;

            var user = new User(header[0]);
            user.TotalPoints = int.Parse(header[1]);

            for (int i = 1; i < lines.Length; i++)
            {
                var goal = Goal.Deserialize(lines[i]);
                if (goal != null)
                {
                    user.AddGoal(goal);
                }
            }
            return user;
        }
        catch
        {
            return null;
        }
    }
}

class Program
{
    static void Main()
    {
        string saveFile = "goals.txt";
        User user = User.Load(saveFile) ?? new User("Ingrid");

        if (user.Level == 0 && user.TotalPoints == 0)
        {
            user.AddGoal(new SimpleGoal("Run a Marathon", "Complete a marathon to get 1000 points", 1000));
            user.AddGoal(new EternalGoal("Daily Scripture Study", "Study scriptures daily", 100));
            user.AddGoal(new ChecklistGoal("Temple Visits", "Visit temple 10 times", 50, 10, 500));
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Eternal Quest: Goal Tracker ===");
            Console.WriteLine("1. Show Goals");
            Console.WriteLine("2. Add New Goal");
            Console.WriteLine("3. Load Goals from File");
            Console.WriteLine("4. Record Goal Event");
            Console.WriteLine("5. Save Goals to File");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

      string? choice = Console.ReadLine();
      if (choice == null) return;

            switch (choice)
            {
                case "1":
                    user.ShowGoals();
                    break;

                case "2":
                    Console.Write("Goal Type (1: Simple, 2: Eternal, 3: Checklist): ");
string? type = Console.ReadLine();
if (string.IsNullOrWhiteSpace(type))
{
    Console.WriteLine("Invalid input for goal type.");
    break; // O return, según contexto
}

Console.Write("Enter goal name: ");
string? name = Console.ReadLine();
if (string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine("Goal name cannot be empty.");
    break;
}

Console.Write("Enter description: ");
string? desc = Console.ReadLine();
if (string.IsNullOrWhiteSpace(desc))
{
    Console.WriteLine("Description cannot be empty.");
    break;
}

Console.Write("Enter points: ");
string? ptsInput = Console.ReadLine();
if (!int.TryParse(ptsInput, out int pts))
{
    Console.WriteLine("Points must be a valid number.");
    break;
}

                    if (type == "1")
                        user.AddGoal(new SimpleGoal(name, desc, pts));
                    else if (type == "2")
                        user.AddGoal(new EternalGoal(name, desc, pts));
                    else if (type == "3")
                    {
                        Console.Write("Target count: ");
                        if (!int.TryParse(Console.ReadLine(), out int target)) break;
                        Console.Write("Bonus points: ");
                        if (!int.TryParse(Console.ReadLine(), out int bonus)) break;
                        user.AddGoal(new ChecklistGoal(name, desc, pts, target, bonus));
                    }
                    break;

                case "3":
                    var loaded = User.Load(saveFile);
                    if (loaded != null)
                    {
                        user = loaded;
                        Console.WriteLine("Goals loaded successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No saved goals found or file error.");
                    }
                    break;

                case "4":
                    user.ShowGoals();
                    Console.Write("Enter goal number: ");
                    if (int.TryParse(Console.ReadLine(), out int index))
                    {
                        try
                        {
                            int earned = user.RecordGoalEvent(index - 1);
                            Console.WriteLine($"You earned {earned} points!");
                        }
                        catch
                        {
                            Console.WriteLine("Invalid goal number.");
                        }
                    }
                    break;

                case "5":
                    user.Save(saveFile);
                    Console.WriteLine("Goals saved.");
                    break;

                case "6":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
