using Volo.Abp;

namespace EventHub.Exceptions;

public class EndTimeEarlierThanStartTimeException : BusinessException
{

    public EndTimeEarlierThanStartTimeException(string date)
        : base(EventHubDomainErrorCodes.EndTimeCantBeEarlierThanStartTime)
    {
        WithData(nameof(date), date);
    }
}