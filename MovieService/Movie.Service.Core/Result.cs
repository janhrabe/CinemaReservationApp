namespace MovieService.Core
{
    public class Result<T>
    {


        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public T? Value { get; }


        private Result(bool isSuccess, string errorMessage, T? value)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Value = value;
        }
        public Result(bool isSuccess, string errorMessage, object value)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Value = (T)value;
        }

        public static Result<T> Success(T value) => new(true, string.Empty, value);


        public static Result<T> Failure(string errorMessage) => new(false, errorMessage, default);


        public static Result<T> Success() => new(true, string.Empty, default);


        public static Result<T> Failure() => new(false, string.Empty, default);

        public static Result<T> NotFound() => new(false, "Not Found", default);
    }

    public class Result(bool isSuccess, string errorMessage) : Result<object>(isSuccess, errorMessage, null)
    {
        public static new Result Success() => new(true, string.Empty);


        public static new Result Failure(string errorMessage) => new(false, errorMessage);
        public static new Result NotFound() => new(false, "Not Found");

    }
}