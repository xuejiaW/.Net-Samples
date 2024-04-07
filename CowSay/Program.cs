using System.Reflection;

namespace CowSay;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            string? versionString = Assembly.GetEntryAssembly()?
               .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
               .InformationalVersion;

            Console.WriteLine($"Cow Say v{versionString}");
            Console.WriteLine("-------------");
            Console.WriteLine("\nUsage:");
            Console.WriteLine("  Cow Say <message>");
            return;
        }

        ShowCow(string.Join(' ', args));
    }

    private static void ShowCow(string message)
    {
        string bot = $"\n        {message}";
        bot += @"
    __________________
                         \
                          \
                             (oo)\_______
                             (__)        )\/\
                                 ||------||
                                 ||      ||
                            ";
        Console.WriteLine(bot);
    }
}