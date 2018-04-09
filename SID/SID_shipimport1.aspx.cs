using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using QADSID;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;

public partial class SID_shipimport1 : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);
        
        }
    }
    protected void BtnShip_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        sid.DelTempShip(Convert.ToInt32(Session["uID"]));
        sid.DelImportError(Convert.ToInt32(Session["uID"]));

        int ErrorRecord = 0;

        if (!ImportExcelFile())
        {
            ErrorRecord += 1;

        }


        if (ErrorRecord == 0)
        {
            Int32 Ierr = 0;

            Ierr = sid.ImportShipData(Convert.ToInt32(Session["uID"]));
            if (Ierr < 0)
            {
                ltlAlert.Text = "alert('重复导入或导入错误！');";

            }
            else
            {
                if (sid.IsExistsAirShip(Convert.ToInt32(Session["uID"])))
                {
                    Session["EXTitle"] = "500^<b>系统提示</b>~^";
                    Session["EXHeader"] = "";
                    Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                    ltlAlert.Text = "alert('导入成功!但有警告!');window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
                }
                else
                {
                    ltlAlert.Text = "alert('导入成功!');";
                }
            }

        }
        else
        {
            Session["EXTitle"] = "500^<b>错误原因</b>~^";
            Session["EXHeader"] = "";
            Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
            ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }

    }

    public Boolean ImportExcelFile1()
    {
        //String strSQL = "";
        DataSet ds = new DataSet();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        //Boolean boolError = false;
        int ErrorRecord = 0;
        

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }
        
        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if(strFileName.Trim().Length <= 0)
        {
          ltlAlert.Text = "alert('请选择导入文件.');";
          return false;
        }
        strUserFileName = strFileName;

        //Modified By Shanzm 2012-12-27：唯一字符串可以设定为“年月日时分秒毫秒”
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

        #region 按SID_det_temp和ImportError的结构构建表
        DataColumn column;

        //构建ImportError
        DataTable tblError = new DataTable("import_error");

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "errInfo";
        tblError.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "uID";
        tblError.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "plantCode";
        tblError.Columns.Add(column);

        //构建SID_det_temp
        DataTable tblTemp = new DataTable("det_temp");
        
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "id";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sno";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qad";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_set";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_box";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qa";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "memo";
        tblTemp.Columns.Add(column);
        
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "so_nbr";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "so_line";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wo";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PO";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "cust_part";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "weight";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "volume";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_pcs";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_pkgs";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fob";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fedx";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "no";
        tblTemp.Columns.Add(column); 
        
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "atl";
        tblTemp.Columns.Add(column);
        
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "createdBy";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "createdDate";
        tblTemp.Columns.Add(column);
        #endregion

        if (filename1.PostedFile != null)
        {
           if(filename1.PostedFile.ContentLength > 8388608)
           {
               ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
               return false;
           }

           try
           {
               filename1.PostedFile.SaveAs(strFileName);
           }
           catch
           {
               ltlAlert.Text = "alert('上传文件失败.');";
               return false ;
           }

         

           if (File.Exists(strFileName))
           {
               //先把商检号取出来
               IList<QADSID.SID_QA> qaList = sid.SelectNeedQASNO(string.Empty);

               if (qaList == null)
               {
                   ltlAlert.Text = "alert('商检号获取失败!请刷新后重新操作一次！');";
                   return false;
               }

               // Get the WorkSheet Name
               String[] arrTable;
               arrTable = new string[20];

                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet." +
                               "OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + strFileName))
                   {
                       conn.Open();
                       DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                       for (int j = 0; j < dt.Rows.Count; j++)
                       {
                           if (dt.Rows[j][2].ToString().Trim().Substring(dt.Rows[j][2].ToString().Trim().Length - 1,1) == "$")
                           {
                               arrTable[j] = dt.Rows[j][2].ToString().Trim();
                           }
                           
                       }
                       conn.Close();
                   }

                   foreach (string aa in arrTable)
                   {
                       if (aa != null )
                       {
                           
                           try
                           {
                               ds = sid.getExcelContents(strFileName, aa);
                           }
                           catch
                           {
                               if (File.Exists(strFileName))
                               {
                                   File.Delete(strFileName);
                               }

                               ltlAlert.Text = "alert('导入文件必须是Excel格式或者模板及内容正确!" + aa + "');";
                               return false;
                           }

                           if (ds.Tables[0].Rows.Count > 0)
                           {
                               if (ds.Tables[0].Columns[0].ColumnName != "产品出运单")
                               {
                                   ds.Reset();
                                   ltlAlert.Text = "alert('导入文件的模版不正确!');";
                                   return false;
                               }

                               DataRow rowError;//错误表的行
                               DataRow rowTemp;//临时表的行

                               
                               rowTemp = tblTemp.NewRow();

                               String _PK = "";
                               String _PKref = "";
                               String _Nbr = "";
                               String _OutDate = "";
                               String _Via = "";
                               String _Ctype = "";
                               String _ShipDate = "";
                               String _ShipTo = "";
                               String _Site = "";
                               String _SNO = "";
                               String _QAD = "";
                               Decimal _Qty_set = 0;
                               Decimal _Qty_box = 0;
                               String _QA = "";
                               String _Memo = "";
                               String _So_nbr = "";
                               String _So_line = "";
                               String _WO = "";
                               String _PO = "";
                               String _Cust_part = "";
                               Decimal _Weight = 0;
                               Decimal _Volume = 0;
                               Decimal _qty_pkgs = 0;
                               Decimal _qty_pcs = 0;
                               String _domain = "";
                               String _Fob = "";
                               String _Fedx = "";
                               Int32 SID = 0;
                               String _NO = "";
                               String _ATL = "";
                               String _LCL = "";

                               string strErrMsg = string.Empty;//前端的错误信息
                               ErrorRecord = 0;


                               for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                               {
                                   _SNO = "";
                                   _QAD = "";
                                   _Qty_set = 0;
                                   _Qty_box = 0;
                                   _QA = "";
                                   _Memo = "";
                                   _So_nbr = "";
                                   _So_line = "";
                                   _WO = "";
                                   _PO = "";
                                   _Cust_part = "";
                                   _Weight = 0;
                                   _Volume = 0;
                                   _qty_pcs = 0;
                                   _qty_pkgs = 0;
                                   _NO = "";
                                   _ATL = "";

                                   #region 导入头栏
                                   if (i == 0)
                                   {
                                       if (ds.Tables[0].Rows[i].IsNull(0) || ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() != "出运单号:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "出运单位置不对,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Nbr = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                                           if (_Nbr.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "出运单不能为空,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(3) || ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim() != "系统货运单号:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "系统货运单位置不对,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _PK = ds.Tables[0].Rows[i].ItemArray[5].ToString().Trim();
                                           if (_PK.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "系统货运单不能为空,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(7) || ds.Tables[0].Rows[i].ItemArray[7].ToString().Trim() != "出厂日期:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "出厂日期位置不对,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _OutDate = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                                           if (_OutDate.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "出厂日期不能为空,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }

                                           Int32 strdate = 0;
                                           Int32 str2mon = 0;
                                           Int32 str3day = 0;
                                           Int32 str4hour = 0;
                                           try
                                           {
                                               strdate = Convert.ToInt32(_OutDate.Trim().Substring(0, 2));
                                               str2mon = Convert.ToInt32(_OutDate.Trim().Substring(3, 2));
                                               str3day = Convert.ToInt32(_OutDate.Trim().Substring(6, 2));
                                               str4hour = Convert.ToInt32(_OutDate.Trim().Substring(11, 2));
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "出厂日期格式不正确, 必须为14-12-01   05点装箱,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }


                                           //if (Convert.ToInt32(_OutDate.Trim().Substring(0, 2)) < 14 || Convert.ToInt32(_OutDate.Trim().Substring(3, 2)) > 12 || Convert.ToInt32(_OutDate.Trim().Substring(6, 2)) > 31 || Convert.ToInt32(_OutDate.Trim().Substring(11, 2)) > 24)
                                           if (strdate < 14 || str2mon > 12 || str3day > 31 || str4hour > 24)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "出厂日期不能为空,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                           _LCL = ds.Tables[0].Rows[i].ItemArray[13].ToString().Trim(); //dt.Rows[i].ItemArray[13].ToString().Trim();
                                       }
                                   }

                                   if (i == 1)
                                   {
                                       if (ds.Tables[0].Rows[i].IsNull(0) || ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() != "运输方式:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "运输方式位置不对,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Via = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                                           if (_Via.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "运输方式不能为空,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(3) || ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim() != "集箱箱型:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "集箱箱型位置不对,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Ctype = ds.Tables[0].Rows[i].ItemArray[5].ToString().Trim();
                                           if (_Ctype.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "集箱箱型不能为空,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(7) || ds.Tables[0].Rows[i].ItemArray[7].ToString().Trim() != "出运日期:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "出运日期位置不对,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _ShipDate = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                                           if (_ShipDate.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "出运日期不能为空,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                           else
                                           {
                                               if (!sid.IsDate(_ShipDate.Trim()))
                                               {
                                                   ErrorRecord += 1;

                                                   rowError = tblError.NewRow();

                                                   rowError["errInfo"] = "出运日期必须为日期类型,见表" + aa;
                                                   rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                   rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                   tblError.Rows.Add(rowError);
                                               }
                                           }
                                       }

                                   }

                                   if (i == 2)
                                   {
                                       if (ds.Tables[0].Rows[i].IsNull(0) || ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() != "运往:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "运往位置不对,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _ShipTo = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                                           if (_ShipTo.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "运往位置不能为空,见表" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(7) || ds.Tables[0].Rows[i].ItemArray[7].ToString() != "装箱地点:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "装箱地点不对,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Site = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(11))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "输入所在的公司,见表" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _domain = ds.Tables[0].Rows[i].ItemArray[11].ToString().Trim();
                                       }

                                       if (_Nbr.Length > 0)
                                       {
                                           if (Char.IsLetter(_Nbr, _Nbr.Length - 1))
                                           {
                                               _PKref = _Nbr.Substring(_Nbr.Length - 1, 1);
                                           }
                                           else
                                           {
                                               _PKref = "";
                                           }
                                       }

                                       if (ErrorRecord <= 0)
                                       {
                                           SID = sid.InsertTempMstr(Convert.ToInt32(Session["uID"]), _PK, _Nbr, _OutDate, _Via, _Ctype, _ShipDate, _ShipTo, _Site, _domain, _PKref,_LCL);
                                           if (SID <= 0)
                                           {
                                               ds.Reset();
                                               ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                                               break;//立即跳出，以导出错误提示
                                           }
                                       }
                                   }
                                   #endregion
                                   
                                   #region 导入明细项
                                   if (i >= 5)
                                   {
                                       rowTemp = tblTemp.NewRow();

                                       //first three column is null, break
                                       if (ds.Tables[0].Rows[i].IsNull(0) && ds.Tables[0].Rows[i].IsNull(1) && ds.Tables[0].Rows[i].IsNull(2))
                                       {
                                           break;
                                       }
                                       else
                                       {
                                           if (ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() == "" && ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim() == "" && ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim() == "")
                                           {
                                               break;
                                           }
                                       }

                                       //序号
                                       if (ds.Tables[0].Rows[i].IsNull(0))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "序号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _NO = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                                       }
                                       //系列
                                       if (ds.Tables[0].Rows[i].IsNull(1))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "系列不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _SNO = ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
                                       }


                                       //QAD
                                       if (ds.Tables[0].Rows[i].IsNull(2))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "QAD不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _QAD = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                                       }

                                       //客户物料
                                       if (ds.Tables[0].Rows[i].IsNull(3))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "客户物料不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Cust_part = ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim();
                                       }

                                       //套数
                                       if (ds.Tables[0].Rows[i].IsNull(4))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "出运套数不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _Qty_set = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[4]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "套数必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //只数
                                       if (ds.Tables[0].Rows[i].IsNull(5))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "只数不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _qty_pcs = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[5]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "只数必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //箱数
                                       if (ds.Tables[0].Rows[i].IsNull(6))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "箱数不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _Qty_box = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[6]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "箱数必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //件数
                                       if (ds.Tables[0].Rows[i].ItemArray[7].ToString().Trim().Length <= 0)
                                       {
                                           _qty_pkgs = 0;
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _qty_pkgs = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "件数必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //商检号 
                                       if (ds.Tables[0].Rows[i].IsNull(8))
                                       {
                                           _QA = "";
                                       }
                                       else
                                       {
                                           _QA = ds.Tables[0].Rows[i].ItemArray[8].ToString().Trim();
                                       }
                                       
                                       bool bQa = false;//标识商检号是否在qaList列表中
                                       foreach (QADSID.SID_QA sqa in qaList)//_SNO.Trim()) && )
                                       {
                                           if (sqa.SNO == _SNO.Trim())
                                           {
                                               bQa = true;
                                           }
                                       }

                                       if (bQa && _QA.Trim().Length == 0)
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = _SNO.Trim() + "系列的商检号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }

                                       //备注
                                       if (ds.Tables[0].Rows[i].IsNull(9))
                                       {
                                           _Memo = "";
                                       }
                                       else
                                       {
                                           _Memo = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                                       }

                                       //销售订单
                                       if (ds.Tables[0].Rows[i].IsNull(10))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "销售订单不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _So_nbr = ds.Tables[0].Rows[i].ItemArray[10].ToString().Trim();
                                       }

                                       //行号
                                       if (ds.Tables[0].Rows[i].IsNull(11))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "行号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _So_line = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[11]).ToString();
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "行号必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //批序号/加工单
                                       if (ds.Tables[0].Rows[i].IsNull(12))
                                       {
                                           _WO = "";
                                       }
                                       else
                                       {
                                           _WO = ds.Tables[0].Rows[i].ItemArray[12].ToString().Trim();
                                       }

                                       //TCP订单号
                                       if (ds.Tables[0].Rows[i].IsNull(13))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "客户订单号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _PO = ds.Tables[0].Rows[i].ItemArray[13].ToString().Trim();
                                       }

                                       //QAD是否存在EDI_DB..CP_PART
                                       if (!sid.ExistsQADShipImport(_QAD, _So_nbr, _So_line, _PO, Convert.ToInt32(Session["uID"])))
                                       {
                                           //ErrorRecord += 1;
                                           sid.InsertErrorInfo("该订单客户物料在物料表中不存在，请联系技术部增加，此处仅做提示！", aa, Convert.ToInt32(Session["uID"]), Convert.ToString(i + 2)); ;
                                       }

                                       //重量
                                       if (ds.Tables[0].Rows[i].IsNull(14))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "重量不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _Weight = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[14]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "重量必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //体积
                                       if (ds.Tables[0].Rows[i].IsNull(15))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "体积不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _Volume = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[15]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "体积必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //客户订单号
                                       if (ds.Tables[0].Rows[i].IsNull(16))
                                       {
                                           _Fob = "";
                                       }
                                       else
                                       {
                                           _Fob = ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim();
                                       }

                                       //ATL订单号
                                       if (ds.Tables[0].Rows[i].IsNull(17))
                                       {
                                           _ATL = "";
                                       }
                                       else
                                       {
                                           _ATL = ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim();
                                       }

                                       //Fedx跟踪号
                                       if (ds.Tables[0].Rows[i].IsNull(18))
                                       {
                                           _Fedx = "";
                                       }
                                       else
                                       {
                                           _Fedx = ds.Tables[0].Rows[i].ItemArray[18].ToString().Trim();
                                       }

                                       //空运
                                       if (_Via.ToUpper() != "A")
                                       {
                                           if (sid.IsAirShipImport(_PO, _So_line))
                                           {
                                               //ErrorRecord += 1;
                                               sid.InsertErrorInfo("该订单物料已经申请为空运，系统已经为您导入进去，此处仅做提示！", aa, Convert.ToInt32(Session["uID"]), Convert.ToString(i + 2)); ;
                                           }
                                       }

                                       if (ErrorRecord <= 0)
                                       {
                                           rowTemp = tblTemp.NewRow();

                                           rowTemp["id"] = SID;
                                           rowTemp["SNO"] = _SNO;
                                           rowTemp["QAD"] = _QAD;
                                           rowTemp["qty_set"] = _Qty_set;
                                           rowTemp["qty_box"] = _Qty_box;
                                           rowTemp["qa"] = _QA;
                                           rowTemp["memo"] = _Memo;
                                           rowTemp["so_nbr"] = _So_nbr;
                                           rowTemp["so_line"] = _So_line;
                                           rowTemp["wo"] = _WO;
                                           rowTemp["PO"] = _PO;
                                           rowTemp["cust_part"] = _Cust_part;
                                           rowTemp["weight"] = _Weight;
                                           rowTemp["volume"] = _Volume;
                                           rowTemp["qty_pcs"] = _qty_pcs;
                                           rowTemp["qty_pkgs"] = _qty_pkgs;
                                           rowTemp["fob"] = _Fob;
                                           rowTemp["fedx"] = _Fedx;
                                           rowTemp["no"] = _NO;
                                           rowTemp["atl"] = _ATL;
                                           rowTemp["createdby"] = Convert.ToInt32(Session["uID"]);
                                           rowTemp["createddate"] = DateTime.Now;

                                           tblTemp.Rows.Add(rowTemp);
                                       }
                                   }
                                   #endregion
                               }
                           } //ds.Tables[0].Rows.Count > 0                           
                       } // a != null)
                       ds.Reset();
                   } //foreach

                   //上传
                   if (tblError != null && tblError.Rows.Count > 0)
                   {
                       using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                       {
                           bulkCopyError.DestinationTableName = "dbo.ImportError";
                           bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                           bulkCopyError.ColumnMappings.Add("uID", "userID");
                           bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                           try
                           {
                               bulkCopyError.WriteToServer(tblError);
                           }
                           catch (Exception ex)
                           {
                               ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                               return false;
                           }
                           finally
                           {
                               bulkCopyError.Close();
                               tblError.Dispose();
                           }
                       }
                   }

                   if (tblTemp != null && tblTemp.Rows.Count > 0)
                   {
                       using (SqlBulkCopy bulkCopyTemp = new SqlBulkCopy(chk.dsn0()))
                       {
                           bulkCopyTemp.DestinationTableName = "dbo.SID_det_temp";
                           bulkCopyTemp.ColumnMappings.Add("id", "SID_id");
                           bulkCopyTemp.ColumnMappings.Add("sno", "SID_SNO");
                           bulkCopyTemp.ColumnMappings.Add("qad", "SID_QAD");
                           bulkCopyTemp.ColumnMappings.Add("qty_set", "SID_qty_set");
                           bulkCopyTemp.ColumnMappings.Add("qty_box", "SID_qty_box");
                           bulkCopyTemp.ColumnMappings.Add("qa", "SID_qa");
                           bulkCopyTemp.ColumnMappings.Add("memo", "SID_memo");
                           bulkCopyTemp.ColumnMappings.Add("so_nbr", "SID_so_nbr");
                           bulkCopyTemp.ColumnMappings.Add("so_line", "SID_so_line");
                           bulkCopyTemp.ColumnMappings.Add("wo", "SID_wo");
                           bulkCopyTemp.ColumnMappings.Add("PO", "SID_PO");
                           bulkCopyTemp.ColumnMappings.Add("cust_part", "SID_cust_part");
                           bulkCopyTemp.ColumnMappings.Add("weight", "SID_weight");
                           bulkCopyTemp.ColumnMappings.Add("volume", "SID_volume");
                           bulkCopyTemp.ColumnMappings.Add("qty_pcs", "SID_qty_pcs");
                           bulkCopyTemp.ColumnMappings.Add("qty_pkgs", "SID_qty_pkgs");
                           bulkCopyTemp.ColumnMappings.Add("fob", "SID_fob");
                           bulkCopyTemp.ColumnMappings.Add("fedx", "SID_fedx");
                           bulkCopyTemp.ColumnMappings.Add("no", "SID_no");
                           bulkCopyTemp.ColumnMappings.Add("atl", "SID_atl");
                           bulkCopyTemp.ColumnMappings.Add("createdBy", "SID_createdby");
                           bulkCopyTemp.ColumnMappings.Add("createdDate", "SID_createddate");

                           try
                           {
                               bulkCopyTemp.WriteToServer(tblTemp);
                           }
                           catch (Exception ex)
                           {
                               ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                               return false;
                           }
                           finally
                           {
                               bulkCopyTemp.Close();
                               tblTemp.Dispose();
                           }
                       }
                   }

                   if (File.Exists(strFileName))
                   {
                       File.Delete(strFileName);
                   }

           } //File.Exists(strFileName)
        } //filename1.PostedFile != null
        
        if (ErrorRecord <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public Boolean ImportExcelFile()
    {
        //String strSQL = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        //Boolean boolError = false;
        int ErrorRecord = 0;


        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return false;
        }
        strUserFileName = strFileName;

        //Modified By Shanzm 2012-12-27：唯一字符串可以设定为“年月日时分秒毫秒”
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

        #region 按SID_det_temp和ImportError的结构构建表
        DataColumn column;

        //构建ImportError
        DataTable tblError = new DataTable("import_error");

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "errInfo";
        tblError.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "uID";
        tblError.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "plantCode";
        tblError.Columns.Add(column);

        //构建SID_det_temp
        DataTable tblTemp = new DataTable("det_temp");

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "id";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sno";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qad";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_set";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_box";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qa";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "memo";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "so_nbr";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "so_line";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wo";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PO";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "cust_part";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "weight";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "volume";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_pcs";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_pkgs";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fob";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fedx";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "no";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "atl";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qadpo";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "qadline";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "createdBy";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "createdDate";
        tblTemp.Columns.Add(column);
        #endregion

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }



            if (File.Exists(strFileName))
            {
                //先把商检号取出来
                IList<QADSID.SID_QA> qaList = sid.SelectNeedQASNO(string.Empty);

                if (qaList == null)
                {
                    ltlAlert.Text = "alert('商检号获取失败!请刷新后重新操作一次！');";
                    return false;
                }

                // Get the WorkSheet Name
                int[] arrTable;

                using (FileStream sr = new FileStream(strFileName, FileMode.OpenOrCreate))
                {   //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档   
                    HSSFWorkbook workbook = new HSSFWorkbook(sr);
                    int x = workbook.Workbook.NumSheets;
                    arrTable = new int[x];
                    List<string> sheetNames = new List<string>();
                    for (int j = 0; j < x; j++)
                    {
                        arrTable[j] = j;//workbook.Workbook.GetSheetName(j);
                    }
                }


                #region //循环所有Sheet
                foreach (int aa in arrTable)
                {
                    if (aa != null)
                    {

                        try
                        {
                            //ds = sid.getExcelContents(strFileName, aa);
                            dt = this.GetExcelContents(strFileName, aa);
                        }
                        catch
                        {
                            if (File.Exists(strFileName))
                            {
                                File.Delete(strFileName);
                            }

                            ltlAlert.Text = "alert('导入文件必须是Excel格式或者模板及内容正确!" + aa + "');";
                            return false;
                        }

                        #region //读取Table数据
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Columns[0].ColumnName != "产品出运单")
                            {
                                ds.Reset();
                                ltlAlert.Text = "alert('导入文件的模版不正确!');";
                                return false;
                            }

                            DataRow rowError;//错误表的行
                            DataRow rowTemp;//临时表的行


                            rowTemp = tblTemp.NewRow();

                            String _PK = "";
                            String _PKref = "";
                            String _Nbr = "";
                            String _OutDate = "";
                            String _Via = "";
                            String _Ctype = "";
                            String _ShipDate = "";
                            String _ShipTo = "";
                            String _Site = "";
                            String _SNO = "";
                            String _QAD = "";
                            Decimal _Qty_set = 0;
                            Decimal _Qty_box = 0;
                            String _QA = "";
                            String _Memo = "";
                            String _So_nbr = "";
                            String _So_line = "";
                            String _WO = "";
                            String _PO = "";
                            String _Cust_part = "";
                            Decimal _Weight = 0;
                            Decimal _Volume = 0;
                            Decimal _qty_pkgs = 0;
                            Decimal _qty_pcs = 0;
                            String _domain = "";
                            String _Fob = "";
                            String _Fedx = "";
                            Int32 SID = 0;
                            String _NO = "";
                            String _ATL = "";
                            String _QADPO = "";
                            String _QADLine = "";
                            String _LCL = "";

                            string strErrMsg = string.Empty;//前端的错误信息
                            ErrorRecord = 0;


                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                _SNO = "";
                                _QAD = "";
                                _Qty_set = 0;
                                _Qty_box = 0;
                                _QA = "";
                                _Memo = "";
                                _So_nbr = "";
                                _So_line = "";
                                _WO = "";
                                _PO = "";
                                _Cust_part = "";
                                _Weight = 0;
                                _Volume = 0;
                                _qty_pcs = 0;
                                _qty_pkgs = 0;
                                _NO = "";
                                _ATL = "";
                                _QADPO = "";
                                _QADLine = "";

                                #region 导入头栏
                                if (i == 0)
                                {
                                    if (dt.Rows[i].IsNull(0) || dt.Rows[i].ItemArray[0].ToString().Trim() != "出运单号:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "出运单位置不对,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Nbr = dt.Rows[i].ItemArray[2].ToString().Trim();
                                        if (_Nbr.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "出运单不能为空,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(3) || dt.Rows[i].ItemArray[3].ToString().Trim() != "系统货运单号:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "系统货运单位置不对,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _PK = dt.Rows[i].ItemArray[5].ToString().Trim();
                                        if (_PK.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "系统货运单不能为空,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(7) || dt.Rows[i].ItemArray[7].ToString().Trim() != "出厂日期:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "出厂日期位置不对,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _OutDate = dt.Rows[i].ItemArray[9].ToString().Trim();
                                        if (_OutDate.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "出厂日期不能为空,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }

                                        Int32 strdate = 0;
                                        Int32 str2mon = 0;
                                        Int32 str3day = 0;
                                        Int32 str4hour = 0;
                                        try
                                        {
                                            strdate = Convert.ToInt32(_OutDate.Trim().Substring(0, 2));
                                            str2mon = Convert.ToInt32(_OutDate.Trim().Substring(3, 2));
                                            str3day = Convert.ToInt32(_OutDate.Trim().Substring(6, 2));
                                            str4hour = Convert.ToInt32(_OutDate.Trim().Substring(11, 2));
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "出厂日期格式不正确, 必须为14-12-01   05点装箱,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }


                                        //if (Convert.ToInt32(_OutDate.Trim().Substring(0, 2)) < 14 || Convert.ToInt32(_OutDate.Trim().Substring(3, 2)) > 12 || Convert.ToInt32(_OutDate.Trim().Substring(6, 2)) > 31 || Convert.ToInt32(_OutDate.Trim().Substring(11, 2)) > 24)
                                        if (strdate < 14 || str2mon > 12 || str3day > 31 || str4hour > 24)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "出厂日期不能为空,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                        _LCL = dt.Rows[i].ItemArray[13].ToString().Trim();

                                    }
                                }

                                if (i == 1)
                                {
                                    if (dt.Rows[i].IsNull(0) || dt.Rows[i].ItemArray[0].ToString().Trim() != "运输方式:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "运输方式位置不对,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Via = dt.Rows[i].ItemArray[2].ToString().Trim();
                                        if (_Via.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "运输方式不能为空,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(3) || dt.Rows[i].ItemArray[3].ToString().Trim() != "集箱箱型:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "集箱箱型位置不对,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Ctype = dt.Rows[i].ItemArray[5].ToString().Trim();
                                        if (_Ctype.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "集箱箱型不能为空,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(7) || dt.Rows[i].ItemArray[7].ToString().Trim() != "出运日期:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "出运日期位置不对,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _ShipDate = dt.Rows[i].ItemArray[9].ToString().Trim();
                                        if (_ShipDate.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "出运日期不能为空,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                        else
                                        {
                                            if (!sid.IsDate(_ShipDate.Trim()))
                                            {
                                                ErrorRecord += 1;

                                                rowError = tblError.NewRow();

                                                rowError["errInfo"] = "出运日期必须为日期类型,见表" + aa;
                                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                tblError.Rows.Add(rowError);
                                            }
                                        }
                                    }

                                }

                                if (i == 2)
                                {
                                    if (dt.Rows[i].IsNull(0) || dt.Rows[i].ItemArray[0].ToString().Trim() != "运往:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "运往位置不对,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _ShipTo = dt.Rows[i].ItemArray[2].ToString().Trim();
                                        if (_ShipTo.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "运往位置不能为空,见表" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(7) || dt.Rows[i].ItemArray[7].ToString() != "装箱地点:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "装箱地点不对,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Site = dt.Rows[i].ItemArray[9].ToString().Trim();
                                    }

                                    if (dt.Rows[i].IsNull(11))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "输入所在的公司,见表" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _domain = dt.Rows[i].ItemArray[11].ToString().Trim();
                                    }

                                    if (_Nbr.Length > 0)
                                    {
                                        if (Char.IsLetter(_Nbr, _Nbr.Length - 1))
                                        {
                                            _PKref = _Nbr.Substring(_Nbr.Length - 1, 1);
                                        }
                                        else
                                        {
                                            _PKref = "";
                                        }
                                    }

                                    if (ErrorRecord <= 0)
                                    {
                                        SID = sid.InsertTempMstr(Convert.ToInt32(Session["uID"]), _PK, _Nbr, _OutDate, _Via, _Ctype, _ShipDate, _ShipTo, _Site, _domain, _PKref, _LCL);
                                        if (SID <= 0)
                                        {
                                            ds.Reset();
                                            ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                                            break;//立即跳出，以导出错误提示
                                        }
                                    }
                                }
                                #endregion

                                #region 导入明细项
                                if (i >= 5)
                                {
                                    rowTemp = tblTemp.NewRow();

                                    //first three column is null, break
                                    if (dt.Rows[i].IsNull(0) && dt.Rows[i].IsNull(1) && dt.Rows[i].IsNull(2))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (dt.Rows[i].ItemArray[0].ToString().Trim() == "" && dt.Rows[i].ItemArray[1].ToString().Trim() == "" && dt.Rows[i].ItemArray[2].ToString().Trim() == "")
                                        {
                                            break;
                                        }
                                    }

                                    //序号
                                    if (dt.Rows[i].IsNull(0))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "序号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _NO = dt.Rows[i].ItemArray[0].ToString().Trim();
                                    }
                                    //系列
                                    if (dt.Rows[i].IsNull(1))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "系列不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _SNO = dt.Rows[i].ItemArray[1].ToString().Trim();
                                    }


                                    //QAD
                                    if (dt.Rows[i].IsNull(2))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "QAD不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _QAD = dt.Rows[i].ItemArray[2].ToString().Trim();
                                    }

                                    //客户物料
                                    if (dt.Rows[i].IsNull(3))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "客户物料不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Cust_part = dt.Rows[i].ItemArray[3].ToString().Trim();
                                    }

                                    //套数
                                    if (dt.Rows[i].IsNull(4))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "出运套数不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _Qty_set = Convert.ToDecimal(dt.Rows[i].ItemArray[4]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "套数必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //只数
                                    if (dt.Rows[i].IsNull(5))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "只数不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _qty_pcs = Convert.ToDecimal(dt.Rows[i].ItemArray[5]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "只数必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //箱数
                                    if (dt.Rows[i].IsNull(6))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "箱数不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _Qty_box = Convert.ToDecimal(dt.Rows[i].ItemArray[6]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "箱数必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //件数
                                    if (dt.Rows[i].ItemArray[7].ToString().Trim().Length <= 0)
                                    {
                                        _qty_pkgs = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _qty_pkgs = Convert.ToDecimal(dt.Rows[i].ItemArray[7]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "件数必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //商检号 
                                    if (dt.Rows[i].IsNull(8))
                                    {
                                        _QA = "";
                                    }
                                    else
                                    {
                                        _QA = dt.Rows[i].ItemArray[8].ToString().Trim();
                                    }

                                    bool bQa = false;//标识商检号是否在qaList列表中
                                    foreach (QADSID.SID_QA sqa in qaList)//_SNO.Trim()) && )
                                    {
                                        if (sqa.SNO == _SNO.Trim())
                                        {
                                            bQa = true;
                                        }
                                    }

                                    if (bQa && _QA.Trim().Length == 0)
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = _SNO.Trim() + "系列的商检号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }

                                    //备注
                                    if (dt.Rows[i].IsNull(9))
                                    {
                                        _Memo = "";
                                    }
                                    else
                                    {
                                        _Memo = dt.Rows[i].ItemArray[9].ToString().Trim();
                                    }

                                    //销售订单
                                    if (dt.Rows[i].IsNull(10))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "销售订单不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _So_nbr = dt.Rows[i].ItemArray[10].ToString().Trim();
                                    }

                                    //行号
                                    if (dt.Rows[i].IsNull(11))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "行号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _So_line = Convert.ToInt32(dt.Rows[i].ItemArray[11]).ToString();
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "行号必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //批序号/加工单
                                    if (dt.Rows[i].IsNull(12))
                                    {
                                        _WO = "";
                                    }
                                    else
                                    {
                                        _WO = dt.Rows[i].ItemArray[12].ToString().Trim();
                                    }

                                    //TCP订单号
                                    if (dt.Rows[i].IsNull(13))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "客户订单号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _PO = dt.Rows[i].ItemArray[13].ToString().Trim();
                                    }

                                    //QAD是否存在EDI_DB..CP_PART
                                    if (!sid.ExistsQADShipImport(_QAD, _So_nbr, _So_line, _PO, Convert.ToInt32(Session["uID"])))
                                    {
                                        //ErrorRecord += 1;
                                        sid.InsertErrorInfo("该订单客户物料在物料表中不存在，请联系技术部增加，此处仅做提示！", aa.ToString(), Convert.ToInt32(Session["uID"]), Convert.ToString(i + 2)); ;
                                    }

                                    //重量
                                    if (dt.Rows[i].IsNull(14))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "重量不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _Weight = Convert.ToDecimal(dt.Rows[i].ItemArray[14]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "重量必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //体积
                                    if (dt.Rows[i].IsNull(15))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "体积不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _Volume = Convert.ToDecimal(dt.Rows[i].ItemArray[15]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "体积必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //客户订单号
                                    if (dt.Rows[i].IsNull(16))
                                    {
                                        _Fob = "";
                                    }
                                    else
                                    {
                                        _Fob = dt.Rows[i].ItemArray[16].ToString().Trim();
                                    }

                                    //ATL订单号
                                    if (dt.Rows[i].IsNull(17))
                                    {
                                        _ATL = "";
                                    }
                                    else
                                    {
                                        _ATL = dt.Rows[i].ItemArray[17].ToString().Trim();
                                    }

                                    //Fedx跟踪号
                                    if (dt.Rows[i].IsNull(18))
                                    {
                                        _Fedx = "";
                                    }
                                    else
                                    {
                                        _Fedx = dt.Rows[i].ItemArray[18].ToString().Trim();
                                    }

                                    //QAD客户订单
                                    if (dt.Rows[i].IsNull(19))
                                    {
                                        _QADPO = "";
                                    }
                                    else
                                    {
                                        _QADPO = dt.Rows[i].ItemArray[19].ToString().Trim();
                                    }

                                    //QAD客户订单行
                                    if (dt.Rows[i].IsNull(20))
                                    {
                                        _QADLine = "";
                                    }
                                    else
                                    {
                                        _QADLine = dt.Rows[i].ItemArray[20].ToString().Trim();//Convert.ToInt32(dt.Rows[i].ItemArray[18].ToString());
                                    }

                                    //空运
                                    if (_Via.ToUpper() != "A")
                                    {
                                        if (sid.IsAirShipImport(_PO, _So_line))
                                        {
                                            //ErrorRecord += 1;
                                            sid.InsertErrorInfo("该订单物料已经申请为空运，系统已经为您导入进去，此处仅做提示！", aa.ToString(), Convert.ToInt32(Session["uID"]), Convert.ToString(i + 2)); ;
                                        }
                                    }

                                    if (ErrorRecord <= 0)
                                    {
                                        rowTemp = tblTemp.NewRow();

                                        rowTemp["id"] = SID;
                                        rowTemp["SNO"] = _SNO;
                                        rowTemp["QAD"] = _QAD;
                                        rowTemp["qty_set"] = _Qty_set;
                                        rowTemp["qty_box"] = _Qty_box;
                                        rowTemp["qa"] = _QA;
                                        rowTemp["memo"] = _Memo;
                                        rowTemp["so_nbr"] = _So_nbr;
                                        rowTemp["so_line"] = _So_line;
                                        rowTemp["wo"] = _WO;
                                        rowTemp["PO"] = _PO;
                                        rowTemp["cust_part"] = _Cust_part;
                                        rowTemp["weight"] = _Weight;
                                        rowTemp["volume"] = _Volume;
                                        rowTemp["qty_pcs"] = _qty_pcs;
                                        rowTemp["qty_pkgs"] = _qty_pkgs;
                                        rowTemp["fob"] = _Fob;
                                        rowTemp["fedx"] = _Fedx;
                                        rowTemp["no"] = _NO;
                                        rowTemp["atl"] = _ATL;
                                        rowTemp["qadpo"] = _QADPO;
                                        if (string.IsNullOrEmpty(_QADLine))
                                        {
                                            rowTemp["qadline"] = 0;
                                        }
                                        else
                                        {
                                            rowTemp["qadline"] = Convert.ToInt32(_QADLine);
                                        }
                                        rowTemp["createdby"] = Convert.ToInt32(Session["uID"]);
                                        rowTemp["createddate"] = DateTime.Now;

                                        tblTemp.Rows.Add(rowTemp);
                                    }
                                }
                                #endregion
                            }
                        } //ds.Tables[0].Rows.Count > 0    
                        #endregion

                    } // a != null)
                    ds.Reset();
                } //foreach
                #endregion

                //上传
                if (tblError != null && tblError.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                    {
                        bulkCopyError.DestinationTableName = "dbo.ImportError";
                        bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                        bulkCopyError.ColumnMappings.Add("uID", "userID");
                        bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                        try
                        {
                            bulkCopyError.WriteToServer(tblError);
                        }
                        catch (Exception ex)
                        {
                            ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                            return false;
                        }
                        finally
                        {
                            bulkCopyError.Close();
                            tblError.Dispose();
                        }
                    }
                }

                if (tblTemp != null && tblTemp.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkCopyTemp = new SqlBulkCopy(chk.dsn0()))
                    {
                        bulkCopyTemp.DestinationTableName = "dbo.SID_det_temp";
                        bulkCopyTemp.ColumnMappings.Add("id", "SID_id");
                        bulkCopyTemp.ColumnMappings.Add("sno", "SID_SNO");
                        bulkCopyTemp.ColumnMappings.Add("qad", "SID_QAD");
                        bulkCopyTemp.ColumnMappings.Add("qty_set", "SID_qty_set");
                        bulkCopyTemp.ColumnMappings.Add("qty_box", "SID_qty_box");
                        bulkCopyTemp.ColumnMappings.Add("qa", "SID_qa");
                        bulkCopyTemp.ColumnMappings.Add("memo", "SID_memo");
                        bulkCopyTemp.ColumnMappings.Add("so_nbr", "SID_so_nbr");
                        bulkCopyTemp.ColumnMappings.Add("so_line", "SID_so_line");
                        bulkCopyTemp.ColumnMappings.Add("wo", "SID_wo");
                        bulkCopyTemp.ColumnMappings.Add("PO", "SID_PO");
                        bulkCopyTemp.ColumnMappings.Add("cust_part", "SID_cust_part");
                        bulkCopyTemp.ColumnMappings.Add("weight", "SID_weight");
                        bulkCopyTemp.ColumnMappings.Add("volume", "SID_volume");
                        bulkCopyTemp.ColumnMappings.Add("qty_pcs", "SID_qty_pcs");
                        bulkCopyTemp.ColumnMappings.Add("qty_pkgs", "SID_qty_pkgs");
                        bulkCopyTemp.ColumnMappings.Add("fob", "SID_fob");
                        bulkCopyTemp.ColumnMappings.Add("fedx", "SID_fedx");
                        bulkCopyTemp.ColumnMappings.Add("no", "SID_no");
                        bulkCopyTemp.ColumnMappings.Add("atl", "SID_atl");
                        bulkCopyTemp.ColumnMappings.Add("qadpo", "SID_qadpo");
                        bulkCopyTemp.ColumnMappings.Add("qadline", "SID_qadline");
                        bulkCopyTemp.ColumnMappings.Add("createdBy", "SID_createdby");
                        bulkCopyTemp.ColumnMappings.Add("createdDate", "SID_createddate");

                        try
                        {
                            bulkCopyTemp.WriteToServer(tblTemp);
                        }
                        catch (Exception ex)
                        {
                            ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                            return false;
                        }
                        finally
                        {
                            bulkCopyTemp.Close();
                            tblTemp.Dispose();
                        }
                    }
                }

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            } //File.Exists(strFileName)
        } //filename1.PostedFile != null

        if (ErrorRecord <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    #region 采用NPOI读取Excel
    /// <summary>
    /// 采用NPOI读取Excel
    /// </summary>
    /// <param name="excelPath">要读取的Excel路径</param>
    /// <param name="header">不能为空！验证Excel表头。格式是：客户,物料号,价格</param>
    /// <returns></returns>
    public DataTable GetExcelContents(string excelPath, int Num)
    {
        string ext = Path.GetExtension(excelPath);
        DataTable dt = null;
        if (ext == ".xls")
        {
            dt = GetExcelContent2003(excelPath, Num);
        }
        else
        {
            dt = GetExcelContent2007(excelPath, Num);
        }
        return dt;
    }
    #endregion

    /// <summary>
    /// 采用NPOI读取Excel2003
    /// </summary>
    /// <param name="excelPath">要读取的Excel路径</param>
    /// <param name="header">不能为空！验证Excel表头。格式是：客户,物料号,价格</param>
    /// <returns></returns>
    public DataTable GetExcelContent2003(string excelPath, int count)
    {
        if (File.Exists(excelPath))
        {
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            FileStream fileStream = new FileStream(excelPath, FileMode.Open);
            IWorkbook workbook = new HSSFWorkbook(fileStream);

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(count);

            DataTable table = new DataTable();
            //获取sheet的首行
            IRow headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum + 1;

            for (int i = (sheet.FirstRowNum + 1); i < rowCount; i++)
            {
                try
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    dataRow[j] = "";
                                    break;
                                case CellType.String:
                                    dataRow[j] = cell.StringCellValue;
                                    break;
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = cell.DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = cell.NumericCellValue;
                                    }
                                    break;
                                case CellType.Formula:
                                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                                    dataRow[j] = e.Evaluate(cell).StringValue;
                                    break;
                                default:
                                    dataRow[j] = cell.ToString();
                                    break;
                            }
                        }
                    }

                    table.Rows.Add(dataRow);
                }
                catch
                {
                    continue;
                }
            }

            workbook = null;
            sheet = null;

            return table;
        }
        else
        {
            return null;
        }
    }

    /// <summary>  
    /// 采用NPOI读取Excel2007
    /// 将Excel文件中的数据读出到DataTable中(xlsx)  
    /// </summary>  
    /// <param name="file"></param>  
    /// <returns></returns>  
    public DataTable GetExcelContent2007(string file, int count)
    {
        DataTable dt = new DataTable();
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
            ISheet sheet = xssfworkbook.GetSheetAt(count);

            //表头  
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            List<int> columns = new List<int>();
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    //continue;  
                }
                else
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                columns.Add(i);
            }
            //数据  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell);
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }

    /// <summary>  
    /// 获取单元格类型(xlsx)  
    /// </summary>  
    /// <param name="cell"></param>  
    /// <returns></returns>  
    private static object GetValueTypeForXLSX(XSSFCell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank:
                return null;
            case CellType.Boolean:
                return cell.BooleanCellValue;
            case CellType.Numeric:
                return cell.NumericCellValue;
            case CellType.String:
                return cell.StringCellValue;
            case CellType.Error:
                return cell.ErrorCellValue;
            case CellType.Formula:
            default:
                return "=" + cell.CellFormula;
        }
    }

}
