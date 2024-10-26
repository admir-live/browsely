using Microsoft.Extensions.Configuration;

namespace Browsely.Common.Application.Extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    ///     Gets a value from the configuration or throws an exception if the key is missing.
    /// </summary>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <param name="key">The key to look up in the configuration.</param>
    /// <returns>The value associated with the specified key.</returns>
    public static T GetValueOrThrow<T>(this IConfiguration configuration, string key)
    {
        T? value = configuration.GetValue<T>(key) ?? throw new InvalidOperationException($"Configuration key '{key}' is missing.");
        return value;
    }
}
