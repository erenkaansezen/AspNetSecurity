using WhiteBlackList.Filters;
using WhiteBlackList.MiddleWares;

var builder = WebApplication.CreateBuilder(args);
// CheckWhiteList s?n?f?n? servis olarak ekle
builder.Services.AddScoped<CheckWhiteList>();
// appsettings.json içindeki IPList bölümünü IPList s?n?f?na ba?la
builder.Services.Configure<IPList>(builder.Configuration.GetSection("IPList"));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//--request ip kontrolü için middleware ekleniyor
app.UseMiddleware<IPSafeMiddleWare>();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseMiddleware<IPSafeMiddleWare>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
