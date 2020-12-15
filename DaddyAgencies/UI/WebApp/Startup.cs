using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Mvc;
using DaddyAgencies.Application.Models.Enums;
using DaddyAgencies.Common.MediatR.CustomAttributes;
using Microsoft.Owin;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using WebApp.Helpers;

[assembly: OwinStartup(typeof(WebApp.Startup))]

namespace WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime), new DatetimeModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime?), new DatetimeModelBinder());
            ConfigureLogging();
            ConfigureIoC();
            ConfigureAuth(app);
        }
    }
}