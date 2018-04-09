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
    public partial class hr_deduct : BasePage
    {
        adamClass adam = new adamClass();
        HR hr = new HR();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPlantID = Session["PlantCode"].ToString().Trim();

                if (Request["sdate"] == null || Request["sdate"].ToString() == string.Empty)
                    txtStdDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                else
                    txtStdDate.Text = Request["sdate"];

                txtWorkDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

                dropType.DataSource = hr.GetType(strPlantID);
                dropType.DataBind();
                dropType.Items.Insert(0, new ListItem("--", "0"));

                dropType2.DataSource = hr.GetType(strPlantID);
                dropType2.DataBind();
                dropType2.Items.Insert(0, new ListItem("--", "0"));

                dropMoneyType.Items.Insert(0, new ListItem("--", "0"));

                dropDept.DataSource = hr.GetDept(strPlantID);
                dropDept.DataBind();
                dropDept.Items.Insert(0, new ListItem("--", "0"));

                DataGridBind();
            }
        }

        private void DataGridBind()
        {
            string plantid = Session["PlantCode"].ToString();
            int temp = int.Parse(Session["temp"].ToString());
            int status = int.Parse(dropStatus.SelectedValue);
            int role = int.Parse(Session["uRole"].ToString());
            int uID = int.Parse(Session["uID"].ToString());
            int conceal = int.Parse(Session["conceal"].ToString());
            string startdate = txtStdDate.Text.Trim();
            string enddate = txtEndDate.Text.Trim();
            int dept = int.Parse(dropDept.SelectedValue);
            string userno = txtUserNo.Text.Trim();
            string username = txtUserName.Text.Trim();
            int type = int.Parse(dropType.SelectedValue);

           

            DataSet _ds = hr.GetDeduct(plantid, temp, status, role, uID, conceal, startdate, enddate, dept, userno, username, type);

            dgDeduct.DataSource = _ds;
            dgDeduct.DataBind();

            _ds.Dispose();
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtStdDate.Text.Trim() != string.Empty)
            {
                try
                {
                    DateTime dt1 = Convert.ToDateTime(txtStdDate.Text.Trim());

                    if (txtEndDate.Text.Trim() != string.Empty) 
                    {
                        try
                        {
                            DateTime dt2 = Convert.ToDateTime(txtEndDate.Text.Trim());
                        }
                        catch 
                        {
                            ltlAlert.Text = "alert('输入结束日期不正确!'); Form1.txtEndDate.focus();";
                            return;
                        }
                    }
                }
                catch
                {
                    ltlAlert.Text = "alert('输入起始日期不正确!'); Form1.txtStdDate.focus();";
                    return;
                }
            }
            else
            {
                ltlAlert.Text = "alert('起始日期不能为空!'); Form1.txtStdDate.focus();";
                return;
            }

            DataGridBind();
        }
        protected void dgDeduct_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            string strId = e.Item.Cells[12].Text.Trim();
            string strPlantId = Session["PlantCode"].ToString().Trim();

            bool bRet = hr.DeleteDeduct(strPlantId, strId);

            if (bRet)
                ltlAlert.Text = "alert('删除成功!');";
            else
                ltlAlert.Text = "alert('删除失败!');";

            DataGridBind();
        }
        protected void dgDeduct_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgDeduct.CurrentPageIndex = e.NewPageIndex;

            DataGridBind();
        }

        protected void dgDeduct_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex != -1)
                {
                    int id = e.Item.ItemIndex + 1;
                    id = dgDeduct.CurrentPageIndex * 19 + id;
                    e.Item.Cells[0].Text = id.ToString();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            HR hr_salary = new HR();
            int intadjust = hr_salary.finAdjust(Convert.ToDateTime(txtWorkDate.Text).Year, Convert.ToDateTime(txtWorkDate.Text).Month, Convert.ToInt32(Session["PlantCode"]), 0);
            if (intadjust < 0)
            {
                string str = @"<script language='javascript'> alert('工资已被财务冻结，不能操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
                return;
            }

            if (txtWorkDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('日期不能为空!');";
                return;
            }
            else 
            {
                try
                {
                    DateTime _dd = DateTime.Parse(txtWorkDate.Text.Trim());
                }
                catch 
                {
                    ltlAlert.Text = "alert('日期格式不对!');";
                    return;
                }
            }

            if (txtUserNo2.Text.Trim() == string.Empty || txtUserName2.Text.Trim() == string.Empty) 
            {
                ltlAlert.Text = "alert('员工信息不正确!');";
                return;
            }

            if (dropType2.SelectedIndex == 0) 
            {
                ltlAlert.Text = "alert('请选择一项性质!');";
                return;
            }

            if (txtAmount.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('金额不能为空!');";
                return;
            }
            else 
            {
                try
                {
                    Decimal _dd = Decimal.Parse(txtAmount.Text.Trim());
                }
                catch 
                {
                    ltlAlert.Text = "alert('金额格式不对!');";
                    return;
                }
            }

            string plantid = Session["PlantCode"].ToString();
            string workdate = txtWorkDate.Text.Trim();
            string userno = txtUserNo2.Text.Trim();
            string typeid = dropType2.SelectedValue;
            string moneyid = dropMoneyType.SelectedValue;
            string amount = txtAmount.Text.Trim();
            string comment = txtRmks.Text.Trim();
            string uID = Session["uID"].ToString();

            if (hr.SaveDeduct(plantid, workdate, userno, typeid, moneyid, amount, comment, uID))
            {
                //ltlAlert.Text = "alert('保存成功!');Form1.txtUserNo2.focus();";
                ltlAlert.Text = "Form1.txtUserNo2.focus();";
                txtUserNo2.Text = "";
                dropType2.SelectedIndex = 0;
                dropMoneyType.SelectedIndex = 0;
                txtAmount.Text = "";
                txtRmks.Text = "";
                txtUserName2.Text = "";
            }
            else
                ltlAlert.Text = "alert('保存失败!');";

            DataGridBind();
        }
        protected void txtUserNo2_TextChanged(object sender, EventArgs e)
        {

            if (txtWorkDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('日期不能为空!');";
                txtUserNo2.Text = "";
                return;
            }

            string strUserNo = txtUserNo2.Text.Trim();
            string strTemp = Session["temp"].ToString().Trim();
            string plantid = Session["PlantCode"].ToString().Trim();

        
            if (strUserNo != string.Empty)
            {
                string strUserName = hr.GetUserNameByNo(plantid, strTemp, strUserNo, txtWorkDate.Text.Trim());

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
                    txtUserName2.Text = strUserName;
                    ltlAlert.Text = "Form1.txtNumber.focus();";
                }
            }
        }
        protected void dropType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropType2.SelectedIndex != 0)
            {
                string plantid = Session["PlantCode"].ToString().Trim();
                string typeID = dropType2.SelectedValue;

                dropMoneyType.Items.Clear();

                dropMoneyType.DataSource = hr.GetMoneyType(plantid, typeID);
                dropMoneyType.DataBind();

                dropMoneyType.Items.Insert(0, new ListItem("--","0"));
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string plantid = Session["PlantCode"].ToString();
            int temp = int.Parse(Session["temp"].ToString());
            int status = int.Parse(dropStatus.SelectedValue);
            int role = int.Parse(Session["uRole"].ToString());
            int uID = int.Parse(Session["uID"].ToString());
            int conceal = int.Parse(Session["conceal"].ToString());
            string startdate = txtStdDate.Text.Trim();
            string enddate = txtEndDate.Text.Trim();
            int dept = int.Parse(dropDept.SelectedValue);
            string userno = txtUserNo.Text.Trim();
            string username = txtUserName.Text.Trim();
            int type = int.Parse(dropType.SelectedValue);

            this.ExportExcel(adam.dsn0()
                    , "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>工段</b>~^<b>日期</b>~^<b>备注</b>~^<b>扣款数量</b>~^<b>扣款金额</b>~^<b>工作性质</b>~^<b>输入员</b>~^<b>输入日期</b>~^<b>扣款分类</b>~^<b>扣款事项</b>~^<b></b>~^"
                    , hr.ExportDeduct(plantid, temp, status, role, uID, conceal, startdate, enddate, dept, userno, username, type, Convert.ToDateTime(startdate).Year.ToString() + "-" + Convert.ToDateTime(startdate).Month.ToString() + "-01")
                    , false);
        }
}
}
