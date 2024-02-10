using Demo_Project.DAL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDataRepo, ItemRepo>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200", "http://localhost:44452")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
        });
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("EnableCORS");
app.MapDefaultControllerRoute();

app.Run();
