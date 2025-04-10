﻿using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using Xunit;

namespace WebApi.Test.Dashboard;

public class GetDashboardInvalidToken : MyRecipeBookClassFixture
{
    private const string METHOD = "dashboard";

    public GetDashboardInvalidToken(CustomWebApplicationFactory webAplication) : base(webAplication)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var response = await DoGet(method: METHOD, token: "tokenInvalid");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Without_Invalid()
    {
        var response = await DoGet(method: METHOD, token: string.Empty);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var response = await DoGet(method: METHOD, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
