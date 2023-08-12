using ErrorHandling.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new CustomHandlerExceptionFilterAttribute() { ErrorPage = "hata1" });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//1.Kullaným
app.UseStatusCodePages("text/plain", "bir hata var. Durum kodu:{0}");

//2.Kullaným
app.UseStatusCodePages(async context =>
{
    context.HttpContext.Response.ContentType = "text/plain";
    await context.HttpContext.Response.WriteAsync($"Bir hata var. Durum kodu: {context.HttpContext.Response.StatusCode} ");
});

//3.Kullaným
app.UseStatusCodePages(async context =>
{
    context.HttpContext.Response.ContentType = "text/html; charset=utf-8";

    await context.HttpContext.Response.WriteAsync("<html><body>");
    await context.HttpContext.Response.WriteAsync("<h1>Hata Oluþtu!</h1>");

    int statusCode = context.HttpContext.Response.StatusCode;

    switch (statusCode)
    {
        case 404:
            await context.HttpContext.Response.WriteAsync("<p>Sayfa bulunamadý.</p>");
            break;
        case 500:
            await context.HttpContext.Response.WriteAsync("<p>Ýçsel sunucu hatasý oluþtu.</p>");
            break;
        default:
            await context.HttpContext.Response.WriteAsync("<p>Beklenmedik bir hata oluþtu.</p>");
            break;
    }

    await context.HttpContext.Response.WriteAsync("</body></html>");
});

app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
