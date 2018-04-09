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
using System.Collections.Generic;
using System.Data.Odbc;
using QCProgress;
using System.IO;

public partial class wo2_wo2_routingEdit2 : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MopProcBind();
            DataGridBind();
        }
    }
    private DataSet GetRouting()
    {
        try
        {
            string strSql = "sp_wo2_selectRouting";

            SqlParameter[] parmArray = new SqlParameter[3];
            parmArray[0] = new SqlParameter("@routing", txtRouting.Text.Trim());
            parmArray[1] = new SqlParameter("@moptype", dropMopProc.SelectedValue);
            parmArray[2] = new SqlParameter("@all", chk.Checked);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
        }
        catch
        {
            return null;
        }
    }
    private void DataGridBind()
    {
        DataTable table = GetMopProcByType();

        for (int i = 1; i < 9; i++)
        {
            dgRouting.Columns[i].HeaderText = String.Empty;
        }

        for (int row = 0; row < table.Rows.Count; row++)
        {
            dgRouting.Columns[row + 1].HeaderText = table.Rows[row][0].ToString();
        }

        dgRouting.DataSource = GetRouting();
        dgRouting.DataBind();
    }
    private DataTable GetMopProcByType()
    {
        try
        {
            string strSql = "sp_wo2_selectMopProcByType";

            SqlParameter[] parmArray = new SqlParameter[1];
            parmArray[0] = new SqlParameter("@moptype", dropMopProc.SelectedValue);

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

            return ds.Tables[0];
        }
        catch
        {
            return null;
        }
    }
    private void MopProcBind()
    {
        listMop.DataSource = GetMopProcByType();
        listMop.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtRouting.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工艺代码不能为空!');";
            return;
        }

        //判断加工时间格式，必须都是浮点数
        foreach (DataListItem item in listMop.Items)
        {
            TextBox txtRun = (TextBox)item.FindControl("txtRun");

            if (txtRun.Text.Trim() != String.Empty)
            {
                try
                {
                    Double d = Convert.ToDouble(txtRun.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('加工时间必须为浮点数!');";
                    return;
                }
            }
        }

        //写入数据库
        foreach (DataListItem item in listMop.Items)
        {
            TextBox txtMopProc = (TextBox)item.FindControl("txtMopProc");
            TextBox txtRun = (TextBox)item.FindControl("txtRun");

            Decimal dRun = 0;

            if (txtRun.Text.Trim() != String.Empty)
                dRun = Convert.ToDecimal(txtRun.Text.Trim());

            try
            {
                string strSql = "sp_wo2_updateRouting";

                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@routing", txtRouting.Text.Trim());
                parmArray[1] = new SqlParameter("@mopproc", txtMopProc.Text.Trim());
                parmArray[2] = new SqlParameter("@run", dRun);
                parmArray[3] = new SqlParameter("@uID", Session["uID"].ToString());

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                DataGridBind();
            }
            catch
            {
                ;
            }
        }

    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        DataGridBind();
    }
    protected void dgRouting_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblDiff = (Label)e.Item.Cells[11].FindControl("lblDiff");

            if (lblDiff.Text.Trim() != String.Empty && lblDiff.Text.Trim() != "&nbsp;")
            {
                if (Convert.ToDecimal(lblDiff.Text.Trim()) == 0)
                    lblDiff.Text = "0";
            }
        }
        else if (e.Item.ItemType == ListItemType.Header)
        {
            ((CheckBox)e.Item.Cells[11].FindControl("chkDiff")).Checked = chk.Checked;
        }
    }
    protected void dgRouting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgRouting.CurrentPageIndex = e.NewPageIndex;
        DataGridBind();
    }
    protected void dropMopProc_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRouting.Text = String.Empty;
        foreach (DataListItem item in listMop.Items)
        {
            TextBox txtRun = (TextBox)item.FindControl("txtRun");
            txtRun.Text = String.Empty;
        }

        MopProcBind();
        DataGridBind();
    }
    protected void dgRouting_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string strSql = "sp_wo2_deleteRouting";

            SqlParameter[] parmArray = new SqlParameter[1];
            parmArray[0] = new SqlParameter("@routing", dgRouting.Items[e.Item.ItemIndex].Cells[0].Text);

            Boolean bRet = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));

            if (bRet)
                ltlAlert.Text = "alert('该工序已被别处引用，不允许删除');";

            txtRouting.Text = String.Empty;

            DataGridBind();
        }
        catch
        {
            ;
        }
    }
    protected void dgRouting_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            int nIndex = ((DataGridItem)(((LinkButton)e.CommandSource).Parent.Parent)).ItemIndex;

            txtRouting.Text = dgRouting.Items[nIndex].Cells[0].Text;

            foreach (DataListItem item in listMop.Items)
            {
                TextBox txtRun = (TextBox)item.FindControl("txtRun");
                String strRun = dgRouting.Items[nIndex].Cells[item.ItemIndex + 1].Text;

                if (strRun == "&nbsp;")
                    strRun = String.Empty;

                txtRun.Text = strRun;
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtRouting.Text = String.Empty;
        foreach (DataListItem item in listMop.Items)
        {
            TextBox txtRun = (TextBox)item.FindControl("txtRun");
            txtRun.Text = String.Empty;
        }

        dgRouting.CurrentPageIndex = 0;
        DataGridBind();
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        //更改为NPOI导出方法
        //ltlAlert.Text = "window.open('wo2_routingExport2.aspx?tp=" + dropMopProc.SelectedValue + "&ro=" + txtRouting.Text.Trim() + "&rm=" + DateTime.Now.ToString() + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0')";      

        #region  打印Excel标题
        string routingHeader1 = "";
        string routingHeader2 = "";
        string routingHeader3 = "";
        string routingHeader4 = "";
        string routingHeader5 = "";
        string routingHeader6 = "";
        string routingHeader7 = "";
        string routingHeader8 = "";
        int rowIndex = 1;
        DataTable routingHeader = GetHeader();
        foreach (DataRow row in routingHeader.Rows)
        {
            if (rowIndex == 1)
            {
                routingHeader1 = row["wo2_mop_procname"].ToString();
            }
            if (rowIndex == 2)
            {
                routingHeader2 = row["wo2_mop_procname"].ToString();
            }
            if (rowIndex == 3)
            {
                routingHeader3 = row["wo2_mop_procname"].ToString();
            }
            if (rowIndex == 4)
            {
                routingHeader4 = row["wo2_mop_procname"].ToString();
            }
            if (rowIndex == 5)
            {
                routingHeader5 = row["wo2_mop_procname"].ToString();
            }
            if (rowIndex == 6)
            {
                routingHeader6 = row["wo2_mop_procname"].ToString();
            }
            if (rowIndex == 7)
            {
                routingHeader7 = row["wo2_mop_procname"].ToString();
            }
            if (rowIndex == 8)
            {
                routingHeader8 = row["wo2_mop_procname"].ToString();
            }
            rowIndex++;
        }

        routingHeader.Dispose();
        #endregion

        DataTable dt = GetRouting().Tables[0];
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }
        string title = "120^<b>工艺代码</b>~^70^<b>" + routingHeader1 + "</b>~^70^<b>" + routingHeader2 + "</b>~^70^<b>" + routingHeader3 + "</b>~^70^<b>" + routingHeader4 + "</b>~^70^<b>" + routingHeader5 + "</b>~^70^<b>" + routingHeader6 + "</b>~^70^<b>" + routingHeader7 + "</b>~^70^<b>" + routingHeader8 + "</b>~^70^<b>100合计</b>~^70^<b>QAD合计</b>~^70^<b>差异</b>~^";
        this.ExportExcel(title, dt, false);   
    }

    protected DataTable GetHeader()
    {
        try
        {
            string strSql = "sp_wo2_selectMopProcByType";

            SqlParameter[] parmArray = new SqlParameter[1];
            parmArray[0] = new SqlParameter("@moptype", dropMopProc.SelectedValue);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void chkDiff_CheckedChanged(object sender, EventArgs e)
    {
        chk.Checked = ((CheckBox)sender).Checked;

        DataGridBind();
    }
}
