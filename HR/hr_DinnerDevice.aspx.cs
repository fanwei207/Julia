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

public partial class hr_DinnerDevice : System.Web.UI.Page
{
    HR hr = new HR();
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            int nRet = chk.securityCheck(Convert.ToString(Session["uID"]), Convert.ToString(Session["uRole"]), Convert.ToString(Session["orgID"]), "14020119", false, false);
            if (nRet <= 0)
            {
                Response.Redirect("~/public/Message.aspx?type=" + nRet.ToString(), true);
            }

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strDevice = txtDevice.Text.Trim();
        string strSensor = txtSensor.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        gvDevice.DataSource = hr.SelectDinnerDevice(strDevice, strSensor, strPlant);
        gvDevice.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDevice_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvDevice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDevice.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvDevice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strDeviceID = gvDevice.DataKeys[e.RowIndex].Value.ToString();

        if (hr.DeleteDinnerDevice(strDeviceID))
        {
            ltlAlert.Text = "alert('删除成功!');";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程出错！'); ";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtDevice.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('机器号 不能为空！');";
            return;
        }

        if (txtSensor.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('设备编号 不能为空！');";
            return;
        }

        //定义参数
        string strDevice = txtDevice.Text.Trim();
        string strSensor = txtSensor.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        if (hr.CheckRecordIsExist(strDevice, strSensor, strPlant))
        {
            ltlAlert.Text = "alert('机器编号为" + strDevice + "或者设备编号为" + strSensor + "的记录已存在！'); ";

            return;
        }
        else
        {
            if (hr.InsertDinnerDevice(strDevice, strSensor, strPlant))
            {
                ltlAlert.Text = "alert('增加成功！');";
                BindData();
            }
            else
            {
                ltlAlert.Text = "alert('增加数据过程出错！'); ";
            }


        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
}
