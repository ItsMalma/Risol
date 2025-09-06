using Risol.Compiler.JS;

namespace Risol.Compiler;

public class CompilerConfiguration(CompilerOptions options) : ICompilerConfiguration
{
    public IBackend Backend { get; } = options.Target switch
    {
        CompileTarget.JavaScript => new JSBackend(),
        _ => throw new NotImplementedException()
    };

    public FileStream OutputFile
    {
        get
        {
            if (_compilerOptions.OutputFile == null)
            {
                string extension = _compilerOptions.Target switch
                {
                    CompileTarget.JavaScript => ".js",
                    _ => ""
                };
                string name = _compilerOptions.SourceFile != null
                    ? Path.GetFileNameWithoutExtension(_compilerOptions.SourceFile.Name)
                    : "out";
                return File.Open(name + extension, FileMode.OpenOrCreate);
            }
            else
            {
                return _compilerOptions.OutputFile.OpenRead();
            }
        }
    }

    public bool ExpectEntryPoint { get; set; } = false;

    private readonly CompilerOptions _compilerOptions = options;

    private readonly ModuleManager _moduleManager = new();
}