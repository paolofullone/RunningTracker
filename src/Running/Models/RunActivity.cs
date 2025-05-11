namespace RunningTracker.Models;

public class RunActivity
{
    public int Id { get; set; }
    public double Distance { get; set; }
    public TimeSpan Duration { get; set; }
    public double Pace { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
