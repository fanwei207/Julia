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
using Wage;


public partial class wl_calendar : BasePage
{
    adamClass chk = new adamClass();
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 验厂作假
            //if (Session["PlantCode"].ToString() == "5")
            //{
            //    btn_list.Visible = false;
            //    btn_export.Visible = false;
            //    btn_detail.Visible = false;
            //    btn_repeat.Visible = false;
            //    btn_exportAll.Visible = false;
            //    btn_listNew.Visible = false;
            //    gvBadProd.Columns[6].Visible = false;
            //    gvBadProd.Columns[11].Visible = false;
            //    gvBadProd.Columns[12].Visible = false;
            //    lbl_qty.Visible = false;
            //}
            //else
            //{
            //}
            #endregion

            #region 验厂作假
            //if (Session["PlantCode"].ToString() != "5")
            //{
            //    btn_detail.Visible = this.Security["7000311"].isValid;
            //    btn_detail.Enabled = this.Security["7000311"].isValid;
            //    if (Session["uRole"].ToString() == "1" || this.Security["7000316"].isValid)
            //    {
            //        btn_exportAll.Visible = true;
            //    }
            //}
            #endregion


            #region 原来的
            btn_detail.Visible = this.Security["7000311"].isValid;
            btn_detail.Enabled = this.Security["7000311"].isValid;
            //验证是否有查询所有的数据，金银花特有权限
            if (Session["uRole"].ToString() == "1" || this.Security["7000316"].isValid)
            {
                btn_exportAll.Visible = true;
            }
            #endregion
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

        StrSql = "select distinct code, name from tcpc" + ddl_site.SelectedValue + ".dbo.Departments a ";
        if (Convert.ToInt32(Session["uRole"]) != 1)
        {
            StrSql += " INNER JOIN tcpc0.dbo.wo_cc_permission cp ON cp.perm_userid = '" + Session["uID"] + "' AND cp.perm_ccid = a.code ";
        }
        StrSql += "where (active = 1 OR isSalary = 1)";
        reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql);
        while (reader.Read())
        {
            ls = new ListItem();
            ls.Value = reader["code"].ToString().Trim();
            ls.Text = "(" + reader["code"].ToString().Trim() + ")" + reader["name"].ToString().Trim();
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
        if (ddl_cc.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择 成本中心！');";
            return;
        }

        if (txb_year.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写 年！');";
            return;
        }
        else
        {
            try
            {
                int _y = Convert.ToInt32(txb_year.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('年 必须是数字！');";
                return;
            }
        }

        if (txb_month.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写 月！');";
            return;
        }
        else
        {
            try
            {
                int _m = Convert.ToInt32(txb_month.Text.Trim());
                if (_m <= 0 || _m > 12)
                {
                    ltlAlert.Text = "alert('月 只能是在1-12之间！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('月 必须是数字！');";
                return;
            }
        }

        try
        {
            string strSql = "sp_hr_creatCalendarSinger";
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@plantid", ddl_site.SelectedValue);
            sqlParam[1] = new SqlParameter("@costCenter", ddl_cc.SelectedValue);
            sqlParam[2] = new SqlParameter("@uyear", txb_year.Text);
            sqlParam[3] = new SqlParameter("@umonth", txb_month.Text);
            sqlParam[4] = new SqlParameter("@userno", txb_userno.Text);

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            ltlAlert.Text = "alert('操作失败！请联系管理员！');";
            return;
        }

        BindData(); 
    }

    protected void LoadData1()
    {
        if (ddl_cc.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择 成本中心！');";
            return;
        }

        if (txb_year.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写 年！');";
            return;
        }
        else
        {
            try
            {
                int _y = Convert.ToInt32(txb_year.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('年 必须是数字！');";
                return;
            }
        }

        if (txb_month.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写 月！');";
            return;
        }
        else
        {
            try
            {
                int _m = Convert.ToInt32(txb_month.Text.Trim());
                if (_m <= 0 || _m > 12)
                {
                    ltlAlert.Text = "alert('月 只能是在1-12之间！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('月 必须是数字！');";
                return;
            }
        }

        try
        {
            string strSql = "sp_hr_creatCalendarSinger1";
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@plantid", ddl_site.SelectedValue);
            sqlParam[1] = new SqlParameter("@costCenter", ddl_cc.SelectedValue);
            sqlParam[2] = new SqlParameter("@uyear", txb_year.Text);
            sqlParam[3] = new SqlParameter("@umonth", txb_month.Text);
            sqlParam[4] = new SqlParameter("@userno", txb_userno.Text);

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            ltlAlert.Text = "alert('操作失败！请联系管理员！');";
            return;
        }

        BindData();
    }
    protected void BindData()
    {
        string StrSql = "";

        //上海从2014-8开始，其他地区从2014-7开始
        //全夜班的计算时长
        int _fullNightHours = 12;

        if (Convert.ToInt32(Session["PlantCode"]) == 1)
        {
            _fullNightHours = 12;
        }
        else
        {
            _fullNightHours = 9;
        }

        if (chk_sum.Checked == false)
        {
            StrSql = " select a.id,a.userid,a.userno,a.username,a.fingerprint,a.starttime,a.endtime,a.totalhr,a.createddate,case when a.usertype=394 then N'A类' when a.usertype=395 then N'B类' when a.usertype=396 then N'C类' when a.usertype=397 then N'D类' when a.usertype=398 then N'E类' end ,ISNULL(q.night,''), createdBy, a.cc";
            //StrSql += ",totalhr2 = Case when ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2) >=12 then ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)-1.5 when ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)>=5 then ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)-1 else ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2) end, cc.ccname";
            StrSql += ",totalhr2 = isnull(a.totalhr,0.0) + isnull(a.totalhrOver,0.0), cc.ccname, a.starttimeOri, a.endtimeOri";
            if (!chk_multi.Checked)
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar a ";
            else
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar_ori a ";

            StrSql += "left join(";
			StrSql += "             select distinct cc, ccname from tcpc0.dbo.hr_Attendance_CC";
		    StrSql += "         ) cc on cc.cc = a.cc";

            StrSql += " LEFT OUTER JOIN (SELECT userID, ";
            StrSql += "                 CASE WHEN (totalhr >= " + _fullNightHours.ToString() + " AND h1<=22 AND h2>=28) THEN N'全夜' ";
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
            //StrSql = " select 0,a.userid,a.userno,a.username,'','','',a.totalhr,'',a.aType,ISNULL(n.night,''), createdBy ,a.cc,a.totalhr2, ccname FROM ";
            StrSql = " select 0,a.userid,a.userno,a.username,'','','',a.totalhr,'',a.aType,ISNULL(n.night,''), createdBy ,a.cc,totalhr2 = isnull(a.totalhr,0.0) + isnull(a.totalhrOver,0.0), ccname , starttimeOri, endtimeOri FROM ";
            StrSql += " (select a.userid,a.userno,a.username,a.usertype,sum(a.totalhr) As totalhr,case when a.usertype=394 then N'A类' when a.usertype=395 then N'B类' when a.usertype=396 then N'C类' when a.usertype=397 then N'D类' when a.usertype=398 then N'E类' end As aType , createdBy ,a.cc";
            StrSql += ", sum(Case when ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2) >=12 then ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)-1.5 when ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)>=5 then ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)-1 else ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2) end) AS totalhr2, cc.ccname, a.starttimeOri, a.endtimeOri";
            if (!chk_multi.Checked)
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar a ";
            else
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar_ori a ";

            StrSql += "left join(";
            StrSql += "             select distinct cc, ccname from tcpc0.dbo.hr_Attendance_CC";
            StrSql += "         ) cc on cc.cc = a.cc";

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
            StrSql += " group by a.userid,a.userno,a.username,a.usertype,a.cc,createdby, cc.ccname) a ";

            StrSql += " LEFT OUTER JOIN ( SELECT userID ,usertype,CAST(SUM(CAST(SubString(night,1,1) As Int)) As Varchar(2))+'@'+CAST(SUM(CAST(SubString(night,2,1) As Int)) As Varchar(2)) +'@'+ CAST(SUM(CAST(SubString(night,3,1) As Int)) As Varchar(2)) As night ";
            StrSql += "                  FROM ( SELECT userID, usertype,CASE WHEN Max(CAST(qq.night As int)) =0  THEN '000' ELSE CASE WHEN Max(CAST(qq.night As int)) =1 THEN '001' ELSE CASE WHEN Max(CAST(qq.night As int)) =10 THEN '010' ELSE '100' END END END As night  ";
            StrSql += "                         FROM( SELECT userID,usertype,CASE WHEN (totalhr >= " + _fullNightHours.ToString() + " AND h1<=22 AND h2>=28) THEN '001' ELSE CASE WHEN (totalhr>=8 AND ((h1<=3 AND h1>=0) or h2 >24 )) THEN '010' ELSE CASE WHEN (totalhr>=8 AND ( h2 <=24 And  h2 >=22)) THEN '100' ELSE '000' END END END As night,workday FROM  ";
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
        dtl.Columns.Add(new DataColumn("group_atten2", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_starttime", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_endtime", typeof(System.String)));

        DataRow drow;
        if (ds.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                drow = dtl.NewRow();

                drow[0] = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                drow[1] = ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
                drow[2] = ds.Tables[0].Rows[i]["ccname"].ToString();
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
                {
                    drow[9] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[6]));
                }
                    drow[10] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]));
                    drow[11] = ds.Tables[0].Rows[i].ItemArray[10].ToString().Trim();
                    drow[12] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[13]));
                
                    //显示实际上下班时间
                    if (ds.Tables[0].Rows[i].ItemArray[15].ToString() != "")
                    {
                        drow[13] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[15]));
                    }
                    if (ds.Tables[0].Rows[i].ItemArray[16].ToString() != "")
                    {
                        drow[14] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16]));
                    } 
                dtl.Rows.Add(drow);

                total2 = total2 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]);

                total1 = total1 + 1;
                if (ds.Tables[0].Rows[i].ItemArray[8].ToString() != "")
                {
                    dd = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[8]));
                }
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
    protected void btn_exportAll_Click(object sender, EventArgs e)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uYear", txb_year.Text.Trim());
            param[1] = new SqlParameter("@uMonth ", txb_month.Text.Trim());
            param[2] = new SqlParameter("@plantID ", Session["plantCode"].ToString());
            dt = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_hr_selectUserAllCalendarInfo", param).Tables[0];

        }
        catch
        {
            this.Alert("获取数据失败，请联系管理员");

        }
        if (dt.Rows.Count > 0)
        {
            string title = "120^<b>成本中心</b>~^80^<b>工号</b>~^100^<b>姓名</b>~^80^<b>类型</b>~^100^<b>考勤号</b>~^100^<b>考勤日期</b>~^40^<b>星期</b>~^180^<b>上班时间</b>~^180^<b>下班时间</b>~^<b>考勤工时</b>~^<b>中夜班</b>~^<b>考勤小时2</b>~^<b>实际上班时间</b>~^<b>实际下班时间</b>~^";
            ExportExcel(title, dt, false);

        }
        else
        {
            this.Alert("没有可导出的数据");
        }
    }

    protected void btn_listNew_Click(object sender, EventArgs e)
    {
        if (ddl_cc.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择一个成本中心！')";
        }
        else
        {
            HR hr_salary = new HR();
            int intadjust = hr_salary.finAdjust(Convert.ToInt32(txb_year.Text), Convert.ToInt32(txb_month.Text), Convert.ToInt32(Session["PlantCode"]), 0);
            if (intadjust < 0)
            {
                string str = @"<script language='javascript'> alert('工资已被财务冻结，不能重复操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
                return;
            }

            ltlAlert.Text = "";
            LoadData1();
            BindData();
        }
    }
}
