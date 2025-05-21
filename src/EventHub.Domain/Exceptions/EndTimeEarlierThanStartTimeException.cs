using Volo.Abp;

namespace EventHub.Exceptions;

public class EndTimeEarlierThanStartTimeException : IHandelGlobalException
{
    public void GenerateExceptionCode(string message, string data)
    {
        throw new BusinessException(message).WithData(nameof(data), data);
    }
}