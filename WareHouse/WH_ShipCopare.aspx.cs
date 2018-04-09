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
using QADSID;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using WHInfo;

public partial class WH_ShipCopare : BasePage
{
    //Wh_Compare compare = new Wh_Compare();
    WareHouse WH = new WareHouse();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            //验货生成Cimload权限
            //if (!this.Security["550020050"].isValid)
            //{
            //    btn_cimload.Visible = false;
            //}
            BindData();
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        for (int i = 0; i < gvorder.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvorder.Rows[i].FindControl("chk_Select");
            if (chkAll.Checked)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
        }
    }

    protected void BindData()
    {
        DataTable dt = WH.GetDate(txt_nbr.Text.Trim(), txt_lot.Text.Trim(), txt_startdate.Text.Trim(), txt_endate.Text.Trim(), txt_domain.Text.Trim(), ddl_type.SelectedValue, Convert.ToInt32(rd_checked.SelectedValue));

        gvorder.DataSource = dt;
        gvorder.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvorder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //int index1 = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
        //if (gvorder.DataKeys[index1].Values["wh_qty_loc100"].ToString() != gvorder.DataKeys[index1].Values["wh_qty_locqad"].ToString())
        //{
        //    //e.Row.Cells[index1].BackColor = Color.Red;
        //}
        if (e.CommandName.ToString() == "Detail1")
        {
            if (gvorder.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('此行是空行！');";
                return;
            }
        }
    }
    protected void gvorder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvorder.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[8].Text.Trim() != e.Row.Cells[9].Text.Trim())
            {
                e.Row.Cells[9].BackColor = Color.Red;
            }
            e.Row.Attributes.Add("ondblclick", "window.showModalDialog('SID_ShipCopareDet.aspx?tsknbr=" + e.Row.Cells[3].Text.Trim() + "&tsklot=" + e.Row.Cells[4].Text.Trim() + "',null,'dialogWidth=1080px;dialogHeight=600px');");
        }
    }

    protected void btn_exporttoexcel_Click(object sender, EventArgs e)
    {
        DataTable dt = WH.GetDate(txt_nbr.Text.Trim(), txt_lot.Text.Trim(), txt_startdate.Text.Trim(), txt_endate.Text.Trim(), txt_domain.Text.Trim(), ddl_type.SelectedValue, Convert.ToInt32(rd_checked.SelectedValue));
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }
        string title = "80^<b>域</b>~^110^<b>地点</b>~^120^<b>单号</b>~^70^<b>批号</b>~^70^<b>行号</b>~^120^<b>库位</b>~^120^<b>物料号</b>~^80^<b>100数量</b>~^80^<b>QAD数量</b>~^80^<b>批序号<b>~^80^<b>价格</b>~^120^<b>类型</b>~^";
        this.ExportExcel(title, dt, false);
    }
    protected void btn_cimload_Click(object sender, EventArgs e)
    {
        int iApplyId1 = WH.DeleteCimloadId(Convert.ToInt32(Session["uid"]), Convert.ToInt32(Session["PlantCode"]));
        if (iApplyId1 == -1)
        {
            ltlAlert.Text = "alert('删除临时数据错误，请联系管理员')";
            return;
        }

        int count = 0;
        string str_domain1 = "";
        string str_type1 = "";
        for (int i = 0; i < this.gvorder.Rows.Count; i++)
        {
            CheckBox cbox = (CheckBox)this.gvorder.Rows[i].Cells[0].Controls[1];
            GridViewRow gvr = this.gvorder.Rows[i];
            if (cbox.Checked == true)
            {
                string str_domain = gvorder.DataKeys[i].Values["wh_domain"].ToString();
                string str_nbr = gvorder.DataKeys[i].Values["wh_nbr"].ToString();
                string str_lot = gvorder.DataKeys[i].Values["wh_lot"].ToString();
                string str_line = gvorder.DataKeys[i].Values["wh_line"].ToString();
                string str_loc = gvorder.DataKeys[i].Values["wh_loc"].ToString();
                string str_part = gvorder.DataKeys[i].Values["wh_part"].ToString();
                string str_serrial = gvorder.DataKeys[i].Values["wh_serial"].ToString();
                string str_type = gvorder.DataKeys[i].Values["wh_type"].ToString();
                string a = gvr.Cells[12].Text;
                if (gvr.Cells[13].Text == "已确认")
                {
                    ltlAlert.Text = "alert('已确认明细不能重复确认，请重新选择!')";
                    return;
                }
                if (!string.IsNullOrEmpty(str_domain1) && str_domain1 != gvorder.DataKeys[i].Values["wh_domain"].ToString())
                {
                    ltlAlert.Text = "alert('每次只能选择一个域的明细生成文件，请重新选择!')";
                    return;
                }
                if (!string.IsNullOrEmpty(str_type1) && str_type1 != gvorder.DataKeys[i].Values["wh_type"].ToString())
                {
                    ltlAlert.Text = "alert('每次只能选择一个类型的的明细生成文件，请重新选择!')";
                    return;
                }

                int iApplyId = WH.GetCimloadId(str_domain, str_nbr, str_lot, str_line, str_loc, str_part, str_serrial, str_type, Convert.ToInt32(Session["uid"]), Convert.ToInt32(Session["PlantCode"]));
                //int iApplyId = Wh_Compare.GetCimloadId(gvorder.DataKeys[i].Values["wh_domain"].ToString(), Convert.ToInt32(Session["uid"]), Convert.ToInt32(Session["PlantCode"]), gvr.Cells[12].Text);
                if (iApplyId == -1)
                {
                    ltlAlert.Text = "alert('获取订单的ID失败，请重新提交一次')";
                    return;
                }
                count += 1;
                if (count > 0)
                {
                    str_domain1 = gvorder.DataKeys[i].Values["wh_domain"].ToString();
                    str_type1 = gvorder.DataKeys[i].Values["wh_type"].ToString();
                }
            }
        }
        if (count <= 0)
        {
            ltlAlert.Text = "alert('获取订单的ID失败，请选择要生成的明细')";
            return;
        }
        //DataTable dt = Wh_Compare.GetWhCimload(Convert.ToInt32(Session["uid"]), Convert.ToInt32(Session["PlantCode"]));
        DataTable dt = WH.GetWhCimload(Convert.ToInt32(Session["uid"]), "", "");
        if (dt.Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('无符合条件的数据，请选择要生成的明细')";
            return;
        }

        //FileInfo file=new FileInfo("cimload");
        string root = @"d:\\"; //Server.MapPath("..\\Excel\\");
        string domain = "";
        string path_szx = "";
        if (dt.Rows.Count > 0)
        {
            domain = dt.Rows[0]["ps_domain"].ToString();
        }
        if (domain == "SZX")
        {
            path_szx = root + "s" + string.Format("{0}.wod", DateTime.Now.ToFileTime().ToString());
        }
        else if (domain == "ZQL")
        {
            path_szx = root + "z" + string.Format("{0}.wod", DateTime.Now.ToFileTime().ToString());
        }
        else if (domain == "YQL")
        {
            path_szx = root + "y" + string.Format("{0}.wod", DateTime.Now.ToFileTime().ToString());
        }
        else if (domain == "HQL")
        {
            path_szx = root + "h" + string.Format("{0}.wod", DateTime.Now.ToFileTime().ToString());
        }
        try
        {
            StreamWriter writer_szx = new StreamWriter(path_szx, false, Encoding.GetEncoding("gb2312"));
            string str = "";
            str += "@@BEGIN" + "\r\n";
            foreach (DataRow row in dt.Rows)
            {
                //str += row["ps_header"].ToString() + "\r\n";
                str += row["ps_nbr"].ToString() + "\r\n";
                str += row["ps_lot"].ToString() + "\r\n";
                str += row["ps_part"].ToString() + "\r\n";
                str += row["ps_m"].ToString() + "\r\n";
                str += row["ps_qty"].ToString() + "\r\n";
                str += row["ps_site"].ToString() + "\r\n";
                str += row["ps_loc"].ToString() + "\r\n";
                str += row["ps_x"].ToString() + "\r\n";
                str += row["ps_remark"].ToString() + "\r\n";
                str += row["ps_end"].ToString() + "\r\n";
                //str += row["ps_finish"].ToString() + "\r\n";
            }
            str += "@@FINISH" + "\r\n";
            writer_szx.WriteLine(str);
            writer_szx.Flush();

            writer_szx.Close();
            ltlAlert.Text = "alert('成功导出到CIM文件！');";
            BindData();
        }
        catch
        {
            ltlAlert.Text = "alert('导出文件失败！');";

        }
    }
    protected void rd_checked_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}