namespace Risol.Compiler;

public class Context(ISource source, IArtifactProvider artifactProvider, IConfiguration configuration) : IContext
{
    private readonly ISource _source = source;

    private readonly IArtifactProvider _artifactProvider = artifactProvider;

    private readonly IConfiguration _configuration = configuration;
}