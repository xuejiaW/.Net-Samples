using System.CommandLine;
using System.CommandLine.Parsing;

namespace SampleCommandLine;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var fileOption = new Option<FileInfo>(description: "The file to read and display on the console",
                                              name: "--file",
                                              isDefault: true,
                                              parseArgument: GetFileInfo!);


        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        rootCommand.AddGlobalOption(fileOption);

        Command readCommand = DefineReadCommand();
        Command deleteCommand = DefineDeleteCommand();
        Command addCommand = DefineAddCommand();

        var quotesCommand = new Command("quotes", "Work with a file that contains quotes.");
        quotesCommand.AddCommand(readCommand);
        quotesCommand.AddCommand(deleteCommand);
        quotesCommand.AddCommand(addCommand);

        rootCommand.AddCommand(quotesCommand);

        return await rootCommand.InvokeAsync(args);


        FileInfo? GetFileInfo(ArgumentResult result)
        {
            if (result.Tokens.Count == 0)
            {
                return new FileInfo("sampleQuotes.txt");
            }

            string filePath = result.Tokens.Single().Value;
            if (File.Exists(filePath)) return new FileInfo(filePath);

            result.ErrorMessage = "File does not exist.";
            return null;
        }

        Command DefineReadCommand()
        {
            var delayOption = new Option<int>(name: "--delay",
                                              description:
                                              "Delay between lines, specified as milliseconds per character in a line.",
                                              getDefaultValue: () => 42);

            var fgColorOption = new Option<ConsoleColor>(name: "--fgcolor",
                                                         description:
                                                         "Foreground color of text displayed on the console.",
                                                         getDefaultValue: () => ConsoleColor.White);

            var lightModeOption = new Option<bool>(name: "--light-mode",
                                                   description:
                                                   "Background color of text displayed on the console: default is black, light mode is white.");

            var read = new Command("read", "Reads a file and displays it on the console")
            {
                delayOption,
                fgColorOption,
                lightModeOption
            };

            read.SetHandler(async (file, delay, fgColor, lightMode) =>
            {
                await ReadFile(file, delay, fgColor, lightMode);
            }, fileOption, delayOption, fgColorOption, lightModeOption);

            return read;
        }

        Command DefineDeleteCommand()
        {
            var searchTermsOption = new Option<string[]>(name: "--search-terms",
                                                         description: "Strings to search for when deleting entries.")
                {IsRequired = true, AllowMultipleArgumentsPerToken = true};

            var delete = new Command("delete", "Deletes lines from a file.");
            delete.AddOption(searchTermsOption);
            delete.SetHandler(DeleteFromFile, fileOption, searchTermsOption);
            return delete;

            void DeleteFromFile(FileInfo file, string[] searchTerms)
            {
                Console.WriteLine("Deleting from file");
                File.WriteAllLines(file.FullName, File.ReadLines(file.FullName)
                                                      .Where(line => searchTerms.All(s => !line.Contains(s))).ToList());
            }
        }

        Command DefineAddCommand()
        {
            var add = new Command("add", "Add an entry to the file.");
            var quoteArgument = new Argument<string>(name: "quote", description: "Text of quote.");
            var bylineArgument = new Argument<string>(name: "byline", description: "Byline of quote.");

            add.AddArgument(quoteArgument);
            add.AddArgument(bylineArgument);
            add.AddAlias("insert");
            add.SetHandler(AddToFile, fileOption, quoteArgument, bylineArgument);
            return add;

            static void AddToFile(FileInfo file, string quote, string byline)
            {
                Console.WriteLine("Adding to file");
                using StreamWriter writer = file.AppendText();
                writer.WriteLine($"{Environment.NewLine}{Environment.NewLine}{quote}");
                writer.WriteLine($"{Environment.NewLine}-{byline}");
                writer.Flush();
            }
        }
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