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
using System.Text.RegularExpressions;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using MRInfo;
using System.Text;
using System.IO;
using System.Xml;
using CommClass;
using System.Collections.Generic;


public partial class mr_OrderRoom : BasePage
{
    MeetingRoom mr = new MeetingRoom();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_back.Visible = false;
            if (Request.QueryString["mr_mstr"] == "1")
            {
                btn_back.Visible = true;
                string FormId = Request["MR_FormId"].ToString();
                BindOrder(FormId);
                BindHoursList();
            }
            else 
            {
                BindHoursList();
                BindCompanyList();
                AppBind();
                BindMeetingRoom();
            }
        }
    }


    private void BindOrder(string FormId)
    {
        SqlParameter param = new SqlParameter("@FormId", FormId);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_mr_selectByFormID", param).Tables[0];
        tb_FormID.Text = dt.Rows[0]["MR_FormId"].ToString();
        tb_AppName.Text = dt.Rows[0]["MR_AppName"].ToString();
        tb_DeptName.Text = dt.Rows[0]["MR_Deptname"].ToString();
        tb_MeettingMemberNum.Text = dt.Rows[0]["MR_MeettingMemberNum"].ToString();
        tb_AppExtNo.Text = dt.Rows[0]["MR_AppExtNo"].ToString();
        tb_Reason.Text = dt.Rows[0]["MR_Reason"].ToString();
        tb_BorrowThings.Text = dt.Rows[0]["MR_BorrowThings"].ToString();
        tb_otherDes.Text = dt.Rows[0]["MR_otherDes"].ToString();

        string plant = dt.Rows[0]["location"].ToString();
        SqlParameter[] parma = new SqlParameter[2];
        parma[0] = new SqlParameter("@plants", plant);
        parma[1] = new SqlParameter("@roomid", "");
        DataTable d = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_mr_selectroominfo", parma).Tables[0];


        BindCompanyList();

        ddl_RoomID.Items.Clear();
        this.ddl_RoomID.DataSource = d;
        this.ddl_RoomID.DataTextField = "mr_name";
        this.ddl_RoomID.DataValueField = "mr_num";
        this.ddl_RoomID.SelectedValue = d.Rows[0]["mr_num"].ToString();
        //ddl_RoomID.Items.Clear();
        
        this.ddl_RoomID.DataBind();


        string company = dt.Rows[0]["location"].ToString().Trim();
        string room = dt.Rows[0]["MR_RoomID"].ToString().Trim();

       
        if (ddl_RoomID.SelectedIndex == 0)
        {
            ddl_RoomID.Items.Insert(0,"--请选择会议室--");
            DataTable mt = mr.GetMeetingRoomGoodsList(company, room);

            if (mt.Rows.Count > 0)
            {

                tb_Phone.Text = mt.Rows[0]["mr_phone"].ToString();
                tb_SeatCount.Text = mt.Rows[0]["mr_seatNum"].ToString();
                rbtl_HasProjector.Text = mt.Rows[0]["mr_projector"].ToString();
                rbtl_HasComputer.Text = mt.Rows[0]["mr_computer"].ToString();
                rbtl_HasEatWaterMa.Text = mt.Rows[0]["mr_waterDispenser"].ToString();
                rbtl_HasWhiteBoard.Text = mt.Rows[0]["mr_whiteboard"].ToString();
                rbtl_HaveConCall.Text = mt.Rows[0]["mr_conCell"].ToString();
                tb_otherDes.Text = mt.Rows[0]["mr_desc"].ToString();
                tb_WBPan.Text = mt.Rows[0]["mr_pen"].ToString();
            }
            else
            {
                tb_Phone.Text = "";
                tb_SeatCount.Text = "";
                rbtl_HasProjector.Text = "";
                rbtl_HasComputer.Text = "";
                rbtl_HasEatWaterMa.Text = "";
                rbtl_HasWhiteBoard.Text = "";
                rbtl_HaveConCall.Text = "";
                tb_otherDes.Text = "";
                tb_WBPan.Text = "";
            }

            tb_OrderDate.Text = Convert.ToDateTime(dt.Rows[0]["begintime"]).ToString("yyyy-MM-dd");
            tb_EndDate.Text = Convert.ToDateTime(dt.Rows[0]["endtime"]).ToString("yyyy-MM-dd");

            ddl_BeginHour.Items.Add(Convert.ToDateTime(dt.Rows[0]["begintime"]).ToString("HH"));
            ddl_BeginMin.SelectedValue = Convert.ToDateTime(dt.Rows[0]["begintime"]).ToString("mm");
            ddl_EndHour.Items.Add(Convert.ToDateTime(dt.Rows[0]["endtime"]).ToString("HH"));
            ddl_EndMin.SelectedValue = Convert.ToDateTime(dt.Rows[0]["endtime"]).ToString("mm");
        }
        if (Convert.ToBoolean(dt.Rows[0]["MR_IsFullDay"].ToString()))
        {
            rbtn_IsFullDay.SelectedValue = "true";
        }
        else 
        {
            rbtn_IsFullDay.SelectedValue = "false";
        }
    }
    /// <summary>
    ///  新申请页面绑定 （必須）
    /// </summary>
    public void AppBind()
    {
        SqlParameter[] param1 = new SqlParameter[4];
        param1[0] = new SqlParameter("@Train_EmpNo", Session["uID"].ToString());
        param1[1] = new SqlParameter("@Train_EmpName", Session["uName"].ToString());
        param1[2] = new SqlParameter("@Train_plantcode", Session["plantcode"].ToString());
        param1[3] = new SqlParameter("@retValue", SqlDbType.VarChar, 30);
        param1[3].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_mr_CreateAppNo", param1);
        tb_FormID.Text = param1[3].Value.ToString();//dt1.Rows[0]["train_AppNo"].ToString();
        this.tb_AppName.Text = Session["uName"].ToString();

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
        param[1] = new SqlParameter("@uID", Convert.ToString(Session["uID"]));
        param[2] = new SqlParameter("@DeptNo", Convert.ToInt32(Session["deptID"].ToString()));
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectDeptByUser", param).Tables[0];
        tb_DeptName.Text = dt.Rows[0]["name"].ToString();
        if (DateTime.Now.Hour > 18)
        {
            this.tb_OrderDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        }
        else
        {
            this.tb_OrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ddl_BeginHour.Text = DateTime.Now.AddHours(1).Hour.ToString().PadLeft(2, '0');
            ddl_EndHour.Text = DateTime.Now.AddHours(1).Hour.ToString().PadLeft(2, '0');
        }
        this.tb_EndDate.Text = tb_OrderDate.Text;
    }

    //公司信息绑定
    private void BindCompanyList()
    {
        string NeedCompanys = Session["uid"].ToString();//System.Configuration.ConfigurationManager.AppSettings["NeedCompanyCode"];
        if (string.IsNullOrEmpty(NeedCompanys))
        {
            this.Alert("Need Set Which Company Site Need To use this electric form!");
            return;
        }
        DataTable dt = mr.GetCompanyList(NeedCompanys);

        foreach (DataRow dr in dt.Rows)
        {
            rbl_CompanyCode.Items.Add(new ListItem(dr["plantcode"].ToString(), dr["plantid"].ToString()));
        }
        if (Session["plantcode"].ToString() == "1")
        {
            rbl_CompanyCode.Items.FindByText("SZX").Selected = true;
        }
        if (Session["plantcode"].ToString() == "2")
        {
            rbl_CompanyCode.Items.FindByText("ZQL").Selected = true;
        }
        if (Session["plantcode"].ToString() == "5")
        {
            rbl_CompanyCode.Items.FindByText("YQL").Selected = true;
        }
        if (Session["plantcode"].ToString() == "8")
        {
            rbl_CompanyCode.Items.FindByText("HQL").Selected = true;
        }
        rbl_CompanyCode.Enabled = false;
    }

    /// <summary>
    /// 绑定会议室编号
    /// </summary>
    private void BindMeetingRoom()
    {
        ddl_RoomID.Items.Clear();
        ddl_RoomID.Items.Add("--请选会议室--");
        try
        {
            String strSQL = "";
            strSQL = "sp_mr_selectroominfo";
            SqlParameter[] parma = new SqlParameter[2];
            parma[0] = new SqlParameter("@plants",rbl_CompanyCode.SelectedItem.Text.Trim());
            parma[1] = new SqlParameter("@roomid", "");
            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddl_RoomID.Items.Add(new ListItem(reader["mr_name"].ToString(), reader["mr_num"].ToString()));
                }
                reader.Close();
            }
        }
        catch
        {
            this.Alert("获取会议室信息失败");
        }
    }

    /// <summary>
    /// 绑定小时下拉框
    /// </summary>
    private void BindHoursList()
    {
        for (int i = 8; i <= 21; i++)
        {
            string hour = i.ToString().PadLeft(2, '0');
            ddl_BeginHour.Items.Add(hour);
            ddl_EndHour.Items.Add(hour);
        }
    }
    /// <summary>
    /// 绑定每个会议室的物品信息
    /// </summary>
    protected void ddl_RoomID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_CompanyCode.SelectedIndex == -1)
        {
            this.Alert("请选择会议室地点");
            return;
        }

        if (ddl_RoomID.SelectedIndex != -1 && ddl_RoomID.SelectedIndex != 0)
        {
            DataTable mt = mr.GetMeetingRoomGoodsList(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim());
            if (mt.Rows.Count > 0)
            {
                tb_Phone.Text = mt.Rows[0]["mr_phone"].ToString();
                tb_SeatCount.Text = mt.Rows[0]["mr_seatNum"].ToString();
                rbtl_HasProjector.Text = mt.Rows[0]["mr_projector"].ToString();
                rbtl_HasComputer.Text = mt.Rows[0]["mr_computer"].ToString();
                rbtl_HasEatWaterMa.Text = mt.Rows[0]["mr_waterDispenser"].ToString();
                rbtl_HasWhiteBoard.Text = mt.Rows[0]["mr_whiteboard"].ToString();
                rbtl_HaveConCall.Text = mt.Rows[0]["mr_conCell"].ToString();
                tb_otherDes.Text = mt.Rows[0]["mr_desc"].ToString();
                tb_WBPan.Text = mt.Rows[0]["mr_pen"].ToString();
            }
            else
            {
                tb_Phone.Text = "";
                tb_SeatCount.Text = "";
                rbtl_HasProjector.Text = "";
                rbtl_HasComputer.Text = "";
                rbtl_HasEatWaterMa.Text = "";
                rbtl_HasWhiteBoard.Text = "";
                rbtl_HaveConCall.Text = "";
                tb_otherDes.Text = "";
                tb_WBPan.Text = "";
            }
        }
    }

    /// <summary>
    /// 根据公司绑定会议室信息
    /// </summary>
    protected void rbl_CompanyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMeetingRoom();
        tb_Phone.Text = "";
        tb_SeatCount.Text = "";
        rbtl_HasProjector.Text = "";
        rbtl_HasComputer.Text = "";
        rbtl_HasEatWaterMa.Text = "";
        rbtl_HasWhiteBoard.Text = "";
        rbtl_HaveConCall.Text = "";
        tb_otherDes.Text = "";
        tb_WBPan.Text = "";
    }

    /// <summary>
    /// 保存当前表单预订的具体时间
    /// </summary>
    private bool SaveCurORDetail()
    {
        string FormId = tb_FormID.Text.Trim();
        DateTime BeginDate = DateTime.Parse(this.tb_OrderDate.Text);
        DateTime EndDate = DateTime.Parse(this.tb_EndDate.Text);
        string BeginTime = ddl_BeginHour.Text + ":" + ddl_BeginMin.Text + ":00";
        string EndTime = ddl_EndHour.Text + ":" + ddl_EndMin.Text + ":00";
        bool IsDenyAll = true;
        
        bool IsOk = IsDenyAll;//当全部控管（初始为可定）//当不控管则（初始为不可定）

        DateTime RBeginTime = DateTime.Now;
        DateTime REndTime = DateTime.Now;
        if (this.rbtn_IsFullDay.SelectedValue.ToUpper() == "TRUE")
        {
           RBeginTime = DateTime.Parse(BeginDate.ToString("yyyy-MM-dd") + " " + BeginTime);
           REndTime = DateTime.Parse(EndDate.ToString("yyyy-MM-dd") + " " + EndTime);
           int count1 = mr.ImportRmDetailsData(FormId, RBeginTime, REndTime, Convert.ToBoolean(rbtn_IsFullDay.SelectedValue), Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());
           if (count1 == 1)
           {
               return true;
           }
           else
           {
               return false;
           }
        }
        else if (this.rbtn_IsFullDay.SelectedValue.ToUpper() == "FALSE")
        {
            while (BeginDate <= EndDate)
            {
                RBeginTime = DateTime.Parse(BeginDate.ToString("yyyy-MM-dd") + " " + BeginTime);
                REndTime = DateTime.Parse(BeginDate.ToString("yyyy-MM-dd") + " " + EndTime);
                int count1 = mr.ImportRmDetailsData(FormId, RBeginTime, REndTime, Convert.ToBoolean(rbtn_IsFullDay.SelectedValue), Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());
                if (count1 == 0)
                {
                    return false;
                }
                BeginDate = BeginDate.AddDays(1);
            }
        }
        return IsOk;
    }

    //保存前做判断
    protected string CheckIsOk()
    {
        string err = "";
        if (string.IsNullOrEmpty(tb_AppExtNo.Text))
        {
            err += "联系方式不能为空!\\n";
        }
        if (ddl_RoomID.SelectedIndex == 0)
        {
            err += "请选择会议室!\\n";
        }
        if (string.IsNullOrEmpty(tb_MeettingMemberNum.Text))
        {
            err += "使用人数不能为空!\\n";
        }
        if (string.IsNullOrEmpty(tb_Reason.Text))
        {
            err += "事由不能为空!\\n";
        }
        if (string.IsNullOrEmpty(tb_OrderDate.Text))
        {
            err += "开始时间不能为空!\\n";
        }
        if (string.IsNullOrEmpty(tb_EndDate.Text))
        {
            err += "结束时间不能为空!\\n";
        }
        DateTime BeginDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        try
        {
            BeginDate = DateTime.Parse(this.tb_OrderDate.Text);
        }
        catch
        {
            err += "开始时间格式不正确!\\n";
        }
        try
        {
            EndDate = DateTime.Parse(this.tb_EndDate.Text);
        }
        catch
        {
            err += "结束时间格式不正确!\\n";
        }
        string BeginTime = ddl_BeginHour.Text + ":" + ddl_BeginMin.Text + ":00";
        string EndTime = ddl_EndHour.Text + ":" + ddl_EndMin.Text + ":00";

        DateTime RBeginTime = DateTime.Now;
        DateTime REndTime = DateTime.Now;
        if (this.rbtn_IsFullDay.SelectedValue.ToUpper() == "TRUE")
        {
            RBeginTime = DateTime.Parse(BeginDate.ToString("yyyy-MM-dd") + " " + BeginTime);
            REndTime = DateTime.Parse(EndDate.ToString("yyyy-MM-dd") + " " + EndTime);
            if (RBeginTime >= REndTime)
            {
                err += "开始时间不能大于等于结束时间!\\n";
            }
            DataTable dt = mr.CheckHaveReport(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim(), RBeginTime, REndTime, Session["uID"].ToString());
            if (dt.Rows.Count > 0)
            {
                err += "此时间段内已有申请!\\n";
            }
            if (BeginDate != EndDate)
            {
                err += "连续时间不能跨天申请!\\n";
            }
        }
        else if (this.rbtn_IsFullDay.SelectedValue.ToUpper() == "FALSE")
        {
            while (BeginDate <= EndDate)
            {
                RBeginTime = DateTime.Parse(BeginDate.ToString("yyyy-MM-dd") + " " + BeginTime);
                REndTime = DateTime.Parse(BeginDate.ToString("yyyy-MM-dd") + " " + EndTime);
                if (RBeginTime >= REndTime)
                {
                    err += "开始时间不能大于等于结束时间!\\n";
                }
                DataTable dt = mr.CheckHaveReport(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim(), RBeginTime, REndTime,Session["uID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    err += "此时间段内已有申请!\\n";
                }
                BeginDate = BeginDate.AddDays(1);
            }
        }
        return err;
    }

    protected void btn_App_Click(object sender, EventArgs e)
    {
        string err = CheckIsOk();
        if (!string.IsNullOrEmpty(err))
        {
            this.Alert(err);
            return;
        }
        string FormId = tb_FormID.Text.Trim();
        string DeptNo = Session["DeptID"].ToString();
        string DeptMame = tb_DeptName.Text.Trim();
        string AppEno = Session["uID"].ToString();
        string AppName = tb_AppName.Text.Trim();
        string AppExtNo = tb_AppExtNo.Text.Trim();
        string CompanyCode = rbl_CompanyCode.SelectedItem.Text.Trim();
        string MeettingMemberNum = tb_MeettingMemberNum.Text.Trim();
        string Reason = tb_Reason.Text.Trim();

        string BorrowThings = tb_BorrowThings.Text.Trim();
        string RoomID = ddl_RoomID.SelectedValue.ToString().Trim();
        string otherDes = tb_otherDes.Text.Trim();

        int count = mr.ImportRmData(FormId, DeptNo, DeptMame, (DateTime.Now).ToString(), AppEno, AppName, AppExtNo, CompanyCode, MeettingMemberNum, Reason, BorrowThings, RoomID, otherDes);
        if (!SaveCurORDetail())
        {
            int count1 = mr.DeleteRmDataByErr(FormId);
            if (count1 < 0)
            {
                this.Alert("保存失败，请联系管理员!");
                return;
            }
            this.Alert("申请保存失败，请重试!");
            return;
        }
        
        this.Alert("会议室申请提交成功!");
        SApplyemail(FormId);
        AppBind();
        Response.Redirect("mr_RoomView.aspx");
    }

    private void SApplyemail(string _no)
    {
        DataTable dt = mr.GetApplyMeetingRoomEmail(_no);
        string mailto = dt.Rows[0]["email"].ToString();
        StringBuilder sb = new StringBuilder();
        string mailSubject = "强凌 - " + dt.Rows[0]["mr_name"].ToString() + "-会议室申请通知";
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {                
                sb.Append("<html>");
                sb.Append("<form>");
                sb.Append("<br>");
                sb.Append("ALL," + "<br>");
                sb.Append("    下列会议室我已提交申请! 详细信息如下。" + "<br>");
                sb.Append("<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "申请单号：" + _no + " ，" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "公司：" + dt.Rows[0]["MR_CompanyCode"].ToString() + " ，" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "会议室：" + dt.Rows[0]["mr_name"].ToString() + " ，" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "时间段：" + dt.Rows[0]["begindate"].ToString() + "~~" + dt.Rows[0]["enddate"].ToString() + "<br>");
                sb.Append("    请尽快批准，谢谢。" + "<br>");
                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");
            }
                                        
        }
        DataTable d = getHaveAuthority();
        if (d.Rows.Count > 0)
        {
            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (!this.SendEmail(mailto, d.Rows[i]["email"].ToString(), "", mailSubject, sb.ToString()))
                {
                    this.Alert("邮件发送失败！");
                    return;
                }
            }
        }          
    }

    private DataTable getHaveAuthority()
    {
        string str = "sp_mr_selectHaveAuthorityEmail";

        return SqlHelper.ExecuteDataset(adam.dsn0(),CommandType.StoredProcedure,str).Tables[0];

    }
    protected void back(object sender, EventArgs e)
    {
        
            Response.Redirect("mr_roomView.aspx");               
    }
}
