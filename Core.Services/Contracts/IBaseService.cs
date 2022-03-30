using System;
using System.Threading.Tasks;

namespace Core.Services.Contracts
{
    public interface IBaseService
    {
        IBaseService WithParameters(params (object data, Type validator)[] requestParameter);

        IBaseService WithParameters(string action, params (object data, Type validator)[] requestParameter);

        void Run(Action service);

        dynamic Run(Func<dynamic> service);

        Task RunAsync(Func<Task> service);

        Task<dynamic> RunAsync(Func<Task<dynamic>> service);
    }
}
