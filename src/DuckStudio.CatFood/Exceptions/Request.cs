namespace DuckStudio.CatFood.Exceptions
{
    /// <summary>
    /// 请求异常
    /// </summary>
    public class Request
    {
        /// <summary>
        /// 请求时出现异常
        /// </summary>
        public class RequestException : Exception
        {
            public RequestException() : base() { }
            public RequestException(string message) : base(message) { }
            public RequestException(string message, Exception inner) : base(message, inner) { }
        }
    }
}
