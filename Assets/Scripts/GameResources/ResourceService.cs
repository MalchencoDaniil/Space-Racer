using System.Collections.Generic;

public class ResourceService : IResourceService
{
    private readonly Dictionary<ResourceType, IResource> _resources;

    public ResourceService()
    {
        _resources = new Dictionary<ResourceType, IResource>
        {
            { ResourceType.Coin, new CoinResource() },
            { ResourceType.Diamond, new DiamondResource() }
        };
    }

    public IResource GetResource(ResourceType type)
    {
        return _resources[type];
    }
}