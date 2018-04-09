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
using QCProgress;

public partial class QC_qc_process_procedure : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropType.DataSource = oqc.GetDefectType(string.Empty, 2);
            dropType.DataBind();

            dropType.Items.Insert(0, new ListItem("--", "0"));

            GridViewBind();
        }
    }

    protected void GridViewBind() 
    {
        string strName = txtName.Text.Trim();
        int tID = int.Parse(dropType.SelectedValue);

        ogv.GridViewDataBind(gvProcedure, oqc.GetProcedure(strName, tID));
    }

    protected void gvProcedure_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        gvProcedure.EditIndex = index;

        DataTable table = oqc.GetProcedure(txtName.Text.Trim(),int.Parse(dropType.SelectedValue));

        ogv.GridViewDataBind(gvProcedure, table);

        if (table.Rows.Count > 0)
        {
            DropDownList dType = (DropDownList)gvProcedure.Rows[index].Cells[2].FindControl("dType");

            dType.Enabled = true;
        }
    }
    protected void gvProcedure_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProcedure.EditIndex = -1;

        GridViewBind();
    }
    protected void gvProcedure_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //order
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                id = gvProcedure.PageIndex * 17+id;
                e.Row.Cells[0].Text = id.ToString();
            }

            DropDownList dType = (DropDownList)e.Row.Cells[2].FindControl("dType");

            dType.DataSource = oqc.GetDefectType(string.Empty,2);
            dType.DataBind();

            dType.Items.Insert(0, new ListItem("--", "0"));

            dType.SelectedValue = gvProcedure.DataKeys[e.Row.RowIndex].Values[1].ToString();

            dType.Enabled = false;
        }
    }
    protected void gvProcedure_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        FuncErrType errtype = FuncErrType.操作成功;

        int ID = int.Parse(gvProcedure.DataKeys[e.RowIndex].Values[0].ToString());
        string strName = ((TextBox)gvProcedure.Rows[e.RowIndex].Cells[1].FindControl("txtProcedure")).Text.Trim();
        DropDownList dType = (DropDownList)gvProcedure.Rows[e.RowIndex].Cells[2].FindControl("dType");
        TextBox txtOrder = (TextBox)gvProcedure.Rows[e.RowIndex].Cells[4].FindControl("txtOrder");

        if (strName == string.Empty)
        {
            ltlAlert.Text = "alert('工序名称不能为空');";
            return;
        }

        if (dType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项类别');";
            return;
        }

        if (txtOrder.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请填写排序');";
            return;
        }
        else 
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!regex.IsMatch(txtOrder.Text.Trim())) 
            {
                ltlAlert.Text = "alert('排序只能是整数');";
                return;
            }
        }

        int nOrder = int.Parse(txtOrder.Text.Trim());

        errtype = oqc.ModifyProcedure(ID, int.Parse(dType.SelectedValue), strName, int.Parse(Session["uID"].ToString()), nOrder);

        if (errtype != FuncErrType.操作成功)
        {
            ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            return;
        }

        gvProcedure.EditIndex = -1;

        GridViewBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool bFlag = true;
        
        if (txtName.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('名称不能为空!');";
            bFlag = false;
        }
        if (dropType.SelectedIndex == 0) 
        {
            ltlAlert.Text = "alert('请选择一项类别!');";
            bFlag = false;
        }

        string strName = txtName.Text;
        int tID = int.Parse(dropType.SelectedValue);
        int uID = int.Parse(Session["uID"].ToString());
        

        FuncErrType errtype = FuncErrType.操作成功;

        if (bFlag)
        {
            errtype = oqc.AddProcedure(strName, tID, uID);
            if (errtype != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + errtype.ToString() + "')";
                return;
            }

            gvProcedure.PageIndex = 0;
        }

        txtName.Text = string.Empty;
        dropType.SelectedIndex = 0;

        GridViewBind();
    }
    protected void gvProcedure_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strMsg = "";
        FuncErrType errtype = FuncErrType.操作成功;
        int ID = int.Parse(gvProcedure.DataKeys[e.RowIndex].Values[0].ToString());

        errtype = oqc.DeleteProcedure(ID, ref strMsg);
        if (errtype != FuncErrType.操作成功)
        {
            ltlAlert.Text = "alert('" + errtype.ToString() + "')";
            return;
        }

        GridViewBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gvProcedure_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProcedure.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
}
