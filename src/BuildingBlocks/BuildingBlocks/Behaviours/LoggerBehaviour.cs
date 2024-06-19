using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behaviours
{
    public class LoggerBehaviour<Trequest, Tresponse> (ILogger<LoggerBehaviour<Trequest, Tresponse>> logger) : IPipelineBehavior<Trequest, Tresponse>
        where Trequest : IRequest<Tresponse>
        where Tresponse : notnull
    {
        public async Task<Tresponse> Handle(Trequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handling request = {request} - Response = {response}", typeof(Trequest).Name,typeof(Tresponse).Name);

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[Performance] Request = {request} - Response = {response} - Time = {timeTaken}", typeof(Trequest).Name, typeof(Tresponse).Name, timeTaken);
            }

            logger.LogInformation("[END] Handling request = {request} - Response = {response}", typeof(Trequest).Name, typeof(Tresponse).Name);

            return response;
        }


    
    }
}
