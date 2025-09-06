namespace Risol.Compiler;

public class Options
{
    public FileInfo? SourceFile { get; set; } = null;

    public bool Help { get; set; } = false;

    public FileInfo? OutputFile { get; set; } = null;

    public Target Target { get; set; } = Target.JavaScript;
}
