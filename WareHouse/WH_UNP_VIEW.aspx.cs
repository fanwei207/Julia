using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wage;
using WHInfo;
using System.Text;
using System.IO;
using System.Xml;

public partial class WH_UNP_VIEW : BasePage
{
    WareHouse WH = new WareHouse();
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            #region 默认值初始化
            //地点绑定
            ddl_site.SelectedValue = Session["Plantcode"].ToString();
            //ddl_site.Enabled = false;
            //项目类型（会计科目）
            ddl_projType.DataSource=BindProjType();
            ddl_projType.DataBind();
            ddl_projType.Items.Insert(0, new ListItem("--", "0"));
            //部门绑定
            ddl_departmentBind();
            #endregion
            txt_startdate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-10));
            string _plantCode = Session["PlantCode"].ToString();

            if (_plantCode == "1")
                _plantCode = "SZX";
            else if (_plantCode == "2")
                _plantCode = "ZQL";
            else if (_plantCode == "5")
                _plantCode = "YQL";
            else if (_plantCode == "8")
                _plantCode = "HQL";
            else
                _plantCode = "";
            hd_domain.Value = _plantCode;
            BindSite();
        }
    }


    /// <summary>
    /// 绑定地点信息
    /// </summary>
    private void BindSite()
    {
        ddl_site.Items.Clear();
        ddl_site.Items.Add("--请选地点--");
        try
        {
            String strSQL = "";
            strSQL = "sp_wh_selectSiteByUnplss";
            SqlParameter[] parma = new SqlParameter[1];
            parma[0] = new SqlParameter("@Domain", hd_domain.Value);
            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddl_site.Items.Add(new ListItem(reader["si_site"].ToString(), reader["si_site"].ToString()));
                }
                reader.Close();
            }
        }
        catch
        {
            this.Alert("获取地点失败,请联系管理员");
        }
    }

    protected string Company()
    {
        string domain = "";
        if (Session["Plantcode"].ToString() == "1")
        {
            domain = "SZX";
        }
        if (Session["Plantcode"].ToString() == "2")
        {
            domain = "ZQL";
        }
        if (Session["Plantcode"].ToString() == "5")
        {
            domain = "YQL";
        }
        if (Session["Plantcode"].ToString() == "8")
        {
            domain = "HQL";
        }
        return domain;
    }
    private void Bindgv()
    {
        string Site = "";
        if (ddl_site.SelectedIndex == 0)
        {
            Site = "";
        }
        else
        {
            Site = ddl_site.SelectedValue;
        }
        DataTable dt = WH.GetUNPHaveAppInfos(txt_no.Text.Trim(), txt_applyer.Text.Trim(), ddl_department.SelectedValue, Site, ddl_type.SelectedValue, rd_type.SelectedValue, ddl_projType.SelectedValue, txt_startdate.Text.Trim(), txt_enddate.Text.Trim(),txt_nbr.Text.Trim(),txt_ID.Text.Trim());
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected DataTable BindProjType()
    {
        try
        {
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_unp_selectProjType").Tables[0];
        }
        catch
        {
            return null;
        }

    }
    protected void ddl_departmentBind()
    {
        ListItem item;
        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                ddl_department.Items.Add(item);
            }
        }
        ddl_department.SelectedValue = Session["deptID"].ToString();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        Bindgv();
    }
    protected DataTable GvBind()
    {
        string sql = "sp_wh_selectunpIss";
        SqlParameter[] param = new SqlParameter[3];
        param[0]= new SqlParameter("@wh_nbr",txt_no.Text.Trim());
        param[1] = new SqlParameter("@domain", hd_domain.Value);//ddl_company.SelectedValue);
        param[2]= new SqlParameter("@site",Convert.ToInt32(ddl_site.SelectedValue));

        try
        {
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql,param).Tables[0];
        }
        catch 
        {
            return null;
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intRow = e.RowIndex ;
        string AppNo = gv.DataKeys[intRow].Values["wh_nbr"].ToString() ;
        int flag = WH.DeleteNUPApp(AppNo);
        if (flag < 0)
        {
            Alert("删除失败，请联系管理员!");
        }
        else
        {
            Alert("删除成功!");
        }
        Bindgv();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="Det")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string type = gv.DataKeys[index].Values["wh_type"].ToString();
            string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString();
            string lot = gv.DataKeys[index].Values["wh_nbr"].ToString();
            if (type == "UNP-ISS")
            {
                ltlAlert.Text = "var w=window.open('WH_UNP_ISS.aspx?nbr=" + nbr + "','docitem','menubar=No,scrollbars = No,resizable=No,width=1200,height=600,top=100,left=100'); w.focus();";
            }
            if (type == "UNP-RCT")
            {
                ltlAlert.Text = "var w=window.open('WH_UNP_RCT.aspx?nbr=" + nbr + "','docitem','menubar=No,scrollbars = No,resizable=No,width=1200,height=600,top=100,left=100'); w.focus();";

            }

        }
        if (e.CommandName == "Submit")
        {

        }
    }

    protected void btn_continue_Click(object sender, EventArgs e)
    {
        this.Redirect("Wh_unpIss.aspx");
    }
    protected void btn_exportexcel_Click(object sender, EventArgs e)
    {
        DataTable dt = WH.GetUnplssAppInfos(txt_no.Text.Trim());
        string title = "120^<b>单据号</b>~^110^<b>项目类型</b>~^70^<b>域</b>~^70^<b>地点</b>~^70^<b>类型</b>~^120^<b>物料编码</b>~^200^<b>品名 规格 型号</b>~^70^<b>单位</b>~^70^<b>库位</b>~^70^<b>需求数量</b>~^70^<b>实发数量</b>~^200^<b>备注</b>~^120^<b>申请者</b>~^120^<b>申请者部门</b>~^";
        this.ExportExcel(title, dt, false);
    }
    protected void btn_find_Click(object sender, EventArgs e)
    {
        Bindgv();
    }
    protected void rd_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgv();
    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgv();
    }
    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgv();
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgv();
    }
    protected void ddl_projType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgv();
    }
}