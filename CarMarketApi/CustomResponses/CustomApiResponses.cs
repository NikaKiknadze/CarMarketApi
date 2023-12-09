namespace CarMarketApi.CustomResponses
{
    public class CustomApiResponses<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static CustomApiResponses<T> SuccesResult(T? data)
        {
            return new CustomApiResponses<T>()
            {
                Success = true,
                Data = data
            };
        }

        public static CustomApiResponses<T> ErrorResult(string? error)
        {
            return new CustomApiResponses<T>()
            {
                Success = false,
                Message = error
            };
        }
    }
}
