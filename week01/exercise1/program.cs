using System;

class Program
{
    static void Main(string[] args)
    {
        //ask the user for their name.
        Console.Write("what is your frist name?");
         string first = Console.ReadLine();

         Console.Write("what is your last name?");
         string last =Console.ReadLine();

         Console.WriteLine($"your name is {last}, {first} {last}");
    }
}
