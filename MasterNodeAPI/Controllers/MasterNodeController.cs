using MasterNodeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MasterNodeAPI.Controllers;

[ApiController]
public class CaesarCipherController : ControllerBase
{
    private readonly ILogger<CaesarCipherController> _logger;
    private readonly ICaesarCipherService _cipherService;

    public CaesarCipherController(ICaesarCipherService cipherService, ILogger<CaesarCipherController> logger)
    {
        _logger = logger;
        _cipherService = cipherService;
    }

    [HttpGet("/DecryptFile")]
    public async Task<IActionResult> DecryptFile([FromQuery, BindRequired] string fileName)
    {
        var fileString = await _cipherService.ReadEncryptedFile(fileName);
        // Call AWS Batch Job API to queue a batch job.

        var response = await _cipherService.SendFileContent(fileString);
        Console.WriteLine("Max Values from Message:\n"+response);

        return Ok(response);
    }
}

