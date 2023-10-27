
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add ConnectionStrings
//var connection = builder.Configuration.GetConnectionString("RestApi");
//builder.Services.AddDbContext<ReDbContext>(options => options.UseSqlServer(connection));

//var IdentityConnection = builder.Configuration.GetConnectionString("IdentityApi");
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(IdentityConnection));







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
