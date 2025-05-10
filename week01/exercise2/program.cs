using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage (0-100): ");
        string input = Console.ReadLine();
        int percentage = int.Parse(input);

        string letter = "";
        string sign = "";

        // Determine letter grade
        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine plus/minus sign
        int lastDigit = percentage % 10;

        if (letter != "F")
        {
            if (letter != "A")
            {
                if (lastDigit >= 7)
                {
                    sign = "+";
                }
                else if (lastDigit < 3)
                {
                    sign = "-";
                }
            }
            else // letter == "A"
            {
                if (lastDigit < 3)
                {
                    sign = "-";
                }
            }
        }

        // Output final result
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Message based on pass/fail
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("You didn't pass this time, but keep trying—you'll get it next time!");
        }
    }
}
