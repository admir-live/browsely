using Browsely.Common.Domain;
using MediatR;

namespace Browsely.Common.Application.Messaging;

/// <summary>
///     Represents a command that returns a <see cref="Result" />.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand;

/// <summary>
///     Represents a command that returns a <see cref="Result{TResponse}" />.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

/// <summary>
///     Marker interface for base command functionality.
/// </summary>
public interface IBaseCommand;
