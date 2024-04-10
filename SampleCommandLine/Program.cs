using System.CommandLine;

namespace SampleCommandLine;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var fileOption = new Option<FileInfo?>(name: "--file",
                                               description: "The file to read and display on the console");
        var delayOption = new Option<int>(name: "--delay",
                                          description:
                                          "Delay between lines, specified as milliseconds per character in a line.",
                                          getDefaultValue: () => 42);

        var fgColorOption = new Option<ConsoleColor>(name: "--fgcolor",
                                                     description: "Foreground color of text displayed on the console.",
                                                     getDefaultValue: () => ConsoleColor.White);

        var lightModeOption = new Option<bool>(name: "--light-mode",
                                               description:
                                               "Background color of text displayed on the console: default is black, light mode is white.");

        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        var readCommand = new Command("read", "Reads a file and displays it on the console")
        {
            fileOption,
            delayOption,
            fgColorOption,
            lightModeOption
        };
        rootCommand.AddCommand(readCommand);

        readCommand.SetHandler(async (file, delay, fgColor, lightMode) =>
        {
            await ReadFile(file!, delay, fgColor, lightMode);
        }, fileOption, delayOption, fgColorOption, lightModeOption);

        return await rootCommand.InvokeAsync(args);
    }

    private static async Task ReadFile(FileInfo file, int delay, ConsoleColor fgColor, bool lightMode)
    {
        Console.BackgroundColor = lightMode ? ConsoleColor.White : ConsoleColor.Black;
        Console.ForegroundColor = fgColor;
        List<string> lines = File.ReadLines(file.FullName).ToList();
        foreach (string line in lines)
        {
            Console.WriteLine(line);
            await Task.Delay(delay * line.Length);
        }
    }
}