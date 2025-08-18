using System;

public class TaskItem
{
    public string Content { get; set; }
    public bool IsImportant { get; set; }
    public bool IsDone { get; set; }
    public DateTime StartTime { get; set; } // Görev eklenme zamanı
}
