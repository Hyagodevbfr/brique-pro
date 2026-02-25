namespace BriquePro.Domain.Common.ErrorsHandling
{
    public class Result<TValue>
    {
        public bool IsSuccess { get; }
        public TValue? Value { get; }
        public object? Error { get; }

        private Result(TValue value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        private Result(object error)
        {
            IsSuccess = false;
            Value = default!;
            Error = error;
        }

        public static Result<TValue> Success(TValue value) => new(value);

        public static Result<TValue> Failure(Error error) => new(error);

        public static Result<TValue> Failure(ValidationError validationError)
            => new(validationError);

        public static implicit operator Result<TValue>(TValue value) => Success(value);

        public static implicit operator Result<TValue>(Error error) => Failure(error);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public object? Error { get; }
        private Result()
        {
            IsSuccess = true;
            Error = null;
        }
        private Result(object error)
        {
            IsSuccess = false;
            Error = error;
        }
        public static Result Success() => new();
        public static Result Failure(Error error) => new(error);
        public static Result Failure(ValidationError validationError)
            => new(validationError);
        public static implicit operator Result(Error error) => Failure(error);
    }
}