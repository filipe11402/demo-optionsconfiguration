## Dotnet options pattern for configurations
Demo Project to showcase how to use the `IOptions` instances and all of it's features.

To have a brief introduction into what this topic is about, please visit microsoft docs [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options).

## Introduction
Dotnet introduces these types of Options

| Options Type | Lifecyle | Reloads Configuration | Can Inject in Singletons | As named instances |
| --- | --- | --- | --- | --- |
| IOptions<T> | Singleton | No | Yes | No |
| IOptionsSnapshot<T> | Scoped | Yes | No | Yes |
| IOptionsMonitor<T> | Singleton | Yes | Yes | Yes |


## Context
Imagine we have 2 different Email providers, on which our app will use to send emails. In this situation is `Gmail` and `Outlook`.
Now, both of them will certainly contain some static configuration and let's assume they both are the same.

Both of them will contain a `Server` and a `Port`.

`appsettings.json`

```json
  ...
  "EmailSettings": {
    "Outlook": {
      "Server": "OUTLOOKSERVER",
      "Port":  100
    },
    "Gmail": {
      "Server": "GMAILSERVER",
      "Port": 80
    }
  }
```

---

## Named instances
Based on our context, this allows us to have a single class, for the sake of this project, called `EmailSettings`.
Because both `Outlook` and `Gmail` have the same properties, this is a good case where named instances come in handy.


### How to test?
Register the named options instances on `Program.cs`
```cs
...
builder.Services.Configure<EmailSettings>(
    "Outlook",
    builder.Configuration.GetSection("EmailSettings:Outlook")
    );
builder.Services.Configure<EmailSettings>(
    "Gmail",
    builder.Configuration.GetSection("EmailSettings:Gmail")
    );
...
```

Select the configuration based on the defined namespace

```cs
...
public DemoController(
    IOptionsMonitor<EmailSettings> optionsMonitor
)
{
    //Outlook configuration provided from appsettings.json
    var outlookSettings = optionsMonitor.Get("Outlook");
    //Gmail configuration provided from appsettings.json
    var gmailSettings = optionsMonitor.Get("Gmail");
}
```
**You can use either `IOptionsMonitor<T>` or  `IOptionsSnapshot<T>` as the named options provider**


---

## Options Validations
This feature allows us to perform validations when starting up the project, so that we can avoid running the project in any invalid state, let's say.

### How to test?

MISSING THIS DOCUMENTATION