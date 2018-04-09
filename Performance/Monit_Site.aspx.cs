using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Performance_CCTV_Site : BasePage
{
    Monitor monitor = new Monitor();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            #region 初始化数据（新增）
            ddl_plant.SelectedValue = "0";
            btn_Back.Visible = false;
            //选中当前域
            ddl_plant.SelectedIndex = -1;
            ddl_plant.Items.FindByValue(Session["PlantCode"].ToString()).Selected = true;
            
            //区域绑定
            BindArea();
            ddl_area.SelectedValue = "--";
            #endregion
            #region 初始化数据（修改）
            if (Request.QueryString["Monit_mID"] != null)
            {
                btn_Back.Visible = true;
                if (Request.QueryString["isModify"] != "1") btn_Add.Visible = false;

                DataTable dt = monitor.SelectMonitorByID(Request.QueryString["Monit_mID"]);
                txt_MonitID.Text = Request.QueryString["Monit_mID"];
                txt_remark.Text = dt.Rows[0]["Monit_Remark"].ToString();
                txt_resolution.Text = dt.Rows[0]["Monit_Resolution"].ToString();
                txt_beltline.Text = dt.Rows[0]["Monit_Beltline"].ToString();
                ddl_plant.SelectedValue = dt.Rows[0]["Monit_PlantID"].ToString();
                ddl_area.SelectedValue = dt.Rows[0]["Monit_AreaID"].ToString();
                txt_MonitID.Enabled = false;
            }
            #endregion
        }
        
    }
    protected void BindArea()
    {
        this.ddl_area.Items.Clear();
        ddl_area.DataSource = monitor.BindArea(ddl_plant.SelectedItem.ToString());
        ddl_area.DataBind();
        ddl_area.Items.Add(new ListItem("--", "--"));
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if(txt_MonitID.Text.Trim().Length==0)
        {
            ltlAlert.Text = "alert('摄像头编号不能为空！')";
            return;
        }
        if (txt_resolution.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('像素不能为空！')";
            return;
        }
        if (ddl_area.SelectedValue=="--")
        {
            ltlAlert.Text = "alert('区域必选！')";
            return;
        }
        if(ddl_plant.SelectedValue=="0")
        {
            ltlAlert.Text = "alert('公司必选！')";
            return;
        }
        string MonitId = txt_MonitID.Text.Trim();
        string ID = Request.QueryString["ID"];
        string Resolution = txt_resolution.Text.Trim();
        string beltline = txt_beltline.Text.Trim();
        string Remark = txt_remark.Text.Trim();
        if (Request.QueryString["Monit_mID"] == null)
        {
            if (monitor.AddMonitor(MonitId, ddl_plant.SelectedValue, ddl_plant.SelectedItem.ToString(), Resolution, beltline,ddl_area.SelectedValue,ddl_area.SelectedItem.ToString(), Remark, Session["uID"].ToString()))
                ltlAlert.Text = "alert('添加成功！')";
            else
                ltlAlert.Text = "alert('添加失败！摄像头编号不能重复！')";
        }
        else
        {
            //修改时同时修改Monit_Log中的mID（当mID变化时）
            if (monitor.ModifyMonitor(ID, MonitId, ddl_plant.SelectedValue, ddl_plant.SelectedItem.ToString(), Resolution, beltline, ddl_area.SelectedValue, ddl_area.SelectedItem.ToString(), Remark, Session["uID"].ToString()))
                ltlAlert.Text = "alert('修改成功！')";
            else
                ltlAlert.Text = "alert('修改失败！摄像头编号不能重复！')";
        }
        
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Monit_SiteList.aspx?Monit_mID="+Request.QueryString["Monit_mID"]);
    }
    protected void ddl_plant_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindArea();
    }
}