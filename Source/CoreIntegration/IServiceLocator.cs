namespace CoreIntegration
{
    public interface IServiceLocator
    {
        T Locate<T>();
    }
}