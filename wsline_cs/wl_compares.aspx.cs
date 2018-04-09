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


public partial class wl_compares : BasePage
{
    adamClass chk = new adamClass();
    DataSet ds;
    WSExcelHelper.WSExcelHelper excel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txb_date1.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";

            //if (Request["site"] !=null)
            //   ddl_site.SelectedValue  = Request["site"];

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

        StrSql = "select distinct cc,ccname from tcpc0.dbo.hr_Attendance_CC where plantID='" + ddl_site.SelectedValue + "'";
        //Response.Write(strSql)
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
        System.Data.DataTable dtl = new System.Data.DataTable();
        dtl.Columns.Add(new DataColumn("group_cc", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_no", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_name", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_machine", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_date", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_atten", typeof(System.String)));
        dtl.Columns.Add(new DataColumn("group_nbr", typeof(System.String)));

        DataRow drow;
        string StrSql = "";
        Int32 total = 0;
        if (ddl_type.SelectedValue == "2") //有考勤,无工单工时
        {
            StrSql = " select a.userno,a.username,a.fingerprint,a.starttime,a.endtime,a.totalhr,isnull(b.wo2_userID,0),isnull(c.wocd_userid,0) ";
            StrSql += " from tcpc0.dbo.hr_Attendance_calendar a ";
            StrSql += " left outer Join wo2_WorkOrderEnter b on a.userid=b.wo2_userID and year(b.wo2_effdate)=year(a.starttime) and month(b.wo2_effdate)=month(a.starttime) and day(b.wo2_effdate)=day(a.starttime) ";
            StrSql += " and (isnull(b.wo2_NewCenter,b.wo2_Center)=a.cc or (a.cc='2460' and isnull(b.wo2_NewCenter,b.wo2_Center)='2420' and substring(b.wo2_part,3,2)='61'))";
            StrSql += " left outer Join wo_cost_detail c ";
            StrSql += " on a.userid=c.wocd_userid and year(c.wocd_date)=year(a.starttime) and month(c.wocd_date)=month(a.starttime) and day(c.wocd_date)=day(a.starttime) ";
            StrSql += " and c.wocd_NewCC=a.cc ";
            StrSql += " where a.plantID='" + ddl_site.SelectedValue + "' and a.cc='" + ddl_cc.SelectedValue + "' ";
            StrSql += " and a.starttime >='" + txb_date1.Text + "' ";
            if (txb_date2.Text.Trim().Length > 0)
            {
                StrSql += " and a.starttime <='" + txb_date2.Text + "' ";
            }
            //StrSql += " and year(a.starttime)='" + txb_year.Text + "' and Month(a.starttime)='" + txb_month.Text + "' ";
            if (txb_userno.Text.Trim().Length > 0)
            {
                StrSql += " and a.userno='" + txb_userno.Text.Trim() + "'";
            }
            StrSql += " and a.usertype=394 and a.totalhr > 0 ";
            StrSql += " order by a.userno,a.starttime ";
            //Response.Write(StrSql);
            //return;
            try
            {
                ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]) == 0 && Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]) == 0)
                        {
                            drow = dtl.NewRow();

                            drow[0] = ddl_cc.SelectedItem.Text;
                            drow[1] = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                            drow[2] = ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
                            drow[3] = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                            if (ds.Tables[0].Rows[i].ItemArray[3].ToString() != "")
                            {
                                drow[4] = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[3]));
                            }

                            drow[5] = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[5]) > 0 ? "有" : "无";
                            //drow[9] = ds.Tables[0].Rows[i].ItemArray[8].ToString();
                            dtl.Rows.Add(drow);
                            total = total + 1;
                        }
                    }
                }
                ds.Reset();
            }
            catch
            {
                Response.Write(StrSql);
            }
        }
        else //无考勤,有工单工时
        {
            try
            {

                ds = GetWoWithnotHr(Convert.ToInt32(ddl_site.SelectedItem.Value), Convert.ToInt32(ddl_cc.SelectedItem.Value), txb_date1.Text.Trim(), txb_date2.Text.Trim(), txb_userno.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {

                        drow = dtl.NewRow();

                        drow[0] = ddl_cc.SelectedItem.Text;
                        drow[1] = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                        drow[2] = ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
                        if (ds.Tables[0].Rows[i].ItemArray[2].ToString() != "")
                        {
                            drow[4] = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]));
                        }
                        drow[5] = "无";
                        drow[6] = ds.Tables[0].Rows[i].ItemArray[4].ToString().Trim();
                        dtl.Rows.Add(drow);

                        total = total + 1;


                    }
                }
                ds.Reset();

            }
            catch
            {
                Response.Write(StrSql);
            }
        }

        lbl_qty.Text = "记录数：" + total.ToString();


        DataView dvw;
        dvw = new DataView(dtl);
        dvw.Sort = "group_date";

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
        if (txb_date1.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请填写比较日期初始值！')";
            return;
        }
        else
        {
            try
            {
                DateTime dtFormat = Convert.ToDateTime(txb_date1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('比较日期格式为yyyy-MM-dd！')";
                return;
            }
        }

        if (txb_date2.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dtFormat = Convert.ToDateTime(txb_date2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('比较日期格式为yyyy-MM-dd！')";
                return;
            }
        }

        BindData();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/wsline/wl_exportcompares.aspx?a=" + ddl_type.SelectedValue + "&pl=" + ddl_site.SelectedValue + "&cc=" + ddl_cc.SelectedValue + "&date1=" + txb_date1.Text.Trim() + "&date2=" + txb_date2.Text.Trim() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
    }

    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCC();
    }

    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        BindData();
    }

    protected DataSet GetWoWithnotHr(int plantcode, int workcenter, string date1, string date2, string userNo)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@plantcode", plantcode);
        param[1] = new SqlParameter("@workcenter", workcenter);
        param[2] = new SqlParameter("@date1", date1);
        param[3] = new SqlParameter("@date2", date2);
        param[4] = new SqlParameter("userNo", userNo);

        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_selectWoWithnotHr", param);
    }
}
