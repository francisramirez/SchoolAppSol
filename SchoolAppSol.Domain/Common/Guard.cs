

namespace SchoolAppSol.Domain.Common
{
    public static class Guard
    {
        public static void NotNull<TValue>(TValue value, string fieldName)
        {
            if (value is null) throw new DomainException($"{fieldName} es requerido.");
        }

        public static void NotNullOrWhiteSpace(string? value, string fieldName, int? maxLen = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException($"{fieldName} es requerido.");

            if (maxLen.HasValue && value!.Length > maxLen.Value)
                throw new DomainException($"{fieldName} no puede exceder {maxLen.Value} caracteres.");
        }

        public static void GreaterThan(int value, int minExclusive, string fieldName)
        {
            if (value <= minExclusive)
                throw new DomainException($"{fieldName} debe ser > {minExclusive}.");
        }

        public static void GreaterOrEqual(decimal value, decimal minInclusive, string fieldName)
        {
            if (value < minInclusive)
                throw new DomainException($"{fieldName} debe ser >= {minInclusive}.");
        }

        public static void Between(decimal value, decimal min, decimal max, string fieldName)
        {
            if (value < min || value > max)
                throw new DomainException($"{fieldName} debe estar entre {min} y {max}.");
        }

        public static void NotFutureDate(DateTime date, string fieldName)
        {
            if (date > DateTime.UtcNow)
                throw new DomainException($"{fieldName} no puede ser una fecha futura.");
        }
    }
}
