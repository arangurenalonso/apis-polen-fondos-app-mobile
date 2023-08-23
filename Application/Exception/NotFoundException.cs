namespace Application.Exception
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string Mensaje) : base(Mensaje)
        {

        }
    }
}
