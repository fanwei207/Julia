using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SampleManagement;

public partial class supplier_SampleReceiveNotesAccDoc : BasePage
{
    Sample sap = new Sample();
    String strbosNbr;
    Int32 ibosDetLine;
    String strBosDetCode;
    String strbosDetQad;

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("320120", "打样单新增权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            strbosNbr = Request.QueryString["strNbr"].ToString().Trim();
            ibosDetLine = Convert.ToInt32(Request.QueryString["line"]);
            strBosDetCode = Server.UrlDecode(Request.QueryString["code"].ToString());
            strbosDetQad = Request.QueryString["qad"].ToString();
            txt_bosnbr.Text = strbosNbr;
            lbl_line.Text = Request.QueryString["line"];
            lbl_bosCode.Text = strBosDetCode;
            if (strbosDetQad != string.Empty)
            {
                lbl_bosQad.Text = "QAD号:  " + strbosDetQad;
                BindQAdDoc();
            }

            chk_EditPermisson.Enabled = this.Security["320120"].isValid;
            tbID_DocAdd.Visible = chk_EditPermisson.Enabled;
            if (Request.QueryString["Mode"].ToString() != "Maintain")
            {
                tbID_DocAdd.Visible = false;
            }
             
            if (tbID_DocAdd.Visible && chk_EditPermisson.Enabled)
            {
                strbosNbr = Request.QueryString["strNbr"].ToString().Trim();
                DataTable dt = sap.getBosMstr(strbosNbr, "0");
                chk_isVendConfirm.Checked = Convert.ToBoolean(dt.Rows[0]["bos_vendIsConfirm"].ToString());
                if (Convert.ToBoolean(dt.Rows[0]["bos_vendIsConfirm"].ToString()))
                {
                    tbID_DocAdd.Visible = false;
                }
            }

            BindDllCategory();
            BindDllType();
            Bindgv_BosRelateDoc();
            BindgvVendImportDoc();
        }
    }

    private void BindgvVendImportDoc()
    {
        string bosNbr = txt_bosnbr.Text;
        int line = Convert.ToInt32(lbl_line.Text);
        DataTable dt = sap.getBosSuppImportDocs(bosNbr, line);
        if (dt.Rows.Count == 0)
        {
            gvlist.Visible = false;
        }
        else
        {
            gvlist.DataSource = dt;
            gvlist.DataBind();
        }
    }

    private void Bindgv_BosRelateDoc()
    {
        strbosNbr = txt_bosnbr.Text.ToString();
        ibosDetLine = Convert.ToInt32(lbl_line.Text.ToString());
        DataTable dt_detdoc = sap.getBosDetDoc(strbosNbr, ibosDetLine);
        gv_relateDoc.Columns[9].Visible = chk_EditPermisson.Enabled;
        if (chk_EditPermisson.Enabled)
        {
            gv_relateDoc.Columns[9].Visible = !chk_isVendConfirm.Checked;
        } 
        gv_relateDoc.DataSource = dt_detdoc;
        gv_relateDoc.DataBind();

    }

    private void bindgv_allDoc()
    {
        int iTypeid = Convert.ToInt32(ddl_Type.SelectedValue);
        int iCategoryid = Convert.ToInt32(ddl_Category.SelectedValue);
        string strKeyWordSearch = txt_KeyWordSearch.Text.ToString().Trim();

        DataTable dt = sap.getDocbyType(iTypeid, iCategoryid, strKeyWordSearch);

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gv_allDoc.DataSource = dt;
            gv_allDoc.DataBind();
            int columnCount = gv_allDoc.Rows[0].Cells.Count;
            gv_allDoc.Rows[0].Cells.Clear();
            gv_allDoc.Rows[0].Cells.Add(new TableCell());
            gv_allDoc.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv_allDoc.Rows[0].Cells[0].Text = "此分类没有文档记录";
            gv_allDoc.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gv_allDoc.DataSource = dt;
            gv_allDoc.DataBind();
        }

    }

    /// <summary>
    /// 选的是Type
    /// </summary>
    private void BindDllCategory()
    {
        DataTable dt = sap.getCategoryInfo(ddl_Type.SelectedValue);
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
        DataTable dt = sap.getTypeInfo();
        ddl_Type.DataSource = dt;
        ddl_Type.DataBind();

        ListItem item1 = new ListItem("--", "0");
        ddl_Type.Items.Insert(0, item1);

    }

    private void BindQAdDoc()
    {
        strbosDetQad = Request.QueryString["qad"].ToString();
        string uType = "Administrator";  //在这里对应的文档只要有权限进入此页面,那就可以看到相关文档
        int iUserId = Convert.ToInt32(Session["uID"]);
        string bosnbr = txt_bosnbr.Text.Trim();

        DataTable dt_doc = PartInfo.getDocInfoByItem(strbosDetQad, uType, iUserId, bosnbr);
        if (dt_doc.Rows.Count == 0)
        {
            tr_gv_bos_Qaddoc.Visible = false;
        }
        else
        {
            tr_gv_bos_Qaddoc.Visible = true; 
            gv_bos_Qaddoc.DataSource = dt_doc;
            gv_bos_Qaddoc.DataBind();
        }
    }

    protected void gv_bos_Qaddoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string accFileName = gv_bos_Qaddoc.DataKeys[e.Row.RowIndex].Values["accFileName"].ToString();
            if (accFileName.Length == 0)
            {
                e.Row.Cells[8].Text = string.Empty;
            }
        }
    }
    protected void gv_bos_Qaddoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_bos_Qaddoc.PageIndex = e.NewPageIndex;
        BindQAdDoc();
    }
    protected void gv_bos_Qaddoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = Convert.ToInt32(e.CommandArgument.ToString());

        if (e.CommandName.ToString() == "myView")
        {
            //打开设计文件，即所谓的源文件
            if (Convert.ToBoolean(gv_bos_Qaddoc.DataKeys[intRow].Values["isNewMechanism"].ToString()))
            {
                ltlAlert.Text = "var w=window.open('/TecDocs/" + gv_bos_Qaddoc.DataKeys[intRow].Values["typeid"].ToString() + "/" + gv_bos_Qaddoc.DataKeys[intRow].Values["cateid"].ToString() + "/" + gv_bos_Qaddoc.DataKeys[intRow].Values["accFileName"].ToString() + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300); w.focus();";
            }
            else
            {
                ltlAlert.Text = "var w=window.open('SampleNotesDocView.aspx?filepath=" + gv_bos_Qaddoc.DataKeys[intRow].Values["filePath"].ToString() + "&code= &document= ', '_blank', 'menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0);w.focus();";

            }

            BindQAdDoc();
            Bindgv_BosRelateDoc();
        }
        else if (e.CommandName.ToString() == "OpenPdfFile")
        {
            //打开PDF文件
            if (Convert.ToBoolean(gv_bos_Qaddoc.DataKeys[intRow].Values["isNewMechanism"].ToString()))
            {
                ltlAlert.Text = "var w=window.open('/TecDocs/" + gv_bos_Qaddoc.DataKeys[intRow].Values["typeid"].ToString() + "/" + gv_bos_Qaddoc.DataKeys[intRow].Values["cateid"].ToString() + "/" + gv_bos_Qaddoc.DataKeys[intRow].Values["fileName"].ToString() + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300);w.focus();";
            }
            else
            {
                ltlAlert.Text = "var w=window.open('SampleNotesDocView.aspx?filepath=" + gv_bos_Qaddoc.DataKeys[intRow].Values["filePath"].ToString() + "&code= &document= ', '_blank', 'menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0);w.focus();";
            }
        }

        BindQAdDoc();
        Bindgv_BosRelateDoc();
    }

    protected void gv_allDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
            strbosNbr = txt_bosnbr.Text.ToString();
            ibosDetLine = Convert.ToInt32(lbl_line.Text.ToString());
            string docid = gv_allDoc.DataKeys[index][0].ToString();
            string docVersion = gv_allDoc.Rows[index].Cells[8].Text.ToString();

            if (sap.addBosDetDocLink(strbosNbr, ibosDetLine, docid, docVersion))
            {
                Bindgv_BosRelateDoc();
            }
            else
            {
                this.Alert("添加关联文档失败,因为此关联文档已添加");
                return;
            }

        }
    }

    protected void gv_relateDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string accFileName = gv_relateDoc.DataKeys[e.Row.RowIndex].Values["bos_accFileName"].ToString();
            if (accFileName.Length == 0)
            {
                e.Row.Cells[8].Text = string.Empty;
            }

            e.Row.Cells[9].Attributes.Add("onclick", "return confirm('你确认要要删除此关联吗?')");
        }
    }
    protected void gv_relateDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_relateDoc.PageIndex = e.NewPageIndex;
        Bindgv_BosRelateDoc();
    }
    protected void gv_relateDoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gv_relateDoc.DataKeys[e.RowIndex]["ID"].ToString());
        if (sap.deleteBosDoc(id))
        {
            this.Alert("成功删除关联文档");
            Bindgv_BosRelateDoc();
            return;
        }
        else
        {
            this.Alert("删除关联文档失败");
            return;
        }

    }
    protected void gv_relateDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = Convert.ToInt32(e.CommandArgument.ToString());

        if (e.CommandName.ToString() == "myView")
        {
            if (Convert.ToBoolean(gv_relateDoc.DataKeys[intRow].Values["bos_docIsNewMechanism"].ToString()))
            {
                ltlAlert.Text = "window.open('/TecDocs/" + gv_relateDoc.DataKeys[intRow].Values["bos_cateID"].ToString() + "/" + gv_relateDoc.DataKeys[intRow].Values["bos_typeID"].ToString() + "/" + gv_relateDoc.DataKeys[intRow].Values["bos_accFileName"].ToString() + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300 ');";
            }
            else
            {
                ltlAlert.Text = "window.open('SampleNotesDocView.aspx?filepath=" + gv_relateDoc.DataKeys[intRow].Values["bos_filePath"].ToString() + "&code= &document= ', '_blank', 'menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0);";

            }
            BindQAdDoc();
            Bindgv_BosRelateDoc();
        }
        else if (e.CommandName.ToString() == "OpenPdfFile")
        {
            if (Convert.ToBoolean(gv_relateDoc.DataKeys[intRow].Values["bos_docIsNewMechanism"].ToString()))
            {
                ltlAlert.Text = "window.open('/TecDocs/" + gv_relateDoc.DataKeys[intRow].Values["bos_cateID"].ToString() + "/" + gv_relateDoc.DataKeys[intRow].Values["bos_typeID"].ToString() + "/" + gv_relateDoc.DataKeys[intRow].Values["bos_fileName"].ToString() + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300);";
            }
            else
            {
                ltlAlert.Text = "window.open('SampleNotesDocView.aspx?filepath=" + gv_relateDoc.DataKeys[intRow].Values["bos_filePath"].ToString() + "&code= &document= ', '_blank', 'menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0);";
            }

            BindQAdDoc();
            Bindgv_BosRelateDoc();
        }
    }

    protected void ddl_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDllCategory();
    }

    protected void ddl_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddl_Category.SelectedIndex > 0)
        //{
        bindgv_allDoc();
        //} 
    }

    protected void btn_DocSearch_Click(object sender, EventArgs e)
    {
        if (ddl_Category.SelectedIndex == 0)
        {
            this.Alert("请选择文档的分类");
            return;
        }
        else
        {
            bindgv_allDoc();
        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Mode"].ToString() == "Receive")
        {
            Response.Redirect("SampleNotesReceiveConfirm.aspx?bos_nbr=" + txt_bosnbr.Text.ToString());
        }
        else if (Request.QueryString["Mode"].ToString() == "Maintain")
        {
            if (!String.IsNullOrEmpty(Request.QueryString["did"]))
            {
                Response.Redirect("SampleNotesMaintain.aspx?bos_nbr=" + txt_bosnbr.Text.ToString()+"&mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                Response.Redirect("SampleNotesMaintain.aspx?bos_nbr=" + txt_bosnbr.Text.ToString());
            }
          
        }
        else if (Request.QueryString["Mode"].ToString() == "Vend")
        {
            Response.Redirect("SampleNotesVendConfirm.aspx?bos_nbr=" + txt_bosnbr.Text.ToString());
        }
        else if (Request.QueryString["Mode"].ToString() == "TechCheck")
        {
            Response.Redirect("SampleNotesTechCheckMaintain.aspx?bos_nbr=" + txt_bosnbr.Text.ToString());
        }
        else if (Request.QueryString["Mode"].ToString() == "QualCheck")
        {
            Response.Redirect("SampleNotesQualityCheckMaintain.aspx?bos_nbr=" + txt_bosnbr.Text.ToString());
        }
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "download")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = gvrow.RowIndex;
            string[] param = e.CommandArgument.ToString().Split(',');
            string vend = param[0].ToString();
            string docid = param[1].ToString();
            string path = param[2].ToString();
            string status = param[3].ToString();
            if (status == "False")
            {
                ltlAlert.Text = "window.open('/TecDocs/Supplier/" + vend + "/" + docid + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300');";
            }
            else
            {
                ltlAlert.Text = "window.open('" + path + gvlist.Rows[index].Cells[1].Text + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300');";
            }
        }
        if (e.CommandName.ToString() == "Transfer")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string id = param[0].ToString();
            string status = param[1].ToString();
            Response.Redirect("SampleNotesDocTransferDetail.aspx?sourceDocID=" + id + "&status=" + status + "&source=" + "bos");
        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        gvlist.SelectedIndex = -1;

        BindgvVendImportDoc();
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gvlist.DataKeys[e.Row.RowIndex].Values["bos_det_isReceipt"].ToString() == "False")
            {
                e.Row.Cells[6].Enabled = false;
            }
            LinkButton linkTransfer = (LinkButton)e.Row.FindControl("linkTransfer");

            if (e.Row.Cells[5].Text == "False")
            {
                linkTransfer.Text = "转移";
                e.Row.Cells[5].Text = "待转移";

            }
            else
            {
                e.Row.Cells[5].Text = "已转移";
                linkTransfer.Text = "详情";
            }

        }
    }
}