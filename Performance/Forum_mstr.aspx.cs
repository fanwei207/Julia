using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.IO;
using System.Threading;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Collections.Generic;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.OleDb;
using System.IO;


public partial class Supplier_Forum_mstr : BasePage
{
    public string _fpath
    {
        get
        {
            return ViewState["fpath"].ToString();
        }
        set
        {
            ViewState["fpath"] = value;
        }
    }
    public string _fname
    {
        get
        {
            return ViewState["fname"].ToString();
        }
        set
        {
            ViewState["fname"] = value;
        }
    }
    public string _parentID
    {
        get
        {
            return ViewState["parentID"].ToString();
        }
        set
        {
            ViewState["parentID"] = value;
        }
    }

    private static adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnTypeManage.Visible = this.Security["8962"].isValid;

            ddlType.DataSource = this.selectTypeList();
            ddlType.DataBind();
            string typeid = Request.QueryString["typeID"];
            if (!string.IsNullOrEmpty(typeid))
            {
                ddlType.SelectedValue = typeid;
            }

            _parentID = "0";
            _parentID = Request.QueryString["parentID"] == null ? "0" : Request.QueryString["parentID"];
          
            //if (Request.QueryString["key"] != null)
            //{
            //  txtkeywords.Text = Request.QueryString["key"].ToString().Trim();
           
            //   //txtkeywords.Text = Request.QueryString["key"].ToString().Trim();
            //    gvMessage.Visible = false;
            //    gvMessagereply.Visible = true;
            //    btn_back.Visible = true;
            //    btn_reply.Visible = true;
            //    _parentID = findparentid(Request.QueryString["key"].ToString().Trim());
               
            //}
            if (Request.QueryString["id"] != null)
            {
                gvMessage.Visible = false;
                gvMessagereply.Visible = true;
                btn_back.Visible = true;
                btn_reply.Visible = true;
                ddlType.Enabled = false;
                _parentID = Request.QueryString["id"];
            }
           
            try
            {
               // hidTabIndex.Value = Request.QueryString["index"];
            }
            catch (Exception)
            {

            }
            try
            {
                string reply = Request.QueryString["reply"];
                if (reply.Trim() == "1")
                {
                  
                    gvMessage.Visible = false;
                    gvMessagereply.Visible = true;
                    btn_back.Visible = true;
                    btn_reply.Visible = true;
                    _parentID = Request.QueryString["parentID"];
                }
            }
            catch (Exception)
            {

            }


            //string custCode = Request.QueryString["custCode"];
            //int flag = custCode.LastIndexOf(",");
            //custCode = custCode.Substring(flag + 1);
           
            BindMessage();
            BindMessagereply();
          
            //_parentID = "0";
        }
    }

    private DataTable selectTypeList()
    {
        string strSql = "sp_Forum_selectTypeList";
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
    }
    #region Project Tracking
    protected void BindMessage()
    {
        string keywords = txtkeywords.Text.Trim();
        keywords = keywords.Replace("*", "%");

        gvMessage.DataSource = SelectTaskGVMessage(keywords, ralStatus.SelectedValue, ddlType.SelectedItem == null ? "1" : ddlType.SelectedItem.Value, "");
        gvMessage.DataBind();
        gvMessage.Attributes.Add("style", "word-break:break-all;word-wrap:break_word");
    }
    public static DataSet SelectTaskGVMessage( string keywords, string status, string typeID ,string me)
    {
        try
        {
            string strSql = "sp_Forum_selectGVMessage";
            SqlParameter[] parms = new SqlParameter[3];
            
            parms[0] = new SqlParameter("@keywords", keywords);
            parms[1] = new SqlParameter("@status", status);
            parms[2] = new SqlParameter("@typeID", typeID);
            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, parms);
        }
        catch
        {
            return null;
        }
    }
    public string findparentid(string keyWords)
    {
        try
        {
            string parentid = string.Empty;
            string strSql = "sp_Forum_selectparentID";
            SqlParameter[] parms = new SqlParameter[2];

            parms[0] = new SqlParameter("@kerwords", keyWords);
           
            SqlDataReader read= SqlHelper.ExecuteReader(adm.dsn0(), CommandType.StoredProcedure, strSql, parms);
            while (read.Read())
            {
                parentid = read["fst_id"].ToString().Trim();
            }
            return parentid;
        }
        catch
        {
            return null;
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("oms_cust.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    //}
    //protected void btnUpLoad_Click(object sender, EventArgs e)
    //{

    //}
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvMessage.PageIndex = e.NewPageIndex;
        BindMessage();
    }

    protected void gvMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //hidTabIndex.Value = "4";

        if (e.CommandName == "DownloadFile")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gvMessage.DataKeys[index].Values["fst_id"].ToString());
            string filePath = gvMessage.DataKeys[index].Values["fst_filepath"].ToString();

            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";



            BindMessage();
        }
        if (e.CommandName == "reply")
        {
            txtkeywords.Text = "";
            //_mstr = ((LinkButton)e.CommandSource).Text;
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            _parentID = gvMessage.DataKeys[index].Values["fst_id"].ToString();
            string Uid = gvMessage.DataKeys[index].Values["fst_createBy"].ToString();
            string closed = gvMessage.DataKeys[index].Values["fst_IsClosed"].ToString().Trim();
            gvMessagereply.Visible = true;
            gvMessage.Visible = false;
            BindMessagereply();
            btn_back.Visible = true;
            btn_reply.Visible = true;
            ralStatus.Visible = false;
            ddlType.Enabled = false;
            btnTypeManage.Visible = false;
           
            if (Session["uID"].ToString() == Uid)
            {
                // btn_close.Visible = true;
            }
            if (closed == "True")
            {

                btn_reply.Visible = false;
            }
        }
        if (e.CommandName == "close")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            _parentID = gvMessage.DataKeys[index].Values["fst_id"].ToString();
            string Uid = gvMessage.DataKeys[index].Values["fst_createBy"].ToString();
            if (Session["uID"].ToString() == Uid)
            {
                closemessage();
            }

        }
    }
    public static DataSet SelectTaskGVMessage(string parentID, string keywords ,string typeID)
    {
        try
        {
            string strSql = "sp_Forum_selectGVMessage";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@parentID", parentID);
           
            param[1] = new SqlParameter("@keywords", keywords);
            param[2] = new SqlParameter("@typeID", typeID);
            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param);
        }
        catch
        {
            return null;
        }
    }
    protected void BindMessagereply()
    {
        string keywords = txtkeywords.Text.Trim();
        keywords = keywords.Replace("*", "%");
        gvMessagereply.DataSource = SelectTaskGVMessage(_parentID, keywords, ddlType.SelectedItem == null ? "1" : ddlType.SelectedItem.Value);
        //gvMessagereply.Columns[1].HeaderText = _mstr;

        gvMessagereply.DataBind();


        //((Label)gvMessagereply.Rows[0].FindControl("Label1")).Text=((Label)gvMessagereply.Rows[0].FindControl("Label1")).Text.Replace("|", "<br>");
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        gvMessage.Visible = true;
        gvMessagereply.Visible = false;
        btn_reply.Visible = false;
        btn_back.Visible = false;
        ddlType.Enabled = true;
        btnTypeManage.Visible = this.Security["8962"].isValid;

        ralStatus.Visible = true;
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        //Response.Redirect("oms_reply.aspx?parentID=" + _parentID + "&custCode=" + lbCustCode.Text + "&custName=" + lbCustName.Text + "&custCode=" + txtCustomer.Text + "&rt=" + DateTime.Now.ToFileTime().ToString());
        ltlAlert.Text = "window.showModalDialog('Forum_replay.aspx?parentID= 0 &type=" + "new" + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'Forum_mstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString() + "'";

    }
    protected void gvMessagereply_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadFile")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gvMessagereply.DataKeys[index].Values["fst_id"].ToString());
            string filePath = gvMessagereply.DataKeys[index].Values["fst_filepath"].ToString();

            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";



            BindMessagereply();
        }
    }
    protected void btn_reply_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.showModalDialog('Forum_replay.aspx?parentID=" + _parentID + "&type= " + "reply" + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'Forum_mstr.aspx?parentID=" + _parentID + "&reply=" + 1 + " &rt=" + DateTime.Now.ToFileTime().ToString() + "'";

    }
    protected void gvMessagereply_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvMessagereply.PageIndex = e.NewPageIndex;
        BindMessagereply();
    }
    public static bool closeTaskMessage(string id)
    {
        try
        {
            string strSql = "sp_Forum_closeGVMessage";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param);
            return true;

        }
        catch
        {
            return false;
        }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        if (closeTaskMessage(_parentID) == true)
        {
            ltlAlert.Text = "alert('主题关闭成功！')";
            gvMessage.Visible = true;
            BindMessage();
            gvMessagereply.Visible = false;

            btn_reply.Visible = false;
            //btnBack.Visible = false;
        }
        else
        {
            ltlAlert.Text = "alert('主题关闭失败！')";
        }
        //hidTabIndex.Value = "4";
        //string Uid = gvMessagereply.DataKeys[0].Values["fst_createBy"].ToString();
        //if (Session["uID"].ToString() == Uid)
        //{
        //    btn_close.Visible = true;
        //}
    }
    
    protected void closemessage()
    {
        if (closeTaskMessage(_parentID) == true)
        {
            ltlAlert.Text = "alert('主题关闭成功！')";
            gvMessage.Visible = true;
            BindMessage();
            gvMessagereply.Visible = false;

            btn_reply.Visible = false;
            //btnBack.Visible = false;
        }
        else
        {
            ltlAlert.Text = "alert('主题关闭失败！')";
        }
      //  hidTabIndex.Value = "4";
    }
    protected void btn_messageselect_Click(object sender, EventArgs e)
    {
        BindMessage();
        BindMessagereply();
        txtkeywords.Text = string.Empty;
       // hidTabIndex.Value = "4";
    }
    protected void gvMessage_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            string closed = gvMessage.DataKeys[index].Values["fst_IsClosed"].ToString().Trim();

            string Uid = gvMessage.DataKeys[index].Values["fst_createBy"].ToString().Trim();
            //e.Row.Cells[3].Enabled = false;
            //LinkButton close = ((LinkButton)e.Row.FindControl("close"));

            //string istop = gvMessage.DataKeys[index].Values["fst_istop"].ToString().Trim();
            //if (istop == "True")
            //{
            //    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
            //}

            if (closed == "True")
            {
                //e.Row.Cells[3].Enabled = false;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Enabled = false;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Font.Underline = false;
                // e.Row.Cells[3].BackColor = System.Drawing.Color.RoyalBlue;
                return;

            }
            else if (Session["uID"].ToString() != Uid)
            {
                e.Row.Cells[3].Enabled = false;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Enabled = false;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Text = string.Empty;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Font.Underline = false;
                return;
            }
            else
            {
                e.Row.Cells[3].Enabled = true;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).ForeColor = System.Drawing.Color.Blue;
                return;
            }
        }
    }
    #endregion
    protected void btnTypeManage_Click(object sender, EventArgs e)
    {
        Response.Redirect("Forum_TypeManage.aspx");
    }
}