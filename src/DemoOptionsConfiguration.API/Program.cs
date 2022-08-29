using DemoOptionsConfiguration.API.Configs;
using DemoOptionsConfiguration.API.Configs.Validations;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuring our default email provider Outlook
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings:Outlook")
    );

//Named Options instances
//builder.Services.Configure<EmailSettings>(
//    EmailSettings.OutlookKey,
//    builder.Configuration.GetSection("EmailSettings:Outlook")
//    );
//builder.Services.Configure<EmailSettings>(
//    EmailSettings.GmailKey,
//    builder.Configuration.GetSection("EmailSettings:Gmail")
//    );

builder.Services.AddOptions<EmailSettings>(EmailSettings.OutlookKey)
    .Bind(builder.Configuration.GetSection("EmailSettings:Outlook"))
    .ValidateOnStart();
builder.Services.AddOptions<EmailSettings>(EmailSettings.GmailKey)
    .Bind(builder.Configuration.GetSection("EmailSettings:Gmail"))
    .ValidateOnStart();

//Register the Validator
builder.Services.AddSingleton<IValidateOptions<EmailSettings>, EmailSettingsValidations>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
