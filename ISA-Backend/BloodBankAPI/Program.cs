using BloodBankAPI.Materials.Consumer;
using BloodBankAPI.Materials.EmailSender;
using BloodBankAPI.Materials.PasswordHasher;
using BloodBankAPI.Materials.QRGenerator;
using BloodBankAPI.Model;
using BloodBankAPI.Repository;
using BloodBankAPI.Services.Addresses;
using BloodBankAPI.Services.Appointments;
using BloodBankAPI.Services.Authentication;
using BloodBankAPI.Services.BloodCenters;
using BloodBankAPI.Services.Forms;
using BloodBankAPI.Services.Questions;
using BloodBankAPI.Services.Users;
using BloodBankAPI.Settings;
using BloodBankAPI.UnitOfWork;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BloodBankDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BloodBankDb")).UseLazyLoadingProxies().LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
  
}
          );
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IEmailSendService, EmailSendService>();
builder.Services.AddScoped<IQRService, QRService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBloodCenterService, BloodCenterService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped( typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStoreLocation,StoreLocation>();
builder.Services.AddHostedService<ConsumerService>();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
    builder.Services.AddSingleton(emailConfig);


    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audence"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration
                    ["Jwt:Key"]))
        };
    });

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "BloodBank API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
             });
    });

builder.Configuration.AddJsonFile("appsettings.json");

// Create the RabbitMQ consumer
var config = builder.Configuration;
var consumer = new ConsumerService(config);



var app = builder.Build();

/*app.Use(async (context, next) =>
{
    // Run the RabbitMQ consumer asynchronously
    await consumer.ConsumeMessages();

    // Call the next middleware in the pipeline
    await next();
});*/


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "BloodBank API v1");
        });
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    }

    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseWebSockets();
    app.MapControllers();






app.Run();