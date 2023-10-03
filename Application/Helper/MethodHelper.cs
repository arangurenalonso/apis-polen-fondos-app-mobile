namespace Application.Helper
{
    using Newtonsoft.Json;
    using System.Reflection;
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
            string[] subcadenas = input.Split(' ');
            string nuevaCadena = string.Join("", subcadenas);

            var inputTrim= nuevaCadena.Trim();
            if (n > inputTrim.Length)
            {
                n = inputTrim.Length; // Si 'n' es mayor que la longitud de la cadena, obtenemos toda la cadena
            }

            return inputTrim.Substring(inputTrim.Length - n, n);
        }

        public static DateTime ObtenerPrimerDiaDelMes(DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, 1);
        }

        public static DateTime ObtenerUltimoDiaDelMes(DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, DateTime.DaysInMonth(fecha.Year, fecha.Month));
        }
        public static string FormatearFechaddMMyyyy(DateTime fecha)
        {
            return fecha.ToString("ddMMyyyy");
        }

        public static T DeepCopy<T>(T original)
        {
            if (original == null) return default(T);
            string json = JsonConvert.SerializeObject(original);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
