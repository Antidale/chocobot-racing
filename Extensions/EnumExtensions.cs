using System.ComponentModel;

namespace chocobot_racing.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            if (memInfo is null)
                return string.Empty;

            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? ((DescriptionAttribute)attributes[0])?.ToString() ?? string.Empty : enumVal?.ToString() ?? string.Empty;
        }

        public static T? GetAttribute<T>(this Enum name) where T : Attribute
        {
            var field = name.GetType().GetField(name.ToString());
            if (field == null)
                return null;

            var attributes = (T[])field.GetCustomAttributes(typeof(T), false);

            return attributes != null && attributes.Length > 0
                ? attributes[0]
                : null;
        }
    }
}
