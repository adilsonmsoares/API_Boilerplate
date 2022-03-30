using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.Contracts;
using Core.Services.Helpers;
using Domain.Entities.Extensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Core.Services.Implementations
{
    public class BaseService : IBaseService
    {
        private readonly ILogger _logger;
        private string _action;
        private (object data, Type validator)[] _requestParameter;

        public BaseService(ILogger<BaseService> logger)
        {
            _logger = logger;
            _requestParameter = new (object data, Type validator)[] { };
        }

        public IBaseService WithParameters(params (object data, Type validator)[] requestParameter)
        {
            return WithParameters(null, requestParameter);
        }

        public IBaseService WithParameters(string action, params (object data, Type validator)[] requestParameter)
        {
            _action = action;
            _requestParameter = requestParameter;

            return this;
        }

        public void Run(Action service)
        {
            ValidateRequestData();

            LogInfo();

            service();
        }

        public dynamic Run(Func<dynamic> service)
        {
            ValidateRequestData();

            var response = service();

            LogInfo(response);

            return response;
        }

        public async Task RunAsync(Func<Task> service)
        {
            ValidateRequestData();

            LogInfo();

            await service();
        }

        public async Task<dynamic> RunAsync(Func<Task<dynamic>> service)
        {
            ValidateRequestData();

            var response = await service();

            LogInfo(response);

            return response;
        }

        #region Private Methods

        private void LogInfo(dynamic response = null)
        {
            if (string.IsNullOrEmpty(_action))
            {
                return;
            }

            var message = $"Request data: {_requestParameter.Select(e => e.data).ToJson()}";

            if (response != null)
            {
                message += $" Response message: {response.ToString()}";
            }

            LoggingHelper.LogInformation(_logger, message, _action);
        }

        private void ValidateRequestData()
        {
            foreach (var (data, validatorType) in _requestParameter)
            {
                var context = new ValidationContext<object>(data);
                var validator = ((IValidator)Activator.CreateInstance(validatorType));
                var validationResult = validator.Validate(context);

                if (validationResult.IsValid)
                {
                    continue;
                }

                var errors = validationResult.Errors.Select(x => x.ErrorMessage);

                LoggingHelper.LogError(_logger, errors.ToJson(), _action);

                throw new ArgumentException(errors.ToJson());
            }
        }

        #endregion
    }
}
