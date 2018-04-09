using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class upcProgram_barcodeimport : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        try
        {
            string strSql = "JDE_Data.dbo.sp_selectBarCodeTemp";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@uID", Session["uID"].ToString());

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

            dgCodeTemp.DataSource = ds;
            dgCodeTemp.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (filename.PostedFile.FileName == string.Empty)
        {
            ltlAlert.Text = "alert('请选择要导入的文件');";
            return;
        }
        else
        {
            string strFileName = filename.PostedFile.FileName.Trim();
            string strExt = strFileName.Substring(strFileName.LastIndexOf('.') + 1);

            if (strExt.ToUpper() != "XLS")
            {
                ltlAlert.Text = "alert('输入文件的格式不正确');";
                return;
            }
            else
            {
                if (filename.PostedFile.ContentLength <= 0 || filename.PostedFile.ContentLength >= 5 * 1024 * 1024)
                {
                    ltlAlert.Text = "alert('输入文件的大小在5M以内');";
                    return;
                }
            }
        }

        string strServerFile = Server.MapPath("/import") + "\\barcode_" + Session["uID"].ToString() + ".xls";
        try
        {
            filename.PostedFile.SaveAs(strServerFile);
        }
        catch
        {
            ltlAlert.Text = "alert('文件上传失败');";
            return;
        }

        DataTable table;
        try
        {
            table = this.GetExcelContents(strServerFile);//adam.getExcelContents(strServerFile).Tables[0];
        }
        catch (Exception ee)
        {
            //throw new Exception(str + ee.ToString() + filename.PostedFile.FileName);
            ltlAlert.Text = "alert('读取Excel文件失败');";
            return;
        }
        if (table.Rows.Count > 0)
        {
            if (table.Columns[0].ColumnName.Replace(" ", "").Trim().ToLower() != "itemnumber" ||
                table.Columns[1].ColumnName.Replace(" ", "").Trim().ToLower() != "itemdescription" ||
                table.Columns[2].ColumnName.Replace(" ", "").Trim().ToLower() != "upc" ||
                table.Columns[3].ColumnName.Replace(" ", "").Trim().ToLower() != "innerpacki2of5" ||
                table.Columns[4].ColumnName.Replace(" ", "").Trim().ToLower() != "masterpacki2of5")
            {
                ltlAlert.Text = "alert('上传的文件格式不正确');";
                return;
            }

            string strSql = string.Empty;

            foreach (DataRow row in table.Rows)
            {
                if (row[2].ToString() != string.Empty)
                {
                    if (row[0].ToString().Contains("e+"))
                    {
                        strSql += " Insert Into JDE_Data.dbo.barcodetemp(bc_item, bc_desc, bc_upc, bc_ipi, bc_mpi, bc_crt_user, bc_crt_date)";
                        strSql += "Values(Cast(" + row[0].ToString() + " As bigint), '" + row[1].ToString() + "', '" + row[2].ToString() + "', '" + row[3].ToString() + "', '" + row[4].ToString() + "', " + Session["uID"].ToString() + ", GetDate()) ";
                    }
                    else
                    {
                        strSql += " Insert Into JDE_Data.dbo.barcodetemp(bc_item, bc_desc, bc_upc, bc_ipi, bc_mpi, bc_crt_user, bc_crt_date)";
                        strSql += "Values('" + row[0].ToString() + "', '" + row[1].ToString() + "', '" + row[2].ToString() + "', '" + row[3].ToString() + "', '" + row[4].ToString() + "', " + Session["uID"].ToString() + ", GetDate()) ";
                    }
                }
            }

            if (strSql.Length != 0)
            {
                try
                {
                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSql);
                }
                catch
                {
                    ltlAlert.Text = "alert('导入数据库失败');";
                    return;
                }
            }
        }

        BindData();
    }
    protected void dgCodeTemp_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgCodeTemp.EditItemIndex = e.Item.ItemIndex;

        BindData();
    }
    protected void dgCodeTemp_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgCodeTemp.EditItemIndex = -1;

        BindData();
    }
    protected void dgCodeTemp_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        TextBox tItem = (TextBox)e.Item.Cells[0].Controls[0];
        TextBox tDesc = (TextBox)e.Item.Cells[1].Controls[0];
        string strUpc = e.Item.Cells[2].Text.Trim();

        try
        {
            string strSql = "JDE_Data.dbo.sp_updateBarCodeTemp";

            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@item", tItem.Text.Trim());
            sqlParam[1] = new SqlParameter("@desc", tDesc.Text.Trim());
            sqlParam[2] = new SqlParameter("@upc", strUpc);
            sqlParam[3] = new SqlParameter("@uID", Session["uID"].ToString());

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            throw new Exception("更新失败");
        }

        dgCodeTemp.EditItemIndex = -1;
        BindData();
    }
    protected void dgCodeTemp_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string strSql = "JDE_Data.dbo.sp_deleteBarCodeTemp";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@upc", e.Item.Cells[2].Text.Trim());
            sqlParam[1] = new SqlParameter("@uID", Session["uID"].ToString());

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            throw new Exception("删除失败");
        }

        BindData();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "JDE_Data.dbo.sp_confirmBarCodeTemp";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@uID", Session["uID"].ToString());

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            throw new Exception("确认失败");
        }

        this.Response.Redirect("Barcodeedit.aspx");
    }

    protected void dgCodeTemp_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgCodeTemp.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
}
