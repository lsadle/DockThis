using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DbApi.Database;
using DbApi.Services;
using DbApi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DbApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connectionString = GetConnectionString();
            services.AddDbContext<DockThisContext>
                (options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }

        private string GetConnectionString()
        {
            const string name = "DockThisDbConnection";
            string connectionString = null;
            var dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());
            var dirs0 = Directory.GetDirectories("/");
            var dirs1 = Directory.GetDirectories("../");
            var dirs2 = Directory.GetDirectories("../run");

#if DEBUG
            connectionString = Configuration[name];
#else
            const string dockerSecretsPath = "/run/secrets/";
            if (Directory.Exists(dockerSecretsPath))
            {
                IFileProvider provider = new PhysicalFileProvider(dockerSecretsPath);
                IFileInfo fileInfo = provider.GetFileInfo(name);
                if (fileInfo.Exists)
                {
                    using (var stream = fileInfo.CreateReadStream())
                    using (var streamReader = new StreamReader(stream))
                    {
                        connectionString = streamReader.ReadToEnd();
                    }
                }
            }
#endif
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"Could not find connection string under '{name}'");
            }

            return connectionString;
        }
    }
}
