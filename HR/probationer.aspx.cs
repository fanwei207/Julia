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

namespace Wage
{
    public partial class hr_probationer : BasePage
    {
        adamClass adam = new adamClass();
        HR hr = new HR();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPlantID = Session["PlantCode"].ToString().Trim();

                dropDept.DataSource = hr.GetDept(strPlantID);
                dropDept.DataBind();
                dropDept.Items.Insert(0, new ListItem("--", "0"));

                txtYear.Text = DateTime.Now.Year.ToString();
                dropMonth.SelectedValue = DateTime.Now.Month.ToString();

                DataGridBind();
            }
        }

        private void DataGridBind()
        {
            string plantid = Session["PlantCode"].ToString().Trim();
            string temp = Session["temp"].ToString().Trim();
            string year = txtYear.Text.Trim();
            string month = dropMonth.SelectedValue;
            string userno = txtUserNo.Text.Trim();
            string username = txtUserName.Text.Trim();
            string dept = dropDept.SelectedValue;
            string enterdate = txtEnterDate.Text.Trim();

            DataSet _ds = hr.GetProbationer(plantid, temp, year, month, userno, username, dept, enterdate);

            dgProb.DataSource = _ds;
            dgProb.DataBind();

            _ds.Dispose();
        }
        protected void dgProb_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgProb.CurrentPageIndex = e.NewPageIndex;

            DataGridBind();
        }
        protected void dgProb_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            string strId = e.Item.Cells[10].Text.Trim();
            string strPlantId = Session["PlantCode"].ToString().Trim();

            bool bRet = hr.DeleteProbationer(strPlantId, strId);

            if (bRet)
                ltlAlert.Text = "alert('删除成功!');";
            else
                ltlAlert.Text = "alert('删除失败!');";

            DataGridBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEnterDate.Text.Trim() != string.Empty)
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(txtEnterDate.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('输入日期不规范!');Form1.txtEnterDate.focus();";
                    return;
                }
            }

            DataGridBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtEnterDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('入公司日期不能为空！');Form1.txtEnterDate.focus();";
                return;
            }
            else
            {

                try
                {
                    DateTime dt = Convert.ToDateTime(txtEnterDate.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('输入入公司日期不规范.例1999-1-1！');Form1.txtEnterDate.focus();";
                    return;
                }
            }

            if (txtAttendence.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('应出勤不能为空！');Form1.attendence.focus();";
                return;
            }
            else
            {
                try
                {
                    int n = Convert.ToInt32(txtAttendence.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('应出勤只能为数字！');Form1.attendence.focus();";
                    return;
                }
            }

            string plantid = Session["PlantCode"].ToString();
            string uID = Session["uID"].ToString();
            string enterdate = txtEnterDate.Text.Trim();
            string userno = txtUserNo.Text.Trim();
            string year = txtYear.Text.Trim();
            string month = dropMonth.SelectedValue;
            string attendence = txtAttendence.Text.Trim();

            bool bRet = hr.SaveProbationer(plantid, uID, enterdate, userno, year, month, attendence);

            if (bRet)
                ltlAlert.Text = "alert('保存成功!');";
            else
                ltlAlert.Text = "alert('保存失败!');";

            DataGridBind();
        }
        protected void txtUserNo_TextChanged(object sender, EventArgs e)
        {
            string strDate = txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-01";
            string strUserNo = txtUserNo.Text.Trim();
            string strTemp = Session["temp"].ToString().Trim();
            string plantid = Session["PlantCode"].ToString().Trim();

            if (strUserNo != string.Empty)
            {
                string strUserName = hr.GetUserNameByNo(plantid, strTemp, strUserNo, strDate);

                if (strUserName == "DB-Opt-Err")
                    ltlAlert.Text = "alert('数据库操作错误!');";
                else if (strUserName == "UserHadLeaved")
                    ltlAlert.Text = "alert('此员工已经离职!');";
                else if (strUserName == "Leaved-User")
                    ltlAlert.Text = "alert('此员工属于离职员工!');";
                else if (strUserName == "NoThisUser")
                    ltlAlert.Text = "alert('此员工不存在!');";
                else
                {
                    txtUserName.Text = strUserName;
                    dropDept.SelectedValue = hr.GetUserDeptByNo(plantid, strTemp, strUserNo);
                    txtEnterDate.Text = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(hr.GetUserEnterDateByNo(plantid, strTemp, strUserNo)));
                    ltlAlert.Text = "Form1.dropDept.focus();";
                }
            }
        }
        protected void dgProb_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex != -1)
                {
                    int id = e.Item.ItemIndex + 1;
                    id = dgProb.CurrentPageIndex * 16 + id;
                    e.Item.Cells[0].Text = id.ToString();
                }
            }
        }
}
}
