using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Service.WCF
{
    [DataContract(Namespace = "http://XCYNService",Name = "StockPrice")]
    public class StockPrice
    {
        [DataMember(Name = "CurrentPrice", Order = 0,IsRequired = true)]
        public double theCurrentPriceNow;

        [DataMember(Name = "CurrentTime", Order = 1, IsRequired = true)]
        public DateTime theCurrentTimeNow;

        [DataMember(Name = "Ticker", Order = 2, IsRequired = true)]
        public string theTickerSymbol;

        [DataMember(Name = "DailyVolume", Order = 3, IsRequired = false)]
        public long theDailyVolumeSoFar;

        [DataMember(Name = "DailyChange", Order = 4, IsRequired = false)]
        public double theDailyChangeSoFar;

        public override string ToString()
        {
            return string.Format("TickerSymbol:{0},CurrentPriceNow:{1},CurrentTimeNow:{2},DailyVolumnSoFar:{3},DailyChangeSoFar:{4}",
                   theCurrentPriceNow,theCurrentTimeNow,theTickerSymbol,theDailyVolumeSoFar,theDailyChangeSoFar);
        }
    }
}
