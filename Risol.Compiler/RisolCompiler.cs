namespace Risol.Compiler;

public class RisolCompiler
{
    private readonly ISource _source;

    private readonly IConfiguration _configuration;

    private RisolCompiler(ISource source, ISource[] embeddedSources, IConfiguration configuration, IContext context)
    {
        _source = source;
        _configuration = configuration;
    }

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
        writer.WriteLine();
    }

    private static Options CreateOptions(string[] args, out int exitCode)
    {
        exitCode = 0;
        Options options = new();
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

    private static int Compile(Options options)
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
        IConfiguration configuration = new Configuration(options)
        {
            ExpectEntryPoint = true
        };
        IArtifactProvider artifactProvider = new ArtifactProvider();
        ISource source = new UriSource(options.SourceFile);
        IContext context = new Context(source, artifactProvider, configuration);
        RisolCompiler compiler = new(source, [], configuration, context);
        return 0;
    }

    public static void Main(string[] args)
    {
        try
        {
            Options options = CreateOptions(args, out int exitCode);
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
        catch (Exception ex)
        {
            ShowErrors(Console.Error, ex.Message);
            Environment.Exit(253);
        }
    }
}
