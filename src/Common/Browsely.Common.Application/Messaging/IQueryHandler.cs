using Browsely.Common.Domain;
using MediatR;

namespace Browsely.Common.Application.Messaging;

/// <summary>
///     Defines a handler for processing queries with a specific response type.
/// </summary>
/// <typeparam name="TQuery">The type of query to handle.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
