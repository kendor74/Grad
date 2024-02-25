
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add ConnectionStrings
var IdentityConnection = builder.Configuration.GetConnectionString("EDU_DataBase_IdentityUser");
builder.Services.AddDbContext<IdentityUserDbContext>(options => options.UseSqlServer(IdentityConnection));

var AppConnection = builder.Configuration.GetConnectionString("EDU_DataBase_App");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(AppConnection));


//Authentication

// to allow some Espcial Characters for UserName
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+#"; // Add any additional allowed characters

}).AddEntityFrameworkStores<IdentityUserDbContext>()
  .AddDefaultTokenProviders();


//Inject Interfaces with Services


builder.Services.AddScoped<IUser, UserServices>();

builder.Services.AddScoped<IRepository<Student>, RepositoryHandler<Student>>();
builder.Services.AddScoped<IRepository<StudentRoom>, RepositoryHandler<StudentRoom>>();
builder.Services.AddScoped<IRepository<TutorRoom>, RepositoryHandler<TutorRoom>>();
builder.Services.AddScoped<IRepository<Tutor>, RepositoryHandler<Tutor>>();
builder.Services.AddScoped<IRepository<Admin>, RepositoryHandler<Admin>>();
builder.Services.AddScoped<IRepository<Room>, RepositoryHandler<Room>>();
builder.Services.AddScoped<IRepository<Transaction>, RepositoryHandler<Transaction>>();
builder.Services.AddScoped<IRepository<Department>, RepositoryHandler<Department>>();




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
