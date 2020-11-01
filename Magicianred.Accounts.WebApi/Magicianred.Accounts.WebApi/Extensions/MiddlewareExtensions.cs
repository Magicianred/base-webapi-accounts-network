using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Magicianred.Accounts.Domain.Models.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Magicianred.Accounts.WebApi.Extensions
{
    public static class MiddlewareExtensions
	{
		public static IServiceCollection AddJwtTokenSetup(this IServiceCollection services, IConfiguration Configuration)
		{
			services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
			var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

			var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
			services.AddSingleton(signingConfigurations);


			services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = false,
					ValidateIssuerSigningKey = true,
					ValidIssuer = tokenOptions.Issuer, // Configuration["JwtToken:Issuer"],
					ValidAudience = tokenOptions.Audience, // Configuration["JwtToken:Issuer"],
					IssuerSigningKey = signingConfigurations.SecurityKey, // new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtToken:SecretKey"]))
					ClockSkew = TimeSpan.Zero
				};

				options.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
						{
							context.Response.Headers.Add("Token-Expired", "true");
						}
						return Task.CompletedTask;
					}
				};
			});
			return services;
		}

		public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(cfg =>
			{
				cfg.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "BASE WEBAPI ACCOUNTS",
					Version = "v0.1",
					Description = "A webapi to handles accounts",
					Contact = new OpenApiContact
					{
						Name = "Magicianred",
						Url = new Uri("https://github.com/Magicianred")
					},
					License = new OpenApiLicense
					{
						Name = "MIT",
					},
				});

				cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "JSON Web Token to access resources. Example: Bearer {token}",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
						},
						new [] { string.Empty }
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				cfg.IncludeXmlComments(xmlPath);
			});

			return services;
		}

		public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger().UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "BASE WEBAPI ACCOUNTS");
				options.DocumentTitle = "BASE WEBAPI ACCOUNTS";
			});

			return app;
		}
	}
}