using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using SampleManagement;

public partial class supplier_SampleNotesLists : BasePage
{
    Sample sap = new Sample();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            txt_CreatedDate1.Text = System.DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-dd");
            BindVend();

            //Response.Redirect("/Supplier/SampleNotesMaintain.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            if (!String.IsNullOrEmpty(Request.QueryString["did"]))
            {
                btn_Back.Visible = true;
            }
            Bindgv_Bos();
        }
    }

    private void Bindgv_Bos()
    {
        string strBosnbr = txt_bosnbr.Text.Trim().ToString();
        if (strBosnbr.IndexOf('*')>0)

        {
            strBosnbr = "*" + strBosnbr;
        }
        string strVend = ddl_vend.SelectedValue.ToString();
        DateTime createdDate1;
        DateTime createdDate2;
        if (txt_CreatedDate1.Text.Trim() != string.Empty)
        {
            try
            {
                createdDate1 = Convert.ToDateTime(txt_CreatedDate1.Text);

            }
            catch
            {
                this.Alert("打样单号生成日期 开始日期 输入格式不对");
                return;
            }
        }
        else
        {
            createdDate1 = Convert.ToDateTime("1900-01-01");
        }

        if (txt_CreatedDate2.Text.Trim() != string.Empty)
        {
            try
            {
                createdDate2 = Convert.ToDateTime(txt_CreatedDate2.Text);

            }
            catch
            {
                this.Alert("打样单号生成日期 结束日期 输入格式不对");
                return;
            }
        }
        else
        {
            createdDate2 = Convert.ToDateTime("1900-01-01");
        }

        DataTable dt;

        //Response.Redirect("/Supplier/SampleNotesMaintain.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        if (!String.IsNullOrEmpty(Request.QueryString["did"]))
        {
            int strDid = Convert.ToInt32(Request.QueryString["did"]);
            dt = sap.getBosMstrByDid(strBosnbr, strVend, createdDate1, createdDate2, strDid);
        }
        else
        {
            dt = sap.getBosMstr(strBosnbr, strVend, createdDate1, createdDate2);
        }

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gv_bos_mstr.DataSource = dt;
            gv_bos_mstr.DataBind();
            int columnCount = gv_bos_mstr.Rows[0].Cells.Count;
            gv_bos_mstr.Rows[0].Cells.Clear();
            gv_bos_mstr.Rows[0].Cells.Add(new TableCell());
            gv_bos_mstr.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv_bos_mstr.Rows[0].Cells[0].Text = "没有记录";
            gv_bos_mstr.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gv_bos_mstr.DataSource = dt;
            gv_bos_mstr.DataBind();
        }
    }

    protected void BindVend()
    {
        ddl_vend.DataSource = sap.getBosSuppliers((SysRole)Enum.Parse(typeof(SysRole), "Supplier", true));//ddlUserType.SelectedValue

        ddl_vend.DataBind();
        ddl_vend.Items.Insert(0, new ListItem("--", "0"));

    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_bosnbr.Text = "";
        ddl_vend.SelectedIndex = -1;
        txt_rmks.Text = "";
        txt_CreatedDate1.Text = System.DateTime.Now.Date.AddDays(-7).ToString("yyyy-MM-dd");
        txt_CreatedDate2.Text = "";
        Bindgv_Bos();

    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        Bindgv_Bos();
    }

    protected void gv_bos_mstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values[1].ToString().ToLower() == "true")
            {
                e.Row.Cells[5].Text = "已确认";
            }
            else
            {
                e.Row.Cells[5].Text = " ";
            }
            if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values[2].ToString().ToLower() == "true")
            {
                e.Row.Cells[6].Text = "已收";
            }
            else
            {
                e.Row.Cells[6].Text = " ";
            }

            if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_isCanceled"].ToString().ToLower() == "true")
            {
                e.Row.Cells[7].Text = "取消";
            }
            else
            {
                e.Row.Cells[7].Text = " ";
            }

            if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_isSendEmail"].ToString().ToLower() == "true")
            {
                LinkButton lnkEmail = (LinkButton)e.Row.FindControl("linkEmail");
                lnkEmail.Text = "再次发送";

                //如果这里已经确认了，就不用再发
                if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values[1].ToString().ToLower() == "true")
                {
                    lnkEmail.Text = "";
                }
            }
        }

    }


    protected void gv_bos_mstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_bos_mstr.PageIndex = e.NewPageIndex;
        Bindgv_Bos();
    }

    protected void gv_bos_mstr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        { 
            string bosNbr = e.CommandArgument.ToString();
            if (!String.IsNullOrEmpty(Request.QueryString["did"]))
            {
                Response.Redirect("SampleNotesMaintain.aspx?bos_nbr=" + bosNbr+ "&mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                Response.Redirect("SampleNotesMaintain.aspx?bos_nbr=" + bosNbr);
            } 
          
        }
        else if (e.CommandName.ToString() == "SendDetail")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string bosnbr = gv_bos_mstr.Rows[index].Cells[0].Text.ToString();
            string bosVend = gv_bos_mstr.Rows[index].Cells[1].Text.ToString();
            string bosVendName = gv_bos_mstr.Rows[index].Cells[2].Text.ToString();
            string bosrmks = gv_bos_mstr.Rows[index].Cells[3].Text.ToString();
            SendVendEmail(bosnbr, bosVend, bosVendName,bosrmks);
        }
    }

    private void SendVendEmail(string bosnbr, string bosVend, string bosVendName, string bosrmks)
    {
        bool SendEmail = false;
        string mailfrom = ConfigurationManager.AppSettings["AdminEmail"].ToString();
        string vendcode = bosVend;

        string mailto = sap.getBosVendEmail(vendcode);
        if (mailto == string.Empty)
        {
            this.Alert("供应商" + vendcode + "的邮箱地址未维护,请联系管理员进行维护");
            return;
        }

        string cc = "";
        string bosCreatedName = "";
        DataTable dt = sap.getBosnbrCreatedbyEmail(bosnbr);
        if (dt.Rows.Count > 0)
        {
            cc = dt.Rows[0]["email"].ToString();
            bosCreatedName = dt.Rows[0]["userName"].ToString();
        }
        
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(mailfrom, "TCP-China System");
        try
        {
            MailAddress addressto = new MailAddress(mailto, bosVendName);
            mail.To.Add(addressto);
            if (cc != string.Empty)
            {
                mail.CC.Add(new MailAddress(cc, bosCreatedName));
            }
            mail.Subject = "No Reply [打样单通知 - 新建], 单号为:" + bosnbr ;
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append("   " + bosVendName + "(" + vendcode + ")" + ",<br>");
            sb.Append("    &nbsp; &nbsp; 您好 !<br>");
            sb.Append("     <br>");
            sb.Append("    我司需向贵司下达打样单。详细信息如下：<br>");
            sb.Append("    打样单号:" + bosnbr + "<br>");
            sb.Append("    打样单备注:" + bosrmks + "<br>");
            sb.Append("     详细内容请登录我司供应链系统进行查看并确认<br>");
            sb.Append("           <a href='"+baseDomain.getSupplierWebsite()+"'target='_blank'>"+baseDomain.getSupplierWebsite()+"/</a><br>");
            sb.Append("         <br><p style='color:Gray; font-size:12px;'> 系统通知邮件,请勿回复</p><br>");
            sb.Append("         <br>");
            sb.Append("</form>");
            sb.Append("</html>");
            mail.Body = Convert.ToString(sb);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            SendEmail = true;
            sb.Remove(0, sb.Length);
        }
        catch
        {
            SendEmail = false;
        }
         
        if (SendEmail)
        {
            sap.updateBosMstrSendEmail(bosnbr);
            this.Alert("邮件发送成功");
            return;
        }
        else
        {
            this.Alert("邮件发送失败");
            return;
        }

    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        //Response.Redirect("/Supplier/SampleNotesMaintain.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        if (!String.IsNullOrEmpty(Request.QueryString["did"]))
        {
          Response.Redirect("SampleNotesMaintain.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
        else
        {
            Response.Redirect("SampleNotesLists.aspx");
        } 
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);     
    }
}
