using DevExtreme.AspNet.Data.ResponseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminUI.Helpers
{
    public static class Methods
    {
        public static string GetSerializeObject(this LoadResult loadResult)
        {
            return JsonConvert.SerializeObject(loadResult, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
        }
    }
}