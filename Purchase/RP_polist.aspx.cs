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
using System.Data.SqlClient;
using System.Reflection;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using CommClass;
using System.Text;
using adamFuncs;

public partial class Purchase_RP_polist : BasePage
{
    public adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //this.SecurityCheck = securityCheck.issecurityCheck(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["uRole"]), 320000, 320001);
            //if (!this.SecurityCheck.isValid)
            //{
            //    Response.Redirect("~/public/ErrMessage.aspx", true);
            //}
            bool a = this.Security["320001"].isValid;
            Session["statusPostBack"] = 1;

            //if (!this.Security["320001"].isValid)//采购员查看
            //{
            //    btnExport.Enabled = false;
            //    txtBuyer.Text = Session["uType"].ToString().ToLower() == "administrator" ? "输入采购员" : Session["uBuyer"].ToString();
            //    dtgList.Columns[12].Visible = false;
            //}
            //else
            //{
            //    txbvend.Text = Session["uCmpCode"].ToString();
            //    txbvend.ReadOnly = Session["uType"].ToString().ToLower() == "administrator" ? false : true;
            //}

            BindData();
        }
    }
    public  DataSet GetPurchaseOrder(string pono, string povend, string poship, string podomain, string poorddate, string poduedate, string pocon, string condate, string stat, string buyer)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@pono", pono);
            param[1] = new SqlParameter("@povend", povend);
            param[2] = new SqlParameter("@poship", poship);
            param[3] = new SqlParameter("@podomain", podomain);
            param[4] = new SqlParameter("@poorddate", poorddate);
            param[5] = new SqlParameter("@poduedate", poduedate);
            param[6] = new SqlParameter("@pocon", pocon);
            param[7] = new SqlParameter("@condate", condate);
            param[8] = new SqlParameter("@postat", stat);
            param[9] = new SqlParameter("@buyer", buyer);

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_selectPurchaseOrder", param);
            return ds;
        }
        catch
        {
            return null;
        }
    }
    public void BindData()
    {
        String strVend = String.Empty;

        if (!this.Security["320001"].isValid)
        {
           // txtBuyer.Visible = true;
            strVend = txbvend.Text.Trim();
        }
        else
        {
           // txtBuyer.Visible = Session["uType"].ToString().ToLower() == "administrator" ? true : false;
            strVend = Session["uType"].ToString().ToLower() == "administrator" ? txbvend.Text.Trim() : Session["uCmpCode"].ToString();
        }

        DataSet dst = GetPurchaseOrder(txbpo.Text,
                                                        strVend,
                                                        ddlsite.SelectedItem.Text,
                                                        ddldomain.SelectedItem.Text,
                                                        txbord.Text,
                                                        txbdue.Text,
                                                        txbconp.Text,
                                                        txtDate.Text.Trim(),
                                                        dropStat.SelectedValue,
                                                        "");

        lblcount.Text = dst.Tables[0].Rows.Count.ToString();

        if (dst.Tables[0].Rows.Count == 0)
        {
            lblcount.Text = "0";
            dst.Tables[0].Rows.Add(dst.Tables[0].NewRow());
            dtgList.DataSource = dst;
            dtgList.DataBind();
            int columnCount = dtgList.Rows[0].Cells.Count;
            dtgList.Rows[0].Cells.Clear();
            dtgList.Rows[0].Cells.Add(new TableCell());
            dtgList.Rows[0].Cells[0].ColumnSpan = columnCount;
            dtgList.Rows[0].Cells[0].Text = "没有记录";
            dtgList.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            dtgList.DataSource = dst;
            dtgList.DataBind();
            lblcount.Text = dst.Tables[0].Rows.Count.ToString();
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txbord.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txbord.Text.Trim());
            }
            catch
            {
                this.Alert("订货日期格式不对");
                return;
            }
        }

        if (txbdue.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txbdue.Text.Trim());
            }
            catch
            {
                this.Alert("截止日期格式不对");
                return;
            }
        }

        if (txtDate.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch
            {
                this.Alert("确认日期格式不对");
                return;
            }
        }

        BindData();
    }

    protected void PageChange(object sender, GridViewPageEventArgs e)
    {
        dtgList.PageIndex = e.NewPageIndex;
        dtgList.SelectedIndex = -1;
        BindData();
    }

    protected void dtgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower().CompareTo("printdelivery") == 0)
        {
            Response.Redirect("RP_editDeliverys.aspx?po=" + dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text + "&domain=" + dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text + "&type=" + "add" + "&rm=" + DateTime.Now.ToString(), true);
        }
        else if (e.CommandName.ToString().ToLower().CompareTo("confirm") == 0)
        {



            int error = 0;
            //string qwa = dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[10].Text;
            if (dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[10].Text == "未审批")
            {

                this.Alert("此采购单审核未完成!");
                return;
            }

            SqlDataReader reader1 = SelectPoDetExists(dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text.ToString(), dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text.ToString(), dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text.ToString());
            if (!reader1.Read())
            {
                error = 1;

            }
            reader1.Close();

            if (error == 1)
            {
                this.Alert("订单信息同步未完全，请联系管理员!");
                return;
            }
            string message = string.Empty;
            SqlDataReader reader = SelectPoDetPrice(dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text.ToString(), dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text.ToString(), dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text.ToString());
            if (reader.HasRows)
            {
                StringBuilder line = new StringBuilder();
                while (reader.Read())
                {
                    line.Append(reader["line"].ToString() + " 、");
                }
                int a = line.ToString().LastIndexOf("、");
                int b = line.Length;
                message = line.ToString().Substring(0, a);


            }
            reader.Close();
            if (message != string.Empty)
            {
                this.Alert("订单行" + message + "没有采购价格，请联系采购员!");
                return;
            }
            if (this.Security["320001"].isValid)
            {
                //int nError = PurchaseOrder.Confirm_Supp_PO(dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text.ToString(), dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text.ToString(), Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text.ToString());

                //if (nError == -99)
                //{
                //    this.Alert("该订单某些订单项文档不齐全,请联系技术部相关人员!");
                //    return;
                //}

                BindData();
            }
        }
     
    }

    protected void dtgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (this.Security["320001"].isValid && (e.Row.Cells[0].Text.Trim() == String.Empty || e.Row.Cells[0].Text.Trim() == "&nbsp;"))
            {
                e.Row.Cells[11].Enabled = true;
                e.Row.Cells[12].Enabled = true;
                e.Row.Cells[13].Enabled = false;
                e.Row.Cells[13].Text = "确认";
                e.Row.Cells[13].Font.Size = 10;

            }
            else if (this.Security["320001"].isValid && e.Row.Cells[0].Text.Trim() != String.Empty && e.Row.Cells[0].Text.Trim() != "&nbsp;")
            {
                e.Row.Cells[11].Enabled = false;
                e.Row.Cells[12].Enabled = false;
                e.Row.Cells[13].Enabled = true;

            }

            if (e.Row.Cells[1].Text.Trim() != "&nbsp;")
            {
                e.Row.Attributes.Add("ondblclick", "location.href='supplier_podetails.aspx?PONO.=" + e.Row.Cells[1].Text.Trim() + "&do=" + e.Row.Cells[4].Text.Trim() + "&stat=" + dropStat.SelectedValue + "&rm=" + DateTime.Now.ToLocalTime() + "&id=330000'");
            }

         
         

          
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        String strVend = String.Empty;

        //if (!this.Security["320001"].isValid)
        //{
        //  //  txtBuyer.Visible = true;
        //   // strVend = txbvend.Text.Trim();
        //}
        //else
        //{
        //   // strVend = Session["uType"].ToString().ToLower() == "administrator" ? txbvend.Text.Trim() : Session["uCmpCode"].ToString();
        //   // txtBuyer.Visible = Session["uType"].ToString().ToLower() == "administrator" ? true : false;
        //   // strVend = Session["uType"].ToString().ToLower() == "administrator" ? txbvend.Text.Trim() : Session["uCmpCode"].ToString();
        //}


        if (!this.Security["320001"].isValid)
        {
            // txtBuyer.Visible = true;
            strVend = txbvend.Text.Trim();
        }
        else
        {
            // txtBuyer.Visible = Session["uType"].ToString().ToLower() == "administrator" ? true : false;
            strVend = Session["uType"].ToString().ToLower() == "administrator" ? txbvend.Text.Trim() : Session["uCmpCode"].ToString();
        }

        //String strParams = txbpo.Text.Trim() + "," + strVend + "," + ddlsite.SelectedItem.Text + "," + ddldomain.SelectedItem.Text + "," + txbord.Text + "," + txbdue.Text + "," + txbconp.Text + "," + txtDate.Text.Trim() + "," + dropStat.SelectedValue + "," + "";

        //strParams = Server.HtmlEncode(strParams);
        
        ltlAlert.Text = "var w=window.open('exportorderlst.aspx?" + "txbpo=" + txbpo.Text + "&strvend=" + strVend + "&ddlsite=" + ddlsite.SelectedItem.Text + "&ddldomain=" + ddldomain.SelectedItem.Text + "&txbord=" + txbord.Text + "&txbdue=" + txbdue.Text + "&txbconp=" + txbconp.Text + "&txtDate=" + txtDate.Text.Trim() + "&dropStat=" + dropStat.SelectedValue +"','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
        // this.OpenWindow("supplier_export.aspx?ty=1&pa=" + strParams);
        //this.Response.Redirect("supplier_export.aspx?ty=1&pa=" + strParams);
    }

    protected SqlDataReader SelectPoDetPrice(string nbr, string vend, string domain)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@vend", vend);
            param[2] = new SqlParameter("@domain", domain);

            return SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_supp_selectPoDetPrice", param);
        }
        catch
        {
            return null;
        }

    }
    protected SqlDataReader SelectPoDetExists(string nbr, string vend, string domain)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@vend", vend);
            param[2] = new SqlParameter("@domain", domain);

            return SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_supp_selectPoDetExists", param);
        }
        catch
        {
            return null;
        }

    }
    protected SqlDataReader SelectPo_appv(string nbr, string domain)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@pov_nbr", nbr);
            param[1] = new SqlParameter("@pov_domain", domain);

            return SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.qadplan"), CommandType.StoredProcedure, "sp_pur_selectpoappvResult", param);
        }
        catch
        {
            return null;
        }

    }
}