using People.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();