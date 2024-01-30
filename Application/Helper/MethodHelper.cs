namespace Application.Helper
{
    using DocumentFormat.OpenXml.Bibliography;
    using Domain.Enum;
    using Domain.Enum.Dictionario;
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

        public static T DeepCopyReferenceIgnore<T>(T original)
        {
            if (original == null) return default(T);

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(original, settings);
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static T DeepCopyWithReference<T>(T original)
        {
            if (original == null) return default(T);

            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            string json = JsonConvert.SerializeObject(original, settings);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static Dictionary<string, int> InitializeCampaignIdMap(string plataforma)
        {
            var campaignIdMap = new Dictionary<string, int>();
            if (plataforma.ToUpper().Trim() == PlataformaEnum.META.GetDescription())
            {
                AddEnumValuesToMap(typeof(CampaignOriginEnumMETA), campaignIdMap);
            }
            if (plataforma.ToUpper().Trim() == PlataformaEnum.TIKTOK.GetDescription())
            {
                AddEnumValuesToMap(typeof(CampaignOriginEnumTIKTOK), campaignIdMap);
            }
            return campaignIdMap;
        }
        private static void AddEnumValuesToMap(Type enumType, Dictionary<string, int> campaignIdMap)
        {
            foreach (var value in Enum.GetValues(enumType))
            {
                var enumValue = (Enum)value;
                try
                {
                    var key = enumValue.GetDescription();
                    if (string.IsNullOrEmpty(key))
                    {
                        //Si no tiene descripcion
                        continue;
                    }

                    campaignIdMap.Add(key, (int)value);
                }
                catch (ArgumentException ex)
                {
                    // La exepción se genera cuando por clave duplicada o registrarla
                }
            }
        }

    }
}
