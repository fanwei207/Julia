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
using RMInfo;
using System.Text;
using System.IO;
using System.Xml;


public partial class OrderRoom : BasePage
{
    RM rm = new RM();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AppBind();
            BindHoursList();
            BindCompanyList();
            BindMeetingRoom();
            BindNearTwoOrderDetail();
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

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
        param[1] = new SqlParameter("@DeptNo", Convert.ToInt32(Session["deptID"].ToString()));
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectDept", param).Tables[0];
        tb_DeptName.Text = dt.Rows[0]["name"].ToString();
        if (DateTime.Now.Hour > 18)
        {
            this.tb_OrderDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        }
        else
        {
            this.tb_OrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ddl_BeginHour.Text = DateTime.Now.AddHours(1).Hour.ToString().PadLeft(2, '0');
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
        DataTable dt = rm.GetCompanyList(NeedCompanys);

        foreach (DataRow dr in dt.Rows)
        {
            rbl_CompanyCode.Items.Add(new ListItem(dr["plantcode"].ToString(), dr["plantid"].ToString()));
        }
        rbl_CompanyCode.SelectedIndex = 0;
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

        if (ddl_RoomID.SelectedIndex != -1)
        {
            DataTable mt = rm.GetMeetingRoomGoodsList(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim());
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
                bindCurRoomOrderList();
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
        bindCurRoomOrderList();
        Literal1.Text = "";
        Literal1.Visible = false;
    }
    /// <summary>
    /// 绑定当前会议室被预订情况表格
    /// </summary>
    private void bindCurRoomOrderList()
    {
        DataTable dt = rm.GetOrderMRAndDetails(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim());

        this.gdv_MroomOrderList.DataSource = dt;
        this.gdv_MroomOrderList.DataBind();
        if (this.gdv_MroomOrderList.Rows.Count <= 0)
        {
            this.pn_ShowOders.Visible = false;
        }
        else
        {
            this.pn_ShowOders.Visible = true;
        }
    }
    /// <summary>
    /// 根据日期生成会议室预定记录HTML表格
    /// </summary>
    /// <param name="dt1"></param>
    /// <returns></returns>
    public string GetConcallOrderHTML(DateTime dt1)
    {
        DateTime begin = new DateTime(2013, 1, 23, 8, 0, 0);
        DateTime end = new DateTime(2013, 1, 23, 17, 0, 0);
        int colunit = 30;
        int colcount = (int)(end - begin).TotalMinutes / colunit;
        DataTable GetMeetingRoom = rm.GetMeetingRoomNO(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim());//rm.GetOrderMRAndDetailsByDay(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim(), Convert.ToDateTime(rbtn.SelectedValue));
        if (GetMeetingRoom == null || GetMeetingRoom.Rows == null || GetMeetingRoom.Rows.Count == 0) return "";
        StringBuilder sbHtml = new StringBuilder();
        sbHtml.AppendFormat("<table width='100%'><tr><td width='50'></td><td colspan='18'></td></tr><tr><td colspan=\"19\" style=\"text-align:center;font-size: 15.0pt;\">{0}日会议室预订记录</td></tr>", dt1.Day);
        sbHtml.Append("<tr style='background-color:#0099FF'>");
        sbHtml.Append("<td>会议室</td>");
        for (int i = 0; i < (end.Hour - begin.Hour); i++)
        {

            sbHtml.AppendFormat("<td colspan='2' style=\"text-align:center;\">{0}</td>  ", begin.AddMinutes(i * 60).ToString("HH:mm"));

        }
        //for (int i = 0; i < (end.Hour - begin.Hour) * 2 + 1; i++)
        //{
        //    sbHtml.AppendFormat("<td>{0}</td>", begin.AddMinutes(i * 30).ToString("HH:mm"));//.Substring(11, 5));
        //}
        sbHtml.Append("</tr>");

        for (int i = 0; i < GetMeetingRoom.Rows.Count; i++)
        {
            sbHtml.Append("<tr>");
            sbHtml.AppendFormat("<td>{0}</td>", GetMeetingRoom.Rows[i]["MR_num"].ToString());
            DataTable dt = rm.GetOrderMRAndDetailsByDay(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim(), Convert.ToDateTime(rbtn.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DateTime beginhour = DateTime.Parse(dt.Rows[j]["MR_BeginTime"].ToString());
                    DateTime endhour = DateTime.Parse(dt.Rows[j]["MR_EndTime"].ToString());

                    int tdbegin = beginhour.Hour - begin.Hour;//7*2
                    int tdbeginMinute = (beginhour.Minute - begin.Minute) / 30;//0
                    int tdend = endhour.Hour - begin.Hour;//(10+1)*2
                    int tdendMinute = (endhour.Minute - begin.Minute) / 30;//0

                    int startcolnum = tdbegin * 2 + tdbeginMinute;
                    int endcolnum = tdend * 2 + tdendMinute;

                    if (j == 0)
                    {
                        for (int k = 0; k <= colcount - 1; k++)
                        {
                            if (startcolnum > k || endcolnum - 1 < k)
                            {
                                sbHtml.AppendFormat("<td>[" + i + ":" + k + "]</td>");//[" + i + ":" + k + "]
                            }
                            else
                            {
                                sbHtml.AppendFormat("<td style='background-color:#FF99CC'></td>");
                            }
                        }
                    }
                    else
                    {
                        for (int k = 0; k <= colcount - 1; k++)
                        {
                            if (startcolnum > k || endcolnum - 1 < k)
                            {
                            }
                            else
                            {
                                sbHtml.Replace("<td>[" + i + ":" + k + "]</td>", "<td style='background-color:#FF99CC'></td>");
                            }
                        }
                    }
                }
            }
            else
            {
                for (int m = 0; m < colcount + 1 - 1; m++)
                {
                    sbHtml.Append("<td ></td>");
                }
            }
            sbHtml.Append("</tr>");
        }
        for (int i = 0; i < GetMeetingRoom.Rows.Count; i++)
        {
            for (int j = 0; j <= colcount - 1; j++)
            {
                sbHtml.Replace("<td>[" + i + ":" + j + "]</td>", "<td></td>");
            }
        }
        sbHtml.Append("</table>");
        return sbHtml.ToString();
    }
    /// <summary>
    /// 綁定最大可預定天數
    /// </summary>
    private void BindNearTwoOrderDetail()
    {
        int EffectDay = 7;
        int.TryParse(System.Configuration.ConfigurationManager.AppSettings["CanOrderDay"], out EffectDay);
        this.lit_ShowInfos.Text = string.Format("查看最近{0}个工作日会议号码预订信息：", EffectDay);
        DateTime dtnext = DateTime.Now;
        for (int a = 0; a < EffectDay; a++)
        {
            rbtn.Items.Add(dtnext.AddDays(a).ToString("yyyy-MM-dd"));
        }
    }

    /// <summary>
    /// 绑定所选日期的会议室预订记录HTML表格
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(rbtn.SelectedValue))
        {
            this.Literal1.Text = GetConcallOrderHTML(Convert.ToDateTime(rbtn.SelectedValue));
        }    
        Literal1.Visible = true;
    }

    /// <summary>
    /// 根据公司绑定会议室信息
    /// </summary>
    protected void rbl_CompanyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMeetingRoom();
        rbtn_SelectedIndexChanged(null, EventArgs.Empty);
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
           int count1 = rm.ImportRmDetailsData(FormId, RBeginTime, REndTime, Convert.ToBoolean(rbtn_IsFullDay.SelectedValue), Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());
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
                int count1 = rm.ImportRmDetailsData(FormId, RBeginTime, REndTime, Convert.ToBoolean(rbtn_IsFullDay.SelectedValue), Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());
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

        DateTime BeginDate = DateTime.Parse(this.tb_OrderDate.Text);
        DateTime EndDate = DateTime.Parse(this.tb_EndDate.Text);
        string BeginTime = ddl_BeginHour.Text + ":" + ddl_BeginMin.Text + ":00";
        string EndTime = ddl_EndHour.Text + ":" + ddl_EndMin.Text + ":00";

        DateTime RBeginTime = DateTime.Now;
        DateTime REndTime = DateTime.Now;
        if (this.rbtn_IsFullDay.SelectedValue.ToUpper() == "TRUE")
        {
            RBeginTime = DateTime.Parse(BeginDate.ToString("yyyy-MM-dd") + " " + BeginTime);
            REndTime = DateTime.Parse(EndDate.ToString("yyyy-MM-dd") + " " + EndTime);
            if (RBeginTime > REndTime)
            {
                err += "开始时间不能大于等于结束时间!\\n";
            }
            DataTable dt = rm.CheckHaveReport(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim(), RBeginTime, REndTime);
            if (dt.Rows.Count > 0)
            {
                err += "此时间段内已有申请!\\n";
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
                DataTable dt = rm.CheckHaveReport(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim(), RBeginTime, REndTime);
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

        int count = rm.ImportRmData(FormId, DeptNo, DeptMame, (DateTime.Now).ToString(), AppEno, AppName, AppExtNo, CompanyCode, MeettingMemberNum, Reason, BorrowThings, RoomID, otherDes);
        if (!SaveCurORDetail())
        {
            int count1 = rm.DeleteRmData(FormId);
            if (count1 < 0)
            {
                this.Alert("保存失败，请联系管理员!");
                return;
            }
            this.Alert("申请保存失败，请重试!");
            return;
        }
        this.Alert("会议室申请提交成功!");
        AppBind();
        bindCurRoomOrderList();
    }
}
