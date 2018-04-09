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
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;


public partial class Bom_Change : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["aamId"]))
            {
                //btnSubmit.Text = "�ύ";
            }
            else   // ����༭�޸�ԭ��������
            {
                if (Request.QueryString["aamId"].ToString() != string.Empty)
                {
                    int iApplyId = Convert.ToInt32(Request.QueryString["aamId"]);
                    string strApplyName = Convert.ToString(Session["uName"]);
                }
            }

            txt_ApplyEmail.Text = Convert.ToString(Session["email"]);
            txt_ApplyName.Text = Convert.ToString(Session["uName"]);

            if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["DID"])))
            {
                btn_back.Visible = false;
                btn_reject.Visible = false;
                btn_Submit.Visible = false;
                btn_access.Visible = false;
                txt_suggest.Visible = false;
                lb_suggest.Visible = false;
                check_update.Visible = false;
            }
            else
            {
                if (Convert.ToString(Request.QueryString["CTP"]) == "S")
                {
                    btn_reject.Visible = false;
                    btn_Submit.Visible = false;
                    btn_access.Visible = false;
                    btn_choose.Visible = false;
                }
                btn_ApplySubmit.Visible = false;
                DataBind();
               
            }
        }
    }

    protected void DataBind()
    {
        DataTable dt = Bom_AccessApply.GetBomCheckInfos(Convert.ToString(Request.QueryString["DID"]), int.Parse(Convert.ToString(Session["uid"])),Convert.ToString(Request.QueryString["CTP"]), int.Parse(Request.QueryString["RAD"].ToString()));
        if (dt.Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('������תʧ�ܣ�������')";
            return;
        }
        if (dt.Rows[0]["ps_Check_Status"].ToString() == "Y")
        {
            btn_reject.Visible = false;
            btn_Submit.Visible = false;
            btn_access.Visible = false;
            txt_suggest.Visible = false;
            lb_suggest.Visible = false;
            check_update.Visible = false;
            btn_choose.Visible = false;
        }
        txt_ps_par.Text = dt.Rows[0]["ps_par"].ToString();
        txt_ps_comp.Text = dt.Rows[0]["ps_comp"].ToString();
        txt_startdate.Text = dt.Rows[0]["ps_start"].ToString();
        txt_enddate.Text = dt.Rows[0]["ps_end"].ToString();
        txt_qty_per.Text = dt.Rows[0]["ps_qty_per"].ToString();
        txt_scrp_pct.Text = dt.Rows[0]["ps_scrp_pct"].ToString();
        txt_ps_to_comp.Text = dt.Rows[0]["ps_to_comp"].ToString();
        txt_tostartdate.Text = dt.Rows[0]["ps_to_start"].ToString();
        txt_toenddate.Text = dt.Rows[0]["ps_to_end"].ToString();
        txt_ApplyReason.Text = dt.Rows[0]["ps_remark"].ToString();
        rbt_ChangBom.SelectedValue = dt.Rows[0]["ps_types"].ToString();
        txt_suggest.Text = dt.Rows[0]["ps_suggest"].ToString();
        if (dt.Rows.Count > 0)
        {
            txt_ps_par.ReadOnly = true;
            txt_ps_comp.ReadOnly = true;
            txt_ApplyReason.ReadOnly = true;
            txt_startdate.ReadOnly = true;
            txt_enddate.ReadOnly = true;
            txt_ps_to_comp.ReadOnly = true;
            txt_tostartdate.ReadOnly = true;
            txt_toenddate.ReadOnly = true;
            rbt_ChangBom.Enabled = false;
        }
        if (dt.Rows[0]["ps_types"].ToString() == "U")
        {
            check_update.Visible = true;
        }
        else
        {
            check_update.Visible = false;
        }
        DataTable dt1 = Bom_AccessApply.CheckBomOwnCheckinfo(Convert.ToString(Request.QueryString["DID"]), int.Parse(Convert.ToString(Session["uid"])), int.Parse(Request.QueryString["RAD"].ToString()));
        if (dt1.Rows.Count > 0)
        {
            if(dt1.Rows[0]["ps_check_status"].ToString()!="")
            {
                btn_Submit.Visible=false;
                btn_reject.Visible=false;
                //btn_back.Visible=false;
                btn_access.Visible = false;

            }
        }
    }

    private bool CheckSubmitInfos()
    {
        string err = "";
        Regex rg = new Regex("^.*[0-9]*[1-9][0-9]*$"); 
        DataTable dt1 = Bom_AccessApply.CheckBomIsExsitChange(txt_ps_par.Text, txt_ps_comp.Text, rbt_ChangBom.SelectedValue, Convert.ToInt32(Session["PlantCode"]));
        if (dt1.Rows.Count > 0)
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ��Ѵ������ڸ����')";
            return false;
        }
        if (rbt_ChangBom.SelectedValue == "U")
        {
            DataTable dt2 = Bom_AccessApply.CheckBomIsExsit(txt_ps_comp.Text, Convert.ToInt32(Session["PlantCode"]));
            if (dt2.Rows.Count <= 0)
            {
                ltlAlert.Text = "alert('�ύʧ�ܣ������ϺŲ�����')";
                return false;
            }
            DataTable dt = Bom_AccessApply.CheckBomIsExsit(txt_ps_to_comp.Text, Convert.ToInt32(Session["PlantCode"]));
            if (dt.Rows.Count <= 0)
            {
                ltlAlert.Text = "alert('�ύʧ�ܣ������ϺŲ�����')";
                return false;
            }
        }
        else
        {
            DataTable dt3 = Bom_AccessApply.CheckBomIsExsit(txt_ps_comp.Text, Convert.ToInt32(Session["PlantCode"]));
            if (dt3.Rows.Count <= 0)
            {
                ltlAlert.Text = "alert('�ύʧ�ܣ������ϺŲ�����')";
                return false;
            }
            if (Convert.ToDateTime(txt_startdate.Text) > Convert.ToDateTime(txt_enddate.Text))
            {
                err += "\\n�Ӽ����ϵ���ʼ���ڲ��ܴ��ڽ������ڣ�";
            }
        }

        if (!rg.Match(txt_qty_per.Text.Trim()).Success)
        {
            err += "\\n�Ӽ�����ÿ��������Ϊ���֣�";
        }
        if (txt_scrp_pct.Text!= "0")
        {
            if (!rg.Match(txt_scrp_pct.Text.Trim()).Success)
            {
                err += "\\n�Ӽ����ϴ�Ʒ����Ϊ���֣�";
            }
        }
        if (string.IsNullOrEmpty(txt_ps_par.Text))
        {
            err += "\\n�������ϺŲ���Ϊ�գ�";
        }
        if (string.IsNullOrEmpty(txt_ps_comp.Text))
        {
            err += "\\n�Ӽ����ϺŲ���Ϊ�գ�";
        }
        if (string.IsNullOrEmpty(rbt_ChangBom.SelectedValue))
        {
            err += "\\n������ͱ���ѡ��һ����";
        }
        if (string.IsNullOrEmpty(txt_startdate.Text))
        {
            err += "\\n�Ӽ����ϵ���ʼ���ڲ���Ϊ�գ�";
        }
        if (rbt_ChangBom.SelectedValue == "U")
        {

            if (string.IsNullOrEmpty(txt_enddate.Text))
            {
                err += "\\n�Ӽ����ϵĽ������ڲ���Ϊ�գ�";
            }
            if (Convert.ToDateTime(txt_startdate.Text) > Convert.ToDateTime(txt_enddate.Text))
            {
                err += "\\n�Ӽ����ϵ���ʼ���ڲ��ܴ��ڽ������ڣ�";
            }
            if (string.IsNullOrEmpty(txt_ps_to_comp.Text))
            {
                err += "\\n���Ӽ����ϺŲ���Ϊ�գ�";
            }
            if (string.IsNullOrEmpty(txt_tostartdate.Text))
            {
                err += "\\n���Ӽ����ϵ���ʼ���ڲ���Ϊ�գ�";
            }
            if (Convert.ToDateTime(txt_tostartdate.Text) > Convert.ToDateTime(txt_toenddate.Text))
            {
                err += "\\n�Ӽ����ϵ���ʼ���ڲ��ܴ��ڽ������ڣ�";
            }
            if (!rg.Match(txt_to_qty_per.Text.Trim()).Success)
            {
                err += "\\n���Ӽ�����ÿ��������Ϊ���֣�";
            }
            if (txt_to_scrp_pct.Text != "0")
            {
                if (!rg.Match(txt_to_scrp_pct.Text.Trim()).Success)
                {
                    err += "\\n���Ӽ����ϴ�Ʒ����Ϊ���֣�";
                }
            }
        }
        if (rbt_ChangBom.SelectedValue == "D")
        {
            if (string.IsNullOrEmpty(txt_enddate.Text))
            {
                err += "\\n�Ӽ����ϵĽ������ڲ���Ϊ�գ�";
            }
        }
        if (string.IsNullOrEmpty(txb_chooseid.Text))
        {
            err += "\\n�ύ������Ϊ�գ�";
        }
        if (string.IsNullOrEmpty(txt_ApplyEmail.Text))
        {
            err += "\\n���������䲻��Ϊ�գ�";
        }
        if (string.IsNullOrEmpty(txt_ApplyReason.Text))
        {
            err += "\\n�������ɲ���Ϊ�գ�";
        }
        if (txt_ApplyReason.Text.Length>100)
        {
            err += "\\n�������ɳ��Ȳ��ܴ���100���ַ���";
        }
        if (!string.IsNullOrEmpty(err))
        {
            ltlAlert.Text = "alert('"+err+"')";
            return false;
        }
        return true;
    }

    protected void btn_ApplySubmit_Click(object sender, EventArgs e)
    {
        if (!CheckSubmitInfos())
        {
            return;
        }
        int iApplyId = Bom_AccessApply.newAccessApplyInfo(txt_ps_par.Text, txt_ps_comp.Text, txt_startdate.Text.Trim(), txt_enddate.Text.Trim(), rbt_ChangBom.SelectedValue, txt_ps_to_comp.Text, txt_tostartdate.Text.Trim(), txt_toenddate.Text.Trim(), Convert.ToString(Session["uID"]), txt_ApplyReason.Text, txb_chooseid.Text, Convert.ToInt32(Session["PlantCode"]), txt_qty_per.Text, txt_scrp_pct.Text,txt_to_qty_per.Text,txt_to_scrp_pct.Text);
        if (iApplyId == -1)
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ��������ύһ��')";
            return;
        }
        else   // �ύ�ɹ���Ҫ�����ʼ�������ˣ������������ģ����Ϣ���浽���ݿ�
        {
            ltlAlert.Text = "alert('�ύ�ɹ�����ȴ�����')";
        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txb_choose.Text))
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ���ѡ���ύ��,\\n���ύ����ѡ��ͨ��')";
            return;
        }

        int iApplyId = Bom_AccessApply.BomCheckInfo(Convert.ToString(Request.QueryString["DID"]), int.Parse(Convert.ToString(Session["uid"])), "Y", txb_chooseid.Text, txt_suggest.Text, int.Parse(Request.QueryString["RAD"].ToString()));
        if (iApplyId == -1)
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ��������ύһ��')";
            return;
        }
        else   // �ύ�ɹ���Ҫ�����ʼ�������ˣ������������ģ����Ϣ���浽���ݿ�
        {
            ltlAlert.Text = "alert('�ύ�ɹ�����ȴ�����')";
            Response.Redirect("~/BomChange/Bom_CheckDetails.aspx");
        }
    }

    protected void btn_find_Click(object sender, EventArgs e)
    {
        string errg = "";
        if (string.IsNullOrEmpty(txt_ps_par.Text))
        {
            errg = "���ϺŲ���Ϊ�գ�";
        }
        if (errg != string.Empty)
        {
            this.ltlAlert.Text = "alert('" + errg + "');";
            return;
        }
        DataTable dt = Bom_AccessApply.getbom(txt_ps_par.Text, "szx");
        if (dt.Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('\\n���ϺŲ����ڣ�')";
            return;
        }
    }

    protected void btn_choose_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/Performance/conn_choose2.aspx?mid=" + Request["mid"] + "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BomChange/Bom_CheckDetails.aspx");
    }

    protected void btn_reject_Click(object sender, EventArgs e)
    {
        if (txt_suggest.Text == "")
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ������������Ϊ�գ�')";
            return;
        }
        if (txt_suggest.Text.Length>100)
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ�����������Ȳ��ܴ���100�ַ���')";
            return;
        }
        int iApplyId = Bom_AccessApply.BomCheckInfo(Convert.ToString(Request.QueryString["DID"]), int.Parse(Convert.ToString(Session["uid"])), "N", "", txt_suggest.Text, int.Parse(Request.QueryString["RAD"].ToString()));
        if (iApplyId == -1)
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ��������ύһ��')";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('�ύ�ɹ����Ѿܾ�����')";
            Response.Redirect("~/BomChange/Bom_CheckDetails.aspx");
        }

    }

    protected void btn_access_Click(object sender, EventArgs e)
    {

        if (txt_suggest.Text.Length > 100)
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ�����������Ȳ��ܴ���100�ַ���')";
            return;
        }
        int iApplyId = Bom_AccessApply.BomCheckInfo(Convert.ToString(Request.QueryString["DID"]), int.Parse(Convert.ToString(Session["uid"])), "Y", "", txt_suggest.Text, int.Parse(Request.QueryString["RAD"].ToString()));
        if (iApplyId == -1)
        {
            ltlAlert.Text = "alert('�ύʧ�ܣ��������ύһ��')";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('�ύ�ɹ�����ͨ������')";
            Response.Redirect("~/BomChange/Bom_CheckDetails.aspx");
        }
    }

    protected void rbt_ChangBom_CheckedChanged(object sender, EventArgs e)
    {
        if (rbt_ChangBom.SelectedValue == "U")
        {
            check_update.Visible = true;
            txt_to_qty_per.Text = txt_qty_per.Text;
            txt_to_scrp_pct.Text = txt_scrp_pct.Text;
        }
        else
        {
            check_update.Visible = false;
            txt_ps_to_comp.Text="";
            txt_tostartdate.Text = "";
            txt_toenddate.Text = "";
        }

    }

    protected void btn_inv_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/BomChange/Bom_CheckDetails.aspx");
        if (string.IsNullOrEmpty(txt_ps_comp.Text) && string.IsNullOrEmpty(txt_ps_to_comp.Text))
        {
            ltlAlert.Text = "alert('���ϺŲ���Ϊ��')";
            return;
        }

        ltlAlert.Text = "window.open('Bom_Inv.aspx?bom=" + txt_ps_comp.Text + "&tobom=" + txt_ps_to_comp.Text + "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') ";
    }

    protected void btn_oninv_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_ps_comp.Text) && string.IsNullOrEmpty(txt_ps_to_comp.Text))
        {
            ltlAlert.Text = "alert('���ϺŲ���Ϊ��')";
            return;
        }
        ltlAlert.Text = "window.open('Bom_OnInv.aspx?bom=" + txt_ps_comp.Text + "&tobom=" + txt_ps_to_comp.Text + "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') ";
    }
}
