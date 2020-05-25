using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSEViewer
{
    public partial class Form1 : Form
    {
        List<ReqIndustryBySector> CompaniesWithDetails; 

        public Form1()
        {
            InitializeComponent();
            txtInvestment.Text = "100000";
            ExternalDataHelper.Load();
            LoadSectors();
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Exit without saving ?" , "Save?", MessageBoxButtons.YesNo ) != DialogResult.Yes )
            {
                e.Cancel = true;
            }
        }

        void LoadSectors()
        {
            this.Cursor = Cursors.WaitCursor; 
            var sectors = CSEHelper.GetSectors();
            cmbSectors.DataSource = sectors;
            cmbSectors.DisplayMember = "name";
            this.Cursor = Cursors.Arrow;
        }
         
        private void cmbSectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = ((Sector)cmbSectors.SelectedValue).id;

            Cursor = Cursors.WaitCursor;
            CompaniesWithDetails = CSEHelper.GetCompanieBySector(selectedValue ,txtInvestment.EditValue == null ? 0 :  double.Parse( txtInvestment.EditValue.ToString()));
            gridControl1.DataSource = CompaniesWithDetails;   
            Cursor = Cursors.Arrow;
        }

        private void btnSaveModifiedExternalData_Click(object sender, EventArgs ev)
        {
            foreach(var e in  CompaniesWithDetails)
            {
                var m = ExternalDataHelper.data.Companies.FirstOrDefault(x => x.Symbol== e.symbol);
                if ( m == null) return;
                m.Dividence = e.Dividence; 
                m.NAVPS = e.NAVPS;
                m.EPS = e.EPS;
                m.CurrentRatio = e.CurrentRatio;
            }
            ExternalDataHelper.Save();
            MessageBox.Show("Saved Successfully");
        }
 

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hi = gridView1.CalcHitInfo(e.X, e.Y);
            if (hi.InRowCell & hi.Column != null && hi.Column.FieldName == "symbol")
            {
                var data = (ReqIndustryBySector)gridView1.GetRow(hi.RowHandle);
                System.Diagnostics.Process.Start(data.Profile);
            }
        }

        private void gridView1_BeforePrintRow(object sender, DevExpress.XtraGrid.Views.Printing.CancelPrintRowEventArgs e)
        {

        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == "EPS")
            {
                double value = Convert.ToDouble(currentView.GetRowCellValue(e.RowHandle, "EPS"));
                if (double.IsInfinity(value) | double.IsNaN(value))
                {
                    e.Appearance.BackColor = Color.Gray;
                    return;
                }

                if (value <= 0)
                {
                    e.Appearance.BackColor = Color.Orange;
                }
            }
            else if (e.Column.FieldName == "PEG")
            {
                double value = Convert.ToDouble(currentView.GetRowCellValue(e.RowHandle, "PEG"));
                if (double.IsInfinity(value) | double.IsNaN(value))
                { 
                    e.Appearance.BackColor = Color.Gray;
                    return;
                }

                if (value < .5f)
                {
                    //Strong Buy
                    e.Appearance.BackColor = Color.Green;
                }
                else if (value < 1)
                {
                    //Buy 
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else if (value == 1)
                {
                    //Fair Price
                }
                else if (value > 1.5)
                {
                    //Strong Sell
                    e.Appearance.BackColor = Color.Red;
                }
                else
                {
                    //Sell
                    e.Appearance.BackColor = Color.Orange;
                }
            }
            else if (e.Column.FieldName == "CurrentRatio" | e.Column.FieldName == "CRStatus")
            {
                double value = Convert.ToDouble(currentView.GetRowCellValue(e.RowHandle, "CurrentRatio"));
                if (value < 1.5f)
                {
                    //Problem
                    e.Appearance.BackColor = Color.Orange;
                }
                else if (value == 1.5)
                {
                    //Normal
                }
                else if (value > 2)
                {
                    //Why ?  - Find out why value is greater than two.
                    e.Appearance.BackColor = Color.LightBlue;
                }
                else if (value > 1.5)
                {
                    //Good
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
            else if (e.Column.FieldName == "symbol")
            {
                string value = currentView.GetRowCellValue(e.RowHandle, "symbol").ToString();
                if (value.Contains(".X"))
                {
                    e.Appearance.BackColor = Color.Orange;
                }
            }
            else if (e.Column.FieldName == "PNAVPS")
            {
               double value = Convert.ToDouble(currentView.GetRowCellValue(e.RowHandle, "PNAVPS"));
                if (double.IsInfinity(value) | double.IsNaN(value))
                {
                    e.Appearance.BackColor = Color.Gray;
                    return;
                }
                if (value <= 1.5)
                {
                    e.Appearance.BackColor = Color.LightGreen; 
                }
                else if (value <= 1)
                {
                    e.Appearance.BackColor = Color.Green;
                }
                else 
                {
                    e.Appearance.BackColor = Color.Orange;
                }  
            }
            else if (e.Column.FieldName == "Value")
            {
                double value = Convert.ToDouble(currentView.GetRowCellValue(e.RowHandle, "Value"));
                double price = Convert.ToDouble(currentView.GetRowCellValue(e.RowHandle, "price"));
                if (double.IsInfinity(value) | double.IsNaN(value))
                {
                    e.Appearance.BackColor = Color.Gray;
                    return;
                }

                if(price / value <= 1f)
                {
                    //Fair price
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else if (price / value <= 2/3f)
                {
                    //Good Safe Price
                        e.Appearance.BackColor = Color.Green;
                }
                else 
                {
                    //Bad price
                    e.Appearance.BackColor = Color.Orange;
                } 
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
