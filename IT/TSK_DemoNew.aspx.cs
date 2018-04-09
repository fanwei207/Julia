using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class IT_TSK_DemoNew : System.Web.UI.Page
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
    public string _ID
    {
        get
        {
            return ViewState["ID"].ToString();
        }
        set
        {
            ViewState["ID"] = value;
        }
    }
    static string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _fpath = "false";
            if (Request.QueryString["title"] != null)
            {
                table2.Visible = true;
                table1.Visible = false;
                txtTitle2.Text = Request.QueryString["title"];
                Btn_Save.Visible = true;
                btnNew.Visible = false;
                _fpath = "true";
                _ID = Request.QueryString["detID"];
                getdet(_ID);
            }
        }

    }
    public void getdet(string detid)
    {
        try
        {
            string strName = "sp_demo_selectDemoDet";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@detid", detid);
          
            DataTable dt = SqlHelper.ExecuteDataset (strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            if (dt.Rows.Count != 0)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                Response.Redirect("TSK_DemoMstr.aspx?from=new&rt=" + DateTime.Now.ToFileTime().ToString());
            }
        }
        catch (Exception)
        {
            
            
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('标题不能为空！')";
            return;
        }

        int n = NEWtitle();
        if (n == 0)
        {
            ltlAlert.Text = "alert('新增成功！')";
            txtname.Text = string.Empty;
            
        }
        if (n == 2)
        {
            ltlAlert.Text = "alert('新增出错！')";
        }
        if (n == 1)
        {
            ltlAlert.Text = "alert('当前标题的目录已存在！')";
        }
    }

    public int NEWtitle()
    {
        try
        {
            string strName = "sp_demo_insertDemo";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@title", txtTitle.Text);
            param[1] = new SqlParameter("@name", txtname.Text);
          
            param[2] = new SqlParameter("@Uid", Session["uID"].ToString());
            param[3] = new SqlParameter("@UName", Session["uName"].ToString());
            param[4] = new SqlParameter("@ismenu", ckb_ismenu.Checked);
            param[5] = new SqlParameter("@reValue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery (strConn, CommandType.StoredProcedure, strName, param);
            return Convert.ToInt32(param[5].Value);
        }
        catch (Exception ex)
        {
            return 2;
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        if (_fpath == "true")
        {
            Response.Redirect("TSK_DemoMstr.aspx?from=new&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else
        {
            ltlAlert.Text = "window.close();";
        }
      
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        int n;
        try
        {
            string strName = "sp_demo_updateDemoMstr";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", _ID);
            param[1] = new SqlParameter("@title", txtTitle2.Text);

           
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
            n = 0;
        }
        catch (Exception ex)
        {
            n = 2;
        }
        if (n == 0)
        {
            ltlAlert.Text = "alert('修改成功！')";
           
            getdet(_ID);

        }
        if (n == 2)
        {
            ltlAlert.Text = "alert('修改出错！')";
        }
       
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string key = gv.DataKeys[e.Row.RowIndex].Values["dmd_IsMenu"].ToString();
            if (key == "True")
            {
                try
                {
                    ((CheckBox)(e.Row.Cells[1].FindControl("ckb_ismenu2"))).Checked = true;
                }
                catch (Exception)
                {

                    ((CheckBox)(e.Row.Cells[1].FindControl("ckb_ismenu3"))).Checked = true;
                }
                
            }
            else
            {
                try
                {
                    ((CheckBox)(e.Row.Cells[1].FindControl("ckb_ismenu2"))).Checked = false;
                }
                catch (Exception)
                {

                    ((CheckBox)(e.Row.Cells[1].FindControl("ckb_ismenu3"))).Checked = false;
                }
            }
        }

    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        getdet(_ID);
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        getdet(_ID);

       
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        String id = gv.DataKeys[e.RowIndex].Values["dmd_id"].ToString();
        string name = ((TextBox)(gv.Rows[e.RowIndex].Cells[0].Controls[0])).Text.ToString().Trim();
        bool ismenu = ((CheckBox)gv.Rows[e.RowIndex].Cells[1].FindControl("ckb_ismenu3")).Checked;
        int n;
        try
        {
            string strName = "sp_demo_updateDemoDet";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@name", name);

            param[2] = new SqlParameter("@Uid", Session["uID"].ToString());
            param[3] = new SqlParameter("@UName", Session["uName"].ToString());
            param[4] = new SqlParameter("@ismenu", ismenu);
            param[5] = new SqlParameter("@reValue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
            n= Convert.ToInt32(param[5].Value);
        }
        catch (Exception ex)
        {
            n= 2;
        }
        if (n == 0)
        {
            ltlAlert.Text = "alert('修改成功！')";
            gv.EditIndex = -1;
            getdet(_ID);

        }
        if (n == 2)
        {
            ltlAlert.Text = "alert('修改出错！')";
        }
        if (n == 1)
        {
            ltlAlert.Text = "alert('当前标题的目录已存在！')";
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        String id = gv.DataKeys[e.RowIndex].Values["dmd_id"].ToString();
        int n;
        try
        {
            string strName = "sp_demo_delete";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);
           


            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
            n = 0;
        }
        catch (Exception ex)
        {
            n = 2;
        }
        if (n == 0)
        {
            ltlAlert.Text = "alert('删除成功！')";
           
            getdet(_ID);

        }
        if (n == 2)
        {
            ltlAlert.Text = "alert('删除出错！')";
        }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        try
        {
            string strName = "sp_demo_closeDemoMstr";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@detid", _ID);
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

        }
        catch (Exception ex)
        {
            ltlAlert.Text = "alert('关闭失败！')";
            return;
        }
        ltlAlert.Text = "alert('关闭成功！')";
        Response.Redirect("TSK_DemoMstr.aspx?from=new&rt=" + DateTime.Now.ToFileTime().ToString());

    }
}