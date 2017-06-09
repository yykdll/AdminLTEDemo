using System.ComponentModel;

namespace MvcBase.Controls
{
    public class ReturnResult
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public ReturnResult()
        {
            this.Status = 500;
        }
    }
    public class ReturnResult<TData> : ReturnResult
    {
        public TData Data { get; set; }
    }
}
