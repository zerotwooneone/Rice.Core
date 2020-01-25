using Rice.Module.Abstractions;

namespace Rice.Core.Abstractions.ModuleLoad
{
    public interface IModuleLoader
    {
        IModule GetModule(ILoadableModule loadableModule);
    }
}