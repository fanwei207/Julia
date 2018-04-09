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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Text;


public partial class Bom_CheckDetails : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            txt_BeginDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
            txt_EndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            gv_CheckInfo.Columns[0].Visible = false;
            BindData();
        }
    }

    protected void BindData()
    {
        DataTable dt = Bom_AccessApply.GetBomCheckDetails(txt_ps_par.Text, rbt_ChangBom.SelectedValue, int.Parse(Convert.ToString(Session["uid"])), Convert.ToInt32(Session["PlantCode"]), txt_BeginDate.Text.Trim(), txt_EndDate.Text.Trim());
        gv_CheckInfo.DataSource = dt;
        gv_CheckInfo.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
            if (gv_CheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('此行是空行！');";
                return;
            }
            ltlAlert.Text = "window.open('Bom_Con_Details.aspx?DID=" + Server.UrlEncode(gv_CheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') ";
            //Response.Redirect("Bom_Change.aspx?DID=" + Server.UrlEncode(gv_CheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&RAD=" + Convert.ToInt32(Session["PlantCode"]));// + "&rm=" + DateTime.Now);
        }
        if (e.CommandName.ToString() == "Check")
        {
            if (gv_CheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
          {
              ltlAlert.Text = "alert('此行是空行！');";
              return;
          }
          string str=gv_CheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim();

          Response.Redirect("Bom_Change.aspx?DID=" + Server.UrlEncode(gv_CheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&RAD=" + Convert.ToInt32(Session["PlantCode"]) + "&CTP=" + rbt_ChangBom.SelectedValue);// + "&rm=" + DateTime.Now);
        }
    }

    protected void gvShip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_CheckInfo.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvShip_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            string str=e.Row.Cells[1].Text;
        }

    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;

        foreach (GridViewRow row in gv_CheckInfo.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

            chkImport.Checked = chkAll.Checked;
        }
    }

    protected void rbt_ChangBom_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
        if (rbt_ChangBom.SelectedValue == "S")
        {
            gv_CheckInfo.Columns[0].Visible = false;
            //gv_CheckInfo.Columns[11].Visible = true;
            btn_exrort.Enabled = false;
        }
        if (rbt_ChangBom.SelectedValue == "W")
        {
            gv_CheckInfo.Columns[0].Visible = false;
            //gv_CheckInfo.Columns[11].Visible = true;
            btn_exrort.Enabled = false;
        }
        if (rbt_ChangBom.SelectedValue == "A" || rbt_ChangBom.SelectedValue == "F")
        {
            gv_CheckInfo.Columns[0].Visible = false;
            //gv_CheckInfo.Columns[11].Visible=false;
            btn_exrort.Enabled = false;
        }
        if (rbt_ChangBom.SelectedValue == "Y")
        {
            gv_CheckInfo.Columns[0].Visible = true;
            //gv_CheckInfo.Columns[11].Visible = false;
            btn_exrort.Enabled = true;
        }
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        int count = 0;
        for (int i = 0; i < this.gv_CheckInfo.Rows.Count; i++)
        {
            CheckBox cbox = (CheckBox)this.gv_CheckInfo.Rows[i].Cells[0].Controls[1];
            GridViewRow gvr = this.gv_CheckInfo.Rows[i];
            if (cbox.Checked == true)
            {
                string a = gvr.Cells[1].Text;
                int iApplyId = Bom_AccessApply.GetCimloadId(int.Parse(gvr.Cells[1].Text), Convert.ToInt32(Session["uid"]), Convert.ToInt32(Session["PlantCode"]),"U");
                if (iApplyId == -1)
                {
                    ltlAlert.Text = "alert('获取BOM的ID失败，请重新提交一次')";
                    return;
                }
                count += 1;
            }
        }
        if (count <= 0)
        {
            ltlAlert.Text = "alert('获取BOM的ID失败，请选择要生成明细')";
            return;
        }
        DataTable dt = Bom_AccessApply.GetBomCimload(Convert.ToInt32(Session["uid"]), Convert.ToInt32(Session["PlantCode"]));
        //FileInfo file=new FileInfo("cimload");
         string root = @"d:\\"; //Server.MapPath("..\\Excel\\");
         string path_szx = root + "cimload1.cim";
         try
         {
             StreamWriter writer_szx = new StreamWriter(path_szx, false, Encoding.GetEncoding("gb2312"));
             string str = "";
             foreach (DataRow row in dt.Rows)
             {
                 str += row["header"].ToString() + "\r\n";
                 str += row["ps_par"].ToString() + "\r\n";
                 str += row["ps_comp"].ToString();
                 str += " " + "-" + " ";
                 str +=  row["ps_start"].ToString() + "\r\n";
                 if (string.IsNullOrEmpty(row["qty_per"].ToString()))
                 {
                     str += "\"" + row["qty_per"].ToString() + " " + "\"";
                 }
                 else
                 {
                     str += "-"+" ";
                 }
                 str +=  "-" + " ";
                 str +=  "-" + " ";
                 str += row["ps_end"].ToString();
                 if (string.IsNullOrEmpty(row["qty_per"].ToString()))
                 {
                     str += "\"" + row["ps_scrp_pct"].ToString() + "\"" + "\r\n";
                 }
                 else
                 {
                     str += " " + "-" + "\r\n";
                 }
                 str += row["ends"].ToString() + "\r\n";
             }
             writer_szx.WriteLine(str);
             writer_szx.Flush();

             writer_szx.Close();
             ltlAlert.Text = "alert('成功导出到CIM文件！');";

         }
         catch
         {
             ltlAlert.Text = "alert('导出文件失败！');";
         }
    }
}

