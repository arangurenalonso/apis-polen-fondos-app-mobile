using Infrastructure;
using Presentation;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Agregar los servicios 

builder.Services.AddHttpContextAccessor();
builder.Services.AddInfraestructureServices(builder.Configuration);
builder.Services.AddPresentationServices();

var presentationAssembly = typeof(PresentationServiceRegistration).Assembly;
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UsePresentationMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
