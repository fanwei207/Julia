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
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Wage
{
    public partial class hr_restleave : BasePage
    {
        adamClass adam = new adamClass();
        adamClass chk = new adamClass();
        HR hr = new HR();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //拥有删除特权
                chkDel.Checked = this.Security["6030201"].isValid;

                string strPlantID = Session["PlantCode"].ToString().Trim();
                //txtWorkDate.Text = DateTime.Now.ToShortDateString();
                txtYear.Text = DateTime.Now.Year.ToString();
                dropMonth.SelectedValue = DateTime.Now.Month.ToString();
                dropDept.DataSource = hr.GetDept(strPlantID);
                dropDept.DataBind();
                dropDept.Items.Insert(0, new ListItem("--", "0"));

                DataGridBind();
            }
        }

        protected void DataGridBind()
        {
            string strPlantId = Session["PlantCode"].ToString().Trim();
            string strWorkDate = txtWorkDate.Text.Trim() == string.Empty ? string.Empty : String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtWorkDate.Text.Trim()));
            string strTemp = Session["temp"].ToString().Trim();
            string strYear = txtYear.Text.Trim();
            string strMonth = dropMonth.SelectedValue.Trim();
            string strUserNo = txtUserNo1.Text.Trim();
            string strUserName = txtUserName1.Text.Trim();
            string strDept = dropDept.SelectedValue;

            dgRest.DataSource = hr.GetRestLeave(strPlantId, strWorkDate, strTemp, strYear, strMonth, strUserNo, strUserName, strDept);
            dgRest.DataBind();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtYear.Text.Trim() != string.Empty)
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(txtYear.Text.Trim() + "-1-1");
                }
                catch
                {
                    ltlAlert.Text = "alert('年月的格式不正确');Form1.txtYear.focus();";
                    return;
                }
            }

            DataGridBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            dgRest.CurrentPageIndex = 0;

            if (lblUserID.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('条件不符合，请重新输入');Form1.txtUserNo.focus();";

                lblEnterDate.Text = string.Empty;
                txtUserNo.Text = string.Empty;
                txtNumber.Text = string.Empty;

                return;
            }

            if (txtWorkDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('日期不能为空!');Form1.txtWorkDate.focus();";
                return;
            }
            else
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(txtWorkDate.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('日期格式不正确!');Form1.txtWorkDate.focus();";
                    return;
                }
            }

            if (txtNumber.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('天数不能为空!');Form1.txtWorkDate.focus();";
                return;
            }
            else
            {
                try
                {
                    decimal dd = Convert.ToDecimal(txtNumber.Text.Trim());

                    if (dd <= 0)
                    {
                        ltlAlert.Text = "alert('申请的年假天数必须为非负数!');Form1.txtNumber.focus();";
                        return;
                    }
                    else if (dd > 1)
                    {
                        ltlAlert.Text = "alert('当天申请的年假天数不能超过1!');Form1.txtNumber.focus();";
                        return;
                    }
                }
                catch
                {
                    ltlAlert.Text = "alert('天数格式不对!');Form1.txtNumber.focus();";
                    return;
                }
            }

            //Get the judgedate to user enterdate from everyone | XXXX-3-1 is limited key| */
            DateTime dtEnterDate = Convert.ToDateTime(lblEnterDate.Text.Trim());
            // DateTime dtJudgeDate = Convert.ToDateTime(dtEnterDate.Year.ToString() + "-3-1");
            DateTime dtCurentDate = DateTime.Now;

            decimal nYear = 0;
            string strPlantId = Session["PlantCode"].ToString().Trim();
            string strUID = lblUserID.Text.Trim();
            DateTime dtWorkDate = Convert.ToDateTime(txtWorkDate.Text.Trim());

            //if (dtWorkDate.Month <= 2) //  Before March 1 in next year
            nYear = dtWorkDate.Year;
            //else
            //    nYear = dtWorkDate.Year + 1;

            DateTime dtStdDate = Convert.ToDateTime(nYear.ToString() + "-1-1");
            DateTime dtEndDate = Convert.ToDateTime(nYear.ToString() + "-12-31");

            decimal nCurentDays = hr.GetRestLeaveDays(strPlantId, strUID, dtStdDate, dtEndDate);

            decimal nRestDays = 0;
            decimal nExtralDays = decimal.Parse(txtNumber.Text.Trim());

            //Leave days for everyday


            nRestDays = hr.Restday(dtEnterDate, Convert.ToDateTime(txtWorkDate.Text));

            if (nRestDays == 0)
            {
                ltlAlert.Text = "alert('该员工未享有年假!');Form1.txtUserNo.focus();";
                return;
            }

            int intLeave;

            intLeave = Leaveday(txtWorkDate.Text, dtStdDate.ToShortDateString(), Convert.ToInt32(strUID), nRestDays);
            if (intLeave < 0)
            {
                ltlAlert.Text = "alert('数据库出错，请重新操作!');Form1.txtUserNo.focus();";
                return;
            }
            else
            {
                if (intLeave == 1)
                {
                    decimal nLastDays = hr.GetRestLeaveDays(strPlantId, strUID, dtStdDate.AddYears(-1), dtEndDate.AddYears(-1));
                    nCurentDays += nLastDays;
                    if (nCurentDays > nRestDays)
                    {
                        ltlAlert.Text = "alert('该员工本年度享有年假超过上限！\\n 请删除其在本年度超过上限" + (nCurentDays - nRestDays).ToString() + "天的年假申请！');Form1.txtUserNo.focus();";
                        return;
                    }
                    else if (nCurentDays == nRestDays)
                    {
                        ltlAlert.Text = "alert('该员工本年度不能享有年假！\\n 其上年度病假天数超过了规定的天数！\\n 详见帮助文档');Form1.txtUserNo.focus();";
                        return;
                    }
                    //Marked By Shan Zhiming 2013-12-13:不满足年假条件的，本年度不应有年假
                    /*
                    // if the staff havn't rest leave days for last year
                    if (hr.GetRestLeaveDays(strPlantId, strUID, dtStdDate, dtStdDate.AddYears(1)) > 0)
                    {
                        ltlAlert.Text = "alert('该员工因病假超过上限未享有年假!');Form1.txtUserNo.focus();";
                        return;
                    }
                     * */
                }
            }

            //年假之和不能大于规定天数
            if (nCurentDays == nRestDays)
            {
                ltlAlert.Text = "alert('该员工已无剩余的年假!');Form1.txtNumber.focus();";
                return;
            }
            else if (nExtralDays + nCurentDays > nRestDays)
            {
                ltlAlert.Text = "alert('该员工剩余年假天数仅为" + Convert.ToString(nRestDays - nCurentDays) + "天!');Form1.txtNumber.focus();";
                return;
            }

            int nRet = hr.SaveRestLeave(strPlantId, strUID, dtWorkDate, nExtralDays, nYear, nRestDays, int.Parse(Session["uID"].ToString()));

            if (nRet == 0)
            {
                ltlAlert.Text = "alert('保存失败!');Form1.txtUserNo.focus();";
            }
            else if (nRet == 1)
            {
                ltlAlert.Text = "alert('保存成功!');Form1.txtUserNo.focus();";
            }
            else
            {
                ltlAlert.Text = "alert('记录已经存在!');Form1.txtUserNo.focus();";
            }

            txtUserNo.Text = string.Empty;
            lblUserName.Text = string.Empty;
            lblUserID.Text = string.Empty;
            lblEnterDate.Text = string.Empty;
            txtNumber.Text = string.Empty;

            DataGridBind();
        }
        protected void dgRest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgRest.CurrentPageIndex = e.NewPageIndex;

            DataGridBind();
        }
        protected void dgRest_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex != -1)
                {
                    int id = e.Item.ItemIndex + 1;
                    id = dgRest.CurrentPageIndex * 16 + id;
                    e.Item.Cells[0].Text = id.ToString();

                    if (Convert.ToInt32(Session["uID"].ToString()) != Convert.ToInt32(e.Item.Cells[11].Text.Trim()))
                    {
                        //如果不是本人创建，那么他如果拥有超级删除权限，也是可以删除的
                        if (!chkDel.Checked)
                        {
                            //否则就禁用掉删除按钮
                            ((LinkButton)e.Item.Cells[10].Controls[0]).Enabled = false;
                            ((LinkButton)e.Item.Cells[10].Controls[0]).Style.Add("font-size", "11px");
                            ((LinkButton)e.Item.Cells[10].Controls[0]).Style.Add("font-weight", "normal");
                        }
                    }
                }
            }
        }
        protected void dgRest_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            string strId = dgRest.DataKeys[e.Item.ItemIndex].ToString();
            string strPlantId = Session["PlantCode"].ToString().Trim();

            bool bRet = hr.DeleteRestLeave(strId, strPlantId);

            if (bRet)
                ltlAlert.Text = "alert('删除成功!');";
            else
                ltlAlert.Text = "alert('删除失败!');";

            DataGridBind();
        }
        protected void txtUserNo_TextChanged(object sender, EventArgs e)
        {

            if (txtWorkDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('日期不能为空!');Form1.txtWorkDate.focus();";
                txtUserNo.Text = "";
                return;
            }

            string strUserNo = txtUserNo.Text.Trim();
            string strTemp = Session["temp"].ToString().Trim();
            string plantid = Session["PlantCode"].ToString().Trim();

            if (strUserNo != string.Empty)
            {
                string strUserName = hr.GetUserNameByNo(plantid, strTemp, strUserNo, txtWorkDate.Text.Trim()).Trim();

                if (strUserName == "DB-Opt-Err")
                {
                    lblUserName.Text = string.Empty;
                    ltlAlert.Text = "alert('数据库操作错误!');Form1.txtUserNo.focus();";
                }
                else if (strUserName == "UserHadLeaved")
                {
                    lblUserName.Text = string.Empty;
                    ltlAlert.Text = "alert('此员工已经离职!');Form1.txtUserNo.focus();";
                }
                else if (strUserName == "Leaved-User")
                {
                    lblUserName.Text = string.Empty;
                    ltlAlert.Text = "alert('此员工属于离职员工!');Form1.txtUserNo.focus();";
                }
                else if (strUserName == "NoThisUser")
                {
                    lblUserName.Text = string.Empty;
                    ltlAlert.Text = "alert('此员工不存在!');Form1.txtUserNo.focus();";
                }
                else
                {
                    lblUserName.Text = strUserName;
                    lblUserID.Text = hr.GetUserIDByNo(plantid, strTemp, strUserNo);
                    lblEnterDate.Text = hr.GetUserEnterDateByNo(plantid, strTemp, strUserNo);
                    ltlAlert.Text = "Form1.txtNumber.focus();";
                }
            }
        }
        protected void txtUserNo1_TextChanged(object sender, EventArgs e)
        {

        }
        protected void export_Click(object sender, EventArgs e)
        {
            ltlAlert.Text = "window.open('/salary/RestLeaveExcel.aspx?ye=" + txtYear.Text.Trim() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') ";
        }
        /// <summary>
        /// 加入病假信息，以判定该员工是否应享有年假。
        /// </summary>
        /// <param name="strEnd">年假当日</param>
        /// <param name="strStart">年假所在年的首日，即2013-1-1</param>
        /// <param name="intUserID"></param>
        /// <param name="decdays">经计算后得出的该员工本年度应享受年假总天数</param>
        /// <returns></returns>
        private int Leaveday(string strEnd, string strStart, int intUserID, decimal decdays)
        {
            int intflag = 0;
            int intCompare;
            decimal decleaveUp //年假日上年度年假总天数
                    , decleavedown;//年假日本年度年假总天数

            if (decdays <= 5)
                intCompare = 60;
            else
            {
                if (decdays <= 10)
                    intCompare = 90;
                else
                {
                    intCompare = 120;
                }
            }

            decleaveUp = hr.Leavedays(Convert.ToDateTime(strStart).AddYears(-1).ToShortDateString(), Convert.ToDateTime(strStart).AddDays(-1).ToShortDateString(), Convert.ToInt32(Session["Plantcode"]), intUserID);
            if (decleaveUp < 0)
                intflag = -1;
            else
            {
                if (decleaveUp > intCompare)
                    intflag = 1;
                else
                {
                    intflag = 0;
                    //Marked By Shanzm 2013-12-13:本年度的病假，不影响本年度的年假天数
                    /*
                    decleavedown = hr.Leavedays(strStart, strEnd, Convert.ToInt32(Session["Plantcode"]), intUserID);
                    if (decleavedown < 0)
                        intflag = -1;
                    else
                    {
                        if (decleavedown > intCompare)
                            intflag = 1;
                    }
                     * */
                }
            }

            return intflag;
        }
        protected void exportByMonth_Click(object sender, EventArgs e)
        {
            string strPlantId = Session["PlantCode"].ToString().Trim();
            string strWorkDate = txtWorkDate.Text.Trim() == string.Empty ? string.Empty : String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtWorkDate.Text.Trim()));
            string strTemp = Session["temp"].ToString().Trim();
            string strYear = txtYear.Text.Trim();
            string strMonth = dropMonth.SelectedValue.Trim();
            string strUserNo = txtUserNo1.Text.Trim();
            string strUserName = txtUserName1.Text.Trim();
            string strDept = dropDept.SelectedValue;

            DataTable ds = hr.GetRestLeaves(strPlantId, strWorkDate, strTemp, strYear, strMonth, strUserNo, strUserName, strDept);

            string EXTitle;
            EXTitle = "100^<b>部门</b>~^100^<b>工号</b>~^100^<b>姓名</b>~^120^<b>日期</b>~^60^<b>天数</b>~^80^<b>入职日期</b>~^90^<b>离职日期</b>~^90^<b>录入员</b>~^100^<b>录入日期</b>~^";
            this.ExportExcel(EXTitle, ds, false);
        }
        protected void btn_import_Click(object sender, EventArgs e)
        {
            if (Session["uID"] == null)
            {
                return;
            }
            if (!hr.DeleteRestLeaveTemp(Session["uID"].ToString(), Session["plantcode"].ToString()))
            {
                ltlAlert.Text = "alert('清空临时表数据失败!');";
                return;
            }
            if (ImportExcelFile())
            {
                int total = hr.RestLeaveTempTotal(Convert.ToInt32(Session["uID"].ToString()), Session["plantcode"].ToString());
                if (total == 0)
                {
                    ltlAlert.Text = "alert('存在重复申请记录!');";
                }
                if (hr.CheckRestLeaveTempErr(Session["uID"].ToString()))
                {
                    DataTable dt = hr.RestLeaveTempCheckTotal(int.Parse(Session["uID"].ToString()), Session["plantcode"].ToString());
                    string error = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        decimal nYear = 0;
                        string workdate = Convert.ToString(dt.Rows[i]["workdate"]);
                        string strUID = Convert.ToString(dt.Rows[i]["usercode"]);
                        string strnumber = Convert.ToString(dt.Rows[i]["number"]);
                        DateTime dtWorkDate = Convert.ToDateTime(workdate);
                        nYear = dtWorkDate.Year;
                        DateTime dtStdDate = Convert.ToDateTime(nYear.ToString() + "-1-1");
                        DateTime dtEndDate = Convert.ToDateTime(nYear.ToString() + "-12-31");
                        DateTime dtEnterDate = DateTime.Now;
                        decimal nCurentDays = hr.GetRestLeaveDays(Session["plantcode"].ToString(), strUID, dtStdDate, dtEndDate);
                        decimal nRestDays = 0;
                        decimal nExtralDays = 0;
                        string strTemp = Session["temp"].ToString().Trim();
                        dtEnterDate = Convert.ToDateTime(hr.GetUserEnterDateByNo(Session["plantcode"].ToString(), strTemp, dt.Rows[i]["loginname"].ToString().Trim()));
                        nRestDays = hr.Restday(dtEnterDate, Convert.ToDateTime(workdate));
                        nRestDays = hr.Restday(dtEnterDate, Convert.ToDateTime(workdate));
                        if (nRestDays == 0)
                        {
                            error += "该员工未享有年假；";
                        }
                        int intLeave = 0;
                        if (!string.IsNullOrEmpty(strUID) && strUID != "0")
                        {
                            intLeave = Leaveday(workdate, dtStdDate.ToShortDateString(), Convert.ToInt32(strUID), nRestDays);
                        }
                        if (intLeave < 0)
                        {
                            error += "数据库出错，请重新操作；";
                        }
                        else
                        {
                            if (intLeave == 1)
                            {
                                decimal nLastDays = hr.GetRestLeaveDays(Session["plantcode"].ToString().Trim(), strUID, dtStdDate.AddYears(-1), dtEndDate.AddYears(-1));
                                nCurentDays += nLastDays;
                                if (nCurentDays > nRestDays)
                                {
                                    error += "该员工本年度享有年假超过上限！\\n 请删除其在本年度超过上限；";
                                }
                                else if (nCurentDays == nRestDays)
                                {
                                    error += "该员工本年度不能享有年假！\\n 其上年度病假天数超过了规定的天数！\\n 详见帮助文档；";
                                }
                            }
                        }
                        nExtralDays = Convert.ToDecimal(dt.Rows[i]["number"].ToString().Trim());
                        //年假之和不能大于规定天数
                        if (nCurentDays == nRestDays)
                        {
                            error += "该员工已无剩余的年假；";
                        }
                        else if (nExtralDays + nCurentDays > nRestDays)
                        {
                            error += "该员工剩余年假天数仅为" + Convert.ToString(nRestDays - nCurentDays) + "天" + ";此次申请" + nExtralDays;
                        }
                        if (!string.IsNullOrEmpty(strUID) && strUID != "0")
                        {
                            if (!hr.CheckRestLeaveIsExists(Convert.ToInt32(strUID), workdate, Convert.ToInt32(Session["Uid"].ToString().Trim()), Session["plantcode"].ToString().Trim()))
                            {
                                error += "记录已经存在；";
                            }
                        }
                        if (error != "")
                        {
                            error = dt.Rows[i]["loginname"].ToString().Trim() + error;
                            ltlAlert.Text = "alert('" + error + "!');";
                            return;
                        }
                    }
                    int nRet = hr.SaveRestLeaveTemp(int.Parse(Session["uID"].ToString()), Session["plantcode"].ToString());
                    if (nRet == 0)
                    {
                        ltlAlert.Text = "alert('保存失败!');";
                    }
                    else if (nRet == 1)
                    {
                        ltlAlert.Text = "alert('保存成功!');";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('记录已经存在!');";
                    }
                }
                else
                {
                    string title = "100^<b>工号</b>~^100^<b>日期</b>~^100^<b>天数</b>~^100^<b>错误信息</b>~^";
                    string sql = " 	SELECT loginname as usercode, workdate, number,errMsg	from tcpc0..hr_RestLeave_Temp left join users on usercode = userid where createdby =" + Session["uID"].ToString() + "and isnull(errMsg,'''') <> '''' ";
                    DataTable dt = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
                    ltlAlert.Text = "alert('导入失败!');";
                    ExportExcel(title, dt, false);
                }
            }
            else
            {
                ltlAlert.Text = "alert('导入失败!');";
            }
        }
        public Boolean ImportExcelFile()
        {
            DataTable dt;
            String strFileName = "";
            String strCatFolder = "";
            String strUserFileName = "";
            int intLastBackslash = 0;

            #region 上传文档例行处理
            strCatFolder = Server.MapPath("/import");

            if (!Directory.Exists(strCatFolder))
            {
                try
                {
                    Directory.CreateDirectory(strCatFolder);
                }
                catch
                {
                    ltlAlert.Text = "alert('Fail to upload file.');";
                    return false;
                }

            }

            strUserFileName = filename1.PostedFile.FileName;
            intLastBackslash = strUserFileName.LastIndexOf("\\");
            strFileName = strUserFileName.Substring(intLastBackslash + 1);
            if (strFileName.Trim().Length <= 0)
            {
                ltlAlert.Text = "alert('Please select a file.');";
                return false;
            }

            strUserFileName = strFileName;

            strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
            #endregion

            if (filename1.PostedFile != null)
            {
                string error = "";
                if (filename1.PostedFile.ContentLength > 8388608)
                {
                    ltlAlert.Text = "alert('The maximum upload file is 8 MB.');";
                    return false;
                }

                try
                {
                    filename1.PostedFile.SaveAs(strFileName);
                }
                catch
                {
                    ltlAlert.Text = "alert('Failed to upload file.');";
                    return false;
                }

                if (File.Exists(strFileName))
                {   
                    try
                    {
                        dt = this.GetExcelContents(strFileName);
                    }
                    catch (Exception e)
                    {

                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }

                        ltlAlert.Text = "alert('Import file must be in Excel format.');";
                        return false;
                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        /*
                         *  导入的Excel文件必须满足：
                         *      1、至少应该有三列
                        */

                        if (dt.Columns.Count != 3)
                        {
                            dt.Reset();

                            ltlAlert.Text = "alert('The file must have 3 columns！');";
                            return false;
                        }

                        #region Excel的列名必须保持一致
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            if (col == 0 && dt.Columns[col].ColumnName.Trim() != "工号")
                            {
                                dt.Reset();
                                ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 工号！');";
                                return false;
                            }
                            if (col == 1 && dt.Columns[col].ColumnName.Trim() != "日期")
                            {
                                dt.Reset();
                                ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 日期！');";
                                return false;
                            }

                            if (col == 2 && dt.Columns[col].ColumnName.Trim() != "天数")
                            {
                                dt.Reset();
                                ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 天数！');";
                                return false;
                            }
                        }
                        #endregion

                        //转换成模板格式
                        DataTable table = new DataTable("temp");
                        DataColumn column;
                        DataRow row;

                        #region 定义表列
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "usercode";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "workdate";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "number";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "workyear";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "restday";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.Int32");
                        column.ColumnName = "createdby";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "createddate";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "errMsg";
                        table.Columns.Add(column);

                        #endregion

                        int _uID = Convert.ToInt32(Session["uID"]);

                            foreach (DataRow r in dt.Rows)
                            {
                                if (r[0].ToString().Trim() != string.Empty)
                                {
                                    row = table.NewRow();

                                    string strUserNo = r[0].ToString().Trim();
                                    string workdate = r[1].ToString().Trim();
                                    string workday= r[2].ToString().Trim();
                                    string strTemp = Session["temp"].ToString().Trim();
                                    string plantid = Session["PlantCode"].ToString().Trim();
                                    #region 赋值、长度判定
                                    error = "";
                                    if (strUserNo == string.Empty)
                                    {
                                        error += "工号不能为空；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                    }
                                    if (workdate == string.Empty)
                                    {
                                        error += "日期不能为空；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            row["workdate"] = Convert.ToDateTime(workdate);
                                        }
                                        catch
                                        {
                                            error += "日期格式不正确；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                        }
                                    }
                                    if (r[2].ToString().Trim() == string.Empty)
                                    {
                                        error += "天数不能为空；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            decimal dd = Convert.ToDecimal(r[2].ToString().Trim());
                                            if (dd <= 0)
                                            {
                                                error += "申请的年假天数必须为非负数；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                            }
                                            else if (dd > 1)
                                            {
                                                error += "当天申请的年假天数不能超过；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                            }
                                        }
                                        catch
                                        {
                                            error += "天数格式不对；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                        }
                                    }
                                    decimal nYear = 0;
                                    string strPlantId = Session["PlantCode"].ToString().Trim();
                                    string strUID = hr.GetUserIDByNo(plantid, strTemp, strUserNo);
                                    if (string.IsNullOrEmpty(strUID) || strUID == "0")
                                    {
                                        error += "工号不存在；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                        strUserNo = "0";
                                        strUID = "0";
                                    }
                                    //Get the judgedate to user enterdate from everyone | XXXX-3-1 is limited key| */
                                    DateTime dtEnterDate = DateTime.Now;
                                    if (strUserNo != "0")
                                    {
                                        dtEnterDate = Convert.ToDateTime(hr.GetUserEnterDateByNo(plantid, strTemp, strUserNo));
                                    }
                                    // DateTime dtJudgeDate = Convert.ToDateTime(dtEnterDate.Year.ToString() + "-3-1");
                                    DateTime dtCurentDate = DateTime.Now;


                                    DateTime dtWorkDate = Convert.ToDateTime(workdate);
                                    row["usercode"] = strUID;
                                    row["workdate"] = Convert.ToDateTime(workdate);
                                    //if (dtWorkDate.Month <= 2) //  Before March 1 in next year
                                    nYear = dtWorkDate.Year;
                                    //else
                                    //    nYear = dtWorkDate.Year + 1;

                                    DateTime dtStdDate = Convert.ToDateTime(nYear.ToString() + "-1-1");
                                    DateTime dtEndDate = Convert.ToDateTime(nYear.ToString() + "-12-31");

                                    decimal nCurentDays = hr.GetRestLeaveDays(strPlantId, strUID, dtStdDate, dtEndDate);

                                    decimal nRestDays = 0;
                                    decimal nExtralDays = 0;
                                    try
                                    {
                                        nExtralDays = decimal.Parse(workday);
                                    }
                                    catch
                                    {
                                        error += "天数格式不对；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                    }
                                    //Leave days for everyday
                                    nRestDays = hr.Restday(dtEnterDate, Convert.ToDateTime(workdate));
                                    if (nRestDays == 0)
                                    {
                                        error += "该员工未享有年假；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                    }
                                    int intLeave = 0;
                                    if (!string.IsNullOrEmpty(strUID) && strUID != "0")
                                    {
                                        intLeave = Leaveday(workdate, dtStdDate.ToShortDateString(), Convert.ToInt32(strUID), nRestDays);
                                    }
                                    if (intLeave < 0)
                                    {
                                        error += "数据库出错，请重新操作；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                    }
                                    else
                                    {
                                        if (intLeave == 1)
                                        {
                                            decimal nLastDays = hr.GetRestLeaveDays(strPlantId, strUID, dtStdDate.AddYears(-1), dtEndDate.AddYears(-1));
                                            nCurentDays += nLastDays;
                                            if (nCurentDays > nRestDays)
                                            {
                                                error += "该员工本年度享有年假超过上限！\\n 请删除其在本年度超过上限；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                            }
                                            else if (nCurentDays == nRestDays)
                                            {
                                                error += "该员工本年度不能享有年假！\\n 其上年度病假天数超过了规定的天数！\\n 详见帮助文档；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                            }
                                            //Marked By Shan Zhiming 2013-12-13:不满足年假条件的，本年度不应有年假
                                            /*
                                            // if the staff havn't rest leave days for last year
                                            if (hr.GetRestLeaveDays(strPlantId, strUID, dtStdDate, dtStdDate.AddYears(1)) > 0)
                                            {
                                                ltlAlert.Text = "alert('该员工因病假超过上限未享有年假!');Form1.txtUserNo.focus();";
                                                return;
                                            }
                                             * */
                                        }
                                    }

                                    //年假之和不能大于规定天数
                                    if (nCurentDays == nRestDays)
                                    {
                                        error += "该员工已无剩余的年假；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                    }
                                    else if (nExtralDays + nCurentDays > nRestDays)
                                    {
                                        error += "该员工剩余年假天数仅为" + Convert.ToString(nRestDays - nCurentDays) + "天" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                    }
                                    if (!string.IsNullOrEmpty(strUID) && strUID != "0")
                                    {
                                        if (!hr.CheckRestLeaveIsExists(Convert.ToInt32(strUID), workdate, _uID, strPlantId))
                                        {
                                            error += "记录已经存在；" + "行" + Convert.ToString(dt.Rows.IndexOf(r) + 2);
                                        }
                                    }

                                    row["number"] = nExtralDays;
                                    row["workyear"] = nYear;
                                    row["restday"] = nRestDays;
                                    #endregion

                                    row["createdby"] = _uID;
                                    row["createddate"] = Convert.ToString(System.DateTime.Now);
                                    if (error == "")
                                    {
                                        row["errMsg"] = string.Empty;
                                    }
                                    else
                                    {
                                        row["errMsg"] = error;
                                    }
                                    table.Rows.Add(row);
                                }
                            }

                            //table有数据的情况下
                            if (table != null && table.Rows.Count > 0)
                            {
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                                {
                                    bulkCopy.DestinationTableName = "dbo.hr_RestLeave_Temp";
                                    bulkCopy.ColumnMappings.Add("usercode", "usercode");
                                    bulkCopy.ColumnMappings.Add("workdate", "workdate");
                                    bulkCopy.ColumnMappings.Add("number", "number");
                                    bulkCopy.ColumnMappings.Add("workyear", "workyear");
                                    bulkCopy.ColumnMappings.Add("restday", "restday");
                                    bulkCopy.ColumnMappings.Add("createdby", "createdby");
                                    //bulkCopy.ColumnMappings.Add("createname", "createname");
                                    bulkCopy.ColumnMappings.Add("createddate", "createddate");
                                    bulkCopy.ColumnMappings.Add("errMsg", "errMsg");
                                    try
                                    {
                                        bulkCopy.WriteToServer(table);
                                    }
                                    catch (Exception ex)
                                    {
                                        ltlAlert.Text = "alert('Operation fails!Please try again!');";
                                        return false;
                                    }
                                    finally
                                    {
                                        table.Dispose();
                                    }
                                }
                            }
                        }
                    dt.Reset();

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
            return true;
        }
    }
}