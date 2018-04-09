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
using System.Data;
using System.Text;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using WHInfo;


public partial class SID_ShipCopareDet : BasePage
{
    WareHouse WH = new WareHouse();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            
            string str1 = Request.QueryString["tsklot"];
            if (!string.IsNullOrEmpty(Request.QueryString["tsknbr"]) || !string.IsNullOrEmpty(Request.QueryString["tsklot"]))
            {
                //txt_nbr.Text = Request.QueryString["tsknbr"];
                //txt_lot.Text = Request.QueryString["tsklot"];
                BindData();
            }
            //验货生成Cimload权限
            //if (!this.Security["550020050"].isValid)
            //{
            //    btn_cimload.Visible = false;
            //}
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvorder.Rows.Count; i++)
        {
            //CheckBox cb = (CheckBox)gvorder.Rows[i].FindControl("chk_Select");
            //if (chkAll.Checked)
            //{
            //    cb.Checked = true;
            //}
            //else
            //{
            //    cb.Checked = false;
            //}
        }
    }

    protected void BindData()
    {
        DataTable dt = WH.GetDate(Request.QueryString["tsknbr"], Request.QueryString["tsklot"], "", "", "", "select", 0);

        gvorder.DataSource = dt;
        gvorder.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvorder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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
            if (e.Row.Cells[7].Text.Trim() != e.Row.Cells[8].Text.Trim())
            {
                e.Row.Cells[8].BackColor = Color.Red;
            }
        }
    }

    protected void btn_exporttoexcel_Click(object sender, EventArgs e)
    {
        //DataTable dt = compare.GetDate(txt_nbr.Text.Trim(), txt_lot.Text.Trim(), "", "", "", "select");
        //if (dt.Rows.Count <= 0)
        //{
        //    this.Alert("无所查询数据！");
        //    return;
        //}
        //string title = "80^<b>域</b>~^110^<b>地点</b>~^120^<b>单号</b>~^70^<b>批号</b>~^70^<b>行号</b>~^120^<b>库位</b>~^120^<b>物料号</b>~^80^<b>100数量</b>~^80^<b>QAD数量</b>~^80^<b>批序号<b>~^80^<b>价格</b>~^120^<b>类型</b>~^";
        //this.ExportExcel(title, dt, false);
    }
    protected void btn_cimload_Click(object sender, EventArgs e)
    {
        DataTable dt = Wh_Compare.GetWhCimload(Convert.ToInt32(Session["uid"]),Request.QueryString["tsknbr"], Request.QueryString["tsklot"]);

        //FileInfo file=new FileInfo("cimload");
        string root = @"d:\\"; //Server.MapPath("..\\Excel\\");
        string domain = "";
        string path_szx = "";
        if (dt.Rows.Count > 0)
        {
            domain = dt.Rows[0]["ps_domain"].ToString();
        }
        if (domain == "szx")
        {
            path_szx = root + "s" + string.Format("{0}.wod", DateTime.Now.ToFileTime().ToString());
        }
        else if (domain == "zql")
        {
            path_szx = root +  "z" + string.Format("{0}.wod", DateTime.Now.ToFileTime().ToString());
        }
        else if (domain == "yql")
        {
            path_szx = root +  "y" + string.Format("{0}.wod", DateTime.Now.ToFileTime().ToString());
        }
        else if (domain == "hql")
        {
            path_szx = root +  "h" + string.Format("{0}.wod", DateTime.Now.ToFileTime().ToString());
        }
        //string path_szx = root + "cimload1.wod";
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
                str += row["ps_end"].ToString() +"\r\n";
                //str += row["ps_finish"].ToString() + "\r\n";
            }
            str += "@@FINISH" + "\r\n";
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