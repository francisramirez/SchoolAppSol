namespace SchoolAppSol.Application.Dtos.Department
{
    public record DepartmentAddDto
    {
        public string Name { get; init; } = string.Empty;
        public decimal Budget { get; init; }
        public DateTime StartDate { get; init; }
        public int? Administrator { get; init; }
        public int CreateUser { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
