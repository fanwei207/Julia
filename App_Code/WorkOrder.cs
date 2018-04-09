using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;


namespace WOrder
{
    /// <summary>
    /// Summary description for WorkOrder
    /// </summary>
    public class WorkOrder
    {
        adamClass adam = new adamClass();
        string strSql;
        public WorkOrder()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Public information

        /// <summary>
        /// 根据登录的公司不同取得不同的地点,域名
        /// 
        /// </summary>
        /// <param name="plantid">域单位</param>
        /// <returns></returns>
        //public SqlDataReader GetDomMes(string plantid,string strSite,int intType)
        //{
        //    try
        //    {
        //        string str = "sp_wo_DomMess";
        //        SqlParameter[] sqlParam = new SqlParameter[3];
        //        sqlParam[0] = new SqlParameter("@plantID", plantid);
        //        sqlParam[1] = new SqlParameter("@site", strSite);
        //        sqlParam[2] = new SqlParameter("@Type", intType);
        //        return SqlHelper.ExecuteReader(adam.dsn0,CommandType.StoredProcedure ,str,sqlParam);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public DataTable GetDomainSite(int Plantcode)
        {
            try
            {
                adamClass adc = new adamClass(); 
                string str = "sp_wo2_GetDomainSite";
                SqlParameter parm = new SqlParameter("@Plantcode", Plantcode);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, str, parm).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable SelectGroupInfo(string strMop,int intPlant)
        {
            try
            {
                string str = "sp_wo2_SelectGroupInfo";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@Plantcode", intPlant);
                sqlParam[1] = new SqlParameter("@Mop", strMop);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region WorkOrder Enter
        public WorkOrderInfor  GetWorkOrderInformation(int intPlantcode, string strWorkOrder, string strOrderID,string strSite,string strConnect)
        {
            try
            {
                DataSet dsInfor;
                string str = "sp_wo2_GetWorkOrderInformation";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@plantcode", intPlantcode);
                sqlParam[1] = new SqlParameter("@workorder", strWorkOrder);
                sqlParam[2] = new SqlParameter("@orderID", strOrderID);
                sqlParam[3] = new SqlParameter("@site", strSite);
                dsInfor = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
                WorkOrderInfor woInfor = new WorkOrderInfor ();
                woInfor.Part = "";
                if (dsInfor.Tables[0].Rows.Count  > 0)
                {
                    woInfor.StartDate = dsInfor.Tables[0].Rows[0].ItemArray[0].ToString ();
                    woInfor.EndDate  =dsInfor.Tables[0].Rows[0].ItemArray[1].ToString ();
                    woInfor.Center  =dsInfor.Tables[0].Rows[0].ItemArray[2].ToString ();
                    woInfor .Order = dsInfor.Tables[0].Rows[0].ItemArray[3].ToString ();
                    woInfor.OrderID  =dsInfor.Tables[0].Rows[0].ItemArray[4].ToString ();
                    woInfor.Part  =dsInfor.Tables[0].Rows[0].ItemArray[5].ToString ();
                    woInfor.PartDesc  =dsInfor.Tables[0].Rows[0].ItemArray[6].ToString ();
                    woInfor.Tec =dsInfor.Tables[0].Rows[0].ItemArray[7].ToString ();
                    woInfor .Line  =dsInfor.Tables[0].Rows[0].ItemArray[8].ToString ();
                    woInfor.WorkOrderID = Convert.ToInt32 (dsInfor.Tables[0].Rows[0].ItemArray[9]);
                }
                else
                {
                    OdbcConnection conn  =new OdbcConnection (strConnect);
                    conn.Open();
                    strSql = "SELECT '','',wo_flr_cc ,wo_nbr, wo_lot, wo_part,wo_routing,'',0,wo_domain FROM PUB.wo_mstr WHERE wo_site='" + strSite + "'";
                    strSql = strSql + "  AND wo_nbr='" + strWorkOrder + "' AND wo_lot='" + strOrderID + "' ";
                    OdbcCommand comm = new OdbcCommand(strSql ,conn );
                   
                    OdbcDataReader dr = comm.ExecuteReader();
                    if (dr.Read())
                    {
                        woInfor.StartDate = dr.GetValue(0).ToString ();
                        woInfor.EndDate  =dr.GetValue(1).ToString ();
                        woInfor.Center  =dr.GetValue(2).ToString ();
                        woInfor .Order = dr.GetValue(3).ToString ();
                        woInfor.OrderID  =dr.GetValue(4).ToString ();
                        woInfor.Part  =dr.GetValue(5).ToString ();
                        woInfor.Tec =dr.GetValue(6).ToString ();
                        //woInfor .Line  =dr.GetValue(7).ToString ();
                        woInfor.WorkOrderID = Convert.ToInt32 (dr.GetValue(8));

                        OdbcConnection conn1  =new OdbcConnection (strConnect);
                        conn1.Open();
                        strSql = "SELECT pt_desc1 + pt_desc2 FROM PUB.pt_mstr WHERE pt_domain = '" + dr.GetValue(9).ToString () +"' AND pt_part='" + woInfor.Part +"' ";
                      
                        OdbcCommand comm1 = new OdbcCommand(strSql ,conn1 );
                       
                        OdbcDataReader dr1 = comm1.ExecuteReader();
                        if (dr1.Read())
                        {
                            woInfor.PartDesc =dr1.GetValue(0).ToString ();
                        }
                         dr1.Close();
                         conn1.Close();

                         comm1.Dispose();
                         conn1.Dispose();
                    }
                    dr.Close();
                    conn.Close();

                    comm.Dispose();
                    conn.Dispose();


                }

                return woInfor;
            }
            catch
            {
                return null;
            }
        }
      

        public int AddWorkOrderPeople(int intWorkproc,int intGroupID,string strUserNo,int intPosition,int intplantCode,int intWorkOrderID,int intCreat,string strCreatName,string strLine,decimal decqty,decimal decProper,string strEffdate,string strSite,string strPart,string strDesc)
        {
            try
            {
                string str = "sp_wo2_AddWorkOrderPeople";
                SqlParameter[] sqlParam = new SqlParameter[15];
                sqlParam[0] = new SqlParameter("@Workproc", intWorkproc);
                sqlParam[1] = new SqlParameter("@GroupID", intGroupID);
                sqlParam[2] = new SqlParameter("@UserNo", strUserNo);
                sqlParam[3] = new SqlParameter("@Position", intPosition);
                sqlParam[4] = new SqlParameter("@plantcode", intplantCode);
                sqlParam[5] = new SqlParameter("@WorkOrderID", intWorkOrderID);
                sqlParam[6] = new SqlParameter("@Creat", intCreat);
                sqlParam[7] = new SqlParameter("@CreatName", strCreatName);
                sqlParam[8] = new SqlParameter("@InputLine", strLine);
                sqlParam[9] = new SqlParameter("@qty", decqty);
                sqlParam[10] = new SqlParameter("@proper", decProper);
                sqlParam[11] = new SqlParameter("@effdate", strEffdate);
                sqlParam[12] = new SqlParameter("@site", strSite);
                sqlParam[13] = new SqlParameter("@part", strPart );
                sqlParam[14] = new SqlParameter("@partDesc", strDesc);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
                //str = SqlHelper.ExecuteScalar (adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString ();

                return 0;
            }
            catch
            {
                return -1;
            }
        }



        //public int DistributionWorkOrder(int intplantCode, int intCreat, int intWorkOrderID)
        //{
        //    try
        //    {
        //        string str = "sp_wo2_DistributionWorkOrder ";
        //        SqlParameter[] sqlParam = new SqlParameter[3];
        //        sqlParam[0] = new SqlParameter("@plantcode", intplantCode);
        //        sqlParam[1] = new SqlParameter("@Creat", intCreat);
        //        sqlParam[2] = new SqlParameter("@WorkOrderID", intWorkOrderID);
                
        //    }
        //    catch
        //    {
        //        return -1;
        //    }

        //}

        public int SaveWorkOrderData(int intplantCode, int intCreat, int intWorkOrderID,int intSite)
        {
            try
            {
                string str = "sp_wo2_SaveWorkOrderData";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@plantcode", intplantCode);
                sqlParam[1] = new SqlParameter("@Creat", intCreat);
                sqlParam[2] = new SqlParameter("@WorkOrderID", intWorkOrderID);
                sqlParam[3] = new SqlParameter("@site", intSite);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                str = "sp_wo2_UpdateWorkOrderEnter";
                sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@plantcode", intplantCode);
                sqlParam[1] = new SqlParameter("@WorkOrderID", intWorkOrderID);
                sqlParam[2] = new SqlParameter("@site", intSite);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }


        public void DeleteTmpdate(int intCreat)
        {
            try
            {
                string str = "sp_wo2_DeleteWorkOrderEnter";
                SqlParameter parm = new SqlParameter("@Creat", intCreat);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, parm);
            }
            catch
            {

            }
        }

        public int DelWorkOrderEntertmp(int intOrderID,decimal decProper,int intType)
        {
            try
            {
                string str = "sp_wo2_WorkOrderTmpDel";
                //SqlParameter parm = new SqlParameter("@OrderID", intOrderID);
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@OrderID", intOrderID);
                sqlParam[1] = new SqlParameter("@Proper", decProper);
                sqlParam[2] = new SqlParameter("@Type", intType);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int WorkOrderInforSave(string strSite,string strSdate,string strEdate,string strOrder,string strID,string strLine,string strPart, string strDesc,string strTec ,string strCC,string strUsername,int intUid,string strWorkOrder,int intPlantcode)
        {
            try
            {
                string str = "sp_wo2_WorkOrderInforSave";
                SqlParameter[] sqlParam = new SqlParameter[14];
                sqlParam[0] = new SqlParameter("@site", strSite);
                sqlParam[1] = new SqlParameter("@sdate", strSdate);
                sqlParam[2] = new SqlParameter("@edate", strEdate);
                sqlParam[3] = new SqlParameter("@Order", strOrder);
                sqlParam[4] = new SqlParameter("@OrderID", strID);
                sqlParam[5] = new SqlParameter("@Line", strLine);
                sqlParam[6] = new SqlParameter("@Part", strPart);
                sqlParam[7] = new SqlParameter("@Desc", strDesc);
                sqlParam[8] = new SqlParameter("@Tec", strTec);
                sqlParam[9] = new SqlParameter("@Center", strCC);
                sqlParam[10] = new SqlParameter("@Creat", strUsername);
                sqlParam[11] = new SqlParameter("@Modify",intUid );
                sqlParam[12] = new SqlParameter("@WorkOrderID", Convert.ToInt32 (strWorkOrder));
                sqlParam[13] = new SqlParameter("@Plantcode", intPlantcode);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public DataSet  GetTmpWorkOder(int intWorkOrderID,int intUid)
        {
            try
            {
                 adamClass adc = new adamClass(); 
                 string str ="sp_wo2_GetTmpWorkOder";
                 SqlParameter[] sqlParam = new SqlParameter[2];
                 sqlParam[0] = new SqlParameter("@Creat", intUid);
                 sqlParam[1] = new SqlParameter("@workOrderID", intWorkOrderID);
                 return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, str, sqlParam);

            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region WorkOrderEnter 4

        public int SaveWorkOrderData4(int intplantCode, int intCreat, int intWorkOrderID)
        {
            try
            {
                string str = "sp_wo2_SaveWorkOrderData";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@plantcode", intplantCode);
                sqlParam[1] = new SqlParameter("@Creat", intCreat);
                sqlParam[2] = new SqlParameter("@WorkOrderID", intWorkOrderID);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                str = "sp_wo2_UpdateWorkOrderEnter";
                sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@plantcode", intplantCode);
                sqlParam[1] = new SqlParameter("@WorkOrderID", intWorkOrderID);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public WorkOrderInfor GetPlanWorkOrder(int intPlantcode, string strWorkOrder, string strOrderID, string strSite, string strLine)
        {
            try
            {
                DataSet dsInfor;
                //string str = "sp_wo2_GetWorkOrderPlan";
                string str = "sp_wo2_GetWorkOrderPlan";
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@plantcode", intPlantcode);
                sqlParam[1] = new SqlParameter("@workorder", strWorkOrder);
                sqlParam[2] = new SqlParameter("@orderID", strOrderID);
                sqlParam[3] = new SqlParameter("@site", strSite);
                sqlParam[4] = new SqlParameter("@sLine", strLine);
                dsInfor = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
                WorkOrderInfor woInfor = new WorkOrderInfor();
                woInfor.Part = "";
                if (dsInfor.Tables[0].Rows.Count > 0)
                {
                //    woInfor.StartDate = dsInfor.Tables[0].Rows[0][0].ToString();
                //    woInfor.EndDate = dsInfor.Tables[0].Rows[0][1].ToString();
                //    woInfor.Center = dsInfor.Tables[0].Rows[0][2].ToString();
                //    woInfor.Part = dsInfor.Tables[0].Rows[0][3].ToString();
                //    woInfor.PartDesc = dsInfor.Tables[0].Rows[0][4].ToString();
                //    woInfor.Tec = dsInfor.Tables[0].Rows[0][5].ToString();
                //    woInfor.WorkOrderID = Convert.ToInt32(dsInfor.Tables[0].Rows[0][6]);
                //    woInfor.Order = dsInfor.Tables[0].Rows[0][7].ToString();

                    woInfor.Center = dsInfor.Tables[0].Rows[0][0].ToString();
                    woInfor.Part = dsInfor.Tables[0].Rows[0][1].ToString();
                    woInfor.PartDesc = dsInfor.Tables[0].Rows[0][2].ToString();
                    woInfor.Tec = dsInfor.Tables[0].Rows[0][3].ToString();
                    woInfor.WorkOrderID = Convert.ToInt32(dsInfor.Tables[0].Rows[0][4]);
                    woInfor.Order = dsInfor.Tables[0].Rows[0][5].ToString();
                }



                return woInfor;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 员工工时
        /// </summary>
        /// <param name="strSdate"></param>
        /// <param name="strEdate"></param>
        /// <param name="intPlant"></param>
        /// <param name="strCenter"></param>
        /// <param name="strSite"></param>
        /// <param name="strNbr"></param>
        /// <param name="strOrder"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="blflag"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intRole"></param>
        /// <param name="strUid"></param>
        /// <returns></returns>
        public DataTable WorkHourSelect(string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, string strUserNo, string strUserName, bool blflag, int intDepartment,  int intRole,string strUid)
        {
            try
            {
                adamClass adc = new adamClass();
                WorkOrder wo = new WorkOrder();
                string str = wo.ExportWorkHours(strSdate, strEdate, intPlant, strCenter, strSite, strNbr, strOrder, strUserNo, strUserName, blflag,intDepartment,intRole,strUid, 0);

                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, str).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 员工工时（历史）
        /// </summary>
        /// <param name="strSdate"></param>
        /// <param name="strEdate"></param>
        /// <param name="intPlant"></param>
        /// <param name="strCenter"></param>
        /// <param name="strSite"></param>
        /// <param name="strNbr"></param>
        /// <param name="strOrder"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="blflag"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intRole"></param>
        /// <param name="strUid"></param>
        /// <returns></returns>
        public DataTable WorkHourSelectHist(string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, string strUserNo, string strUserName, bool blflag, int intDepartment, int intRole, string strUid)
        {
            try
            {
                adamClass adc = new adamClass();
                WorkOrder wo = new WorkOrder();
                string str = wo.ExportWorkHoursHist(strSdate, strEdate, intPlant, strCenter, strSite, strNbr, strOrder, strUserNo, strUserName, blflag, intDepartment, intRole, strUid, 0);

                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, str).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 员工工时
        /// </summary>
        /// <param name="strSdate"></param>
        /// <param name="strEdate"></param>
        /// <param name="intPlant"></param>
        /// <param name="strCenter"></param>
        /// <param name="strSite"></param>
        /// <param name="strNbr"></param>
        /// <param name="strOrder"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="blflag"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intRole"></param>
        /// <param name="strUid"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string ExportWorkHours(string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, string strUserNo, string strUserName, bool blflag, int intDepartment, int intRole,string strUid, int intType)
        {
            try
            {
                string str = "sp_hr_SalaryWorkHours";
                SqlParameter[] sqlParam = new SqlParameter[14];
                sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
                sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
                sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[3] = new SqlParameter("@Center", strCenter);
                sqlParam[4] = new SqlParameter("@Site", strSite);
                sqlParam[5] = new SqlParameter("@WorkOrder", strNbr);
                sqlParam[6] = new SqlParameter("@OrderID", strOrder);
                sqlParam[7] = new SqlParameter("@UserNo", strUserNo);
                sqlParam[8] = new SqlParameter("@UserName", strUserName);
                sqlParam[9] = new SqlParameter("@Flag", blflag);
                sqlParam[10] = new SqlParameter("@Department", intDepartment);
                sqlParam[11] = new SqlParameter("@Role", intRole);
                sqlParam[12] = new SqlParameter("@Uid", strUid);
                sqlParam[13] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();

            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 员工工时（历史）
        /// </summary>
        /// <param name="strSdate"></param>
        /// <param name="strEdate"></param>
        /// <param name="intPlant"></param>
        /// <param name="strCenter"></param>
        /// <param name="strSite"></param>
        /// <param name="strNbr"></param>
        /// <param name="strOrder"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="blflag"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intRole"></param>
        /// <param name="strUid"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string ExportWorkHoursHist(string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, string strUserNo, string strUserName, bool blflag, int intDepartment, int intRole, string strUid, int intType)
        {
            try
            {
                string str = "sp_hr_SalaryWorkHoursHist";
                SqlParameter[] sqlParam = new SqlParameter[14];
                sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
                sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
                sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[3] = new SqlParameter("@Center", strCenter);
                sqlParam[4] = new SqlParameter("@Site", strSite);
                sqlParam[5] = new SqlParameter("@WorkOrder", strNbr);
                sqlParam[6] = new SqlParameter("@OrderID", strOrder);
                sqlParam[7] = new SqlParameter("@UserNo", strUserNo);
                sqlParam[8] = new SqlParameter("@UserName", strUserName);
                sqlParam[9] = new SqlParameter("@Flag", blflag);
                sqlParam[10] = new SqlParameter("@Department", intDepartment);
                sqlParam[11] = new SqlParameter("@Role", intRole);
                sqlParam[12] = new SqlParameter("@Uid", strUid);
                sqlParam[13] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();

            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工时导出
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="strSdate"></param>
        /// <param name="strEdate"></param>
        /// <param name="intPlant"></param>
        /// <param name="strCenter"></param>
        /// <param name="strSite"></param>
        /// <param name="strNbr"></param>
        /// <param name="strOrder"></param>
        /// <param name="blflag"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intRole"></param>
        /// <param name="strUid"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string Wo2Export(string strID, string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, bool blflag, int intDepartment, int intRole, string strUid, int intType)
        {
            try
            {
                string str = "sp_hr_Wo2Export";
                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
                sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
                sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[3] = new SqlParameter("@Center", strCenter);
                sqlParam[4] = new SqlParameter("@Site", strSite);
                sqlParam[5] = new SqlParameter("@WorkOrder", strNbr);
                sqlParam[6] = new SqlParameter("@OrderID", strOrder);
                sqlParam[7] = new SqlParameter("@Flag", blflag);
                sqlParam[8] = new SqlParameter("@Department", intDepartment);
                sqlParam[9] = new SqlParameter("@Role", intRole);
                sqlParam[10] = new SqlParameter("@Uid", strUid);
                sqlParam[11] = new SqlParameter("@wid", strID);
                sqlParam[12] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工时导出（历史）
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="strSdate"></param>
        /// <param name="strEdate"></param>
        /// <param name="intPlant"></param>
        /// <param name="strCenter"></param>
        /// <param name="strSite"></param>
        /// <param name="strNbr"></param>
        /// <param name="strOrder"></param>
        /// <param name="blflag"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intRole"></param>
        /// <param name="strUid"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string Wo2ExportHist(string strID, string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, bool blflag, int intDepartment, int intRole, string strUid, int intType)
        {
            try
            {
                string str = "sp_hr_Wo2ExportHist";
                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
                sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
                sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[3] = new SqlParameter("@Center", strCenter);
                sqlParam[4] = new SqlParameter("@Site", strSite);
                sqlParam[5] = new SqlParameter("@WorkOrder", strNbr);
                sqlParam[6] = new SqlParameter("@OrderID", strOrder);
                sqlParam[7] = new SqlParameter("@Flag", blflag);
                sqlParam[8] = new SqlParameter("@Department", intDepartment);
                sqlParam[9] = new SqlParameter("@Role", intRole);
                sqlParam[10] = new SqlParameter("@Uid", strUid);
                sqlParam[11] = new SqlParameter("@wid", strID);
                sqlParam[12] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public string Wo2ExportALL(string strID, string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, bool blflag, int intDepartment, int intRole, string strUid, int intType)
        {
            try
            {
                string str = "sp_hr_Wo2ExportALL";
                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
                sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
                sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[3] = new SqlParameter("@Center", strCenter);
                sqlParam[4] = new SqlParameter("@Site", strSite);
                sqlParam[5] = new SqlParameter("@WorkOrder", strNbr);
                sqlParam[6] = new SqlParameter("@OrderID", strOrder);
                sqlParam[7] = new SqlParameter("@Flag", blflag);
                sqlParam[8] = new SqlParameter("@Department", intDepartment);
                sqlParam[9] = new SqlParameter("@Role", intRole);
                sqlParam[10] = new SqlParameter("@Uid", strUid);
                sqlParam[11] = new SqlParameter("@wid", strID);
                sqlParam[12] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工时（历史）
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="strSdate"></param>
        /// <param name="strEdate"></param>
        /// <param name="intPlant"></param>
        /// <param name="strCenter"></param>
        /// <param name="strSite"></param>
        /// <param name="strNbr"></param>
        /// <param name="strOrder"></param>
        /// <param name="blflag"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intRole"></param>
        /// <param name="strUid"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string Wo2ExportALLHist(string strID, string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, bool blflag, int intDepartment, int intRole, string strUid, int intType)
        {
            try
            {
                string str = "sp_hr_Wo2ExportALLHist";
                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
                sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
                sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[3] = new SqlParameter("@Center", strCenter);
                sqlParam[4] = new SqlParameter("@Site", strSite);
                sqlParam[5] = new SqlParameter("@WorkOrder", strNbr);
                sqlParam[6] = new SqlParameter("@OrderID", strOrder);
                sqlParam[7] = new SqlParameter("@Flag", blflag);
                sqlParam[8] = new SqlParameter("@Department", intDepartment);
                sqlParam[9] = new SqlParameter("@Role", intRole);
                sqlParam[10] = new SqlParameter("@Uid", strUid);
                sqlParam[11] = new SqlParameter("@wid", strID);
                sqlParam[12] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public string WoExport(string strID, string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, bool blflag, int intDepartment, int intRole, string strUid, int intType)
        {
            try
            {
                string str = "sp_hr_WoExport";
                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
                sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
                sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[3] = new SqlParameter("@Center", strCenter);
                sqlParam[4] = new SqlParameter("@Site", strSite);
                sqlParam[5] = new SqlParameter("@WorkOrder", strNbr);
                sqlParam[6] = new SqlParameter("@OrderID", strOrder);
                sqlParam[7] = new SqlParameter("@Flag", blflag);
                sqlParam[8] = new SqlParameter("@Department", intDepartment);
                sqlParam[9] = new SqlParameter("@Role", intRole);
                sqlParam[10] = new SqlParameter("@Uid", strUid);
                sqlParam[11] = new SqlParameter("@wid", strID);
                sqlParam[12] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 员工工时（历史）
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="strSdate"></param>
        /// <param name="strEdate"></param>
        /// <param name="intPlant"></param>
        /// <param name="strCenter"></param>
        /// <param name="strSite"></param>
        /// <param name="strNbr"></param>
        /// <param name="strOrder"></param>
        /// <param name="blflag"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intRole"></param>
        /// <param name="strUid"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string WoExportHist(string strID, string strSdate, string strEdate, int intPlant, string strCenter, string strSite, string strNbr, string strOrder, bool blflag, int intDepartment, int intRole, string strUid, int intType)
        {
            try
            {
                string str = "sp_hr_WoExportHist";
                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
                sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
                sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[3] = new SqlParameter("@Center", strCenter);
                sqlParam[4] = new SqlParameter("@Site", strSite);
                sqlParam[5] = new SqlParameter("@WorkOrder", strNbr);
                sqlParam[6] = new SqlParameter("@OrderID", strOrder);
                sqlParam[7] = new SqlParameter("@Flag", blflag);
                sqlParam[8] = new SqlParameter("@Department", intDepartment);
                sqlParam[9] = new SqlParameter("@Role", intRole);
                sqlParam[10] = new SqlParameter("@Uid", strUid);
                sqlParam[11] = new SqlParameter("@wid", strID);
                sqlParam[12] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }


        #endregion

        #region Work Order Adjust

        public DataSet GetWorkOrder(int intWorkOrder, string strMop, string strSop, string UserNo, int intPlantCode, int intCreat,string strLine)
        {
            try
            {

                string str = "sp_wo2_GetWorkOrderEnterDisplay";
                SqlParameter parm = new SqlParameter("@Display", GetWorkOrderStr(intWorkOrder, strMop, strSop, UserNo, intPlantCode,intCreat,strLine ));
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, parm);

            }
            catch
            {
                return null;
            }
        }

        public string GetWorkOrderStr(int intWorkOrder, string strMop, string strSop, string UserNo, int intPlantCode, int intCreat, string strLine)
        {
            try
            {
                string str = "sp_wo2_GetWorkOrderEnter";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@WorkOrderID", intWorkOrder);
                sqlParam[1] = new SqlParameter("@Mop", strMop);
                sqlParam[2] = new SqlParameter("@Sop", strSop);
                sqlParam[3] = new SqlParameter("@UserNo", UserNo);
                sqlParam[4] = new SqlParameter("@PlantCode", intPlantCode);
                sqlParam[5] = new SqlParameter("@Creat", intCreat);
                sqlParam[6] = new SqlParameter("@Line", strLine);
                return Convert.ToString (SqlHelper.ExecuteScalar (adam.dsn0(), CommandType.StoredProcedure, str, sqlParam));

            }
            catch
            {
                return "";
            }
        }

        public int DelWorkOrderEnter(int intOrderID, decimal decProper, int intType,string strMop,string strSop,string strLine,int intPlantCode,int intCreat,int intWid,string strDedate)
        {
            try
            {
                string str = "sp_wo2_DelWorkOrder";
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@WorkOrderID", intOrderID);
                sqlParam[1] = new SqlParameter("@Mop", strMop);
                sqlParam[2] = new SqlParameter("@Sop", strSop);
                sqlParam[3] = new SqlParameter("@Proper", decProper);
                sqlParam[4] = new SqlParameter("@PlantCode", intPlantCode);
                sqlParam[5] = new SqlParameter("@Creat", intCreat);
                sqlParam[6] = new SqlParameter("@sLine", strLine);
                sqlParam[7] = new SqlParameter("@Type", intType);
                sqlParam[8] = new SqlParameter("@wid", intWid);
                sqlParam[9] = new SqlParameter("@dedate", strDedate);

                //string dd = Convert.ToString (SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam));
               SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }


        public DataSet GetWorkOrderNew(int intWorkOrder, string strMop, string strSop, string UserNo, int intPlantCode, int intCreat, string strLine,string strSite)
        {
            try
            {

                string str = "sp_wo2_GetWorkOrderEnterDisplayNew";
                SqlParameter parm = new SqlParameter("@Display", GetWorkOrderStrNew(intWorkOrder, strMop, strSop, UserNo, intPlantCode, intCreat, strLine,strSite));
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, parm);

            }
            catch
            {
                return null;
            }
        }

        public string GetWorkOrderStrNew(int intWorkOrder, string strMop, string strSop, string UserNo, int intPlantCode, int intCreat, string strLine, string strSite)
        {
            try
            {
                string str = "sp_wo2_GetWorkOrderEnterNew";
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@WorkOrderID", intWorkOrder);
                sqlParam[1] = new SqlParameter("@Mop", strMop);
                sqlParam[2] = new SqlParameter("@Sop", strSop);
                sqlParam[3] = new SqlParameter("@UserNo", UserNo);
                sqlParam[4] = new SqlParameter("@PlantCode", intPlantCode);
                sqlParam[5] = new SqlParameter("@Creat", intCreat);
                sqlParam[6] = new SqlParameter("@Line", strLine);
                sqlParam[7] = new SqlParameter("@Site", strSite);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam));

            }
            catch
            {
                return "";
            }
        }

        public int DelWorkOrderEnterNew(int intOrderID, decimal decProper, int intType, string strMop, string strSop, string strLine, int intPlantCode, int intCreat, int intWid, string strDedate,string strSite)
        {
            try
            {
                string str = "sp_wo2_DelWorkOrderNew";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@WorkOrderID", intOrderID);
                sqlParam[1] = new SqlParameter("@Mop", strMop);
                sqlParam[2] = new SqlParameter("@Sop", strSop);
                sqlParam[3] = new SqlParameter("@Proper", decProper);
                sqlParam[4] = new SqlParameter("@PlantCode", intPlantCode);
                sqlParam[5] = new SqlParameter("@Creat", intCreat);
                sqlParam[6] = new SqlParameter("@sLine", strLine);
                sqlParam[7] = new SqlParameter("@Type", intType);
                sqlParam[8] = new SqlParameter("@wid", intWid);
                sqlParam[9] = new SqlParameter("@dedate", strDedate);
                sqlParam[10] = new SqlParameter("@site", strSite);
                //string dd = Convert.ToString (SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam));
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region  Workproc Maintence

        public string GetWorkprocData(int intUid, string strTec, string strPName, string strGl, int intPlantCode)
        {
            try
            {
                strSql = "sp_WO_WorkprocedureSelect";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@creat", intUid);
                parmArray[1] = new SqlParameter("@woTec", strTec);
                parmArray[2] = new SqlParameter("@woProName", strPName);
                parmArray[3] = new SqlParameter("@woGl", strGl);
                parmArray[4] = new SqlParameter("@plantcode", intPlantCode);
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString();
            }
            catch
            {
                return "";
            }
        }

        public bool UpdateAndDeleteWorkProc(string strGl, int intID, int intPlantCode,int intkind)
        {
            try
            {
                strSql = "sp_WO_UpAndDelWorkProc";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@woGl", strGl.Length > 0 ? Convert.ToDecimal(strGl) : 0);
                parmArray[1] = new SqlParameter("@woID", intID);
                parmArray[2] = new SqlParameter("@plantcode", intPlantCode);
                parmArray[3] = new SqlParameter("@kind", intkind);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region  Work Group Update


        public string CheckGroupCode(string strCode, int intPlant)
        {
            try
            {
                string str = "sp_wo2_GetUpdateGroupInfor";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Creat", strCode);
                sqlParam[1] = new SqlParameter("@Type", "0");
                sqlParam[2] = new SqlParameter("@plantCode", intPlant);
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();

               
            }
            catch
            {
                return "";
            }
        }


        public string CheckMop(string strCode, int intPlant)
        {
            try
            {
                string str = "sp_wo2_GetUpdateGroupInfor";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Creat", strCode);
                sqlParam[1] = new SqlParameter("@Type", "1");
                sqlParam[2] = new SqlParameter("@plantCode", intPlant);
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }


        public string CheckSop(string strCode, int intPlant)
        {
            try
            {
                string str = "sp_wo2_GetUpdateGroupInfor";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Creat", strCode);
                sqlParam[1] = new SqlParameter("@Type", "2");
                sqlParam[2] = new SqlParameter("@plantCode", intPlant);
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }


        public string CheckUser(string strUser, string strName,int intPlant)
        {
            try
            {
                string str = "sp_wo2_CheckUser";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@userNo", strUser);
                sqlParam[1] = new SqlParameter("@userName", strName);
                sqlParam[2] = new SqlParameter("@plantCode", intPlant);
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public int UpdateGroup(int intUserID,string strUserNo,string strUserName,string strGroup,string strMop,string strSop,int intCreat,int intPlant,int intType)
        {
            try
            {
                string str = "sp_wo2_UpdateGroup";
                SqlParameter[] sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@userID", intUserID);
                sqlParam[1] = new SqlParameter("@userNo", strUserNo);
                sqlParam[2] = new SqlParameter("@UserName", strUserName);
                sqlParam[3] = new SqlParameter("@Group", strGroup);
                sqlParam[4] = new SqlParameter("@Mop", strMop);
                sqlParam[5] = new SqlParameter("@Sop", strSop);
                sqlParam[6] = new SqlParameter("@Creat", intCreat);
                sqlParam[7] = new SqlParameter("@plantCode", intPlant);
                sqlParam[8] = new SqlParameter("@Type", intType);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int DelGroupDetail(string strGID, int intPlant)
        {
            try
            {
                string str = "sp_wo2_DelGroupTmp";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Creat", strGID);
                sqlParam[1] = new SqlParameter("@Type","0");
                sqlParam[2] = new SqlParameter("@plantCode", intPlant);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }


        public int DelTmpData(int intUserID, int intPlant)
        {
            try
            {
                string str = "sp_wo2_DelGroupTmp";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Creat", intUserID);
                sqlParam[1] = new SqlParameter("@Type", "1");
                sqlParam[2] = new SqlParameter("@plantCode", intPlant);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }
        
#endregion

        #region User's Compare between work order and attendence
        public string WoAtCompare(string strStart,string strEnd,bool bolflag,int intDept,string strUser,string strName,int intWork,int intRole,int intPlant,int intCreat,int intType)
        {
            try
            {
                string str = "sp_wo2_UserWoAtCompare1";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@Startdate", strStart);
                sqlParam[1] = new SqlParameter("@Enddate", strEnd);
                sqlParam[2] = new SqlParameter("@Flag", bolflag);
                sqlParam[3] = new SqlParameter("@Department", intDept);
                sqlParam[4] = new SqlParameter("@UserNo", strUser);
                sqlParam[5] = new SqlParameter("@UserName", strName);
                sqlParam[6] = new SqlParameter("@Workshop", intWork);
                sqlParam[7] = new SqlParameter("@Role", intRole);
                sqlParam[8] = new SqlParameter("@PlantCode", intPlant);
                sqlParam[9] = new SqlParameter("@Creat", intCreat);
                sqlParam[10] = new SqlParameter("@Type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public DataTable WoAtCompareSelect(string strStart,string strEnd,bool bolflag,int intDept,string strUser,string strName,int intWork,int intRole,int intPlant,int intCreat)
        {
            try
            {
                adamClass adc = new adamClass();
                WorkOrder wo = new WorkOrder();
                string str = wo.WoAtCompare(strStart, strEnd, bolflag, intDept, strUser, strName, intWork,intRole,intPlant,intCreat, 0);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, str).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Check Salary

        public DataTable CheckSalary(int intYear, int intMonth, int intDepart, string strUser, string strUserName, int intUserType)
        {
            try
            {
                adamClass adc = new adamClass();
                //string str = ExportCheckSalary(intYear, intMonth, intDepart, strUser, strUserName, 0, intUserType);
                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.Text, ExportCheckSalary(intYear, intMonth, intDepart, strUser, strUserName, 0, intUserType)).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable CheckSalary1(int intYear, int intMonth, int intDepart, string strUser, string strUserName, int intUserType)
        {
            try
            {
                adamClass adc = new adamClass();
                //string str = ExportCheckSalary(intYear, intMonth, intDepart, strUser, strUserName, 0, intUserType);
                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.Text, ExportCheckSalary1(intYear, intMonth, intDepart, strUser, strUserName, 0, intUserType)).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public string ExportCheckSalary(int intYear, int intMonth, int intDepart, string strUser, string strUserName, int intType,int intUserType)
        {
            try
            {
                string str = "sp_hr_CheckSalary";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@Year", intYear);
                sqlParam[1] = new SqlParameter("@Month", intMonth);
                sqlParam[2] = new SqlParameter("@UserNo", strUser);
                sqlParam[3] = new SqlParameter("@UserName", strUserName);
                sqlParam[4] = new SqlParameter("@DepartmentID", intDepart);
                sqlParam[5] = new SqlParameter("@userType", intUserType);
                sqlParam[6] = new SqlParameter("@Type", intType);
                return SqlHelper.ExecuteScalar (adam.dsnx(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public string ExportCheckSalary1(int intYear, int intMonth, int intDepart, string strUser, string strUserName, int intType, int intUserType)
        {
            try
            {
                string str = "sp_hr_CheckSalary1";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@Year", intYear);
                sqlParam[1] = new SqlParameter("@Month", intMonth);
                sqlParam[2] = new SqlParameter("@UserNo", strUser);
                sqlParam[3] = new SqlParameter("@UserName", strUserName);
                sqlParam[4] = new SqlParameter("@DepartmentID", intDepart);
                sqlParam[5] = new SqlParameter("@userType", intUserType);
                sqlParam[6] = new SqlParameter("@Type", intType);
                return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }


        public string ExportCheckAttendance(string strStart,string strEnd,string strUserNo,string strUserName,int intDepart,int intUserType,int intType,int intflag)
        {

            try
            {
                string str = "sp_hr_CheckAttendance";
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@starttime", strStart);
                sqlParam[1] = new SqlParameter("@endtime", strEnd);
                sqlParam[2] = new SqlParameter("@UserNo", strUserNo);
                sqlParam[3] = new SqlParameter("@UserName", strUserName);
                sqlParam[4] = new SqlParameter("@DepartmentID", intDepart);
                sqlParam[5] = new SqlParameter("@userType", intUserType);
                sqlParam[6] = new SqlParameter("@Type", intType);
                sqlParam[7] = new SqlParameter("@flag", intflag);
                return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public string ExportCheckAttendance1(string strStart, string strEnd, string strUserNo, string strUserName, int intDepart, int intUserType, int intType, int intflag)
        {

            try
            {
                string str = "sp_hr_CheckAttendance1";
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@starttime", strStart);
                sqlParam[1] = new SqlParameter("@endtime", strEnd);
                sqlParam[2] = new SqlParameter("@UserNo", strUserNo);
                sqlParam[3] = new SqlParameter("@UserName", strUserName);
                sqlParam[4] = new SqlParameter("@DepartmentID", intDepart);
                sqlParam[5] = new SqlParameter("@userType", intUserType);
                sqlParam[6] = new SqlParameter("@Type", intType);
                sqlParam[7] = new SqlParameter("@flag", intflag);
                return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 考勤信息汇总 Add By Shanzm 2013-04-26
        /// </summary>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="intDepart"></param>
        /// <param name="intUserType"></param>
        /// <param name="intType"></param>
        /// <param name="intflag"></param>
        /// <returns></returns>
        public string ExportCheckAttendanceSum(string strStart, string strEnd, string strUserNo, string strUserName, int intDepart, int intUserType, int intType, int intflag)
        {

            try
            {
                string str = "sp_hr_CheckAttendanceSum";
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@starttime", strStart);
                sqlParam[1] = new SqlParameter("@endtime", strEnd);
                sqlParam[2] = new SqlParameter("@UserNo", strUserNo);
                sqlParam[3] = new SqlParameter("@UserName", strUserName);
                sqlParam[4] = new SqlParameter("@DepartmentID", intDepart);
                sqlParam[5] = new SqlParameter("@userType", intUserType);
                sqlParam[6] = new SqlParameter("@Type", intType);
                sqlParam[7] = new SqlParameter("@flag", intflag);
                return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 考勤信息汇总1 
        /// Add By Shanzm 2013-04-26  Modified BY LY  11/20/13
        /// </summary>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="intDepart"></param>
        /// <param name="intUserType"></param>
        /// <param name="intType"></param>
        /// <param name="intflag"></param>
        /// <returns></returns>
        public string ExportCheckAttendanceSum1(string strStart, string strEnd, string strUserNo, string strUserName, int intDepart, int intUserType, int intType, int intflag)
        {

            try
            {
                string str = "sp_hr_CheckAttendanceSum1";
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@starttime", strStart);
                sqlParam[1] = new SqlParameter("@endtime", strEnd);
                sqlParam[2] = new SqlParameter("@UserNo", strUserNo);
                sqlParam[3] = new SqlParameter("@UserName", strUserName);
                sqlParam[4] = new SqlParameter("@DepartmentID", intDepart);
                sqlParam[5] = new SqlParameter("@userType", intUserType);
                sqlParam[6] = new SqlParameter("@Type", intType);
                sqlParam[7] = new SqlParameter("@flag", intflag);
                return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public DataTable CheckAttendance(string strStart, string strEnd, string strUserNo, string strUserName, int intDepart,int intUserType,int intflag)
        {
            try
            {
                adamClass adc = new adamClass();
                //string str = ExportCheckAttendance(strStart, strEnd, strUserNo, strUserName, intDepart, intUserType, 0, intflag);
                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.Text, ExportCheckAttendance(strStart, strEnd, strUserNo, strUserName, intDepart, intUserType, 0, intflag)).Tables[0];
                //return  null;
            }
            catch
            {
                return null;
            }
        }

        public DataTable CheckAttendance1(string strStart, string strEnd, string strUserNo, string strUserName, int intDepart, int intUserType, int intflag)
        {
            try
            {
                adamClass adc = new adamClass();
                //string str = ExportCheckAttendance(strStart, strEnd, strUserNo, strUserName, intDepart, intUserType, 0, intflag);
                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.Text, ExportCheckAttendance(strStart, strEnd, strUserNo, strUserName, intDepart, intUserType, 0, intflag)).Tables[0];
                //return  null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 更新hr_ChAttendence
        /// </summary>
        /// <param name="strStart">起始日期</param>
        public void UpdateCheckAttendance(string strStart)
        {
            string str = "sp_hr_UpdateCheckAttendance";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@starttime", strStart);
            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, sqlParam);
        }

        #endregion

        #region Allowance for Atendance & Workyear 
        public int SaveAW(string strUserNo, string strComment,int intYear,int intMonth,decimal decAmount,int intplantcode,int intType)
        {
            try
            {
                string str = "sp_Hr_SaveAW";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@userNo", strUserNo);
                sqlParam[1] = new SqlParameter("@comment", strComment);
                sqlParam[2] = new SqlParameter("@Year", intYear);
                sqlParam[3] = new SqlParameter("@Month", intMonth);
                sqlParam[4] = new SqlParameter("@Amount", decAmount);
                sqlParam[5] = new SqlParameter("@plantcode", intplantcode);
                sqlParam[6] = new SqlParameter("@AType", intType);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }


        public string ExportAW(string strUserNo, int intdep, int intType, int intYear, int intMonth,int intEp,int intPlantcode)
        {
            try
            {
                string str = "sp_Hr_SelectAW";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@userNo", strUserNo);
                sqlParam[1] = new SqlParameter("@Year", intYear);
                sqlParam[2] = new SqlParameter("@Month", intMonth);
                sqlParam[3] = new SqlParameter("@Dept", intdep);
                sqlParam[4] = new SqlParameter("@AType", intType);
                sqlParam[5] = new SqlParameter("@plantcode", intPlantcode);
                sqlParam[6] = new SqlParameter("@EType", intEp);
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public DataTable SelectAW(string strUserNo, int intdep, int intType, int intYear, int intMonth, int intPlantcode)
        {
            try
            {
                string str =ExportAW(strUserNo, intdep, intType, intYear, intMonth, 0, intPlantcode);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text,str ).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        public int DelAW(int intplantcode, int Id, int intType)
        {
            try
            {
                string str = "sp_Hr_DelAW";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@plantcode", intplantcode);
                sqlParam[1] = new SqlParameter("@Id", Id);
                sqlParam[2] = new SqlParameter("@AType", intType);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public bool InsertFullAttendanceAW(int year, int month, int plantCode )
        {
            try
            {
                string str = "sp_Hr_InsertFullAttendanceAW";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@year", year);
                sqlParam[1] = new SqlParameter("@month", month);
                sqlParam[2] = new SqlParameter("@plantCode", plantCode);
                sqlParam[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, sqlParam);
                return Convert.ToBoolean(sqlParam[3].Value);
            }
            catch
            {
                return false;
            }
        
        }
        #endregion        
    }
}