namespace Risol.Compiler;

public static class CommandLineParser
{
    public static CompilerOptions Parse(string[] args)
    {
        CompilerOptions options = new();

        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i].Trim();

            if (string.IsNullOrEmpty(arg))
            {
                continue;
            }

            if (arg.StartsWith('-'))
            {
                switch (arg)
                {
                    case "-h" or "--help":
                        options.Help = true;
                        break;
                    case "-o" or "--out":
                        options.OutputFile = i + 1 < args.Length
                            ? new FileInfo(args[++i])
                            : throw new CommandLineException($"Missing filename after '{arg}'");
                        break;
                    case "-t" or "--target":
                        options.Target = i + 1 < args.Length
                            ? args[++i] switch
                            {
                                "js" => CompileTarget.JavaScript,
                                _ => throw new CommandLineException($"Invalid target '{args[i]}' after '{arg}'")
                            }
                            : throw new CommandLineException($"Missing target after '{arg}'");
                        break;
                    default:
                        throw new CommandLineException($"Unrecognized option '{arg}'");
                }
                continue;
            }

            string extension = arg[arg.LastIndexOf('.')..];
            if (extension != ".rsl")
            {
                throw new CommandLineException($"Invalid source file extension '{extension}'");
            }
            options.SourceFile = new FileInfo(arg);
        }

        return options;
    }
}