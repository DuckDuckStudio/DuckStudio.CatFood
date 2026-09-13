using DuckStudio.CatFood.Exceptions;

namespace DuckStudio.CatFood.Tests.Exceptions
{
    public class ExceptionTests
    {
        [Theory]
        [InlineData(typeof(Operation.OperationFailed))]
        [InlineData(typeof(Operation.TryOtherMethods))]
        [InlineData(typeof(Operation.CancelOther))]
        [InlineData(typeof(Operation.OperationNotSupported))]
        [InlineData(typeof(Request.RequestException))]
        public void CustomExceptionsSupportMessageAndInnerException(Type exceptionType)
        {
            Exception inner = new InvalidOperationException("inner");
            Exception exception = (Exception)Activator.CreateInstance(
                exceptionType,
                "message",
                inner)!;

            Assert.Equal("message", exception.Message);
            Assert.Same(inner, exception.InnerException);
        }
    }
}