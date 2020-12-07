using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MTTS.IND890.CE;

namespace IND890APIClientTool_NonIPC
{
    public partial class frmProductDetail : Form
    {
        private int SelectedIndex = 0;
        private CProductDetail productObj;
        private CDBFunctions m_DBFunction;

        #region Constructor
        public frmProductDetail()
        {
            InitializeComponent();
            productObj = new CProductDetail();
            m_DBFunction = CDBFunctions.GetInstance();
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            bool updateResult = false;
            productObj.ProductNumber = Convert.ToInt32(txtProductNo.Text);
            productObj.ProductName = txtProductName.Text;
            productObj.ProductWeight = txtWeight.Text;
            productObj.ActualProductWeight = txtActualWeight.Text;
            if (string.IsNullOrEmpty(txtProductName.Text.ToString()))
            {
                MessageBox.Show("Please Enter Product Name");
                return;
            }
            if (string.IsNullOrEmpty(txtWeight.Text.ToString()))
            {
                MessageBox.Show("Please Enter Product Weight");
                return;
            }
            if (cmbScaleId.SelectedIndex <= -1)
            {
                MessageBox.Show("Please select scale number!");
                return;
            }
            productObj.Scale = Convert.ToByte(cmbScaleId.Items[SelectedIndex].ToString());
            if (productObj != null)
                updateResult = m_DBFunction.UpdateRecord(productObj);
            if (updateResult)
                this.Close();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            bool result = false;
            productObj.ProductNumber = Convert.ToInt32(txtProductNo.Text);
            productObj.ProductName = txtProductName.Text;
            productObj.ProductWeight = txtWeight.Text;
            productObj.ActualProductWeight = txtActualWeight.Text;

            if (string.IsNullOrEmpty(txtProductName.Text.ToString()))
            {
                MessageBox.Show("Please Enter Product Name");
                return;
            }
            if (string.IsNullOrEmpty(txtWeight.Text.ToString()))
            {
                MessageBox.Show("Please Enter Product Weight");
                return;
            }

            if (cmbScaleId.SelectedIndex <= -1)
            {
                MessageBox.Show("Please select scale number!");
                return;
            }
            productObj.Scale = Convert.ToByte(cmbScaleId.Items[SelectedIndex].ToString());
            if (productObj != null)
                result = m_DBFunction.AddRecord(productObj);
            this.Close();
        }

        private void cmbScaleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndex = cmbScaleId.SelectedIndex;
            productObj.Scale = Convert.ToByte(cmbScaleId.Items[SelectedIndex].ToString());
        }

        private void ProductDetail_Load(object sender, EventArgs e)
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width/2);
            int y = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2);
            Location = new Point(x, y);

            if (btnAddProduct.Enabled)
            {
                int rowCount = m_DBFunction.GetLastRowCount();
                if (rowCount == -1) rowCount = 0;
                txtProductNo.Text = (rowCount + 1).ToString();
                txtProductNo.Enabled = false;
                txtProductName.Text = "";
                txtWeight.Text = "";
                txtActualWeight.Text = "";
                cmbScaleId.SelectedIndex=-1;
                btnUpdateProduct.Visible = false;
            }
            else
            {
                btnUpdateProduct.Visible = true;
                txtProductNo.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region public
        /// <summary>
        /// Update value from object into textbox.
        /// </summary>
        /// <param name="prodObj"></param>
        public void UpdateValue(CProductDetail prodObj)
        {
            txtProductNo.Text = prodObj.ProductNumber.ToString();
            txtProductName.Text = prodObj.ProductName.ToString();
            txtWeight.Text = prodObj.ProductWeight.ToString();
            txtActualWeight.Text = prodObj.ActualProductWeight.ToString();
            cmbScaleId.SelectedIndex = Convert.ToInt32(prodObj.Scale.ToString());
        }
        #endregion
    }
}