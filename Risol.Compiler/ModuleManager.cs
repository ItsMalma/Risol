namespace Risol.Compiler;

public class ModuleManager
{
    private Module[] _modules = [];

    public ModuleManager()
    {
        _modules = [
            GetModule("io"),
            GetModule("math")
        ];
    }

    protected Module GetModule(string name, string? path = null)
    {
        Uri moduleUri = new(Path.Combine(AppContext.BaseDirectory, "std", path ?? name) + Path.DirectorySeparatorChar);
        DirectoryInfo moduleDirectory = new(moduleUri.LocalPath);
        return moduleDirectory.Exists
            ? new Module(name, moduleUri, moduleDirectory)
            : throw new Exception($"Failed to find module: {name}");
    }
}
