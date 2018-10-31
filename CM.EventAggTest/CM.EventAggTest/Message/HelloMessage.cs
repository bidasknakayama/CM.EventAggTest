using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.EventAggTest.Message
{
   
    public class HelloMessage
    {
        public Boolean UiAsync;
        public String msg;
        public HelloMessage(String msg, Boolean UiAsync)
        {
            this.UiAsync = UiAsync;
            this.msg = msg;
        }
    }
}
