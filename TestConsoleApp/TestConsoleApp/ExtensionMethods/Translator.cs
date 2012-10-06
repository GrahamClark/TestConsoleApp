namespace TestConsoleApp.ExtensionMethods
{
    static class Translator
    {
        public static OtherItem Translate(this Item i)
        {
            return new OtherItem()
            {
                Name = i.Name,
                Count = i.Count
            };
        }
    }
}
