namespace Rice.Core.Abstractions.ModuleLoad
{
    public interface ILoadableModuleFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPathToDll"></param>
        /// <param name="assemblyName">The assembly name, only required if it is different from the file name.</param>
        /// <returns></returns>
        ILoadableModule Create(string fullPathToDll, 
            string assemblyName = null);
    }
}