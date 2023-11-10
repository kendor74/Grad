
using EducationlPlatform.Models.InterfaceHandler.UsersHandler;
using EducationlPlatform.Models.Interfaces.UserInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add ConnectionStrings
var connection = builder.Configuration.GetConnectionString("EDU_DataBase");
builder.Services.AddDbContext<IdentityUserDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));


//Authentication
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityUserDbContext>()
    .AddDefaultTokenProviders();


//Inject Interfaces with Services

builder.Services.AddScoped<IService<Student> , StudentServices>();
builder.Services.AddScoped<IStudent , StudentServices>();

builder.Services.AddScoped<IService<Admin>, AdminServices>();
builder.Services.AddScoped<IAdmin, AdminServices>();

builder.Services.AddScoped<IService<Tutor>, TutorServices>();
builder.Services.AddScoped<ITutor, TutorServices>();



//JWT Bearer Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(options =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audiance").Value,
        IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
    };
#pragma warning restore CS8604 // Possible null reference argument.
});



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
