namespace RealEstateMediaPlatform.API.Common
{
    public class ApiResponse<T>
    {
        public bool Succeed { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

//写 ？ 是为了能接受null值，不然会报错

// static 适用于创建新的静态对象，不需要依赖已有的对象。
//static 是一种通过类名直接调用的方法，不依赖“某个现有实例”才能工作。它特别适合只依赖输入参数的计算、转换、验证和对象创建方法，但不代表它一定没有外部依赖或完全不受其他状态影响。
        public static ApiResponse<T> Success(T data, string? message = null) 
        //string? message = null，写这个=null是为了提高可读性，提前赋值。不然后面调用参数还得自己手动写一个null，不写就会报错。
        {
            return new ApiResponse<T>
            {
                Succeed = true,
                Data = data,
                Message = message
            };
        }
        public static ApiResponse<T> Fail(string errorMessage, string? errorCode = null)
        {
            return new ApiResponse<T>
            {
                Succeed = false,
                ErrorMessage = errorMessage,
                ErrorCode = errorCode
            };
        }
    }

}
