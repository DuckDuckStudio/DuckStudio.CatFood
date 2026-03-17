namespace DuckStudio.CatFood.Exceptions
{
    /// <summary>
    /// 操作异常
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// 当前操作失败
        /// </summary>
        public class OperationFailed : Exception
        {
            public OperationFailed() : base() { }
            public OperationFailed(string message) : base(message) { }
            public OperationFailed(string message, Exception inner) : base(message, inner) { }
        }

        /// <summary>
        /// 尝试当前方法失败，请尝试其他方法
        /// </summary>
        public class TryOtherMethods : Exception
        {
            public TryOtherMethods() : base() { }
            public TryOtherMethods(string message) : base(message) { }
            public TryOtherMethods(string message, Exception inner) : base(message, inner) { }
        }

        /// <summary>
        /// 取消后续操作
        /// </summary>
        public class CancelOther : Exception
        {
            public CancelOther() : base() { }
            public CancelOther(string message) : base(message) { }
            public CancelOther(string message, Exception inner) : base(message, inner) { }
        }

        /// <summary>
        /// 不支持的操作
        /// </summary>
        public class OperationNotSupported : Exception
        {
            public OperationNotSupported() : base() { }
            public OperationNotSupported(string message) : base(message) { }
            public OperationNotSupported(string message, Exception inner) : base(message, inner) { }
        }
    }
}
