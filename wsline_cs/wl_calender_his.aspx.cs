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


public partial class wsline_cs_Default : BasePage
{
    adamClass chk = new adamClass();
    DataSet ds;
    WSExcelHelper.WSExcelHelper excel = null;

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("7000311", "考勤数据");
        }

        base.OnInit(e);
    }

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
    }


    protected void BindData()
    {
        string StrSql = "";

        if (chk_sum.Checked == false)
        {

            StrSql = " select a.id,a.userid,a.userno,a.username,a.fingerprint,a.starttime,a.endtime,a.totalhr,a.createddate,case when a.usertype=394 then N'A类' when a.usertype=395 then N'B类' when a.usertype=396 then N'C类' when a.usertype=397 then N'D类' when a.usertype=398 then N'E类' end ,ISNULL(q.night,''), createdBy";
            StrSql += " from tcpc0.dbo.hr_Attendance_calendar_his a ";
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
            StrSql += "                  CASE WHEN datepart(hh,endtime) + ROUND(datepart(mi,starttime)/60.0,2)<= datepart(hh,starttime) + ROUND(datepart(mi,starttime)/60.0,2) THEN datepart(hh,endtime) +ROUND(datepart(mi,endtime)/60.0,2)+ 24 ELSE datepart(hh,endtime) + Round(datepart(mi,endtime)/60.0,2) END As h2, ";
            //StrSql += "                  CONVERT(varchar(10), starttime, 120) As workday  ";
            StrSql += "                 starttime ";
            StrSql += "                 FROM tcpc0.dbo.hr_Attendance_calendar_his WHERE uyear='" + txb_year.Text + "' AND umonth='" + txb_month.Text + "' AND plantid='" + ddl_site.SelectedValue + "' AND cc ='" + ddl_cc.SelectedValue + "' ";
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

            StrSql += " where a.plantID='" + ddl_site.SelectedValue + "' and a.cc='" + ddl_cc.SelectedValue + "' ";
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
            StrSql = " select 0,a.userid,a.userno,a.username,'','','',a.totalhr,'',a.aType,ISNULL(n.night,''), createdBy  FROM ";
            StrSql += " (select a.userid,a.userno,a.username,a.usertype,sum(a.totalhr) As totalhr,case when a.usertype=394 then N'A类' when a.usertype=395 then N'B类' when a.usertype=396 then N'C类' when a.usertype=397 then N'D类' when a.usertype=398 then N'E类' end As aType , createdBy";
            StrSql += " from tcpc0.dbo.hr_Attendance_calendar_his a ";

            StrSql += " where a.plantID='" + ddl_site.SelectedValue + "' and a.cc='" + ddl_cc.SelectedValue + "' ";
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
            StrSql += " group by a.userid,a.userno,a.username,a.usertype,createdby ) a ";

            StrSql += " LEFT OUTER JOIN ( SELECT userID ,usertype,CAST(SUM(CAST(SubString(night,1,1) As Int)) As Varchar(2))+'@'+CAST(SUM(CAST(SubString(night,2,1) As Int)) As Varchar(2)) +'@'+ CAST(SUM(CAST(SubString(night,3,1) As Int)) As Varchar(2)) As night ";
            StrSql += "                  FROM ( SELECT userID, usertype,CASE WHEN Max(CAST(qq.night As int)) =0  THEN '000' ELSE CASE WHEN Max(CAST(qq.night As int)) =1 THEN '001' ELSE CASE WHEN Max(CAST(qq.night As int)) =10 THEN '010' ELSE '100' END END END As night  ";
            StrSql += "                         FROM( SELECT userID,usertype,CASE WHEN (totalhr>=12 AND h1<=22 AND h2>=30) THEN '001' ELSE CASE WHEN (totalhr>=8 AND ((h1<=3 AND h1>=0) or h2 >24 )) THEN '010' ELSE CASE WHEN (totalhr>=8 AND ( h2 <=24 And  h2 >=22)) THEN '100' ELSE '000' END END END As night,workday FROM  ";
            StrSql += "                               (SELECT  userID,totalhr,usertype,datepart(hh,starttime)+ ROUND(datepart(mi,starttime)/60.0,2) As h1,CASE WHEN datepart(hh,endtime) + ROUND(datepart(mi,starttime)/60.0,2)<= datepart(hh,starttime) + ROUND(datepart(mi,starttime)/60.0,2) THEN datepart(hh,endtime) +ROUND(datepart(mi,endtime)/60.0,2)+ 24 ELSE datepart(hh,endtime) + Round(datepart(mi,endtime)/60.0,2) END As h2,CONVERT(varchar(10), starttime, 120) As workday FROM tcpc0.dbo.hr_Attendance_calendar_his  ";
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
                drow[2] = ddl_cc.SelectedItem.Text;
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
                lbl_qty.Text = "月考勤工时： " + string.Format("{0:##0.##}", total2);
            }
            else
            {
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
    protected void btn_search_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        BindData();
    }
    protected void btn_detail_Click(object sender, EventArgs e)
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

        ltlAlert.Text = "var w=window.open('/wsline/wl_exportcalendar.aspx?a=1&co=" + txb_userno.Text + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&dd1=" + Convert.ToString(datefrom) + "&dd2=" + Convert.ToString(dateto) + "&his=1','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
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

        ltlAlert.Text = "var w=window.open('/wsline/wl_exportcalendar.aspx?a=4&co=" + txb_userno.Text + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&yy=" + txb_year.Text + "&mm=" + txb_month.Text + "&his=1','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
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

        if (chk_sum.Checked == false)
            ltlAlert.Text = "var w=window.open('/wsline/wl_exportcalendar.aspx?a=2&co=" + txb_userno.Text + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&yy=" + txb_year.Text + "&mm=" + txb_month.Text + "&day=" + txb_day.Text.Trim() + "&his=1','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
        else
            ltlAlert.Text = "var w=window.open('/wsline/wl_exportcalendar.aspx?a=3&co=" + txb_userno.Text + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&yy=" + txb_year.Text + "&mm=" + txb_month.Text + "&day=" + txb_day.Text.Trim() + "&his=1','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
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

}
