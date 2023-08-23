namespace Application.Exception
{
    using FluentValidation.Results;
    public class CustomValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; set; }

        public CustomValidationException() : base("Se presentaron errores de validación")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CustomValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
