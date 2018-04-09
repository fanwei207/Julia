using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Collections;
using adamFuncs;
/// <summary>
/// Summary description for MinorPurchase
/// </summary>
/// 
namespace MinorP
{
    public class MinorPurchase
    {

        adamClass adam = new adamClass();
        public MinorPurchase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        

#region MinorPurchaseType

        public string SelectMPType(string strName)
        {
            try
            {
                string str;
                str = "SELECT systemcodeID,systemcodeName As strName FROM  systemcode  Where comments IS NULL and systemcodeTypeID IN (SELECT systemcodeTypeID FROM systemcodeType Where systemcodeTypeName='Minor Purchase')";
                return str;
            }
            catch
            {
                return "";
            }
        }


        public DataTable MinorPType(string strName)
        {
            try
            {

               DataTable dsType =  SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, SelectMPType(strName)).Tables[0];
               return dsType;
            }
            catch
            {
                return null;
            }
        }


        public int updateMPType(int systemcodeID, string strName)
        {
            try
            {
                string str = "IF Not EXISTS (SELECT * FROM systemcode WHERE systemcodeName=N'" + strName + "') Update systemcode SET systemcodeName =N'" + strName + "' Where systemcodeID ='" + systemcodeID.ToString() + "' ";
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int DelMPType(int systemcodeID)
        {
            try
            {
                string str = "Update systemcode SET comments ='x'  Where systemcodeID ='" + systemcodeID.ToString() + "' ";
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int SaveMPType(string strName)
        {
            try
            {
                string str = "IF Not EXISTS (SELECT * FROM systemcode WHERE systemcodeName=N'" + strName + "' AND systemcodeTypeID =43)  INSERT INTO systemcode(systemCodeTypeID,systemcodeName) SELECT systemcodeTypeID,N'"+ strName +"' FROM systemcodeType Where systemcodeTypeName='Minor Purchase' ";
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
#endregion

        #region MinorPurchase Order

        public string UserEmail(int intUid)
        {
            try
            {
                string str = " SELECT ISNULL(email,'') FROM tcpc0..Users Where deleted=0 And isactive=1 And leavedate is null And userID =" + intUid.ToString();
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, str));
            }
            catch
            {
                return "";
            }

        }

        public DataTable ApplicatUser(int intDept,string strUserName,int intPlant)
        {
            try
            {
                string str = "SELECT userID,userName FROM Users Where deleted=0 and isactive=1 and leavedate IS NULL and plantcode ='" + intPlant.ToString() + "' ";
                if (intDept >0)
                    str = str + " AND departmentID ='" + intDept.ToString() + "' ";
                if (strUserName.Trim().Length >0 )
                    str = str + " AND userName like N'%" + strUserName  + "%' ";
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable ApplySelect(int intAid,int intPlant)
        {
            try
            {
                string str = "SELECT u.userName,ISNULL(d.name,'') as dname,a.Appdate,a.comments FROM MPApplication a INNER JOIN Users u ON u.userID =a.Appuid LEFT OUTER JOIN tcpc" + intPlant.ToString() + "..departments d ON d.departmentID =u.departmentID WHERE mpid ='" + intAid.ToString() + "' ORDER BY Appdate desc ";
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable AttachedSelect(int intAid,int intUid)
        {
            try 
            {
                string str = "SELECT  AttUserID,AttID,attname,attuser,attdate FROM MPAttachFile WHERE MPid= " + intAid.ToString() + " Union SELECT  AttUserID,AttID,attname,attuser,attdate FROM MPAttachFile WHERE MPid=0 And AttUserID=" + intUid.ToString() + " ORDER BY attdate desc ";
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str).Tables[0];

            }
            catch
            {
                return null;
            }
        }

        public int DelAttached(int intAtt)
        {
            try
            {
                string str = "Delete FROM MPAttachFile Where AttID =" + intAtt;
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int SaveApplication(int intDept,int intPtype,decimal decQty,decimal decPrice, string strPart,string strSP,int intPlant,int intCreat,int intNext,string strCom,int intAid,int intFin)
        {
            try
            {
                string str = "sp_mp_SaveApplication";
                SqlParameter[] parmArray = new SqlParameter[12];
                parmArray[0] = new SqlParameter("@Dept", intDept);
                parmArray[1] = new SqlParameter("@Ptype", intPtype);
                parmArray[2] = new SqlParameter("@Qty", decQty);
                parmArray[3] = new SqlParameter("@Price", decPrice);
                parmArray[4] = new SqlParameter("@Part", strPart);
                parmArray[5] = new SqlParameter("@Sper", strSP);
                parmArray[6] = new SqlParameter("@PlantCode", intPlant);
                parmArray[7] = new SqlParameter("@creat", intCreat);
                parmArray[8] = new SqlParameter("@backer", intNext);
                parmArray[9] = new SqlParameter("@comment", strCom);
                parmArray[10] = new SqlParameter("@Aid", intAid);
                parmArray[11] = new SqlParameter("@Fid", intFin);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray));

                
            }
            catch
            {
                return -1;
            }
        }



        public int SaveAttched(string strFile,byte[] byImage,string strType,int intUid,string strName,int intAid)
        {
            try
            {
                string str = "sp_mp_SaveAttached";
                SqlParameter[] parmArray = new SqlParameter[12];
                parmArray[0] = new SqlParameter("@fname", strFile);
                parmArray[1] = new SqlParameter("@Imagedata", byImage);
                parmArray[2] = new SqlParameter("@Stype", strType);
                parmArray[3] = new SqlParameter("@uid", intUid);
                parmArray[4] = new SqlParameter("@uname", strName);
                parmArray[5] = new SqlParameter("@Aid", intAid);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, parmArray);

                return 0;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region MinorP Order List
        /// <summary>
        /// Minor Purchase Order Detail Information
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="intDept"></param>
        /// <param name="intType"></param>
        /// <param name="strSp"></param>
        /// <param name="intPlant"></param>
        /// <param name="blchk"></param>
        /// <param name="intsid"></param>
        /// <returns></returns>
        public  DataTable MinorPList( int intPlant,int intsid,int intAid)
        {
            try
            {
                adamClass adc = new adamClass();

                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, MinorPString("", 0, 0, "", intPlant, 0, intsid,intAid, 0)).Tables[0];
            }
            catch
            {
                return null;
            }
        }

       

        public DataTable MPList(string strUser, int intDept, int intType, string strSp, int intPlant, bool blChk, int intsid)
        {
            
            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, MinorPString(strUser, intDept, intType, strSp, intPlant, blChk ? 1 : 0, intsid, 0, 0)).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public string MinorPString(string strUser, int intDept, int intType, string strSp, int intPlant, int intChk, int intsid, int intAid, int intEtype)
        {
            try
            {
                string str = "sp_mp_SelectMP";
                SqlParameter[] parmArray = new SqlParameter[9];
                parmArray[0] = new SqlParameter("@Apper", strUser);
                parmArray[1] = new SqlParameter("@Dept", intDept);
                parmArray[2] = new SqlParameter("@Ptype", intType);
                parmArray[3] = new SqlParameter("@Sper", strSp);
                parmArray[4] = new SqlParameter("@Pcode", intPlant);
                parmArray[5] = new SqlParameter("@Chkp", intChk);
                parmArray[6] = new SqlParameter("@AID", intAid);
                parmArray[7] = new SqlParameter("@Etype", intEtype);
                parmArray[8] = new SqlParameter("@sid", intsid);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray));
            }
            catch
            {
                return "";
            }
        }

        public int DeleteMP(int intID,int intUid)
        {
            try
            {
                string str = "UPDATE MinorPurchaseOrder SET Pstatus = 'X',Deletor='"+ intUid.ToString() +"' WHERE id=" + intID.ToString();
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        #endregion


        #region MP Recieved
         
          public string MPRorFstring(string strUser,int intDept,int intPtype,string strSp,int intEtype,int intKind,int intPlant, int intUid)
          {
              try
              {
                  string str = "sp_mp_RorFselect";
                  SqlParameter[] parmArray = new SqlParameter[8];
                  parmArray[0] = new SqlParameter("@Apper", strUser);
                  parmArray[1] = new SqlParameter("@Dept", intDept);
                  parmArray[2] = new SqlParameter("@Ptype", intPtype);
                  parmArray[3] = new SqlParameter("@Sper", strSp);
                  parmArray[4] = new SqlParameter("@Etype", intEtype);
                  parmArray[5] = new SqlParameter("@Pcode", intPlant);
                  parmArray[6] = new SqlParameter("@Kind", intKind);
                  parmArray[7] = new SqlParameter("@Uid", intUid);
       

                  return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray));
              }
              catch 
              {
                  return "";
              }
          }

        public DataTable MPRecieve(string strUser, int intDept, int intPtype, string strSp, int intPlant, int intUid)
        {
            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, MPRorFstring(strUser, intDept, intPtype, strSp, 0, 0, intPlant, intUid)).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        public int SaveMpRecieve(decimal decQty, int intDept, int intAid, int intcreat)
        {
            try
            {
                string str = "Update MinorPurchaseOrder SET RecieveQty=" + decQty.ToString() + ",Redepart=" + intDept.ToString() + ",Reciever=" + intcreat.ToString() + ",Pstatus='R' Where id=" + intAid.ToString();
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);
                return 0;

            }
            catch
            {
                return -1;
            }
        }
        #endregion


        #region MP Finance

        public DataTable MPFinance(string strUser, int intDept, int intPtype, string strSp, int intPlant, int intUid)
        {
            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, MPRorFstring(strUser, intDept, intPtype, strSp,0, 1, intPlant, intUid)).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public int SaveFinance(string strNum, decimal decPrice, decimal decQty, int intAid, int intPlant, int intCreat)
        {
            try
            {
                string str = "Update MinorPurchaseOrder SET FinNo='" + strNum + "',FinPrice=" + decPrice.ToString() + ",FinQty=" + decQty.ToString() + ",FinDate=getdate(),Finer=" + intCreat.ToString() + ",Pstatus='F' WHERE id=" + intAid.ToString ();
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public DataTable MPFinanceSelect(int intAid,int intPlant)
        {
            try
            {
                string str = "SELECT  m.id as Aid, m.Pstatus,m.userID, m.userName as Aname, ISNULL(d.name,'') As dname, Part, ISNULL(s.systemcodeName,'') As Ptype, m.Quantity,  m.creatdate,m.Price,m.Supplier,ISNULL(m.RecieveQty,0) ";
                str = str + " FROM tcpc0..MinorPurchaseOrder m ";
                str = str + " LEFT OUTER JOIN tcpc" + intPlant.ToString() + "..departments d ON d.departmentID = m.departmentID ";
                str = str + " LEFT OUTER JOIN tcpc0..systemcode s ON s.systemcodeID = m.Parttype ";
                str = str + " Where  m.Pstatus<>'X' And  id =" + intAid.ToString();
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str).Tables[0];
            }
            catch
            {
                return null;
            }

        }
        #endregion


        #region MP Summary
        public string MPSummaryString(string strStart, string strEnd, int intDept, int intPtype, string strSP, int intCond, int intPlant, int intCreat,int intType,int intAid)
        {
            try
            {
                string str = "sp_mp_SummarySelect";
                SqlParameter[] parmArray = new SqlParameter[10];
                parmArray[0] = new SqlParameter("@start", strStart);
                parmArray[1] = new SqlParameter("@sEnd", strEnd);
                parmArray[2] = new SqlParameter("@Dept", intDept);
                parmArray[3] = new SqlParameter("@Ptype", intPtype);
                parmArray[4] = new SqlParameter("@Sper", strSP);
                parmArray[5] = new SqlParameter("@Cond", intCond);
                parmArray[6] = new SqlParameter("@Pcode", intPlant);
                parmArray[7] = new SqlParameter("@Uid", intCreat);
                parmArray[8] = new SqlParameter("@Kind", intType);
                parmArray[9] = new SqlParameter("@Aid", intAid);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray));
            }
            catch
            {
                return "";
            }
        }

        public DataTable MPSummary(string strStart, string strEnd, int intDept, int intPtype, string strSP, bool blAll,bool blRe,bool blFin, int intPlant, int intCreat,  int intAid)
        {
            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, MPSummaryString(strStart, strEnd, intDept, intPtype, strSP, blAll ? 1 : (blRe ? 2 : 3), intPlant, intCreat, 0, intAid)).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}