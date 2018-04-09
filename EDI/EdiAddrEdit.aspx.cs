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
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;

public partial class EdiAddrEdit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["uID"] == "13")
            {
                btnCimload.Visible = true;
                btnUpdateDB.Visible = true;
                btnUpdateDB.Enabled = true;
            }

            BindGridView();
        }
    }


    protected void BindGridView()
    {
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectAddrEditList");

        gvlist.DataSource = ds;
        gvlist.DataBind();
        /*
        #region 绑定客户
        try
        {
            ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectAddrCustomers");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell c1 = new HtmlTableCell();
                HtmlTableCell c2 = new HtmlTableCell();
                HtmlTableCell c3 = new HtmlTableCell();
                HtmlTableCell c4 = new HtmlTableCell();
                c1.InnerText = row["a1"].ToString();
                tRow.Cells.Add(c1);

                c2.InnerText = row["n1"].ToString();
                tRow.Cells.Add(c2);

                c3.InnerText = row["a2"].ToString();
                tRow.Cells.Add(c3);

                c4.InnerText = row["n2"].ToString();
                tRow.Cells.Add(c4);

                tblRelations.Rows.Add(tRow);
            }
        }
        catch
        { ;}
        #endregion
        */
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gvlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvlist.EditIndex = e.NewEditIndex;

        BindGridView();
    }
    protected void gvlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string addr = gvlist.Rows[e.RowIndex].Cells[0].Text.Trim();
        TextBox txtLine1 = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtLine1");
        TextBox txtLine2 = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtLine2");
        TextBox txtLine3 = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtLine3");
        TextBox txtCity = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtCity");
        TextBox txtState = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtState");
        TextBox txtZip = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtZip");
        TextBox txtCustCode = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txtCustCode");

        try
        {
            SqlParameter[] sqlParam = new SqlParameter[9];
            sqlParam[0] = new SqlParameter("@addr", addr);
            sqlParam[1] = new SqlParameter("@line1", txtLine1.Text.Trim());
            sqlParam[2] = new SqlParameter("@line2", txtLine2.Text.Trim());
            sqlParam[3] = new SqlParameter("@line3", txtLine3.Text.Trim());
            sqlParam[4] = new SqlParameter("@city", txtCity.Text.Trim());
            sqlParam[5] = new SqlParameter("@state", txtState.Text.Trim());
            sqlParam[6] = new SqlParameter("@zip", txtZip.Text.Trim());
            sqlParam[7] = new SqlParameter("@custCode", txtCustCode.Text.Trim());
            sqlParam[8] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[8].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateAddrEditList", sqlParam);

            if (!Convert.ToBoolean(sqlParam[8].Value.ToString()))
            {
                ltlAlert.Text = "alert('客户代码不存在！');";

                return;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('保存失败！请重新操作一次！');";
        }

        gvlist.EditIndex = -1;
        BindGridView();
    }
    protected void gvlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvlist.EditIndex = -1;

        BindGridView();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //将错误信息按cimload格式导出

        string path = Server.MapPath("..\\Excel\\adstmt.cim");

        if (File.Exists(path))
        {
            //删除文件
            File.Delete(path);
        }

        DataTable dt = getEdiData.GetNoAddrRows();

        if (dt != null && dt.Rows.Count > 0)
        {
            StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    writer.WriteLine(row["val"].ToString());
                }

                btnUpdateDB.Enabled = true;
            }
            catch
            {
                ltlAlert.Text = "alert('导出失败!');";
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }

            ltlAlert.Text = "window.open('/Excel/adstmt.cim','text','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }
    }
    protected void btnUpdateDB_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "Update EDI_DB..EdiPoHrd Set errmsg = '' Where errmsg <> '' and errmsg not like '价格不匹配%'";

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strSql);
        }
        catch
        { }

        BindGridView();
    }
}
