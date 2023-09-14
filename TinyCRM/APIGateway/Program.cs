using ApiGateway.Authentication;
using BuildingBlock.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IamService>();

builder.Services.AddSwagger();
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, IamAuthenticationHandler>("Iam", null);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerForOcelotUI().UseOcelot();

app.UseRouting();

app.Run();