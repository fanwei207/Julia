using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using Wage;
using System.Configuration;

public partial class wsline_wl_calendarModi : BasePage
{
    
    private adamClass chk = new adamClass();
    private HR_AttendanceInfo AttenInfo = new HR_AttendanceInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            ddl_site.SelectedValue = Convert.ToString(Session["PlantCode"].ToString());
            ddl_site.Enabled = false;
            txb_year.Text = DateTime.Now.ToString("yyyy-MM-dd");
            LoadCC();
            BindData();
        }
    }
    protected void LoadCC()
    {
        ddl_cc.Items.Clear();
        ListItem ls;
        string StrSql;
        StrSql = " Select distinct a.cc cc,a.ccname name from tcpc0.dbo.hr_Attendance_CC a ";
        if (Convert.ToInt32(Session["uRole"]) !=1)
        {
            StrSql = StrSql + " INNER JOIN tcpc0.dbo.wo_cc_permission cp ON cp.perm_userid='" +  Convert.ToInt32(Session["uID"]) + "' AND cp.perm_ccid=a.cc ";
        }
        StrSql =StrSql + " Where a.plantID='" + ddl_site.SelectedValue + "'";
        SqlDataReader reader=SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql);
        while (reader.Read())
        {
                ls = new ListItem();
                ls.Value = reader["cc"].ToString().Trim();
                ls.Text = reader["name"].ToString().Trim() + "- " + reader["cc"].ToString().Trim();
                ddl_cc.Items.Add(ls);
        }
        reader.Close();
        ls = new ListItem("--", "0");
        ddl_cc.Items.Insert(0, ls);
    }
    protected void BindData()
    {
        if (string.IsNullOrEmpty(txb_year.Text.Trim()))
        {
            ltlAlert.Text = "alert('考勤日期不能为空！');";
            return;
        }
        else
        {
            try { Convert.ToDateTime(txb_year.Text.Trim()); }
            catch
            {
                ltlAlert.Text = "alert('考勤日期格式不正确，请按照（yyyy-mm-dd）格式输入！');";
                return;
            }
        }
        int year = Convert.ToDateTime(txb_year.Text.Trim()).Year;
        int month = Convert.ToDateTime(txb_year.Text.Trim()).Month;
        string userCenter = ddl_cc.SelectedValue;
        int  userType = Convert.ToInt32(ddl_type.SelectedValue);
        int plantID = Convert.ToInt32(ddl_site.SelectedValue);
        string userCode = txb_userno.Text.Trim();
        string checkType = ddl_atten.SelectedValue;
        int isManual = Convert.ToInt32(ddl_company.SelectedValue);
        int isCompany = Convert.ToInt32(ddl_company.SelectedValue);
        GridView1.DataSource = AttenInfo.SelectAttenInfos(year,month,/*day,*/userCenter,userType,plantID,userCode,checkType,isManual,isCompany);
        GridView1.DataBind();

            #region 如果已结算，则无法更改
            int intadjust = new HR().finAdjust(year, month, plantID, 0);
            if (intadjust < 0)
            {
                GridView1.Columns[14].Visible = false;
                GridView1.Columns[15].Visible = false;
            }
            else
            {
                GridView1.Columns[14].Visible = true;
                GridView1.Columns[15].Visible = true;          
            }
            #endregion
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string AttendID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string _C_ID = GridView1.DataKeys[e.RowIndex].Values["C_ID"].ToString();
        string _AttendanceTime = GridView1.DataKeys[e.RowIndex].Values["AttendanceTime"].ToString();

        string StrSql = string.Empty;
        if (GridView1.DataKeys[e.RowIndex].Values["HrType"].ToString() == "temp")
        {
            StrSql = "sp_hr_disableAttendanceInfos";
        }
        else if (GridView1.DataKeys[e.RowIndex].Values["HrType"].ToString() == "formal")
        {
            StrSql = "sp_hr_disableAttendanceInfo";
        }
        SqlParameter[] parmArray = new SqlParameter[5];
        parmArray[0] = new SqlParameter("@AttendanceID", AttendID);
        parmArray[1] = new SqlParameter("@CheckTime", _AttendanceTime);
        parmArray[2] = new SqlParameter("@AttendanceUserID", _C_ID);
        parmArray[3] = new SqlParameter("@uName", Session["uName"].ToString());
        parmArray[4] = new SqlParameter("@retValue", SqlDbType.Bit);
        parmArray[4].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, StrSql, parmArray);

        if (!Convert.ToBoolean(parmArray[4].Value))
        {
            this.Alert("删除失败！请联系管理员！");
        }

        BindData();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string _cc = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].FindControl("txtCenter"))).Text.Trim().ToString();
        string _cname="";
        string _C_ID = GridView1.DataKeys[e.RowIndex].Values["C_ID"].ToString();
        string _AttendanceTime = GridView1.DataKeys[e.RowIndex].Values["AttendanceTime"].ToString();
        string _utype = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("txtUserType"))).Text.Trim().ToString();
        string _atype = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("txtAttendanceType"))).Text.Trim().ToString();
        string StrSql = "select ccname from tcpc0.dbo.hr_Attendance_CC where plantID='" + ddl_site.SelectedValue + "' and cc='" + _cc + "'";
        _cname = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, StrSql).ToString();

        if (_cname==null || _cname=="")
        {
            ltlAlert.Text = "alert('请输入有效的成本中心')";
            return;
        }
        if (_utype.ToUpper()=="A")
        {
            if (!Security["14020116"].isValid)
            {
                ltlAlert.Text = "alert('没有权限维护类型是A类的员工！');";
                return;
            }
            else
            {
                _utype="394";
            }
        }
        else if (_utype.ToUpper() == "B")
        {
            _utype = "395";
        }
        else if (_utype.ToUpper() == "C")
        {
            _utype = "396";
        }
        else if (_utype.ToUpper() == "D")
        {
            _utype = "397";
        }
        else if (_utype.ToUpper() == "E")
        {
            _utype = "398";
        }
        else
        {
            ltlAlert.Text = "alert('请输入员工类型A、B、C、D、E(大写的哦)！')";
            return;
        }
        if (_atype.ToUpper() != "I" && _atype.ToUpper() != "O")
        {
            ltlAlert.Text = "alert('请输入考勤类型I、O(大写)！')";
            return;
        }

        string AttendID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        if (GridView1.DataKeys[e.RowIndex].Values["HrType"].ToString() == "temp")
        {
            StrSql = "sp_hr_adjustAttendanceInfos";
        }
        else if (GridView1.DataKeys[e.RowIndex].Values["HrType"].ToString() == "formal")
        {
            StrSql = "sp_hr_adjustAttendanceInfo";
        }
        SqlParameter[] parmArray = new SqlParameter[10];
        parmArray[0] = new SqlParameter("@AttendanceID", AttendID);
        parmArray[1] = new SqlParameter("@CheckTime", _AttendanceTime);
        parmArray[2] = new SqlParameter("@AttendanceUserID", _C_ID);
        parmArray[3] = new SqlParameter("@AttendanceUserCenter", _cc);
        parmArray[4] = new SqlParameter("@AttendanceUserCenterName", _cname);
        parmArray[5] = new SqlParameter("@AttendanceUserType", _utype.ToUpper());
        parmArray[6] = new SqlParameter("@CheckType", _atype.ToUpper());
        parmArray[7] = new SqlParameter("@modifiedby", Session["uName"].ToString());
        parmArray[8] = new SqlParameter("@ImportedBy", Session["uID"].ToString());
        parmArray[9] = new SqlParameter("@retValue", SqlDbType.Bit);
        parmArray[9].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, StrSql, parmArray);

        if (!Convert.ToBoolean(parmArray[9].Value))
        {
            this.Alert("更新失败！请联系管理员！");
        }

        GridView1.EditIndex = -1;
        BindData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        septime.Text = "";
        BindData();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (ddl_cc.SelectedIndex==0)
        {
            ltlAlert.Text = "alert('请选择成本中心')";
            return;
        }
        if (ddl_atten.SelectedIndex==0)
        {
            ltlAlert.Text = "alert('请选择考勤类型')";
            return;
        }
        if (ddl_company.SelectedIndex==0)
        {
            ltlAlert.Text = "alert('请选择补漏类型')";
            return;
        }
        if (ddl_type.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择员工类型')";
            return;
        }
        if (txb_userno.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请输入工号')";
            return;
        }
        if (txb_year.Text.Trim().Length<11)
        {
            ltlAlert.Text = "alert('请输入考勤时间yyyy-MM-dd HH:mm:ss')";
            return;
        }
        else if (!IsDate(txb_year.Text.Trim()))
        {
            ltlAlert.Text = "alert('请输入考勤时间yyyy-MM-dd HH:mm:ss')";
            return;
        }
        else if (Convert.ToDateTime(txb_year.Text) <= System.DateTime.Now.AddDays(-45) || Convert.ToDateTime(txb_year.Text) >= System.DateTime.Now)
        {
            ltlAlert.Text = "alert('只能补漏过去45天的考勤时间')";
            return;
        }

        string _uid = "";
        string _uname = "";
        string _utype = "";
        string _muid = "";

        string StrSql = "select userID,userName,isnull(Fingerprint,0) Fingerprint,usertype from tcpc0.dbo.users where plantCode='" + ddl_site.SelectedValue + "' and userNo='" + txb_userno.Text.Trim() + "'";
        SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql);
        while (reader.Read())
        {
            _uid = reader["userID"].ToString();
            _uname = reader["userName"].ToString();
            _utype = reader["usertype"].ToString();
            _muid = reader["Fingerprint"].ToString();
        }
        reader.Close();

        if (_uid!="")
        {
            if (_utype=="394")
            {
                if (!Security["14020116"].isValid)
                {
                    ltlAlert.Text = "alert('没有权限维护类型是A类的员工！');";
                }
            }
        }

        StrSql = "sp_hr_appendAttendanceInfo";
        SqlParameter[] parmArray = new SqlParameter[15];
        parmArray[0] = new SqlParameter("@CheckTime", Convert.ToDateTime(txb_year.Text));
        parmArray[1] = new SqlParameter("@CheckType", ddl_atten.SelectedValue);
        parmArray[2] = new SqlParameter("@PlantID", ddl_site.SelectedValue);
        parmArray[3] = new SqlParameter("@AttendanceUserID", _uid);
        parmArray[4] = new SqlParameter("@AttendanceUserNo", _muid);
        parmArray[5] = new SqlParameter("@AttendanceUserName", _uname);
        parmArray[6] = new SqlParameter("@AttendanceUserCode", txb_userno.Text);
        parmArray[7] = new SqlParameter("@AttendanceUserCenter", ddl_cc.SelectedValue);
        parmArray[8] = new SqlParameter("@AttendanceUserCenterName", ddl_cc.SelectedItem.Text);
        parmArray[9] = new SqlParameter("@AttendanceUserType", ddl_type.SelectedValue);
        parmArray[10] = new SqlParameter("@isCompany", ddl_company.SelectedValue);
        parmArray[11] = new SqlParameter("@uID", Session["uID"].ToString());
        parmArray[12] = new SqlParameter("@uName", Session["uName"].ToString());
        parmArray[13] = new SqlParameter("@septime", septime.Text.Trim());
        parmArray[14] = new SqlParameter("@retValue", SqlDbType.Bit);
        parmArray[14].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, StrSql, parmArray);

        if (!Convert.ToBoolean(parmArray[13].Value))
        {
            this.Alert("更新失败！请联系管理员！");
        }

        GridView1.EditIndex = -1;//取消编辑状态
        BindData();
    }
    //clear the data of control
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        ddl_type.SelectedIndex = 0;
        ddl_atten.SelectedIndex = 0;
        ddl_company.SelectedIndex = 0;
        txb_userno.Text = "";
        GridView1.PageIndex = 0;
        septime.Text = "";
        BindData();
    }
    //导出
    protected void btn_exportExcel_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/wsline/wl_calendarModiToExcel.aspx?plantID=" + ddl_site.SelectedValue + "&userCenter=" + ddl_cc.SelectedValue + "&userType=" + ddl_type.SelectedValue + "&userCode=" + txb_userno.Text.Trim() + "&checkTime=" + txb_year.Text.Trim() + "&checktype=" + ddl_atten.SelectedValue + "&isCompany=" + ddl_company.SelectedValue + "&rt=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";
    }
    //导出包含工段
    protected void btn_exportExcel2_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/wsline/wl_calendarModiIncludToExcel.aspx?plantID=" + ddl_site.SelectedValue + "&userCenter=" + ddl_cc.SelectedValue + "&userType=" + ddl_type.SelectedValue + "&userCode=" + txb_userno.Text.Trim() + "&checkTime=" + txb_year.Text.Trim() + "&checktype=" + ddl_atten.SelectedValue + "&isCompany=" + ddl_company.SelectedValue + "&rt=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";
    }
    //更新成本中心
    protected void btn_updateCC_Click(object sender, EventArgs e)
    {
        if (!IsDate(txb_year.Text.Trim()))
        {
            Alert("考勤日期格式不正确！");
            return;
        }
        string StrSql = "sp_hr_modifyUserCostCenterByDate";
        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@date", txb_year.Text.Trim());
        sqlParam[1] = new SqlParameter("@plantID", Session["PlantCode"]);

        SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, StrSql, sqlParam);
        GridView1.EditIndex = -1;
        BindData();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
}