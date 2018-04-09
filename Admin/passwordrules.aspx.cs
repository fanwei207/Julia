using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class admin_passwordrules : BasePage
{
    public adamClass chk = new adamClass();
    private string strNumberRegex = @"^.*\d+.*$";
    private string strLowLetterRegex = @"^.*[a-z]+.*$";
    private string strUpLetterRegex = @"^.*[A-Z]+.*$";
    private string strSpecialRegex = @"^.*[^\w]+.*$";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            BindData();
        }
    }

    protected void BindData()
    {
        try
        {
            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_hr_selectPasswordRules");
            if (ds != null && ds.Tables.Count >= 2)
            {
                dropRules.Items.Clear();
                InitControlState();
                DataTable dtRuleList = ds.Tables[0];
                DataTable dtDefaultRule = ds.Tables[1];
                if (dtRuleList != null && dtRuleList.Rows.Count > 0)
                {
                    ListItem item;
                    for (int i = 0; i < dtRuleList.Rows.Count; i++)
                    {
                        item = new ListItem();
                        item.Text = dtRuleList.Rows[i]["name"].ToString();
                        item.Value = dtRuleList.Rows[i]["id"].ToString();
                        dropRules.Items.Add(item);
                    }
                }
                if (dtDefaultRule != null && dtDefaultRule.Rows.Count > 0)
                {
                    dropRules.SelectedValue = dtDefaultRule.Rows[0]["id"].ToString();
                    chkDefaultRule.Checked = true;
                    txbRuleName.Text = dtDefaultRule.Rows[0]["name"].ToString();
                    txbMinLen.Text = dtDefaultRule.Rows[0]["minLen"].ToString();
                    txbMaxLen.Text = dtDefaultRule.Rows[0]["maxLen"].ToString();
                    txbValidity.Text = dtDefaultRule.Rows[0]["validityCount"].ToString();
                    dropValidity.SelectedValue = dtDefaultRule.Rows[0]["validityUnit"].ToString();
                    txbRepeatCount.Text = dtDefaultRule.Rows[0]["repeatCount"].ToString();
                    chkNumber.Checked = Convert.ToBoolean(dtDefaultRule.Rows[0]["hasNumber"]);
                    chkLowLetter.Checked = Convert.ToBoolean(dtDefaultRule.Rows[0]["hasLowLetter"]);
                    chkUpLetter.Checked = Convert.ToBoolean(dtDefaultRule.Rows[0]["hasUpLetter"]);
                    chkSpecial.Checked = Convert.ToBoolean(dtDefaultRule.Rows[0]["hasSpecial"]);
                    txbPwdStructureDesc.Text = dtDefaultRule.Rows[0]["structureDesc"].ToString();
                }
                ds.Dispose();
                dtRuleList.Dispose();
                dtDefaultRule.Dispose();
            }
        }
        catch
        {
            ltlAlert.Text = "alert('��������ȡʧ�ܣ������ԣ�')";
            return;
        }
    }

    protected void InitControlState()
    {
        chkDefaultRule.Checked = false;
        txbRuleName.Text = string.Empty;
        txbMinLen.Text = string.Empty;
        txbMaxLen.Text = string.Empty;
        txbValidity.Text = string.Empty;
        txbRepeatCount.Text = string.Empty;
        chkNumber.Checked = false;
        chkLowLetter.Checked = false;
        chkUpLetter.Checked = false;
        chkSpecial.Checked = false;
        txbPwdStructureDesc.Text = string.Empty;
    }

    protected void ChangeControlState()
    {
        txbRuleName.ReadOnly = !txbRuleName.ReadOnly;
        txbMinLen.ReadOnly = !txbMinLen.ReadOnly;
        txbMaxLen.ReadOnly = !txbMaxLen.ReadOnly;
        txbRepeatCount.ReadOnly = !txbRepeatCount.ReadOnly;
        txbValidity.ReadOnly = !txbValidity.ReadOnly;
    }

    protected void CheckControlText(ref bool bIsValid)
    {
        string strError = string.Empty;

        if (txbRuleName.Text == string.Empty)
        {
            strError += "- �����������Ϊ��\\n";
        }
        if (txbMinLen.Text == string.Empty)
        {
            strError += "- ������С����Ϊ��\\n";
        }
        if (txbMaxLen.Text == string.Empty)
        {
            strError += "- ������󳤶�Ϊ��\\n";
        }
        if (txbValidity.Text == string.Empty)
        {
            strError += "- ������Ч��Ϊ��\\n";
        }
        if (txbRepeatCount.Text == string.Empty)
        {
            strError += "- �����ظ�����Ϊ��\\n";
        }
        if (!chkNumber.Checked && !chkLowLetter.Checked && !chkUpLetter.Checked && !chkSpecial.Checked)
        {
            strError += "- ���빹��û��ѡ��һ���\\n";
        }
        if (strError != string.Empty)
        {
            bIsValid = false;
        }
        else
        {
            if (!IsNumeric(txbMinLen.Text.Trim()) || !IsNumeric(txbMaxLen.Text.Trim()))
            {
                strError += "- ������С���Ȼ���󳤶Ȳ�������\\n";
            }
            if (!IsNumeric(txbValidity.Text.Trim()))
            {
                strError += "- ������Ч�ڲ�������\\n";
            }
            if (!IsNumeric(txbRepeatCount.Text.Trim()))
            {
                strError += "- �����ظ�������������\\n";
            }

            if (strError != string.Empty)
            {
                bIsValid = false;
            }
            else
            {
                if (int.Parse(txbMinLen.Text.Trim()) == 0 || int.Parse(txbMaxLen.Text.Trim()) == 0)
                {
                    strError += "- ������С����Ϊ����󳤶�0";
                }
                if (int.Parse(txbMinLen.Text.Trim()) >= int.Parse(txbMaxLen.Text.Trim()))
                {
                    strError += "- ������С���ȴ��ڵ���������󳤶�";
                }
                if (strError != string.Empty)
                {
                    bIsValid = false;
                }
                else
                {
                    int temp = 0;
                    temp = chkNumber.Checked ? temp + 1 : temp;
                    temp = chkLowLetter.Checked ? temp + 1 : temp;
                    temp = chkUpLetter.Checked ? temp + 1 : temp;
                    temp = chkSpecial.Checked ? temp + 1 : temp;
                    if (temp > Convert.ToInt32(txbMinLen.Text.Trim()))
                    {
                        strError = "- ����������"+temp.ToString()+"λ";
                    }
                    if (strError != string.Empty)
                    {
                        bIsValid = false;
                    }
                }
            }
        }
        if (strError != string.Empty)
        {
            ltlAlert.Text = "alert('���������ֶΣ�\\n" + strError + "')";
        }
    }

    protected bool IsNumeric(string strValue)
    {
        try
        {
            int val = int.Parse(strValue);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void GenerateStructureDesc()
    {
        txbPwdStructureDesc.Text = "����������£�\r\n";
        if (chkNumber.Checked && chkLowLetter.Checked && chkUpLetter.Checked && chkSpecial.Checked)
        {
            txbPwdStructureDesc.Text += ("- �������������֡�Сд��ĸ����д��ĸ�������ַ�\r\n");
        }
        else
        {
            txbPwdStructureDesc.Text += "- ����������";
            if (chkNumber.Checked)
            {
                txbPwdStructureDesc.Text += "���֡�";
            }
            if (chkLowLetter.Checked)
            {
                txbPwdStructureDesc.Text += "Сд��ĸ��";
            }
            if (chkUpLetter.Checked)
            {
                txbPwdStructureDesc.Text += "��д��ĸ��";
            }
            if (chkSpecial.Checked)
            {
                txbPwdStructureDesc.Text += "�����ַ���";
            }
            txbPwdStructureDesc.Text = txbPwdStructureDesc.Text.Substring(0, txbPwdStructureDesc.Text.Length - 1);
            txbPwdStructureDesc.Text += "\r\n- ";
        }
        switch (dropValidity.SelectedValue)
        { 
            case "YEAR":
                txbPwdStructureDesc.Text += ("������Ч��Ϊ��" + txbValidity.Text.Trim() + "��");
                break;
            case "MONTH":
                txbPwdStructureDesc.Text += ("������Ч��Ϊ��" + txbValidity.Text.Trim() + "����");
                break;
            case "DAY":
                txbPwdStructureDesc.Text += ("������Ч��Ϊ��" + txbValidity.Text.Trim() + "��");
                break;
            case "HOUR":
                txbPwdStructureDesc.Text += ("������Ч��Ϊ��" + txbValidity.Text.Trim() + "Сʱ");
                break;
            case "MINUTE":
                txbPwdStructureDesc.Text += ("������Ч��Ϊ��" + txbValidity.Text.Trim() + "����");
                break;
            case "SECOND":
                txbPwdStructureDesc.Text += ("������Ч��Ϊ��" + txbValidity.Text.Trim() + "����");
                break;
            default:
                break;
        }
        txbPwdStructureDesc.Text += ("\r\n- ������̣�" + txbMinLen.Text.Trim() + "λ�����" + txbMaxLen.Text.Trim() + "λ\r\n");
        txbPwdStructureDesc.Text += ("- �����ظ�������" + txbRepeatCount.Text.ToString().Trim() + " ���������벻����ǰ" + txbRepeatCount.Text.ToString() + "��������ͬ");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('�����µ�¼��')";
            return;
        }
        if (dropRules.SelectedValue == string.Empty)
        {
            ltlAlert.Text = "alert('��ѡ��Ҫ�༭���������')";
            return;
        }
        if (btnEdit.Text == "�༭")
        {
            ChangeControlState();
            btnEdit.Text = "����";
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
        }
        else if (btnEdit.Text == "����")
        {
            bool bIsValid = true;
            CheckControlText(ref bIsValid);
            if (bIsValid)
            {
                GenerateStructureDesc();
                int intRetValue = UpdatePasswordRule(int.Parse(dropRules.SelectedValue), txbRuleName.Text.Trim(), int.Parse(txbMinLen.Text.Trim()), int.Parse(txbMaxLen.Text.Trim()), int.Parse(txbValidity.Text.Trim()), dropValidity.SelectedValue, int.Parse(txbRepeatCount.Text.Trim()), chkNumber.Checked, chkLowLetter.Checked, chkUpLetter.Checked, chkSpecial.Checked, txbPwdStructureDesc.Text.Trim(), chkDefaultRule.Checked);
                if (intRetValue == 1)
                {
                    ltlAlert.Text = "alert('�༭�������ɹ���')";
                }
                else if (intRetValue == 0)
                {
                    ltlAlert.Text = "alert('�ù��������Ѵ��ڣ�')";
                }
                else
                {
                    ltlAlert.Text = "alert('�༭�������ʧ�ܣ���ˢ�º����ԣ�')";
                }
                ChangeControlState();
                btnEdit.Text = "�༭";
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                BindData();
            }
        }
        else
        {
            btnEdit.Text = "�༭";
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('�����µ�¼��')";
            return;
        }
        if (btnAdd.Text == "����")
        {
            InitControlState();
            ChangeControlState();
            btnAdd.Text = "����";
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        else if (btnAdd.Text == "����")
        {
            bool bIsValid = true;
            CheckControlText(ref bIsValid);
            if (bIsValid)
            {
                GenerateStructureDesc();
                int intRetValue = AddNewRule(txbRuleName.Text.Trim(), int.Parse(txbMinLen.Text.Trim()), int.Parse(txbMaxLen.Text.Trim()), int.Parse(txbValidity.Text.Trim()), dropValidity.SelectedValue, int.Parse(txbRepeatCount.Text.Trim()), chkNumber.Checked, chkLowLetter.Checked, chkUpLetter.Checked, chkSpecial.Checked, txbPwdStructureDesc.Text.Trim(), chkDefaultRule.Checked);
                if (intRetValue == 1)
                {
                    ltlAlert.Text = "alert('�����������ɹ���')";
                }
                else if (intRetValue == 0)
                {
                    ltlAlert.Text = "alert('�ù��������Ѵ��ڣ�')";
                }
                else
                {
                    ltlAlert.Text = "alert('�����������ʧ�ܣ���ˢ�º����ԣ�')";
                }
                ChangeControlState();
                btnAdd.Text = "����";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                BindData();
            }
        }
        else
        {
            btnAdd.Text = "����";
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('�����µ�¼��')";
            return;
        }
        if (dropRules.SelectedValue == string.Empty)
        {
            ltlAlert.Text = "alert('��ѡ��Ҫɾ�����������')";
            return;
        }
        btnAdd.Enabled = false;
        btnEdit.Enabled = false;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ruleID", dropRules.SelectedValue);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_hr_deletePasswordRule", param);
            if (!Convert.ToBoolean(param[1].Value))
            {
                ltlAlert.Text = "alert('" + dropRules.SelectedItem.Text + "ɾ��ʧ�ܣ���ˢ�º����ԣ�')";
            }
        }
        catch
        {
            ltlAlert.Text = "alert('" + dropRules.SelectedItem.Text + "ɾ���쳣�������µ�¼�����ԣ�')";
        }
        finally
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            BindData();
        }
    }

    protected int AddNewRule(string strRuleName, int intMinLen, int intMaxLen, int intValidityCount, string strValidityUnit, int intRepeatCount, bool hasNumber, bool hasLowLetter, bool hasUpLetter, bool hasSpecial, string strStructureDesc, bool isDefault)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@ruleName", strRuleName);
            param[1] = new SqlParameter("@minLen", intMinLen);
            param[2] = new SqlParameter("@maxLen", intMaxLen);
            param[3] = new SqlParameter("@validityCount", intValidityCount);
            param[4] = new SqlParameter("@validityUnit", strValidityUnit);
            param[5] = new SqlParameter("@repeatCount", intRepeatCount);
            param[6] = new SqlParameter("@hasNumber", hasNumber);
            param[7] = new SqlParameter("@numberRegex", strNumberRegex);
            param[8] = new SqlParameter("@hasLowLetter", hasLowLetter);
            param[9] = new SqlParameter("@lowLetterRegex", strLowLetterRegex);
            param[10] = new SqlParameter("@hasUpLetter", hasUpLetter);
            param[11] = new SqlParameter("@upLetterRegex", strUpLetterRegex);
            param[12] = new SqlParameter("@hasSpecial", hasSpecial);
            param[13] = new SqlParameter("@specialRegex", strSpecialRegex);
            param[14] = new SqlParameter("@structureDesc", strStructureDesc);
            param[15] = new SqlParameter("@isDefault", isDefault);
            param[16] = new SqlParameter("@createBy", Session["uID"]);
            param[17] = new SqlParameter("@retValue", DbType.Int32);
            param[17].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_hr_insertPasswordRule", param);
            return Convert.ToInt32(param[17].Value);
        }
        catch
        {
            return -1;
        }
    }

    protected int UpdatePasswordRule(int intruleID, string strRuleName, int intMinLen, int intMaxLen, int intValidityCount, string strValidityUnit, int intRepeatCount, bool hasNumber, bool hasLowLetter, bool hasUpLetter, bool hasSpecial, string strStructureDesc, bool isDefault)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@ruleID", intruleID);
            param[1] = new SqlParameter("@ruleName", strRuleName);
            param[2] = new SqlParameter("@minLen", intMinLen);
            param[3] = new SqlParameter("@maxLen", intMaxLen);
            param[4] = new SqlParameter("@validityCount", intValidityCount);
            param[5] = new SqlParameter("@validityUnit", strValidityUnit);
            param[6] = new SqlParameter("@repeatCount", intRepeatCount);
            param[7] = new SqlParameter("@hasNumber", hasNumber);
            param[8] = new SqlParameter("@numberRegex",strNumberRegex);
            param[9] = new SqlParameter("@hasLowLetter", hasLowLetter);
            param[10] = new SqlParameter("@lowLetterRegex",strLowLetterRegex);
            param[11] = new SqlParameter("@hasUpLetter", hasUpLetter);
            param[12] = new SqlParameter("@upLetterRegex",strUpLetterRegex);
            param[13] = new SqlParameter("@hasSpecial", hasSpecial);
            param[14] = new SqlParameter("@specialRegex",strSpecialRegex);
            param[15] = new SqlParameter("@structureDesc", strStructureDesc);
            param[16] = new SqlParameter("@isDefault", isDefault);
            param[17] = new SqlParameter("@modifyBy", Session["uID"]);
            param[18] = new SqlParameter("@retValue", DbType.Int16);
            param[18].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_hr_updatePasswordRule", param);
            return Convert.ToUInt16(param[18].Value);
        }
        catch
        {
            return -1;
        }
    }

    protected void dropRules_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsRule = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ruleID", int.Parse(dropRules.SelectedValue));
            dsRule = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_hr_selectSelectdRule", param);
        }
        catch
        {
            dsRule = null;
        }
        if (dsRule != null && dsRule.Tables[0].Rows.Count > 0)
        {
            InitControlState();
            dropRules.SelectedValue = dsRule.Tables[0].Rows[0]["id"].ToString();
            chkDefaultRule.Checked = Convert.ToBoolean(dsRule.Tables[0].Rows[0]["isDefault"]);
            txbRuleName.Text = dsRule.Tables[0].Rows[0]["name"].ToString();
            txbMinLen.Text = dsRule.Tables[0].Rows[0]["minLen"].ToString();
            txbMaxLen.Text = dsRule.Tables[0].Rows[0]["maxLen"].ToString();
            txbValidity.Text = dsRule.Tables[0].Rows[0]["validityCount"].ToString();
            dropValidity.SelectedValue = dsRule.Tables[0].Rows[0]["validityUnit"].ToString();
            txbRepeatCount.Text = dsRule.Tables[0].Rows[0]["repeatCount"].ToString();
            chkNumber.Checked = Convert.ToBoolean(dsRule.Tables[0].Rows[0]["hasNumber"]);
            chkLowLetter.Checked = Convert.ToBoolean(dsRule.Tables[0].Rows[0]["hasLowLetter"]);
            chkUpLetter.Checked = Convert.ToBoolean(dsRule.Tables[0].Rows[0]["hasUpLetter"]);
            chkSpecial.Checked = Convert.ToBoolean(dsRule.Tables[0].Rows[0]["hasSpecial"]);
            txbPwdStructureDesc.Text = dsRule.Tables[0].Rows[0]["structureDesc"].ToString();
            dsRule.Dispose();
        }
        else
        {
            ltlAlert.Text = "alert('" + dropRules.SelectedItem.Text + "������Ϣ��ȡʧ�ܣ�')";
        }
    }
}
