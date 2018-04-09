using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;

public partial class Supplier_admin_docType : System.Web.UI.Page
{
    private string ids
    {
        get
        {
            if (ViewState["ids"] == null)
            {
                ViewState["ids"] = ""
        ;
            }
            return ViewState["ids"].ToString();
        }
        set
        {
            ViewState["ids"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Databind();
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        Databind();
    }
    public void Databind()
    {
        gv.DataSource = ShareDocument.getDocType(txbExt.Text,txbName.Text);
        gv.DataBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mydelete")
        {
            if (ShareDocument.deleteDocumentType(e.CommandArgument.ToString()))
            {
                ltlAlert.Text = "alert('删除成功！')";
                txbExt.Text = string.Empty;
                txbName.Text = string.Empty;
                Databind();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败！')";
            }

        }
        if (e.CommandName == "myupdate")
        {

            ids = e.CommandArgument.ToString();
            SqlDataReader reader = ShareDocument.getDocType(ids);
            btnSave.Visible = true;
            btnAdd.Visible = false;
            while (reader.Read())
            {

                txbExt.Text = reader["doc_ext"].ToString();
                txbName.Text = reader["doc_name"].ToString();


            }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Databind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txbExt.Text == null || txbExt.Text == string.Empty)
        {
            ltlAlert.Text = "alert('格式代码不能为空！')";
        }
        else if (txbName.Text == null || txbName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('格式名称不能为空！')";
        }
        else
        {
            int stu = ShareDocument.InsertDocumentType(txbExt.Text, txbName.Text, Session["uID"].ToString(), Session["uName"].ToString());
            if (stu == 3)
            {
                ltlAlert.Text = "alert('格式代码必须为数字！')";
            }
            else if (stu == 0)
            {
                ltlAlert.Text = "alert('新增失败！')";
            }
            else if (stu == 2)
            {
                ltlAlert.Text = "alert('格式代码已存在，不可重复添加！')";
            }
            else
            {
               
                ltlAlert.Text = "alert('新增成功！')";
                txbExt.Text = string.Empty;
                txbName.Text = string.Empty;
                Databind();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txbExt.Text.Trim() == null || txbExt.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('格式代码不能为空！')";
        }
        else if (txbName.Text.Trim() == null || txbName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('格式名称不能为空！')";
        }
        else
        {
            int stutes = ShareDocument.UpdateDocumentType(ids, txbExt.Text, txbName.Text, Session["uID"].ToString(), Session["uName"].ToString());

            if (stutes == 2)
            {
                ltlAlert.Text = "alert('格式代码已存在，不可重复！')";
            }

            else if (stutes == 1)
            {
                ltlAlert.Text = "alert('修改成功！')";
                txbExt.Text = string.Empty;
                txbName.Text = string.Empty;
                btnSave.Visible = false;
                btnAdd.Visible = true;
                Databind();
            }
            else if (stutes == -1)
            {
                ltlAlert.Text = "alert('修改失败！')";
            }
            else if (stutes == 2)
            {
                ltlAlert.Text = "alert('格式代码已存在，不可重复添加！')";
            }
            else
            {
                ltlAlert.Text = "alert('格式代码必须为数字！')";
            }
        }

    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        txbExt.Text = string.Empty;
        txbName.Text = string.Empty;
        btnSave.Visible = false;
        btnAdd.Visible = true;
        Databind();
    }
}