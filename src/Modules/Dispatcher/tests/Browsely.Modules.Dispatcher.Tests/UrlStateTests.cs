using Browsely.Common.Application.Exceptions;
using Browsely.Modules.Dispatcher.Domain.Url;
using FluentAssertions;

namespace Browsely.Modules.Dispatcher.Tests;

public class UrlStateTests
{
    [Fact]
    public void Url_Should_Have_Initial_State_As_Scheduled()
    {
        // Arrange
        var url = Url.Create(Ulid.NewUlid(), new Uri("https://example.com"));

        // Act
        IUrlState initialState = url.CurrentState;

        // Assert
        initialState.ToString().Should().Be("Scheduled");
    }

    [Fact]
    public void Url_Should_Transition_From_Scheduled_To_InReview()
    {
        // Arrange
        var url = Url.Create(Ulid.NewUlid(), new Uri("https://example.com"));

        // Act
        url.NextState();
        IUrlState nextState = url.CurrentState;

        // Assert
        nextState.ToString().Should().Be("In Review");
    }

    [Fact]
    public void Url_Should_Transition_From_InReview_To_Active()
    {
        // Arrange
        var url = Url.Create(Ulid.NewUlid(), new Uri("https://example.com"));
        url.NextState();

        // Act
        url.NextState();
        IUrlState nextState = url.CurrentState;

        // Assert
        nextState.ToString().Should().Be("Active");
    }

    [Fact]
    public void Url_Should_Transition_From_Active_To_Expired()
    {
        // Arrange
        var url = Url.Create(Ulid.NewUlid(), new Uri("https://example.com"));
        url.NextState();
        url.NextState();

        // Act
        url.NextState();
        IUrlState nextState = url.CurrentState;

        // Assert
        nextState.ToString().Should().Be("Expired");
    }

    [Fact]
    public void Url_Should_Throw_Exception_When_Transitioning_From_Expired()
    {
        // Arrange
        var url = Url.Create(Ulid.NewUlid(), new Uri("https://example.com"));
        url.NextState();
        url.NextState();
        url.NextState();

        // Act
        Action act = () => url.NextState();

        // Assert
        act.Should().Throw<BrowselyException>()
            .WithMessage("Cannot transition to next state from Expired state.");
    }

    [Fact]
    public void Url_Should_Transition_To_Failed_State()
    {
        // Arrange
        var url = Url.Create(Ulid.NewUlid(), new Uri("https://example.com"));

        // Act
        url.Fail();
        IUrlState currentState = url.CurrentState;

        // Assert
        currentState.ToString().Should().Be("Failed");
    }

    [Fact]
    public void Url_Should_Throw_Exception_When_Transitioning_From_Failed()
    {
        // Arrange
        var url = Url.Create(Ulid.NewUlid(), new Uri("https://example.com"));
        url.Fail();

        // Act
        Action act = () => url.NextState();

        // Assert
        act.Should().ThrowExactly<BrowselyException>()
            .WithMessage("Cannot transition to next state from Failed state.");
    }
}
