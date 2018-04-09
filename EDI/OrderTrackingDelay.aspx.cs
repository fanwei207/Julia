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

public partial class plan_OrderTrackingDelay : BasePage
{
    private edi.OrderTracking helper = new edi.OrderTracking();
    private string connStr = ConfigurationManager.AppSettings["SqlConn.Conn_edi"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRegionData();
            //BindData();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        //string status = ddlStatus.SelectedValue;
        DataTable dt = GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, type);
        foreach (DataRow row in dt.Rows)
        {
            if (!this.Security["44000611"].isValid)
            {
                row["det_price"] = DBNull.Value;
            }
        }

        gvlist.DataSource = dt;
        gvlist.DataBind();
        txtTotal.Text = dt.Rows.Count.ToString();
    }
    public DataTable GetOrderTracking(string po1, string po2, string dateFrom, string dateTo, string region, string customer, string type)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@po1", po1);
        param[1] = new SqlParameter("@po2", po2);
        param[2] = new SqlParameter("@ordDate1", dateFrom);
        param[3] = new SqlParameter("@ordDate2", dateTo);
        param[4] = new SqlParameter("@region", region);
        param[5] = new SqlParameter("@customer", customer);
       
        param[6] = new SqlParameter("@type", type);
        return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectOrderTrackingDelay", param).Tables[0];
    }
    public DataTable GetOrderTrackingimport(string po1, string po2, string dateFrom, string dateTo, string region, string customer, string type)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@po1", po1);
        param[1] = new SqlParameter("@po2", po2);
        param[2] = new SqlParameter("@ordDate1", dateFrom);
        param[3] = new SqlParameter("@ordDate2", dateTo);
        param[4] = new SqlParameter("@region", region);
        param[5] = new SqlParameter("@customer", customer);

        param[6] = new SqlParameter("@type", type);
        return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectOrderTrackingDelayimport", param).Tables[0];
    }
    private void BindRegionData()
    {
        ddlRegion.DataSource = helper.GetRegions(Session["uID"].ToString());
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("--", ""));
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        
        DataTable dt = helper.GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, "0", type);
        foreach (DataRow row in dt.Rows)
        {
            if (!this.Security["44000611"].isValid)
            {
                row["det_price"] = DBNull.Value;
            }
        }

        string title = "<b>Order#</b>~^<b>Order Date</b>~^<b>Region</b>~^<b>Customer Code</b>~^200^<b>Customer</b>~^50^<b>Line</b>~^200^<b>Item</b>~^<b>Order Qty</b>~^200^<b>Order Question</b>~^<b>Load QAD Date</b>~^<b>Qad So#</b>~^110^<b>QAD Part</b>~^<b>Part Type</b>~^<b>Work Hours</b>~^<b>Order Qty</b>~^<b>Request Date</b>~^<b>Wo Qty</b>~^<b>Online Qty</b>~^<b>Ship Qty</b>~^<b>Inspection Date</b>~^<b>Ship Date</b>~^<b>PCD</b>~^<b>PCD创建人</b>~^<b>PCD创建日期</b>~^<b>价格</b>~^<b>分类</b>~^200^<b>PCD备注</b>~^<b>是否开票</b>~^<b>制地</b>~^<b>周期章</b>~^";
        this.ExportExcel(title, dt, true);

    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = e.Row.RowIndex;
            if (gvlist.DataKeys[rowIndex].Values["que"].ToString() == "0")
            {
                //e.Row.Cells[5].Controls[0].Visible = false;
            }
            string planDate = gvlist.DataKeys[rowIndex].Values["planDate"].ToString();
            if (planDate == "")
            {
               // (e.Row.Cells[16].FindControl("linkPCD") as LinkButton).Text = "Apply";
            }
            if (gvlist.DataKeys[rowIndex].Values["sod_ord_date"].ToString() == string.Empty)
            {
               // e.Row.Cells[5].Controls[0].Visible = true;
            }
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        helper.RefreshOrderTracking();
        BindData();
    }
    protected void btnExportWo_Click(object sender, EventArgs e)
    {
        
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void linkDownload_Click(object sender, EventArgs e)
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();

        DataTable dt = GetOrderTrackingimport(po1, po2, orderDate1, orderDate2, region, customer, type);
        string title = "<b>订单号</b>~^<b>出运单号</b>~^<b>客户</b>~^<b>订单行</b>~^<b>客户物料号</b>~^<b>order date</b>~^100^<b>订单需求日期</b>~^100^<b>订单出运日期</b>~^100^<b>Load QAD Date</b>~^100^<b>Request Date</b>~^<b>延期原因</b>~^<b>延期备注</b>~^";
        this.ExportExcel(title, dt, true);
    }
    protected void gvlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvlist.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void gvlist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvlist_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String po_nbr = gvlist.DataKeys[e.RowIndex].Values["poNbr"].ToString();
        String po_line = gvlist.DataKeys[e.RowIndex].Values["poLine"].ToString();
        String sid = gvlist.DataKeys[e.RowIndex].Values["SID_nbr"].ToString();
        TextBox txtdelay = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtdelay");
        TextBox txtremark = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtremark");
        if (txtdelay.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('延期原因不能为空!');";
            return;
        }
        string SQL = "select * from OrderTrackingDelayCom where delay_item = '"+txtdelay.Text.Trim()+"'";
        DataSet ds2 = SqlHelper.ExecuteDataset(connStr, CommandType.Text, SQL);
        if (!(ds2 != null && ds2.Tables[0].Rows.Count > 0))
        {
            ltlAlert.Text = "alert('延期原因不存在，请到配置表维护!');";
            return;
        }

        try
        {
             SqlParameter[] param = new SqlParameter[6];
             param[0] = new SqlParameter("@item", txtdelay.Text.Trim());
             param[1] = new SqlParameter("@remark", txtremark.Text.Trim());
             param[2] = new SqlParameter("@po_nbr", po_nbr);
             param[3] = new SqlParameter("@po_line", po_line);
             param[4] = new SqlParameter("@sid", sid.Trim());
             param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
             param[5].Direction = ParameterDirection.Output;
             SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_insertOrderTrackingDelay", param);
             if (!Convert.ToBoolean(param[5].Value))
             {
                 ltlAlert.Text = "alert('请按刷新后再试');";
                 return;
             }
             else
             {  
                 ltlAlert.Text = "alert('修改成功!');";
             }
             gvlist.EditIndex = -1;
             BindData();
            
             return;
        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('修改失败!');";
            return;
        }
    }
    protected void gvlist_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
    }
    protected void gvlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvlist.EditIndex = -1;
        BindData();
    }
    protected void btnimport_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (checkTemp())
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('导入成功!');";
                    BindData();
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!');";
                }
            }

        }
    }
    public Boolean ImportExcelFile()
    {
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int line = 0;
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
                    dt = this.GetExcelContents(strFileName);
                    //ds = chk.getExcelContents(strFileName);
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

                if (dt.Rows.Count > 0)
                {
                    /*
                     *  导入的Excel文件必须满足：
                     *      1、至少应该有五列
                     *      2、从第五列开始即视为工序
                     *      3、工序名称必须在wo2_mop中存在
                    */



                    #region Excel的列名必须保持一致
                    
                    #endregion
                    //构建ImportError
                    DataColumn column;
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

                    //DataRow rowError;//错误表的行

                    //转换成模板格式
                    DataTable table = new DataTable("OrderTrackingDelayTemp");

                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "po_nbr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "po_line";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "remark";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createby";
                    table.Columns.Add(column);

                   
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Item";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "SID_nbr";
                    table.Columns.Add(column);
                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);
                    string strerror = "";

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            line = line + 1;
                            row = table.NewRow();

                            #region 赋值、长度判定
                            if (r["订单号"].ToString().Trim().Length == 0)
                            {
                                strerror += "订单号不能为空.";
                            }
                            //if (r["出运单号"].ToString().Trim().Length == 0)
                            //{
                            //    strerror += "出运单号不能为空.";
                            //}
                            if (r["订单行"].ToString().Trim().Length == 0)
                            {
                                strerror += "订单行不能为空.";
                            }
                            if (r["延期备注"].ToString().Trim().Length != 0)
                            {
                                if (r["延期原因"].ToString().Trim().Length == 0)
                                {
                                    strerror += "延期原因不能为空.";
                                }
                            }

                            row["po_nbr"] = r["订单号"].ToString().Trim();
                            row["SID_nbr"] = r["出运单号"].ToString().Trim();
                            row["po_line"] = r["订单行"].ToString().Trim();
                            row["Item"] = r["延期原因"].ToString().Trim();
                            row["remark"] = r["延期备注"].ToString().Trim();
                            row["CreateBy"] = Session["uID"].ToString();
                            //row["CreateName"] = Session["uName"].ToString();

                            #endregion

                            if (strerror != "")
                            {
                                row["errMsg"] = strerror;
                            }
                            else
                            {
                                row["errMsg"] = string.Empty;
                            }
                            table.Rows.Add(row);
                            strerror = "";
                        }


                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connStr))
                            {
                                bulkCopy.DestinationTableName = "dbo.OrderTrackingDelayTemp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("po_nbr", "po_nbr");
                                bulkCopy.ColumnMappings.Add("po_line", "po_line");
                                bulkCopy.ColumnMappings.Add("remark", "remark");
                                bulkCopy.ColumnMappings.Add("createby", "createby");
                                bulkCopy.ColumnMappings.Add("Item", "Item");
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");
                                bulkCopy.ColumnMappings.Add("SID_nbr", "SID_nbr");
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('导入临时表时出错，请联系系统管理员！');";
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

                dt.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }
    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1]; 
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_edi_deleteOrderTrackingDelayTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }
    private bool checkTemp()
    {
        SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_edi_checkOrderTrackingDelayTemp");

        string strSql2 = "select * from OrderTrackingDelayTemp where isnull( errMsg ,'') <>''";
        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(connStr, CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "100^<b>订单号</b>~^100^<b>订单行</b>~^100^<b>延期原因</b>~^100^<b>延期备注</b>~^100^<b>错误信息</b>~^";

                string sql = " select po_nbr,po_line,Item,remark,errMsg from OrderTrackingDelayTemp  ";

                DataTable dt = SqlHelper.ExecuteDataset(connStr, CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('导入失败!');";
                ExportExcel(title, dt, false);
                return false;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Submission information verification failed! \\n 提交信息验证失败！');Form1.usercode.focus();";
            return false;
        }
        return true;
    }
    private bool InsertBatchTemp(string uID, string uName)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_edi_insertOrderTrackingDelayImport", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        //string status = ddlStatus.SelectedValue;
        DataTable dt = GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, type);
        string title = "<b>Order#</b>~^<b>SID nbr</b>~^<b>Order Date</b>~^<b>Request Date</b>~^<b>Ship Date</b>~^100^<b>Customer Code</b>~^100^<b>Line</b>~^200^<b>Item</b>~^200^<b>延期原因</b>~^200^<b>延期备注</b>~^<b>Qad So#</b>~^<b>QAD Part</b>~^<b>Order Qty</b>~^<b>Wo Qty</b>~^<b>Online Qty</b>~^<b>Ship Qty</b>~^<b>分类</b>~^";
        this.ExportExcel(title, dt, true);
    }
    protected void btnFresh_Click(object sender, EventArgs e)
    {
        try
        {

            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_edi_OrderTrackingDelayFresh");
            ltlAlert.Text = "alert('刷新成功！');";
           
        }
        catch
        {
            ltlAlert.Text = "alert('刷新失败！');";
        }
    }
}