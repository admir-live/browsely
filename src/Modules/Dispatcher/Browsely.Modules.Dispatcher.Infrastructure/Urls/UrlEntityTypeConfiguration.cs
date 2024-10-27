using Browsely.Modules.Dispatcher.Domain.Url;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Browsely.Modules.Dispatcher.Infrastructure.Urls;

internal sealed class UrlEntityTypeConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder
            .HasKey(url => url.Id);

        builder
            .Ignore(url => url.DomainEvents);

        builder
            .Property(p => p.Uri)
            .HasConversion(new ValueConverter<Uri, string>(
                uri => uri.ToString(),
                uri => new Uri(uri)))
            .HasMaxLength(500);

        builder
            .Property(p => p.HtmlContent)
            .HasConversion(new ValueConverter<Payload, string>(
                htmlContent => htmlContent.Value,
                htmlContent => new Payload(htmlContent)))
            .IsRequired(false);

        builder
            .Property(p => p.CurrentState)
            .HasConversion(new ValueConverter<IUrlState, string>(
                state => ConvertStateToString(state),
                state => ConvertStringToState(state)))
            .HasMaxLength(20);

        builder
            .HasIndex(url => url.CurrentState);
    }

    private static IUrlState ConvertStringToState(string stateString)
    {
        return stateString switch
        {
            "Scheduled" => new ScheduledState(),
            "InReview" => new InReviewState(),
            "Active" => new ActiveState(),
            "Expired" => new ExpiredState(),
            _ => throw new InvalidOperationException($"Unknown state value: {stateString}")
        };
    }

    private static string ConvertStateToString(IUrlState currentState)
    {
        return currentState switch
        {
            ScheduledState => "Scheduled",
            InReviewState => "InReview",
            ActiveState => "Active",
            ExpiredState => "Expired",
            _ => throw new InvalidOperationException($"Unsupported state type: {currentState.GetType().Name}")
        };
    }
}
