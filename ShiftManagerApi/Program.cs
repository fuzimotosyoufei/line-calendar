using Microsoft.EntityFrameworkCore;
using ShiftManagerApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // あなたのGitHub PagesのURL（またはすべてのURL "*"）からのアクセスを許可する
        policy.WithOrigins("https://fuzimotosyoufei.github.io")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// ?? もともとあるCORS設定などの近くに追加します
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));//要解読

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(); //  これで上の許可設定が有効になります！

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
