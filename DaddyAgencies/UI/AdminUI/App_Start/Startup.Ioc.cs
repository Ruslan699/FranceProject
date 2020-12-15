using AdminUI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminUI
{
    public partial class Startup
    {
        private void ConfigureIoc()
        {
            ServiceConfig.Register();
        }
    }
}