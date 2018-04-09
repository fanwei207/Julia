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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


public partial class wl_calendar : BasePage
{
    adamClass chk = new adamClass();
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_detail.Visible = this.Security["7000311"].isValid;
            btn_detail.Enabled = this.Security["7000311"].isValid;

            if (Request["yy"] == null)
                txb_year.Text = string.Format("{0:yyyy}", DateTime.Today);
            else
                txb_year.Text = Request["yy"];

            if (Request["mm"] == null)
                txb_month.Text = string.Format("{0:MM}", DateTime.Today);
            else
                txb_month.Text = Request["mm"];

            if (Convert.ToInt16(Session["uRole"]) != 1)
            {
                ddl_site.Enabled = false;
            }
            ddl_site.SelectedValue = Session["PlantCode"].ToString();


            LoadCC();
            if (Request["cc"] != null)
                ddl_cc.SelectedValue = Request["cc"];

            BindData();

            btn_list.Attributes.Add("onclick", "return confirm('刷新将重新计算公司成本中心年月的考勤，可能需较长的时间，是否继续？');");
        }
    }


    protected void LoadCC()
    {
        ddl_cc.Items.Clear();

        ListItem ls;
        string StrSql;
        SqlDataReader reader;

        StrSql = "select distinct a.cc,a.ccname from tcpc0.dbo.hr_Attendance_CC a ";
        if (Convert.ToInt32(Session["uRole"]) != 1)
        {
            StrSql += " INNER JOIN tcpc0.dbo.wo_cc_permission cp ON cp.perm_userid='" + Session["uID"] + "' AND cp.perm_ccid=a.cc ";
        }
        StrSql += " where a.plantID='" + ddl_site.SelectedValue + "'";
        reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql);
        while (reader.Read())
        {
            ls = new ListItem();
            ls.Value = reader[0].ToString().Trim();
            ls.Text = reader[1].ToString().Trim();
            ddl_cc.Items.Add(ls);
        }
        reader.Close();
        reader.Dispose();

        ls = new ListItem();
        ls.Text = "--";
        ls.Value = "0";
        ddl_cc.Items.Insert(0, ls);
        ddl_cc.SelectedIndex = 0;

    }

    protected void LoadData()
    {
        string StrSql = "";

        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_hr_clearCalendar", new SqlParameter("@plantid", ddl_cc.SelectedValue));

        if (txb_userno.Text.Trim().Length > 0)
        {
            StrSql = "Delete from tcpc0.dbo.hr_Attendance_calendar where plantID='" + ddl_site.SelectedValue + "' and cc='" + ddl_cc.SelectedValue + "' and uyear=" + Convert.ToInt32(txb_year.Text) + " and umonth=" + Convert.ToInt32(txb_month.Text) + " and userno='" + txb_userno.Text.Trim() + "' ";
        }
        else
        {
            StrSql = "Delete from tcpc0.dbo.hr_Attendance_calendar where plantID='" + ddl_site.SelectedValue + "' and cc='" + ddl_cc.SelectedValue + "' and uyear=" + Convert.ToInt32(txb_year.Text) + " and umonth=" + Convert.ToInt32(txb_month.Text);
        }
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);

        int i = 0;
        if (txb_year.Text.Trim().Length == 0)
            txb_year.Text = string.Format("{0:yyyy}", DateTime.Today);
        if (txb_month.Text.Trim().Length == 0)
            txb_month.Text = string.Format("{0:MM}", DateTime.Today);

        DateTime dateto;
        DateTime datefrom = Convert.ToDateTime(txb_year.Text + "-" + txb_month.Text + "-01");
        if (Convert.ToInt16(txb_month.Text) == 12)
        {
            dateto = Convert.ToDateTime(Convert.ToString(Convert.ToInt16(txb_year.Text) + 1) + "-01-01");
        }
        else
        {
            dateto = Convert.ToDateTime(txb_year.Text + "-" + Convert.ToString(Convert.ToInt16(txb_month.Text) + 1) + "-01");
        }

        //考勤工时
        int st = 0;
        DateTime dd1 = Convert.ToDateTime("2000-01-01");
        DateTime dd2 = Convert.ToDateTime("2000-01-01");
        Int32 uid = -1;
        string uno = string.Empty;

        string _userid = "";
        string _userno = "";
        string _username = "";
        string _usertype = "";


        StrSql = " select a.AttendanceUserNo,a.checktype";

        if (Convert.ToInt16(ddl_site.SelectedValue) == 1)
        {
            StrSql += " , checkTime = Case When a.CheckType = 'I' and Cast((Convert(varchar(10), a.CheckTime, 120) + ' ' + case when us.userTypeName = N'A' then '07:00' else us.atWork end) As Datetime) > a.CheckTime Then Convert(varchar(10), a.CheckTime, 120) + ' ' + case when us.userTypeName = N'A' then '07:00' else us.atWork end Else a.checkTime End ";
            StrSql += " , a.AttendanceUserID,a.AttendanceUserCode,a.AttendanceUserName,a.AttendanceUserType ";
            StrSql += " from tcpc0.dbo.hr_AttendanceInfo a ";
            StrSql += " Left Join tcpc" + ddl_site.SelectedValue + ".dbo.UserSchedule us On us.userID = a.AttendanceUserID ";
        }
        else
        {
            StrSql += " , a.checkTime, a.AttendanceUserID, a.AttendanceUserCode, a.AttendanceUserName, a.AttendanceUserType ";
            StrSql += " from tcpc0.dbo.hr_AttendanceInfo a ";
        }

        StrSql += " where a.checkTime>='" + datefrom + "' and a.checkTime<= DateAdd(dd,1,'" + dateto + "') ";
        if (ddl_site.SelectedIndex > 0)
        {
            StrSql += " and a.plantID='" + ddl_site.SelectedValue + "' ";
        }
        if (ddl_cc.SelectedIndex > 0)
        {
            StrSql += " and a.AttendanceUserCenter='" + ddl_cc.SelectedValue + "' ";
        }
        if (txb_userno.Text.Trim().Length > 0)
        {
            StrSql += " and a.AttendanceUserCode='" + txb_userno.Text.Trim() + "' ";
        }
        StrSql += " and isnull(a.isDisable,0)=0 And AttendanceUserID Is Not Null ";
        StrSql += " order by a.plantID,a.AttendanceUserID,a.checkTime ";

        //Response.Write(StrSql);
        //return;

        decimal hfdays = 4;
        //decimal hfday = Convert.ToInt32(Session["plantCode"]) == 1 ? 1.0M:0.5M;
        decimal hfday = Convert.ToInt32(ddl_site.SelectedValue) <= 2 ? 1.0M : 0.5M;
        decimal alldays = 12;
        //decimal allday = Convert.ToInt32(Session["plantCode"]) == 1 ? 1.5M : 1.0M;
        decimal allday = Convert.ToInt32(ddl_site.SelectedValue) <= 2 ? 1.5M : 1.0M;
        decimal timeinte = 0;

        ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, StrSql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                //换人
                if (uid != -1 && uid != Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]))
                {
                    if (dd1 != Convert.ToDateTime("2000-01-01") && dd2 != Convert.ToDateTime("2000-01-01"))
                    {
                        timeinte = Convert.ToDecimal((dd2 - dd1).TotalHours);
                        if (timeinte > 24)
                        {
                            //timeinte = 10;
                            timeinte = 0;
                        }
                        else if (timeinte >= alldays)
                        {
                            timeinte = timeinte - allday;
                        }
                        else if (timeinte >= hfdays && timeinte < alldays)
                        {
                            if (Convert.ToInt32(Session["plantCode"]) == 1 && timeinte >= hfdays + 1)
                            {
                                timeinte = timeinte - hfday;
                            }
                            if (Convert.ToInt32(Session["plantCode"]) != 1 && timeinte >= hfdays)
                            {
                                timeinte = timeinte - hfday;
                            }
                        }

                        if (timeinte > 0)
                        {
                            StrSql = "Insert into tcpc0.dbo.hr_Attendance_calendar(plantid,cc,userid,userno,username,starttime,endtime,totalhr,fingerprint,createdby,createddate,usertype,uyear,umonth,utype) ";
                            StrSql += " values('" + ddl_site.SelectedValue + "','" + ddl_cc.SelectedValue + "','" + _userid + "' ";
                            StrSql += " ,'" + _userno + "' ";
                            StrSql += " ,N'" + _username + "' ";
                            StrSql += " ,'" + dd1 + "','" + dd2 + "','" + timeinte.ToString() + "' ";
                            if (uno.Length != 0) StrSql += " ,'" + uno + "'";
                            else StrSql += " , null ";
                            StrSql += ", '" + Session["uID"] + "',getdate(),'" + _usertype + "'," + dd1.Year.ToString() + "," + dd1.Month.ToString() + ",1)";
                            //StrSql += " ,'" + uno  + "','" + Session["uID"] + "',getdate(),'" + _usertype  + "'," + Convert.ToInt32(txb_year.Text) + "," + Convert.ToInt32(txb_month.Text) + ",1)";

                            //if (Convert.ToString(ds.Tables[0].Rows[i].ItemArray[3])=="4559")
                            //Response.Write(StrSql + "--" + uno.ToString());
                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);
                        }

                    }
                    st = 0;
                    dd1 = Convert.ToDateTime("2000-01-01");
                    dd2 = Convert.ToDateTime("2000-01-01");
                    uid = -1;
                }

                if (ds.Tables[0].Rows[i].ItemArray[1].ToString() == "I")
                {

                    if (st == 0)
                    {
                        st = 1;
                        dd1 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]);
                        dd2 = Convert.ToDateTime("2000-01-01");
                    }
                    else if (st == 1)
                    { 
                        st = 1;
                        dd1 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]);
                        dd2 = Convert.ToDateTime("2000-01-01");
                    }
                    else if (st == 2)
                    {
                        if (dd1 != Convert.ToDateTime("2000-01-01") && dd2 != Convert.ToDateTime("2000-01-01"))
                        {
                            timeinte = Convert.ToDecimal((dd2 - dd1).TotalHours);
                            if (timeinte > 24)
                            {
                                //timeinte = 10;
                                timeinte = 0;
                            }
                            else if (timeinte >= alldays)
                            {
                                timeinte = timeinte - allday;
                            }
                            else if (timeinte >= hfdays && timeinte < alldays)
                            {
                                if (Convert.ToInt32(Session["plantCode"]) == 1 && timeinte >= hfdays + 1)
                                {
                                    timeinte = timeinte - hfday;
                                }
                                if (Convert.ToInt32(Session["plantCode"]) != 1 && timeinte >= hfdays)
                                {
                                    timeinte = timeinte - hfday;
                                }
                            }

                            if (timeinte > 0)
                            {
                                StrSql = " Insert into tcpc0.dbo.hr_Attendance_calendar(plantid,cc,userid,userno,username,starttime,endtime,totalhr,fingerprint,createdby,createddate,usertype,uyear,umonth,utype) ";
                                StrSql += " values('" + ddl_site.SelectedValue + "','" + ddl_cc.SelectedValue + "','" + _userid + "' ";
                                StrSql += " ,'" + _userno + "', N'" + _username + "' ,'" + dd1 + "','" + dd2 + "','" + timeinte.ToString() + "' ";
                                if (uno.Length != 0) StrSql += " ,'" + uno + "'";
                                else StrSql += " , null ";
                                StrSql += " ,'" + Session["uID"] + "',getdate(),'" + _usertype + "'," + dd1.Year.ToString() + "," + dd1.Month.ToString() + ",2)";

                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);
                            }
                        }
                        st = 1;
                        dd1 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]);
                        dd2 = Convert.ToDateTime("2000-01-01");
                    }
                }
                else if (ds.Tables[0].Rows[i].ItemArray[1].ToString() == "O")
                {
                    if (st == 0)
                    {
                        //先有Out，放弃。 
                    }
                    else if (st == 1)
                    {
                        st = 2;
                        dd2 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]);
                    }
                    else if (st == 2)
                    {
                        st = 2;
                        //dd2 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]);
                    }
                }

                uno = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                uid = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]);
                _userid = Convert.ToString(ds.Tables[0].Rows[i].ItemArray[3]);
                _userno = Convert.ToString(ds.Tables[0].Rows[i].ItemArray[4]);
                _username = Convert.ToString(ds.Tables[0].Rows[i].ItemArray[5]);
                _usertype = Convert.ToString(ds.Tables[0].Rows[i].ItemArray[6]);
            }

            if (dd1 != Convert.ToDateTime("2000-01-01") && dd2 != Convert.ToDateTime("2000-01-01"))
            {
                timeinte = Convert.ToDecimal((dd2 - dd1).TotalHours);
                if (timeinte > 24)
                {
                    //timeinte = 10;
                    timeinte = 0;
                }
                else if (timeinte >= alldays)
                {
                    timeinte = timeinte - allday;
                }
                else if (timeinte >= hfdays && timeinte < alldays)
                {
                    if (Convert.ToInt32(Session["plantCode"]) == 1 && timeinte >= hfdays + 1)
                    {
                        timeinte = timeinte - hfday;
                    }
                    if (Convert.ToInt32(Session["plantCode"]) != 1 && timeinte >= hfdays)
                    {
                        timeinte = timeinte - hfday;
                    }
                }

                if (timeinte > 0)
                {
                    StrSql = "Insert into tcpc0.dbo.hr_Attendance_calendar(plantid,cc,userid,userno,username,starttime,endtime,totalhr,fingerprint,createdby,createddate,usertype,uyear,umonth,utype) ";
                    StrSql += " values('" + ddl_site.SelectedValue + "','" + ddl_cc.SelectedValue + "','" + _userid + "' ";
                    StrSql += " ,'" + _userno + "' ";
                    StrSql += " ,N'" + _username + "' ";
                    StrSql += " ,'" + dd1 + "','" + dd2 + "','" + timeinte.ToString() + "' ";
                    if (uno.Length != 0) StrSql += " ,'" + uno + "'";
                    else StrSql += ", null ";
                    StrSql += " ,'" + Session["uID"] + "',getdate(),'" + _usertype + "'," + dd1.Year.ToString() + "," + dd1.Month.ToString() + ",3)";

                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);
                }
            }
        }

        string strSql = "sp_hr_ReCreateCalendar";
        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@plant", ddl_site.SelectedValue);
        sqlParam[1] = new SqlParameter("@dd", datefrom);

        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

        ds.Reset();
    }

    protected void BindData()
    {
        string StrSql = "";

        if (chk_sum.Checked == false)
        {

            StrSql = " select a.id,a.userid,a.userno,a.username,a.fingerprint,a.starttime,a.endtime,a.totalhr,a.createddate,case when a.usertype=394 then N'A类' when a.usertype=395 then N'B类' when a.usertype=396 then N'C类' when a.usertype=397 then N'D类' when a.usertype=398 then N'E类' end ,ISNULL(q.night,''), createdBy, a.cc";

            if (!chk_multi.Checked)
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar a ";
            else
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar_ori a ";

            StrSql += " LEFT OUTER JOIN (SELECT userID, ";
            StrSql += "                 CASE WHEN (totalhr>=12 AND h1<=22 AND h2>=30) THEN N'全夜' ";
            StrSql += "                 ELSE ";
            StrSql += "                 CASE WHEN (totalhr>=8 AND ((h1<=3 AND h1>=0) or h2 >24 )) THEN N'夜班' ";
            StrSql += "                 ELSE ";
            StrSql += "                 CASE WHEN (totalhr>=8 AND ( h2 <=24 And  h2 >=22)) THEN N'中班' ";
            StrSql += "                 ELSE NULL  ";
            StrSql += "                 END  ";
            StrSql += "                 END   ";
            StrSql += "                 END As night,starttime    ";
            StrSql += "                 FROM  ";
            StrSql += "                 (SELECT  userID,totalhr,datepart(hh,starttime)+ ROUND(datepart(mi,starttime)/60.0,2) As h1, ";
            StrSql += "                  CASE WHEN datediff(day,starttime,endtime)<> 0  THEN datepart(hh,endtime) +ROUND(datepart(mi,endtime)/60.0,2)+ 24 ELSE datepart(hh,endtime) + Round(datepart(mi,endtime)/60.0,2) END  As h2, ";
            //StrSql += "                  CONVERT(varchar(10), starttime, 120) As workday  ";
            StrSql += "                 starttime ";

            if (!chk_multi.Checked)
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar a ";
            else
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar_ori a ";

            StrSql += "                 WHERE uyear='" + txb_year.Text + "' AND umonth='" + txb_month.Text + "' AND plantid='" + ddl_site.SelectedValue + "' ";
            if (ddl_cc.SelectedIndex > 0)
            {
                StrSql += "AND cc ='" + ddl_cc.SelectedValue + "' ";
            }

            if (txb_day.Text.Trim().Length > 0)
            {
                StrSql += " and day(starttime)='" + txb_day.Text + "'";
            }
            if (txb_userno.Text.Trim().Length > 0)
            {
                StrSql += " and userno='" + txb_userno.Text.Trim() + "'";
            }
            if (ddl_type.SelectedIndex > 0)
            {
                StrSql += " and usertype='" + ddl_type.SelectedValue + "'";
            }
            StrSql += "           ) As qq   )   as q oN q.userID = a.userID And  q.starttime = a.starttime ";

            StrSql += " where a.plantID='" + ddl_site.SelectedValue + "' ";
            if (ddl_cc.SelectedIndex > 0)
            {
                StrSql += "and a.cc='" + ddl_cc.SelectedValue + "' ";
            }
            StrSql += " and year(a.starttime)='" + txb_year.Text + "' and Month(a.starttime)='" + txb_month.Text + "' ";
            if (txb_day.Text.Trim().Length > 0)
            {
                StrSql += " and day(a.starttime)='" + txb_day.Text + "'";
            }
            if (txb_userno.Text.Trim().Length > 0)
            {
                StrSql += " and a.userno='" + txb_userno.Text.Trim() + "'";
            }
            if (ddl_type.SelectedIndex > 0)
            {
                StrSql += " and a.usertype='" + ddl_type.SelectedValue + "'";
            }
            StrSql += " and a.userno<>'' and a.totalhr>0 ";
            StrSql += " order by a.userno,a.starttime ";

        }
        else
        {
            StrSql = " select 0,a.userid,a.userno,a.username,'','','',a.totalhr,'',a.aType,ISNULL(n.night,''), createdBy ,a.cc FROM ";
            StrSql += " (select a.userid,a.userno,a.username,a.usertype,sum(a.totalhr) As totalhr,case when a.usertype=394 then N'A类' when a.usertype=395 then N'B类' when a.usertype=396 then N'C类' when a.usertype=397 then N'D类' when a.usertype=398 then N'E类' end As aType , createdBy ,a.cc";

            if (!chk_multi.Checked)
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar a ";
            else
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar_ori a ";

            StrSql += " where a.plantID='" + ddl_site.SelectedValue + "'";
            if (ddl_cc.SelectedIndex > 0)
            {
                StrSql += "and a.cc='" + ddl_cc.SelectedValue + "' ";
            }
            StrSql += " and year(a.starttime)='" + txb_year.Text + "' and Month(a.starttime)='" + txb_month.Text + "' ";
            if (txb_day.Text.Trim().Length > 0)
            {
                StrSql += " and day(a.starttime)='" + txb_day.Text + "'";
            }
            if (txb_userno.Text.Trim().Length > 0)
            {
                StrSql += " and a.userno='" + txb_userno.Text.Trim() + "'";
            }
            if (ddl_type.SelectedIndex > 0)
            {
                StrSql += " and a.usertype='" + ddl_type.SelectedValue + "'";
            }
            StrSql += " and a.userno<>'' and a.totalhr>0 ";
            StrSql += " group by a.userid,a.userno,a.username,a.usertype,a.cc,createdby ) a ";

            StrSql += " LEFT OUTER JOIN ( SELECT userID ,usertype,CAST(SUM(CAST(SubString(night,1,1) As Int)) As Varchar(2))+'@'+CAST(SUM(CAST(SubString(night,2,1) As Int)) As Varchar(2)) +'@'+ CAST(SUM(CAST(SubString(night,3,1) As Int)) As Varchar(2)) As night ";
            StrSql += "                  FROM ( SELECT userID, usertype,CASE WHEN Max(CAST(qq.night As int)) =0  THEN '000' ELSE CASE WHEN Max(CAST(qq.night As int)) =1 THEN '001' ELSE CASE WHEN Max(CAST(qq.night As int)) =10 THEN '010' ELSE '100' END END END As night  ";
            StrSql += "                         FROM( SELECT userID,usertype,CASE WHEN (totalhr>=12 AND h1<=22 AND h2>=30) THEN '001' ELSE CASE WHEN (totalhr>=8 AND ((h1<=3 AND h1>=0) or h2 >24 )) THEN '010' ELSE CASE WHEN (totalhr>=8 AND ( h2 <=24 And  h2 >=22)) THEN '100' ELSE '000' END END END As night,workday FROM  ";
            //StrSql += "                               (SELECT  userID,totalhr,usertype,datepart(hh,starttime)+ ROUND(datepart(mi,starttime)/60.0,2) As h1,CASE WHEN datepart(hh,endtime) + ROUND(datepart(mi,starttime)/60.0,2)<= datepart(hh,starttime) + ROUND(datepart(mi,starttime)/60.0,2) THEN datepart(hh,endtime) +ROUND(datepart(mi,endtime)/60.0,2)+ 24 ELSE datepart(hh,endtime) + Round(datepart(mi,endtime)/60.0,2) END As h2,CONVERT(varchar(10), starttime, 120) As workday FROM tcpc0.dbo.hr_Attendance_calendar  ";
            StrSql += "                               (SELECT  userID,totalhr,usertype,datepart(hh,starttime)+ ROUND(datepart(mi,starttime)/60.0,2) As h1,CASE WHEN datediff(day,starttime,endtime)<> 0  THEN datepart(hh,endtime) +ROUND(datepart(mi,endtime)/60.0,2)+ 24 ELSE datepart(hh,endtime) + Round(datepart(mi,endtime)/60.0,2) END As h2,CONVERT(varchar(10), starttime, 120) As workday FROM tcpc0.dbo.hr_Attendance_calendar  ";
            StrSql += "                                WHERE uyear='" + txb_year.Text + "' AND umonth= '" + txb_month.Text + "' AND plantid='" + ddl_site.SelectedValue + "' ";
            if (txb_day.Text.Trim().Length > 0)
            {
                StrSql += " and day(starttime)='" + txb_day.Text + "'";
            }
            if (txb_userno.Text.Trim().Length > 0)
            {
                StrSql += " and userno='" + txb_userno.Text.Trim() + "'";
            }
            if (ddl_type.SelectedIndex > 0)
            {
                StrSql += " and usertype='" + ddl_type.SelectedValue + "'";
            }
            StrSql += "  ) q ) qq GROUP BY userID,workday,usertype ) qqq  GrOUP BY userID, usertype )  as n  ON n.userID =a.userID And n.usertype =a.usertype ";

            StrSql += " order by a.userno ";
        }
        //Response.Write(StrSql);
        //return;

        Decimal total2 = 0;
        string dd = "";
        Int32 uid = 0;
        Int32 total1 = 0;

        ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);

        System.Data.DataTable dtl = new System.Data.DataTable();
        dtl.Columns.Add(new DataColumn("group_id", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("user_id", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_cc", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_no", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_name", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_type", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_machine", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_date", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_start", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_end", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_atten", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_night", typeof(System.String)));

        DataRow drow;
        if (ds.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                drow = dtl.NewRow();

                drow[0] = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                drow[1] = ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
                if (ddl_cc.SelectedIndex > 0)
                {
                    drow[2] = ddl_cc.SelectedItem.Text;
                }
                else
                {
                    for (int j = 0; j < ddl_cc.Items.Count; j++)
                    {
                        if (ddl_cc.Items[j].Value == ds.Tables[0].Rows[i].ItemArray[12].ToString())
                        {
                            drow[2] = ddl_cc.Items[j].Text;
                            break;
                        }
                    }
                }
                drow[3] = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                drow[4] = ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim();
                drow[5] = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                drow[6] = ds.Tables[0].Rows[i].ItemArray[4].ToString().Trim();


                if (ds.Tables[0].Rows[i].ItemArray[5].ToString() != "")
                {
                    drow[7] = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[5]));
                    drow[8] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[5]));

                }
                if (ds.Tables[0].Rows[i].ItemArray[6].ToString() != "")
                    drow[9] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[6]));

                drow[10] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]));
                drow[11] = ds.Tables[0].Rows[i].ItemArray[10].ToString().Trim();
                dtl.Rows.Add(drow);

                total2 = total2 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]);

                total1 = total1 + 1;

                dd = ds.Tables[0].Rows[i].ItemArray[8].ToString();

                uid = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[11]);
            }
        }

        ds.Reset();



        //Response.Write(total.ToString());

        if (total2 > 0)
        {
            if (chk_sum.Checked == false)
            {
                if (uid == 0)
                {
                    lbl_time.Text = "刷新时间： " + dd + " (自动)  ";
                }
                else
                {
                    lbl_time.Text = "刷新时间： " + dd + " (" + uid.ToString() + ")  ";
                }
                lbl_qty.Text = "月考勤工时： " + string.Format("{0:##0.##}", total2);
            }
            else
            {
                if (uid == 0)
                {
                    lbl_time.Text = "人数： " + total1.ToString() + " (自动)  ";
                }
                else
                {
                    lbl_time.Text = "人数： " + total1.ToString() + " (" + uid.ToString() + ")  ";
                }
                lbl_qty.Text = "月考勤工时： " + string.Format("{0:##0.##}", total2);
            }
        }

        DataView dvw;
        dvw = new DataView(dtl);

        gvBadProd.DataSource = dvw;
        gvBadProd.DataBind();
    }

    protected void gvBadProd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBadProd.PageIndex = e.NewPageIndex;
        BindData();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvBadProd_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btn_list_Click(object sender, EventArgs e)
    {
        if (ddl_cc.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择一个成本中心！')";
        }
        else
        {
            ltlAlert.Text = "";
            LoadData();
            BindData();
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        BindData();
    }

    protected void btn_detail_Click(object sender, EventArgs e)
    {
        if (ddl_cc.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择一个成本中心！')";
        }
        else
        {
            if (txb_year.Text.Trim().Length == 0)
                txb_year.Text = string.Format("{0:yyyy}", DateTime.Today);
            if (txb_month.Text.Trim().Length == 0)
                txb_month.Text = string.Format("{0:MM}", DateTime.Today);

            DateTime dateto;
            DateTime datefrom = Convert.ToDateTime(txb_year.Text + "-" + txb_month.Text + "-01");
            if (Convert.ToInt16(txb_month.Text) == 12)
            {
                dateto = Convert.ToDateTime(Convert.ToString(Convert.ToInt16(txb_year.Text) + 1) + "-01-01");
            }
            else
            {
                dateto = Convert.ToDateTime(txb_year.Text + "-" + Convert.ToString(Convert.ToInt16(txb_month.Text) + 1) + "-01");
            }

            ltlAlert.Text = "var w=window.open('/wsline/wl_exportcalendar.aspx?a=1&co=" + txb_userno.Text + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&dd1=" + Convert.ToString(datefrom) + "&dd2=" + Convert.ToString(dateto) + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
        }
    }

    protected void btn_repeat_Click(object sender, EventArgs e)
    {
        if (txb_year.Text.Trim().Length == 0)
            txb_year.Text = string.Format("{0:yyyy}", DateTime.Today);
        if (txb_month.Text.Trim().Length == 0)
            txb_month.Text = string.Format("{0:MM}", DateTime.Today);

        DateTime dateto;
        DateTime datefrom = Convert.ToDateTime(txb_year.Text + "-" + txb_month.Text + "-01");
        if (Convert.ToInt16(txb_month.Text) == 12)
        {
            dateto = Convert.ToDateTime(Convert.ToString(Convert.ToInt16(txb_year.Text) + 1) + "-01-01");
        }
        else
        {
            dateto = Convert.ToDateTime(txb_year.Text + "-" + Convert.ToString(Convert.ToInt16(txb_month.Text) + 1) + "-01");
        }

        ltlAlert.Text = "var w=window.open('/wsline/wl_exportcalendar.aspx?a=4&co=" + txb_userno.Text + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&yy=" + txb_year.Text + "&mm=" + txb_month.Text + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        if (txb_year.Text.Trim().Length == 0)
            txb_year.Text = string.Format("{0:yyyy}", DateTime.Today);
        if (txb_month.Text.Trim().Length == 0)
            txb_month.Text = string.Format("{0:MM}", DateTime.Today);

        DateTime dateto;
        DateTime datefrom = Convert.ToDateTime(txb_year.Text + "-" + txb_month.Text + "-01");
        if (Convert.ToInt16(txb_month.Text) == 12)
        {
            dateto = Convert.ToDateTime(Convert.ToString(Convert.ToInt16(txb_year.Text) + 1) + "-01-01");
        }
        else
        {
            dateto = Convert.ToDateTime(txb_year.Text + "-" + Convert.ToString(Convert.ToInt16(txb_month.Text) + 1) + "-01");
        }

        string str = chk_multi.Checked ? "1" : "0";

        if (chk_sum.Checked == false)
            ltlAlert.Text = "var w=window.open('/wsline/wl_exportcalendar.aspx?a=2&b=" + str + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&yy=" + txb_year.Text + "&mm=" + txb_month.Text + "&co=" + txb_userno.Text.Trim() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
        else
            ltlAlert.Text = "var w=window.open('/wsline/wl_exportcalendar.aspx?a=3&b=" + str + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&yy=" + txb_year.Text + "&mm=" + txb_month.Text + "&co=" + txb_userno.Text.Trim() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
    }

    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCC();
    }

    protected void chk_sum_CheckedChanged(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        BindData();
    }

    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        BindData();
    }

    protected void chk_multi_CheckedChanged(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        BindData();
    }
}
