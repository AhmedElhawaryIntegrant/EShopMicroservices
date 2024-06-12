using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommandHandler<in TCommand> : ICommadHandler<TCommand,Unit> where TCommand : ICommand<Unit>
    {
    }
    public interface ICommadHandler<in TCommand,Tresponse> : IRequestHandler<TCommand, Tresponse> where TCommand : ICommand<Tresponse> where Tresponse : notnull
    {
    }
}
