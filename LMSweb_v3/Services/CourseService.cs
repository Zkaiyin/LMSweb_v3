using LMSweb_v3.ViewModels;
using LMSweb_v3.ViewModels.Course;
using LMSwebDB.Models;
using LMSwebDB.Repositories;
using Microsoft.Identity.Client;

namespace LMSweb_v3.Services;

public class CourseService
{
    private readonly LMSRepository _repo;

    public CourseService(LMSRepository repo)
    {
        _repo = repo;
    }

    public List<TeacherHomeViewModel> GetCourses(string teacherId)
    {
        var courses = _repo.Query<Course>()
                    .Where(c => c.TeacherId == teacherId)
                    .Select(x => new TeacherHomeViewModel
                    {
                        CourseId = x.CourseId,
                        CourseName = x.CourseName
                    }).ToList();
        return courses;
    }

    public string CreateCourse(string teacherId, string courseName)
    {
        var course = new Course
        {
            TeacherId = teacherId,
            CreateTime = DateTime.Now,
            CourseId = $"C{DateTime.Now:yyMMddHHmmss}",
            CourseName = courseName,
            SystemPrompt = """
            #zh-tw
            你是一位有幫助的AI助教，以下是對話紀錄:
            <!context>
            """,
            UserPrompt = """"
            以下為已發生事實的資料，請自行判斷是否需要參考資料，不要產生多餘的文字
            """
            <!reference_data>
            """
            <!question>
            """",
            GreetingMessage = "嗨~ 今天想問什麼問題呢?",
            Temperature = 0.75f
        };
        _repo.Create(course);
        _repo.SaveChanges();
        return course.CourseId;
    }

    public CourseEditViewModel? GetCourseEditViewModel(string courseId)
    {
        var course = _repo.Query<Course>().FirstOrDefault(x => x.CourseId == courseId);
        if (course == null)
        {
            return null;
        }
        return new CourseEditViewModel
        {
            CourseName = course.CourseName
        };
    }

    public bool EditCourse(string courseId, CourseEditViewModel courseViewModel)
    {
        var course = _repo.Query<Course>().FirstOrDefault(x => x.CourseId == courseId);
        if (course == null)
        {
            return false;
        }
        course.CourseName = courseViewModel.CourseName;
        _repo.Update(course);
        _repo.SaveChanges();
        return true;
    }

    public CourseDeleteViewModel? GetWillBeDeleteCourse(string courseId, string teacherId)
    {
        var course = (from c in _repo.Query<Course>()
                      join t in _repo.Query<Teacher>() on c.TeacherId equals teacherId
                      where c.CourseId == courseId
                      select new CourseDeleteViewModel
                      {
                          TeacherName = t.TeacherName,
                          CourseID = c.CourseId,
                          CourseName = c.CourseName
                      }).FirstOrDefault();
        return course;
    }   

    public bool DeleteCourse(string courseId)
    {
        if (string.IsNullOrEmpty(courseId))
        {
            return false;
        }
        // 透過id找到課程，並將資料從資料庫刪除
        var course = _repo.Query<Course>().FirstOrDefault(x => x.CourseId == courseId);
        if (course == null)
        {
            return false;
        }
        else
        {
            // 依據 CourseStudents_list 刪除 student 表中的資料
            var CourseStudents_list = _repo.Query<Course>().Where(x => x.CourseId == courseId).ToList();
            var CourseStudents = _repo.Query<Student>().Where(x => x.CourseId == courseId).ToList();
            var QALogsInCourse = _repo.Query<UserQALog>().Where(x => x.CourseId == courseId).ToList();
            _repo.DeleteMany(CourseStudents);
            _repo.DeleteMany(QALogsInCourse);
            _repo.SaveChanges();

            // 依據 CourseStudents_list 刪除 user 表中的資料
            foreach (var student in CourseStudents)
            {
                var user = _repo.Query<User>().FirstOrDefault(x => x.UserId == student.StudentId);
                if (user != null)
                    _repo.Delete(user);
            }
            _repo.SaveChanges();

            _repo.Delete(course);
            _repo.SaveChanges();
            return true;
        }
    }

    public PromptManageViewModel? GetCourseDefaultPrompt(string courseId)
    {
        var course = _repo.Query<Course>().FirstOrDefault(x => x.CourseId == courseId);
        if (course == null)
        {
            return null;
        }
        var data = new PromptManageViewModel
        {
            CourseId = courseId,
            CourseName = course.CourseName,
            SystemPrompt = course.SystemPrompt ?? "#zh-tw\n你是一位有幫助的AI助教",
            UserPrompt = course.UserPrompt ?? """"
            以下為已發生事實的資料，請自行判斷是否需要參考資料，不要產生多餘的文字
            """
            <!reference_data>
            """
            <!question>
            """",
            Greeting = course.GreetingMessage ?? "嗨~ 今天想問什麼問題呢?",
            Temperature = course.Temperature,
            IsNeedContext = course.IsNeedContext,
            LLMModel = course.LLMModel ?? ""
        };
        return data;
    }

    public bool UpdateDefaultPrompt(PromptManageViewModel prompt)
    {
        var course = _repo.Query<Course>().FirstOrDefault(x => x.CourseId == prompt.CourseId);
        if (course != null)
        {
            course.SystemPrompt = prompt.SystemPrompt;
            course.UserPrompt = prompt.UserPrompt;
            course.GreetingMessage = prompt.Greeting;
            course.Temperature = prompt.Temperature;
            course.IsNeedContext = prompt.IsNeedContext;
            course.LLMModel = prompt.LLMModel;
            _repo.Update(course);
            _repo.SaveChanges();
            return true;
        }
        return false;
    }

    internal void AddPrompt(LMSwebDB.Models.Prompt prompt)
    {
        throw new NotImplementedException();
    }

    internal void AddPrompt(Microsoft.Identity.Client.Prompt prompt)
    {
        throw new NotImplementedException();
    }
}
