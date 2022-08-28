using DemoOptionsConfiguration.API.Configs;
using DemoOptionsConfiguration.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DemoOptionsConfiguration.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private IConfiguration _configuration;

    private IOptions<EmailSettings> _optionsSettings;

    private IOptionsMonitor<EmailSettings> _optionsMonitorSettings;

    private IOptionsSnapshot<EmailSettings> _optionsSnapshotSettings;

    public EmailController(
        IConfiguration configuration,
        IOptions<EmailSettings> optionsSettings,
        IOptionsMonitor<EmailSettings> optionsMonitorSettings,
        IOptionsSnapshot<EmailSettings> optionsSnapshotSettings)
    {
        _configuration = configuration;
        _optionsSettings = optionsSettings;
        _optionsMonitorSettings = optionsMonitorSettings;
        _optionsSnapshotSettings = optionsSnapshotSettings;
    }

    [HttpGet]
    [Route("settings/get")]
    public IActionResult GetSettings() 
    {
        return Ok(
            new EmailSettingsViewModel
            {
                FromConfiguration = _configuration.GetSection("EmailSettings").Value,
                FromOptions = $"GMAIL: {_optionsSettings.Value.Server}, {_optionsSettings.Value.Port}\nOUTLOOK: {_optionsSettings.Value.Server}, {_optionsSettings.Value.Port}",
                FromMonitor = $"GMAIL: {_optionsMonitorSettings.Get(EmailSettings.GmailKey).Server}, {_optionsMonitorSettings.Get(EmailSettings.GmailKey).Port}\nOUTLOOK: {_optionsMonitorSettings.Get(EmailSettings.OutlookKey).Server}, {_optionsMonitorSettings.Get(EmailSettings.OutlookKey).Port}",
                FromSnapshot = $"GMAIL: {_optionsSnapshotSettings.Get(EmailSettings.GmailKey).Server}, {_optionsSnapshotSettings.Get(EmailSettings.GmailKey).Port}\nOUTLOOK: {_optionsSnapshotSettings.Get(EmailSettings.OutlookKey).Server}, {_optionsSnapshotSettings.Get(EmailSettings.OutlookKey).Port}"
            });
    }
}
