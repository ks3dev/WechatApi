using Infrastructure;
using Infrastructure.Core.DDD;
using Infrastructure.Core.EF;
using Infrastructure.Redis;
using Infrastructure.Redis.Config;
using Infrastructure.Redis.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WeiXin.Auth;
using WeiXin.Core;
using WeiXin.Core.Interface;
using WeiXin.Domain.Interfaces;
using WeiXin.Repositories;
using WeiXin.Repositories.Infrastructure;

namespace WeiXin
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
            services.Configure<RedisConfig>(Configuration.GetSection("RedisConfig"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddTransient<IRedisHelper, RedisHelper>();
            services.AddTransient<IAuthCore, AuthCore>();
            services.AddTransient(typeof(IQuery<>), typeof(Query<>));

            services.AddScoped<IUnitOfWork, MySqlDbContext>();
            services.AddScoped<IWxUsersRepository, WxUsersRepository>();
            services.AddScoped<IWxFriendsRepository, WxFriendsRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            services.AddDbContext<MySqlDbContext>();
            services.AddMvc(m=> {
                m.Filters.Add<FormAuthAttribute>();//表单验证过滤器
                m.Filters.Add<ErrorFilter>();//异常拦截过滤器
            }).AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //nlog
            loggerFactory.AddNLog();

            //TODO 异常处理拦截器无效
            //app.UseErrorHandling();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "login",
                    template: "{controller=Login}/{action=QrCode}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
