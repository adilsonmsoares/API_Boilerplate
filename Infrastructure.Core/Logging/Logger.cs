using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Infrastructure.Core.Logging
{
    public static class Logger
    {
        public static void Init(IConfiguration configuration)
        {
            string elasticUri = configuration["ElasticSearch:Uri"];
            string index = configuration["ElasticSearch:Index"];

            if (!string.IsNullOrEmpty(elasticUri) && !string.IsNullOrEmpty(index))
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(elasticUri))
                        {
                            AutoRegisterTemplate = true,
                            IndexFormat = index
                        })
                    .CreateLogger();
            }
        }
    }
}
