using System;
using System.Linq;
using Elasticsearch.Net;
using FitApp.UserPrivateTrainingDetailRepository.Abstract;
using FitApp.UserPrivateTrainingDetailRepository.Settings;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace FitApp.UserPrivateTrainingDetailRepository
{
    public static class UserPrivateTrainingDetailRepositoryExtension
    {
        public static IServiceCollection AddUserPrivateTrainingDetailRepository(this IServiceCollection services)
        {
            UserPrivateTrainingDetailRepositorySettings settings = new UserPrivateTrainingDetailRepositorySettings();
            //var elasticUrl = "http://localhost:9200/";
            /*switch (environmentMode)
            {
                case EnvironmentMode.Qa:
                    // elasticUrl = "http://192.168.55.60:9200/";
                    elasticUrl = "http://192.168.49.56:9200/";
                    break;
                case EnvironmentMode.Prod:
                    // elasticUrl = "http://10.168.100.102:9200/;http://10.168.100.103:9200/;http://10.168.100.104:9200/";
                    
                    elasticUrl = "http://10.168.36.81:9200/;http://10.168.36.82:9200/;http://10.168.36.83:9200/;" +
                                 "http://10.168.36.84:9200/;http://10.168.36.85:9200/;http://10.168.36.86:9200/;" +
                                 "http://10.168.36.87:9200/;http://10.168.36.88:9200/;http://10.168.36.89:9200/";
                    
                    settings.NumberOfReplicas = 1;
                    settings.NumberOfShards = 5;
                    break;
                // ReSharper disable once RedundantCaseLabel
                case EnvironmentMode.Local:
                // ReSharper disable once RedundantCaseLabel
                case EnvironmentMode.Test:
                default:
                    // elasticUrl = "http://192.168.55.12:9200/";
                    elasticUrl = "http://192.168.49.55:9200/";
                    break;
            }*/

            //var uriList = elasticUrl.Split(';').Select(x => new Uri(x));
            //var pool = new StaticConnectionPool(uriList);
            var cloudId =
                "fitapp:ZXUtd2VzdC0yLmF3cy5jbG91ZC5lcy5pbyQwODY1NTg0NjE0ZTk0MzllYjUxNmEzYzg4ZWVjMTk4NSQ0ZTZiZjE1NDlhNzQ0OTI2YjNkODM3NDBjNDRjZjVhMQ==";
            var credentials = new BasicAuthenticationCredentials("elastic", "3g6RTJgBBRDKLB3V1C4dy9kg");
            var pool = new CloudConnectionPool(cloudId, credentials);
            var elasticConnectionSettings = new ConnectionSettings(pool).ThrowExceptions().EnableDebugMode();
            var elasticClient = new ElasticClient(elasticConnectionSettings);
            var repository = new UserPrivateTrainingDetailRepository(elasticClient, settings);
            services.AddSingleton<IUserPrivateTrainingDetailRepository>(repository);

            return services;
        }
    }
}