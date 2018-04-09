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
using System.IO;
using CommClass;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class supplier_supplier_poImport : BasePage
{
    admClass chk = new admClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.SecurityCheck = securityCheck.issecurityCheck(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["uRole"]), 320000);
            if (!this.SecurityCheck.isValid)
            {
                Response.Redirect("~/public/ErrMessage.aspx", true);
            }
            else
            {
                string ponbr = Request.QueryString["po"].ToString();
                int line = Convert.ToInt32(Request.QueryString["line"]);
                string domain = Request.QueryString["do"].ToString();

                SqlDataReader reader = PurchaseOrderDetail.SelectPod(ponbr, line, domain);
                if (reader.Read())
                {
                    lblVend.Text = reader["po_vend"].ToString();
                    lblPo.Text = ponbr;
                    lblLine.Text = line.ToString();
                    lblPart.Text = reader["pod_part"].ToString();
                    lblDesc.Text = reader["ptdesc"].ToString();
                    lbDomain.Text = domain;
                }
                BindData();
            }
        }
        PostBackTrigger trigger = new PostBackTrigger();
        trigger.ControlID = uploadPartBtn.UniqueID;
        ((UpdatePanel)(this.Master.FindControl("UpdatePanel4"))).Triggers.Add(trigger);
    }

    protected void uploadPartBtn_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        if (dropType.SelectedValue == "N")
        {
            this.Alert("请选择一种文件类型");
            BindData();
            return;
        }
        if (!fileAttachFile.HasFile)
        {
            this.Alert("请选择导入文件");
            BindData();
            return;
        }
        else
        {
            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("#") > 0)
            {
                this.Alert("文件名不允许包含字符#");
                BindData();
                return;
            }

            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("%") > 0)
            {
                this.Alert("文件名不允许包含字符%");
                BindData();
                return;
            }

            ImportFile();
        }
    }

    protected void ImportFile()
    {
        string attachName = Path.GetFileNameWithoutExtension(fileAttachFile.FileName);
        string attachExtension = Path.GetExtension(fileAttachFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../Temp/"), DateTime.Now.ToFileTime().ToString() + "-" + attachName);//合并两个路径为上传到服务器上的全路径
        string ponbr = lblPo.Text;

        string prdnbr = string.Empty;
        if (Convert.ToInt32(Request.QueryString["tp"].ToString()) == 2)
        {
            prdnbr = Request.QueryString["prd"].ToString();
        }

        string vender = lblVend.Text;
        int line = Convert.ToInt32(lblLine.Text);
        int createBy = Convert.ToInt32(Session["uID"]);
        string createName = Convert.ToString(Session["uName"]);
        string type = dropType.SelectedValue;
        string typeName = dropType.SelectedItem.Text;

        try
        {
            if (this.fileAttachFile.ContentLength > 0)
            {
                try
                {
                    this.fileAttachFile.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        catch
        {
            this.Alert("保存临时文件失败！请刷新后重新操作一次！");
            BindData();
            return;
        }


        string path = "/TecDocs/Supplier/" + vender + "/";

        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        string docid = ponbr + "-" + line.ToString() + "-" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + attachExtension;
        path = path + docid;

        try
        {
            File.Move(SaveFileName, Server.MapPath(path));
        }
        catch
        {
            this.Alert("文件转移失败，请联系管理员!");
            DataBind();
            return;
        }

        if (insertDocs(prdnbr, line, ponbr, attachName, docid, createBy, createName, type, typeName, txt_docsDecs.Text.Trim(), lbDomain.Text.Trim(), attachName + attachExtension, "/TecDocs/Supplier/" + vender + "/"))
        {
            BindData();
        }
        else
        {
            this.Alert("上传文件失败，请联系管理员!");
            //文件信息插入失败后删除已经转移的文件
            File.Delete(path);
            DataBind();
            return;
        }



    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Transfer")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string id = param[0].ToString();
            string status = param[1].ToString();
            Response.Redirect("SampleNotesDocTransferDetail.aspx?sourceDocID=" + id + "&status=" + status + "&source=" + "sp" + "&domain=" + lbDomain.Text.Trim());
        }
        if (e.CommandName.ToString() == "download")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string vend = param[0].ToString();
            string docid = param[1].ToString();
            this.OpenWindow("/TecDocs/Supplier/" + vend + "/" + docid + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300");
            return;
        }

        else if (e.CommandName.ToString() == "del")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            int id = Convert.ToInt32(param[0]);
            string vend = param[1].ToString().Trim();
            string docid = param[2].ToString().Trim();

            if (deleteDocs(id))
            {
                if (File.Exists(Server.MapPath("/TecDocs/Supplier/" + vend + "/" + docid)))
                {
                    try
                    {
                        File.Delete(Server.MapPath("/TecDocs/Supplier/" + vend + "/" + docid));
                    }
                    catch
                    {
                        this.Alert("删除源文件失败，请联系管理员!");
                    }
                }

            }
            else
            {
                this.Alert("数据库删除失败，请联系管理员!");
            }
            if (Convert.ToInt32(Request.QueryString["tp"].ToString()) == 2)
            {
                Response.Redirect("supplier_docsImport.aspx?po=" + lblPo.Text.Trim() + "&prd=" + Request.QueryString["prd"].ToString() + "&line=" + lblLine.Text.Trim() + "&vend=" + lblVend.Text.Trim() + "&do=" + Request.QueryString["do"].ToString() + "&tp=2" + "&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                Response.Redirect("supplier_docsImport.aspx?po=" + lblPo.Text.Trim() + "&line=" + lblLine.Text.Trim() + "&do=" + Request.QueryString["do"].ToString() + "&id=330000" + "&tp=1" + "&rm=" + DateTime.Now.ToString(), true);
            }
        }
    }

    private void BindData()
    {
        int type = Convert.ToInt32(Request.QueryString["tp"].ToString());
        string ponbr = lblPo.Text;
        int line = Convert.ToInt32(lblLine.Text);
        string prdnbr = string.Empty;
        string domain = Request.QueryString["do"].ToString();
        DataTable dt = null;

        if (type == 2)//从送货单进入
        {
            prdnbr = Request.QueryString["prd"].ToString();
            dt = selectDocs(prdnbr, line, ponbr, domain);
        }
        else//从订单项列表详细进入
        {
            dt = selectDocs(prdnbr, line, ponbr, domain);
        }

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gvlist.DataSource = dt;
            gvlist.DataBind();
            int columnCount = gvlist.Rows[0].Cells.Count;
            gvlist.Rows[0].Cells.Clear();
            gvlist.Rows[0].Cells.Add(new TableCell());
            gvlist.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvlist.Rows[0].Cells[0].Text = "没有记录";
            gvlist.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gvlist.DataSource = dt;
            gvlist.DataBind();
        }
    }

    private bool insertDocs(string prdnbr, int line, string ponbr, string docname, string docid, int createBy, string createName, string type, string typeName, string docDesc, string domain, string fileName, string path)
    {
        SqlParameter[] param = new SqlParameter[14];
        param[0] = new SqlParameter("@prd_nbr", prdnbr);
        param[1] = new SqlParameter("@prd_line", line);
        param[2] = new SqlParameter("@prd_po_nbr", ponbr);
        param[3] = new SqlParameter("@prd_doc_name", docname);
        param[4] = new SqlParameter("@prd_doc_id", docid);
        param[5] = new SqlParameter("@prd_createBy", createBy);
        param[6] = new SqlParameter("@prd_createName", createName);
        param[7] = new SqlParameter("@prd_type", type);
        param[8] = new SqlParameter("@prd_typeName", typeName);
        param[9] = new SqlParameter("@prd_docDesc", docDesc);
        param[10] = new SqlParameter("@prd_domian", domain);
        param[11] = new SqlParameter("@prd_doc_filename", fileName);
        param[12] = new SqlParameter("@prd_path", path);
        param[13] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[13].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_supp_insertDocs", param);
        return Convert.ToBoolean(param[13].Value);
    }

    private DataTable selectDocs(string prdnbr, int line, string ponbr, string domain)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@prd_nbr", prdnbr);
        param[1] = new SqlParameter("@prd_line", line);
        param[2] = new SqlParameter("@prd_po_nbr", ponbr);
        param[3] = new SqlParameter("@prd_domian", domain);

        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_supp_selectDocs", param).Tables[0];
    }

    private bool deleteDocs(int id)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_supp_deleteDocs", param);
        return Convert.ToBoolean(param[1].Value);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string ponbr = lblPo.Text;

        if (Convert.ToInt32(Request.QueryString["tp"].ToString()) == 2)
        {
            string prdnbr = Request.QueryString["prd"].ToString();
            Response.Redirect("supplier_editDeliverys.aspx?prd=" + prdnbr + "&po=" + ponbr + "&rm=" + DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("supplier_podetails.aspx?PONO.=" + lblPo.Text.Trim() + "&do=" + lbDomain.Text.Trim() + "&rm=" + DateTime.Now.ToLocalTime() + "&id=330000'", true);
        }
    }

    protected void PageChange(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        gvlist.SelectedIndex = -1;

        BindData();
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton linkTransfer = (LinkButton)e.Row.FindControl("linkTransfer");
            if (e.Row.Cells[8].Text == "False")
            {
                linkTransfer.Text = "转移";
                e.Row.Cells[8].Text = "待转移";

            }
            else
            {
                e.Row.Cells[8].Text = "已转移";
                linkTransfer.Text = "详情";
            }

        }
    }
}
