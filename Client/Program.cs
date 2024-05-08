using Client.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRefitClient<InterfaceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7271"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Cookie";
    options.DefaultSignInScheme = "Cookie";
    options.DefaultChallengeScheme = "Cookie";
})
    .AddCookie("Cookie", options =>
    {
        options.Cookie.HttpOnly = true;
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
