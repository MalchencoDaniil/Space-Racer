using Zenject;

public class ResourceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IResourceService>().To<ResourceService>().AsSingle();
    }
}