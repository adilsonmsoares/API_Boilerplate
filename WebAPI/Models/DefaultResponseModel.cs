using System.Collections.Generic;

namespace WebAPI.Models
{
    public class DefaultResponseModel
    {
        public bool Success { get; set; } = true;

        public IList<ErrorResponseModel> Errors { get; set; }
    }
}
