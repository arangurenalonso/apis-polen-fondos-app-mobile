namespace Application.Helper
{
    public class ValidationMessages
    {
        public const string IdNotEmptyMessage = "La llave primaria '{PropertyName}' del grupo es obligatorio";
        public const string MaxLengthMessage = "El '{PropertyName}' no puede exceder los {MaxLength} caracteres";
        public const string ForeignKeyRequired = "La llave foranea '{PropertyName}' es obligatorio";
        public const string NotNullNotEmptyMessage = "El '{PropertyName}' no puede ser nula ni blanco";
        public const string NotNullMessage = "El campo '{PropertyName}' no puede ser nula";
        public const string FechaRequiredMessage = "El campo '{PropertyName}' debe ser una fecha";
        public const string FechaInvalidFormatMessage = "El campo '{PropertyName}' debe tener el formato 'yyyy-MM-dd'.";
        public const string PorcentajeNotValidMessage = "El campo '{PropertyName}' debe ser un porcentaje debe estar entre 0 y 100.";
        public const string ValueNotBetweenValidMessage = "El campo '{0}' debe estar entre  {1} y {2}.";
        public const string MayorQueNotValidMessage = "El campo '{PropertyName}' debe ser mayor a {ComparisonValue}.";
        public const string ForeignKeyNotExist = "El Id Proporcionado '{0}' no existe en la base de datos.";

    }
}
