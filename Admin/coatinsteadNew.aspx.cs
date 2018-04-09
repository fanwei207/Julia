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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class coatinsteadNew : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    Hr_coat coat = new Hr_coat();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCoatTypes();
            BindDepartments();
            GridViewBind();
            //this.gv_coatdetail.Columns[0].Visible = false;
        }
    }

    private void BindCoatTypes()
    {
        ddl_coattype.Items.Clear();
        ddl_coattypeadd.Items.Clear();
        try
        {
            ddl_coattype.DataSource = coat.GetCoatTypes();//ds;
            ddl_coattype.DataBind();
            ddl_coattype.Items.Insert(0, "--请选择--");
            ddl_coattype.SelectedIndex = 0;

            ddl_coattypeadd.DataSource = coat.GetCoatTypes();//ds;
            ddl_coattypeadd.DataBind();
            ddl_coattypeadd.Items.Insert(0, "--请选择--");
            ddl_coattypeadd.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取名称列表失败!");
        }
    }

    private void BindDepartments()
    {
        ddl_department.Items.Clear();
        try
        {
            ddl_department.DataSource = coat.GetDepartments();//ds;
            ddl_department.DataBind();
            ddl_department.Items.Insert(0, "--请选择--");
            ddl_department.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取名称列表失败!");
        }
    }

    private void GridViewBind() 
    {
        try
        {
            string userno = txt_userno.Text.Trim();
            string username = txt_name.Text.Trim();
            string deparment = ddl_department.SelectedValue == "--请选择--" ? "0" : ddl_department.SelectedValue;
            string coattype = ddl_coattype.SelectedValue;
            bool isleave = ck_leaveperson.Checked;
            int plantid = Convert.ToInt32(Session["plantcode"]);
            gv_coatdetail.DataSource = coat.GetCoatDetails(userno, username, deparment, coattype, isleave, plantid);
            gv_coatdetail.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_coatdetail.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (coat.CoatInfoDelete(gv_coatdetail.DataKeys[e.RowIndex].Values["userUniformDetailID"].ToString()))
            {
                this.Alert("删除成功！");
            }
            else
            {
                this.Alert("删除失败！");
            }
        }
        catch
        {
            this.Alert("删除失败！请联系管理员！"); ;
        }

        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_coatdetail.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void btn_Query_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_coatdetail.PageIndex = e.NewPageIndex;

        GridViewBind();
    }

    protected void ddl_formtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void txt_numadd_TextChanged(object sender, EventArgs e)
    {
        if (coat.GetAppName(txt_appuserno.Text.Trim(), Session["plantcode"].ToString()).Rows.Count <= 0)
        {
            this.Alert("工号不存在!");
            this.txt_appname.Text = null;
            return;
        }
        txt_appname.Text = coat.GetAppName(txt_appuserno.Text.Trim(), Session["plantcode"].ToString()).Rows[0]["username"].ToString();
        txt_userno.Text = txt_appuserno.Text;
        txt_name.Text = txt_appname.Text; 
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        string userno = txt_appuserno.Text.Trim();
        if (coat.GetAppName(userno, Session["plantcode"].ToString()).Rows.Count <= 0)
        {
            this.Alert("工号不存在!");
            this.txt_appname.Text = null;
            return;
        }
        DateTime appdate = System.DateTime.Now;
        try
        {
            appdate = Convert.ToDateTime(txt_Appdate.Text);
        }
        catch
        {
            this.Alert("申领日期必须为日期格式!");
            return;
        }
        string apptype = ddl_coattypeadd.SelectedValue;
        if (ddl_coattypeadd.SelectedIndex == 0)
        {
            this.Alert("请选择服装类型!");
            return;
        }
        int appcount = 0;
        try
        {
            appcount = Convert.ToInt32(txt_count.Text.Trim());
        }
        catch
        {
            this.Alert("数量必须为整数!");
            return;
        }
         coat.CoatInfoAdd(userno, appdate, apptype, appcount, Convert.ToInt32(Session["uid"]), Convert.ToInt32(Session["plantcode"]));
         GridViewBind();
         this.Alert("添加成功!");
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string userno = txt_userno.Text.Trim();
        string username = txt_name.Text.Trim();
        string deparment = ddl_department.SelectedValue == "--请选择--" ? "0" : ddl_department.SelectedValue;
        string coattype = ddl_coattype.SelectedValue;
        bool isleave = ck_leaveperson.Checked;
        int plantid = Convert.ToInt32(Session["plantcode"]);
        DataTable dt = coat.GetCoatDetails(userno, username, deparment, coattype, isleave, plantid);

        string title = "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>申请日期</b>~^<b>服装类型</b>~^<b>件数</b>~^<b>录入人</b>~^<b>录入时间</b>~^";
        ExportExcel2007(title, dt, false);
    }
}
