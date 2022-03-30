using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class BaseController : Controller
    {
        protected IMapper Mapper { get; }

        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected JsonResult OK()
        {
            return new JsonResult(new DefaultResponseModel());
        }

        protected JsonResult OK<TModel>(dynamic data) where TModel : class
        {
            if (data.GetType().IsGenericType)
            {
                return new JsonResult(new ListResponseModel<TModel>() { Data = Mapper.Map<IList<TModel>>(data) });
            }

            return new JsonResult(new SingleResponseModel<TModel>() { Data = Mapper.Map<TModel>(data) });
        }
    }
}
