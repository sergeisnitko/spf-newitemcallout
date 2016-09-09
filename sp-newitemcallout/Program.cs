using SP.Cmd.Deploy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_newitemcallout
{
    class Program
    {
        static void Main(string[] args)
        {
            SharePoint.CmdExecute(args, "SPF Newitem Callout",
                options =>
                {
                    Model.Deploy(options);
                },
                null,
                null
            );

            var t = "";
        }
    }
}
