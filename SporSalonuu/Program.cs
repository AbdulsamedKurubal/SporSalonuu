using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using SporSalonuu.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. MVC Servisini (Controller ve View'larý) sisteme ekle
builder.Services.AddControllersWithViews();

// 2. Veritabaný Baðlantýsý (SporSalonuContext)
builder.Services.AddDbContext<SporSalonuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Çerez (Cookie) ile Kimlik Doðrulama Ayarý
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Kullanýcý giriþ yapmamýþsa bu sayfaya yönlendir:
        options.LoginPath = "/Hesap/Giris";
        // Oturum 60 dakika boyunca açýk kalsýn:
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

var app = builder.Build();

// Hata ayýklama modlarý (Otomatik gelen kodlar)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot klasörünü (CSS, JS, Resimler) açar

app.UseRouting();

// --- DÝKKAT: Sýralama Çok Önemli ---
app.UseAuthentication(); // Önce: Kimsin? (Kimlik Doðrulama)
app.UseAuthorization();  // Sonra: Yetkin var mý? (Yetkilendirme)
// ------------------------------------

// Varsayýlan Rota Ayarý (Anasayfa)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();