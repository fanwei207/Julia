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
using Wage;

public partial class HR_hr_leave : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";
            txtEndDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        int type = Convert.ToInt32(ddlType.SelectedValue.ToString());
        string strStart = txtStartDate.Text.Trim();
        string strEnd = txtEndDate.Text.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strUserName = txtUserName.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"]);
        int uRole = Convert.ToInt32(Session["uRole"]);

        HR hr = new HR();

        gvLeave.DataSource = hr.SelectLeaveList(type, strStart, strEnd, strUserNo, strUserName, uID, uRole);
        gvLeave.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLeave_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvLeave_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        int LeaveID = Convert.ToInt32(gvLeave.DataKeys[e.RowIndex].Value.ToString());
        int Type = Convert.ToInt32(ddlType.SelectedValue.Trim());

        if (hr.DeleteLeaveInfo(LeaveID))
        {
            ltlAlert.Text = "alert('" + ddlType.SelectedItem.Text + " 删除成功！');";

            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程中出错！'); ";
        }
    }

    protected void gvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeave.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        #region 验证
        if (txtLaborNo.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('员工工号 不能为空！');";
            return;
        }

        if (txtStartDate.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('起始日期 不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtStartDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期 格式不正确！');";
                return;
            }
        }

        if (txtEndDate.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('结束日期 不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEndDate.Text.Trim());

                if (_dt < Convert.ToDateTime(txtStartDate.Text.Trim()))
                {
                    ltlAlert.Text = "alert('结束日期 不能小于 起始日期！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期 格式不正确！');";
                return;
            }
        }

        if (ddlType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项 请假类型！');";
            return;
        }

        if (txtDays.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请假天数 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtDays.Text.Trim());

                if (_dc <= 0)
                {
                    ltlAlert.Text = "alert('请假天数 只能大于0！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('请假天数 只能是数字！');";
                return;
            }
        }
        #endregion

        //定义参数
        string strDay = "0";
        int Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
        string strStart = txtStartDate.Text.Trim();
        string strEnd = txtEndDate.Text.Trim();
        string strMemo = txtMemo.Text.Trim();
        string strLaborID = lblUID.Text.Trim();
        string strLaborNo = txtLaborNo.Text.Trim();
        string strLaborName = lblLaborNameValue.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"]);
        bool Ret = false;
        int cntMerrage = 0;

        if(txtDays.Text.Trim() != "") strDay = txtDays.Text.Trim();

        Ret = hr.CheckLeaveDays(strStart, strEnd, strDay);
        if (Ret)
        {
            if (Type.ToString() == "3")
            {
                InsertMerrageInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID);
                BindData();
            }
            else
            {
                if (hr.InsertLeaveInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID))
                {
                    ltlAlert.Text = "alert('" + ddlType.SelectedItem.Text + "新增成功！');";
                    
                    txtLaborNo.Text = "";
                    lblLaborNameValue.Text = "";
                    lblUID.Text = "";
                    txtDays.Text = "";
                    txtMemo.Text = "";

                    BindData();
                }
                else
                {
                    ltlAlert.Text = "alert('新增数据过程中出错！'); ";
                }
            }
        }
        else
        {
            TimeSpan startdate = new TimeSpan(Convert.ToDateTime(strStart).Ticks);
            TimeSpan enddate = new TimeSpan(Convert.ToDateTime(strEnd).Ticks);
            TimeSpan datediff = enddate.Subtract(startdate).Duration();
            ltlAlert.Text = "alert('请假天数应介于" + Convert.ToString(datediff.Days) + "到" + Convert.ToString(datediff.Days + 1) + "之间！'); Form1.txtDays.focus();";
            txtDays.Text = "";
        }
    }

    //判断输入员工工号是否合法
    protected void txtLaborNo_TextChanged(object sender, EventArgs e)
    {
        string strUserNo = txtLaborNo.Text.Trim();
        int PlantID = Convert.ToInt32(Session["PlantCode"]);

        string strUserName = hr.SelectUserName(strUserNo, PlantID);

        switch (strUserName)
        {
            case "":
                ltlAlert.Text = "alert('工号不存在！'); Form1.txtLaborNo.focus();";
                txtLaborNo.Text = "";
                lblLaborNameValue.Text = "";
                lblUID.Text = "";
                break;

            case "此员工属于离职员工！":
                ltlAlert.Text = "alert('" + strUserName + "'); Form1.txtLaborNo.focus();";
                txtLaborNo.Text = "";
                lblLaborNameValue.Text = "";
                lblUID.Text = "";
                break;

            default:
                string[] struser = strUserName.Split(',');
                if (struser[2] != "")
                {
                    ltlAlert.Text = "alert('" + struser[2] + "'); Form1.txtMemo.focus();";
                }
                lblLaborNameValue.Text = struser[1];
                lblUID.Text = struser[0];

                string strLaborID = lblUID.Text.Trim();
                if (ddlType.SelectedValue == "3" && hr.SelectMerrageDaysInfo(strLaborID) > 0)
                {
                    btnAddNew.Attributes.Add("onclick", "return confirm('该员工已经存在一次婚假，是否继续？');");
                }

                break;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    //判断是否可以删除
    protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HR_LeaveInfo li = (HR_LeaveInfo)e.Row.DataItem;

            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Font.Bold = false;
            btnDelete.Font.Size = new FontUnit(8);

            if (Convert.ToInt32(li.CreatedBy) == Convert.ToInt32(Session["uID"]))
            {
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }
        }
    }

    protected void InsertMerrageInfo(int Type, string strStart, string strEnd, string strDay, string strLaborID, string strLaborNo, string strLaborName, string strMemo,int  uID)
    {
        if (hr.InsertLeaveInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID))
        {
            ltlAlert.Text = "alert('婚假新增成功！');";

            txtLaborNo.Text = "";
            lblLaborNameValue.Text = "";
            lblUID.Text = "";
            txtDays.Text = "";
            txtMemo.Text = "";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('新增数据过程中出错！'); ";
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //婚假
        if (ddlType.SelectedValue == "3" && txtLaborNo.Text.Trim().Length > 0)
        {
            string strLaborID = lblUID.Text.Trim();
            if (hr.SelectMerrageDaysInfo(strLaborID) > 0)
            {
                btnAddNew.Attributes.Add("onclick", "return confirm('该员工已经存在一次婚假，是否继续？');");
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int type = Convert.ToInt32(ddlType.SelectedValue.ToString());
        string strStart = txtStartDate.Text.Trim();
        string strEnd = txtEndDate.Text.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strUserName = txtUserName.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"]);
        int uRole = Convert.ToInt32(Session["uRole"]);

        this.ExportExcel(chk.dsnx()
                , "60^<b>工号</b>~^80^<b>姓名</b>~^100^<b>开始日期</b>~^100^<b>结束日期</b>~^60^<b>天数</b>~^80^<b>创建人</b>~^100^<b>创建日期</b>~^150^<b>备注</b>~^<b>部门</b>~^"
                , hr.SelectLeaveExcel(type, strStart, strEnd, strUserNo, strUserName, uID, uRole)
                , false);
    }
}
