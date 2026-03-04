

namespace SchoolAppSol.Application.Base
{
    public class ServiceResult<TModel>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public TModel? Data { get; set; }
      
    }
}
