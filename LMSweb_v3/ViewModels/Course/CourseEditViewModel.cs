using System.ComponentModel.DataAnnotations;

namespace LMSweb_v3.ViewModels.Course;

public class CourseEditViewModel
{
    [Display(Name = "課程名稱")]
    public string CourseName { get; set; } = null!;
}