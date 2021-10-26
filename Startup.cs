using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace endpoints
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Endpoints Task:\n~/headers - get resquest headers\n ~/plural - pluralization\n ~/frequency - count words in request");
                });

                endpoints.MapGet("/headers", async context =>
                {
                    var collection = context.Request.Headers;
                    foreach (var i in collection)
                    {
                        await context.Response.WriteAsync(i.Key + ":" + i.Value + "\n");
                    }
                    
                });

                endpoints.MapGet("/plural", async context =>
                {
                    var parameters = context.Request.Query;

                    int num = int.Parse(parameters["number"]);
                    string[] forms = parameters["forms"].ToString().Split(',');

                    string responseData = Methods.Pluralization(num, forms[0], forms[1], forms[2]);
                    await context.Response.WriteAsync(responseData);
                });

                endpoints.MapPost("/frequency", async context =>
                {
                    string body;

                    body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    Dictionary<string, int> result = Methods.FindFrequency(body.Split(' '));

                    context.Response.ContentType = "application/json";
                    context.Response.Headers.Add("Words-count", result.Count.ToString());
                    context.Response.Headers.Add("Most-Frequent-Word", result.Max(x => x.Key));

                    await context.Response.WriteAsJsonAsync(result);
                });
            });
        }
    }
}
