using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("How to Cook Perfect Rice", "Chef Juan", 420);
        video1.AddComment(new Comment("Laura", "Delicious! It worked perfectly."));
        video1.AddComment(new Comment("Carlos", "Very easy to follow."));
        video1.AddComment(new Comment("Maria", "Thanks for sharing."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Introduction to C#", "CodeAcademy", 880);
        video2.AddComment(new Comment("Peter", "Now I understand classes."));
        video2.AddComment(new Comment("Anna", "Great explanation."));
        video2.AddComment(new Comment("Luis", "Perfect for beginners."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Stretching Exercises", "FitLife", 600);
        video3.AddComment(new Comment("Carmen", "Helped with my back pain."));
        video3.AddComment(new Comment("Joseph", "Great for mornings."));
        video3.AddComment(new Comment("Helen", "Thanks, I feel better."));
        videos.Add(video3);

        // Video 4
        Video video4 = new Video("Trip to Machu Picchu", "Andean Traveler", 720);
        video4.AddComment(new Comment("Agnes", "What a beautiful place!"));
        video4.AddComment(new Comment("Raul", "Thanks for showing us your journey."));
        video4.AddComment(new Comment("Sophia", "Very nice footage."));
        videos.Add(video4);

        // Display all video info
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
            }
            Console.WriteLine(new string('-', 40));
        }
    }
}
