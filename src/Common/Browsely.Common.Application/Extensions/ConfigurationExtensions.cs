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
    public static string GetRequiredValue(this IConfiguration configuration, string key)
    {
        string? value = configuration[key];
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException($"Configuration value for key '{key}' is missing or empty.");
        }

        return value;
    }
}
