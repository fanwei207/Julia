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
using CommClass;

public partial class EDIPOImport :BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //每个更新要设置权限
            foreach (ListItem item in chkList.Items)
            {
                if (item.Value == "10")
                {
                    item.Enabled = this.Security["10000101"].isValid;
                }
                else if (item.Value == "20")
                {
                    item.Enabled = this.Security["10000102"].isValid;
                }
                else if (item.Value == "30")
                {
                    item.Enabled = this.Security["10000103"].isValid;
                }
                else if (item.Value == "40")
                {
                    item.Enabled = this.Security["10000104"].isValid;
                }
                else if (item.Value == "120")
                {
                    item.Enabled = this.Security["10000104"].isValid;
                }
                else if (item.Value == "50")
                {
                    item.Enabled = this.Security["10000105"].isValid;
                }
                else if (item.Value == "60")
                {
                    item.Enabled = this.Security["10000106"].isValid;
                }
                else if (item.Value == "70")
                {
                    item.Enabled = this.Security["10000107"].isValid;
                }
                else if (item.Value == "90")
                {
                    item.Enabled = this.Security["10000108"].isValid;
                }
                else if (item.Value == "100")
                {
                    item.Enabled = this.Security["10000109"].isValid;
                }
                else if (item.Value == "110")
                {
                    item.Enabled = this.Security["10000111"].isValid;
                }
                else if (item.Value == "120")
                {
                    item.Enabled = this.Security["10000112"].isValid;
                }
            }
        }
    }

    private bool InsertBatchTemp(string uID, bool bPlanDate, bool bDueDate, bool bUnitPrice, bool bSite, bool bCust, bool bPart, bool bCustPart, bool bSoNbr, bool bQty, bool bSample, bool bremark, bool bDomain, bool bPromisedDeliveryDate)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[15];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@bPlanDate", bPlanDate);
            sqlParam[2] = new SqlParameter("@bDueDate", bDueDate);
            sqlParam[3] = new SqlParameter("@bUnitPrice", bUnitPrice);
            sqlParam[4] = new SqlParameter("@bSite", bSite);
            sqlParam[5] = new SqlParameter("@bCust", bCust);
            sqlParam[6] = new SqlParameter("@bPart", bPart);
            sqlParam[7] = new SqlParameter("@bCustPart", bCustPart);
            sqlParam[8] = new SqlParameter("@bSoNbr", bSoNbr);
            sqlParam[9] = new SqlParameter("@bQty", bQty);

            sqlParam[10] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[10].Direction = ParameterDirection.Output;
            sqlParam[11] = new SqlParameter("@bSample", bSample);
            sqlParam[12] = new SqlParameter("@bremark", bremark);
            sqlParam[13] = new SqlParameter("@bDomain", bDomain);
            sqlParam[14] = new SqlParameter("@bPromisedDeliveryDate", bPromisedDeliveryDate);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertbatchEDIPO", sqlParam);

            return Convert.ToBoolean(sqlParam[10].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckValidity(string uID, bool bPlanDate, bool bDueDate, bool bUnitPrice, bool bSite, bool bCust, bool bPart, bool bCustPart, bool bSoNbr, bool bQty, bool bSample, bool bremark, bool bDomain, bool bPromisedDeliveryDate)
    {
        try
        {
            string strSql = "sp_edi_checkEDIPOValidity";

            SqlParameter[] sqlParam = new SqlParameter[14];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@bPlanDate", bPlanDate);
            sqlParam[2] = new SqlParameter("@bDueDate", bDueDate);
            sqlParam[3] = new SqlParameter("@bUnitPrice", bUnitPrice);
            sqlParam[4] = new SqlParameter("@bSite", bSite);
            sqlParam[5] = new SqlParameter("@bCust", bCust);
            sqlParam[6] = new SqlParameter("@bPart", bPart);
            sqlParam[7] = new SqlParameter("@bCustPart", bCustPart);
            sqlParam[8] = new SqlParameter("@bSoNbr", bSoNbr);
            sqlParam[9] = new SqlParameter("@bQty", bQty);

            sqlParam[10] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[10].Direction = ParameterDirection.Output;
            sqlParam[11] = new SqlParameter("@bSample", bSample);
            sqlParam[12] = new SqlParameter("@bDomain", bDomain);
            sqlParam[13] = new SqlParameter("@bPromisedDeliveryDate", bPromisedDeliveryDate);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[10].Value);
        }
        catch
        {
            return false;
        }
    }

    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_clearTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean ImportExcelFile()
    {
        DataTable ds = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

        #region 上传文档例行处理
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

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
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
                try
                {
                    ds = this.GetExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('导入文件必须是Excel格式!');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (ds.Rows.Count > 0)
                {
                    /*
                     *  导入的Excel文件必须满足：
                     *      1、至少应该有五列
                     *      2、从第五列开始即视为工序
                     *      3、工序名称必须在wo2_mop中存在
                    */
                    if (ds.Columns.Count != 24)
                    {
                        ds.Reset();
                        ltlAlert.Text = "alert('该文件必须有24列！');";
                        return false;
                    }

                    #region Excel的列名必须保持一致
                    for (int col = 0; col < ds.Columns.Count;col ++ )
                    {
                        if (col == 0 && ds.Columns[col].ColumnName.Trim() != "日期")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 日期！');";
                            return false;
                        }

                        if (col == 1 && ds.Columns[col].ColumnName.Trim() != "客户代码")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 客户代码！');";
                            return false;
                        }

                        if (col == 2 && ds.Columns[col].ColumnName.Trim() != "港口")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 港口！');";
                            return false;
                        }

                        if (col == 3 && ds.Columns[col].ColumnName.Trim() != "TCP订单号")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 TCP订单号！');";
                            return false;
                        }

                        if (col == 4 && ds.Columns[col].ColumnName.Trim() != "客户订单号")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 客户订单号！');";
                            return false;
                        }

                        if (col == 5 && ds.Columns[col].ColumnName.Trim() != "SW1")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 SW1！');";
                            return false;
                        }

                        if (col == 6 && ds.Columns[col].ColumnName.Trim() != "SW2")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 SW2！');";
                            return false;
                        }

                        if (col == 7 && ds.Columns[col].ColumnName.Trim() != "截止日期")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 截止日期！');";
                            return false;
                        }

                        if (col == 8 && ds.Columns[col].ColumnName.Trim() != "序号")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 序号！');";
                            return false;
                        }

                        if (col == 9 && ds.Columns[col].ColumnName.Trim() != "SZX销售订单")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 SZX销售订单！');";
                            return false;
                        }

                        if (col == 10 && ds.Columns[col].ColumnName.Trim() != "ATL销售订单")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 ATL销售订单！');";
                            return false;
                        }

                        if (col == 11 && ds.Columns[col].ColumnName.Trim() != "QAD号编码")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 QAD号编码！');";
                            return false;
                        }

                        if (col == 12 && ds.Columns[col].ColumnName.Trim() != "产品型号")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 产品型号！');";
                            return false;
                        }

                        if (col == 13 && ds.Columns[col].ColumnName.Trim() != "订购数量(套)")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 订购数量(套)！');";
                            return false;
                        }

                        if (col == 14 && ds.Columns[col].ColumnName.Trim() != "数量(只)")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 数量(只)！');";
                            return false;
                        }

                        if (col == 15 && ds.Columns[col].ColumnName.Trim() != "销售单地点")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 销售单地点！');";
                            return false;
                        }

                        if (col == 16 && ds.Columns[col].ColumnName.Trim() != "备注")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 备注！');";
                            return false;
                        }

                        if (col == 17 && ds.Columns[col].ColumnName.Trim() != "采购价格")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 采购价格！');";
                            return false;
                        }

                        if (col == 18 && ds.Columns[col].ColumnName.Trim() != "留样")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 留样！');";
                            return false;
                        }

                        if (col == 19 && ds.Columns[col].ColumnName.Trim() != "计划日期")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 计划日期！');";
                            return false;
                        }

                        if (col == 20 && ds.Columns[col].ColumnName.Trim() != "样品")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 样品！');";
                            return false;
                        }
                        if (col == 21 && ds.Columns[col].ColumnName.Trim() != "BOM延期备注")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 BOM延期备注！');";
                            return false;
                        }
                        if (col == 22 && ds.Columns[col].ColumnName.Trim() != "制地")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 制地！');";
                            return false;
                        }
                        if (col == 23 && ds.Columns[col].ColumnName.Trim() != "承诺发货日期")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 承诺发货日期！');";
                            return false;
                        }
                    }																
                    #endregion

                    //转换成模板格式
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "date";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cust";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "port";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "tcp_po";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cust_po";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sw1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sw2";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "due_date";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "line";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "szx_so";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "atl_so";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qad";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "item";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qty_ord";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qty_ord1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "site";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "rmks";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "price";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sample";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createdBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "planDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "IsSample";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "remarks";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "domain";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "promisedDeliveryDate";
                    table.Columns.Add(column);
                    #endregion
                  
                    int _uID = Convert.ToInt32(Session["uID"].ToString());

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in ds.Rows)
                        {
                            row = table.NewRow();

                            #region 赋值、长度判定
                            //date的长度允许最长10个字符，否则截取
                            if (r[0].ToString().Length > 10)
                            {
                                try
                                {
                                    row["date"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[0]));
                                }
                                catch
                                {
                                    row["date"] = r[0].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["date"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[0]));
                                }
                                catch
                                {
                                    row["date"] = r[0].ToString();
                                }
                            }

                            //cust的长度允许最长50个字符，否则截取
                            if (r[1].ToString().Length > 50)
                            {
                                row["cust"] = r[1].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["cust"] = r[1].ToString();
                            }

                            //port的长度允许最长50个字符，否则截取
                            if (r[2].ToString().Length > 50)
                            {
                                row["port"] = r[2].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["port"] = r[2].ToString();
                            }

                            //tcp_po的长度允许最长20个字符，否则截取
                            if (r[3].ToString().Length > 20)
                            {
                                row["tcp_po"] = r[3].ToString().Substring(0, 20);
                            }
                            else
                            {
                                row["tcp_po"] = r[3].ToString();
                            }

                            //tcp_po的长度允许最长20个字符，否则截取
                            if (r[4].ToString().Length > 20)
                            {
                                row["cust_po"] = r[4].ToString().Substring(0, 20);
                            }
                            else
                            {
                                row["cust_po"] = r[4].ToString();
                            }

                            //sw1的长度允许最长10个字符，否则截取
                            if (r[5].ToString().Length > 10)
                            {
                                try
                                {
                                    row["sw1"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[5]));
                                }
                                catch
                                {
                                    row["sw1"] = r[5].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["sw1"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[5]));
                                }
                                catch
                                {
                                    row["sw1"] = r[5].ToString();
                                }
                            }

                            //sw2的长度允许最长10个字符，否则截取
                            if (r[6].ToString().Length > 10)
                            {
                                try
                                {
                                    row["sw2"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[6]));
                                }
                                catch
                                {
                                    row["sw2"] = r[6].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["sw2"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[6]));
                                }
                                catch
                                {
                                    row["sw2"] = r[6].ToString();
                                }
                            }

                            //due_date的长度允许最长10个字符，否则截取
                            if (r[7].ToString().Length > 10)
                            {
                                try
                                {
                                    row["due_date"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[7]));
                                }
                                catch
                                {
                                    row["due_date"] = r[7].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["due_date"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[7]));
                                }
                                catch
                                {
                                    row["due_date"] = r[7].ToString();
                                }
                            }

                            //line的长度允许最长4个字符，否则截取
                            if (r[8].ToString().Length > 4)
                            {
                                row["line"] = r[8].ToString().Substring(0, 4);
                            }
                            else
                            {
                                row["line"] = r[8].ToString();
                            }

                            //szx_so的长度允许最长8个字符，否则截取
                            if (r[9].ToString().Length > 8)
                            {
                                row["szx_so"] = r[9].ToString().Substring(0, 8);
                            }
                            else
                            {
                                row["szx_so"] = r[9].ToString();
                            }

                            //atl_so的长度允许最长8个字符，否则截取
                            if (r[10].ToString().Length > 8)
                            {
                                row["atl_so"] = r[10].ToString().Substring(0, 8);
                            }
                            else
                            {
                                row["atl_so"] = r[10].ToString();
                            }

                            //qad的长度允许最长14个字符，否则截取
                            if (r[11].ToString().Length > 14)
                            {
                                row["qad"] = r[11].ToString().Substring(0, 14);
                            }
                            else
                            {
                                row["qad"] = r[11].ToString();
                            }

                            //item的长度允许最长50个字符，否则截取
                            if (r[12].ToString().Length > 50)
                            {
                                row["item"] = r[12].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["item"] = r[12].ToString();
                            }

                            //qty_ord的长度允许最长8个字符，否则截取
                            if (r[13].ToString().Length > 8)
                            {
                                row["qty_ord"] = r[13].ToString().Substring(0, 8);
                            }
                            else
                            {
                                row["qty_ord"] = r[13].ToString();
                            }

                            //qty_ord1的长度允许最长8个字符，否则截取
                            if (r[14].ToString().Length > 8)
                            {
                                row["qty_ord1"] = r[14].ToString().Substring(0, 8);
                            }
                            else
                            {
                                row["qty_ord1"] = r[14].ToString();
                            }

                            //site的长度允许最长4个字符，否则截取
                            if (r[15].ToString().Length > 4)
                            {
                                row["site"] = r[15].ToString().Substring(0, 4);
                            }
                            else
                            {
                                row["site"] = r[15].ToString();
                            }

                            //rmks的长度允许最长10个字符，否则截取
                            if (r[16].ToString().Length > 10)
                            {
                                row["rmks"] = r[16].ToString().Substring(0, 10);
                            }
                            else
                            {
                                row["rmks"] = r[16].ToString();
                            }

                            //price的长度允许最长10个字符，否则截取
                            if (r[17].ToString().Length > 10)
                            {
                                row["price"] = r[17].ToString().Substring(0, 10);
                            }
                            else
                            {
                                row["price"] = r[17].ToString();
                            }

                            //sample的长度允许最长50个字符，否则截取
                            if (r[18].ToString().Length > 50)
                            {
                                row["sample"] = r[18].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["sample"] = r[18].ToString();
                            }

                            //planDate的长度允许最长10个字符，否则截取
                            if (r[19].ToString().Length > 10)
                            {
                                try
                                {
                                    row["planDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[19]));
                                }
                                catch
                                {
                                    row["planDate"] = r[19].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["planDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[19]));
                                }
                                catch
                                {
                                    row["planDate"] = r[19].ToString();
                                }
                            }

                            if (r[20].ToString().Length > 10)
                            {
                                try
                                {
                                    row["IsSample"] = Convert.ToBoolean(r[20]).ToString();
                                }
                                catch
                                {
                                    row["IsSample"] = "error";
                                }
                            
                            }
                            else
                            {
                                try
                                {
                                    row["IsSample"] = Convert.ToBoolean(r[20]).ToString();
                                }
                                catch
                                {
                                    row["IsSample"] = "error";
                                }
                            
                            }
                            try
                            {
                                row["remarks"] = r[21].ToString();
                            }
                            catch
                            {
                                row["remarks"] = "error";
                            }

                            row["domain"] = r[22].ToString();

                            try
                            {
                                row["promisedDeliveryDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[23]));
                            }
                            catch
                            {
                                row["promisedDeliveryDate"] = r[23].ToString();
                            }
                           
                            #endregion

                            row["createdBy"] = _uID;
                            row["errMsg"] = string.Empty;

                            table.Rows.Add(row);
                        }

                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
                            {
                                bulkCopy.DestinationTableName = "dbo.ediTemp";
                                bulkCopy.ColumnMappings.Add("date", "et_date");
                                bulkCopy.ColumnMappings.Add("cust", "et_cust");
                                bulkCopy.ColumnMappings.Add("port", "et_port");
                                bulkCopy.ColumnMappings.Add("tcp_po", "et_tcp_po");
                                bulkCopy.ColumnMappings.Add("cust_po", "et_cust_po");
                                bulkCopy.ColumnMappings.Add("sw1", "et_sw1");
                                bulkCopy.ColumnMappings.Add("sw2", "et_sw2");
                                bulkCopy.ColumnMappings.Add("due_date", "et_due_date");
                                bulkCopy.ColumnMappings.Add("line", "et_line");
                                bulkCopy.ColumnMappings.Add("szx_so", "et_szx_so");
                                bulkCopy.ColumnMappings.Add("atl_so", "et_atl_so");
                                bulkCopy.ColumnMappings.Add("qad", "et_qad");
                                bulkCopy.ColumnMappings.Add("item", "et_item");
                                bulkCopy.ColumnMappings.Add("qty_ord", "et_qty_ord");
                                bulkCopy.ColumnMappings.Add("qty_ord1", "et_qty_ord1");
                                bulkCopy.ColumnMappings.Add("site", "et_site");
                                bulkCopy.ColumnMappings.Add("rmks", "et_rmks");
                                bulkCopy.ColumnMappings.Add("price", "et_price");
                                bulkCopy.ColumnMappings.Add("sample", "et_sample");
                                bulkCopy.ColumnMappings.Add("planDate", "et_planDate");
                                bulkCopy.ColumnMappings.Add("createdBy", "et_createdBy");
                                bulkCopy.ColumnMappings.Add("errMsg", "et_errMsg");
                                bulkCopy.ColumnMappings.Add("IsSample", "et_IsSample");
                                bulkCopy.ColumnMappings.Add("remarks", "et_remark");
                                bulkCopy.ColumnMappings.Add("domain", "et_domain");
                                bulkCopy.ColumnMappings.Add("promisedDeliveryDate", "et_promisedDeliveryDate");
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('导入时出错，请联系系统管理员！');";

                                    return false;
                                }
                                finally
                                {
                                    table.Dispose();
                                }
                            }
                        }
                    }
                }                            

                ds.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        #region 条件设置
        bool bPlanDate = false;
        bool bDueDate = false;
        bool bUnitPrice = false;
        bool bSite = false;
        bool bCust = false;
        bool bPart = false;
        bool bCustPart = false;
        bool bSoNbr = false;
        bool bQty = false;
        bool bIsSample = false;
        bool bRemark = false;
        bool bPromisedDeliveryDate = false;
        bool bDomain = false;
        foreach (ListItem item in chkList.Items)
        {
            //计划日期
            if (Convert.ToInt32(item.Value) == 10 && item.Selected)
            {
                bPlanDate = true;
            }
            //截止日期
            if (Convert.ToInt32(item.Value) == 20 && item.Selected)
            {
                bDueDate = true;
            }
            //单价
            if (Convert.ToInt32(item.Value) == 30 && item.Selected)
            {
                bUnitPrice = true;
            }
            //制地
            if (Convert.ToInt32(item.Value) == 40 && item.Selected)
            {
                bSite = true;
            }
            //客户
            if (Convert.ToInt32(item.Value) == 50 && item.Selected)
            {
                bCust = true;
            }
            //QAD号
            if (Convert.ToInt32(item.Value) == 60 && item.Selected)
            {
                bPart = true;
            }
            //客户零件
            if (Convert.ToInt32(item.Value) == 70 && item.Selected)
            {
                bCustPart = true;
            }
            //销售订单
            if (Convert.ToInt32(item.Value) == 80 && item.Selected)
            {
                bSoNbr = true;
            }
            //数量
            if (Convert.ToInt32(item.Value) == 90 && item.Selected)
            {
                bQty = true;
            }
            //样品
            if (Convert.ToInt32(item.Value) == 100 && item.Selected)
            {
                bIsSample = true;
            }
            //BOM延期原因
            if (Convert.ToInt32(item.Value) == 110 && item.Selected)
            {
                bRemark = true;
            }
            //承诺发货日期
            if (Convert.ToInt32(item.Value) == 120 && item.Selected)
            {
                bPromisedDeliveryDate = true;
            }

            //域
            if (Convert.ToInt32(item.Value) == 130 && item.Selected)
            {
                bDomain = true;
            }
        }

        if (!bPlanDate && !bDueDate && !bUnitPrice && !bSite && !bCust && !bPart && !bCustPart && !bSoNbr && !bQty && !bIsSample && !bRemark && !bDomain && !bPromisedDeliveryDate)
        {
            ltlAlert.Text = "alert('请至少选择一项功能!');";
            return;
        }
        #endregion
        if (ImportExcelFile())
        {
            if (!CheckValidity(Session["uID"].ToString(), bPlanDate, bDueDate, bUnitPrice, bSite, bCust, bPart, bCustPart, bSoNbr, bQty, bIsSample, bRemark, bDomain, bPromisedDeliveryDate))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), bPlanDate, bDueDate, bUnitPrice, bSite, bCust, bPart, bCustPart, bSoNbr, bQty, bIsSample, bRemark, bDomain, bPromisedDeliveryDate))
                {
                    ltlAlert.Text = "alert('导入成功!');";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!');";
                }
            }
            else
            {
                ltlAlert.Text = "window.open('EDIPOImportError.aspx?rt=" + DateTime.Now.ToString() + "', '_blank');";
            }
        }
    }
}
