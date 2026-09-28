namespace StudentAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly List<Student> _students = new()
        {
            new Student
            {
                Id= 100,
                Name= "rahim",
                Email= "rahim@gmail.com",
                Password= "12345"
            },

        }
    }
}
