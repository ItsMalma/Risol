namespace Risol.Compiler;

public interface IConfiguration
{
    public IBackend Backend { get; }

    public FileStream OutputFile { get; }

    public bool ExpectEntryPoint { get; }
}