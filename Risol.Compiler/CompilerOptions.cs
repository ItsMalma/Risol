namespace Risol.Compiler;

public class CompilerOptions
{
    public FileStream? SourceFile { get; set; } = null;

    public bool Help { get; set; }

    public FileStream? OutputFile { get; set; } = null;

    public CompileTarget Target { get; set; } = CompileTarget.JavaScript;
}
