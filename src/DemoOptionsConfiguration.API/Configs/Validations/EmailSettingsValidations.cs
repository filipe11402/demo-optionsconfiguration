using Microsoft.Extensions.Options;

namespace DemoOptionsConfiguration.API.Configs.Validations;

public class EmailSettingsValidations : IValidateOptions<EmailSettings>
{
    public ValidateOptionsResult Validate(string name, EmailSettings options)
    {
       string errorString = string.Empty;

        if (options.Port <= 0) 
        {
            errorString = string.Join("\n", errorString, $"Invalid Email Port number on: {name} section");
        }

        if (string.IsNullOrWhiteSpace(options.Server)) 
        {
            errorString = string.Join("\n", errorString, $"Email Server cannot be empty on: {name} section");
        }

        return string.IsNullOrWhiteSpace(errorString) ? 
            ValidateOptionsResult.Success : 
            ValidateOptionsResult.Fail(errorString);
    }
}
