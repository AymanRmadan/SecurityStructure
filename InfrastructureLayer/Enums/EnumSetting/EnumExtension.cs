using System.Reflection;

namespace InfrastructureLayer.Enums.EnumSetting
{
    public static class EnumExtension
    {
        public static string ToArabicValue<T>(this T enumeration) where T : Enum
        {
            MemberInfo memberInfo = typeof(T).GetMember(enumeration.ToString()).First();

            var eunmValueAtrr = memberInfo.GetCustomAttribute<EnumValueAttribute>(false);

            return eunmValueAtrr is not null && !string.IsNullOrWhiteSpace(eunmValueAtrr.ArabicValue) ? eunmValueAtrr.ArabicValue : enumeration.ToString();
        }
    }
}
