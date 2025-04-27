using Volo.Abp.DependencyInjection;

namespace EventHub.Exceptions;

public class HandleGlobalException(IHandelGlobalException handelGlobalException) : ITransientDependency
{
    public void GenerateExceptionCode(string message, string data)
    {
        handelGlobalException.GenerateExceptionCode(message, data);
    }
}