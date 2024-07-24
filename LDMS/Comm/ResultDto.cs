using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMS.Comm
{
    internal class ResultDto<T>
    {
        /// <summary>
        /// 消息
        /// </summary>      
        public string Message { get; set; } = "fail";

        public long Total { get; set; } //TODO: Can be a long value..?

        public T Data { get; set; }

        public virtual void SetData(T data, long total)
        {
            this.Message = "success";
            this.Data = data;
            this.Total = total;
        }

        public virtual void Error(string message = "fail")
        {
            this.Message = message;
        }
    }
}
