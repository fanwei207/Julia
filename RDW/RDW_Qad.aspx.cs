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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.IO;
using RD_WorkFlow;

public partial class RDW_RDW_Qad : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BtnAdd.Enabled = this.Security["170016"].isValid;
            BtnImport.Enabled = this.Security["170016"].isValid;
            BindData();
        }
    }

    protected void BindData()
    {
        string mid = "";
        if (Request.QueryString["mid"] != null)
        {
            mid = Request.QueryString["mid"].ToString();
        }
        DataTable dt = rdw.SelectRdwQad(mid, 0);
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gvRWDQad.DataSource = dt;
            gvRWDQad.DataBind();
            int columnCount = gvRWDQad.Rows[0].Cells.Count;
            gvRWDQad.Rows[0].Cells.Clear();
            gvRWDQad.Rows[0].Cells.Add(new TableCell());
            gvRWDQad.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvRWDQad.Rows[0].Cells[0].Text = "No data";
            gvRWDQad.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gvRWDQad.DataSource = dt;
            gvRWDQad.DataBind();
        }

    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["mid"] == null)
        {
            ltlAlert.Text = "alert('No project!'); ";
            BindData();
            return;
        }
        if (txtQad.Text.Length == 0)
        {
            ltlAlert.Text = "alert('item can not be empty!'); ";
            BindData();
            return;
        }
        if (!rdw.IsExistQad(txtQad.Text.Trim()))
        {
            ltlAlert.Text = "alert('item does not exists!'); ";
            BindData();
            return;
        }
        if (rdw.IsExistRdwQad(Request.QueryString["mid"], txtQad.Text.Trim()))
        {
            ltlAlert.Text = "alert('this item does already exists!'); ";
            BindData();
            return;
        }
        if (!rdw.InsertRdwQad(Request.QueryString["mid"], txtQad.Text.Trim(), Convert.ToInt32(Session["uID"])))
        {
            ltlAlert.Text = "alert('add item failed!'); ";
            BindData();
            return;
        }
        BindData();




    }
    protected void gvRWDQad_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strID = gvRWDQad.DataKeys[e.RowIndex]["id"].ToString();
        if (rdw.DeleteRdwQad(strID))
        {
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('delete data failed!'); ";
            BindData();
            return;
        }
    }
    //protected static DataTable SelectRdwQad(string id)
    //{
    //    try
    //    {
    //        string strName = "sp_rdw_selectRdwQadList";
    //        SqlParameter[] sqlParam = new SqlParameter[1];
    //        sqlParam[0] = new SqlParameter("@id", id);
    //        return SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, sqlParam).Tables[0];
    //    }
    //    catch
    //    {
    //        return null;
    //    }

    //}

    //protected static bool DeleteRdwQad(string id)
    //{
    //    try
    //    {
    //        string strName = "sp_rdw_deleteRdwQadList";
    //        SqlParameter[] param = new SqlParameter[2];
    //        param[0] = new SqlParameter("@id", id);
    //        param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
    //        param[1].Direction = ParameterDirection.Output;
    //        SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
    //        return Convert.ToBoolean(param[1].Value);

    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}

    //protected static bool InsertRdwQad(string mid, string qad, int createBy)
    //{
    //    try
    //    {
    //        string strName = "sp_rdw_insertRdwQadList";
    //        SqlParameter[] param = new SqlParameter[4];
    //        param[0] = new SqlParameter("@mid", mid);
    //        param[1] = new SqlParameter("@qad", qad);
    //        param[2] = new SqlParameter("@createBy", createBy);
    //        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
    //        param[3].Direction = ParameterDirection.Output;
    //        SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
    //        return Convert.ToBoolean(param[3].Value);

    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}

    //protected static bool IsExistRdwQad(string mid, string qad)
    //{
    //    try
    //    {
    //        string strName = "sp_rdw_checkRdwQadList";
    //        SqlParameter[] param = new SqlParameter[3];
    //        param[0] = new SqlParameter("@mid", mid);
    //        param[1] = new SqlParameter("@qad", qad);
    //        param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
    //        param[2].Direction = ParameterDirection.Output;
    //        SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
    //        return Convert.ToBoolean(param[2].Value);

    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }


    //}

    //protected static bool IsExistQad(string qad)
    //{
    //    try
    //    {
    //        string strName = "sp_rdw_checkQad";
    //        SqlParameter[] param = new SqlParameter[2];
    //        param[0] = new SqlParameter("@qad", qad);
    //        param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
    //        param[1].Direction = ParameterDirection.Output;
    //        SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
    //        return Convert.ToBoolean(param[1].Value);

    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }


    //}

    protected void gvRWDQad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRWDQad.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gvRWDQad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = 0;
        string part = string.Empty;
        if (e.CommandName.ToString() == "gobom")
        {
            part = e.CommandArgument.ToString();
            ltlAlert.Text = "var w=window.open('/RDW/RDW_BomViewDoc.aspx?part=" + part + "&mid=" + Request.QueryString["mid"] + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";

        }
    }
    protected void gvRWDQad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[8].Enabled = this.Security["170016"].isValid;

            if (!this.Security["170021"].isValid)
            {
                if (!this.Security["170022"].isValid)
                {
                    e.Row.Cells[7].Enabled = false;
                    e.Row.Cells[7].Font.Bold = true;
                }
            }

        }
    }
    protected void BtnImport_Click(object sender, EventArgs e)
    {

        ImportExcelFile();
    }

    private bool CheckQad(string qad, int index, string aa, out string message)
    {
        message = "";
        if (index != 0)
        {
            message = "sheet " + aa + " row " + index + ":";
        }
        if (qad.Length == 0)
        {
            message += "item can not be empty!\\n";
            return false;
        }
        if (!rdw.IsExistQad(qad))
        {
            message += "item does not exists!\\n";
            return false;
        }
        if (rdw.IsExistRdwQad(Request.QueryString["mid"], qad))
        {
            if (index == 0)
            {
                message += "this item does already exists!\\n";
            }
            else
            {
                message = "";
            }
            return false; ;
        }
        return true;
    }

    public void ImportExcelFile()
    {
        //String strSQL = "";
        DataSet ds = new DataSet();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        //Boolean boolError = false;

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
                return;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        //Modified By Shanzm 2012-12-27：唯一字符串可以设定为“年月日时分秒毫秒”
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                string[] arrTable = null;
                try
                {
                    arrTable = rdw.GetExcelSheetName(strFileName);
                }
                catch
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('导入文件必须是Excel格式(.xls)或者模板及内容正确!');";
                    return;
                }
                string message = "";
                foreach (string aa in arrTable)
                {
                    if (aa != null)
                    {

                        try
                        {
                            ds = rdw.GetExcelContents(strFileName, aa);
                        }
                        catch
                        {
                            if (File.Exists(strFileName))
                            {
                                File.Delete(strFileName);
                            }

                            ltlAlert.Text = "alert('导入文件必须是Excel格式(.xls)或者模板及内容正确!" + aa + "');";
                            return;
                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Columns[0].ColumnName.ToLower() != "qad")
                            {
                                ds.Reset();
                                ltlAlert.Text = "alert('导入文件的模版不正确!');";
                                return;
                            }


                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string qad = ds.Tables[0].Rows[i][0].ToString().Trim();
                                string detailMessage = "";
                                if (CheckQad(qad, i + 2, aa, out detailMessage))
                                {
                                    //DataRow newRow = dt.NewRow();
                                    //newRow["qad"] = qad;
                                    //newRow["selected"] = 1;
                                    //dt.Rows.Add(newRow);
                                    if (!rdw.InsertRdwQad(Request.QueryString["mid"], qad, Convert.ToInt32(Session["uID"])))
                                    {
                                        detailMessage = "sheet " + aa + " row " + (i+2) + ":add item failed!\\n";
                                        message += detailMessage;                                        
                                    }
                                }
                                else
                                {
                                    message += detailMessage;
                                }
                            }
                        }

                    }
                    if (message != "")
                    {
                        ltlAlert.Text = "alert('" + message + "')";
                    }
                    BindData();
                }
            }
        }
    }
}
