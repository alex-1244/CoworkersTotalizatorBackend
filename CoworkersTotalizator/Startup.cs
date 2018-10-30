using System.Linq;
using System.Reflection;
using CoworkersTotalizator.Core;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Filters;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoworkersTotalizator
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment env)
		{
			Configuration = configuration;
			Environment = env;
		}

		private IHostingEnvironment Environment { get; set; }

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(opt =>
			{
				opt.Filters.Add(new AuthorizationMetadata());
			}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddDbContext<CoworkersTotalizatorContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("CoworkersTotalizatorDatabase"),
					sqlOptions =>
						sqlOptions.MigrationsAssembly(typeof(CoworkersTotalizatorContext).GetTypeInfo().Assembly.GetName().Name));
			});

			var allowedDomainsArr = Configuration.GetSection("AllowedEmailDomains").GetChildren().ToArray().Select(c => c.Value)
				.ToArray();
			var emailPass = Configuration.GetSection("EmailPassword").Value;

			services.AddScoped(provider =>
					new LoginService(
						(CoworkersTotalizatorContext) provider.GetService(typeof(CoworkersTotalizatorContext)),
						emailPass,
						allowedDomainsArr))
				.AddSingleton(provider => this.Environment)
				.AddScoped<LotteryService>();
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.TryAddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors(builder => builder.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials());

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc(routes =>
			{
				routes.MapRoute("login", "{controller=Login}/{action=Login}");
			});
		}
	}
}
