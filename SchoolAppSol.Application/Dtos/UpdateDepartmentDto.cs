namespace SchoolAppSol.Application.Dtos.Department
{
    public record UpdateDepartmentDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Budget { get; init; }
        public DateTime StartDate { get; init; }
        public int? Administrator { get; init; }
        public int UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
