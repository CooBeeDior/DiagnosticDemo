using Microsoft.Extensions.DependencyInjection;
using PersistenceAbstraction;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageQueueAbstraction
{
    public interface IPublisher<TIn> where TIn : class
    {
        Task Publish(TIn request, CancellationToken cancellationToken = default(CancellationToken));
    }


    //public interface IPublisher<TIn, TOut> where TIn : class, IRequest<TOut>
    //{
    //    Task<TOut> Publish(TIn request, CancellationToken cancellationToken = default(CancellationToken));
    //}

    public class Publisher<TIn> : IPublisher<TIn> where TIn : class
    {
        private readonly IPersistence _persistence;
        public Publisher(Func<string, IPersistence> func)
        {
            _persistence = func.Invoke("Mongodb");
        }
        public async Task Publish(TIn request, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _persistence.InsertAsync(request);


        }
    }


    public static class Extensions
    {
        public static void AddPublisher(this IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IPublisher<>), typeof(Publisher<>), ServiceLifetime.Singleton));


        }
    }

    //public interface IBaseRequest
    //{
    //}

    //public interface IRequest<out TResponse> : IBaseRequest
    //{

    //}
}
