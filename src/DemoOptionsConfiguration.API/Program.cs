using DemoOptionsConfiguration.API.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Defaulting to Outlook for demo purposes
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings:Outlook")
    );


//Named Options instances
builder.Services.Configure<EmailSettings>(
    EmailSettings.OutlookKey,
    builder.Configuration.GetSection("EmailSettings:Outlook")
    );
builder.Services.Configure<EmailSettings>(
    EmailSettings.GmailKey,
    builder.Configuration.GetSection("EmailSettings:Gmail")
    );

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
