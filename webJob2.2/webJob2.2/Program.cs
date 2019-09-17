using System;
using Microsoft.Extensions.Hosting;

namespace webJob2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorage();
            });
            var host = builder.Build();
            using (host)
            {
                host.Run();
            }
        }
    }
}
