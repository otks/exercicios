using MediatR;

namespace Questao5.Application.Commands.Responses;

public interface IResponseBase<T>: IRequest<T>
{
    public int StatusCode { get; protected set; }
}