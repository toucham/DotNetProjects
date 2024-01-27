using System.Reflection;
using DIRegisterServices;

var builder = WebApplication.CreateBuilder(args);

// add logging
builder.Services.AddHttpLogging(log =>
{
    log.MediaTypeOptions.AddText("application/json");
    //response
    log.ResponseHeaders.Add("responseheader");
    log.ResponseBodyLogLimit = 4096;
    // request
    log.RequestHeaders.Add("X-Client-Id");
    log.RequestBodyLogLimit = 4096;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// get all referenced assemblies
Assembly curr = Assembly.GetExecutingAssembly();
// register all services with attribute
builder.Services.RegisterServices(multiAssembly: true, currentAssembly: curr);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// middleware
app.UseHttpLogging();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run("http://localhost:8080");
