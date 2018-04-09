using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class part_Npart_partTypeForQADManager : BasePage
{
    Npart_help help = new Npart_help();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidMstrID.Value = Request.QueryString["mstrID"].ToString();

            bind();
        }
    }

    private void bind()
    {
        txtEnum.Text = string.Empty;
        txtRep.Text = string.Empty;
        txtEnumEnglish.Text = string.Empty;
        txtQADin.Text = string.Empty;

        string mstrID = hidMstrID.Value;

        gvDet.DataSource = help.selectTypeQADByMstrID(mstrID);
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

            string detID = gvDet.DataKeys[index].Values["ID"].ToString();

            string flag = help.deleteTypeForQADByID(detID,Session["uID"].ToString(),Session["uName"].ToString());

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

        string typeName = txtEnum.Text.Trim();

        string typeRepName = txtRep.Text.Trim();

        string typeNameEnglish = txtEnumEnglish.Text.Trim();


        if (string.Empty.Equals(typeName))
        {
            Alert("列名禁止为空");
            return;
        }
        if (string.Empty.Equals(typeNameEnglish))
        {
            Alert("列名禁止为空");
            return;
        }

        string QADin = txtQADin.Text.Trim();

        int QADinInt = 0;
        if (string.Empty.Equals(QADin))
        {
            Alert("相关QAD不能为空");
            return;
        }

        if (int.TryParse(QADin, out QADinInt))
        {
            if (QADinInt < 0)
            {
                Alert("顺序必须是正整数");
                return;
            }
        }
        else
        {
            Alert("顺序列请输入整数");
            return;
        }

        string flag = help.addTypeForQAD(mstrID, typeName,typeRepName,typeNameEnglish, QADin, Session["uID"].ToString(), Session["uName"].ToString());

        if (flag.Equals("-1"))
        {
            Alert("类型名不能重复");
            return;
        }
        else if (flag.Equals("-2"))
        {
            Alert("类型英文名不能重复");
            return;
        }
        else if (flag.Equals("0"))
        {
            Alert("删除失败，请联系管理员");
            return;
        }
        else
        {
            Alert("新增成功");
            bind();
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEnum.Text = string.Empty;
        txtEnumEnglish.Text = string.Empty;
        txtRep.Text = string.Empty;
        txtQADin.Text = string.Empty;
    }
}