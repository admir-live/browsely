using Browsely.Common.Domain;
using MediatR;

namespace Browsely.Common.Application.Messaging;

/// <summary>
///     Represents a query that returns a result of type <typeparamref name="TResponse" />.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
