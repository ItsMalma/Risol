using Risol.Compiler.JS;

namespace Risol.Compiler;

public class Configuration(Options options) : IConfiguration
{
    public IBackend Backend { get; } = options.Target switch
    {
        Target.JavaScript => new JSBackend(),
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
                    Target.JavaScript => ".js",
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

    private readonly Options _compilerOptions = options;

    private readonly ModuleManager _moduleManager = new();
}