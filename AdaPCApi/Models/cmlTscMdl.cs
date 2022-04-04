using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaPCApi.Models
{
    public class cmlReqStockMasterSelect
    {
        public Nullable<long> pnNo_ { get; set; }
        public int pnTransaction_ID { get; set; }
        public int pnPart_Number { get; set; }
        public string ptPart_name { get; set; }
        public string ptTransaction_type { get; set; }
        public string ptTransaction_volume { get; set; }
        public string ptTransaction_note { get; set; }
        public Nullable<System.DateTime> pdTransaction_Date { get; set; }
        public string ptTransaction_by { get; set; }
    }
    public class cmlResStockMasterSelect
    {
        public Nullable<long> rnNo_ { get; set; }
        public int rnTransaction_ID { get; set; }
        public int rnPart_Number { get; set; }
        public string rtPart_name { get; set; }
        public string rtTransaction_type { get; set; }
        public string rtTransaction_volume { get; set; }
        public string rtTransaction_note { get; set; }
        public Nullable<System.DateTime> rdTransaction_Date { get; set; }
        public string rtTransaction_by { get; set; }
    }

    public class cmlReqStockTransactionInsert
    {
        public int pnFNStkMstID { get; set; }
        public Nullable<int> pnFNStkTscTyp { get; set; }
        public string ptFTStkTscVol { get; set; }
        public string ptFTStkTscNte { get; set; }
        public int pnFNUsrID { get; set; }
         
    }

    public class cmlReqStockMasterUpdate
    {
        public int pnFNStkMstID { get; set; }
        public decimal pcFCStkMstTtl { get; set; }
    }


}