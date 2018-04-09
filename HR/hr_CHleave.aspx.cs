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

using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Collections;

using System.IO;

public partial class HR_hr_CHleave : BasePage
{
    adamClass chk = new adamClass();
  
    adamClass adam = new adamClass();
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

      
        gvLeave.DataSource = SelectLeaveList(type, strStart, strEnd, strUserNo, strUserName, uID, uRole);
        gvLeave.DataBind();
    }


    /// <summary>
    /// 获得请假信息
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="strStart"></param>
    /// <param name="strEnd"></param>
    /// <param name="strUserNo"></param>
    /// <param name="strUserName"></param>
    /// <returns>返回请假信息</returns>
    public IList<HR_LeaveInfo> SelectLeaveList(int Type, string strStart, string strEnd, string strUserNo, string strUserName, int uID, int uRole)
    {
        try
        {
            string strSql = string.Empty;
            SqlParameter[] sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@start", strStart);
            sqlParam[1] = new SqlParameter("@end", strEnd);
            sqlParam[2] = new SqlParameter("@userno", strUserNo);
            sqlParam[3] = new SqlParameter("@username", strUserName);
            sqlParam[4] = new SqlParameter("@uID", uID);
            sqlParam[5] = new SqlParameter("@uRole", uRole);

            switch (Type)
            {
                //事假
                case 1:
                    strSql = "sp_hr_CHSelectBussinessLeave";
                    break;

                //病假
                case 2:
                    strSql = "sp_hr_CHSelectSickLeave";
                    break;

                //婚假
                case 3:
                    strSql = "sp_hr_CHSelectMerrageLeave";
                    break;

                //丧假
                case 4:
                    strSql = "sp_hr_CHSelectFuneralLeave";
                    break;

                //旷工
                case 5:
                    strSql = "sp_hr_CHSelectMinerLeave";
                    break;

                //产假
                case 6:
                    strSql = "sp_hr_CHSelectMaternityLeave";
                    break;

                //工伤
                case 7:
                    strSql = "sp_hr_CHSelectInjuryLeave";
                    break;
            }

            IList<HR_LeaveInfo> HR_LeaveInfo = new List<HR_LeaveInfo>();
            IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                HR_LeaveInfo li = new HR_LeaveInfo();
                li.LeaveID = Convert.ToInt32(reader["LeaveID"]);
                li.UserCode = reader["UserCode"].ToString();
                li.UserName = reader["UserName"].ToString();
                li.StartDate = Convert.ToDateTime(reader["StartDate"]);
                li.EndDate = Convert.ToDateTime(reader["EndDate"]);
                li.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                li.UserName = reader["UserName"].ToString();
                li.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                li.Creater = reader["Creater"].ToString();
                li.Days = Convert.ToDecimal(reader["Days"]);
                li.Comment = reader["Comment"].ToString();
                HR_LeaveInfo.Add(li);
            }
            reader.Close();
            return HR_LeaveInfo;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
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

        if (DeleteLeaveInfo(LeaveID))
        {
            ltlAlert.Text = "alert('" + ddlType.SelectedItem.Text + " 删除成功！');";

            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程中出错！'); ";
        }
    }
    /// <summary>
    /// 删除请假信息
    /// </summary>
    /// <param name="LevelID"></param>
    /// <returns></returns>
    public bool DeleteLeaveInfo(int LeaveID)
    {
        try
        {
            string strSql = "sp_hr_CHDeleteLeaveInfo";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@leaveid", LeaveID);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
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

        if (txtDays.Text.Trim() != "") strDay = txtDays.Text.Trim();

        Ret = CheckLeaveDays(strStart, strEnd, strDay);
        if (Ret)
        {
            if (Type.ToString() == "3")
            {
                InsertMerrageInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID);
                BindData();
            }
            else
            {
                if (InsertLeaveInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID))
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
    /// <summary>
    /// 判断输入天数是否落在结束日期与开始日期的差值内
    /// </summary>
    /// <param name="strStart"></param>
    /// <param name="strEnd"></param>
    /// <param name="strDay"></param>
    /// <returns>返回True/False</returns>
    public bool CheckLeaveDays(string strStart, string strEnd, string strDay)
    {
        try
        {
            TimeSpan startdate = new TimeSpan(Convert.ToDateTime(strStart).Ticks);
            TimeSpan enddate = new TimeSpan(Convert.ToDateTime(strEnd).Ticks);
            TimeSpan datediff = enddate.Subtract(startdate).Duration();

            if (strDay != "0")
            {
                if (Convert.ToDecimal(strDay) > datediff.Days + 1 || Convert.ToDecimal(strDay) < datediff.Days) return false;
                else return true;
            }
            else return true;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
    }

    /// <summary>
    /// 新增请假信息
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="strStart"></param>
    /// <param name="strEnd"></param>
    /// <param name="lID"></param>
    /// <param name="lNo"></param>
    /// <param name="lName"></param>
    /// <param name="uID"></param>
    /// <returns>返回是否新增成功</returns>
    public bool InsertLeaveInfo(int Type, string strStart, string strEnd, string strDay, string strLaborID, string strLaborNo, string strLaborName, string strMemo, int uID)
    {
        try
        {
            string strSql = string.Empty;

            //if (strDay == "") strDay = "0";

            SqlParameter[] sqlParam = new SqlParameter[8];
            sqlParam[0] = new SqlParameter("@start", strStart);
            sqlParam[1] = new SqlParameter("@end", strEnd);
            sqlParam[2] = new SqlParameter("@day", strDay == "" ? "0" : strDay);
            sqlParam[3] = new SqlParameter("@laborid", strLaborID);
            sqlParam[4] = new SqlParameter("@laborno", strLaborNo);
            sqlParam[5] = new SqlParameter("@laborname", strLaborName);
            sqlParam[6] = new SqlParameter("@memo", strMemo);
            sqlParam[7] = new SqlParameter("@uid", uID);

            switch (Type)
            {
                //事假
                case 1:
                    strSql = "sp_hr_CHInsertBussinessLeave";
                    break;

                //病假
                case 2:
                    strSql = "sp_hr_CHInsertSickLeave";
                    break;

                //婚假
                case 3:
                    strSql = "sp_hr_CHInsertMerrageLeave";
                    break;

                //丧假
                case 4:
                    strSql = "sp_hr_CHInsertFuneralLeave";
                    break;

                //旷工
                case 5:
                    strSql = "sp_hr_CHInsertMinerLeave";
                    break;

                //产假
                case 6:
                    strSql = "sp_hr_CHInsertMaternityLeave";
                    break;

                //工伤
                case 7:
                    strSql = "sp_hr_CHInsertInjuryLeave";
                    break;
            }

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
    }
    /// <summary>
    /// 获得员工信息
    /// </summary>
    /// <param name="strUserNo">员工工号</param>
    /// <returns>返回员工姓名</returns>
    public string SelectUserName(string strUserNo, int PlantID)
    {
        try
        {
            string strSql = "sp_hr_SelectUserName";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uno", strUserNo);
            sqlParam[1] = new SqlParameter("@plant", PlantID);

            return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
        }
        catch (Exception ex)
        {
            //throw ex;
            return "";
        }
    }

    //判断输入员工工号是否合法
    protected void txtLaborNo_TextChanged(object sender, EventArgs e)
    {
        string strUserNo = txtLaborNo.Text.Trim();
        int PlantID = Convert.ToInt32(Session["PlantCode"]);

        string strUserName = SelectUserName(strUserNo, PlantID);

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
                if (ddlType.SelectedValue == "3" && SelectMerrageDaysInfo(strLaborID) > 0)
                {
                    btnAddNew.Attributes.Add("onclick", "return confirm('该员工已经存在一次婚假，是否继续？');");
                }

                break;
        }
    }
    /// <summary>
    /// 获得婚假记录
    /// </summary>
    /// <param name="strLaborID"></param>
    /// <returns>返回婚假条数</returns>
    public int SelectMerrageDaysInfo(string strLaborID)
    {
        try
        {
            string strSql = "sp_hr_CHSelectMerrageLeaveInfo";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@laborid", strLaborID);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
        }
        catch (Exception ex)
        {
            //throw ex;
            return -1;
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

    protected void InsertMerrageInfo(int Type, string strStart, string strEnd, string strDay, string strLaborID, string strLaborNo, string strLaborName, string strMemo, int uID)
    {
        if (InsertLeaveInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID))
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
            if (SelectMerrageDaysInfo(strLaborID) > 0)
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
                , SelectLeaveExcel(type, strStart, strEnd, strUserNo, strUserName, uID, uRole)
                , false);
    }
    /// <summary>
    /// 获得请假信息Sql
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="strStart"></param>
    /// <param name="strEnd"></param>
    /// <param name="strUserNo"></param>
    /// <param name="strUserName"></param>
    /// <param name="uID"></param>
    /// <param name="uRole"></param>
    /// <returns>返回请假信息Sql</returns>
    public string SelectLeaveExcel(int Type, string strStart, string strEnd, string strUserNo, string strUserName, int uID, int uRole)
    {
        try
        {
            string strSql = string.Empty;
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@start", strStart);
            sqlParam[1] = new SqlParameter("@end", strEnd);
            sqlParam[2] = new SqlParameter("@userno", strUserNo);
            sqlParam[3] = new SqlParameter("@username", strUserName);
            sqlParam[4] = new SqlParameter("@uID", uID);
            sqlParam[5] = new SqlParameter("@uRole", uRole);
            sqlParam[6] = new SqlParameter("@type", Type.ToString());

            strSql = "sp_hr_CHSelectLeaveExcel";

            return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
        }
        catch (Exception ex)
        {
            //throw ex;
            return "";
        }
    }
}
