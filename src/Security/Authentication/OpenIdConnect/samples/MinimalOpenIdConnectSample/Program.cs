// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddOpenIdConnect();

var app = builder.Build();

app.MapGet("/protected", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}!")
    .RequireAuthorization();

app.Run();
