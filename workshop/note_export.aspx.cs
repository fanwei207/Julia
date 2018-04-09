using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Collections.Generic;

public partial class workshop_note_export : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtEndOnLine.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(+1));
            //txtStartOnLine.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            //txtStartOffDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            //txtEndOffDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(+1));
          //  BindGrid();
        }
    }
    protected  void BindGrid()
    {
        try
        {
            string strName = "sp_note_selectExportWoNote";
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@sOnLineDate", txtStartOnLine.Text);
            param[1] = new SqlParameter("@eOnLineDate",txtEndOnLine.Text);
            param[2] = new SqlParameter("@sOffDate", txtStartOffDate.Text);
            param[3] = new SqlParameter("@eOffDate", txtEndOffDate.Text);
            param[4] = new SqlParameter("@type", ddlType.SelectedValue);
            param[5] = new SqlParameter("@nbr", txtNbr.Text.Trim());
            param[6] = new SqlParameter("@site", txtSite.Text.Trim());
            param[7] = new SqlParameter("@plant", Session["PlantCode"].ToString());
            DataTable ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStartOnLine.Text.Trim().Length > 0)
            {
                DateTime dt1 = Convert.ToDateTime(txtStartOnLine.Text.Trim());
            }
            if (txtStartOffDate.Text.Trim().Length > 0)
            {
                DateTime dt2 = Convert.ToDateTime(txtStartOffDate.Text.Trim());
            }
            if (txtEndOffDate.Text.Trim().Length > 0)
            {
                DateTime dt3 = Convert.ToDateTime(txtEndOffDate.Text.Trim());
            }
            if (txtStartOnLine.Text.Trim().Length > 0)
            {
                DateTime dt4 = Convert.ToDateTime(txtStartOnLine.Text.Trim());
            }
            BindGrid();
        }
        catch
        {
            this.Alert("日期格式有误！");
            return;
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGrid();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _id = gv.DataKeys[index].Values["note_id"].ToString();
            string _type = ddlType.SelectedValue;
            this.Redirect("note_det.aspx?id=" + _id + "&type="+_type+"&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if(txtStartOnLine.Text.Trim().Length>0)
                {
                    DateTime dt1 = Convert.ToDateTime(txtStartOnLine.Text.Trim());
                }
                if (txtStartOffDate.Text.Trim().Length>0)
                {
                    DateTime dt2 = Convert.ToDateTime(txtStartOffDate.Text.Trim());
                }
                if (txtEndOffDate.Text.Trim().Length > 0)
                {
                    DateTime dt3 = Convert.ToDateTime(txtEndOffDate.Text.Trim());
                }
                if (txtStartOnLine.Text.Trim().Length>0)
                {
                    DateTime dt4 = Convert.ToDateTime(txtStartOnLine.Text.Trim());
                }
                
            }
            catch
            {
                this.Alert("日期格式有误！");
                return;
            }
            string strName = "sp_note_selectExportWoNoteDetail";
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@sOnLineDate", txtStartOnLine.Text);
            param[1] = new SqlParameter("@eOnLineDate", txtEndOnLine.Text);
            param[2] = new SqlParameter("@sOffDate", txtStartOffDate.Text);
            param[3] = new SqlParameter("@eOffDate", txtEndOffDate.Text);
            param[4] = new SqlParameter("@type", ddlType.SelectedValue);
            param[5] = new SqlParameter("@nbr", txtNbr.Text.Trim());
            param[6] = new SqlParameter("@site", txtSite.Text.Trim());
            param[7] = new SqlParameter("@plant", Session["PlantCode"].ToString());
            DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            string title = "<b>工单</b>~^<b>ID</b>~^120^<b>地点</b>~^<b>域</b>~^<b>产线</b>~^<b>日期</b>~^<b>车间</b>~^<b>线长</b>~^150^<b>工单上下线</b>~^150^<b>备注</b>~^";
            title += "<b>事项</b>~^200^<b>工作内容</b>~^<b>是否已做</b>~^";
            string[] pa = { "wo_nbr", "wo_lot" };
            ExportExcel(title, table, false, 10, pa);
        }
        catch
        {
            this.Alert("导出失败！请联系管理员！");
        }
    }
    string nbr = "";
    string lot = "";
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("\n", "<br />");
            #region
            //int index = e.Row.RowIndex;
            //if (index == 0)
            //{
            //    nbr = e.Row.Cells[0].Text;
            //    lot = e.Row.Cells[1].Text;
            //}
            //else
            //{
            //    if (e.Row.Cells[0].Text != nbr && e.Row.Cells[1].Text != lot)
            //    {
            //        nbr = e.Row.Cells[0].Text;
            //        lot = e.Row.Cells[1].Text;
            //    }
            //    else
            //    {
            //        for (int i = 0; i <= 9; i++)
            //        {
            //            e.Row.Cells[i].Text = "";
            //        }
            //    }
            //}
            #endregion
        }
    }
}