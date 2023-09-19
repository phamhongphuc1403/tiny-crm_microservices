using BuildingBlock.Presentation.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDefaultOpenApi(builder.Configuration);
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("TinyCRM", policyBuilder =>
    {
        policyBuilder.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
} );


var app = builder.Build();
app.UseCors("TinyCRM");

app.UseSwagger();
app.UseSwaggerForOcelotUI().UseOcelot();

app.UseRouting();

app.Run();