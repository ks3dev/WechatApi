using Newtonsoft.Json;
using System;
namespace Infrastructure.Data
{
    public class CustomerException: Exception
    {
        public CustomerException(string message, int code) : base(
            JsonConvert.SerializeObject(new { Message = message, Code = code }))
        {
        }
        public static void Throw(string message, int code)
        {
            throw new CustomerException(message, code);
        }
    }
}
