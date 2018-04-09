using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace QCProgress{
    /// <summary>
    /// 要限制的文件格式 枚举类型
    /// </summary>
    public enum Section
    {
        TXT, //txt文件格式
        XLS,  //excel文件格式
        ERROR
    }

    /// <summary>
    /// F执行文件的导入、导出
    /// </summary>
    public class FileOperate
    {
        private HtmlInputFile _File;//the file that will be uploaded to the server
        private string _FileName;//file neme
        private Section _FileSection;//section of the file 
        private string _FolderPath;//the path the file will be saved
        private int _LengthLimit;//file's length
        private Section _SectionLimit;//dicide which section that the file can be uploaded

        public FileOperate(HtmlInputFile postfile, string folderpath, int lenght)
        {
            //initial the members

            string strPostFileName = postfile.PostedFile.FileName.Trim();
            string strFileName = strPostFileName.Substring(strPostFileName.LastIndexOf('\\') + 1).Trim();

            if (strFileName != "")
            {
                _File = postfile;
                _FileSection = ConvertSectionToEnum(strFileName.Substring(strFileName.LastIndexOf('.') + 1).Trim());
                _FileName = strFileName.Substring(0, strFileName.Length - strFileName.LastIndexOf('.') - 3) + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                _FolderPath = folderpath;
                _LengthLimit = lenght;
                _SectionLimit = Section.TXT;
            }
        }

        #region 设置限制的文件格式 set the limited section that a file can be uploaded
        /// <summary>
        /// set the limited section that a file can be uploaded 设置限制的文件格式
        /// </summary>
        public Section SectionLimit
        {
            get 
            {
                return _SectionLimit;
            }
            set
            {
                _SectionLimit = value;
            }
        }
        #endregion

        #region 将字符串转换成枚举 convert a string to an Enum
        /// <summary>
        /// convert a string to an Enum 将字符串转换成枚举
        /// </summary>
        /// <param name="sct"></param>
        /// <returns></returns>
        private Section ConvertSectionToEnum(string sct) 
        {
            Section returnVal = Section.TXT;

            foreach(string str in Enum.GetNames(typeof(Section)))
            {
                if (sct.Trim().ToUpper() == str)
                {
                    returnVal = (Section)Enum.Parse(typeof(Section), str);
                    break;
                }
                else 
                {
                    returnVal = Section.ERROR;
                }
            }

            return returnVal;
        }
        #endregion

        #region 将文件保存到服务器上 save the uploaded file to the server
        /// <summary>
        /// save the uploaded file to the server 将文件保存到服务器上
        /// </summary>
        /// <param name="msg">返回出错信息</param>
        /// <returns></returns>
        public bool SaveFileToServer(ref string msg)
        {
            //check file selection
            if (_FileName == null || _FileName == "")
            {
                msg = "请选择要导入的文件";
                return false;
            }

            //check section
            if (_FileSection == Section.ERROR || _FileSection != _SectionLimit) 
            {
                msg = "文件格式只能为:" + _SectionLimit;
                return false;
            }

            //check folder
            if (!Directory.Exists(_FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(_FolderPath);
                }
                catch (Exception e1)
                {
                    msg = "文件夹创建失败!原因:" + e1.ToString();
                    return false;
                }
            }

            //check file length
            if (_File.PostedFile.ContentLength > _LengthLimit)
            {
                msg = "上传的文件最大为 8 MB";
                return false;
            }

            //save file
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();
            try
            {
                _File.PostedFile.SaveAs(strFullName);
            }
            catch (Exception e2)
            {
                msg = "上传文件失败!原因:" + e2.ToString();
                return false;
            }
            return true;
        }
        #endregion

        #region 读取指定的txt文件，并存入数据库 import a txt file to the db
        /// <summary>
        /// import a txt file to the db 读取指定的txt文件，并存入数据库
        /// </summary>
        /// <param name="msg"></param>
        public void ImportTxt(string line, string receiver, int hour, string way,string part,int person, ref string msg) 
        {
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();

            if (File.Exists(strFullName))
            {
                string strLine;
                StreamReader strReader = new StreamReader(strFullName,Encoding.Default);

                //line 1-8
                string _ProductType;
                string _Manufacturer;
                string _Instrument;
                string _TestDepartment;
                string _Temperature;
                string _Humidity;
                string _TestOperator;
                string _TestDate;
                string _TestType = hour.ToString()+"h/" + way;

                try
                {
                    strLine = strReader.ReadLine().Trim();
                    _ProductType = strLine.Split(new char[]{':'},StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    if (part != "" && part != _ProductType) 
                    {
                        msg = "产品型号不一致!位置: Line 1";
                        return;
                    }

                    strLine = strReader.ReadLine().Trim();
                    _Manufacturer = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Instrument = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestDepartment = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Temperature = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Humidity = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestOperator = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestDate = strLine.Substring(strLine.IndexOf(':')+1).Trim();
                }
                catch 
                {
                    msg = "请检查文件的完整性!位置: Line 1--8";
                    return;
                }
                //line 9
                strReader.ReadLine();

                //line 10
                string _x;
                string _y;
                string _u;
                string _v;
                string[] strArray10 = new string[4];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");

                try
                {
                    strArray10 = strLine.Split(' ');
                    _x = strArray10[0].Split('=')[1].Trim();
                    _y = strArray10[1].Split('=')[1].Trim();
                    _u = strArray10[2].Split('=')[1].Trim();
                    _v = strArray10[3].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 10";
                    return;
                }

                //line 11
                string _Tc;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Tc = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 11";
                    return;
                }

                //line 12
                string _Err;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Err = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 12";
                    return;
                }

                //line 13
                string _Ra;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Ra = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 13";
                    return;
                }

                //line 14
                string _R1;
                string _R2;
                string _R3;
                string _R4;
                string _R5;
                string _R6;
                string _R7;
                string _R8;
                string _R9;
                string _R10;
                string _R11;
                string _R12;
                string _R13;
                string _R14;
                string[] strArray14 = new string[14];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray14 = strLine.Split(' ');
                    _R1 = strArray14[0].Split('=')[1].Trim();
                    _R2 = strArray14[1].Split('=')[1].Trim();
                    _R3 = strArray14[2].Split('=')[1].Trim();
                    _R4 = strArray14[3].Split('=')[1].Trim();
                    _R5 = strArray14[4].Split('=')[1].Trim();
                    _R6 = strArray14[5].Split('=')[1].Trim();
                    _R7 = strArray14[6].Split('=')[1].Trim();
                    _R8 = strArray14[7].Split('=')[1].Trim();
                    _R9 = strArray14[8].Split('=')[1].Trim();
                    _R10 = strArray14[9].Split('=')[1].Trim();
                    _R11 = strArray14[10].Split('=')[1].Trim();
                    _R12 = strArray14[11].Split('=')[1].Trim();
                    _R13 = strArray14[12].Split('=')[1].Trim();
                    _R14 = strArray14[13].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 14";
                    return;
                }

                //line 15
                string _PeakWave;
                string _HalfWave;
                string[] strArray15 = new string[2];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray15 = strLine.Split(new string[] { "nm" }, StringSplitOptions.RemoveEmptyEntries);
                    _PeakWave = strArray15[0].Split('=')[1].Trim() + "nm";
                    _HalfWave = strArray15[1].Split('=')[1].Trim() + "nm";
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 15";
                    return;
                }

                //line 16
                string _RedRatio;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _RedRatio = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 16";
                    return;
                }

                //line 17
                string _Flux;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _Flux = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 17";
                    return;
                }

                //line 18
                string _Efficiency;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _Efficiency = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 18";
                    return;
                }
                //line 19
                string _U;
                string _I;
                string _P;
                string _PF;
                string[] strArray19 = new string[4];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray19 = strLine.Split(' ');
                    _U = strArray19[0].Split('=')[1].Trim();
                    _I = strArray19[1].Split('=')[1].Trim();
                    _P = strArray19[2].Split('=')[1].Trim();
                    _PF = strArray19[3].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 19";
                    return;
                }
                //now generate sql
                string strSQL = "Insert Into luminousFlux(";
                strSQL += "prh_line,prh_receiver,";
                strSQL += "ProductType,Manufacturer,Instrument,TestDepartment,Temperature,Humidity, ";
                strSQL += "TestOperator,TestDate,TestType,x,y,u,v,Tc,Err,Ra,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,";
                strSQL += "R13,R14,PeakWave,HalfWave,RedRatio,Flux,Efficiency,U1,I1,P1,PF1,createBy,createDate)Values('";
                strSQL += line + "','" + receiver + "',N'";
                strSQL += _ProductType + "',N'" + _Manufacturer + "',N'" + _Instrument + "',N'" + _TestDepartment + "',N'" + _Temperature + "',N'" + _Humidity + "',N'";
                strSQL += _TestOperator + "',N'" + _TestDate + "',N'" + _TestType + "',N'" + _x + "',N'" + _y + "',N'" + _u + "',N'" + _v + "',N'" + _Tc + "',N'" + _Err + "',N'" + _Ra + "',N'" + _R1 + "',N'" + _R2 + "',N'";
                strSQL += _R3 + "',N'" + _R4 + "',N'" + _R5 + "',N'" + _R6 + "',N'" + _R7 + "',N'" + _R8 + "',N'" + _R9 + "',N'" + _R10 + "',N'" + _R11 + "',N'" + _R12 + "',N'";
                strSQL += _R13 + "',N'" + _R14 + "',N'" + _PeakWave + "',N'" + _HalfWave + "',N'" + _RedRatio + "',N'" + _Flux + "',N'" + _Efficiency + "',N'" + _U + "',N'" + _I + "',N'" + _P + "',N'" + _PF + "',"+person+",GetDate())";
                

                //execut database
                adamClass chk = new adamClass();
                try
                {
                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL);
                }
                catch (Exception ee)
                {
                    msg = "写入数据库时失败!原因:" + ee.ToString();
                    return;
                }
                finally
                {
                    strReader.Dispose();
                    File.Delete(strFullName);
                }

                msg = "成功写入数据库";

                strReader.Dispose();
            }
            else 
            {
                msg = "给定的文件不存在!";
            }
            return;
        }
        #endregion

        #region 光电色Excel模板检查
        public string ChechLumTemplete(DataTable templete)
        {
            if (templete.Columns.Count != 22)
            {
                return "模板列数不正确!模板共22列;少于22列的,请添加完整;多余22列的,请检查自W列之后是否有文字,不论有无,请选择尽可能多的列,然后做一次删除操作!";
            }
            else
            {
                if (templete.Columns[0].ColumnName != "序号")
                {
                    return "模板第1列名必须是'序号'";
                }

                if (templete.Columns[1].ColumnName != "产品型号")
                {
                    return "模板第2列名必须是'产品型号'";
                }

                if (templete.Columns[2].ColumnName != "测试日期")
                {
                    return "模板第3列名必须是'测试日期'";
                }

                if (templete.Columns[3].ColumnName != "点燃方式")
                {
                    return "模板第4列名必须是'点燃方式'";
                }

                if (templete.Columns[4].ColumnName.ToLower() != "x")
                {
                    return "模板第5列名必须是'x'";
                }

                if (templete.Columns[5].ColumnName.ToLower() != "y")
                {
                    return "模板第6列名必须是'y'";
                }

                if (templete.Columns[6].ColumnName.ToLower() != "u")
                {
                    return "模板第7列名必须是'u'";
                }

                if (templete.Columns[7].ColumnName.ToLower() != "v")
                {
                    return "模板第8列名必须是'v'";
                }

                if (templete.Columns[8].ColumnName.ToUpper() != "色温(K)")
                {
                    return "模板第9列名必须是'色温(K)'";
                }

                if (templete.Columns[9].ColumnName.ToUpper() != "色容差(SDCM)")
                {
                    return "模板第10列名必须是'色容差(SDCM)'";
                }

                if (templete.Columns[10].ColumnName.ToLower() != "光通量(lm)")
                {
                    return "模板第11列名必须是'光通量(lm)'";
                }

                if (templete.Columns[11].ColumnName.ToLower() != "光效(lm/w)")
                {
                    return "模板第12列名必须是'光效(lm/W)'";
                }

                if (templete.Columns[12].ColumnName.ToUpper() != "辐射功率(W)")
                {
                    return "模板第13列名必须是'辐射功率(W)'";
                }

                if (templete.Columns[13].ColumnName.ToUpper() != "U(V)")
                {
                    return "模板第14列名必须是'U(V)'";
                }

                if (templete.Columns[14].ColumnName.ToUpper() != "I(A)")
                {
                    return "模板第15列名必须是'I(A)'";
                }

                if (templete.Columns[15].ColumnName.ToUpper() != "P(W)")
                {
                    return "模板第16列名必须是'P(W)'";
                }

                if (templete.Columns[16].ColumnName.ToUpper() != "PF")
                {
                    return "模板第17列名必须是'PF'";
                }

                if (templete.Columns[17].ColumnName != "红色比(%)")
                {
                    return "模板第18列名必须是'红色比(%)'";
                }

                if (templete.Columns[18].ColumnName != "色纯度(%)")
                {
                    return "模板第19列名必须是'色纯度(%)'";
                }

                if (templete.Columns[19].ColumnName.ToLower() != "主波长(nm)")
                {
                    return "模板第20列名必须是'主波长(nm)'";
                }

                if (templete.Columns[20].ColumnName.ToLower() != "峰值波长(nm)")
                {
                    return "模板第21列名必须是'峰值波长(nm)'";
                }

                if (templete.Columns[21].ColumnName.ToLower() != "ra")
                {
                    return "模板第22列名必须是'Ra'";
                }

                return string.Empty;
            }
        }
        #endregion

        #region 成品检验中的光电色导入
        public void ProductImportTxt(string nbr, string lot, int hour, string way, string part, int person, Boolean tcp, ref string msg)
        {
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();

            if (File.Exists(strFullName))
            {
                string strLine;
                StreamReader strReader = new StreamReader(strFullName, Encoding.Default);

                //line 1-8
                string _ProductType;
                string _Manufacturer;
                string _Instrument;
                string _TestDepartment;
                string _Temperature;
                string _Humidity;
                string _TestOperator;
                string _TestDate;
                string _TestType = hour.ToString() + "h/" + way;

                try
                {
                    strLine = strReader.ReadLine().Trim();
                    _ProductType = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    if (part != "" && part != _ProductType)
                    {
                        msg = "产品型号不一致!位置: Line 1";
                        return;
                    }

                    strLine = strReader.ReadLine().Trim();
                    _Manufacturer = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Instrument = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestDepartment = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Temperature = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Humidity = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestOperator = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestDate = strLine.Substring(strLine.IndexOf(':') + 1).Trim();
                }
                catch
                {
                    msg = "请检查文件的完整性!位置: Line 1--8";
                    return;
                }
                //line 9
                strReader.ReadLine();

                //line 10
                string _x;
                string _y;
                string _u;
                string _v;
                string[] strArray10 = new string[4];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");

                try
                {
                    strArray10 = strLine.Split(' ');
                    _x = strArray10[0].Split('=')[1].Trim();
                    _y = strArray10[1].Split('=')[1].Trim();
                    _u = strArray10[2].Split('=')[1].Trim();
                    _v = strArray10[3].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 10";
                    return;
                }

                //line 11
                string _Tc;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Tc = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 11";
                    return;
                }

                //line 12
                string _Err;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Err = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 12";
                    return;
                }

                //line 13
                string _Ra;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Ra = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 13";
                    return;
                }

                //line 14
                string _R1;
                string _R2;
                string _R3;
                string _R4;
                string _R5;
                string _R6;
                string _R7;
                string _R8;
                string _R9;
                string _R10;
                string _R11;
                string _R12;
                string _R13;
                string _R14;
                string[] strArray14 = new string[14];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray14 = strLine.Split(' ');
                    _R1 = strArray14[0].Split('=')[1].Trim();
                    _R2 = strArray14[1].Split('=')[1].Trim();
                    _R3 = strArray14[2].Split('=')[1].Trim();
                    _R4 = strArray14[3].Split('=')[1].Trim();
                    _R5 = strArray14[4].Split('=')[1].Trim();
                    _R6 = strArray14[5].Split('=')[1].Trim();
                    _R7 = strArray14[6].Split('=')[1].Trim();
                    _R8 = strArray14[7].Split('=')[1].Trim();
                    _R9 = strArray14[8].Split('=')[1].Trim();
                    _R10 = strArray14[9].Split('=')[1].Trim();
                    _R11 = strArray14[10].Split('=')[1].Trim();
                    _R12 = strArray14[11].Split('=')[1].Trim();
                    _R13 = strArray14[12].Split('=')[1].Trim();
                    _R14 = strArray14[13].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 14";
                    return;
                }

                //line 15
                string _PeakWave;
                string _HalfWave;
                string[] strArray15 = new string[2];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray15 = strLine.Split(new string[] { "nm" }, StringSplitOptions.RemoveEmptyEntries);
                    _PeakWave = strArray15[0].Split('=')[1].Trim() + "nm";
                    _HalfWave = strArray15[1].Split('=')[1].Trim() + "nm";
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 15";
                    return;
                }

                //line 16
                string _RedRatio;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _RedRatio = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 16";
                    return;
                }

                //line 17
                string _Flux;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _Flux = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 17";
                    return;
                }

                //line 18
                string _Efficiency;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _Efficiency = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 18";
                    return;
                }
                //line 19
                string _U;
                string _I;
                string _P;
                string _PF;
                string[] strArray19 = new string[4];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray19 = strLine.Split(' ');
                    _U = strArray19[0].Split('=')[1].Trim();
                    _I = strArray19[1].Split('=')[1].Trim();
                    _P = strArray19[2].Split('=')[1].Trim();
                    _PF = strArray19[3].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 19";
                    return;
                }
                //now generate sql
                string strSQL = "Insert Into qc_product_lum(";
                strSQL += "wo_nbr,wo_lot,";
                strSQL += "ProductType,Manufacturer,Instrument,TestDepartment,Temperature,Humidity, ";
                strSQL += "TestOperator,TestDate,TestType,x,y,u,v,Tc,Err,Ra,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,";
                strSQL += "R13,R14,PeakWave,HalfWave,RedRatio,Flux,Efficiency,U1,I1,P1,PF1,createBy,createDate, tcp)Values('";
                strSQL +=  nbr + "','" + lot + "',N'";
                strSQL += _ProductType + "',N'" + _Manufacturer + "',N'" + _Instrument + "',N'" + _TestDepartment + "',N'" + _Temperature + "',N'" + _Humidity + "',N'";
                strSQL += _TestOperator + "',N'" + _TestDate + "',N'" + _TestType + "',N'" + _x + "',N'" + _y + "',N'" + _u + "',N'" + _v + "',N'" + _Tc + "',N'" + _Err + "',N'" + _Ra + "',N'" + _R1 + "',N'" + _R2 + "',N'";
                strSQL += _R3 + "',N'" + _R4 + "',N'" + _R5 + "',N'" + _R6 + "',N'" + _R7 + "',N'" + _R8 + "',N'" + _R9 + "',N'" + _R10 + "',N'" + _R11 + "',N'" + _R12 + "',N'";
                strSQL += _R13 + "',N'" + _R14 + "',N'" + _PeakWave + "',N'" + _HalfWave + "',N'" + _RedRatio + "',N'" + _Flux + "',N'" + _Efficiency + "',N'" + _U + "',N'" + _I + "',N'" + _P + "',N'" + _PF + "'," + person + ",GetDate(), '" + tcp.ToString() + "')";


                //execut database
                adamClass chk = new adamClass();
                try
                {
                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL);
                }
                catch (Exception ee)
                {
                    msg = "写入数据库时失败!原因:" + ee.ToString();
                    return;
                }
                finally
                {
                    strReader.Dispose();
                    File.Delete(strFullName);
                }

                msg = "成功写入数据库";

                strReader.Dispose();
            }
            else
            {
                msg = "给定的文件不存在!";
            }
            return;
        }
        public void ProductImportExcel(string nbr, string lot, int hour, int way, string part, int person, Boolean tcp, ref string msg)
        {
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();
            adamClass chk = new adamClass();
            string strSQL = "";

            string _line;
            string _ProductType;
            string _Manufacturer;
            string _Instrument;
            string _TestDepartment;
            string _Temperature;
            string _Humidity;
            string _TestOperator;
            string _TestDate;
            string _TestType;
            string _x;
            string _y;
            string _u;
            string _v;
            string _Tc;
            string _Err;
            string _Ra;
            string _R1;
            string _R2;
            string _R3;
            string _R4;
            string _R5;
            string _R6;
            string _R7;
            string _R8;
            string _R9;
            string _R10;
            string _R11;
            string _R12;
            string _R13;
            string _R14;
            string _PeakWave;
            string _HalfWave;
            string _RedRatio;
            string _Flux;
            string _Efficiency;
            string _U;
            string _I;
            string _P;
            string _PF;
            string _color;//色纯度
            string _fushe;//辐射功率(W)

            if (File.Exists(strFullName))
            {
                DataSet dst;
                try
                {
                    dst = chk.getExcelContents(strFullName);
                }
                catch
                {
                    msg = "获取数据失败，请与管理员联系";
                    return;
                }

                if (dst.Tables[0].Rows.Count > 0)
                {
                    string error = ChechLumTemplete(dst.Tables[0]);

                    if (error != string.Empty)
                    {
                        msg = error;
                        return;
                    }

                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                    {
                        _line = dst.Tables[0].Rows[i][0].ToString().Trim();
                        _ProductType = dst.Tables[0].Rows[i][1].ToString().Trim();

                        if (part != "" && part != _ProductType)
                        {
                            continue;
                        }

                        _Manufacturer = "YQL";
                        _Instrument = "PMS-50";
                        _TestDepartment = "测试中心";
                        _Temperature = "25℃";
                        _Humidity = "60%";
                        _TestOperator = "测试001";
                        _TestDate = dst.Tables[0].Rows[i][2].ToString().Trim();
                        _TestType = dst.Tables[0].Rows[i][3].ToString().Trim();
                        _x = dst.Tables[0].Rows[i][4].ToString().Trim();
                        _y = dst.Tables[0].Rows[i][5].ToString().Trim();
                        _u = dst.Tables[0].Rows[i][6].ToString().Trim();
                        _v = dst.Tables[0].Rows[i][7].ToString().Trim();
                        _Tc = dst.Tables[0].Rows[i][8].ToString().Trim();
                        _Err = dst.Tables[0].Rows[i][9].ToString().Trim();
                        _Ra = dst.Tables[0].Rows[i][21].ToString().Trim();
                        _R1 = "";
                        _R2 = "";
                        _R3 = "";
                        _R4 = "";
                        _R5 = "";
                        _R6 = "";
                        _R7 = "";
                        _R8 = "";
                        _R9 = "";
                        _R10 = "";
                        _R11 = "";
                        _R12 = "";
                        _R13 = "";
                        _R14 = "";
                        _PeakWave = dst.Tables[0].Rows[i][19].ToString().Trim();
                        _HalfWave = dst.Tables[0].Rows[i][20].ToString().Trim();
                        _RedRatio = dst.Tables[0].Rows[i][17].ToString().Trim();
                        _Flux = dst.Tables[0].Rows[i][10].ToString().Trim();
                        _Efficiency = dst.Tables[0].Rows[i][11].ToString().Trim();
                        _U = dst.Tables[0].Rows[i][13].ToString().Trim();
                        _I = dst.Tables[0].Rows[i][14].ToString().Trim();
                        _P = dst.Tables[0].Rows[i][15].ToString().Trim();
                        _PF = dst.Tables[0].Rows[i][16].ToString().Trim();
                        _color = dst.Tables[0].Rows[i][18].ToString().Trim();
                        _fushe = dst.Tables[0].Rows[i][12].ToString().Trim();

                        strSQL += "  Insert Into qc_product_lum(";
                        strSQL += "fushe, color, Line,wo_nbr,wo_lot,";
                        strSQL += "ProductType,Manufacturer,Instrument,TestDepartment,Temperature,Humidity, ";
                        strSQL += "TestOperator,TestDate,TestType,x,y,u,v,Tc,Err,Ra,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,";
                        strSQL += "R13,R14,PeakWave,HalfWave,RedRatio,Flux,Efficiency,U1,I1,P1,PF1,CreateBy,CreateDate, tcp)Values('";
                        strSQL += _fushe + "','" + _color + "','" + _line + "','" + nbr + "','" + lot + "',N'";
                        strSQL += _ProductType + "',N'" + _Manufacturer + "',N'" + _Instrument + "',N'" + _TestDepartment + "',N'" + _Temperature + "',N'" + _Humidity + "',N'";
                        strSQL += _TestOperator + "',N'" + _TestDate + "','" + _TestType + "',N'" + _x + "',N'" + _y + "',N'" + _u + "',N'" + _v + "',N'" + _Tc + "',N'" + _Err + "',N'" + _Ra + "',N'" + _R1 + "',N'" + _R2 + "',N'";
                        strSQL += _R3 + "','" + _R4 + "','" + _R5 + "','" + _R6 + "','" + _R7 + "','" + _R8 + "','" + _R9 + "','" + _R10 + "','" + _R11 + "','" + _R12 + "','";
                        strSQL += _R13 + "',N'" + _R14 + "',N'" + _PeakWave + "',N'" + _HalfWave + "',N'" + _RedRatio + "',N'" + _Flux + "',N'" + _Efficiency + "',N'" + _U + "',N'" + _I + "',N'" + _P + "',N'" + _PF + "'," + person + ",GetDate(), '" + tcp.ToString() + "') ";
                    }

                    //excut database
                    if (strSQL == "")
                    {
                        msg = "没有符合条件的记录";
                        return;
                    }

                    try
                    {
                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL);
                    }
                    catch (Exception ee)
                    {
                        msg = "写入数据库时失败!原因:" + ee.ToString();
                        return;
                    }
                    finally
                    {
                        File.Delete(strFullName);
                    }

                    msg = "成功写入数据库";
                }
                else
                    msg = "请确保将要被导入的Excel文件中的工作表名称不包含汉字";
            }
        }
        #endregion

        #region 样品等光电色导入
        public void FluxImportTxt(int id,string hour,string way, int person, ref string msg)
        {
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();

            if (File.Exists(strFullName))
            {
                string strLine;
                StreamReader strReader = new StreamReader(strFullName, Encoding.Default);

                //line 1-8
                string _ProductType;
                string _Manufacturer;
                string _Instrument;
                string _TestDepartment;
                string _Temperature;
                string _Humidity;
                string _TestOperator;
                string _TestDate;
                string _TestType = hour.ToString() + "h/" + way;

                try
                {
                    strLine = strReader.ReadLine().Trim();
                    _ProductType = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Manufacturer = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Instrument = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestDepartment = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Temperature = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Humidity = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestOperator = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestDate = strLine.Substring(strLine.IndexOf(':') + 1).Trim();
                }
                catch
                {
                    msg = "请检查文件的完整性!位置: Line 1--8";
                    return;
                }
                //line 9
                strReader.ReadLine();

                //line 10
                string _x;
                string _y;
                string _u;
                string _v;
                string[] strArray10 = new string[4];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");

                try
                {
                    strArray10 = strLine.Split(' ');
                    _x = strArray10[0].Split('=')[1].Trim();
                    _y = strArray10[1].Split('=')[1].Trim();
                    _u = strArray10[2].Split('=')[1].Trim();
                    _v = strArray10[3].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 10";
                    return;
                }

                //line 11
                string _Tc;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Tc = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 11";
                    return;
                }

                //line 12
                string _Err;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Err = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 12";
                    return;
                }

                //line 13
                string _Ra;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Ra = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 13";
                    return;
                }

                //line 14
                string _R1;
                string _R2;
                string _R3;
                string _R4;
                string _R5;
                string _R6;
                string _R7;
                string _R8;
                string _R9;
                string _R10;
                string _R11;
                string _R12;
                string _R13;
                string _R14;
                string[] strArray14 = new string[14];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray14 = strLine.Split(' ');
                    _R1 = strArray14[0].Split('=')[1].Trim();
                    _R2 = strArray14[1].Split('=')[1].Trim();
                    _R3 = strArray14[2].Split('=')[1].Trim();
                    _R4 = strArray14[3].Split('=')[1].Trim();
                    _R5 = strArray14[4].Split('=')[1].Trim();
                    _R6 = strArray14[5].Split('=')[1].Trim();
                    _R7 = strArray14[6].Split('=')[1].Trim();
                    _R8 = strArray14[7].Split('=')[1].Trim();
                    _R9 = strArray14[8].Split('=')[1].Trim();
                    _R10 = strArray14[9].Split('=')[1].Trim();
                    _R11 = strArray14[10].Split('=')[1].Trim();
                    _R12 = strArray14[11].Split('=')[1].Trim();
                    _R13 = strArray14[12].Split('=')[1].Trim();
                    _R14 = strArray14[13].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 14";
                    return;
                }

                //line 15
                string _PeakWave;
                string _HalfWave;
                string[] strArray15 = new string[2];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray15 = strLine.Split(new string[] { "nm" }, StringSplitOptions.RemoveEmptyEntries);
                    _PeakWave = strArray15[0].Split('=')[1].Trim() + "nm";
                    _HalfWave = strArray15[1].Split('=')[1].Trim() + "nm";
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 15";
                    return;
                }

                //line 16
                string _RedRatio;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _RedRatio = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 16";
                    return;
                }

                //line 17
                string _Flux;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _Flux = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 17";
                    return;
                }

                //line 18
                string _Efficiency;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _Efficiency = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 18";
                    return;
                }
                //line 19
                string _U;
                string _I;
                string _P;
                string _PF;
                string[] strArray19 = new string[4];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray19 = strLine.Split(' ');
                    _U = strArray19[0].Split('=')[1].Trim();
                    _I = strArray19[1].Split('=')[1].Trim();
                    _P = strArray19[2].Split('=')[1].Trim();
                    _PF = strArray19[3].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 19";
                    return;
                }
                //now generate sql
                string strSQL = "Insert Into qc_flux_det(fl_id,";
                strSQL += "ProductType,Manufacturer,Instrument,TestDepartment,Temperature,Humidity, ";
                strSQL += "TestOperator,TestDate,TestType,x,y,u,v,Tc,Err,Ra,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,";
                strSQL += "R13,R14,PeakWave,HalfWave,RedRatio,Flux,Efficiency,U1,I1,P1,PF1,createBy,createDate)Values(";
                strSQL += id.ToString() + ",N'";
                strSQL += _ProductType + "',N'" + _Manufacturer + "',N'" + _Instrument + "',N'" + _TestDepartment + "',N'" + _Temperature + "',N'" + _Humidity + "',N'";
                strSQL += _TestOperator + "',N'" + _TestDate + "',N'" + _TestType + "',N'" + _x + "',N'" + _y + "',N'" + _u + "',N'" + _v + "',N'" + _Tc + "',N'" + _Err + "',N'" + _Ra + "',N'" + _R1 + "',N'" + _R2 + "',N'";
                strSQL += _R3 + "',N'" + _R4 + "',N'" + _R5 + "',N'" + _R6 + "',N'" + _R7 + "',N'" + _R8 + "',N'" + _R9 + "',N'" + _R10 + "',N'" + _R11 + "',N'" + _R12 + "',N'";
                strSQL += _R13 + "',N'" + _R14 + "',N'" + _PeakWave + "',N'" + _HalfWave + "',N'" + _RedRatio + "',N'" + _Flux + "',N'" + _Efficiency + "',N'" + _U + "',N'" + _I + "',N'" + _P + "',N'" + _PF + "'," + person + ",GetDate())";


                //execut database
                adamClass chk = new adamClass();
                try
                {
                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL);
                }
                catch (Exception ee)
                {
                    msg = "写入数据库时失败!原因:" + ee.ToString();
                    return;
                }
                finally
                {
                    strReader.Dispose();
                    File.Delete(strFullName);
                }

                msg = "成功写入数据库";

                strReader.Dispose();
            }
            else
            {
                msg = "给定的文件不存在!";
            }
            return;
        }
        public void FluxImportExcel(int id, int person, ref string msg)
        {
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();
            adamClass chk = new adamClass();
            string strSQL = "";

            string _ProductType;
            string _Manufacturer;
            string _Instrument;
            string _TestDepartment;
            string _Temperature;
            string _Humidity;
            string _TestOperator;
            string _TestDate;
            string _TestType;
            string _x;
            string _y;
            string _u;
            string _v;
            string _Tc;
            string _Err;
            string _Ra;
            string _R1;
            string _R2;
            string _R3;
            string _R4;
            string _R5;
            string _R6;
            string _R7;
            string _R8;
            string _R9;
            string _R10;
            string _R11;
            string _R12;
            string _R13;
            string _R14;
            string _PeakWave;
            string _HalfWave;
            string _RedRatio;
            string _Flux;
            string _Efficiency;
            string _U;
            string _I;
            string _P;
            string _PF;
            string _DUV;

            if (File.Exists(strFullName))
            {
                DataTable dst;
                try
                {
                    BasePage nw = new BasePage();
                    dst = nw.GetExcelContents(strFullName);
                }
                catch
                {
                    msg = "获取数据失败,请和管理员联系";
                    return;
                }
               
                if (dst.Rows.Count > 0)
                {
                    for (int i = 0; i < dst.Rows.Count; i++)
                    {
                        _ProductType = dst.Rows[i][1].ToString().Trim();

                        if (_ProductType.ToString().Trim() == string.Empty)
                            continue;

                        _Manufacturer = "YQL";
                        _Instrument = "PMS-50";
                        _TestDepartment = "测试中心";
                        _Temperature = "25℃";
                        _Humidity = "60%";
                        _TestOperator = "测试001";
                        _TestDate = dst.Rows[i][2].ToString().Trim();
                        _TestType = dst.Rows[i][3].ToString().Trim();
                        _x = dst.Rows[i][4].ToString().Trim();
                        _y = dst.Rows[i][5].ToString().Trim();
                        _u = dst.Rows[i][6].ToString().Trim();
                        _v = dst.Rows[i][7].ToString().Trim();
                        _Tc = dst.Rows[i][8].ToString().Trim();
                        _Err = dst.Rows[i][9].ToString().Trim();
                        _Ra = dst.Rows[i][21].ToString().Trim();
                        _R1 = "";
                        _R2 = "";
                        _R3 = "";
                        _R4 = "";
                        _R5 = "";
                        _R6 = "";
                        _R7 = "";
                        _R8 = "";
                        if (dst.Columns.Count >= 23)
                        {
                            _R9 = dst.Rows[i][22].ToString().Trim();
                        }
                        else
                        {
                            _R9 = "";
                        }
                        _R10 = "";
                        _R11 = "";
                        _R12 = "";
                        _R13 = "";
                        _R14 = "";
                        _PeakWave = dst.Rows[i][19].ToString().Trim();
                        _HalfWave = dst.Rows[i][20].ToString().Trim();
                        _RedRatio = dst.Rows[i][17].ToString().Trim();
                        _Flux = dst.Rows[i][10].ToString().Trim();
                        _Efficiency = dst.Rows[i][11].ToString().Trim();
                        _U = dst.Rows[i][13].ToString().Trim();
                        _I = dst.Rows[i][14].ToString().Trim();
                        _P = dst.Rows[i][15].ToString().Trim();
                        _PF = dst.Rows[i][16].ToString().Trim();
                        _DUV = dst.Rows[i][23].ToString().Trim();

                        strSQL += "  Insert Into qc_flux_det(fl_id,";
                        strSQL += "ProductType,Manufacturer,Instrument,TestDepartment,Temperature,Humidity, ";
                        strSQL += "TestOperator,TestDate,TestType,x,y,u,v,Tc,Err,Ra,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,";
                        strSQL += "R13,R14,PeakWave,HalfWave,RedRatio,Flux,Efficiency,U1,I1,P1,PF1,DUV,CreateBy,CreateDate)Values(";
                        strSQL += id.ToString() + ",N'";
                        strSQL += _ProductType + "',N'" + _Manufacturer + "',N'" + _Instrument + "',N'" + _TestDepartment + "',N'" + _Temperature + "',N'" + _Humidity + "',N'";
                        strSQL += _TestOperator + "',N'" + _TestDate + "',N'" + _TestType + "',N'" + _x + "',N'" + _y + "',N'" + _u + "',N'" + _v + "',N'" + _Tc + "',N'" + _Err + "',N'" + _Ra + "',N'" + _R1 + "',N'" + _R2 + "',N'";
                        strSQL += _R3 + "','" + _R4 + "','" + _R5 + "','" + _R6 + "','" + _R7 + "','" + _R8 + "','" + _R9 + "','" + _R10 + "','" + _R11 + "','" + _R12 + "','";
                        strSQL += _R13 + "',N'" + _R14 + "',N'" + _PeakWave + "',N'" + _HalfWave + "',N'" + _RedRatio + "',N'" + _Flux + "',N'" + _Efficiency + "',N'" + _U + "',N'" + _I + "',N'" + _P + "',N'" + _PF + "',N'" + _DUV + "'," + person + ",GetDate())   ";
                    }

                    //excut database
                    if (strSQL == "")
                    {
                        msg = "没有符合条件的记录";
                        return;
                    }

                    try
                    {
                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL);
                    }
                    catch (Exception ee)
                    {
                        msg = "写入数据库时失败!原因:" + ee.ToString();
                        return;
                    }
                    finally 
                    {
                        File.Delete(strFullName);
                    }

                    msg = "成功写入数据库";
                }
                else
                    msg = "请确保将要被导入的Excel文件中的工作表名称不包含汉字";
            }
        }
        #endregion

        #region 整月光电色数据导入

        public void FluxImportMonth(int person, ref string msg)
        {
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();

            if (File.Exists(strFullName))
            {
                string strLine;
                StreamReader strReader = new StreamReader(strFullName, Encoding.Default);

                //line 1-8
                string _ProductType;
                string _Manufacturer;
                string _Instrument;
                string _TestDepartment;
                string _Temperature;
                string _Humidity;
                string _TestOperator;
                string _TestDate;

                try
                {
                    strLine = strReader.ReadLine().Trim();
                    _ProductType = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Manufacturer = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Instrument = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestDepartment = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Temperature = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _Humidity = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestOperator = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    strLine = strReader.ReadLine().Trim();
                    _TestDate = strLine.Substring(strLine.IndexOf(':') + 1).Trim();
                }
                catch
                {
                    msg = "请检查文件的完整性!位置: Line 1--8";
                    return;
                }
                //line 9
                strReader.ReadLine();

                //line 10
                string _x;
                string _y;
                string _u;
                string _v;
                string[] strArray10 = new string[4];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");

                try
                {
                    strArray10 = strLine.Split(' ');
                    _x = strArray10[0].Split('=')[1].Trim();
                    _y = strArray10[1].Split('=')[1].Trim();
                    _u = strArray10[2].Split('=')[1].Trim();
                    _v = strArray10[3].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 10";
                    return;
                }

                //line 11
                string _Tc;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Tc = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 11";
                    return;
                }

                //line 12
                string _Err;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Err = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 12";
                    return;
                }

                //line 13
                string _Ra;

                strLine = strReader.ReadLine().Trim();
                try
                {
                    _Ra = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 13";
                    return;
                }

                //line 14
                string _R1;
                string _R2;
                string _R3;
                string _R4;
                string _R5;
                string _R6;
                string _R7;
                string _R8;
                string _R9;
                string _R10;
                string _R11;
                string _R12;
                string _R13;
                string _R14;
                string[] strArray14 = new string[14];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray14 = strLine.Split(' ');
                    _R1 = strArray14[0].Split('=')[1].Trim();
                    _R2 = strArray14[1].Split('=')[1].Trim();
                    _R3 = strArray14[2].Split('=')[1].Trim();
                    _R4 = strArray14[3].Split('=')[1].Trim();
                    _R5 = strArray14[4].Split('=')[1].Trim();
                    _R6 = strArray14[5].Split('=')[1].Trim();
                    _R7 = strArray14[6].Split('=')[1].Trim();
                    _R8 = strArray14[7].Split('=')[1].Trim();
                    _R9 = strArray14[8].Split('=')[1].Trim();
                    _R10 = strArray14[9].Split('=')[1].Trim();
                    _R11 = strArray14[10].Split('=')[1].Trim();
                    _R12 = strArray14[11].Split('=')[1].Trim();
                    _R13 = strArray14[12].Split('=')[1].Trim();
                    _R14 = strArray14[13].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 14";
                    return;
                }

                //line 15
                string _PeakWave;
                string _HalfWave;
                string[] strArray15 = new string[2];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray15 = strLine.Split(new string[] { "nm" }, StringSplitOptions.RemoveEmptyEntries);
                    _PeakWave = strArray15[0].Split('=')[1].Trim() + "nm";
                    _HalfWave = strArray15[1].Split('=')[1].Trim() + "nm";
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 15";
                    return;
                }

                //line 16
                string _RedRatio;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _RedRatio = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 16";
                    return;
                }

                //line 17
                string _Flux;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _Flux = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 17";
                    return;
                }

                //line 18
                string _Efficiency;

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    _Efficiency = strLine.Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 18";
                    return;
                }
                //line 19
                string _U;
                string _I;
                string _P;
                string _PF;
                string[] strArray19 = new string[4];

                strLine = strReader.ReadLine().Trim();
                strLine = Regex.Replace(strLine, @"=[ ]+", @"=");
                try
                {
                    strArray19 = strLine.Split(' ');
                    _U = strArray19[0].Split('=')[1].Trim();
                    _I = strArray19[1].Split('=')[1].Trim();
                    _P = strArray19[2].Split('=')[1].Trim();
                    _PF = strArray19[3].Split('=')[1].Trim();
                }
                catch
                {
                    msg = "文件格式不对!位置: Line 19";
                    return;
                }
                //now generate sql
                string strSQL = "Insert Into qc_flux_det(";
                strSQL += "ProductType,Manufacturer,Instrument,TestDepartment,Temperature,Humidity, ";
                strSQL += "TestOperator,TestDate,x,y,u,v,Tc,Err,Ra,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,";
                strSQL += "R13,R14,PeakWave,HalfWave,RedRatio,Flux,Efficiency,U1,I1,P1,PF1,createBy,createDate)Values(";
                strSQL += "N'";
                strSQL += _ProductType + "',N'" + _Manufacturer + "',N'" + _Instrument + "',N'" + _TestDepartment + "',N'" + _Temperature + "',N'" + _Humidity + "',N'";
                strSQL += _TestOperator + "',N'" + _TestDate + "',N'" + _x + "',N'" + _y + "',N'" + _u + "',N'" + _v + "',N'" + _Tc + "',N'" + _Err + "',N'" + _Ra + "',N'" + _R1 + "',N'" + _R2 + "',N'";
                strSQL += _R3 + "',N'" + _R4 + "',N'" + _R5 + "',N'" + _R6 + "',N'" + _R7 + "',N'" + _R8 + "',N'" + _R9 + "',N'" + _R10 + "',N'" + _R11 + "',N'" + _R12 + "',N'";
                strSQL += _R13 + "',N'" + _R14 + "',N'" + _PeakWave + "',N'" + _HalfWave + "',N'" + _RedRatio + "',N'" + _Flux + "',N'" + _Efficiency + "',N'" + _U + "',N'" + _I + "',N'" + _P + "',N'" + _PF + "'," + person + ",GetDate())";


                //execut database
                adamClass chk = new adamClass();
                try
                {
                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL);
                }
                catch (Exception ee)
                {
                    msg = "写入数据库时失败!原因:" + ee.ToString();
                    return;
                }
                finally
                {
                    strReader.Dispose();
                    File.Delete(strFullName);
                }

                msg = "成功写入数据库";

                strReader.Dispose();
            }
            else
            {
                msg = "给定的文件不存在!";
            }
            return;
        }

        #endregion

        #region 读取指定的Excel文件，并存入数据库 import an excel file to the db
        /// <summary>
        /// import an excel file to the db读取指定的Excel文件，并存入数据库
        /// </summary>
        /// <param name="msg"></param>
        public void ImportExcel(string line, string receiver, int hour, int way,string part,int person, ref string msg) 
        {
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();
            adamClass chk = new adamClass();
            string strSQL = "";

            string _ProductType;
            string _Manufacturer;
            string _Instrument;
            string _TestDepartment;
            string _Temperature;
            string _Humidity;
            string _TestOperator;
            string _TestDate;
            string _TestType;
            string _x;
            string _y;
            string _u;
            string _v;
            string _Tc;
            string _Err;
            string _Ra;
            string _R1;
            string _R2;
            string _R3;
            string _R4;
            string _R5;
            string _R6;
            string _R7;
            string _R8;
            string _R9;
            string _R10;
            string _R11;
            string _R12;
            string _R13;
            string _R14;
            string _PeakWave;
            string _HalfWave;
            string _RedRatio;
            string _Flux;
            string _Efficiency;
            string _U;
            string _I;
            string _P;
            string _PF;

            if (File.Exists(strFullName))
            {
                DataSet dst;
                try
                {
                    dst = chk.getExcelContents(strFullName);
                }
                catch
                {
                    msg = "获取数据失败，请和管理员联系";
                    return;
                }
                if (dst.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                    {
                        _ProductType = dst.Tables[0].Rows[i][1].ToString().Trim();

                        if (part != "" && part != _ProductType)
                        {
                            continue;
                        }

                        _Manufacturer = "YQL";
                        _Instrument = "PMS-50";
                        _TestDepartment = "测试中心";
                        _Temperature = "25℃";
                        _Humidity = "60%";
                        _TestOperator = "测试001";
                        _TestDate = dst.Tables[0].Rows[i][2].ToString().Trim();
                        _TestType = dst.Tables[0].Rows[i][3].ToString().Trim();
                        _x = dst.Tables[0].Rows[i][4].ToString().Trim();
                        _y = dst.Tables[0].Rows[i][5].ToString().Trim();
                        _u = dst.Tables[0].Rows[i][6].ToString().Trim();
                        _v = dst.Tables[0].Rows[i][7].ToString().Trim();
                        _Tc = dst.Tables[0].Rows[i][8].ToString().Trim();
                        _Err = dst.Tables[0].Rows[i][9].ToString().Trim();
                        _Ra = dst.Tables[0].Rows[i][21].ToString().Trim();
                        _R1 = "";
                        _R2 = "";
                        _R3 = "";
                        _R4 = "";
                        _R5 = "";
                        _R6 = "";
                        _R7 = "";
                        _R8 = "";
                        _R9 = "";
                        _R10 = "";
                        _R11 = "";
                        _R12 = "";
                        _R13 = "";
                        _R14 = "";
                        _PeakWave = dst.Tables[0].Rows[i][19].ToString().Trim();
                        _HalfWave = dst.Tables[0].Rows[i][20].ToString().Trim();
                        _RedRatio = dst.Tables[0].Rows[i][17].ToString().Trim();
                        _Flux = dst.Tables[0].Rows[i][10].ToString().Trim();
                        _Efficiency = dst.Tables[0].Rows[i][11].ToString().Trim();
                        _U = dst.Tables[0].Rows[i][13].ToString().Trim();
                        _I = dst.Tables[0].Rows[i][14].ToString().Trim();
                        _P = dst.Tables[0].Rows[i][15].ToString().Trim();
                        _PF = dst.Tables[0].Rows[i][16].ToString().Trim();

                        strSQL += "  Insert Into luminousFlux(";
                        strSQL += "prh_line,prh_receiver,TestType,";
                        strSQL += "ProductType,Manufacturer,Instrument,TestDepartment,Temperature,Humidity, ";
                        strSQL += "TestOperator,TestDate,x,y,u,v,Tc,Err,Ra,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,";
                        strSQL += "R13,R14,PeakWave,HalfWave,RedRatio,Flux,Efficiency,U1,I1,P1,PF1,CreateBy,CreateDate)Values('";
                        strSQL += line + "','" + receiver + "',N'" + _TestType + "',N'";
                        strSQL += _ProductType + "',N'" + _Manufacturer + "',N'" + _Instrument + "',N'" + _TestDepartment + "',N'" + _Temperature + "',N'" + _Humidity + "',N'";
                        strSQL += _TestOperator + "',N'" + _TestDate + "',N'" + _x + "',N'" + _y + "',N'" + _u + "',N'" + _v + "',N'" + _Tc + "',N'" + _Err + "',N'" + _Ra + "',N'" + _R1 + "',N'" + _R2 + "',N'";
                        strSQL += _R3 + "','" + _R4 + "','" + _R5 + "','" + _R6 + "','" + _R7 + "','" + _R8 + "','" + _R9 + "','" + _R10 + "','" + _R11 + "','" + _R12 + "','";
                        strSQL += _R13 + "',N'" + _R14 + "',N'" + _PeakWave + "',N'" + _HalfWave + "',N'" + _RedRatio + "',N'" + _Flux + "',N'" + _Efficiency + "',N'" + _U + "',N'" + _I + "',N'" + _P + "',N'" + _PF + "'," + person + ",GetDate())   ";
                    }

                    //excut database
                    if (strSQL == "")
                    {
                        msg = "没有符合条件的记录";
                        return;
                    }

                    try
                    {
                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL);
                    }
                    catch (Exception ee)
                    {
                        msg = "写入数据库时失败!原因:" + ee.ToString();
                        return;
                    }
                    finally
                    {
                        File.Delete(strFullName);
                    }

                    msg = "成功写入数据库";
                }
                else
                    msg = "请确保将要被导入的Excel文件中的工作表名称不包含汉字";
            }
        }
        /// <summary>
        /// 重载。计算零件箱数、体积和重量
        /// </summary>
        /// <param name="person"></param>
        /// <param name="msg"></param>
        public void ImportExcel(int person, ref string msg)
        {
            adamClass chk = new adamClass();

            string strFullName = _FolderPath + "\\" + _FileName + ".xls";
            string strDelSQL = "";
            string strSQL = "";

            string _pt_part;
            string _pt_number;

            if (File.Exists(strFullName))
            {
                strDelSQL = "delete from pt_mstr_excel where uid = " + person;
                try
                {
                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strDelSQL);
                }
                catch (Exception ee)
                {
                    msg = "原数据库中的数据删除失败!原因:" + ee.ToString();
                    return;
                }

                DataSet dst;
                try
                {
                    dst = GetExcelContents(strFullName);
                }
                catch
                {
                    msg = "获取数据失败,请和管理员联系";
                    return;
                }

                if (dst == null)
                {
                    msg = "读取Excel数据失败，请按模板格式输入";
                    return;
                }

                if (dst.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                    {
                        _pt_part = dst.Tables[0].Rows[i][0].ToString().Trim();

                        if (_pt_part.ToString().Trim() == string.Empty)
                            continue;

                        _pt_part = dst.Tables[0].Rows[i][0].ToString().Trim();
                        _pt_number = dst.Tables[0].Rows[i][1].ToString().Trim();

                        strSQL += "Insert Into pt_mstr_excel(id ,pt_part,";
                        strSQL += "pt_number,uid) Values(";
                        strSQL += (i + 1) + ",N'" + _pt_part + "',N'" + _pt_number + "',N'" + person + "')";

                    }
                    //excut database
                    if (strSQL == "")
                    {
                        msg = "没有符合条件的记录";
                        return;
                    }

                    try
                    {
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL);
                    }
                    catch (Exception ee)
                    {
                        msg = "写入数据库时失败!原因:" + ee.ToString();
                        return;
                    }
                    finally
                    {
                        File.Delete(strFullName);
                    }

                    msg = "成功写入数据库";
                }
                else
                    msg = "请确保将要被导入的Excel文件中的工作表名称不包含汉字";
            }
        }
        #endregion

        #region Import Excel Data To a DataSet 
        public DataSet GetExcelContents(string filePath)
        {
            string conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 8.0;" + "Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString() ;

            try
            {
                OleDbConnection excelConnection = new OleDbConnection(conString);

                excelConnection.Open();

                DataTable dtSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });

                String excelSchemaName = dtSchema.Rows[0].ItemArray[2].ToString();

                OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter("SELECT * FROM [" + excelSchemaName + "]", excelConnection);

                DataSet ds = new DataSet();

                excelDataAdapter.AcceptChangesDuringFill = false;

                excelDataAdapter.Fill(ds);

                excelConnection.Close();

                return ds;
            }
            catch(Exception ee)
            {
                string msg = ee.ToString();
            }
            return null;
        }
        #endregion

        #region Budget的Excel导入

        public void ImportBudgetExcel(string uID,ref int msg,ref DataTable dt) 
        {
            string strFullName = _FolderPath + "\\" + _FileName + "." + _FileSection.ToString().ToLower();
            adamClass chk = new adamClass();
            string Success = "";

            string _id;
            string _master;
            string _dept;
            string _acc;
            string _sub;
            string _cc;
            string _project;
            string _period;
            string _budget;
            string _year;
            string _per;

            if (File.Exists(strFullName))
            {
                DataSet dst;
                try
                {
                    dst = GetExcelContents(strFullName);
                }
                catch 
                {
                    msg = -1;
                    return;
                }

                DataTable dtFailure = new DataTable("Failure");
                dtFailure.Columns.Add("ID", typeof(System.String));
                dtFailure.Columns.Add("主管", typeof(System.String));
                dtFailure.Columns.Add("部门", typeof(System.String));
                dtFailure.Columns.Add("账户", typeof(System.String));
                dtFailure.Columns.Add("分账户", typeof(System.String));
                dtFailure.Columns.Add("成本中心", typeof(System.String));
                dtFailure.Columns.Add("项目", typeof(System.String));
                dtFailure.Columns.Add("期间", typeof(System.String));
                dtFailure.Columns.Add("预测", typeof(System.String));
                dtFailure.Columns.Add("错误信息", typeof(System.String));

                if (dst.Tables[0].Rows.Count > 0)
                {
                    Success += " DECLARE @count int ";
                    Success += " CREATE TABLE #temp(bg_master nvarchar(50),bg_masterC nvarchar(50), bg_dept nvarchar(50), bg_acc nvarchar(50), bg_sub nvarchar(50),bg_cc nvarchar(50), bg_project nvarchar(50), bg_year nvarchar(50), bg_per nvarchar(50), bg_budget nvarchar(50),Error nvarchar(100)) ";

                    _id = dst.Tables[0].Rows[0][0].ToString().Trim();
                    _master = dst.Tables[0].Rows[0][1].ToString().Trim();
                    _dept = dst.Tables[0].Rows[0][2].ToString().Trim();
                    _acc = dst.Tables[0].Rows[0][3].ToString().Trim();
                    _sub = dst.Tables[0].Rows[0][4].ToString().Trim();
                    _cc = dst.Tables[0].Rows[0][5].ToString().Trim();
                    _project = dst.Tables[0].Rows[0][6].ToString().Trim();
                    _period = dst.Tables[0].Rows[0][7].ToString().Trim();
                    _budget = dst.Tables[0].Rows[0][8].ToString().Trim();

                    if (_id != "ID" || _master != "主管" || _dept != "部门" || _acc != "账户" || _sub != "分账户" || _cc != "成本中心" || _project != "项目" || _period != "期间" || _budget != "预测") 
                    {
                        msg = -2;
                        return;
                    }

                    for (int i = 1; i < dst.Tables[0].Rows.Count; i++)
                    {
                        _id = dst.Tables[0].Rows[i][0].ToString().Trim();
                        _master = dst.Tables[0].Rows[i][1].ToString().Trim();
                        _dept = dst.Tables[0].Rows[i][2].ToString().Trim();
                        _acc = dst.Tables[0].Rows[i][3].ToString().Trim();
                        _sub = dst.Tables[0].Rows[i][4].ToString().Trim();
                        _cc = dst.Tables[0].Rows[i][5].ToString().Trim();
                        _project = dst.Tables[0].Rows[i][6].ToString().Trim();
                        _period = dst.Tables[0].Rows[i][7].ToString().Trim();
                        _budget = dst.Tables[0].Rows[i][8].ToString().Trim();


                        if (_id == string.Empty || _master == string.Empty || _dept == string.Empty || _acc == string.Empty || _sub == string.Empty || _period == string.Empty || _period.Length != 6)
                        {
                            DataRow row = dtFailure.NewRow();

                            row[0] = _id;
                            row[1] = _master;
                            row[2] = _dept;
                            row[3] = _acc;
                            row[4] = _sub;
                            row[5] = _cc;
                            row[6] = _project;
                            row[7] = _period;
                            row[8] = _budget;
                            row[9] = "";
                            if (_id == string.Empty)
                                row[9] += "ID不能为空; ";

                            if (_master == string.Empty)
                                row[9] += "主管不能为空; ";

                            if (_dept == string.Empty)
                                row[9] += "部门不能为空; ";

                            if (_acc == string.Empty)
                                row[9] += "账户不能为空; ";

                            if (_sub == string.Empty)
                                row[9] += "分账户不能为空; ";

                            if (_period == string.Empty)
                                row[9] += "期间不能为空; ";

                            if (_period.Length != 6)
                                row[9] += "期间必须为6位数字; ";


                            dtFailure.Rows.Add(row);

                            continue;
                        }

                        _year = _period.Substring(0, 4);
                        _per = _period.Substring(4, 2);

                        Regex reg = new Regex(@"^\d+(\.\d*)?$");
                        Match match = reg.Match(_budget);
                        if (!match.Success) 
                        {
                            DataRow row = dtFailure.NewRow();

                            row[0] = _id;
                            row[1] = _master;
                            row[2] = _dept;
                            row[3] = _acc;
                            row[4] = _sub;
                            row[5] = _cc;
                            row[6] = _project;
                            row[7] = _period;
                            row[8] = _budget;
                            row[9] = "预测结果必须为浮点数";

                            dtFailure.Rows.Add(row);

                            continue;
                        }

                        Success += " set @count = -1 ";
                        Success += " SELECT @count = count(userID) FROM Users WHERE userID = " + _id + " AND userNo =N'" + _master + "' ";
                        Success += " IF(@count < 1) ";
                        Success += " INSERT INTO #temp(bg_master,bg_masterC, bg_dept, bg_acc, bg_sub, bg_cc, bg_project, bg_year, bg_per, bg_budget,Error)Values(N'" + _id + "',N'" + _master + "',N'" + _dept + "',N'" + _acc + "',N'" + _sub + "',N'" + _cc + "',N'" + _project + "'," + _year + "," + _per + " ," + _budget + ",N'ID 或主管号错误') ";
                        Success += " ELSE ";
                        Success += " BEGIN ";
                        Success += " set @count = -1 ";
                        Success += " SELECT @count = count(cc_id) FROM bg_cc WHERE cc_master = N'" + _id + "' AND cc_code in (SELECT String FROM tcpc1.dbo.Split('" + _cc + "')) AND cc_dept = N'" + _dept + "' AND cc_modifier LIKE N'%" + uID + "%' ";
                        Success += " IF(@count < 1) ";
                        Success += " INSERT INTO #temp(bg_master,bg_masterC, bg_dept, bg_acc, bg_sub, bg_cc, bg_project, bg_year, bg_per, bg_budget,Error)Values(N'" + _id + "',N'" + _master + "',N'" + _dept + "',N'" + _acc + "',N'" + _sub + "',N'" + _cc + "',N'" + _project + "'," + _year + "," + _per + " ," + _budget + ",N'主管、成本中心或部门错误，或没有修改权限') ";
                        Success += " ELSE ";
                        Success += " BEGIN ";
                        Success += " set @count = -1 ";
                        Success += " SELECT @count = count(bg_master) FROM bg_mstr WHERE bg_master = N'" + _id + "' AND bg_masterC = '" + _master + "' AND bg_dept = N'" + _dept + "' AND bg_acc = N'" + _acc + "' AND bg_sub = N'" + _sub + "' AND bg_project = N'" + _project + "' ";
                        Success += " IF(@count < 1) ";
                        Success += " INSERT INTO #temp(bg_master,bg_masterC, bg_dept, bg_acc, bg_sub, bg_cc, bg_project, bg_year, bg_per, bg_budget,Error)Values(N'" + _id + "',N'" + _master + "',N'" + _dept + "',N'" + _acc + "',N'" + _sub + "',N'" + _cc + "',N'" + _project + "'," + _year + "," + _per + " ," + _budget + ",N'主管、部门、账户、分账户或者项目错误') ";
                        Success += " ELSE ";
                        Success += " BEGIN ";
                        Success += " SET @count = null ";
                        Success += " SELECT @count = bg_id FROM bg_mstr_bg WHERE bg_master = N'" + _id + "' AND bg_dept = N'" + _dept + "' AND bg_acc = N'" + _acc + "' AND bg_sub = N'" + _sub + "' AND bg_project = N'" + _project + "' AND bg_year = " + _year + " AND bg_per = " + _per + " "; 
	                    Success += " IF(@count is null) ";
                        Success += " INSERT INTO bg_mstr_bg(bg_master,bg_masterC, bg_dept, bg_acc, bg_sub, bg_cc, bg_project, bg_year, bg_per, bg_budget)Values(N'" + _id + "',N'" + _master + "',N'" + _dept + "',N'" + _acc + "',N'" + _sub + "',N'" + _cc + "',N'" + _project + "'," + _year + "," + _per + " ," + _budget + ") ";
                        Success += " ELSE ";
                        Success += " UPDATE bg_mstr_bg SET bg_master=N'" + _id + "', bg_dept=N'" + _dept + "', bg_acc=N'" + _acc + "', bg_sub=N'" + _sub + "', bg_project=N'" + _project + "', bg_year=" + _year + ", bg_per=" + _per + ", bg_budget=" + _budget + " WHERE bg_id = @count  ";
                        Success += " END ";
                        Success += " END ";
                        Success += " END ";
                    }

                    Success += " SELECT bg_master,bg_masterC, bg_dept, bg_acc, bg_sub, bg_cc, bg_project, bg_year+CASE WHEN bg_per < 10 THEN '0'+bg_per ELSE bg_per END bg_period, bg_budget,Error FROM #temp ";
                    Success += " DROP TABLE #temp ";
                    Success += " EXEC sp_bg_access ";

                    dt = dtFailure;

                    //excut database
                    try
                    {
                        DataSet ds;
                        ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, Success);

                        if (ds.Tables[0].Rows.Count > 0) 
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows) 
                            {
                                dt.Rows.Add(dr.ItemArray);
                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        msg = -1;// "写入数据库时失败!";
                        return;
                    }
                    finally
                    {
                        File.Delete(strFullName);
                    }

                    msg = 0;// "成功写入数据库";
                }
                else
                    msg = -1;
            } 
        }
        #endregion

        #region 导出数据
        /// <summary>
        /// 导出数据到Excel文件中
        /// </summary>
        /// <param name="dt">数据集</param>
        public static void ExportToExcel(DataTable dt)
        {
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=excel.xls");
            string colHeaders = "", ls_item = "";

            //定义表对象与行对象，同时用DataSet对其值进行初始化 
            DataRow[] myRow = dt.Select();
            int i = 0;
            int cl = dt.Columns.Count;

            //取得工作表各列标题，各标题之间以t分割，最后一个列标题后加回车符 
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                }
                else
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                }
            }
            HttpContext.Current.Response.Write(colHeaders);
            //向HTTP输出流中写入取得的数据信息 

            //逐行处理数据   
            foreach (DataRow row in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加n
                    {
                        ls_item += row[i].ToString() + "\n";
                    }
                    else
                    {
                        ls_item += row[i].ToString() + "\t";
                    }

                }
                HttpContext.Current.Response.Write(ls_item);
                ls_item = "";

            }
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();  
        }
        #endregion
    }
}
