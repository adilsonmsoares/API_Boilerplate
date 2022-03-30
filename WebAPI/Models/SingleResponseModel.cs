namespace WebAPI.Models
{
    public class SingleResponseModel<T>: DefaultResponseModel where T : class
    {
        public T Data { get; set; }
    }
}
