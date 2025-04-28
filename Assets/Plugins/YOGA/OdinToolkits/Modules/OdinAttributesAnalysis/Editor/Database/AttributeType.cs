namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public enum AttributeType
    {
        Essentials = 0,
        Buttons = 1 << 1,
        Collections = 1 << 2,
        Conditionals = 1 << 3,
        Groups = 1 << 4,
        Numbers = 1 << 5,
        TypeSpecifics = 1 << 6,
        Validation = 1 << 7,
        Misc = 1 << 8,
        Meta = 1 << 9,
        Unity = 1 << 10,
        Debug = 1 << 11
    }
}