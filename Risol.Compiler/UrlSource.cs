namespace Risol.Compiler;

public class UrlSource : ISource
{
    private readonly static Uri _currentDirectory = new(Path.GetFullPath("."));

    private readonly static Uri _baseUri = _currentDirectory;

    private readonly Uri _uri;

    private readonly Uri _absoluteUri;

    private readonly Uri _translatedUri;

    protected readonly ModuleManager _moduleManager;

    public UrlSource(FileInfo file)
    {
        Uri fileUri = new(Path.GetFullPath(file.FullName));

        _uri = _baseUri.MakeRelativeUri(fileUri);
        _translatedUri = _absoluteUri = new Uri(_baseUri, fileUri);
        _moduleManager = new ModuleManager();
    }
}