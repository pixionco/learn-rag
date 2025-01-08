using Pixion.LearnRag.API.Configs;

namespace Pixion.LearnRag.API.Endpoints;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app, EndpointsConfig config);
}