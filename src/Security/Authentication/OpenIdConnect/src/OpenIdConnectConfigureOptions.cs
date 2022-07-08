// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Authentication.OpenIdConnect;

internal sealed class OpenIdConnectConfigureOptions : IConfigureNamedOptions<OpenIdConnectOptions>
{
    private readonly IAuthenticationConfigurationProvider _authenticationConfigurationProvider;

    /// <summary>
    /// Initializes a new <see cref="OpenIdConnectConfigureOptions"/> given the configuration
    /// provided by the <paramref name="configurationProvider"/>.
    /// </summary>
    /// <param name="configurationProvider">An <see cref="IAuthenticationConfigurationProvider"/> instance.</param>
    public OpenIdConnectConfigureOptions(IAuthenticationConfigurationProvider configurationProvider)
    {
        _authenticationConfigurationProvider = configurationProvider;
    }

    /// <inheritdoc />
    public void Configure(string? name, OpenIdConnectOptions options)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        var configSection = _authenticationConfigurationProvider.GetSchemeConfiguration(name);

        if (configSection is null || !configSection.GetChildren().Any())
        {
            return;
        }

        options.ClientId = configSection["ClientId"];
        options.ClientSecret = configSection["ClientSecret"];
        options.Authority = configSection["Authority"];
    }

    /// <inheritdoc />
    public void Configure(OpenIdConnectOptions options)
    {
        Configure(Options.DefaultName, options);
    }
}
