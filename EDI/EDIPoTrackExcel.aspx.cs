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
using CommClass;
using System.IO;

public partial class EDIPoTrackExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindData();

            GroupRows(gvlist, 0, 0, 1);
            GroupRows(gvlist, 1, 0, 1);
            GroupRows(gvlist, 2, 0, 1);
            GroupRows(gvlist, 3, 0, 1);
            GroupRows(gvlist, 4, 0, 1);
            GroupRows(gvlist, 5, 0, 1);
            GroupRows(gvlist, 6, 0, 1);
            GroupRows(gvlist, 7, 0, 1);
            GroupRows(gvlist, 11, 0, 1);
            GroupRows(gvlist, 12, 0, 1);
            GroupRows(gvlist, 13, 0, 1);
            GroupRows(gvlist, 14, 0, 1);
            GroupRows(gvlist, 15, 0, 1);
            GroupRows(gvlist, 16, 0, 1);
            GroupRows(gvlist, 17, 0, 1);
            GroupRows(gvlist, 18, 0, 1);
            GroupRows(gvlist, 19, 0, 1);

            string style = @"<style> .text { mso-number-format:\@; } </script> ";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=potracking.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvlist.RenderControl(htw);
            // Style is added dynamically
            Response.Write(style);
            Response.Write(sw.ToString());
            Response.End();
        }
    }

    private void bindData()
    {
        DataSet ds = GetPoTrack();

        this.gvlist.DataSource = ds;
        this.gvlist.DataBind();
    }

    public DataSet GetPoTrack()
    {
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@condition", Request.QueryString["condition"].ToString());

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectOrdTrackExcel", sqlParam);
        return ds;
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //为导出做样式准备
            foreach (TableCell cell in e.Row.Cells)
            {
                cell.Attributes.Add("class", "text");
            }
        }
    }
    //   <summary>   
    ///   合并GridView列中相同的行   
    ///   </summary>   
    ///   <param   name="GridView1">GridView对象</param>   
    ///   <param   name="cellNum">需要合并的列</param>   
    public static void GroupRows(GridView GridView1, int cellNum)
    {
        int i = 0, rowSpanNum = 1;
        while (i < GridView1.Rows.Count - 1)
        {
            GridViewRow gvr = GridView1.Rows[i];
            for (++i; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gvrNext = GridView1.Rows[i];
                if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                {
                    gvrNext.Cells[cellNum].Visible = false;
                    rowSpanNum++;
                }
                else
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    rowSpanNum = 1;
                    break;
                }

                if (i == GridView1.Rows.Count - 1)
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                }
            }
        }
    }
    ///   <summary>   
    ///   根据条件列合并GridView列中相同的行   
    ///   </summary>   
    ///   <param   name="GridView1">GridView对象</param>   
    ///   <param   name="cellNum">需要合并的列</param>
    ///   ///   <param   name="cellNum2">条件列(根据某条件列还合并)</param>
    public static void GroupRows(GridView GridView1, int cellNum, int cellNum2, int cellNum3)
    {
        int i = 0, rowSpanNum = 1;
        while (i < GridView1.Rows.Count - 1)
        {
            GridViewRow gvr = GridView1.Rows[i];
            for (++i; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gvrNext = GridView1.Rows[i];
                if (gvr.Cells[cellNum].Text + gvr.Cells[cellNum2].Text + gvr.Cells[cellNum3].Text == gvrNext.Cells[cellNum].Text + gvrNext.Cells[cellNum2].Text + gvrNext.Cells[cellNum3].Text)
                {
                    gvrNext.Cells[cellNum].Visible = false;
                    rowSpanNum++;
                }
                else
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    rowSpanNum = 1;
                    break;
                }

                if (i == GridView1.Rows.Count - 1)
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                }
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}
