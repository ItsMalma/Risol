namespace Risol.Compiler;

public class CompilerOptions
{
    public FileInfo? SourceFile { get; set; } = null;

    public bool Help { get; set; } = false;

    public FileInfo? OutputFile { get; set; } = null;

    public CompileTarget Target { get; set; } = CompileTarget.JavaScript;
}
