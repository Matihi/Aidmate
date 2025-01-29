var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom services
builder.Services.AddSingleton<IParamedicService, ParamedicService>();
var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.MapControllers();
app.Run();