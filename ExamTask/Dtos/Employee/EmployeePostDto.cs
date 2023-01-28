namespace ExamTask.Dtos.Employee
{
    public class EmployeePostDto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string About { get; set; }
        public IFormFile formFile { get; set; }
        public string? TwitterUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? LinkedinUrl { get; set; }
    }
}
