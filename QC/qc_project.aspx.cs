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

public partial class QC_qc_project : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }

    protected void GridViewBind()
    {
        ogv.GridViewDataBind(gvProject, oqc.GetProject(txtPro.Text.Trim(), 0));
    }

    protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                id = gvProject.PageIndex * 17 + id;
                e.Row.Cells[0].Text = id.ToString();
            }

            //bind checkbox
            string str = gvProject.DataKeys[e.Row.RowIndex].Values[1].ToString();
            if (str != string.Empty && str != ";nbsp")
            {
                try
                {
                    CheckBox chk = (CheckBox)e.Row.Cells[2].FindControl("CheckBox1");
                    chk.Checked = bool.Parse(str);
                }
                catch
                {
                    ;
                }
            }

            DropDownList dType = (DropDownList)e.Row.Cells[2].FindControl("dType");

            dType.DataSource = oqc.GetDefectType(string.Empty,4);
            dType.DataBind();

            dType.Items.Insert(0, new ListItem("--", "0"));

            dType.SelectedValue = gvProject.DataKeys[e.Row.RowIndex].Values[2].ToString();

            dType.Enabled = false;
        }
    }
    protected void gvProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intProID = int.Parse(gvProject.DataKeys[e.RowIndex].Values[0].ToString());

        //delete Project
        oqc.DeleteProject(intProID);

        GridViewBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    string Conditions 
    {
        get 
        {
            string strSql = " where proDelete = 0 ";

            if (txtPro.Text.Trim() != string.Empty)
            {
                strSql += " And proName Like N'%" + txtPro.Text.Trim() + "%'";
            }
            return strSql;
        }
    }
    protected void gvProject_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        gvProject.EditIndex = index;

        GridViewBind();

        DropDownList dType = (DropDownList)gvProject.Rows[index].Cells[2].FindControl("dType");

        dType.Enabled = true;
    }
    protected void gvProject_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProject.EditIndex = -1;

        GridViewBind();
    }
    protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProject.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void gvProject_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int intProID = int.Parse(gvProject.DataKeys[e.RowIndex].Values[0].ToString());
        string strProName = ((TextBox)gvProject.Rows[e.RowIndex].Cells[1].FindControl("txtPro")).Text.Trim();
        DropDownList dType = (DropDownList)gvProject.Rows[e.RowIndex].Cells[2].FindControl("dType");

        string strMsg = string.Empty;
        FuncErrType errtype = FuncErrType.操作成功;

        TextBox tMin = (TextBox)gvProject.Rows[e.RowIndex].Cells[3].FindControl("txtMin");
        TextBox tMax = (TextBox)gvProject.Rows[e.RowIndex].Cells[3].FindControl("txtMax");
        TextBox tNum = (TextBox)gvProject.Rows[e.RowIndex].Cells[3].FindControl("txtNum");
        TextBox tAc = (TextBox)gvProject.Rows[e.RowIndex].Cells[3].FindControl("txtAc");
        TextBox tRe = (TextBox)gvProject.Rows[e.RowIndex].Cells[3].FindControl("txtRe");

        if (tMin.Text.Trim() == string.Empty || tMax.Text.Trim() == string.Empty || tNum.Text.Trim() == string.Empty || tAc.Text.Trim() == string.Empty || tRe.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('不能有空项!');";
            return;
        }

        try
        {
            int n1 = int.Parse(tMin.Text.Trim());
            int n2 = int.Parse(tMax.Text.Trim());
            int n3 = int.Parse(tNum.Text.Trim());
            int n4 = int.Parse(tAc.Text.Trim());
            int n5 = int.Parse(tRe.Text.Trim());

            if (n1 != 0 && n2 != 0 && n1 == n2) 
            {
                ltlAlert.Text = "alert('当Min值不为0时,Min值不能等于Max值!');";
                return;
            }
            if (n1 == 0 && n2 == 0 && (n3 != 0 || n4 != 0 || n5 != 0))
            {
                ltlAlert.Text = "alert('当Min值和Max值均为0时,系统已默认按国标检验;请将检验数量、Ac和Re值置为0!');";
                return;
            }
        }
        catch 
        {
            ltlAlert.Text = "alert('填写的必须为整数!');";
            return;
        }

        int intMin = int.Parse(tMin.Text.Trim());
        int intMax = int.Parse(tMax.Text.Trim());
        int intNum = int.Parse(tNum.Text.Trim());
        int intRe = int.Parse(tAc.Text.Trim());
        int intAc = int.Parse(tRe.Text.Trim());

        if (intMin > intMax) 
        {
            ltlAlert.Text = "alert('Min不能大于Max');";
            return;
        }

        if (strProName.Trim() == "<新项目名称>")
        {
            ltlAlert.Text = "alert('请对重新命名项目名称');";
            return;
        }

        if (dType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项类别');";
            return;
        }

        errtype = oqc.ModifyProject(intProID, strProName, intMin, intMax, intNum, intRe, intAc, int.Parse(Session["uID"].ToString()),int.Parse(dType.SelectedValue));

        if (errtype != FuncErrType.操作成功)
        {
            ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            return;
        }

        gvProject.EditIndex = -1;

        GridViewBind();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (txtPro.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('项目名称不能为空!');";
            return;
        }

        string strMsg = string.Empty;

        oqc.AddProject(txtPro.Text.Trim(), int.Parse(Session["uID"].ToString()), ref strMsg);

        if (strMsg != string.Empty)
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }

        gvProject.PageIndex = 0;

        GridViewBind();

        //gvProject_RowEditing(this, new GridViewEditEventArgs(0));
    }
    protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            LinkButton source = (LinkButton)e.CommandSource;

            int index = ((GridViewRow)(source.Parent.Parent)).RowIndex;

            Response.Redirect("qc_project_item.aspx?pro=" + ((Label)gvProject.Rows[index].Cells[1].FindControl("Label1")).Text.ToString().Trim());
        }
    }
}
