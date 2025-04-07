using Client.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Добавляем доступ к HttpContext
builder.Services.AddHttpContextAccessor();

// Регистрируем Refit клиент с прокидыванием куки
builder.Services
    .AddRefitClient<InterfaceClient>()
    .ConfigureHttpClient((serviceProvider, client) =>
    {
        var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        var context = accessor.HttpContext;

        if (context?.Request?.Cookies != null &&
            context.Request.Cookies.TryGetValue(".AspNetCore.Identity.Application", out var authCookie))
        {
            client.DefaultRequestHeaders.Add("Cookie", $".AspNetCore.Identity.Application={authCookie}");
        }

        client.BaseAddress = new Uri("https://localhost:7271");
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
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
