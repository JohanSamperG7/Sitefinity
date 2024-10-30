using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Renderer.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();
builder.Services.AddScoped<NavbarWidgetViewModel>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSitefinity();
app.MapBlazorHub();

app.UseEndpoints(endpoints =>
{
    endpoints.MapSitefinityEndpoints();
});

if (app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("Content-Security-Policy",
            "default-src 'self'; connect-src 'self' wss://localhost:44329 https://*.insight.sitefinity.com https://*.dec.sitefinity.com wss://localhost;");
        await next();
    });
}

app.Run();