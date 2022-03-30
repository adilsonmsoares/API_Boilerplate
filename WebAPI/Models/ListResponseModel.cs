using System.Collections.Generic;

namespace WebAPI.Models
{
    public class ListResponseModel<T> : DefaultResponseModel where T : class
    {
        public IList<T> Data { get; set; }
    }
}
