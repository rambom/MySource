﻿using Amazon_analyzer.Database;
using Amazon_analyzer.Helpers;
using Aspose.Cells;
using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Amazon_analyzer
{
    public partial class Form1 : Form
    {
        static string conn_string = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        int timeout = int.Parse(ConfigurationManager.AppSettings["timeout"]);
        string asinUrl = ConfigurationManager.AppSettings["asin_url"];
        IDataBase db = new OracleHandler(conn_string);
        int minimumWidth = 200;
        private ProgressForm progressForm = null;
        private delegate bool IncreaseHandle(int nValue);
        private IncreaseHandle myIncrease = null;
        public Form1()
        {
            InitializeComponent();
            this.textBox1.DoubleClick += this.importFileTextBox_DoubleClick;
            this.textBox2.DoubleClick += this.importFileTextBox_DoubleClick;
            this.textBox3.DoubleClick += this.importFileTextBox_DoubleClick;
            this.textBox4.DoubleClick += this.importFileTextBox_DoubleClick;
            this.textBox5.DoubleClick += this.importFileTextBox_DoubleClick;
            this.textBox6.DoubleClick += this.importFileTextBox_DoubleClick;
            this.textBox7.DoubleClick += this.importFileTextBox_DoubleClick;
            this.textBox8.DoubleClick += this.importFileTextBox_DoubleClick;
            this.linkLabel1.Click += linkLabel1_Click;
            this.linkLabel2.Click += linkLabel2_Click;
            this.linkLabel3.Click += linkLabel3_Click;
            this.linkLabel4.Click += linkLabel4_Click;
            this.linkLabel5.Click += linkLabel5_Click;
            this.linkLabel6.Click += linkLabel6_Click;
            this.linkLabel7.Click += linkLabel7_Click;
            this.linkLabel8.Click += linkLabel8_Click;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker4.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker5.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker5.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker5.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker6.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker6.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker6.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker7.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker7.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker7.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker8.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker8.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker8.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.label1.Text = "";
            this.label2.Text = "";
            this.label3.Text = "";
            this.label4.Text = "";
            this.label5.Text = "";
            this.label6.Text = "";
            this.label7.Text = "";
            this.label8.Text = "";
            this.pager1.EventPaging += pager1_EventPaging;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.RowPostPaint += dataGridView_RowPostPaint;
            this.dataGridView1.CellContentClick += dataGridView_CellContentClick;
            this.dataGridView1.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            this.dataGridView1.DataBindingComplete += dataGridView_DataBindingComplete;
            this.pager2.EventPaging += pager2_EventPaging;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.RowPostPaint += dataGridView_RowPostPaint;
            this.dataGridView2.CellContentClick += dataGridView_CellContentClick;
            this.dataGridView2.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            this.dataGridView2.DataBindingComplete += dataGridView_DataBindingComplete;
            this.pager3.EventPaging += pager3_EventPaging;
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.RowPostPaint += dataGridView_RowPostPaint;
            this.dataGridView3.CellContentClick += dataGridView_CellContentClick;
            this.dataGridView3.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            this.dataGridView3.DataBindingComplete += dataGridView_DataBindingComplete;
            this.pager4.EventPaging += pager4_EventPaging;
            this.dataGridView4.AutoGenerateColumns = false;
            this.dataGridView4.RowPostPaint += dataGridView_RowPostPaint;
            this.dataGridView4.CellContentClick += dataGridView_CellContentClick;
            this.dataGridView4.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            this.dataGridView4.DataBindingComplete += dataGridView_DataBindingComplete;
            this.pager5.EventPaging += pager5_EventPaging;
            this.dataGridView5.AutoGenerateColumns = false;
            this.dataGridView5.RowPostPaint += dataGridView_RowPostPaint;
            this.dataGridView5.CellContentClick += dataGridView_CellContentClick;
            this.dataGridView5.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            this.dataGridView5.DataBindingComplete += dataGridView_DataBindingComplete;
            this.pager6.EventPaging += pager6_EventPaging;
            this.dataGridView6.AutoGenerateColumns = false;
            this.dataGridView6.RowPostPaint += dataGridView_RowPostPaint;
            this.dataGridView6.CellContentClick += dataGridView_CellContentClick;
            this.dataGridView6.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            this.dataGridView6.DataBindingComplete += dataGridView_DataBindingComplete;
            this.pager7.EventPaging += pager7_EventPaging;
            this.dataGridView7.AutoGenerateColumns = false;
            this.dataGridView7.RowPostPaint += dataGridView_RowPostPaint;
            this.dataGridView7.CellContentClick += dataGridView_CellContentClick;
            this.dataGridView7.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            this.dataGridView7.DataBindingComplete += dataGridView_DataBindingComplete;
            this.pager8.EventPaging += pager8_EventPaging;
            this.dataGridView8.AutoGenerateColumns = false;
            this.dataGridView8.RowPostPaint += dataGridView_RowPostPaint;
            this.dataGridView8.CellContentClick += dataGridView_CellContentClick;
            this.dataGridView8.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            this.dataGridView8.DataBindingComplete += dataGridView_DataBindingComplete;

            this.export1.Click += export_Click;
            this.export2.Click += export_Click;
            this.export3.Click += export_Click;
            this.export4.Click += export_Click;
            this.export5.Click += export_Click;
            this.export6.Click += export_Click;
            this.export7.Click += export_Click;
            this.export8.Click += export_Click;

            foreach (DataGridViewColumn col in this.dataGridView1.Columns)
            {
                col.MinimumWidth = minimumWidth;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            foreach (DataGridViewColumn col in this.dataGridView2.Columns)
            {
                col.MinimumWidth = minimumWidth;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            foreach (DataGridViewColumn col in this.dataGridView3.Columns)
            {
                col.MinimumWidth = minimumWidth;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            foreach (DataGridViewColumn col in this.dataGridView4.Columns)
            {
                col.MinimumWidth = minimumWidth;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            foreach (DataGridViewColumn col in this.dataGridView5.Columns)
            {
                col.MinimumWidth = minimumWidth;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            foreach (DataGridViewColumn col in this.dataGridView6.Columns)
            {
                col.MinimumWidth = minimumWidth;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            foreach (DataGridViewColumn col in this.dataGridView7.Columns)
            {
                col.MinimumWidth = minimumWidth;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            foreach (DataGridViewColumn col in this.dataGridView8.Columns)
            {
                col.MinimumWidth = minimumWidth;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }
        private void ShowProcessBar()
        {
            progressForm = new ProgressForm();

            // Init increase event
            myIncrease = new IncreaseHandle(progressForm.Increase);
            progressForm.StartPosition = FormStartPosition.CenterScreen;
            progressForm.ShowDialog();
            progressForm = null;
        }
        void export_Click(object sender, EventArgs e)
        {
            string strCondition = "";
            string tableName = "";
            List<DataParameter> param = new List<DataParameter>();
            string orderBy = "";
            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                    this.GetPager1Condition(out strCondition, out tableName, out param);
                    orderBy = this.getOrderBy(dataGridView1);
                    break;
                case 1:
                    this.GetPager2Condition(out strCondition, out tableName, out param);
                    orderBy = this.getOrderBy(dataGridView2);
                    break;
                case 2:
                    this.GetPager3Condition(out strCondition, out tableName, out param);
                    orderBy = this.getOrderBy(dataGridView3);
                    break;
                case 3:
                    this.GetPager4Condition(out strCondition, out tableName, out param);
                    orderBy = this.getOrderBy(dataGridView4);
                    break;
                case 4:
                    this.GetPager5Condition(out strCondition, out tableName, out param);
                    orderBy = this.getOrderBy(dataGridView5);
                    break;
                case 5:
                    this.GetPager6Condition(out strCondition, out tableName, out param);
                    orderBy = this.getOrderBy(dataGridView6);
                    break;
                case 6:
                    this.GetPager7Condition(out strCondition, out tableName, out param);
                    orderBy = this.getOrderBy(dataGridView7);
                    break;
                case 7:
                    this.GetPager8Condition(out strCondition, out tableName, out param);
                    orderBy = this.getOrderBy(dataGridView8);
                    break;
            }
            Thread fThread = new Thread(new ParameterizedThreadStart(ExportExcel));//开辟一个新的线程
            fThread.Start(new object[] { strCondition, tableName, param, orderBy });
        }

        public void ExportExcel(object args)
        {
            string strCondition = ((object[])args)[0] as string;
            string tableName = ((object[])args)[1] as string;
            List<DataParameter> param = ((object[])args)[2] as List<DataParameter>;
            string orderBy = ((object[])args)[3] as string;

            string fileName = string.Format("{0}_{1:yyyyMMddHHmmss}.xlsx", tableName, DateTime.Now);


            Workbook workbook = new Workbook();
            workbook.Worksheets.Add();
            bool first = true;
            int rowIndex = 0;
            Style style = new Style();

            MethodInvoker mi = new MethodInvoker(ShowProcessBar);
            this.BeginInvoke(mi);
            Thread.Sleep(1000);
            this.Invoke(this.myIncrease, new object[] { 0 });

            int rowsCount = (int)db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DbDataReader dataReader = db.ExecuteDataReader(string.Format("select * from {0} where 1=1 {1} {2}", tableName, strCondition, string.IsNullOrEmpty(orderBy) ? "" : "order by " + orderBy), param);

            while (dataReader.Read())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (first)
                    {
                        style.Custom = "@";
                        style.Font.IsBold = true;
                        workbook.Worksheets[0].Cells[rowIndex, i].SetStyle(style);
                        workbook.Worksheets[0].Cells[rowIndex, i].Value = dataReader.GetName(i);
                    }

                    style.Font.IsBold = false; if (dataReader[i] is string)
                    {
                        style.Custom = "@";
                    }
                    else if (dataReader[i] is DateTime)
                    {
                        style.Custom = "yyyy-MM-dd";
                    }
                    else if (dataReader[i] is decimal)
                    {
                        style.Custom = "####.#####";
                        style.ShrinkToFit = true;
                    }
                    workbook.Worksheets[0].Cells[rowIndex + 1, i].SetStyle(style);
                    workbook.Worksheets[0].Cells[rowIndex + 1, i].Value = dataReader[i];
                }
                first = false;
                rowIndex++;
                this.Invoke(this.myIncrease, new object[] { (rowIndex * 100 - 1) / rowsCount });
            }
            workbook.Worksheets[0].AutoFitColumns();
            workbook.Worksheets[0].AutoFitRows(true);
            if (fileName.ToLower().EndsWith(".xlsx"))
                workbook.Save(fileName, FileFormatType.Excel2007Xlsx);
            else
                workbook.Save(fileName, FileFormatType.Default);
            this.Invoke(this.myIncrease, new object[] { 100 });
            System.Diagnostics.Process.Start(fileName);

        }

        void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            if (!gridOrder.ContainsKey(dg.Name)) return;
            if (gridOrder[dg.Name][1].EndsWith("asc"))
            {
                dg.Columns[int.Parse(gridOrder[dg.Name][0])].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
            }
            else
            {
                dg.Columns[int.Parse(gridOrder[dg.Name][0])].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
            }
        }

        void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            if (gridOrder.ContainsKey(dg.Name))
            {
                if (gridOrder[dg.Name][1].Equals(dg.Columns[e.ColumnIndex].DataPropertyName + " desc"))
                {
                    gridOrder[dg.Name][1] = dg.Columns[e.ColumnIndex].DataPropertyName + " asc";
                }
                else
                {
                    gridOrder.Remove(dg.Name);
                    gridOrder.Add(dg.Name, new string[2]);
                    gridOrder[dg.Name][0] = e.ColumnIndex.ToString();
                    gridOrder[dg.Name][1] = dg.Columns[e.ColumnIndex].DataPropertyName + " desc";
                }
            }
            else
            {
                gridOrder.Remove(dg.Name);
                gridOrder.Add(dg.Name, new string[2]);
                gridOrder[dg.Name][0] = e.ColumnIndex.ToString();
                gridOrder[dg.Name][1] = dg.Columns[e.ColumnIndex].DataPropertyName + " desc";
            }

            switch (dg.Name)
            {
                case "dataGridView1":
                    this.pager1.Bind();
                    break;
                case "dataGridView2":
                    this.pager2.Bind();
                    break;
                case "dataGridView3":
                    this.pager3.Bind();
                    break;
                case "dataGridView4":
                    this.pager4.Bind();
                    break;
                case "dataGridView5":
                    this.pager5.Bind();
                    break;
                case "dataGridView6":
                    this.pager6.Bind();
                    break;
                case "dataGridView7":
                    this.pager7.Bind();
                    break;
                case "dataGridView8":
                    this.pager8.Bind();
                    break;
            }
        }

        private IDictionary<string, string[]> gridOrder = new Dictionary<string, string[]>();


        public string getOrderBy(DataGridView grid)
        {
            return gridOrder.ContainsKey(grid.Name) ? gridOrder[grid.Name][1] : "";
        }

        int pager1_EventPaging(Control.EventPagingArg e)
        {

            string strCondition;
            string tableName;
            List<DataParameter> param;
            GetPager1Condition(out strCondition, out tableName, out param);

            decimal rowsCount = db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DataTable dt = db.ExecuteDataTablePaged(tableName, "*", "", strCondition, param, getOrderBy(this.dataGridView1), (this.pager1.PageCurrent - 1) * this.pager1.PageSize, (this.pager1.PageCurrent) * this.pager1.PageSize);
            this.dataGridView1.DataSource = dt;
            return (int)rowsCount;
        }

        private void GetPager1Condition(out string strCondition, out string tableName, out List<DataParameter> param)
        {
            strCondition = "";
            tableName = ConfigurationManager.AppSettings["top_asin_table"];
            param = new List<DataParameter>();
            if (!string.IsNullOrEmpty(textBox9.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label9.Text.Trim());
                param.Add(new DataParameter(label9.Text.Trim(), DbType.String, 50, textBox9.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox10.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label10.Text.Trim());
                param.Add(new DataParameter(label10.Text.Trim(), DbType.String, 50, textBox10.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox11.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label11.Text.Trim());
                param.Add(new DataParameter(label11.Text.Trim(), DbType.String, 50, textBox11.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox12.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label12.Text.Trim());
                param.Add(new DataParameter(label12.Text.Trim(), DbType.String, 50, textBox12.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(textBox43.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label43.Text.Trim());
                param.Add(new DataParameter(label43.Text.Trim(), DbType.String, 50, textBox43.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox42.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label42.Text.Trim());
                param.Add(new DataParameter(label42.Text.Trim(), DbType.String, 50, textBox42.Text.Trim()));
            }
        }

        int pager2_EventPaging(Control.EventPagingArg e)
        {

            string strCondition;
            string tableName;
            List<DataParameter> param;
            GetPager2Condition(out strCondition, out tableName, out param);

            decimal rowsCount = db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DataTable dt = db.ExecuteDataTablePaged(tableName, "*", "", strCondition, param, getOrderBy(this.dataGridView2), (this.pager2.PageCurrent - 1) * this.pager1.PageSize, (this.pager2.PageCurrent) * this.pager2.PageSize);
            this.dataGridView2.DataSource = dt;
            return (int)rowsCount;
        }

        private void GetPager2Condition(out string strCondition, out string tableName, out List<DataParameter> param)
        {
            strCondition = "";
            tableName = ConfigurationManager.AppSettings["top_seller_table"];
            param = new List<DataParameter>();
            if (!string.IsNullOrEmpty(textBox13.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label13.Text.Trim());
                param.Add(new DataParameter(label13.Text.Trim(), DbType.String, 50, textBox13.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox14.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label14.Text.Trim());
                param.Add(new DataParameter(label14.Text.Trim(), DbType.String, 50, textBox14.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox15.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label15.Text.Trim());
                param.Add(new DataParameter(label15.Text.Trim(), DbType.String, 50, textBox15.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox16.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label16.Text.Trim());
                param.Add(new DataParameter(label16.Text.Trim(), DbType.String, 50, textBox16.Text.Trim()));
            }
        }
        int pager3_EventPaging(Control.EventPagingArg e)
        {

            string strCondition;
            string tableName;
            List<DataParameter> param;
            GetPager3Condition(out strCondition, out tableName, out param);

            decimal rowsCount = db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DataTable dt = db.ExecuteDataTablePaged(tableName, "*", "", strCondition, param, getOrderBy(this.dataGridView3), (this.pager3.PageCurrent - 1) * this.pager3.PageSize, (this.pager3.PageCurrent) * this.pager3.PageSize);
            this.dataGridView3.DataSource = dt;
            return (int)rowsCount;
        }

        private void GetPager3Condition(out string strCondition, out string tableName, out List<DataParameter> param)
        {
            strCondition = "";
            tableName = ConfigurationManager.AppSettings["top_brand_table"];
            param = new List<DataParameter>();
            if (!string.IsNullOrEmpty(textBox17.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label17.Text.Trim());
                param.Add(new DataParameter(label17.Text.Trim(), DbType.String, 50, textBox17.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox18.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label18.Text.Trim());
                param.Add(new DataParameter(label18.Text.Trim(), DbType.String, 50, textBox18.Text.Trim()));
            }
        }
        int pager4_EventPaging(Control.EventPagingArg e)
        {

            string strCondition;
            string tableName;
            List<DataParameter> param;
            GetPager4Condition(out strCondition, out tableName, out param);

            decimal rowsCount = db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DataTable dt = db.ExecuteDataTablePaged(tableName, "*", "", strCondition, param, getOrderBy(this.dataGridView4), (this.pager4.PageCurrent - 1) * this.pager4.PageSize, (this.pager4.PageCurrent) * this.pager4.PageSize);
            this.dataGridView4.DataSource = dt;
            return (int)rowsCount;
        }

        private void GetPager4Condition(out string strCondition, out string tableName, out List<DataParameter> param)
        {
            strCondition = "";
            tableName = ConfigurationManager.AppSettings["top_subcategory_table"];
            param = new List<DataParameter>();
            if (!string.IsNullOrEmpty(textBox19.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label19.Text.Trim());
                param.Add(new DataParameter(label19.Text.Trim(), DbType.String, 50, textBox19.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox20.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label20.Text.Trim());
                param.Add(new DataParameter(label20.Text.Trim(), DbType.String, 50, textBox20.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox21.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label21.Text.Trim());
                param.Add(new DataParameter(label21.Text.Trim(), DbType.String, 50, textBox21.Text.Trim()));
            }
        }
        int pager5_EventPaging(Control.EventPagingArg e)
        {

            string strCondition;
            string tableName;
            List<DataParameter> param;
            GetPager5Condition(out strCondition, out tableName, out param);

            decimal rowsCount = db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DataTable dt = db.ExecuteDataTablePaged(tableName, "*", "", strCondition, param, getOrderBy(this.dataGridView5), (this.pager5.PageCurrent - 1) * this.pager5.PageSize, (this.pager5.PageCurrent) * this.pager5.PageSize);
            this.dataGridView5.DataSource = dt;
            return (int)rowsCount;
        }

        private void GetPager5Condition(out string strCondition, out string tableName, out List<DataParameter> param)
        {
            strCondition = "";
            tableName = ConfigurationManager.AppSettings["mover_shaker_asin_table"];
            param = new List<DataParameter>();
            if (!string.IsNullOrEmpty(textBox22.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label22.Text.Trim());
                param.Add(new DataParameter(label22.Text.Trim(), DbType.String, 50, textBox22.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox23.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label23.Text.Trim());
                param.Add(new DataParameter(label23.Text.Trim(), DbType.String, 50, textBox23.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox24.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label24.Text.Trim());
                param.Add(new DataParameter(label24.Text.Trim(), DbType.String, 50, textBox24.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox25.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label25.Text.Trim());
                param.Add(new DataParameter(label25.Text.Trim(), DbType.String, 50, textBox25.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox26.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label26.Text.Trim());
                param.Add(new DataParameter(label26.Text.Trim(), DbType.String, 50, textBox26.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox27.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label27.Text.Trim());
                param.Add(new DataParameter(label27.Text.Trim(), DbType.String, 50, textBox27.Text.Trim()));
            }
        }
        int pager6_EventPaging(Control.EventPagingArg e)
        {

            string strCondition;
            string tableName;
            List<DataParameter> param;
            GetPager6Condition(out strCondition, out tableName, out param);

            decimal rowsCount = db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DataTable dt = db.ExecuteDataTablePaged(tableName, "*", "", strCondition, param, getOrderBy(this.dataGridView6), (this.pager6.PageCurrent - 1) * this.pager6.PageSize, (this.pager6.PageCurrent) * this.pager6.PageSize);
            this.dataGridView6.DataSource = dt;
            return (int)rowsCount;
        }

        private void GetPager6Condition(out string strCondition, out string tableName, out List<DataParameter> param)
        {
            strCondition = "";
            tableName = ConfigurationManager.AppSettings["mover_shaker_brand_table"];
            param = new List<DataParameter>();
            if (!string.IsNullOrEmpty(textBox28.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label28.Text.Trim());
                param.Add(new DataParameter(label28.Text.Trim(), DbType.String, 50, textBox28.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox29.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label29.Text.Trim());
                param.Add(new DataParameter(label29.Text.Trim(), DbType.String, 50, textBox29.Text.Trim()));
            }
        }
        int pager7_EventPaging(Control.EventPagingArg e)
        {

            string strCondition;
            string tableName;
            List<DataParameter> param;
            GetPager7Condition(out strCondition, out tableName, out param);

            decimal rowsCount = db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DataTable dt = db.ExecuteDataTablePaged(tableName, "*", "", strCondition, param, getOrderBy(this.dataGridView7), (this.pager7.PageCurrent - 1) * this.pager7.PageSize, (this.pager7.PageCurrent) * this.pager7.PageSize);
            this.dataGridView7.DataSource = dt;
            return (int)rowsCount;
        }

        private void GetPager7Condition(out string strCondition, out string tableName, out List<DataParameter> param)
        {
            strCondition = "";
            tableName = ConfigurationManager.AppSettings["top_asin_with_limited_match_table"];
            param = new List<DataParameter>();
            if (!string.IsNullOrEmpty(textBox31.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label31.Text.Trim());
                param.Add(new DataParameter(label31.Text.Trim(), DbType.String, 50, textBox31.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox32.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label32.Text.Trim());
                param.Add(new DataParameter(label32.Text.Trim(), DbType.String, 50, textBox32.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox34.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label34.Text.Trim());
                param.Add(new DataParameter(label34.Text.Trim(), DbType.String, 50, textBox34.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox33.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label33.Text.Trim());
                param.Add(new DataParameter(label33.Text.Trim(), DbType.String, 50, textBox33.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox35.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label35.Text.Trim());
                param.Add(new DataParameter(label35.Text.Trim(), DbType.String, 50, textBox35.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox30.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label30.Text.Trim());
                param.Add(new DataParameter(label30.Text.Trim(), DbType.String, 50, textBox30.Text.Trim()));
            }
        }
        int pager8_EventPaging(Control.EventPagingArg e)
        {

            string strCondition;
            string tableName;
            List<DataParameter> param;
            GetPager8Condition(out strCondition, out tableName, out param);

            decimal rowsCount = db.ExecuteScalar("select count(1) from " + tableName + " where 1=1 " + strCondition, param);
            DataTable dt = db.ExecuteDataTablePaged(tableName, "*", "", strCondition, param, getOrderBy(this.dataGridView8), (this.pager8.PageCurrent - 1) * this.pager8.PageSize, (this.pager8.PageCurrent) * this.pager8.PageSize);
            this.dataGridView8.DataSource = dt;
            return (int)rowsCount;
        }

        private void GetPager8Condition(out string strCondition, out string tableName, out List<DataParameter> param)
        {
            strCondition = "";
            tableName = ConfigurationManager.AppSettings["top_conversion_rate_table"];
            param = new List<DataParameter>();
            if (!string.IsNullOrEmpty(textBox37.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label37.Text.Trim());
                param.Add(new DataParameter(label37.Text.Trim(), DbType.String, 50, textBox37.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox38.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label38.Text.Trim());
                param.Add(new DataParameter(label38.Text.Trim(), DbType.String, 50, textBox38.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox40.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label40.Text.Trim());
                param.Add(new DataParameter(label40.Text.Trim(), DbType.String, 50, textBox40.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox39.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label39.Text.Trim());
                param.Add(new DataParameter(label39.Text.Trim(), DbType.String, 50, textBox39.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox41.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label41.Text.Trim());
                param.Add(new DataParameter(label41.Text.Trim(), DbType.String, 50, textBox41.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(textBox36.Text.Trim()))
            {
                strCondition += string.Format(" and {0} like '%' || :{0} || '%'", this.label36.Text.Trim());
                param.Add(new DataParameter(label36.Text.Trim(), DbType.String, 50, textBox36.Text.Trim()));
            }
        }
        ~Form1()
        {
            if (db != null)
                db.CloseConn();

        }

        private void importFileTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = string.Join(";", openFileDialog.FileNames);

            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            string[] files = this.textBox1.Text.Split(';');
            this.label1.Text = "";

            foreach (string path in files)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("文件[{0}]不存在.", path));
                    return;
                }
            }
            this.textBox1.Enabled = false;
            this.linkLabel1.Enabled = false;
            object[] para = new object[2];
            para[0] = 0;
            para[1] = files;
            this.backgroundWorker1.RunWorkerAsync(para);
        }
        private void linkLabel2_Click(object sender, EventArgs e)
        {
            string[] files = this.textBox2.Text.Split(';');
            this.label2.Text = "";

            foreach (string path in files)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("文件[{0}]不存在.", path));
                    return;
                }
            }
            this.textBox2.Enabled = false;
            this.linkLabel2.Enabled = false;
            object[] para = new object[2];
            para[0] = 1;
            para[1] = files;
            this.backgroundWorker2.RunWorkerAsync(para);
        }
        private void linkLabel3_Click(object sender, EventArgs e)
        {
            string[] files = this.textBox3.Text.Split(';');
            this.label3.Text = "";

            foreach (string path in files)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("文件[{0}]不存在.", path));
                    return;
                }
            }
            this.textBox3.Enabled = false;
            this.linkLabel3.Enabled = false;
            object[] para = new object[2];
            para[0] = 2;
            para[1] = files;
            this.backgroundWorker3.RunWorkerAsync(para);
        }
        private void linkLabel4_Click(object sender, EventArgs e)
        {
            string[] files = this.textBox4.Text.Split(';');
            this.label4.Text = "";

            foreach (string path in files)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("文件[{0}]不存在.", path));
                    return;
                }
            }
            this.textBox4.Enabled = false;
            this.linkLabel4.Enabled = false;
            object[] para = new object[2];
            para[0] = 3;
            para[1] = files;
            this.backgroundWorker4.RunWorkerAsync(para);
        }
        private void linkLabel5_Click(object sender, EventArgs e)
        {
            string[] files = this.textBox5.Text.Split(';');
            this.label5.Text = "";

            foreach (string path in files)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("文件[{0}]不存在.", path));
                    return;
                }
            }
            this.textBox5.Enabled = false;
            this.linkLabel5.Enabled = false;
            object[] para = new object[2];
            para[0] = 4;
            para[1] = files;
            this.backgroundWorker5.RunWorkerAsync(para);
        }
        private void linkLabel6_Click(object sender, EventArgs e)
        {
            string[] files = this.textBox6.Text.Split(';');
            this.label6.Text = "";

            foreach (string path in files)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("文件[{0}]不存在.", path));
                    return;
                }
            }
            this.textBox6.Enabled = false;
            this.linkLabel6.Enabled = false;
            object[] para = new object[2];
            para[0] = 5;
            para[1] = files;
            this.backgroundWorker6.RunWorkerAsync(para);
        }
        private void linkLabel7_Click(object sender, EventArgs e)
        {
            string[] files = this.textBox7.Text.Split(';');
            this.label7.Text = "";

            foreach (string path in files)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("文件[{0}]不存在.", path));
                    return;
                }
            }
            this.textBox7.Enabled = false;
            this.linkLabel7.Enabled = false;
            object[] para = new object[2];
            para[0] = 6;
            para[1] = files;
            this.backgroundWorker7.RunWorkerAsync(para);
        }
        private void linkLabel8_Click(object sender, EventArgs e)
        {
            string[] files = this.textBox8.Text.Split(';');
            this.label8.Text = "";

            foreach (string path in files)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("文件[{0}]不存在.", path));
                    return;
                }
            }
            this.textBox8.Enabled = false;
            this.linkLabel8.Enabled = false;
            object[] para = new object[2];
            para[0] = 7;
            para[1] = files;
            this.backgroundWorker8.RunWorkerAsync(para);
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] para = (object[])e.Argument;
            int tabInx = (int)para[0];
            string[] files = (string[])para[1];
            string strHead = "";
            string strDelimited = "";
            string strMap = "";
            string strTable = "";
            bool clear = true;

            switch (tabInx)
            {
                case 0:
                    strHead = ConfigurationManager.AppSettings["top_asin_head"];
                    strDelimited = ConfigurationManager.AppSettings["top_asin_delimited"];
                    strMap = ConfigurationManager.AppSettings["top_asin_map"];
                    strTable = ConfigurationManager.AppSettings["top_asin_table"];
                    clear = this.checkBox1.Checked;
                    break;
                case 1:
                    strHead = ConfigurationManager.AppSettings["top_seller_head"];
                    strDelimited = ConfigurationManager.AppSettings["top_seller_delimited"];
                    strMap = ConfigurationManager.AppSettings["top_seller_map"];
                    strTable = ConfigurationManager.AppSettings["top_seller_table"];
                    clear = this.checkBox2.Checked;
                    break;
                case 2:
                    strHead = ConfigurationManager.AppSettings["top_brand_head"];
                    strDelimited = ConfigurationManager.AppSettings["top_brand_delimited"];
                    strMap = ConfigurationManager.AppSettings["top_brand_map"];
                    strTable = ConfigurationManager.AppSettings["top_brand_table"];
                    clear = this.checkBox3.Checked;
                    break;
                case 3:
                    strHead = ConfigurationManager.AppSettings["top_subcategory_head"];
                    strDelimited = ConfigurationManager.AppSettings["top_subcategory_delimited"];
                    strMap = ConfigurationManager.AppSettings["top_subcategory_map"];
                    strTable = ConfigurationManager.AppSettings["top_subcategory_table"];
                    clear = this.checkBox4.Checked;
                    break;
                case 4:
                    strHead = ConfigurationManager.AppSettings["mover_shaker_asin_head"];
                    strDelimited = ConfigurationManager.AppSettings["mover_shaker_asin_delimited"];
                    strMap = ConfigurationManager.AppSettings["mover_shaker_asin_map"];
                    strTable = ConfigurationManager.AppSettings["mover_shaker_asin_table"];
                    clear = this.checkBox5.Checked;
                    break;
                case 5:
                    strHead = ConfigurationManager.AppSettings["mover_shaker_brand_head"];
                    strDelimited = ConfigurationManager.AppSettings["mover_shaker_brand_delimited"];
                    strMap = ConfigurationManager.AppSettings["mover_shaker_brand_map"];
                    strTable = ConfigurationManager.AppSettings["mover_shaker_brand_table"];
                    clear = this.checkBox6.Checked;
                    break;
                case 6:
                    strHead = ConfigurationManager.AppSettings["top_asin_with_limited_match_head"];
                    strDelimited = ConfigurationManager.AppSettings["top_asin_with_limited_match_delimited"];
                    strMap = ConfigurationManager.AppSettings["top_asin_with_limited_match_map"];
                    strTable = ConfigurationManager.AppSettings["top_asin_with_limited_match_table"];
                    clear = this.checkBox7.Checked;
                    break;
                case 7:
                    strHead = ConfigurationManager.AppSettings["top_conversion_rate_head"];
                    strDelimited = ConfigurationManager.AppSettings["top_conversion_rate_delimited"];
                    strMap = ConfigurationManager.AppSettings["top_conversion_rate_map"];
                    strTable = ConfigurationManager.AppSettings["top_conversion_rate_table"];
                    clear = this.checkBox8.Checked;
                    break;
            }
            ImportTxt((BackgroundWorker)sender, tabInx, clear, files, strHead, strDelimited, strMap, strTable);
            e.Result = tabInx;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage < 0)
            {
                MessageBox.Show(e.UserState.ToString());
            }
            else
            {
                switch (e.ProgressPercentage)
                {
                    case 0:
                        this.label1.Text = e.UserState.ToString();
                        break;
                    case 1:
                        this.label2.Text = e.UserState.ToString();
                        break;
                    case 2:
                        this.label3.Text = e.UserState.ToString();
                        break;
                    case 3:
                        this.label4.Text = e.UserState.ToString();
                        break;
                    case 4:
                        this.label5.Text = e.UserState.ToString();
                        break;
                    case 5:
                        this.label6.Text = e.UserState.ToString();
                        break;
                    case 6:
                        this.label7.Text = e.UserState.ToString();
                        break;
                    case 7:
                        this.label8.Text = e.UserState.ToString();
                        break;
                }
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch ((int)e.Result)
            {
                case 0:
                    this.textBox1.Enabled = true;
                    this.linkLabel1.Enabled = true;
                    break;
                case 1:
                    this.textBox2.Enabled = true;
                    this.linkLabel2.Enabled = true;
                    break;
                case 2:
                    this.textBox3.Enabled = true;
                    this.linkLabel3.Enabled = true;
                    break;
                case 3:
                    this.textBox4.Enabled = true;
                    this.linkLabel4.Enabled = true;
                    break;
                case 4:
                    this.textBox5.Enabled = true;
                    this.linkLabel5.Enabled = true;
                    break;
                case 5:
                    this.textBox6.Enabled = true;
                    this.linkLabel6.Enabled = true;
                    break;
                case 6:
                    this.textBox7.Enabled = true;
                    this.linkLabel7.Enabled = true;
                    break;
                case 7:
                    this.textBox8.Enabled = true;
                    this.linkLabel8.Enabled = true;
                    break;
            }
        }
        private void ImportTxt(BackgroundWorker sender, int tabInx, bool clear, string[] files, string head, string delimited, string colMap, string table)
        {
            int totalRows = 0;
            long totalTime = 0;

            if (clear)
            {
                db.ExecuteNoQuery(string.Format("truncate table {0}", table), null);
            }

            foreach (string path in files)
            {
                sender.ReportProgress(tabInx, string.Format("当前文件：{0}", Path.GetFileName(path)));
                string strErr = string.Empty;
                int importRows = 0;
                long spentTime = 0;

                Dictionary<string, string> columnMap = new Dictionary<string, string>();
                foreach (string str in colMap.Split(';'))
                {
                    var arr = str.Split(',');
                    columnMap.Add(arr[0], arr[1]);
                }

                bool flag = db.FastImportTextToDb(conn_string, 1000, timeout, path, head, delimited, table, columnMap, null, 2, "", out importRows, out spentTime, out strErr);

                totalRows += importRows;
                totalTime += spentTime;
                if (flag)
                {
                    sender.ReportProgress(tabInx, string.Format("已处理{0}条数据，用时{1}秒", totalRows
                        , totalTime / 1000));
                }
                else
                {
                    sender.ReportProgress(-1, string.Format("导入失败,已处理{0}条数据：{1}", totalRows, strErr));
                }
            }
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pager1.Bind();
        }

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pager2.Bind();
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pager3.Bind();
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pager4.Bind();
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pager5.Bind();
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pager6.Bind();
        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pager7.Bind();
        }

        private void linkLabel16_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pager8.Bind();
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataGridView)sender).Columns[e.ColumnIndex].DataPropertyName == "ASIN")
            {
                System.Diagnostics.Process.Start(string.Format("{0}{1}", asinUrl, ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
            }
        }
    }
}
