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
using Microsoft.ApplicationBlocks.Data;

namespace Wage
{
    public partial class hr_allowance : BasePage
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

                dropRole.DataSource = hr.GetRole(strPlantID);
                dropRole.DataBind();
                dropRole.Items.Insert(0, new ListItem("--", "0"));

                txtYear.Text = DateTime.Now.Year.ToString();
                dropMonth.SelectedValue = DateTime.Now.Month.ToString();

                btnSave.Attributes.Add("onclick", "return confirm('你确定要保存吗?');");
                DataGridBind();

            }
        }

        protected void DataGridBind()
        {
            string strAllowId = lblAllowID.Text.Trim();
            string strPlantId = Session["PlantCode"].ToString().Trim();
            string strTypeId = dropType.SelectedValue;
            string strTemp = Session["temp"].ToString().Trim();
            string strYear = txtYear.Text.Trim();
            string strMonth = dropMonth.SelectedValue.Trim();
            string strUserNo = txtUserNo.Text.Trim();
            string strUserName = txtName.Text.Trim();
            string strDept = dropDept.SelectedValue;
            string strRole = dropRole.SelectedValue;

            DataSet dsAllow = hr.GetAllowance(strPlantId, strTypeId, strTemp, strYear, strMonth, strUserNo, strUserName, strDept, strRole);
            int total=dsAllow.Tables[0].Rows.Count;
            int pagecount = total / dgAllow.PageSize;
            if (total % dgAllow.PageSize>0)
            {
                pagecount += 1;
            }
            if (dgAllow.CurrentPageIndex >= pagecount)
            {
                if (pagecount==0)
                {
                    dgAllow.CurrentPageIndex=0;
                }
                else 
                {
                    dgAllow.CurrentPageIndex = pagecount - 1;
                }
            }

            dgAllow.DataSource = dsAllow;
            dgAllow.DataBind();
            createSQL();

        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            DataGridBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strUID = Session["uID"].ToString();
            string strAllowId = lblAllowID.Text.Trim();
            string strPlantId = Session["PlantCode"].ToString().Trim();
            string strTemp = Session["temp"].ToString().Trim();
            string strYear = txtYear.Text.Trim();
            string strMonth = dropMonth.SelectedValue.Trim();
            string strUserNo = txtUserNo.Text.Trim();
            string strAmount = txtAmount.Text.Trim();
            string strType = dropType.SelectedValue;
            string strCom = txtCom.Text.Trim();

            if (strYear == string.Empty)
            {
                ltlAlert.Text = "alert('年月不能为空');";
                return;
            }
            try
            {
                DateTime dt = Convert.ToDateTime(strYear + "-1-1");
            }
            catch
            {
                ltlAlert.Text = "alert('年月格式不对。例如：2009');";
                return;
            }

            if (strUserNo == string.Empty)
            {
                ltlAlert.Text = "alert('工号不能为空');";
                return;
            }

            if (strAmount == string.Empty)
            {
                ltlAlert.Text = "alert('津贴金额不能为空');";
                return;
            }

            try
            {
                Decimal dec = Convert.ToDecimal(strAmount);
            }
            catch
            {
                ltlAlert.Text = "alert('津贴金额的格式不正确');";
                return;
            }

            //Adjudge whether the record is exist 
            string strRecord = "0";
            adamClass adc = new adamClass();
            string str = "SELECT ISNULL(id,0) FROM tcpc" + Convert.ToString(Session["Plantcode"]) + ".dbo.hr_allowance WHERE  Year(WorkDate) =" + strYear + " AND Month(WorkDate)=" + strMonth + " AND ";
            str = str + " userID IN (SELECT userID FROM tcpc0.dbo.Users Where userno='" + strUserNo + "' And plantcode=" + Convert.ToString(Session["Plantcode"]) + " ) ";
            strRecord = SqlHelper.ExecuteScalar(adc.dsn0(), CommandType.Text, str) == null ? "0" : SqlHelper.ExecuteScalar(adc.dsn0(), CommandType.Text, str).ToString();

            int nRet = hr.SaveAllowance(strAllowId, strPlantId, strType, strYear + "-" + strMonth + "-1", strUserNo, strAmount, strCom, strUID, strRecord);
            if (nRet == 0)
            {
                ltlAlert.Text = "alert('津贴保存成功!');";
                txtUserNo.Text = string.Empty;
                txtName.Text = string.Empty;
                txtAmount.Text = string.Empty;
            }
            else
            {
                if (nRet == 1)
                    ltlAlert.Text = "alert('员工不存在!');";
                else if (nRet == 2)
                    ltlAlert.Text = "alert('所添加的其他津贴已经存在!');";
                else if (nRet == 3)
                    ltlAlert.Text = "alert('所添加的独身子女津贴已经存在!');";
                else
                    ltlAlert.Text = "alert('数据库操作错误!');";
            }

            DataGridBind();
        }
        protected void dgAllow_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex != -1)
                {
                    int id = e.Item.ItemIndex + 1;
                    id = dgAllow.CurrentPageIndex * 16 + id;
                    e.Item.Cells[0].Text = id.ToString();
                }
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                ((TextBox)e.Item.Cells[3].Controls[0]).Style.Add("width", "50px");
                ((TextBox)e.Item.Cells[6].Controls[0]).Style.Add("width", "700px");
            }
        }
        protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgAllow.Columns[3].HeaderText = dropType.SelectedItem.Text.Trim();

            DataGridBind();
        }
        protected void dgAllow_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgAllow.CurrentPageIndex = e.NewPageIndex;
            DataGridBind();
        }
        protected void dgAllow_EditCommand(object source, DataGridCommandEventArgs e)
        {
            dgAllow.EditItemIndex = e.Item.ItemIndex;
            DataGridBind();
        }
        protected void dgAllow_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            dgAllow.EditItemIndex = -1;
            DataGridBind();
        }
        protected void dgAllow_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            string allowid = dgAllow.DataKeys[e.Item.ItemIndex].ToString();
            string plantid = Session["PlantCode"].ToString().Trim();

            bool bRet = hr.DeleteAllowance(allowid, plantid, Convert.ToInt32(dropType.SelectedValue));
            if (bRet)
                ltlAlert.Text = "alert('删除成功!');";
            else
                ltlAlert.Text = "alert('删除失败!');";

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

                else if (strUserName == "NoThisUser")
                    ltlAlert.Text = "alert('此员工不存在!');";
                else
                {
                    if (strUserName == "Leaved-User")
                        ltlAlert.Text = "alert('此员工属于离职员工!');";
                    txtName.Text = strUserName;
                    ltlAlert.Text = "Form1.txtAmount.focus();";
                }
            }
        }
        protected void dgAllow_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            bool bFlag = true;

            string plantid = Session["PlantCode"].ToString().Trim();
            string id = dgAllow.DataKeys[e.Item.ItemIndex].ToString();
            string amount = ((TextBox)e.Item.Cells[3].Controls[0]).Text.Trim();
            string rmks = ((TextBox)e.Item.Cells[6].Controls[0]).Text.Trim();

            if (amount == string.Empty)
            {
                ltlAlert.Text = "alert('金额不能为空!');";
                bFlag = false;
            }
            else
            {
                try
                {
                    decimal d = Convert.ToDecimal(amount);
                }
                catch
                {
                    ltlAlert.Text = "alert('金额格式不正确!');";
                    bFlag = false;
                }
            }

            if (bFlag)
            {
                if (hr.ModifyAllowance(plantid, id, amount, rmks, dropType.SelectedValue))
                    ltlAlert.Text = "alert('修改成功!');";
                else
                    ltlAlert.Text = "alert('修改失败!');";

                dgAllow.EditItemIndex = -1;

                DataGridBind();
            }
        }

        protected string createSQL()
        {
            string str = "SELECT u.userNo,u.userName,ISNULL(d.name,''),";
            switch (Convert.ToInt32(dropType.SelectedValue))
            {
                case 0:
                    str = str + " a.amount,a.comment, ";
                    break;
                case 1:
                    str = str + " a.singleton As amount,a.comment, ";
                    break;
                case 2:
                    str = str + " a.hightemper As amount,a.hcomment As comment, ";
                    break;
                case 3:
                    str = str + " a.humanAllowance As amount,a.Acomment As comment, ";
                    break;
                case 4:
                    str = str + " a.newUser As amount,a.ncomment As comment, ";
                    break;
                case 5:
                    str = str + " a.student As amount,a.scomment As comment, ";
                    break;
                case 6:
                    str = str + " a.bonus As amount,a.bcomment As comment, ";
                    break;
            }

            str = str + " u.enterdate,u.leavedate FROM tcpc" + Convert.ToString(Session["plantcode"]) + ".dbo.hr_Allowance a  ";
            str = str + "INNER JOIN tcpc0.dbo.users u ON u.userID =a.userID ";
            str = str + " LEFT OUTER JOIN tcpc" + Convert.ToString(Session["plantcode"]) + ".dbo.departments d ON d.departmentID = u.departmentID ";
            str = str + " WHERE Year(workdate)=" + txtYear.Text.Trim() + " AND Month(workdate)=" + dropMonth.SelectedValue;
            if (txtUserNo.Text.Trim().Length > 0)
                str = str + " And u.userNo = '" + txtUserNo.Text.Trim() + "' ";
            if (dropDept.SelectedIndex > 0)
                str = str + " And u.departmentID =" + dropDept.SelectedValue;

            switch (Convert.ToInt32(dropType.SelectedValue))
            {
                case 0:
                    str = str + " And a.amount is not null ";
                    break;
                case 1:
                    str = str + " And a.singleton is not null ";
                    break;
                case 2:
                    str = str + " And a.hightemper is not null ";
                    break;
                case 3:
                    str = str + " And a.humanAllowance is not null ";
                    break;
                case 4:
                    str = str + " And a.newUser is not null ";
                    break;
                case 5:
                    str = str + " And a.student is not null ";
                    break;
                case 6:
                    str = str + " And a.bonus is not null ";
                    break;
            }


            str = str + " Order by u.userID,workdate ";
            return str;

        }


        protected void ButExcel_Click(object sender, EventArgs e)
        {
            string EXTitle = "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>金额</b>~^<b>备注</b>~^<b>入公司日期</b>~^<b>离职日期</b>~^";
            string ExSql = createSQL();
            this.ExportExcel(adam.dsnx(), EXTitle, ExSql, false);
        }

    }
}