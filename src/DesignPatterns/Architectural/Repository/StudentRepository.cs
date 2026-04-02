namespace DesignPatterns.Architectural.Repository;

// In-memory repository for understanding the pattern without a database.
public class StudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();

    public void Add(Student student)
    {
        _students.Add(student);
    }

    public Student? GetById(int id)
    {
        return _students.FirstOrDefault(student => student.Id == id);
    }

    public IReadOnlyList<Student> GetAll()
    {
        return _students.AsReadOnly();
    }

    public void Update(Student student)
    {
        var existingStudent = GetById(student.Id);
        if (existingStudent is null)
        {
            return;
        }

        existingStudent.Name = student.Name;
        existingStudent.Email = student.Email;
    }

    public void Delete(int id)
    {
        var student = GetById(id);
        if (student is not null)
        {
            _students.Remove(student);
        }
    }
}