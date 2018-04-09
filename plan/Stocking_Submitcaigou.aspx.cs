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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using Stockingbase;

public partial class plan_Stocking_Submitcaigou : BasePage
{
    adamClass adam = new adamClass();
    Stocking sk = new Stocking();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvSID.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvSID.Rows[i].FindControl("chk_Select");
            if (cb.Enabled == true)
            {
                if (chkAll.Checked)
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void BindData()
    {
        //定义参数
        string strnbr = txtNbr.Text.Trim().Replace("*", "%");
        string strvent = txtvent.Text.Trim();
        string strpart = txtpart.Text.Trim().Replace("*", "%");
        string strQAD = txtQAD.Text.Trim();



        gvSID.DataSource = sk.SelectStockingSubmitcaigou(strnbr, strvent, strpart, strQAD, ddlstatus.SelectedValue);
        gvSID.DataBind();
    }
   
    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";

        //判断是否有选择
        for (int i = 0; i < gvSID.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvSID.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gvSID.DataKeys[i].Value.ToString() + ",";
            }
        }
        return strSelect;
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //定义参数
        //string time = DateTime.Now.ToFileTime().ToString();
        string strRet = chkSelect();
        string struID = Convert.ToString(Session["uID"]);
        string[] strsid;

        if (strRet.Length != 0)
        {
            strRet = strRet.Substring(0, strRet.Length - 1);




            strsid = strRet.Split(',');
            foreach (var item in strsid)
            {
                sk.submitstocking(item, Session["uID"].ToString());
            }

            //Response.Redirect("SID_UpPicture.aspx?from=new&shipid=" + time + "&sid=" + strRet + "&rt=" + DateTime.Now.ToFileTime().ToString());

        }
        BindData();
    }
   
    protected void gvSID_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSID.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void gvSID_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSID.EditIndex = -1;
        BindData();
    }
    protected void gvSID_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String id = gvSID.DataKeys[e.RowIndex].Values["sk_nbr"].ToString();
        TextBox txtponbr = (TextBox)gvSID.Rows[e.RowIndex].FindControl("txtponbr");
        TextBox txtpolinr = (TextBox)gvSID.Rows[e.RowIndex].FindControl("txtpolinr");

        if (txtponbr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('订单号不能为空!');";
            return;
        }
        else if (txtpolinr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('订单行不能为空!');";
            return;
        }
        sk.updatestocking(id, txtponbr.Text, txtpolinr.Text);
        gvSID.EditIndex = -1;
        BindData();
    }
   
    protected void linkDownload_Click(object sender, EventArgs e)
    {
        string strnbr = txtNbr.Text.Trim().Replace("*", "%");
        string strvent = txtvent.Text.Trim();
        string strpart = txtpart.Text.Trim().Replace("*", "%");
        string strQAD = txtQAD.Text.Trim();

        DataTable dt = sk.SelectStockingSubmitcaigou(strnbr, strvent, strpart, strQAD, ddlstatus.SelectedValue);
        string title = "<b>备货单号</b>~^<b>供应商</b>~^<b>供应商名称</b>~^<b>客户物料</b>~^<b>QAD</b>~^100^<b>价格</b>~^100^<b>数量</b>~^100^<b>单位</b>~^100^<b>备注</b>~^<b>订单号</b>~^<b>订单行</b>~^";
        this.ExportExcel(title, dt, true);


    }
    private bool checkTemp()
    {
        //SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_edi_checkOrderTrackingDelayTemp");

        string strSql2 = "select * from Stocking_SubmitTemp where isnull( errMsg ,'') <>''";
        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "100^<b>备货单号</b>~^100^<b>订单号</b>~^100^<b>订单行</b>~^100^<b>错误信息</b>~^";

                string sql = " select sk_nbr,po_line,po_nbr,po_line,errMsg from Stocking_SubmitTemp  ";

                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
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
                if (sk.InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
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
                    DataTable table = new DataTable("Stocking_SubmitTemp");

                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_nbr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "po_Nbr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "po_line";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);

                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);
                    string strerror = "";

                    if (sk.ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            line = line + 1;
                            row = table.NewRow();

                            #region 赋值、长度判定
                            if (r["备货单号"].ToString().Trim().Length == 0)
                            {
                                strerror += "备货单号不能为空.";
                            }
                            string lines = r["订单行"].ToString().Trim();
                            try
                            {
                                if (lines != string.Empty)
                                {
                                    int intline = Convert.ToInt32(lines);
                                }
                            }
                            catch (Exception)
                            {

                                strerror += "订单号必须为数字.";
                            }

                            row["sk_nbr"] = r["备货单号"].ToString().Trim();
                            row["po_Nbr"] = r["订单号"].ToString().Trim();
                            row["po_line"] = r["订单行"].ToString().Trim();


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
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adam.dsn0()))
                            {
                                bulkCopy.DestinationTableName = "dbo.Stocking_SubmitTemp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("sk_nbr", "sk_nbr");
                                bulkCopy.ColumnMappings.Add("po_Nbr", "po_Nbr");
                                bulkCopy.ColumnMappings.Add("po_line", "po_line");

                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");

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
   
    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }
}