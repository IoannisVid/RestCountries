namespace RestCountries.Application.Common.Exceptions
{
    public abstract class AppException : Exception
    {
        public virtual int StatusCode { get; }
        protected AppException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
