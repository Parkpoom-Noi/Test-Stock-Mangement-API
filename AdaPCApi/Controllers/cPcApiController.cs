using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using AdaPCApi.Models;
using System.Globalization;

namespace AdaPCApi.Controllers
{

    [Authorize]
    [RoutePrefix("AdaPcApi")]
    public class cPcApiController : ApiController
    { 
        [AllowAnonymous]
        [Route("GetStockTransaction")]
        [HttpGet]

        public List<cmlResStockMasterSelect> GET_PRCoStockTransaction(cmlReqStockMasterSelect oVal)
        {
            cDbConController oDbCtrl = new cDbConController();
            List<cmlResStockMasterSelect> aoLstStkTsc = new List<cmlResStockMasterSelect>();
            using (SqlConnection oDbCon = oDbCtrl.C_PRCoDbCon())
            {
                string tDbSqlTxt = "SELECT [No.] ,[Transaction ID] ,[Part Number] ,[Part name] ,[Transaction type] ,[Transaction volume] ,[Transaction note] ,[Transaction Date] ,[Transaction by] FROM [dbo].[STP_PRCbGetStockTransaction]";
                using (SqlCommand oDbCmd = new SqlCommand(tDbSqlTxt, oDbCon))
                {
                    DataTable oDbTbl = new DataTable();
                    SqlDataAdapter oDbAdt = new SqlDataAdapter(oDbCmd);
                    oDbAdt.Fill(oDbTbl);
                    oDbAdt.Dispose();
                    foreach (DataRow oRow in oDbTbl.Rows)
                    {
                        cmlResStockMasterSelect oStkMst = new cmlResStockMasterSelect();
                        oStkMst.rnNo_ = long.Parse(oRow["No."].ToString());
                        oStkMst.rnTransaction_ID = int.Parse(oRow["Transaction ID"].ToString());
                        oStkMst.rnPart_Number = int.Parse(oRow["Part Number"].ToString());
                        oStkMst.rtPart_name = oRow["Part name"].ToString();
                        oStkMst.rtTransaction_type = oRow["Transaction type"].ToString();
                        oStkMst.rtTransaction_volume = oRow["Transaction volume"].ToString();
                        oStkMst.rtTransaction_note = oRow["Transaction note"].ToString();
                        oStkMst.rdTransaction_Date = DateTime.Parse(oRow["Transaction Date"].ToString());
                        oStkMst.rtTransaction_by = oRow["Transaction by"].ToString();
                        aoLstStkTsc.Add(oStkMst);
                    }
                }
                oDbCon.Close();
            }
            return aoLstStkTsc;
        }


        [AllowAnonymous]
        [Route("InsertStkTsc")]
        [HttpPost]
        public cmlResExcMdl POST_PRCoInsertStkTsc(cmlReqStockTransactionInsert oVal)
        {
            cmlResExcMdl oExeMdl = new cmlResExcMdl();
            cDbConController oDbCtrl = new cDbConController();
            using (SqlConnection oDbCon = oDbCtrl.C_PRCoDbCon())
            {
                string tDbSqlTxt = "INSERT INTO [dbo].[TPCTStkTsc]  \r\n" +
                                       "([FNStkMstID]  \r\n" +
                                       ",[FNStkTscTyp]  \r\n" +
                                       ",[FTStkTscVol]  \r\n" +
                                       ",[FTStkTscNte]  \r\n" +
                                       ",[FDStkTscDte]  \r\n" +
                                       ",[FNUsrID])  OUTPUT inserted.[FNStkTscID]  \r\n" +
                                 "VALUES  \r\n" +
                                       "( '" + oVal.pnFNStkMstID + "' \r\n" +
                                       ", '" + oVal.pnFNStkTscTyp + "' \r\n" +
                                       ", '" + oVal.ptFTStkTscVol + "' \r\n" +
                                       ", '" + oVal.ptFTStkTscNte + "'\r\n" +
                                       ", GETDATE() \r\n" +
                                       ", " + oVal.pnFNUsrID + ")";
                using (SqlCommand oDbCmd = new SqlCommand(tDbSqlTxt, oDbCon))
                {
                    try
                    {
                        int nIndexReturn = Int32.Parse(oDbCmd.ExecuteScalar().ToString());
                        if (nIndexReturn >= 1)
                        {
                            oExeMdl.rnResult = 0;
                            oExeMdl.rtCode = "OK";
                            oExeMdl.rtIndexRetrn = nIndexReturn.ToString();
                        }
                    }
                    catch (Exception oEx)
                    {
                        oExeMdl.rnResult = 1;
                        oExeMdl.rtCode = oEx.Message;
                    }
                }
                oDbCon.Close();
            }
            return oExeMdl;
        }


        [AllowAnonymous]
        [Route("UpdateStkMstTtl")]
        [HttpPost]
        public cmlResExcMdl POST_PRCoUpdateStkMstTtl(cmlReqStockMasterUpdate oVal)
        {
            cmlResExcMdl oExeMdl = new cmlResExcMdl();
            cDbConController oDbCtrl = new cDbConController();
            using (SqlConnection oDbCon = oDbCtrl.C_PRCoDbCon())
            {
                string tDbSqlTxt = "UPDATE [dbo].[TPCMStkMst] SET [FCStkMstTtl] = '" + oVal.pcFCStkMstTtl + "' WHERE [FNStkMstID] = " + oVal.pnFNStkMstID;
                using (SqlCommand oDbCmd = new SqlCommand(tDbSqlTxt, oDbCon))
                {
                    try
                    {
                        if (Int32.Parse(oDbCmd.ExecuteNonQuery().ToString()) >= 1)
                        {
                            oExeMdl.rnResult = 0;
                            oExeMdl.rtCode = "OK";
                        }
                        else
                        {
                            oExeMdl.rnResult = 1;
                            oExeMdl.rtCode = tDbSqlTxt;
                        }
                    }
                    catch (Exception oEx)
                    {
                        oExeMdl.rnResult = 1;
                        oExeMdl.rtIndexRetrn = oEx.Message;
                    }
                }
                oDbCon.Close();
            }
            return oExeMdl;
        }
    }
}
