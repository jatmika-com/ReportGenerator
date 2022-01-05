using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportGenerator
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      //set global thread worker pool
      int worker_min_threads = 128; //set your thread pool setting here to avoid thread starvation

      int minWorker, minIOC;
      // Get the current settings.
      ThreadPool.GetMinThreads(out minWorker, out minIOC);
      // Change the minimum number of worker threads to anything, but keep the old setting for minimum asynchronous I/O completion threads.
      if (ThreadPool.SetMinThreads(worker_min_threads, minIOC))
      {
        // The minimum number of threads was set successfully.
        Console.WriteLine("SetMinThreads to " + worker_min_threads + " succeded");
      }
      else
      {
        // The minimum number of threads was not changed.
        Console.WriteLine("SetMinThreads fail safe to default " + minWorker);
      }
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
