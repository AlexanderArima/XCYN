using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common;
using XCYN.Winform.Report;

namespace XCYN.Winform.Model.Report
{
    public class ReportState2 : AbstractState
    {
        public override object Handle(Context context)
        {
            if (context.reportName.Equals("CrystalReport2.rpt"))
            {
                return HandleAsync(context).Result;
            }
            else
            {
                throw new Exception("未找到报表文件："+ context.reportName);
            }
        }
        
        public async Task<object> HandleAsync(Context context)
        {
            var table = new DataTable();
            table = await Task.Run(() =>
            {
                table = DbHelperSQL.GetDataTable(@"
                    SELECT TOP 1000 [ID] ,[Name] ,[Author] ,[Price]
                    FROM [Book].[dbo].[MyBook]");
                return table;
            });
            CrystalReport2 c = new CrystalReport2();
            c.SetDataSource(table);
            return c;
        }
    }
}
