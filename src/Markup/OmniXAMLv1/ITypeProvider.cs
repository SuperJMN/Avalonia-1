namespace OmniXamlV1
{
    using System;

    // ReSharper disable once UnusedMember.Global
    public interface ITypeProvider
    {
        Type GetType(string typeName, string clrNamespace, string assemblyName);
    }
}