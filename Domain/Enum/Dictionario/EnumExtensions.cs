namespace Domain.Enum.Dictionario
{
    using System;
    using System.ComponentModel;

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null)
                return value.ToString();

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
        public static Guid GetId(this Enum value)
        {
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo != null)
            {
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    description = attributes[0].Description;
                }
            }
            if (Guid.TryParse(description, out Guid result))
            {
                return result;
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}
