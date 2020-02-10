using System;
using System.IO;

namespace CoreIntegration
{
    public interface ITestContext : IDisposable
    {
        IServiceLocator ServiceLocator { get; }
        public DirectoryInfo OutputDirectory { get; }
    }
}