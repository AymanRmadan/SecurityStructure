using InfrastructureLayer.Enums.EnumSetting;

namespace InfrastructureLayer.Enums.EnumsClasses
{
    public enum Degree
    {
        [EnumValueAttribute("ممتاز")]
        Excellent = 1,
        [EnumValueAttribute("جيد جدا")]
        VeryGood,
        [EnumValueAttribute("جيد")]
        Good
    }
}
