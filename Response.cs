using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FieldAgent.Core
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}
