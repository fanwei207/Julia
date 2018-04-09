using System;
using System.IO;//用于文件存取 
using System.Data;//用于数据访问 
using System.Drawing;//提供画GDI+图形的基本功能 
using System.Drawing.Text;//提供画GDI+图形的高级功能 
using System.Drawing.Drawing2D;//提供画高级二维，矢量图形功能 
using System.Drawing.Imaging;//提供画GDI+图形的高级功能 
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;


namespace TCPC.WebChart
{
    //坐标轴类型
    /// <summary>
    /// 坐标轴的类型
    /// </summary>
    public enum AxisType
    {
        /// <summary>
        /// X轴
        /// </summary>
        xAxis,
        /// <summary>
        /// Y轴
        /// </summary>
        yAxis
    }
    //Char类型
    /// <summary>
    /// Char类型
    /// </summary>
    public enum ChartType
    {
        /// <summary>
        /// 折线图
        /// </summary>
        Line,
        /// <summary>
        /// 柱状图
        /// </summary>
        ColumnStacked
    }
    //常用的画笔
    /// <summary>
    /// 常用的画笔集合
    /// </summary>
    public class TBrush
    {
        public static Brush GetBrush(Int32 i)
        {
            switch (i) 
            {
                case 1: return Brushes.DarkBlue;
                case 2: return Brushes.DarkRed;
                case 3: return Brushes.DarkGreen;
                case 4: return Brushes.Purple;
                case 5: return Brushes.Chocolate;
                case 6: return Brushes.Khaki;
                case 7: return Brushes.Silver;
                default: return Brushes.LightBlue;
            }
        }
    }
    //类Series
    /// <summary>
    /// Series类
    /// </summary>
    public class Series
    {
        /// <summary>
        /// 相邻柱形的宽
        /// </summary>
        private Int32 _Width;
        /// <summary>
        /// 图例
        /// </summary>
        private Legend _Legend;
        /// <summary>
        /// 数据组
        /// </summary>
        private String[] _Data;
        /// <summary>
        /// 图表类型
        /// </summary>
        private ChartType _chartType;
        /// <summary>
        /// 相邻柱形图间的距离
        /// </summary>
        private Int32 _gapWidth;
        /// <summary>
        /// 画笔颜色
        /// </summary>
        private Brush _Brush;
        //Series创建向导
        /// <summary>
        /// Series创建向导
        /// </summary>
        /// <param name="xAxis">x轴</param>
        /// <param name="yAxis">y轴</param>
        /// <param name="chartType">图表类型</param>
        public Series()
        {
            this._chartType = ChartType.ColumnStacked;
            this._gapWidth = 0;
            this._Brush = Brushes.Green;
            this._Legend = new Legend("图例");
            this._Width = 50;
        }
        //获取或设置柱形的宽
        /// <summary>
        /// 获取或设置柱形的宽
        /// </summary>
        public Int32 Width
        {
            get { return this._Width; }
            set { this._Width = value; }
        }
        //设置图表类型
        /// <summary>
        /// 设置图表类型
        /// </summary>
        public ChartType ChartType
        {
            get { return this._chartType; }
            set { this._chartType = value; }
        }
        //设置数据
        /// <summary>
        /// 设置数据
        /// </summary>
        public String[] Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }
        //设置柱形图相邻间的空隙
        /// <summary>
        /// 设置柱形图相邻间的空隙
        /// </summary>
        public Int32 GapWidth
        {
            get { return this._gapWidth; }
            set { this._gapWidth = value; }
        }
        //设置填充颜色
        /// <summary>
        /// 设置填充颜色
        /// </summary>
        public Brush Brush
        {
            get { return _Brush; }
            set { this._Brush = value; }
        }
        //设置或获取图例
        /// <summary>
        /// 设置或获取图例
        /// </summary>
        public Legend Legend
        {
            get { return this._Legend; }
            set { this._Legend = value; }
        }
    }
    //Series集合类
    /// <summary>
    /// Series集合类
    /// </summary>
    public class SeriesGroup
    {
        /// <summary>
        /// 长度每单位
        /// </summary>
        private Double _Unit;
        /// <summary>
        /// GRAPHIC中间变量
        /// </summary>
        private Graphics _Graphice;
        /// <summary>
        /// Series集合
        /// </summary>
        private Series[] _Group;
        /// <summary>
        /// 数据源
        /// </summary>
        private DataTable _source;
        //Series集合创建向导
        /// <summary>
        /// Series集合创建向导
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="graphice">画布</param>
        public SeriesGroup(DataTable source, ref Graphics graphice)
        {
            if (source == null || source.Columns.Count < 2 || source.Rows.Count < 1)
                throw new Exception("请确保数据源至少有两列且至少有一行数据");

            foreach (DataRow row in source.Rows)
            {
                Regex reg = new Regex(@"^[0-9]*$");

                for (int col = 1; col < source.Columns.Count; col++)
                {
                    if (!reg.IsMatch(row[col].ToString()))
                        throw new Exception("数据源不符合要求.必须是整数");
                }
            }

            //从第二列开始
            this._Group = new Series[source.Columns.Count - 1];

            for (int i = 0; i < this._Group.Length; i++)
            {
                this._Group[i] = new Series();
            }

            for (int col = 1; col < source.Columns.Count; col++)
            {
                this._Group[col - 1].Data = new String[source.Rows.Count];

                for (int row = 0; row < source.Rows.Count; row++)
                {
                    this._Group[col - 1].Data[row] = source.Rows[row][col].ToString();
                }

                this._Group[col - 1].Brush = TBrush.GetBrush(col);

                this._Group[col - 1].Legend.Text = source.Columns[col].ColumnName;
                this._Group[col - 1].Legend.Brush = this._Group[col - 1].Brush;
            }

            this._source = source;

            this._Graphice = graphice;
        }
        //集合个数
        /// <summary>
        /// 集合个数
        /// </summary>
        public Int32 SeriesCount
        {
            get { return this._Group.Length; }
        }
        //get series集合
        /// <summary>
        /// get series集合
        /// </summary>
        public Series[] Group 
        {
            get { return this._Group; }
            set { this._Group = value; }
        }
        //设置单位每长度
        /// <summary>
        /// 设置单位每长度
        /// </summary>
        public Double Unit
        {
            set { this._Unit = value; }
        }
        //画Series
        /// <summary>
        /// 画Series
        /// </summary>
        public void Paint()
        {
            Rectangle[] rectArray = new Rectangle[this._source.Rows.Count];

            //初始化
            for (int i = 0; i < rectArray.Length; i++)
            {
                rectArray[i] = new Rectangle(0, 0, 0, 0);
            }

            //ChartType.ColumnStacked组中，取最大的GapWidth。对ColumnStacked有效
            Int32 nWidth = 0;//柱形图宽度
            Int32 nGapWidth = 0;//相邻柱形图间隙

            foreach (Series series in this._Group)
            {
                if (series.ChartType == ChartType.ColumnStacked)
                {
                    if (nGapWidth < series.GapWidth)
                    {
                        nGapWidth = series.GapWidth;
                    }

                    if (nWidth < series.Width)
                    {
                        nWidth = series.Width;
                    }
                }
            }

            foreach (Series series in this._Group)
            {
                if (series.ChartType == ChartType.Line)//画折线图
                {
                    Point p1 = new Point(-100, 0);
                    Point p2 = new Point(0, 0);

                    foreach (String data in series.Data)
                    {
                        if (p1.X < 0)
                        {
                            p1.X = 0;
                            p1.Y = Convert.ToInt32(-Int32.Parse(data) * this._Unit);
                            p2.X = p1.X;
                            p2.Y = p1.Y;
                        }
                        else
                        {
                            p2.X += series.Width;
                            p2.Y = Convert.ToInt32(-Int32.Parse(data) * this._Unit);
                        }

                        this._Graphice.DrawLine(new Pen(series.Brush, 2), p1, p2);

                        p1.X = p2.X;
                        p1.Y = p2.Y;
                    }
                }
                else if (series.ChartType == ChartType.ColumnStacked)//画柱状图
                {
                    for (int i = 0; i < series.Data.Length; i++)
                    {
                        rectArray[i].X = nWidth * i - (nWidth - nGapWidth) / 2;
                        rectArray[i].Height = Convert.ToInt32(Int32.Parse(series.Data[i]) * this._Unit);
                        rectArray[i].Y -= rectArray[i].Height;
                        rectArray[i].Width = nWidth - nGapWidth;

                        if (rectArray[i].X < 0)
                        {
                            rectArray[i].X = 0;
                            rectArray[i].Width = rectArray[i].Width / 2;
                        }

                        if (rectArray[i].X + rectArray[i].Width > (nWidth + nGapWidth) * (this._source.Rows.Count - 1))
                        {
                            rectArray[i].Width = rectArray[i].Width / 2;
                        }

                        this._Graphice.FillRectangle(series.Brush, rectArray[i]);
                        this._Graphice.DrawRectangle(new Pen(Brushes.Black), rectArray[i]);
                    }
                }
            }
        }
    }
    //Axis类
    /// <summary>
    /// Axis类
    /// </summary>
    public class Axis
    {
        #region 公共属性
        /// <summary>
        /// 原点
        /// </summary>
        private Point _pOrigin;
        /// <summary>
        /// 坐标轴标题
        /// </summary>
        private String _Title;
        /// <summary>
        /// 坐标值
        /// </summary>
        private String[] _Values;
        /// <summary>
        /// 步长
        /// </summary>
        private Int32 _unitLen;
        /// <summary>
        /// 单位数量
        /// </summary>
        private Int32 _unitVol;
        /// <summary>
        /// 是否要箭头
        /// </summary>
        private Boolean _hasArrow;
        /// <summary>
        /// 是否显示明细刻度
        /// </summary>
        private Boolean _showDeitals;
        /// <summary>
        /// 坐标类型
        /// </summary>
        private AxisType _axisType;
        /// <summary>
        /// 画笔
        /// </summary>
        private Brush _myBrush;
        /// <summary>
        /// 字体倾斜度
        /// </summary>
        private Int32 _Angle;
        /// <summary>
        /// 画布
        /// </summary>
        private Graphics _Graphice;
        #endregion

        #region 公共方法
       /// <summary>
       /// 设置坐标轴标题
       /// </summary>
        public String Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }
        /// <summary>
        /// 设置坐标值
        /// </summary>
        public String[] Values
        {
            get { return this._Values; }
            set { this._Values = value; }
        }
        /// <summary>
        /// 设置步长
        /// </summary>
        public Int32 UnitLen
        {
            get { return this._unitLen; }
            set { this._unitLen = value; }
        }
        /// <summary>
        /// 获取或设置单位数量
        /// </summary>
        public Int32 UnitVol
        {
            get { return this._unitVol; }
            set { this._unitVol = value; }
        }
        /// <summary>
        /// 设置是否需要显示箭头
        /// </summary>
        public Boolean HasArrow
        {
            get { return this._hasArrow; }
            set { this._hasArrow = value; }
        }
        /// <summary>
        /// 设置是否需要显示更详细的刻度
        /// </summary>
        public Boolean ShowDeitals
        {
            get { return this._showDeitals; }
            set { this._showDeitals = value; }
        }
        /// <summary>
        /// 设置画笔
        /// </summary>
        public Brush Brush 
        {
            get { return this._myBrush; }
            set { this._myBrush = value; }
        }
        /// <summary>
        /// 设置字体倾斜度
        /// </summary>
        public Int32 Angle 
        {
            get { return this._Angle; }
            set { this._Angle = value; }
        }
        /// <summary>
        /// 坐标轴长度
        /// </summary>
        /// <returns></returns>
        public Int32 AxisLength
        {
            get
            {
                return (this._Values.Length - 1) * this._unitLen;
            }
        }
        /// <summary>
        /// 坐标轴创建向导
        /// </summary>
        /// <param name="pOrigin">坐标轴标原点</param>
        /// <param name="title">坐标轴标题</param>
        /// <param name="values">坐标轴值</param>
        /// <param name="UnitLen">坐标轴步长</param>
        /// <param name="hasArrow">是否要显示坐标轴的箭头</param>
        /// <param name="showdetials">是否显示更详细的刻度</param>
        /// <param name="axisType">坐标轴类型</param>
        /// <param name="myBrush">字体颜色</param>
        /// <param name="angle">字体倾斜角度</param>
        public Axis(Point pOrigin, String title, AxisType axisType, ref Graphics graphice)
        {
            this._pOrigin = pOrigin;
            this._Title = title;
            this._Values = new String[1];

            if (axisType == AxisType.xAxis)
            {
                this._unitLen = 50;
                this._Angle = 25;
            }
            else
            {
                this._unitLen = 33;
                this._Angle = 0;
            }
            this._unitVol = 10;
            this._hasArrow = true;
            this._showDeitals = false;
            this._axisType = axisType;
            this._myBrush = Brushes.Black;
            this._Graphice = graphice;
        }
        /// <summary>
        /// 画坐标
        /// </summary>
        /// <param name="_Graphice">画布</param>
        public void Paint()
        {
            #region 画坐标
            Point pStart = new Point(0, 0);
            Point pEnd = new Point(0, 0);

            if (this._axisType == AxisType.xAxis)
            {
                pEnd.X = this._unitLen * this._Values.Length + 20;
            }
            else
            {
                pEnd.Y = -this._unitLen * this._Values.Length - 20;
            }

            this._Graphice.DrawLine(new Pen(this._myBrush), pStart, pEnd);
            #endregion

            #region 画坐标的箭头
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 0);
            Point p3 = new Point(0, 0);

            if (_hasArrow)
            {
                if (this._axisType == AxisType.xAxis)
                {
                    p1.X = pEnd.X;
                    p2.X = pEnd.X - 10;
                    p2.Y = -5;
                    p3.X = pEnd.X - 10;
                    p3.Y = 5;
                }
                else
                {
                    p1.Y = pEnd.Y;
                    p2.X = -5;
                    p2.Y = pEnd.Y + 10;
                    p3.X = 5;
                    p3.Y = pEnd.Y + 10;
                }

                Point[] pArrows = new Point[3] { p1, p2, p3 };

                this._Graphice.FillPolygon(this._myBrush, pArrows);//x轴箭头
            }
            #endregion

            #region 画坐标轴刻度线
            for (int i = 0; i < this._Values.Length; i++)
            {
                if (this._axisType == AxisType.xAxis)
                {
                    p1.X = this._unitLen * i;
                    p1.Y = 0;
                    p2.X = this._unitLen * i;
                    p2.Y = -5;
                }
                else
                {
                    p1.X = 5;
                    p1.Y = -this._unitLen * i;
                    p2.X = 0;
                    p2.Y = -this._unitLen * i;
                }

                this._Graphice.DrawLine(new Pen(this._myBrush), p1, p2);
            }
            #endregion

            #region 旋转
            if (this._axisType == AxisType.xAxis)
            {
                p1.X = this._pOrigin.X;
                p1.Y = this._pOrigin.Y;
            }
            else
            {
                p1.X = this._pOrigin.X - 55;
                p1.Y = this._pOrigin.Y;
            }

            if (this._axisType == AxisType.xAxis)
            {
                this._Graphice.RotateTransform(this._Angle, MatrixOrder.Prepend); //旋转图像            

                this._Graphice.DrawString(this._Values[0], new Font("宋体", 10), this._myBrush, 0, 0);

                this._Graphice.ResetTransform();
            }
            else
            {
                this._Graphice.DrawString(this._Values[0], new Font("宋体", 10), this._myBrush, -20, 0);
            }

            for (int i = 0; i < this._Values.Length; i++)
            {
                if (this._axisType == AxisType.xAxis)
                {
                    p1.X = this._pOrigin.X + this._unitLen * i;
                    p1.Y = this._pOrigin.Y;
                }
                else
                {
                    p1.X = this._pOrigin.X - 55;
                    p1.Y = this._pOrigin.Y - this._unitLen * i;
                }

                this._Graphice.TranslateTransform(p1.X, p1.Y);

                this._Graphice.RotateTransform(this._Angle, MatrixOrder.Prepend); //旋转图像            

                this._Graphice.DrawString(this._Values[i], new Font("宋体", 10), this._myBrush, 0, 0);

                this._Graphice.ResetTransform();
            }

            this._Graphice.TranslateTransform(this._pOrigin.X, this._pOrigin.Y);
            #endregion
        }
        #endregion
    }
    //网格类
    /// <summary>
    /// 网格类
    /// </summary>
    public class GridLine
    {
        /// <summary>
        /// x轴
        /// </summary>
        private Axis _xAxis;
        /// <summary>
        /// y轴
        /// </summary>
        private Axis _yAxis;
        /// <summary>
        /// 画笔
        /// </summary>
        private Pen _myPen;
        /// <summary>
        /// 画布
        /// </summary>
        private Graphics _Graphice;
        //设置画笔
        /// <summary>
        /// 设置画笔
        /// </summary>
        public Pen Pen
        {
            get { return this._myPen; }
            set { this._myPen = value; }
        }
        //网格线向导
        /// <summary>
        /// 网格线向导
        /// </summary>
        /// <param name="xAxis">x轴</param>
        /// <param name="yAxis">y轴</param>
        /// <param name="myPen">画笔</param>
        public GridLine(Axis xAxis, Axis yAxis, ref Graphics graphice)
        {
            this._xAxis = xAxis;
            this._yAxis = yAxis;
            this._myPen = new Pen(Brushes.Black);
            this._Graphice = graphice;
        }
        //画x轴
        /// <summary>
        /// 画x轴
        /// </summary>
        public void xGridLine()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 0);

            String[] x_values = this._xAxis.Values;

            for (int i = 1; i <= x_values.Length; i++)
            {
                p1.X = this._xAxis.UnitLen * i;
                p2.X = this._xAxis.UnitLen * i;
                p2.Y = this._yAxis.AxisLength;

                this._Graphice.DrawLine(this._myPen, p1, p2);
            }
        }
        //画y轴
        /// <summary>
        /// 画y轴
        /// </summary>        
        public void yGridLine()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 0);

            String[] y_values = this._yAxis.Values;

            for (int i = 1; i < y_values.Length; i++)
            {
                p1.Y = -this._yAxis.UnitLen * i;
                p2.X = this._xAxis.AxisLength;
                p2.Y = -this._yAxis.UnitLen * i;

                this._Graphice.DrawLine(this._myPen, p1, p2);
            }
        }
    }
    //图示类
    /// <summary>
    /// 图示类
    /// </summary>
    public class Legend
    {
        /// <summary>
        /// 画笔
        /// </summary>
        private Brush _Brush;
        /// <summary>
        /// 内容
        /// </summary>
        private String _Text;
        /// <summary>
        /// 图例的高高
        /// </summary>
        private Int32 _Height;
        /// <summary>
        /// 图例的高宽
        /// </summary>
        private Int32 _Width;
        //图示创建向导
        /// <summary>
        /// 图示创建向导
        /// </summary>
        /// <param name="text">图示内容</param>
        public Legend(String text)
        {
            this._Brush = Brushes.LightPink;
            this._Text = text;
            this._Width = 15;
            this._Height = 10;
        }
        //图示创建向导
        /// <summary>
        /// 图示创建向导
        /// </summary>
        /// <param name="leg">图例</param>
        public Legend(Legend leg)
        {
            this._Brush = leg.Brush;
            this._Text = leg.Text;
            this._Width = leg.Width;
            this._Height = leg.Height;
        }
        //设置画笔
        /// <summary>
        /// 设置画笔
        /// </summary>
        public Brush Brush
        {
            get { return this._Brush; }
            set { _Brush = value; }
        }
        //设置内容
        /// <summary>
        /// 设置内容
        /// </summary>
        public String Text 
        {
            set { this._Text = value; }
            get { return this._Text; }
        }
        //设置或获取图例的高
        /// <summary>
        /// 设置或获取图例的高
        /// </summary>
        public Int32 Height 
        {
            get { return this._Height; }
            set { this._Height = value; }
        }
        //设置或获取图例的宽
        /// <summary>
        /// 设置或获取图例的宽
        /// </summary>
        public Int32 Width 
        {
            get { return this._Width; }
            set { this._Width = value; }
        }
    }
    //图示组
    /// <summary>
    /// 图示组
    /// </summary>
    public class LegendGroup
    {
        /// <summary>
        /// GRAPHIC中间变量
        /// </summary>
        private Graphics _Graphice;
        /// <summary>
        /// 图列集合
        /// </summary>
        private Legend[] _Legends;
        /// <summary>
        /// 图例的定位点
        /// </summary>
        private Point _Anchor;
        /// <summary>
        /// 图例间的空隙
        /// </summary>
        private Int32 _gapWidth;
        /// <summary>
        /// 柱形集合
        /// </summary>
        private SeriesGroup _seriesGroup;
        //图例组创建向导
        /// <summary>
        /// 图例组创建向导
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="graphice">画布</param>
        public LegendGroup(SeriesGroup seriesGroup, ref Graphics graphice)
        {
            this._seriesGroup = seriesGroup;

            this._Legends = new Legend[this._seriesGroup.SeriesCount];

            for (int col = 0; col < this._Legends.Length; col++)
            {
                this._Legends[col] = new Legend(this._seriesGroup.Group[col].Legend);
            }
             
            this._Graphice = graphice;

            this._gapWidth = 10;

            this._Anchor = new Point(50, 50);
        }
        //设置起点
        /// <summary>
        /// 图例定位点
        /// </summary>
        public Point Anchor
        {
            set { this._Anchor = value; }
        }
        //画图例
        /// <summary>
        /// 画图例
        /// </summary>
        public void Paint()
        {
            Point p = new Point(0, this._Anchor.Y);

            foreach (Legend leg in _Legends)
            {
                p.X = this._Anchor.X;
                p.Y += this._gapWidth + leg.Height;

                this._Graphice.FillRectangle(leg.Brush, p.X, p.Y, leg.Width, leg.Height);
                this._Graphice.DrawRectangle(new Pen(Brushes.Black), p.X, p.Y, leg.Width, leg.Height);

                this._Graphice.DrawString(leg.Text, new Font("宋体", 10), Brushes.Black, p.X + 20, p.Y);
            }
        }
    }
    //Web端画类
    /// <summary>
    /// Web端画图
    /// </summary>
    public class Chart
    {
        #region 成员变量
        /// <summary>
        /// 宽
        /// </summary>
        private Int32 _Width;
        /// <summary>
        /// 高
        /// </summary>
        private Int32 _Height;
        /// <summary>
        /// BITMAP中间变量
        /// </summary>
        private Bitmap _BitMap;
        /// <summary>
        /// GRAPHIC中间变量
        /// </summary>
        private Graphics _Graphice;
        /// <summary>
        /// 图片保存路径
        /// </summary>
        private String _Path;
        /// <summary>
        /// 原点坐标
        /// </summary>
        private Point _pOrigin;
        /// <summary>
        /// x轴
        /// </summary>
        private Axis _xAxis;
        /// <summary>
        /// y轴
        /// </summary>       
        private Axis _yAxis;
        /// <summary>
        /// 网格线
        /// </summary>
        private GridLine _gridLine;
        /// <summary>
        /// 数据源
        /// </summary>
        private DataTable _source;
        /// <summary>
        /// 数据组
        /// </summary>
        private SeriesGroup _SeriesGroup;
        /// <summary>
        /// 图例集合
        /// </summary>
        private LegendGroup _LegendGroup;
        #endregion

        #region 公共方法
        //设置保存路径
        /// <summary>
        /// 设置保存路径
        /// </summary>
        public String Path
        {
            get { return this._Path; }
            set { this._Path = value + ".bmp"; }
        }
        /// <summary>
        /// 获取数据组
        /// </summary>
        public SeriesGroup SeriesGroup
        {
            get { return this._SeriesGroup; }
        }
        //获取x轴
        /// <summary>
        /// 获取x轴
        /// </summary>
        public Axis xAxis
        {
            get { return this._xAxis; }
        }
        //获取y轴
        /// <summary>
        /// 获取y轴
        /// </summary>
        public Axis yAxis
        {
            get { return this._yAxis; }
        }
        //图片创建向导
        /// <summary>
        /// 图片创建向导
        /// </summary>
        /// <param name="width">图片的宽</param>
        /// <param name="height">图片的高</param>
        /// <param name="x_title">x轴标题</param>
        /// <param name="y_title">y轴标题</param>
        /// <param name="_x">x轴刻度值</param>
        /// <param name="_y">y轴刻度值</param>
        public Chart(Int32 width, Int32 height, DataTable source)
        {
            if (source == null || source.Columns.Count < 2 || source.Rows.Count < 1)
                throw new Exception("请确保数据源至少有两列且至少有一行数据");

            foreach (DataRow row in source.Rows)
            {
                Regex reg = new Regex(@"^[0-9]*$");

                for (int col = 1; col < source.Columns.Count; col++)
                {
                    if (!reg.IsMatch(row[col].ToString()))
                        throw new Exception("数据源不符合要求.必须是整数");
                }
            }

            this._Width = width;
            this._Height = height;
            this._pOrigin = new Point(90, this._Height - 50);
            this._Path = @"C:\Chart.bmp";
            this._source = source;

            this.Initialization();
        }

        ~Chart()
        {
            _BitMap.Dispose();
            _Graphice.Dispose();
        }
        //获取x轴坐标值
        /// <summary>
        /// 获取x轴坐标值
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns>x轴坐标值数组</returns>
        private String[] xValues
        {
            get
            {
                //把第一列作为x轴
                String[] xValues = new String[this._source.Rows.Count];

                for (int i = 0; i < xValues.Length; i++)
                {
                    xValues[i] = this._source.Rows[i][0].ToString();
                }

                return xValues;
            }
        }
        //获取y轴坐标值
        /// <summary>
        /// 获取y轴坐标值
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns>y轴坐标值数组</returns>
        private String[] yValues
        {
            get
            {
                //把第一列之后的各列作为y轴
                String[] _yValues = new String[11];

                Int32 nMax = 0;//数据列最大数，保证y轴刻度的完全性
                Int32 nTemp = 0;

                foreach (DataRow row in this._source.Rows)
                {
                    nTemp = 0;

                    for (int col = 1; col < this._source.Columns.Count; col++)
                    {
                        if (this._SeriesGroup.Group[col - 1].ChartType == ChartType.Line)
                        {
                            if (nMax < Int32.Parse(row[col].ToString()))
                            {
                                nMax = Int32.Parse(row[col].ToString());
                            }
                        }
                        else if (this._SeriesGroup.Group[col - 1].ChartType == ChartType.ColumnStacked)
                        {
                            nTemp += Int32.Parse(row[col].ToString());
                        }
                    }

                    if (nMax < nTemp)
                    {
                        nMax = nTemp;
                    }
                }

                Int32 n = 1;

                for (int i = 1; i < 10; i++)
                {
                    if (10 >= nMax)
                    {
                        n = i - 1;
                        break;
                    }

                    nMax = Convert.ToInt32(nMax / 10);
                }

                nMax = nMax + 1;

                for (int i = 0; i <= 10; i++)
                {
                    _yValues[i] = Convert.ToString(i * nMax * Math.Pow(10, n - 1));
                }

                this._yAxis.UnitVol = Convert.ToInt32(nMax * Math.Pow(10, n - 1));

                if (this._yAxis.UnitVol == 0)
                    this._yAxis.UnitVol = 10;

                return _yValues;
            }
        }
        //获取或设置网格线
        /// <summary>
        /// 获取或设置网格线
        /// </summary>
        public GridLine GridLine
        {
            get { return this._gridLine; }
            set { this._gridLine = value; }
        }
        //是否要显示x轴刻度线
        /// <summary>
        /// 是否要显示x轴刻度线
        /// </summary>
        /// <param name="myPen">画笔</param>
        public void xGridLine()
        {
            this._gridLine.xGridLine();

            this._BitMap.Save(this._Path);
        }
        //是否要显示y轴刻度线
        /// <summary>
        /// 是否要显示y轴刻度线
        /// </summary>
        /// <param name="myPen">画笔</param>
        public void yGridLine()
        {
            this._gridLine.yGridLine();

            this._BitMap.Save(this._Path);
        }
        //初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialization()
        {
            this._BitMap = new Bitmap(this._Width, this._Height);

            this._Graphice = Graphics.FromImage(this._BitMap);
            this._Graphice.TranslateTransform(this._pOrigin.X, this._pOrigin.Y);
            this._Graphice.Clear(Color.White);

            this._xAxis = new Axis(this._pOrigin, this._source.Columns[0].ColumnName, AxisType.xAxis, ref this._Graphice);
            this._xAxis.Values = this.xValues;

            this._yAxis = new Axis(this._pOrigin, "数量", AxisType.yAxis, ref this._Graphice);

            this._SeriesGroup = new SeriesGroup(this._source, ref this._Graphice);

            this._SeriesGroup.Group[0].Width = this._xAxis.UnitLen;

            this._gridLine = new GridLine(this._xAxis, this._yAxis, ref this._Graphice);

            this._LegendGroup = new LegendGroup(this._SeriesGroup, ref this._Graphice);
        }
        //调整画布大小
        /// <summary>
        /// 调整画布大小
        /// </summary>
        public void Adjust()
        {
            if (this._Width < this._xAxis.AxisLength + 200)
            {
                this._Width = this._xAxis.AxisLength + 200;

                this.Initialization();
            }
        }
        //画图
        /// <summary>
        /// 画图
        /// </summary>
        public void Paint()
        {
            this._yAxis.Values = this.yValues;

            this._SeriesGroup.Unit = 1.0 * this._yAxis.UnitLen / this._yAxis.UnitVol;

            this._LegendGroup.Anchor = new Point(this._xAxis.AxisLength + 20, -this._yAxis.AxisLength * 2 / 3);

            this._SeriesGroup.Paint();

            this._xAxis.Paint();
            this._yAxis.Paint();

            this._LegendGroup.Paint();

            this._BitMap.Save(this._Path);
        }
        #endregion
    }
}