var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Apının istek atıcağı yerleri belirtiyoruz
builder.Services.AddCors(options =>
{
    //options.AddDefaultPolicy(policy =>
    //{
    //    //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); // dışarıdan gelen bütün istekleri kabul eder.

    //});


    options.AddPolicy("MyPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7167") // sadece bu adresten gelen istekleri kabul eder.
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
    options.AddPolicy("AnotherPolicy", policy =>
    {
        policy.WithOrigins("https://example.com").WithMethods("GET", "POST"); // sadece bu adresten ve bu metodlarla gelen istekleri kabul eder.
    });
    options.AddPolicy("AllowSites", policy =>
    {
        policy.WithOrigins("https://localhost:7167", "https://example.com").WithHeaders("Content-Type", "Authorization").WithMethods("GET", "POST", "PUT", "DELETE"); // sadece bu adresten ve bu metodlarla gelen istekleri kabul eder.
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseCors(); // apı herkese açık ise bu şekilde kalabilir
app.UseCors("AllowSites");
app.MapControllers();

app.Run();
