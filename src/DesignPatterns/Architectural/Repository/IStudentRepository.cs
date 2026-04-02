namespace DesignPatterns.Architectural.Repository;

// Repository contract that hides data-access details from callers.
public interface IStudentRepository
{
    void Add(Student student);
    Student? GetById(int id);
    IReadOnlyList<Student> GetAll();
    void Update(Student student);
    void Delete(int id);
}