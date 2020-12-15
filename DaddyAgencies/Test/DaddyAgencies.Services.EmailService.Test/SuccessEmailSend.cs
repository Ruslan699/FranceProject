using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Services.EmailService.Test
{
    [TestClass]
    public class SuccessEmailSend
    {
        [TestMethod]
        public void Send()
        {
            var sender = new EmailSender();
            //sender.Send();
        }
    }
}
