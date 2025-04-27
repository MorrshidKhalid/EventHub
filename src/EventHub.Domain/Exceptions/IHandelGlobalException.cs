using Volo.Abp;

namespace EventHub.Exceptions;

public interface IHandelGlobalException : IBusinessException
{
    public void GenerateExceptionCode(string message, string data);
}