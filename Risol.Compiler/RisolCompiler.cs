namespace Risol.Compiler;

public class RisolCompiler
{
    private static void ShowErrors(TextWriter writer, params string[] errors)
    {
        foreach (string error in errors)
        {
            writer.WriteLine($"Error: {error}");
        }
        writer.WriteLine();
    }

    private static void ShowHelp(TextWriter writer)
    {
        writer.WriteLine("Usage: risol [options] <source-file>");
        writer.WriteLine();
        writer.WriteLine("Options:");
        writer.WriteLine("  -h | --help               Display this information");
        writer.WriteLine("  -o | --out <file>         Place the output into <file>");
        writer.WriteLine("  -t | --target <target>    Specify compile target (js)");
    }

    private static CompilerOptions CreateCompilerOptions(string[] args, out int exitCode)
    {
        exitCode = 0;
        CompilerOptions options = new();
        try
        {
            options = CommandLineParser.Parse(args);
            if (options.Help || args.Length == 0)
            {
                ShowHelp(Console.Error);
                exitCode = 1;
            }
        }
        catch (CommandLineException exception)
        {
            ShowErrors(Console.Error, exception.Message);
            ShowHelp(Console.Error);
            exitCode = 1;
        }
        return options;
    }

    private static int Compile(CompilerOptions options)
    {
        if (options.SourceFile == null)
        {
            ShowErrors(Console.Error, "No source file were specified");
            ShowHelp(Console.Error);
            return 1;
        }
        if (!File.Exists(options.SourceFile.Name))
        {
            ShowErrors(Console.Error, $"Source file \"{options.SourceFile.Name}\" not found");
            ShowHelp(Console.Error);
            return 1;
        }
        CompilerConfiguration configuration = new(options);
        return 0;
    }

    public static void Main(string[] args)
    {
        try
        {
            CompilerOptions options = CreateCompilerOptions(args, out int exitCode);
            if (exitCode != 0)
            {
                Environment.Exit(exitCode);
            }
            exitCode = Compile(options);
            if (exitCode != 0)
            {
                Environment.Exit(exitCode);
            }
        }
        catch
        {
            Environment.Exit(253);
        }
    }
}
