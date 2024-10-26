using System.Reflection;

namespace Browsely.Modules.Dispatcher.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
