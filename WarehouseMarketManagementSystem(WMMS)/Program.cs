using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WMMS.BLL.Model.Exception;
using WMMS.BLL;
using WMMS.DAL;
using WMMS.DAL.Context;
using WMMS.Domain.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var Configuration = builder.Configuration;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.BusinessLogicInjection();
services.DataAccessDependencyInjectionMethod(Configuration);
services.AddControllers();



services.AddControllers(opt => opt.Filters.Add<GlobalExceptionHandler>());

services.AddIdentity<AppUser, AppRole>(op =>
{

}).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();



services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});


services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalProject.API", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please insert JWT with Bearer into field",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer"
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme
				{
				   Reference = new OpenApiReference
				   {
					  Type = ReferenceType.SecurityScheme,
					  Id = "Bearer"
				   }
			},
			new string[] { }
		}
	});
});


services.ConfigureApplicationCookie((configure) =>
{
	configure.Events.OnRedirectToLogin = context =>
	{
		context.Response.StatusCode = StatusCodes.Status401Unauthorized;
		return Task.CompletedTask;
	};
});


services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	o.Events = new JwtBearerEvents
	{
		OnAuthenticationFailed = context =>
		{

			return Task.CompletedTask;
		}
	};
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = Configuration["AppSettings:ValidIssuer"],
		ValidAudience = Configuration["AppSettings:ValidAudience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"])),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ClockSkew = TimeSpan.Zero

	};
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}



app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
