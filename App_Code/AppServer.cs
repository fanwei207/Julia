using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Progress.Open4GL.Proxy;   
using Progress.Open4GL.DynamicAPI;
using Microsoft.ApplicationBlocks.Data;

namespace OEAppServer
{
    public class AppServer
    {
        string appsvurl = System.Configuration.ConfigurationSettings.AppSettings["Appsever.TCP"];  
        string conqad = System.Configuration.ConfigurationSettings.AppSettings["SqlConn.QAD_DATA"];
		
		/*查询返回类方法*/
        public DataTable prodcost(String part,Decimal tax,Decimal rate,Decimal dif)
        {
            Connection conn = new Connection(appsvurl, "", "", "");
            conn.SessionModel = 1;
            OpenAppObject openAO = new OpenAppObject(conn, "TCPAPPSV");
            ParamArray pm1 = new ParamArray(0);
            OpenProcObject openPO = openAO.CreatePO("appsvmain.p", pm1);
            pm1.Clear();
            ParamArray parms = new ParamArray(5);
            parms.AddCharacter(0, part, ParamArrayMode.INPUT);
            parms.AddDecimal(1, tax, ParamArrayMode.INPUT);
            parms.AddDecimal(2, rate, ParamArrayMode.INPUT);
            parms.AddDecimal(3, dif, ParamArrayMode.INPUT);
            TempTableMetaData metaData1 = new TempTableMetaData("temppsmstr", null, 12, false, 0, null, null, null);
            parms.AddTableHandle(4, null, ParamArrayMode.OUTPUT, metaData1);
            openPO.RunProc("prodcost", parms);
            DataTable dt = (DataTable)parms.GetOutputParameter(4);
            parms.Clear();
            openPO.Dispose();
            openAO.Dispose();
            conn.ReleaseConnection();
            return dt;
        }
	    public DataTable moldmrp(DataTable dt)
        {
            dt.TableName = "tempmold";
            Connection conn = new Connection(appsvurl, "", "", "");
            conn.SessionModel = 1;
            OpenAppObject openAO = new OpenAppObject(conn, "TCPAPPSV");
            ParamArray pm1 = new ParamArray(0);
            OpenProcObject openPO = openAO.CreatePO("appsvmain.p", pm1);
            pm1.Clear();
            ParamArray parms = new ParamArray(1);
            TempTableMetaData metaData1 = new TempTableMetaData("tempmold", null, 6, false, 0, null, null, null);
            parms.AddTableHandle(0, dt, ParamArrayMode.INPUT_OUTPUT, metaData1);
            openPO.RunProc("moldptmrp", parms);
            dt = (DataTable)parms.GetOutputParameter(0);
            parms.Clear();
            openPO.Dispose();
            openAO.Dispose();
            conn.ReleaseConnection();
            return dt;
        }
        public DataSet moldfcst(String part,DateTime edate)
        {
            Connection conn = new Connection(appsvurl, "", "", "");
            conn.SessionModel = 1;
            OpenAppObject openAO = new OpenAppObject(conn, "TCPAPPSV");
            ParamArray pm1 = new ParamArray(0);
            OpenProcObject openPO = openAO.CreatePO("appsvmain.p", pm1);
            pm1.Clear();
            ParamArray parms = new ParamArray(3);
            parms.AddCharacter(0, part, ParamArrayMode.INPUT);
            parms.AddDate(1, edate, ParamArrayMode.INPUT);
            ProDataSetMetaData dsMetaData = new ProDataSetMetaData("promoldset", null);
            TempTableMetaData metaData0 = new TempTableMetaData("tpmold", null, 7, false, 0, null, null, null);
            TempTableMetaData metaData1 = new TempTableMetaData("tpdemd", null, 3, false, 0, null, null, null);
            TempTableMetaData metaData2 = new TempTableMetaData("tpfcdis", null, 5, false, 0, null, null, null);
            dsMetaData.AddDataTable(metaData0);
            dsMetaData.AddDataTable(metaData1);
            dsMetaData.AddDataTable(metaData2);
            parms.AddDatasetHandle(2, null, ParamArrayMode.OUTPUT, dsMetaData);
            openPO.RunProc("moldfcst", parms);
            DataSet ds = (DataSet)parms.GetOutputParameter(2);
            parms.Clear();
            openPO.Dispose();
            openAO.Dispose();
            conn.ReleaseConnection();
            return ds;
        }

        public DataSet purstate(String domain, String part, String part1, String vend, String vend1, String pnbr, String pnbr1, DateTime edate, DateTime edate1)
        {
            Connection conn = new Connection(appsvurl, "", "", "");
            conn.SessionModel = 1;
            OpenAppObject openAO = new OpenAppObject(conn, "TCPAPPSV");
            ParamArray pm1 = new ParamArray(0);
            OpenProcObject openPO = openAO.CreatePO("appsvmain.p", pm1);
            pm1.Clear();
            ParamArray parms = new ParamArray(10);
            parms.AddCharacter(0, domain, ParamArrayMode.INPUT);
            parms.AddCharacter(1, part, ParamArrayMode.INPUT);
            parms.AddCharacter(2, part1, ParamArrayMode.INPUT);
            parms.AddCharacter(3, vend, ParamArrayMode.INPUT);
            parms.AddCharacter(4, vend1, ParamArrayMode.INPUT);
            parms.AddCharacter(5, pnbr, ParamArrayMode.INPUT);
            parms.AddCharacter(6, pnbr1, ParamArrayMode.INPUT);
            parms.AddDate(7, edate, ParamArrayMode.INPUT);
            parms.AddDate(8, edate1, ParamArrayMode.INPUT);
            ProDataSetMetaData dsMetaData = new ProDataSetMetaData("prhset", null);
            TempTableMetaData metaData0 = new TempTableMetaData("tempurst", null, 13, false, 0, null, null, null);
            TempTableMetaData metaData1 = new TempTableMetaData("tpprh", null, 6, false, 0, null, null, null);
            dsMetaData.AddDataTable(metaData0);
            dsMetaData.AddDataTable(metaData1);
            parms.AddDatasetHandle(9, null, ParamArrayMode.OUTPUT, dsMetaData);
            openPO.RunProc("purstate", parms);
            DataSet ds = (DataSet)parms.GetOutputParameter(9);
            parms.Clear();
            openPO.Dispose();
            openAO.Dispose();
            conn.ReleaseConnection();
            return ds;
        }

		/*更新数据的方法*/
		public bool upptmstr(String uid)
        {
			try
			{
            Connection conn = new Connection(appsvurl, "", "", "");
            conn.SessionModel = 1;
            OpenAppObject openAO = new OpenAppObject(conn, "TCPAPPSV");
            ParamArray pm1 = new ParamArray(0);
            OpenProcObject openPO = openAO.CreatePO("appsvmain.p", pm1);
            pm1.Clear();
            ParamArray parms = new ParamArray(1);
			parms.AddCharacter(0, uid, ParamArrayMode.INPUT);
            openPO.RunProc("upptmstr", parms);
            parms.Clear();
            openPO.Dispose();
            openAO.Dispose();
            conn.ReleaseConnection();  
			return true;
			}
			catch(Exception ex)
			{
			return false;	
			}
        }
		
		/*以下方法用来实现远程过程调用，可代替WEBSERVICE的调用*/
	    public bool RPCMETHOD(String cmd)
        {
            Connection conn = new Connection(appsvurl, "", "", "");
            conn.SessionModel = 1;
            OpenAppObject openAO = new OpenAppObject(conn, "TCPAPPSV");
            ParamArray pm1 = new ParamArray(0);
            OpenProcObject openPO = openAO.CreatePO("appsvmain.p", pm1);
            pm1.Clear();
            ParamArray parms = new ParamArray(2);
            parms.AddCharacter(0, cmd, ParamArrayMode.INPUT);
            parms.AddLogical(1,false,ParamArrayMode.OUTPUT);
            openPO.RunProc("oscmd", parms);
            bool runok = !(bool)parms.GetOutputParameter(1);
            parms.Clear();
            openPO.Dispose();
            openAO.Dispose();
            conn.ReleaseConnection();
            return runok;
        }
		
		/*以下是非APPSERVER的方法，而是附带支持某些菜单*/
		public string getcurrate()
        {
            String strSQL = "SELECT top 1 exr_rate2 / exr_rate as rate FROM exr_rate where exr_domain = 'szx' order by exr_start_date desc";
            String rate = Convert.ToString(SqlHelper.ExecuteScalar(conqad, CommandType.Text, strSQL));
            if (rate != String.Empty)
            { 
                return rate;
            }
            else 
            {
               return "6.66";
            }
        }
    }
}