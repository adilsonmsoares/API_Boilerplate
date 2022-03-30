using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Tests.Extensions
{
    public static class ConverterExtensions
    {
        public static DefaultResponseModel AsDefault(this IActionResult result)
        {
            var jsonResult = result as JsonResult;

            return jsonResult.Value as DefaultResponseModel;
        }

        public static SingleResponseModel<TModel> AsSingle<TModel>(this IActionResult result) where TModel : class
        {
            var jsonResult = result as JsonResult;

            return jsonResult.Value as SingleResponseModel<TModel>;
        }

        public static ListResponseModel<TModel> AsList<TModel>(this IActionResult result) where TModel : class
        {
            var jsonResult = result as JsonResult;

            return jsonResult.Value as ListResponseModel<TModel>;
        }
    }
}
