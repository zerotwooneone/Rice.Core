namespace Rice.Core.Abstractions.ModuleLoad
{
    public interface ILoadableModuleFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName">The assembly name, only required if it is different from the file name.</param>
        /// <param name="fullPathToDll"></param>
        /// <returns></returns>
        ILoadableModule Create(string assemblyName, string fullPathToDll);
    }
}