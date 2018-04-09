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

public partial class hr_AttendanceEntry : BasePage
{
    HR hr = new HR();
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            LoadCenter();

            LoadUserType();

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strUserNo = txtUserNo.Text.Trim();
        string strType = ddlType.SelectedItem.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);
        string strDate = txtDate.Text.Trim();
        string strUserType = ddlUserType.SelectedItem.Text.Trim();
        string strCenter = ddlCenter.SelectedItem.Text;
        if (strCenter.LastIndexOf('(') > 0)
        {
            strCenter = strCenter.Substring(strCenter.LastIndexOf('(') + 1, 4);
        }
        else
        {
            strCenter = "0";
        }

        if (Convert.ToString(Session["conceal"]) == "1")
        {
            gvAttendance.DataSource = hr.SelectAttendanceAllInfo(strUserNo, strType, strPlant, strDate, strUserType, strCenter);
        }
        else
        {
            gvAttendance.DataSource = hr.SelectAttendanceInfo(strUserNo, strType, strPlant, strDate, strUserType, strCenter);
        }
        gvAttendance.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAttendance_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAttendance.PageIndex = e.NewPageIndex;
        BindData();
    }
    
    protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //定义参数
            string strMID = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                HR_AttendanceInfo hr_ai = (HR_AttendanceInfo)e.Row.DataItem;

                if (hr_ai.IsManual == true)
                {
                    if (hr_ai.CreatedBy.ToString().Trim() != Convert.ToString(Session["uID"]))
                    {
                        btnDelete.Enabled = false;
                        btnDelete.Text = "";
                    }
                }
                else
                {
                    if (!this.Security["14020112"].isValid)
                    {
                        btnDelete.Enabled = false;
                        btnDelete.Text = "";
                    }
                }
            }
        }
        catch
        { }
    }

    protected void gvAttendance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strAID = gvAttendance.DataKeys[e.RowIndex].Values["AttendanceID"].ToString();
        string strUID = Convert.ToString(Session["uID"]);

        if (hr.DeleteAttendanceInfoManual(strAID, strUID))
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
        if (txtUserNo.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('工号 不能为空！');";
            return;
        }

        if (txtDate.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('考勤日期 不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('考勤日期 格式不正确！');";
                return;
            }
        }

        if (ddlCenter.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择一项 成本中心！');";
            return;
        }

        if (ddlType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择一项 考勤类型！');";
            return;
        }

        if (ddlUserType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择一项 类别！');";
            return;
        }
        
        //定义参数
        string strUserNo = txtUserNo.Text.Trim();
        string strSensor = ddlCenter.SelectedValue.Trim();
        string strType = ddlType.SelectedValue.Trim();
        string strDate = txtDate.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);
        string strUserID = hr.CheckUserIsValid(strUserNo, strPlant);
        string strCenter = ddlCenter.SelectedItem.Text.Trim();
        string strUserType = ddlUserType.SelectedValue.Trim();

        if (!hr.CheckUserIsReg(strUserNo))
        {
            ltlAlert.Text = "alert('员工工号为" + strUserNo + "的员工没有考勤机ID卡号或者指纹编号，请补！');";
            return;
        }
        else
        {
            if (strUserID.Length == 0)
            {
                ltlAlert.Text = "alert('员工工号为" + strUserNo + "不存在或者已删除或者已离职或者无效！'); ";
                txtUserNo.Text = "";
                return;
            }
            else
            {
                HR_AttendanceInfo hr_ai = new HR_AttendanceInfo();
                hr_ai.AttendanceUserNo = strUserNo;
                hr_ai.AttendanceType = strType;
                hr_ai.AttendanceTime = Convert.ToDateTime(strDate);
                hr_ai.CreatedBy = Convert.ToInt32(Session["uID"]);
                hr_ai.Sensor = strSensor;
                hr_ai.IsManual = true;
                hr_ai.orgID = Convert.ToInt32(strPlant);
                int posS = strCenter.LastIndexOf("(");
                int posE = strCenter.LastIndexOf(")");
                hr_ai.Center = strCenter.Substring(posS + 1, posE - posS - 1);
                hr_ai.CenterName = strCenter.Substring(0, posS);
                hr_ai.UserTypeID = Convert.ToInt32(strUserType);

                // --Limited the A type User maintanance
                if (ddlUserType.SelectedItem.Text.Trim() == "A")
                {
                    if (!this.Security["14020116"].isValid)
                    {
                        ltlAlert.Text = "alert('没有权限维护类型是A类的员工！');";
                        return;
                    }
                    //---End
                }

                if (hr.InsertAttendanceInfoManual(hr_ai))
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
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void LoadCenter()
    {
        //定义参数
        string strPlant = Convert.ToString(Session["plantCode"]);

        ddlCenter.Items.Clear();
        ListItem item;
        item = new ListItem("--", "0");
        ddlCenter.Items.Add(item);

        DataTable dtbCenter = hr.SelectAttendanceCenter(strPlant);
        if (dtbCenter.Rows.Count > 0)
        {
            for (int i = 0; i < dtbCenter.Rows.Count; i++)
            {
                item = new ListItem(dtbCenter.Rows[i].ItemArray[1].ToString(), dtbCenter.Rows[i].ItemArray[0].ToString());
                ddlCenter.Items.Add(item);
            }
        }
        ddlCenter.SelectedIndex = 0;
    }

    protected void LoadUserType()
    {
        //定义参数
        ddlUserType.Items.Clear();
        ListItem item;
        item = new ListItem("--", "0");
        ddlUserType.Items.Add(item);

        DataTable dtbUserType = hr.SelectUserType();
        if (dtbUserType.Rows.Count > 0)
        {
            for (int i = 0; i < dtbUserType.Rows.Count; i++)
            {
                item = new ListItem(dtbUserType.Rows[i].ItemArray[1].ToString(), dtbUserType.Rows[i].ItemArray[0].ToString());
                ddlUserType.Items.Add(item);
            }
        }
        ddlUserType.SelectedIndex = 0;
    }
}
