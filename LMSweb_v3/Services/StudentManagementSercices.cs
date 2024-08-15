using LMSweb_v3.ViewModels;
using LMSweb_v3.ViewModels.StudentManagement;
using LMSwebDB.Helper;
using LMSwebDB.Models;
using LMSwebDB.Repositories;

namespace LMSweb_v3.Services
{
    public class StudentManagementSercices
    {
        private readonly LMSRepository _context;

        public StudentManagementSercices(LMSRepository context)
        {
            _context = context;
        }

        /// <summary>
        /// 取得某一門課底下所有學生
        /// </summary>
        /// <param name="cid">課程編號</param>
        /// <returns>某一門課底下所有學生</returns>
        public StudentManagementViewModel? GetStudents(string cid)
        {
            var vm = _context.Query<Course>().Select(
                x => new StudentManagementViewModel
                {
                    CourseId = x.CourseId,
                    CourseName = x.CourseName,
                    FirstCreate = false,
                }).FirstOrDefault(x => x.CourseId == cid);

            if (vm == null) return null;

            var enrolledStudents = _context.Query<Student>()
                .Where(x => x.CourseId == cid)
                .Select(x => new EnrolledStudent
                {
                    StudentId = x.StudentId,
                    StudentName = x.StudentName,
                    StudentSex = x.Gender ?? "",
                });

            vm.Students = [.. enrolledStudents];
            return vm;
        }

        /// <summary>
        /// 取得某一門課底下某一位學生
        /// </summary>
        /// <param name="cid">課程編號</param>
        /// <param name="sid">學號</param>
        /// <returns>某一門課底下某一位學生</returns>
        public StudentEditViewModel? GetStudent(string cid, string sid)
        {
            var student_data = (from s in _context.Query<Student>()
                                join c in _context.Query<Course>() on s.CourseId equals c.CourseId
                                where s.StudentId == sid && s.CourseId == cid
                                select new StudentEditViewModel
                                {
                                    CourseId = c.CourseId,
                                    CourseName = c.CourseName,
                                    StudentName = s.StudentName,
                                    StudentSex = s.Gender ?? "",
                                    Greeting = c.GreetingMessage ?? "",
                                }).SingleOrDefault();
            return student_data;
        }

        /// <summary>
        /// 加入修課生資料
        /// </summary>
        /// <param name="enrolledStudents">修課生資料</param>
        /// <param name="cid">課程編號</param>
        public void AddStudents(IList<EnrolledStudent> enrolledStudents, string cid)
        {
            List<User> users = [];
            List<Student> students = [];
            foreach (var item in enrolledStudents)
            {
                var user = new User
                {
                    UserId = item.StudentId,
                    Name = item.StudentName,
                    Upassword = HashHelper.SHA256Hash(item.StudentId),
                    RoleName = "Student"
                };

                var student = new Student
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    Gender = item.StudentSex,
                    CourseId = cid,
                };
                users.Add(user);
                students.Add(student);
            }
            _context.CreateMany(users);
            _context.CreateMany(students);
            _context.SaveChanges();
        }

        /// <summary>
        /// 是否存在該學生
        /// </summary>
        /// <param name="studentId">學號</param>
        /// <returns>True/False</returns>
        public bool IsStudentExist(string studentId)
        {
            return _context.Query<Student>().Any(x => x.StudentId == studentId);
        }

        /// <summary>
        /// 更新學生資料
        /// </summary>
        /// <param name="student_data">學生資料</param>
        public void UpdateStudent(StudentEditViewModel student_data)
        {
            var student = _context.Query<Student>()
                .SingleOrDefault(x => x.StudentId == student_data.StudentId
                                    && x.CourseId == student_data.CourseId);
            var user = _context.Query<User>().SingleOrDefault(x => x.Name == student_data.StudentId);
            if (student == null && user == null) return;
            student.StudentName = student_data.StudentName;
            student.Gender = student_data.StudentSex;
            user.Name = student_data.StudentName;
            _context.Update(user);
            _context.Update(student);
            _context.SaveChanges();
        }

        /// <summary>
        /// 刪除學生
        /// </summary>
        /// <param name="sid">學號</param>
        /// <param name="cid">課程編號</param>
        public void DeleteStudent(string sid, string cid)
        {
            var student = _context.Query<Student>()
                .SingleOrDefault(x => x.StudentId == sid && x.CourseId == cid);
            var user = _context.Query<User>().SingleOrDefault(x => x.UserId == sid);
            if (student != null && user != null)
            {
                _context.Delete(student);
                _context.Delete(user);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// 取得學生修課資料
        /// </summary>
        /// <param name="sid">學號</param>
        /// <returns>修課資料</returns>
        public StudentHomeViewModel? GetCourseForStudent(string sid)
        {
            var enrolledCourse = (from c in _context.Query<Course>()
                                 join s in _context.Query<Student>() on c.CourseId equals s.CourseId
                                 join t in _context.Query<Teacher>() on c.TeacherId equals t.TeacherId
                                 where c.TeacherId == t.TeacherId
                                        && s.CourseId == c.CourseId
                                        && s.StudentId == sid
                                 select new StudentHomeViewModel
                                 {
                                        CourseID = c.CourseId,
                                        CourseName = c.CourseName,
                                        TeacherName = t.TeacherName
                                 }).SingleOrDefault();
            return enrolledCourse;
        }
    }
}
