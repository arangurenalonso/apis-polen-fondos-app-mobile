
namespace Application.Helper
{
    public class MethodHelper
    {
        public static string GetFirstNCharacters(string? input, int n)
        {
            if (input == null)
            {
                return "";
            }
            var inputTrim = input.Trim();

            if (n > inputTrim.Length)
            {
                n = inputTrim.Length;
            }

            return inputTrim.Substring(0, n);
        }
        public static string GetLastNCharacters(string? input, int n)
        {
            if (input == null)
            {
                return "";
            }
            var inputTrim= input.Trim();
            if (n > inputTrim.Length)
            {
                n = inputTrim.Length; // Si 'n' es mayor que la longitud de la cadena, obtenemos toda la cadena
            }

            return inputTrim.Substring(inputTrim.Length - n, n);
        }
    }
}
