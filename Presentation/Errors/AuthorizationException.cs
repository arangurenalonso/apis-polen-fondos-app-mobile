namespace Presentation.Errors
{
    public class AuthorizationException : ApplicationException
    {
        public object Detail;
        public AuthorizationException(string message, object detail = null) : base(message)
        {
            Detail = detail;

        }
    }
}
