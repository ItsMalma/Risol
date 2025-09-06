using Risol.Compiler.JS;

namespace Risol.Compiler;

public class CompilerConfiguration(CompilerOptions options) : ICompilerConfiguration
{
    public IBackend Backend { get; private set; } = options.Target switch
    {
        CompileTarget.JavaScript => new JSBackend(),
        _ => throw new NotImplementedException()
    };

    private readonly CompilerOptions _compilerOptions = options;
}