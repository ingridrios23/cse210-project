using System;
using System.Collections.Generic;

abstract class Activity
{
    private DateTime date;
    private int duration; // in minutes

    public Activity(DateTime date, int duration)
    {
        this.date = date;
        this.duration = duration;
    }

    public DateTime GetDate() => date;
    public int GetDuration() => duration;

    public abstract double GetDistance(); // km
    public abstract double GetSpeed();    // km/h
    public abstract double GetPace();     // min/km

    public virtual string GetSummary()
    {
        return $"{date:dd MMM yyyy} {this.GetType().Name} ({duration} min): " +
               $"Distance: {GetDistance():0.0} km, " +
               $"Speed: {GetSpeed():0.0} km/h, " +
               $"Pace: {GetPace():0.00} min/km";
    }
}

class Running : Activity
{
    private double distance; // in km

    public Running(DateTime date, int duration, double distance)
        : base(date, duration)
    {
        this.distance = distance;
    }

    public override double GetDistance() => distance;
    public override double GetSpeed() => (distance / GetDuration()) * 60;
    public override double GetPace() => GetDuration() / distance;
}

class Cycling : Activity
{
    private double speed; // in km/h

    public Cycling(DateTime date, int duration, double speed)
        : base(date, duration)
    {
        this.speed = speed;
    }

    public override double GetSpeed() => speed;
    public override double GetDistance() => (speed * GetDuration()) / 60;
    public override double GetPace() => 60 / speed;
}

class Swimming : Activity
{
    private int laps; // number of laps

    public Swimming(DateTime date, int duration, int laps)
        : base(date, duration)
    {
        this.laps = laps;
    }

    public override double GetDistance() => laps * 50.0 / 1000; // km
    public override double GetSpeed() => (GetDistance() / GetDuration()) * 60;
    public override double GetPace() => GetDuration() / GetDistance();
}

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 4.8),
            new Cycling(new DateTime(2022, 11, 3), 30, 9.7),
            new Swimming(new DateTime(2022, 11, 3), 30, 20)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
