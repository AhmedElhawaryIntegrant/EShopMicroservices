using MediatR;
using BuildingBlocks.CQRS;
using FluentValidation;
namespace BuildingBlocks.Behaviours
{
    public class ValidationBehaviour<Trequest,Tresponse>(IEnumerable<IValidator<Trequest>> validators) : IPipelineBehavior<Trequest,Tresponse>
        where Trequest : ICommand<Tresponse>
        where Tresponse : notnull
    {
        
        public async Task<Tresponse> Handle(Trequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
           var validationContext = new ValidationContext<Trequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));

            var failures = validationResults.Where(r=> r.Errors.Any()).SelectMany(r => r.Errors).ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
    
}
