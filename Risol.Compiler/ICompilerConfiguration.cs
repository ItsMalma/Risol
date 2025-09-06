namespace Risol.Compiler;

public interface ICompilerConfiguration
{
    public IBackend Backend { get; }

    public FileStream OutputFile { get; }

    public bool ExpectEntryPoint { get; }
}