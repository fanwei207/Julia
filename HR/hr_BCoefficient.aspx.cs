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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;

namespace Wage
{
    public partial class HR_hr_BCoefficient : BasePage
    {
        adamClass adam = new adamClass();
        HR hr = new HR();

        protected void Page_Load(object sender, EventArgs e)
        {
            ltlAlert.Text = "";
            if (!IsPostBack)
            {
                txtYear.Text = DateTime.Now.Year.ToString();

                try
                {
                    dropMonth.SelectedIndex = -1;

                    dropMonth.Items.FindByText(DateTime.Now.AddMonths(-1).Month.ToString()).Selected = true;
                }
                catch
                {
                    ;
                }

                dropDept.DataSource = hr.GetDept(Session["PlantCode"].ToString().Trim());
                dropDept.DataBind();
                dropDept.Items.Insert(0, new ListItem("--", "0"));

                DataGridBind();
            }
        }

        private void DataGridBind()
        {
            try
            {
                string strSql = "sp_hr_selectBCoef";

                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@year", txtYear.Text.Trim());
                parmArray[1] = new SqlParameter("@month", dropMonth.SelectedValue);
                parmArray[2] = new SqlParameter("@userno", txtSUserNo.Text.Trim());
                parmArray[3] = new SqlParameter("@username", txtSUserName.Text.Trim());
                parmArray[4] = new SqlParameter("@dep", dropDept.SelectedValue);
                parmArray[5] = new SqlParameter("@plantcode", Session["PlantCode"].ToString().Trim());
                parmArray[6] = new SqlParameter("@dflag", chkDepartment.Checked);

                DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                dgBCoef.DataSource = ds;
                dgBCoef.DataBind();


                Label1.Text = "人数：" + ds.Tables[0].Rows.Count;

                ds.Dispose();
            }
            catch
            {
                ;
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtYear.Text.Trim() != String.Empty)
            {
                try
                {
                    DateTime dd = Convert.ToDateTime(txtYear.Text + "-1-1");
                }
                catch
                {
                    ltlAlert.Text = "alert('年月格式不正确!');";
                    return;
                }
            }

            DataGridBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int intFlag = chkDepartment.Checked ? 0 : 1;
            ltlAlert.Text = "var w=window.open('/HR/hr_exportBCoef.aspx?p=" + Session["PlantCode"].ToString().Trim() + "&y=" + txtYear.Text.Trim() + "&m=" + dropMonth.SelectedValue + "&u=" + txtSUserNo.Text.Trim() + "&un=" + txtSUserName.Text.Trim() + "&dp=" + dropDept.SelectedValue + "&rm=" + DateTime.Now.ToString() + "&g=" + intFlag.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //验证
            if (txtYear.Text.Trim() == String.Empty)
            {
                ltlAlert.Text = "alert('年月不能为空!');";
                return;
            }
            else
            {
                try
                {
                    DateTime dd = Convert.ToDateTime(txtYear.Text + "-1-1");
                }
                catch
                {
                    ltlAlert.Text = "alert('年月格式不正确!');";
                    return;
                }
            }

            if (txtUserNo.Text.Trim() == String.Empty)
            {
                ltlAlert.Text = "alert('工号不能为空!');";
                return;
            }

            if (txtCoef.Text.Trim() == String.Empty)
            {
                ltlAlert.Text = "alert('系数不能为空!');";
                return;
            }
            else
            {
                try
                {
                    Double dd = Convert.ToDouble(txtCoef.Text.Trim());

                    if (dd <= 0)
                    {
                        ltlAlert.Text = "alert('系数必须大于0');";
                        return;
                    }
                }
                catch
                {
                    ltlAlert.Text = "alert('系数格式必须为整数或小数');";
                    return;
                }
            }

            //操作
            try
            {
                string strSql = "sp_hr_insertBCoef";

                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@userno", txtUserNo.Text.Trim());
                parmArray[1] = new SqlParameter("@username", txtUserName.Text.Trim());
                parmArray[2] = new SqlParameter("@coef", txtCoef.Text.Trim());
                parmArray[3] = new SqlParameter("@startdate", txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-1");
                parmArray[4] = new SqlParameter("@plantcode", Session["PlantCode"].ToString().Trim());
                parmArray[5] = new SqlParameter("@uID", Session["uID"].ToString().Trim());
                parmArray[6] = new SqlParameter("@dflag", chkDepartment.Checked ? 1 : 0);

                int nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                txtSUserNo.Text = txtUserNo.Text;
                txtSUserName.Text = txtUserName.Text;

                txtUserNo.Text = String.Empty;
                txtUserName.Text = String.Empty;
                txtCoef.Text = String.Empty;
                chkDepartment.Checked = false;

                DataGridBind();
            }
            catch
            {
                ;
            }
        }
        protected void dgBCoef_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgBCoef.CurrentPageIndex = e.NewPageIndex;

            DataGridBind();
        }
        protected void dgBCoef_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string strSql = "sp_hr_deleteBCoef";

                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@bc_id", Convert.ToInt32(dgBCoef.Items[e.Item.ItemIndex].Cells[14].Text));
                parmArray[1] = new SqlParameter("@plantCode", Convert.ToInt32(Session["plantCode"]));

                int nRet = Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));

                if (nRet < 0)
                {
                    ltlAlert.Text = "alert('删除失败!');";
                    DataGridBind();
                    return;
                }
                else
                {
                    DataGridBind();
                }
            }
            catch
            {
                ;
            }
        }
        protected void txtUserNo_TextChanged(object sender, EventArgs e)
        {
            string strDate = DateTime.Now.Date.ToShortDateString();
            string strUserNo = txtUserNo.Text.Trim();
            string strTemp = Session["temp"].ToString().Trim();
            string plantid = Session["PlantCode"].ToString().Trim();

            if (strUserNo != string.Empty)
            {
                string strUserName = hr.GetUserNameByNo(plantid, strTemp, strUserNo, strDate);

                if (strUserName == "DB-Opt-Err")
                {
                    txtUserNo.Text = String.Empty;
                    txtUserName.Text = String.Empty;
                    ltlAlert.Text = "alert('数据库操作错误!');";
                }
                else if (strUserName == "UserHadLeaved")
                {
                    txtUserNo.Text = String.Empty;
                    txtUserName.Text = String.Empty;
                    ltlAlert.Text = "alert('此员工已经离职!');";
                }
                else if (strUserName == "Leaved-User")
                {
                    txtUserNo.Text = String.Empty;
                    txtUserName.Text = String.Empty;
                    ltlAlert.Text = "alert('此员工属于离职员工!');";
                }
                else if (strUserName == "NoThisUser")
                {
                    txtUserNo.Text = String.Empty;
                    txtUserName.Text = String.Empty;
                    ltlAlert.Text = "alert('此员工不存在!');";
                }
                else
                {
                    txtUserName.Text = strUserName;
                    ltlAlert.Text = "Form1.txtCoef.focus();";
                }
            }
        }
        protected void dgBCoef_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "MoreLine")
            {
                LinkButton linkPlan = (LinkButton)e.CommandSource;
                int index = ((DataGridItem)linkPlan.Parent.Parent).ItemIndex;

                string userNo = dgBCoef.Items[index].Cells[0].Text.Trim();
                string userName = ((LinkButton)dgBCoef.Items[index].Cells[1].FindControl("linkMore")).Text.Replace("<u>", "").Replace("</u>", "").Trim();
                string dept = dgBCoef.Items[index].Cells[2].Text.Trim();
                string line = dgBCoef.Items[index].Cells[3].Text.Trim();
                string coef = dgBCoef.Items[index].Cells[8].Text.Trim();

                //Response.Redirect("hr_BCoefMore.aspx?userNo=" + userNo + "&userName=" + Server.HtmlEncode(userName) + "&dept=" + Server.HtmlEncode(dept) + "&rt=" + DateTime.Now.ToString());
                ltlAlert.Text = "window.showModalDialog('hr_BCoefMore.aspx?userNo=" + userNo + "&userName=" + Server.UrlEncode(userName) + "&dept=" + Server.UrlEncode(dept) + "&line=" + Server.UrlEncode(line) + "&coef=" + coef + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 750px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
            }
        }
    }
}