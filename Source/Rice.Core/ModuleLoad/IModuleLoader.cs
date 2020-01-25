using Rice.Module.Abstractions;

namespace Rice.Core.ModuleLoad
{
    public interface IModuleLoader
    {
        IModule GetModule(ILoadableModule loadableModule);
    }
}