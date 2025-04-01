using Microservice.Policy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHttpClient();
builder.Services.AddHostedService<PolicyBackgroundService>();
builder.Services.AddSingleton<ICloudantSettings, CloudantSettings>();
builder.Services.AddSingleton<IMQSettings, MQSettings>();
builder.Services.AddScoped<ICloudantService<Policy>, CloudantService<Policy>>();
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<PublishMQService>();
builder.Services.AddScoped<ConsumerMQService>();
builder.Services.AddScoped<MQService>();
builder.Services.AddScoped<Func<string, IMQService>>(provider => key =>
{
    switch (key)
    {
        case "publish":
            return provider.GetRequiredService<PublishMQService>();
        case "consume":
            return provider.GetRequiredService<ConsumerMQService>();
        default:
            return provider.GetRequiredService<MQService>();
    }
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerUI();

app.UseSwagger();

app.MapControllers();

await app.RunAsync();
