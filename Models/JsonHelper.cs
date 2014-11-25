using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PremKaushal.Models
{
    public static class JsonHelper
    {
        public static string Serialize(Object obj)
        {
            string myJsonString;
            myJsonString = JsonConvert.SerializeObject(obj, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            return myJsonString;
        }
    }
}