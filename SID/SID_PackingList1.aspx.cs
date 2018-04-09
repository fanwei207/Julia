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
using adamFuncs;
using QADSID;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class SID_PackingList1 : BasePage
{
    adamClass chk = new adamClass();
    //SID sid = new SID();
    SID_Packing pack = new SID_Packing();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["_pk"]) || !string.IsNullOrEmpty(Request["_nbr"]) || !string.IsNullOrEmpty(Request["_ShipDate1"])
                || !string.IsNullOrEmpty(Request["_ShipDate2"]) || !string.IsNullOrEmpty(Request["_pricestatus"]))
            {
                txtSysPKNo.Text = Request["_pk"];
                txtShipNo.Text = Request["_nbr"];
                txtShipDate1.Text = Request["_ShipDate1"];
                txtShipDate2.Text = Request["_ShipDate2"];
                ddl_pricestatus.SelectedValue = Request["_pricestatus"];
            }
            BindNbrCombine();
            BindData();
        }
    }
    protected void BindNbrCombine()
    {
        this.txt_CombineNbr.Text = pack.GetPackingNbrCombineNo(Request["nbr"], Request["nbrCombine"]) + ",";
    }
    protected void BindData()
    {
        //�������
        string strPK = txtSysPKNo.Text.Trim();
        //string strRef = txtSysPKRef.Text.Trim();
        string strNbr = txtShipNo.Text.Trim();
        string strDomain = txtDomain.Text.Trim();
        string strpricestatus = ddl_pricestatus.SelectedValue.ToString();

        //Add By Shanzm 2011.02.14
        string strShipDate1 = txtShipDate1.Text.Trim();
        string strShipDate2 = txtShipDate2.Text.Trim();

        gvPacking.DataSource = pack.SelectSIDList(strPK, strpricestatus, strNbr, strDomain, strShipDate1, strShipDate2);
        gvPacking.DataBind();
    }

    protected void gvPacking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPacking.PageIndex = e.NewPageIndex;
        BindData();
    }
    
    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSID_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtShipDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d1 = Convert.ToDateTime(txtShipDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ��');";
                return;
            }
        }

        if (txtShipDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d2 = Convert.ToDateTime(txtShipDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ��');";
                return;
            }
        }

        BindData();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //�������
        //string strRet1 = chkShipno();
        //if (strRet1.ToString() == "0")
        //{
        //    ltlAlert.Text = "alert('������Ϣ��δ¼�룡');";
        //    return;
        //}

        string strRet = chkSelect();
        string struID = Convert.ToString(Session["uID"]);
        bool Ret = false;
        if (strRet.ToString() == "0")
        {
            ltlAlert.Text = "alert('���ֻ��ѡ��һ�����˵���ӡ��');";
            return;
        }

        if (strRet.Length != 0)
        {
            strRet = strRet.Substring(0, strRet.Length - 1);

            Ret = false;

            Ret = pack.InsertPrintPackingTemp(strRet, struID);

            if (Ret)
            {
                //Response.Redirect("/SID/SID_Declaration.aspx?type=temp", true);
                string pk = txtSysPKNo.Text.Trim();
                string ShipDate1 = txtShipDate1.Text.Trim();
                string ShipDate2 = txtShipDate2.Text.Trim();
                string shipno = txtShipNo.Text.Trim();
                string pricestatus = ddl_pricestatus.SelectedValue;
                Response.Redirect("/SID/SID_PackingList.aspx?&type=temp;&_ID= " + strRet + "&_pk=" + pk + "&_ShipDate1=" + ShipDate1 + "&_ShipDate2=" + ShipDate2 + "&_nbr=" + shipno + "&_pricestatus=" + pricestatus + "", true);
            }
            else
            {
                ltlAlert.Text = "alert('ѡ��Ҫ��ӡ�ĵ��������⣬����ϵ����Ա��');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('û��ѡ��Ҫ��ӡ�ĵ��ݣ�');";
        }
    }

    protected string chkShipno()
    {
        //�������
        string strSelect = "";

        //�ж��Ƿ���ѡ��
        for (int i = 0; i < gvPacking.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvPacking.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                string str = gvPacking.Rows[i].Cells[4].Text.ToString().Trim();
                if (string.IsNullOrEmpty(gvPacking.Rows[i].Cells[4].Text.ToString().Trim()) || gvPacking.Rows[i].Cells[4].Text.ToString().Trim() == "&nbsp;")
                {
                    return strSelect = "0";
                }
            }
        }
        return strSelect;
    }

    protected string chkSelect()
    {
        //�������
        string strSelect = "";
        int j = 0;

        //�ж��Ƿ���ѡ��
        for (int i = 0; i < gvPacking.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvPacking.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gvPacking.DataKeys[i].Value.ToString() + ",";
                j += 1;
            }
            if (j > 1)
            {
                strSelect = "0";
                return strSelect;
            }
        }
        return strSelect;
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvPacking.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvPacking.Rows[i].FindControl("chk_Select");
            if (chkAll.Checked)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
        }
    }

    protected void gvPacking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Trim() == string.Empty)
            {
                e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(e.Row.Cells[5].Text));
            }
        }
    }

    protected void gvPacking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Confirm1")
        {
            //int intRow = 0;
            //string strshipno = string.Empty;
            //intRow = Convert.ToInt32(e.CommandArgument.ToString());
            //strshipno = gvPacking.DataKeys[intRow].Value.ToString();
            //gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvPacking.DataKeys[index].Values["SID"].ToString();
            if (!CheckDocumentHaveCheckPrice(Convert.ToInt32(id),Convert.ToInt32(Session["uID"].ToString().Trim())))
            {
                ltlAlert.Text = "alert('��δ�󶨷�Ʊ');";
                return;
            }
            if (DocumentsConfirmShipInfo(id, Session["uID"].ToString(), "ORG1"))
            {
                ltlAlert.Text = "alert('��Ʊ���ϳɹ�����ȴ�����ȷ�ϣ�');";
            }
            else
            {
                ltlAlert.Text = "alert('��Ʊ����ȷ��ʧ�ܣ�����ϵ����Ա��');";
                return;
            }
            BindData();
        }
        else if (e.CommandName == "Confirm2")
        {
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvPacking.DataKeys[index].Values["SID"].ToString();
            if (!CheckDocumentHaveCheckPrice(Convert.ToInt32(id), Convert.ToInt32(Session["uID"].ToString().Trim())))
            {
                ltlAlert.Text = "alert('��δ�󶨷�Ʊ');";
                return;
            }
            if (!DocumentsUncheck(id, Session["uID"].ToString(), "ORG2"))
            {
                ltlAlert.Text = "alert('��֤��δȷ�����ϣ�');";
                return;
            }
            if (!CustomssHaveReject(id, Session["uID"].ToString(), "ORG2"))
            {
                ltlAlert.Text = "alert('�Ѿܾ���������ȷ�ϣ�');";
                return;
            }
            if (CustomsConfirmShipInfo(id, Session["uID"].ToString(), "ORG2"))
            {
                ltlAlert.Text = "alert('��Ʊ���ϳɹ������ؼ۸�������赥֤����ȷ�ϼ۸�');";
            }
            else
            {
                ltlAlert.Text = "alert('ȡ��ȷ��ʧ�ܣ�����ϵ����Ա��');";
                return;
            }
            BindData();
        }
        else if (e.CommandName == "Confirm3")
        {
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvPacking.DataKeys[index].Values["SID"].ToString();
            if (!CheckDocumentHaveCheckPrice(Convert.ToInt32(id), Convert.ToInt32(Session["uID"].ToString().Trim())))
            {
                ltlAlert.Text = "alert('��δ�󶨷�Ʊ');";
                return;
            }
            if (!DocumentsUncheck(id, Session["uID"].ToString(), "ORG3"))
            {
                ltlAlert.Text = "alert('��֤��δȷ�����ϣ�');";
                return;
            }
            if (CustomsRejectCancel(id, Session["uID"].ToString(), "ORG3"))
            {
                ltlAlert.Text = "alert('�ܾ��ɹ���');";
            }
            else
            {
                ltlAlert.Text = "alert('�ܾ�ȷ��ʧ�ܣ�����ϵ����Ա��');";
                return;
            }
            BindData();
        }
        else if (e.CommandName == "detail")
        {
            LinkButton linkPlan = (LinkButton)e.CommandSource;
            int index = ((GridViewRow)linkPlan.Parent.Parent).RowIndex;
            Response.Redirect("../sid/SID_PackingNbrCombine.aspx?id=0&nbr=" + gvPacking.Rows[index].Cells[3].Text.Trim() + "&rm=" + DateTime.Now.ToString());
            //this.Alert("��ϸ");
        }
    }

    /// <summary>
    /// ��֤��ȷ�ϼ۸�
    /// </summary>
    public bool CheckDocumentHaveCheckPrice(Int32 id, Int32 uID)
    {
        string strSQL = "sp_SID_shipinvDocmentCheckPrice";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ID", id);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[2].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, param);
        if (Convert.ToBoolean(param[2].Value))
        {
            return true;
        }
        else
        {
            return false;//param[1].Value.ToString().Trim();
        }
    }

    public bool DocumentsConfirmShipInfo(String id, string uID, string ORG)
    {
        string strSQL = "sp_SID_shipinvDocumentCancel";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@UID", uID);
        parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[2].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        if (Convert.ToBoolean(parm[2].Value))
        {
            return true;
        }
        else
        {
            return false;//param[1].Value.ToString().Trim();
        }
    }

    public bool DocumentsUncheck(String id, string uID, string ORG)
    {
        string strSQL = "sp_SID_shipinvDocumentUncheck";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@UID", uID);
        parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[2].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        if (Convert.ToBoolean(parm[2].Value))
        {
            return true;
        }
        else
        {
            return false;//param[1].Value.ToString().Trim();
        }
    }

    public bool CustomssHaveReject(String id, string uID, string ORG)
    {
        string strSQL = "sp_SID_shipinvCustomsHaveReject";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@UID", uID);
        parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[2].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        if (Convert.ToBoolean(parm[2].Value))
        {
            return true;
        }
        else
        {
            return false;//param[1].Value.ToString().Trim();
        }
    }

    public bool CustomsConfirmShipInfo(String id, string uID, string ORG)
    {
        string strSQL = "sp_SID_shipinvCustomsCancel";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@UID", uID);
        parm[2] = new SqlParameter("@ORG", ORG);
        parm[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[3].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        if (Convert.ToBoolean(parm[3].Value))
        {
            return true;
        }
        else
        {
            return false;//param[1].Value.ToString().Trim();
        }
    }

    public bool CustomsRejectCancel(String id, string uID, string ORG)
    {
        string strSQL = "sp_SID_shipinvCustomsCancel";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@UID", uID);
        parm[2] = new SqlParameter("@ORG", ORG);
        parm[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[3].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        if (Convert.ToBoolean(parm[3].Value))
        {
            return true;
        }
        else
        {
            return false;//param[1].Value.ToString().Trim();
        }
    }

    protected void ddl_pricestatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        //string strSelect = "";
        string nbr = "";
        for (int i = 0; i < gvPacking.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvPacking.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                //strSelect = strSelect + gvPacking.DataKeys[i].Value.ToString() + ",";
                nbr = nbr + gvPacking.DataKeys[i]["Nbr"].ToString().Trim() + ",";
            }
        }
        //strSelect = strSelect.Substring(0, strSelect.Length - 1);
        txt_CombineNbr.Text = txt_CombineNbr.Text + nbr;
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_CombineNbr.Text = "";
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        string nbr = txt_CombineNbr.Text;
        if (string.IsNullOrEmpty(nbr))
        {
            this.Alert("��ѡ��Ҫ�ϲ��ĳ��˵���!");
            return;
        }
        int uID = Convert.ToInt32(Session["uID"]);
        string id = DateTime.Now.ToString("yyyyMMddHHmmss");
        if (pack.PackingCombineNbrDelete(id, uID, Request["nbr"], Request["nbrCombine"]) != 0)
        {
            this.Alert("ɾ����¼ʧ�ܣ�����ϵ����Ա!");
            return;
        }
        string[] str = nbr.Split(',');
        foreach(string i in str)
        {
            if (!string.IsNullOrEmpty(i.ToString()))
            {
                int ret = pack.PackingCombineNbr(i.ToString(), uID, id);
                if (ret != 0)
                {
                    if (pack.PackingCombineNbrDelete(id, uID, Request["nbr"], "0") != 0)
                    {
                        this.Alert("ɾ����¼ʧ�ܣ�����ϵ����Ա!");
                        return;
                    }
                    this.Alert("���˵����Ѵ���������ϣ���ȷ��!");
                    return;
                }
            }
        }
        if (!string.IsNullOrEmpty(Request["nbrCombine"]))
        {
            string str1 = str[1];
            Response.Redirect("../sid/SID_PackingNbrCombine.aspx?id=0&nbr=" + str1 + "&rm=" + DateTime.Now.ToString());
        }
        BindData();
    }
}
