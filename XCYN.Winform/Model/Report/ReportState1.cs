using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common;
using XCYN.Winform.Report;

namespace XCYN.Winform.Model.Report
{
    public class ReportState1 : AbstractState
    {
        public override object Handle(Context context)
        {
            if (context.reportName.Equals("CrystalReport1.rpt"))
            {
                return HandleAsync(context).Result;
            }
            else
            {
                context.state = new ReportState2();
                return context.Request();
            }
        }

        public async Task<object> HandleAsync(Context context)
        {
            var table = new DataTable();

            table = await Task.Run(() =>
            {
                table = DbHelperSQL.GetDataTable(@"
                        SELECT TOP 1000 [ID],[Name],[Standard],[BatchNumber],[VolumeNumber],
                        [Brand],[Licence],[Size],[Weight],[PrintDate],[AddTime],[State] 
                        FROM T_Wire");
                return table;
            });

            CrystalReport1 c = new CrystalReport1();
            c.SetDataSource(table);
            return c;
        }
    }
}
