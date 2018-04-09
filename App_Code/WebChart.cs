using System;
using System.IO;//�����ļ���ȡ 
using System.Data;//�������ݷ��� 
using System.Drawing;//�ṩ��GDI+ͼ�εĻ������� 
using System.Drawing.Text;//�ṩ��GDI+ͼ�εĸ߼����� 
using System.Drawing.Drawing2D;//�ṩ���߼���ά��ʸ��ͼ�ι��� 
using System.Drawing.Imaging;//�ṩ��GDI+ͼ�εĸ߼����� 
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;


namespace TCPC.WebChart
{
    //����������
    /// <summary>
    /// �����������
    /// </summary>
    public enum AxisType
    {
        /// <summary>
        /// X��
        /// </summary>
        xAxis,
        /// <summary>
        /// Y��
        /// </summary>
        yAxis
    }
    //Char����
    /// <summary>
    /// Char����
    /// </summary>
    public enum ChartType
    {
        /// <summary>
        /// ����ͼ
        /// </summary>
        Line,
        /// <summary>
        /// ��״ͼ
        /// </summary>
        ColumnStacked
    }
    //���õĻ���
    /// <summary>
    /// ���õĻ��ʼ���
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
    //��Series
    /// <summary>
    /// Series��
    /// </summary>
    public class Series
    {
        /// <summary>
        /// �������εĿ�
        /// </summary>
        private Int32 _Width;
        /// <summary>
        /// ͼ��
        /// </summary>
        private Legend _Legend;
        /// <summary>
        /// ������
        /// </summary>
        private String[] _Data;
        /// <summary>
        /// ͼ������
        /// </summary>
        private ChartType _chartType;
        /// <summary>
        /// ��������ͼ��ľ���
        /// </summary>
        private Int32 _gapWidth;
        /// <summary>
        /// ������ɫ
        /// </summary>
        private Brush _Brush;
        //Series������
        /// <summary>
        /// Series������
        /// </summary>
        /// <param name="xAxis">x��</param>
        /// <param name="yAxis">y��</param>
        /// <param name="chartType">ͼ������</param>
        public Series()
        {
            this._chartType = ChartType.ColumnStacked;
            this._gapWidth = 0;
            this._Brush = Brushes.Green;
            this._Legend = new Legend("ͼ��");
            this._Width = 50;
        }
        //��ȡ���������εĿ�
        /// <summary>
        /// ��ȡ���������εĿ�
        /// </summary>
        public Int32 Width
        {
            get { return this._Width; }
            set { this._Width = value; }
        }
        //����ͼ������
        /// <summary>
        /// ����ͼ������
        /// </summary>
        public ChartType ChartType
        {
            get { return this._chartType; }
            set { this._chartType = value; }
        }
        //��������
        /// <summary>
        /// ��������
        /// </summary>
        public String[] Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }
        //��������ͼ���ڼ�Ŀ�϶
        /// <summary>
        /// ��������ͼ���ڼ�Ŀ�϶
        /// </summary>
        public Int32 GapWidth
        {
            get { return this._gapWidth; }
            set { this._gapWidth = value; }
        }
        //���������ɫ
        /// <summary>
        /// ���������ɫ
        /// </summary>
        public Brush Brush
        {
            get { return _Brush; }
            set { this._Brush = value; }
        }
        //���û��ȡͼ��
        /// <summary>
        /// ���û��ȡͼ��
        /// </summary>
        public Legend Legend
        {
            get { return this._Legend; }
            set { this._Legend = value; }
        }
    }
    //Series������
    /// <summary>
    /// Series������
    /// </summary>
    public class SeriesGroup
    {
        /// <summary>
        /// ����ÿ��λ
        /// </summary>
        private Double _Unit;
        /// <summary>
        /// GRAPHIC�м����
        /// </summary>
        private Graphics _Graphice;
        /// <summary>
        /// Series����
        /// </summary>
        private Series[] _Group;
        /// <summary>
        /// ����Դ
        /// </summary>
        private DataTable _source;
        //Series���ϴ�����
        /// <summary>
        /// Series���ϴ�����
        /// </summary>
        /// <param name="source">����Դ</param>
        /// <param name="graphice">����</param>
        public SeriesGroup(DataTable source, ref Graphics graphice)
        {
            if (source == null || source.Columns.Count < 2 || source.Rows.Count < 1)
                throw new Exception("��ȷ������Դ������������������һ������");

            foreach (DataRow row in source.Rows)
            {
                Regex reg = new Regex(@"^[0-9]*$");

                for (int col = 1; col < source.Columns.Count; col++)
                {
                    if (!reg.IsMatch(row[col].ToString()))
                        throw new Exception("����Դ������Ҫ��.����������");
                }
            }

            //�ӵڶ��п�ʼ
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
        //���ϸ���
        /// <summary>
        /// ���ϸ���
        /// </summary>
        public Int32 SeriesCount
        {
            get { return this._Group.Length; }
        }
        //get series����
        /// <summary>
        /// get series����
        /// </summary>
        public Series[] Group 
        {
            get { return this._Group; }
            set { this._Group = value; }
        }
        //���õ�λÿ����
        /// <summary>
        /// ���õ�λÿ����
        /// </summary>
        public Double Unit
        {
            set { this._Unit = value; }
        }
        //��Series
        /// <summary>
        /// ��Series
        /// </summary>
        public void Paint()
        {
            Rectangle[] rectArray = new Rectangle[this._source.Rows.Count];

            //��ʼ��
            for (int i = 0; i < rectArray.Length; i++)
            {
                rectArray[i] = new Rectangle(0, 0, 0, 0);
            }

            //ChartType.ColumnStacked���У�ȡ����GapWidth����ColumnStacked��Ч
            Int32 nWidth = 0;//����ͼ���
            Int32 nGapWidth = 0;//��������ͼ��϶

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
                if (series.ChartType == ChartType.Line)//������ͼ
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
                else if (series.ChartType == ChartType.ColumnStacked)//����״ͼ
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
    //Axis��
    /// <summary>
    /// Axis��
    /// </summary>
    public class Axis
    {
        #region ��������
        /// <summary>
        /// ԭ��
        /// </summary>
        private Point _pOrigin;
        /// <summary>
        /// ���������
        /// </summary>
        private String _Title;
        /// <summary>
        /// ����ֵ
        /// </summary>
        private String[] _Values;
        /// <summary>
        /// ����
        /// </summary>
        private Int32 _unitLen;
        /// <summary>
        /// ��λ����
        /// </summary>
        private Int32 _unitVol;
        /// <summary>
        /// �Ƿ�Ҫ��ͷ
        /// </summary>
        private Boolean _hasArrow;
        /// <summary>
        /// �Ƿ���ʾ��ϸ�̶�
        /// </summary>
        private Boolean _showDeitals;
        /// <summary>
        /// ��������
        /// </summary>
        private AxisType _axisType;
        /// <summary>
        /// ����
        /// </summary>
        private Brush _myBrush;
        /// <summary>
        /// ������б��
        /// </summary>
        private Int32 _Angle;
        /// <summary>
        /// ����
        /// </summary>
        private Graphics _Graphice;
        #endregion

        #region ��������
       /// <summary>
       /// �������������
       /// </summary>
        public String Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }
        /// <summary>
        /// ��������ֵ
        /// </summary>
        public String[] Values
        {
            get { return this._Values; }
            set { this._Values = value; }
        }
        /// <summary>
        /// ���ò���
        /// </summary>
        public Int32 UnitLen
        {
            get { return this._unitLen; }
            set { this._unitLen = value; }
        }
        /// <summary>
        /// ��ȡ�����õ�λ����
        /// </summary>
        public Int32 UnitVol
        {
            get { return this._unitVol; }
            set { this._unitVol = value; }
        }
        /// <summary>
        /// �����Ƿ���Ҫ��ʾ��ͷ
        /// </summary>
        public Boolean HasArrow
        {
            get { return this._hasArrow; }
            set { this._hasArrow = value; }
        }
        /// <summary>
        /// �����Ƿ���Ҫ��ʾ����ϸ�Ŀ̶�
        /// </summary>
        public Boolean ShowDeitals
        {
            get { return this._showDeitals; }
            set { this._showDeitals = value; }
        }
        /// <summary>
        /// ���û���
        /// </summary>
        public Brush Brush 
        {
            get { return this._myBrush; }
            set { this._myBrush = value; }
        }
        /// <summary>
        /// ����������б��
        /// </summary>
        public Int32 Angle 
        {
            get { return this._Angle; }
            set { this._Angle = value; }
        }
        /// <summary>
        /// �����᳤��
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
        /// �����ᴴ����
        /// </summary>
        /// <param name="pOrigin">�������ԭ��</param>
        /// <param name="title">���������</param>
        /// <param name="values">������ֵ</param>
        /// <param name="UnitLen">�����Ჽ��</param>
        /// <param name="hasArrow">�Ƿ�Ҫ��ʾ������ļ�ͷ</param>
        /// <param name="showdetials">�Ƿ���ʾ����ϸ�Ŀ̶�</param>
        /// <param name="axisType">����������</param>
        /// <param name="myBrush">������ɫ</param>
        /// <param name="angle">������б�Ƕ�</param>
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
        /// ������
        /// </summary>
        /// <param name="_Graphice">����</param>
        public void Paint()
        {
            #region ������
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

            #region ������ļ�ͷ
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

                this._Graphice.FillPolygon(this._myBrush, pArrows);//x���ͷ
            }
            #endregion

            #region ��������̶���
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

            #region ��ת
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
                this._Graphice.RotateTransform(this._Angle, MatrixOrder.Prepend); //��תͼ��            

                this._Graphice.DrawString(this._Values[0], new Font("����", 10), this._myBrush, 0, 0);

                this._Graphice.ResetTransform();
            }
            else
            {
                this._Graphice.DrawString(this._Values[0], new Font("����", 10), this._myBrush, -20, 0);
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

                this._Graphice.RotateTransform(this._Angle, MatrixOrder.Prepend); //��תͼ��            

                this._Graphice.DrawString(this._Values[i], new Font("����", 10), this._myBrush, 0, 0);

                this._Graphice.ResetTransform();
            }

            this._Graphice.TranslateTransform(this._pOrigin.X, this._pOrigin.Y);
            #endregion
        }
        #endregion
    }
    //������
    /// <summary>
    /// ������
    /// </summary>
    public class GridLine
    {
        /// <summary>
        /// x��
        /// </summary>
        private Axis _xAxis;
        /// <summary>
        /// y��
        /// </summary>
        private Axis _yAxis;
        /// <summary>
        /// ����
        /// </summary>
        private Pen _myPen;
        /// <summary>
        /// ����
        /// </summary>
        private Graphics _Graphice;
        //���û���
        /// <summary>
        /// ���û���
        /// </summary>
        public Pen Pen
        {
            get { return this._myPen; }
            set { this._myPen = value; }
        }
        //��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="xAxis">x��</param>
        /// <param name="yAxis">y��</param>
        /// <param name="myPen">����</param>
        public GridLine(Axis xAxis, Axis yAxis, ref Graphics graphice)
        {
            this._xAxis = xAxis;
            this._yAxis = yAxis;
            this._myPen = new Pen(Brushes.Black);
            this._Graphice = graphice;
        }
        //��x��
        /// <summary>
        /// ��x��
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
        //��y��
        /// <summary>
        /// ��y��
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
    //ͼʾ��
    /// <summary>
    /// ͼʾ��
    /// </summary>
    public class Legend
    {
        /// <summary>
        /// ����
        /// </summary>
        private Brush _Brush;
        /// <summary>
        /// ����
        /// </summary>
        private String _Text;
        /// <summary>
        /// ͼ���ĸ߸�
        /// </summary>
        private Int32 _Height;
        /// <summary>
        /// ͼ���ĸ߿�
        /// </summary>
        private Int32 _Width;
        //ͼʾ������
        /// <summary>
        /// ͼʾ������
        /// </summary>
        /// <param name="text">ͼʾ����</param>
        public Legend(String text)
        {
            this._Brush = Brushes.LightPink;
            this._Text = text;
            this._Width = 15;
            this._Height = 10;
        }
        //ͼʾ������
        /// <summary>
        /// ͼʾ������
        /// </summary>
        /// <param name="leg">ͼ��</param>
        public Legend(Legend leg)
        {
            this._Brush = leg.Brush;
            this._Text = leg.Text;
            this._Width = leg.Width;
            this._Height = leg.Height;
        }
        //���û���
        /// <summary>
        /// ���û���
        /// </summary>
        public Brush Brush
        {
            get { return this._Brush; }
            set { _Brush = value; }
        }
        //��������
        /// <summary>
        /// ��������
        /// </summary>
        public String Text 
        {
            set { this._Text = value; }
            get { return this._Text; }
        }
        //���û��ȡͼ���ĸ�
        /// <summary>
        /// ���û��ȡͼ���ĸ�
        /// </summary>
        public Int32 Height 
        {
            get { return this._Height; }
            set { this._Height = value; }
        }
        //���û��ȡͼ���Ŀ�
        /// <summary>
        /// ���û��ȡͼ���Ŀ�
        /// </summary>
        public Int32 Width 
        {
            get { return this._Width; }
            set { this._Width = value; }
        }
    }
    //ͼʾ��
    /// <summary>
    /// ͼʾ��
    /// </summary>
    public class LegendGroup
    {
        /// <summary>
        /// GRAPHIC�м����
        /// </summary>
        private Graphics _Graphice;
        /// <summary>
        /// ͼ�м���
        /// </summary>
        private Legend[] _Legends;
        /// <summary>
        /// ͼ���Ķ�λ��
        /// </summary>
        private Point _Anchor;
        /// <summary>
        /// ͼ����Ŀ�϶
        /// </summary>
        private Int32 _gapWidth;
        /// <summary>
        /// ���μ���
        /// </summary>
        private SeriesGroup _seriesGroup;
        //ͼ���鴴����
        /// <summary>
        /// ͼ���鴴����
        /// </summary>
        /// <param name="source">����Դ</param>
        /// <param name="graphice">����</param>
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
        //�������
        /// <summary>
        /// ͼ����λ��
        /// </summary>
        public Point Anchor
        {
            set { this._Anchor = value; }
        }
        //��ͼ��
        /// <summary>
        /// ��ͼ��
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

                this._Graphice.DrawString(leg.Text, new Font("����", 10), Brushes.Black, p.X + 20, p.Y);
            }
        }
    }
    //Web�˻���
    /// <summary>
    /// Web�˻�ͼ
    /// </summary>
    public class Chart
    {
        #region ��Ա����
        /// <summary>
        /// ��
        /// </summary>
        private Int32 _Width;
        /// <summary>
        /// ��
        /// </summary>
        private Int32 _Height;
        /// <summary>
        /// BITMAP�м����
        /// </summary>
        private Bitmap _BitMap;
        /// <summary>
        /// GRAPHIC�м����
        /// </summary>
        private Graphics _Graphice;
        /// <summary>
        /// ͼƬ����·��
        /// </summary>
        private String _Path;
        /// <summary>
        /// ԭ������
        /// </summary>
        private Point _pOrigin;
        /// <summary>
        /// x��
        /// </summary>
        private Axis _xAxis;
        /// <summary>
        /// y��
        /// </summary>       
        private Axis _yAxis;
        /// <summary>
        /// ������
        /// </summary>
        private GridLine _gridLine;
        /// <summary>
        /// ����Դ
        /// </summary>
        private DataTable _source;
        /// <summary>
        /// ������
        /// </summary>
        private SeriesGroup _SeriesGroup;
        /// <summary>
        /// ͼ������
        /// </summary>
        private LegendGroup _LegendGroup;
        #endregion

        #region ��������
        //���ñ���·��
        /// <summary>
        /// ���ñ���·��
        /// </summary>
        public String Path
        {
            get { return this._Path; }
            set { this._Path = value + ".bmp"; }
        }
        /// <summary>
        /// ��ȡ������
        /// </summary>
        public SeriesGroup SeriesGroup
        {
            get { return this._SeriesGroup; }
        }
        //��ȡx��
        /// <summary>
        /// ��ȡx��
        /// </summary>
        public Axis xAxis
        {
            get { return this._xAxis; }
        }
        //��ȡy��
        /// <summary>
        /// ��ȡy��
        /// </summary>
        public Axis yAxis
        {
            get { return this._yAxis; }
        }
        //ͼƬ������
        /// <summary>
        /// ͼƬ������
        /// </summary>
        /// <param name="width">ͼƬ�Ŀ�</param>
        /// <param name="height">ͼƬ�ĸ�</param>
        /// <param name="x_title">x�����</param>
        /// <param name="y_title">y�����</param>
        /// <param name="_x">x��̶�ֵ</param>
        /// <param name="_y">y��̶�ֵ</param>
        public Chart(Int32 width, Int32 height, DataTable source)
        {
            if (source == null || source.Columns.Count < 2 || source.Rows.Count < 1)
                throw new Exception("��ȷ������Դ������������������һ������");

            foreach (DataRow row in source.Rows)
            {
                Regex reg = new Regex(@"^[0-9]*$");

                for (int col = 1; col < source.Columns.Count; col++)
                {
                    if (!reg.IsMatch(row[col].ToString()))
                        throw new Exception("����Դ������Ҫ��.����������");
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
        //��ȡx������ֵ
        /// <summary>
        /// ��ȡx������ֵ
        /// </summary>
        /// <param name="source">����Դ</param>
        /// <returns>x������ֵ����</returns>
        private String[] xValues
        {
            get
            {
                //�ѵ�һ����Ϊx��
                String[] xValues = new String[this._source.Rows.Count];

                for (int i = 0; i < xValues.Length; i++)
                {
                    xValues[i] = this._source.Rows[i][0].ToString();
                }

                return xValues;
            }
        }
        //��ȡy������ֵ
        /// <summary>
        /// ��ȡy������ֵ
        /// </summary>
        /// <param name="source">����Դ</param>
        /// <returns>y������ֵ����</returns>
        private String[] yValues
        {
            get
            {
                //�ѵ�һ��֮��ĸ�����Ϊy��
                String[] _yValues = new String[11];

                Int32 nMax = 0;//���������������֤y��̶ȵ���ȫ��
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
        //��ȡ������������
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        public GridLine GridLine
        {
            get { return this._gridLine; }
            set { this._gridLine = value; }
        }
        //�Ƿ�Ҫ��ʾx��̶���
        /// <summary>
        /// �Ƿ�Ҫ��ʾx��̶���
        /// </summary>
        /// <param name="myPen">����</param>
        public void xGridLine()
        {
            this._gridLine.xGridLine();

            this._BitMap.Save(this._Path);
        }
        //�Ƿ�Ҫ��ʾy��̶���
        /// <summary>
        /// �Ƿ�Ҫ��ʾy��̶���
        /// </summary>
        /// <param name="myPen">����</param>
        public void yGridLine()
        {
            this._gridLine.yGridLine();

            this._BitMap.Save(this._Path);
        }
        //��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Initialization()
        {
            this._BitMap = new Bitmap(this._Width, this._Height);

            this._Graphice = Graphics.FromImage(this._BitMap);
            this._Graphice.TranslateTransform(this._pOrigin.X, this._pOrigin.Y);
            this._Graphice.Clear(Color.White);

            this._xAxis = new Axis(this._pOrigin, this._source.Columns[0].ColumnName, AxisType.xAxis, ref this._Graphice);
            this._xAxis.Values = this.xValues;

            this._yAxis = new Axis(this._pOrigin, "����", AxisType.yAxis, ref this._Graphice);

            this._SeriesGroup = new SeriesGroup(this._source, ref this._Graphice);

            this._SeriesGroup.Group[0].Width = this._xAxis.UnitLen;

            this._gridLine = new GridLine(this._xAxis, this._yAxis, ref this._Graphice);

            this._LegendGroup = new LegendGroup(this._SeriesGroup, ref this._Graphice);
        }
        //����������С
        /// <summary>
        /// ����������С
        /// </summary>
        public void Adjust()
        {
            if (this._Width < this._xAxis.AxisLength + 200)
            {
                this._Width = this._xAxis.AxisLength + 200;

                this.Initialization();
            }
        }
        //��ͼ
        /// <summary>
        /// ��ͼ
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