namespace TestTask.WebApi.Contracts.SubmitFile;

public record SubmitFileRequest(string Email, IFormFile File);