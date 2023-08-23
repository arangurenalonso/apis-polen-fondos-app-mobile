namespace Application.Exception
{
    public class ForeignKeyNotFoundException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; set; }

        public ForeignKeyNotFoundException(IDictionary<string, string[]> errores) : base($"Una o mas llaves foraneas no se encontraron")
        {
            Errors = errores;
        }
    }
}
