using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using RD_WorkFlow;

public partial class RDW_RDW_FromDocs : System.Web.UI.Page 
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDllCategory();
            BindDllType(); 
        }
    }

    /// <summary>
    /// 选的是Type
    /// </summary>
    private void BindDllCategory()
    {
        DataTable dt = rdw.getCategoryInfo(ddl_Type.SelectedValue);
        ddl_Category.DataSource = dt;
        ddl_Category.DataBind();

        ListItem item1 = new ListItem("--", "0");
        ddl_Category.Items.Insert(0, item1);
    }

    /// <summary>
    /// 选的是Category
    /// </summary>
    private void BindDllType()
    {
        DataTable dt = rdw.getTypeInfo();
        ddl_Type.DataSource = dt;
        ddl_Type.DataBind();

        ListItem item1 = new ListItem("--", "0");
        ddl_Type.Items.Insert(0, item1);

    }

    private void bindgv_allDoc()
    {
        int iTypeid = Convert.ToInt32(ddl_Type.SelectedValue);
        int iCategoryid = Convert.ToInt32(ddl_Category.SelectedValue);
        string strKeyWordSearch = txt_KeyWordSearch.Text.ToString().Trim();

        DataTable dt = rdw.getDocbyType(iTypeid, iCategoryid, strKeyWordSearch);

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gv_allDoc.DataSource = dt;
            gv_allDoc.DataBind();
            int columnCount = gv_allDoc.Rows[0].Cells.Count;
            gv_allDoc.Rows[0].Cells.Clear();
            gv_allDoc.Rows[0].Cells.Add(new TableCell());
            gv_allDoc.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv_allDoc.Rows[0].Cells[0].Text = "No document in this type!";
            gv_allDoc.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gv_allDoc.DataSource = dt;
            gv_allDoc.DataBind();
        }

    }
    protected void gv_allDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_allDoc.PageIndex = e.NewPageIndex;
        bindgv_allDoc();
    }
    protected void gv_allDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "myAddlink")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string docid = gv_allDoc.DataKeys[index][0].ToString();
            string Path = gv_allDoc.DataKeys[index][4].ToString().Trim();
            string FileName = gv_allDoc.DataKeys[index][5].ToString().Trim();

            if (File.Exists(Server.MapPath(Path + FileName)))
            {
                if (rdw.addBosDetDocLink(docid, Convert.ToString(Request.QueryString["did"]), Convert.ToString(Session["uID"])))
                {
                    ltlAlert.Text = "alert('Document Add Success!');";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('Document Add Failed!');";
                    return;
                }
            }
            else
            {
                ltlAlert.Text = "alert('Can not Find File!');";
                return;
            }
        }
    }

    protected void ddl_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDllCategory();
    }

    protected void ddl_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgv_allDoc();
    }

    protected void btn_DocSearch_Click(object sender, EventArgs e)
    {
        if (ddl_Category.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('Please Choose Document Category!');";
            return;
        }
        else
        {
            bindgv_allDoc();
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
    }
}