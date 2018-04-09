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
    public partial class HR_hr_SalaryCCMaintance : BasePage
    {
        adamClass adam = new adamClass();
        HR hr = new HR();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtYear.Text = DateTime.Now.Year.ToString();
                dropMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
                BindCostCenter();
                DataGridBind();
            }
        }

        private void DataGridBind()
        {
            try
            {
                //string strSql = "sp_hr_selectSalaryCC";
                string strSql = "sp_hr_selectSalaryWorkday";

                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@costcenter", dropCostCenter.SelectedValue);
                parmArray[1] = new SqlParameter("@year", txtYear.Text.Trim());
                parmArray[2] = new SqlParameter("@month", dropMonth.SelectedValue);
                parmArray[3] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());

                DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                dgCenter.DataSource = ds;
                dgCenter.DataBind();

                ds.Dispose();
            }
            catch
            {
                ;
            }
        }

        private void BindCostCenter()
        {
            try
            {
                ListItem item;
                item = new ListItem("--", "0");
                dropCostCenter.Items.Add(item);

                DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
                if (dtDropDept.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDropDept.Rows.Count; i++)
                    {
                        item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                        dropCostCenter.Items.Add(item);
                    }
                }
                dropCostCenter.SelectedIndex = 0;
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //验证
            if (dropCostCenter.SelectedItem.Text == "--")
            {
                //ltlAlert.Text = "alert('请选择一项成本中心!');";
                ltlAlert.Text = "alert('请选择部门!');";
                return;
            }

            if (txtAttend.Text.Trim() == String.Empty)
            {
                ltlAlert.Text = "alert('应出勤不能为空!');";
                return;
            }
            else
            {
                try
                {
                    Double dd = Convert.ToDouble(txtAttend.Text);
                }
                catch
                {
                    ltlAlert.Text = "alert('应出勤格式不正确!');";
                    return;
                }
            }

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

            //操作
            try
            {
                string strSql = "sp_hr_insertSalaryWorkday";



                string str = " SELECT COUNT(*) FROM hr_SalaryWorkday Where departmentID =" + dropCostCenter.SelectedValue + " And Year(workdate)=" + txtYear.Text.Trim() + " And Month(workdate)=" + dropMonth.SelectedValue + " And plantcode =" + Convert.ToString(Session["plantcode"]) + " ";
                int nRet = Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, str));

                if (nRet > 0)
                {
                    str = " UPDATE hr_SalaryWorkday SET workdays =" + txtAttend.Text.Trim() + ",creatby=" + Convert.ToString(Session["plantcode"]) + " ,creatdate =getdate()  WHERE departmentID =" + dropCostCenter.SelectedValue + " And Year(workdate)=" + txtYear.Text.Trim() + " And Month(workdate)=" + dropMonth.SelectedValue + " And plantcode =" + Convert.ToString(Session["plantcode"]) + " ";
                }
                else
                {
                    string strDate = txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-1";
                    str = " INSERT INTO hr_SalaryWorkday(departmentID, workdate, workdays, plantcode, creatby, creatdate) VALUES (" + dropCostCenter.SelectedValue + ",'" + strDate + "'," + txtAttend.Text.Trim() + "," + Convert.ToString(Session["plantcode"]) + "," + Convert.ToString(Session["uid"]) + ",getdate())";
                }
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);

                txtAttend.Text = String.Empty;
                DataGridBind();
            }
            catch
            {
                ;
            }
        }

        protected void dgCenter_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgCenter.CurrentPageIndex = e.NewPageIndex;

            DataGridBind();
        }
        protected void dgCenter_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string strSql = "sp_hr_insertSalaryWorkday";

                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@departmentID", dgCenter.Items[e.Item.ItemIndex].Cells[5].Text);
                parmArray[1] = new SqlParameter("@attend", 0);
                parmArray[2] = new SqlParameter("@Year", 0);
                parmArray[3] = new SqlParameter("@Month", 0);
                parmArray[4] = new SqlParameter("@plantcode", Session["PlantCode"].ToString().Trim());
                parmArray[5] = new SqlParameter("@uID", Session["uID"].ToString().Trim());
                parmArray[6] = new SqlParameter("@type", 1);
                int nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                DataGridBind();
                //}
            }
            catch
            {
                ;
            }
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            Session["EXSQL1"] = hr.SalaryCC(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["plantCode"]), 0);
            Session["EXTitle1"] = hr.SalaryCC(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["plantCode"]), 1);
            Session["EXHeader1"] = "";
            ltlAlert.Text = "window.open('/public/exportExcel1.aspx', '_blank');";
        }
    }
}