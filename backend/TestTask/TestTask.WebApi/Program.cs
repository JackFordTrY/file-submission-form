using FluentValidation;
using System.Reflection;
using TestTask.WebApi.Contracts.SubmitFile;
using TestTask.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<SubmitFileRequest>, SubmitFileRequestValidator>();

builder.Services.AddScoped<IBlobUploadService, BlobUploadService>();

builder.Services.AddCors(options =>{
    options.AddPolicy("DefaultPolicy", builder=>{
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("https://testtaskfrontend.azurewebsites.net");
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("DefaultPolicy");

app.MapControllers();

app.Run();
