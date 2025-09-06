namespace Risol.Compiler;

public class CompilerOptions
{
    public FileStream? SourceFile { get; set; } = null;

    public bool Help { get; set; } = false;

    public FileStream? OutputFile { get; set; } = null;

    public CompileTarget Target { get; set; } = CompileTarget.JavaScript;
}
