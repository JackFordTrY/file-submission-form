using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestTask.WebApi.Contracts.SubmitFile;
using TestTask.WebApi.Services;

namespace TestTask.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]   
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IValidator<SubmitFileRequest> _validator;
        private readonly IBlobUploadService _blobUpload;

        public HomeController(IValidator<SubmitFileRequest> validator, IBlobUploadService blobUpload)
        {
            _validator = validator;
            _blobUpload = blobUpload;
        }

        [HttpPost("SubmitFile")]
        public async Task<IActionResult> SubmitFile([FromForm]SubmitFileRequest request)
        {
            
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors.Select(e=>e.ErrorMessage));
            }

            _blobUpload.UploadFile(request.File, request.Email);

            return Ok("Your file has been submitted! Check your email for confirmation.\nIt may take some time :(");
        }
    }
}
