namespace Risol.Compiler;

public class RisolCompiler
{
    private static void ShowError(TextWriter writer, string message)
    {
        if (!message.EndsWith('.'))
        {
            message += '.';
        }
        writer.WriteLine($"Error: {message}");
    }

    private static void ShowHelp(TextWriter writer)
    {
        writer.WriteLine("Usage: risol [options] <source-file>");
        writer.WriteLine();
        writer.WriteLine("Options:");
        writer.WriteLine("  -h | --help               Display this information.");
        writer.WriteLine("  -o | --out <file>         Place the output into <file>.");
        writer.WriteLine("  -t | --target <target>    Specify compile target (js).");
    }

    private static CompilerOptions ProcessOptions(string[] args)
    {
        CompilerOptions options = new();
        try
        {
            options = CommandLineParser.Parse(args);
            if (options.SourceFile == null || options.Help)
            {
                ShowHelp(Console.Error);
                Environment.Exit(1);
            }
        }
        catch
        {
            ShowHelp(Console.Error);
            Environment.Exit(1);
        }
        return options;
    }

    public static void Main(string[] args)
    {
        try
        {
            CompilerOptions options = ProcessOptions(args);
        }
        catch
        {
            Environment.Exit(253);
        }
    }
}