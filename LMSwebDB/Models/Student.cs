namespace LMSwebDB.Models;

public class Student
{
    public string StudentId { get; set; } = null!;

    public string? CourseId { get; set; }

    public string StudentName { get; set; } = null!;

    public string? Gender { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User StudentNavigation { get; set; } = null!;
}
