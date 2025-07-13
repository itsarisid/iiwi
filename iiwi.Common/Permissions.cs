using System.Reflection;

namespace iiwi.Common;

public static class Permissions
{
    public const string Permission = "Permission";

    public static class Test
    {
        public const string Read = "Test.Read";
        public const string Write = "Test.Write";
        public const string Delete = "Test.Remove";
    }

    public static IEnumerable<string> GetAll() => typeof(Permissions)
            .GetNestedTypes()
            .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .Select(x => (string)x.GetRawConstantValue()));

}
