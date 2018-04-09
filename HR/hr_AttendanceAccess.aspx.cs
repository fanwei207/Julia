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
using Wage;
using adamFuncs;

public partial class hr_AttendanceAccess : BasePage
{
    HR hr = new HR();
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strSensor = txtSensor.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        gvAccess.DataSource = hr.SelectAttendanceAccess(strSensor, strPlant);
        gvAccess.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAccess_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvAccess_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccess.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvAccess_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strSensor = gvAccess.DataKeys[e.RowIndex].Value.ToString();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        if (hr.DeleteAttendanceAccess(strSensor, strPlant))
        {
            ltlAlert.Text = "alert('删除成功!'); window.location.href='/HR/hr_AttendanceAccess.aspx?rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程出错！'); ";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtSensor.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('门禁设备号 不能为空！'); ";
            return;
        }
        
        //定义参数
        string strSensor = txtSensor.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        if (hr.InsertAttendanceAccess(strSensor, strPlant))
        {
            ltlAlert.Text = "alert('增加成功！')";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('增加数据过程出错！'); ";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        txtSensor.Text = "";
    }
}
