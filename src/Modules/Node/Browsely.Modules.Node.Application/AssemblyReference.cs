using System.Reflection;

namespace Browsely.Modules.Node.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
