using InstaSharper.Converters;

namespace InstaSharper.Helpers
{
    public static class ConvertersHelper
    {
        public static IConvertersFabric GetDefaultFabric()
        {
            return ConvertersFabric.Instance;
        }
    }
}