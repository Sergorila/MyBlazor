using Microsoft.Extensions.Logging;

namespace MyBlazor.BusinessLogic.Logic;

public class BaseLogic
{
    protected readonly ILogger Logger;

    protected BaseLogic(ILogger<BaseLogic> logger)
    {
        Logger = logger;
    }
}