using ThesaurusLibrary.IServices;
using ThesaurusLibrary.Services;
using ThesaurusWebAPI.ApiServices;
using ThesaurusWebAPI.IApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Logging in application
builder.Services.AddSingleton<ILoggerService, LoggerService>();
// CustomService injection
builder.Services.AddSingleton<ISynonymService, SynonymService>();
builder.Services.AddSingleton<ISupportService, SupportService>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddSingleton<IDataReaderService, FileReaderService>();
builder.Services.AddSingleton<IValidationService, ValidationService>();


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


// Use CORS AT FIRST!
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}


app.UseDefaultFiles();  // for fiinding my html file 
app.UseStaticFiles();   // for serving css and js files

//app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }

