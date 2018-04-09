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
using Wage;
using adamFuncs;

public partial class hr_AttendanceCenter : BasePage
{
    HR hr = new HR();
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            BindData();
            ExportExcel();
        }
    }

    protected void BindData()
    {
        //�������
        string strCenter = txtCenter.Text.Trim();
        string strSensor = txtSensor.Text.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        gvCenter.DataSource = hr.SelectAttendanceCenter(strCenter, strSensor, strPlant, strUserNo);
        gvCenter.DataBind();
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCenter_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCenter.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvCenter_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //�������
        string strCID = gvCenter.DataKeys[e.RowIndex].Value.ToString();

        if (hr.DeleteAttendanceCenter(strCID))
        {
            ltlAlert.Text = "alert('ɾ���ɹ�!');";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('ɾ�����ݹ��̳���'); ";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtCenter.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('�ɱ����� ����Ϊ�գ�');";
            return;
        }

        if (txtSensor.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('�豸�� ����Ϊ�գ�');";
            return;
        }

        //������� 
        string strCode = txtCenter.Text.Trim();
        string strSensor = txtSensor.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);
        string strName = hr.CheckCenterQADIsExist(strCode, strPlant);
        string strUserNo = txtUserNo.Text.Trim();
        string strUserID = hr.CheckUserIsValid(strUserNo, strPlant);

        if (strName.Length == 0)
        {
            ltlAlert.Text = "alert('�ɱ�����Ϊ" + strCode + "��QADϵͳ�в����ڣ�'); document.getElementById('txtCenter').value = ''; Form1.txtCenter.focus(); ";
            return;
        }
        else
        {
            if (hr.CheckRecordIsExist(strCode, strSensor, strPlant, strUserNo))
            {
                if (strUserNo.Length == 0)
                    ltlAlert.Text = "alert('�ɱ�����Ϊ" + strCode + "���߿����豸��Ϊ" + strSensor + "�ļ�¼�Ѵ��ڣ�'); ";
                else
                    ltlAlert.Text = "alert('Ա������Ϊ" + strUserNo + "�ڳɱ�����Ϊ" + strCode + "���߿����豸��Ϊ" + strSensor + "�ļ�¼�Ѵ��ڣ�'); ";

                return;
            }
            else
            {
                if (strUserID.Length == 0 && strUserNo.Length > 0)
                {
                    ltlAlert.Text = "alert('Ա������Ϊ" + strUserNo + "�����ڻ�����ɾ����������ְ������Ч��'); ";
                    txtUserNo.Text = "";
                    return;
                }
                else
                {
                    HR_AttendanceCenter hr_ac = new HR_AttendanceCenter();
                    hr_ac.CenterCode = strCode;
                    hr_ac.CenterName = strName;
                    hr_ac.Sensor = strSensor;
                    hr_ac.orgID = Convert.ToInt32(Session["PlantCode"]);
                    hr_ac.UserNo = strUserNo;
                    hr_ac.UserID = strUserID;

                    if (hr.InsertAttendanceCenter(hr_ac))
                    {
                        ltlAlert.Text = "alert('���ӳɹ���');";
                        BindData();
                    }
                    else
                    {
                        ltlAlert.Text = "alert('�������ݹ��̳���'); ";
                    }
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        ExportExcel();
    }


    private void ExportExcel()
    {
        string str = "";
        str = " Select ID As CenterID,cc As CenterCode, ccname As CenterName,  PlantID As orgID, ccname + '(' + cc + ')' As Center, ";
        str = str + " Case When userName Is Null Then N'����(*)' Else userName + '(' + ac.userNo + ')' End As UserNo, Sensorid As Sensor ";
        str = str + " From tcpc0.dbo.hr_Attendance_CC ac   Left Outer Join tcpc0.dbo.Users u On u.userID = ac.UserID Where PlantID =" + Session["plantcode"].ToString();
        if (txtCenter.Text.Trim().Length >0)
            str = str + " And cc = " + txtCenter.Text.Trim();
        if (txtSensor.Text.Trim().Length > 0)
            str = str + " And Sensorid = " + txtSensor.Text.Trim();
        if (txtUserNo.Text.Trim().Length > 0)
            str = str + " And u.userNo = " + txtUserNo.Text.Trim();

        Session["EXHeader"] = "";
        Session["EXSQL"] = str;
        Session["EXTitle"] = "<b>�ɱ�����</b>~^<b>Ա������</b>~^<b>�豸��</b>~^";
    }
}
