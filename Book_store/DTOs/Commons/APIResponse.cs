namespace Book_store.DTOs.Commons
{
    public class APIResponse<T>
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public dynamic Metadata { get; set; }
        public T Data { get; set; }
    }
}
