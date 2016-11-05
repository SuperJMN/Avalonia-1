namespace OmniXamlV1.Builder
{
    using System.Reflection;

    public static class Route
    {
        public static ConfiguredAssembly Assembly(Assembly assembly)
        {
            return new ConfiguredAssembly(assembly);
        }
    }
}