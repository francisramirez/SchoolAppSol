

namespace SchoolAppSol.Domain.Exceptions
{
    public class CourseException : Exception
    {
        public CourseException(string message) : base(message)
        {
            // You can add additional properties or methods here if needed
            LogException(message);
            SendException(message);

        }
        private void LogException(string message)
        {
            // Implement logging logic here, e.g., log to a file or monitoring system
            Console.WriteLine($"CourseException: {Message}");
        }
        public void SendException(string message) 
        {
            // Implement logging logic here, e.g., log to a file or monitoring system
            Console.WriteLine($"CourseException: {Message}");
        }
    }
}
