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


public partial class wl_analysis : BasePage
{
    adamClass chk = new adamClass();
    DataSet ds;
    WSExcelHelper.WSExcelHelper excel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["yy"] ==null)
               txb_year.Text = string.Format("{0:yyyy}",DateTime.Today);
            else
               txb_year.Text = Request["yy"];

            if (Request["mm"] == null)
               txb_month.Text = string.Format("{0:MM}", DateTime.Today);
            else
               txb_month.Text = Request["mm"];

            //if (Request["site"] !=null)
            //   ddl_site.SelectedValue  = Request["site"];

            if (Convert.ToInt16(Session["uRole"]) != 1)
            {
               ddl_site.Enabled = false;
            }
            ddl_site.SelectedValue = Session["PlantCode"].ToString() ;
         


             LoadCC();
             if (Request["cc"] != null)
                 ddl_cc.SelectedValue = Request["cc"];

             //if (Request["ty"] != null)
             //    ddl_type.SelectedValue = Request["ty"];

             btn_list.Attributes.Add("onclick", "return confirm('刷新将重新计算，可能需较长的时间，是否继续？');"); 

             //LoadData();

             BindData();
        }
    }

    protected void LoadData()
    {
         lbl_qty.Text = "";
         lbl_time.Text  ="";

         string StrSql = "";

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

        
         StrSql = "Delete from tcpc0.dbo.hr_Attendance_disp where (userid='" + Session["uID"] + "'or userid=0) and plantid='" + ddl_site.SelectedValue + "' and cc='" + ddl_cc.SelectedValue + "' ";
         StrSql += " and year(ddate) ='" + Convert.ToInt16(txb_year.Text) + "' and month(ddate)='" + Convert.ToInt16(txb_month.Text) + "' ";
         SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, StrSql);

         //StrSql = "Delete from tcpc0.dbo.hr_Attendance_disp_temp where userid='" + Session["uID"] + "'or userid=0";
         //SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, StrSql);

         //StrSql = "Delete from tcpc0.dbo.hr_Attendance_calendar where createdby='" + Session["uID"] + "'or isnull(createdby,0)=0 and plantid='" + ddl_site.SelectedValue + "' " ;
         //SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, StrSql);

         decimal coefficient = 1.02M;
           

         for (i = 0; i < 31; i++)
         {
             StrSql = "Insert into tcpc0.dbo.hr_Attendance_disp(userid,dispx,hr_comp,hr_atten,ddate,hr_qty_comp,hr_qty_atten,hr_qty_rcomp,hr_rcomp,hr_qty_unplan,hr_unplan,createddate,plantid,cc,usertype) ";
             StrSql += " values('" + Session["uID"] + "','" + i.ToString() + "',0,0,DateADD(dd," + i.ToString() + ", '" + datefrom + "'),0,0,0,0,0,0,getdate(),'" + ddl_site.SelectedValue + "','" + ddl_cc.SelectedValue + "','" + ddl_type.SelectedValue + "')";
             SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, StrSql);
         }


             //QAD正常加工单完工入库数和工时,wo_type=''
         if (Convert.ToInt32(ddl_type.SelectedValue) == 1) //A+B类
         {
             StrSql = " select tr_effdate, sum(tt*isnull(Cast(r.ro_run as decimal(9,5)),0))* " + Convert.ToString(coefficient) +",sum(tt) from ";
         }
         else //A类
         {
             StrSql = " select tr_effdate, sum(tt*isnull(Cast(r.ro_run as decimal(9,5)),0)* " + Convert.ToString(coefficient) + "),sum(tt) from ";
         }
             StrSql += " (select h.tr_effdate,w.wo_routing, w.wo_domain,sum(isnull(h.tr_qty_loc,0)) as tt from QAD_Data.dbo.tr_hist h ";
             StrSql += " Inner Join QAD_Data.dbo.wo_mstr w on w.wo_domain=h.tr_domain and w.wo_site=h.tr_site and w.wo_nbr=h.tr_nbr and w.wo_lot=h.tr_lot and w.wo_type=''";
             if (Convert.ToString(ddl_cc.SelectedValue)=="2460")
             {
                StrSql += " and (w.wo_flr_cc ='2460' or w.wo_flr_cc='3430'  or w.wo_flr_cc='2440') ";
             }
             else
             {
                StrSql += " and w.wo_flr_cc ='" + ddl_cc.SelectedValue + "' ";
             }
             StrSql += " where h.tr_effdate>='" + datefrom + "' and h.tr_effdate<'" + dateto + "' and h.tr_type='rct-wo'" ;
             StrSql += " and h.tr_nbr<>'' and h.tr_qty_loc<>0 ";
             StrSql += " group by h.tr_effdate,w.wo_routing,w.wo_domain) ss ";
             StrSql += " LEFT OUTER JOIN QAD_Data.dbo.ro_det r ON r.ro_routing = ss.wo_routing AND r.ro_domain = ss.wo_domain ";
             StrSql += " group by tr_effdate ";
             //Response.Write(StrSql)
             //Exit Sub

             ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);

             if (ds.Tables[0].Rows.Count > 0)
             {
                 for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                 {
                     StrSql = "Update tcpc0.dbo.hr_Attendance_disp set ";
                     StrSql += " hr_qty_comp=isnull(hr_qty_comp,0) +  " + ds.Tables[0].Rows[i].ItemArray[2] + ",hr_comp=isnull(hr_comp,0) +  " + ds.Tables[0].Rows[i].ItemArray[1];
                     StrSql += " where userid='" + Session["uID"] + "' and ddate='" + ds.Tables[0].Rows[i].ItemArray[0] + "' and plantid='" + ddl_site.SelectedValue + "' and cc='" + ddl_cc.SelectedValue + "' ";
                     SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);
                 }
             }
             ds.Reset();

             //QAD返工加工单入库数和工时wo_type='R',已确认的，以生效日期为工时日期
             StrSql = "select cd.wocd_date,sum(isnull(h.tr_qty_loc,0)) as tt,sum(cd.ct) from QAD_Data.dbo.tr_hist h ";
             StrSql += " Inner Join QAD_Data.dbo.wo_mstr w on w.wo_domain=h.tr_domain and w.wo_site=h.tr_site and w.wo_nbr=h.tr_nbr and w.wo_lot=h.tr_lot and w.wo_type='R'";
             if (Convert.ToString(ddl_cc.SelectedValue) == "2460")
             {
                 StrSql += " and (w.wo_flr_cc ='2460' or w.wo_flr_cc='3430'  or w.wo_flr_cc='2440')  Inner Join";
             }
             else
             {
                 StrSql += " and w.wo_flr_cc ='" + ddl_cc.SelectedValue + "' Inner Join";
             }
             StrSql += " (select wocd_date,wocd_site,wocd_cc,wocd_nbr,wocd_id,sum(isnull(wocd_cost,0)) as ct from wo_cost_detail ";
             StrSql += " where wocd_type='R' and wocd_apprdate is not null ";
             StrSql += " and wocd_date>='" + datefrom + "' and wocd_date<'" + dateto + "' and wocd_cc ='" + ddl_cc.SelectedValue + "' ";
             StrSql += " group by wocd_date,wocd_site,wocd_cc,wocd_nbr,wocd_id ) cd ";
             StrSql += " on w.wo_site=cd.wocd_site and w.wo_nbr=cd.wocd_nbr and w.wo_lot=cd.wocd_id and w.wo_flr_cc=cd.wocd_cc ";
             StrSql += " where h.tr_type='rct-wo'";
             StrSql += " group by cd.wocd_date";
             //Response.Write(StrSql)
             //Exit Sub

             ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);

             if (ds.Tables[0].Rows.Count > 0)
             {
                 for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                 {
                     StrSql = "Update tcpc0.dbo.hr_Attendance_disp set ";
                     StrSql += " hr_qty_rcomp=isnull(hr_qty_rcomp,0) +  " + ds.Tables[0].Rows[i].ItemArray[1] + ",hr_rcomp=isnull(hr_rcomp,0) +  " + ds.Tables[0].Rows[i].ItemArray[2];
                     StrSql += " where userid='" + Session["uID"] + "' and ddate='" + ds.Tables[0].Rows[i].ItemArray[0] + "' and plantid='" + ddl_site.SelectedValue + "'  and cc='" + ddl_cc.SelectedValue + "' ";
                     SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);
                 }
             }
             ds.Reset();

             //计划外用工单完工入库数和工时 woo_type='A',已确认的，以生效日期为工时日期。
             StrSql = " select cd.wocd_date,sum(cd.ct),sum(isnull(o.woo_qty_comp,0)) ";
             StrSql += " from wo_order o inner join ";
             StrSql += " (select wocd_date,wocd_site,wocd_cc,wocd_nbr,sum(isnull(wocd_cost,0)) as ct ";
             StrSql += " from wo_cost_detail ";
             StrSql += " where wocd_apprdate is not null and wocd_cc ='" + ddl_cc.SelectedValue + "' and wocd_type='A' ";
             StrSql += " and wocd_date>='" + datefrom + "' and wocd_date<'" + dateto + "' ";
             StrSql += " group by wocd_date,wocd_site,wocd_cc,wocd_nbr) cd ";
             StrSql += " on o.woo_site=cd.wocd_site and o.woo_cc_to=cd.wocd_cc and o.woo_nbr=cd.wocd_nbr ";
             StrSql += " where o.woo_qty_comp>0 and o.deletedBy is null and o.approveddate is not null ";
             StrSql += " and o.woo_type='A' and o.woo_cc_to ='" + ddl_cc.SelectedValue + "' ";
             StrSql += " group by cd.wocd_date ";
             //Response.Write(StrSql)
             //Exit Sub
             ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);

             if (ds.Tables[0].Rows.Count > 0)
             {
                 for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                 {
                     StrSql = "Update tcpc0.dbo.hr_Attendance_disp set ";
                     StrSql += " hr_qty_unplan=isnull(hr_qty_unplan,0) +  " + ds.Tables[0].Rows[i].ItemArray[2] + ",hr_unplan=isnull(hr_unplan,0) +  " + ds.Tables[0].Rows[i].ItemArray[1];
                     StrSql += " where userid='" + Session["uID"] + "' and ddate='" + ds.Tables[0].Rows[i].ItemArray[0] + "' and plantid='" + ddl_site.SelectedValue + "'  and cc='" + ddl_cc.SelectedValue + "' ";
                     SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);
                 }
             }
             ds.Reset();

             //考勤工时
             DateTime dd1 = Convert.ToDateTime("2000-01-01");
             Int32  uid = 0;

             Int32 total = 0;
             Decimal timeinte = 0;

             if (Convert.ToInt32(ddl_type.SelectedValue) == 2) //A类
             {
                 StrSql = " select a.totalhr,a.starttime,a.userid from tcpc0.dbo.hr_Attendance_calendar a ";
                 StrSql += " where a.starttime>='" + datefrom + "' and a.starttime< DateAdd(dd,1,'" + dateto + "')";
                 StrSql += " and a.plantid='" + ddl_site.SelectedValue + "' and a.cc='" + ddl_cc.SelectedValue + "' ";
                 StrSql += " and a.usertype=394 ";
                 StrSql += " order by a.starttime,a.userid ";
             }
             else //A + B类
             {
                 StrSql = " select a.totalhr,a.starttime,a.userid from tcpc0.dbo.hr_Attendance_calendar a ";
                 StrSql += " where a.starttime>='" + datefrom + "' and a.starttime< DateAdd(dd,1,'" + dateto + "')";
                 StrSql += " and a.plantid='" + ddl_site.SelectedValue + "' and a.cc='" + ddl_cc.SelectedValue + "' ";
                 StrSql += " and (a.usertype=394 or a.usertype=395)";
                 StrSql += " order by a.starttime,a.userid ";
             }
             //Response.Write(StrSql);
   
             ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);
             if (ds.Tables[0].Rows.Count > 0)
             {
                 for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                 {
                     if (string.Format("{0:yyyy-MM-dd}", dd1) != string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[1])) && dd1 != Convert.ToDateTime("2000-01-01"))
                     {
                         StrSql = "Update tcpc0.dbo.hr_Attendance_disp set ";
                         StrSql += " hr_atten=isnull(hr_atten,0) + " + timeinte + ",hr_qty_atten=isnull(hr_qty_atten,0) + " + total ;
                         StrSql += " where userid='" + Session["uID"] + "' and  ddate='" + string.Format("{0:yyyy-MM-dd}", dd1) + "' and plantid='" + ddl_site.SelectedValue + "' and cc='" + ddl_cc.SelectedValue + "' ";
                         SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);

                         total = 0;
                         timeinte = 0;
                     }
                     else
                     {
                         if (uid != Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]))
                         {
                             total = total + 1;
                         }
                         timeinte = timeinte + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[0]);
                     }
                     uid=Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]);
                     dd1= Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[1]);
                 }
             }
             ds.Reset();

             if ((total >0 || timeinte >0) && dd1 != Convert.ToDateTime("2000-01-01"))
             {
                StrSql = "Update tcpc0.dbo.hr_Attendance_disp set ";
                StrSql += " hr_atten=isnull(hr_atten,0) + " + timeinte + ",hr_qty_atten=isnull(hr_qty_atten,0) + " + total ;
                StrSql += " where userid='" + Session["uID"] + "' and  ddate='" + string.Format("{0:yyyy-MM-dd}", dd1) + "' and plantid='" + ddl_site.SelectedValue + "' and cc='" + ddl_cc.SelectedValue + "' ";
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, StrSql);
             }

    }

    protected void LoadCC()
    {
       ddl_cc.Items.Clear();
    
       ListItem ls ; 
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
      
            string StrSql="";

            StrSql = " select dispx,isnull(ddate,''), hr_qty_comp, hr_qty_rcomp , hr_qty_unplan, hr_comp , hr_rcomp , hr_unplan,hr_qty_atten, hr_atten,createddate from tcpc0.dbo.hr_Attendance_disp ";
            StrSql += " where userid='" + Session["uID"] + "'and year(ddate) ='" + Convert.ToInt16(txb_year.Text) + "' and month(ddate)='" + Convert.ToInt16(txb_month.Text) +"' and plantid='" + ddl_site.SelectedValue + "' and cc='" +　ddl_cc.SelectedValue + "' and usertype='" + ddl_type.SelectedValue  + "' order by ddate ";

            //Response.Write(StrSql);
            //return;

            Decimal total1 =0;
            Decimal total2 =0;

            Decimal h1 = 0;
            Decimal h2 = 0;
            Decimal h3 = 0;
            Decimal h4 = 0;
            Decimal h5 = 0;
            Decimal h6 = 0;
            string dd = ""; 

            ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);

            System.Data.DataTable dtl = new System.Data.DataTable();
            dtl.Columns.Add(new DataColumn("group_x", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_cc", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_date", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_qty", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_total", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_qty2", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_total2", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_qty3", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_total3", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_qty_atten", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_atten", typeof(System.String)));
            dtl.Columns.Add(new DataColumn("group_pass", typeof(System.Decimal)));

            DataRow drow;
            if (ds.Tables[0].Rows.Count > 0)
            {
               int i =0;
               for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++) 
               {  
                     drow = dtl.NewRow();
                    
                     drow[0]=ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();


                     drow[1] = ddl_cc.SelectedItem.Text;

                     if (ds.Tables[0].Rows[i].ItemArray[1].ToString() != "")
                         drow[2] = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[1]));


 
                     drow[3] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[2]));
                     drow[4] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[5]));
                     drow[5] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[3]));
                     drow[6] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[6]));
                     drow[7] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[4]));
                     drow[8] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]));
                     drow[9] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[8]));
                     drow[10] = string.Format("{0:##0.##}", Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[9]));

                     if (Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[9]) !=0)
                         drow[11] = string.Format("{0:##0.##}", (Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[5]) + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[6]) + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7])) / Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[9]) * 100);

                     dtl.Rows.Add(drow);


                     if (txb_day.Text.Trim().Length > 0)
                     {
                         if (Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[1]).Day <= Convert.ToInt32(txb_day.Text))
                         {
                             h1 = h1 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[2]);
                             h2 = h2 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[5]);
                             h3 = h3 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[3]);
                             h4 = h4 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[6]);
                             h5 = h5 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[4]);
                             h6 = h6 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]);

                             total1 = total1 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[8]);
                             total2 = total2 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[9]);
                         }
                     }
                     else
                     {
                         h1 = h1 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[2]);
                         h2 = h2 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[5]);
                         h3 = h3 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[3]);
                         h4 = h4 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[6]);
                         h5 = h5 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[4]);
                         h6 = h6 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]);

                         total1 = total1 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[8]);
                         total2 = total2 + Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[9]);
                     }

                     dd = ds.Tables[0].Rows[i].ItemArray[10].ToString();

               }
            }
          
            ds.Reset();

            drow = dtl.NewRow();
            drow[2] = "合计：";
 
            drow[3] = string.Format("{0:##0.##}", h1);
            drow[4] = string.Format("{0:##0.##}", h2);
            drow[5] = string.Format("{0:##0.##}", h3);
            drow[6] = string.Format("{0:##0.##}", h4);
            drow[7] = string.Format("{0:##0.##}", h5);
            drow[8] = string.Format("{0:##0.##}", h6);
            drow[9] = string.Format("{0:##0.##}", total1);
            drow[10] = string.Format("{0:##0.##}", total2);
            dtl.Rows.Add(drow);

 
            //Response.Write(total.ToString());

            if (total2 != 0)
                lbl_qty.Text = "月平均： " + string.Format("{0:##0.##}", (h2 + h4 + h6) / total2 * 100) + "%";
            else
                lbl_qty.Text = "";

             lbl_time.Text = "刷新时间： " + dd; 

            DataView dvw ;
            dvw = new DataView(dtl);
            dvw.Sort = "group_date asc";

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
        ltlAlert.Text = "";

        LoadData();
 
        BindData();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        BindData();
    }

    protected void btn_excel_Click(object sender, EventArgs e)
    {
        //定义参数
        string strFile = "wl_Analysis" + Convert.ToString(gvBadProd.Rows.Count) +"_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string strTemplate = "../docs/wl_analysis.xls";
        
        excel = new WSExcelHelper.WSExcelHelper(Server.MapPath(strTemplate), Server.MapPath("../Excel/") + strFile);


        excel.WOCompAtten(Convert.ToString(Session["uID"]), Convert.ToString(txb_year.Text), Convert.ToString(txb_month.Text), Convert.ToString(ddl_site.SelectedValue), Convert.ToString(ddl_cc.SelectedValue), Convert.ToString(ddl_site.SelectedItem.Text) + " " + Convert.ToString(ddl_cc.SelectedItem.Text) + " 工单入库工时和考勤工时比较");

        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

    }
    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        LoadCC();
    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        LoadData();

        BindData();
    }
}
