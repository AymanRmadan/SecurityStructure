namespace InfrastructureLayer.Enums.EnumSetting
{
    public class EnumValueAttribute : Attribute
    {
        public string ArabicValue { get; set; }
        public EnumValueAttribute(string arabicvalue)
        {
            ArabicValue = arabicvalue;
        }
    }
}
