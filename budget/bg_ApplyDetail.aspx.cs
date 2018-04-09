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
using adamFuncs;
using BudgetProcess;

public partial class bg_ApplyDetail : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            if (Request.QueryString["aid"] == null)
            {
                Response.Redirect("/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                BindData();
            }
        }
    }

    protected void BindData()
    {
        //�������
        string strAID = Convert.ToString(Request.QueryString["aid"]);
        
        int i = 1;
        bool isLastRec = false;
        string strReceipt = string.Empty;
        string strCopyTo = string.Empty;
        string strSub = string.Empty;
        string strCC = string.Empty;
        string strValue = string.Empty;

        DataTable dtb = new DataTable();
        dtb.Columns.Add(new DataColumn("Item", typeof(string)));
        dtb.Columns.Add(new DataColumn("Notes", typeof(string))); 

        DataRow dr;
        
        SqlDataReader reader = Budget.getApplyInfo(strAID);
        while (reader.Read())
        {
            if (i == 1)
            {
                dr = dtb.NewRow();
                dr["Item"] = "������:";
                dr["Notes"] = reader[0].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "��������:";
                dr["Notes"] = reader[1].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "��������:";
                dr["Notes"] = reader[2].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "����˻�:";
                dr["Notes"] = reader[4].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "������;:";
                dr["Notes"] = reader[3].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "�ɱ�����:";
                dr["Notes"] = reader[5].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "��������:";
                dr["Notes"] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader[11]);
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "������:";
                dr["Notes"] = string.Format("{0:N2}", reader[6]);
                dtb.Rows.Add(dr);

                strSub = reader[16].ToString().Trim();
                strCC = reader[17].ToString().Trim();

                dr = dtb.NewRow();
                dr["Item"] = "Ԥ�����:";
                strValue = string.Empty;
                strValue = Budget.getBudgetValue(strSub, strCC);
                dr["Notes"] = strValue.Length == 0 ? "0" : string.Format("{0:N2}", Convert.ToDecimal(strValue));
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "�ۼ��������:";
                strValue = string.Empty;
                strValue = Budget.getCumulation(strSub, strCC);
                dr["Notes"] = strValue.Length == 0 ? "0" : string.Format("{0:N2}", Convert.ToDecimal(strValue));
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "ʵ�ʷ�������:";
                dr["Notes"] = string.Format("{0:N2}", Convert.ToDecimal(reader[18])); ;
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "�ύ��:";
                dr["Notes"] = reader[7].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "���͸�:";
                dr["Notes"] = reader[8].ToString().Trim();
                dtb.Rows.Add(dr);

            }

            if (i == Convert.ToInt32(reader[14].ToString().Trim()))
            {
                if (isLastRec)
                {
                    dr = dtb.NewRow();
                    dr["Item"] = "�ύ��:";
                    dr["Notes"] = reader[7].ToString().Trim();
                    dtb.Rows.Add(dr);

                    dr = dtb.NewRow();
                    dr["Item"] = "���͸�:";
                    dr["Notes"] = reader[8].ToString().Trim();
                    dtb.Rows.Add(dr);
                }

                if (Convert.ToBoolean(reader[15]))
                {
                    dr = dtb.NewRow();
                    dr["Item"] = "&nbsp;";
                    dr["Notes"] = "&nbsp;";
                    dtb.Rows.Add(dr);

                    dr = dtb.NewRow();
                    dr["Item"] = "�����:";
                    dr["Notes"] = reader[7].ToString().Trim();
                    dtb.Rows.Add(dr);

                    dr = dtb.NewRow();
                    dr["Item"] = "��������:";
                    dr["Notes"] = reader[13].ToString().Trim();
                    dtb.Rows.Add(dr);

                    dr = dtb.NewRow();
                    dr["Item"] = "�������:";
                    dr["Notes"] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader[9]);
                    dtb.Rows.Add(dr);

                    dr = dtb.NewRow();
                    dr["Item"] = "�������:";
                    dr["Notes"] = reader[12].ToString().Trim();
                    dtb.Rows.Add(dr);

                    dr = dtb.NewRow();
                    dr["Item"] = "�ر�����:";
                    dr["Notes"] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader[10]);
                    dtb.Rows.Add(dr);

                    isLastRec = false;

                    //if (isLastRec)
                    //{
                    //    dr = dtb.NewRow();
                    //    dr["Item"] = "�ύ��:";
                    //    dr["Notes"] = reader[7].ToString().Trim();
                    //    dtb.Rows.Add(dr);

                    //    dr = dtb.NewRow();
                    //    dr["Item"] = "���͸�:";
                    //    dr["Notes"] = reader[8].ToString().Trim();
                    //    dtb.Rows.Add(dr);

                    //    isLastRec = false;
                    //}
                }
            }
            else
            {
                if (isLastRec)
                {
                    dr = dtb.NewRow();
                    dr["Item"] = "�ύ��:";
                    dr["Notes"] = reader[7].ToString().Trim();
                    dtb.Rows.Add(dr);

                    dr = dtb.NewRow();
                    dr["Item"] = "���͸�:";
                    dr["Notes"] = reader[8].ToString().Trim();
                    dtb.Rows.Add(dr);

                    isLastRec = false;
                }

                dr = dtb.NewRow();
                dr["Item"] = "&nbsp;";
                dr["Notes"] = "&nbsp;";
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "�����:";
                dr["Notes"] = reader[7].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "��������:";
                dr["Notes"] = reader[13].ToString().Trim();
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "�������:";
                dr["Notes"] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader[9]);
                dtb.Rows.Add(dr);

                dr = dtb.NewRow();
                dr["Item"] = "�������:";
                dr["Notes"] = reader[12].ToString().Trim();
                dtb.Rows.Add(dr);

                isLastRec = true;
                i++;
                
            }

            strReceipt = reader[7].ToString().Trim();
            strCopyTo = reader[8].ToString().Trim();
        }
        reader.Close();

        if (isLastRec)
        {
            dr = dtb.NewRow();
            dr["Item"] = "�ύ��:";
            dr["Notes"] = strReceipt;
            dtb.Rows.Add(dr);

            dr = dtb.NewRow();
            dr["Item"] = "���͸�:";
            dr["Notes"] = strCopyTo;
            dtb.Rows.Add(dr);
        }

        //DataView dvw = new DataView(dtb);

        gvApply.DataSource = dtb;
        gvApply.DataBind();
    }
}
