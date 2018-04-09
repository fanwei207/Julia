using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class part_Npart_partTypeEnumManager : BasePage
{
    Npart_help help = new Npart_help();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidMstrID.Value = Request.QueryString["mstrID"];
            hidDetId.Value = Request.QueryString["detId"];

            bind();
        }
    }

    private void bind()
    {
        string mstrID = hidMstrID.Value;
        string detID = hidDetId.Value;

        gvDet.DataSource = help.selectTypeDetEnumByID(mstrID, detID);
        gvDet.DataBind();
    }
    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string isCanUpdate = gvDet.DataKeys[e.Row.RowIndex].Values["isCanUpdate"].ToString();
            if (isCanUpdate != "True")
            {
                ((LinkButton)e.Row.FindControl("lkbDelete")).Visible = false;

            }

        }
    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbDelete")
        {

            string mstrID = hidMstrID.Value;

            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string ID = gvDet.DataKeys[index].Values["ID"].ToString();

            string flag = help.deleteTypeDetEnumByID(ID, Session["uID"].ToString(), Session["uName"].ToString());

            if (flag.Equals("-1"))
            {
                Alert("已经使用不可删除");
                return;
            }
            else if (flag.Equals("0"))
            {
                Alert("删除失败，请联系管理员");
                return;
            }
            else
            {
                Alert("删除成功");
                bind();
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string mstrID = hidMstrID.Value;
        string detID = hidDetId.Value;
        string enumValue = txtEnum.Text.Trim();
        string repValue = txtReplace.Text.Trim();

        string flag = help.addTypeDetEnum(mstrID, detID, enumValue,repValue, Session["uID"].ToString(), Session["uName"].ToString());

        if (flag == "-1")
        {
            Alert("存在相同的枚举值请重新输入");
            return;
        }
        else if (flag == "0")
        {
            Alert("新增失败,请联系管理员");
            return;
        }
        else
        {
            Alert("更新成功");
            bind();
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEnum.Text = string.Empty;
        txtReplace.Text = string.Empty;
    }
}