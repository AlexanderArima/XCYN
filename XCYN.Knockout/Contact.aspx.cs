using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCYN.Knockout
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            const int i = 1;
            using (FileStream stream = new FileStream("c://1.txt", FileMode.Create))
            {
                var b = System.Text.Encoding.Default.GetBytes("111");
                stream.Write(b, 0, b.Length);
            }
        }
    }
}