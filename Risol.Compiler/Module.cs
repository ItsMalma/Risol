namespace Risol.Compiler;

public class Module(string name, Uri uri, DirectoryInfo directory)
{
    public string Name { get; set; } = name;

    public Uri Uri { get; set; } = uri;

    public DirectoryInfo Directory { get; set; } = directory;
}
