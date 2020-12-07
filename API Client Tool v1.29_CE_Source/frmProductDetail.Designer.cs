namespace IND890APIClientTool_NonIPC
{
    partial class frmProductDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnUpdateProduct = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.cmbScaleId = new System.Windows.Forms.ComboBox();
            this.labelscale = new System.Windows.Forms.Label();
            this.txtActualWeight = new System.Windows.Forms.TextBox();
            this.labelActualWeight = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.labelweight = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProductNo = new System.Windows.Forms.TextBox();
            this.labelNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpdateProduct
            // 
            this.btnUpdateProduct.Location = new System.Drawing.Point(327, 13);
            this.btnUpdateProduct.Name = "btnUpdateProduct";
            this.btnUpdateProduct.Size = new System.Drawing.Size(114, 38);
            this.btnUpdateProduct.TabIndex = 76;
            this.btnUpdateProduct.Text = "Update";
            this.btnUpdateProduct.Visible = false;
            this.btnUpdateProduct.Click += new System.EventHandler(this.btnUpdateProduct_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 71);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 38);
            this.btnCancel.TabIndex = 75;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(327, 13);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(114, 38);
            this.btnAddProduct.TabIndex = 74;
            this.btnAddProduct.Text = "ADD";
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // cmbScaleId
            // 
            this.cmbScaleId.Items.Add("1");
            this.cmbScaleId.Items.Add("2");
            this.cmbScaleId.Items.Add("3");
            this.cmbScaleId.Items.Add("4");
            this.cmbScaleId.Location = new System.Drawing.Point(135, 208);
            this.cmbScaleId.Name = "cmbScaleId";
            this.cmbScaleId.Size = new System.Drawing.Size(116, 23);
            this.cmbScaleId.TabIndex = 72;
            // 
            // labelscale
            // 
            this.labelscale.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelscale.Location = new System.Drawing.Point(13, 215);
            this.labelscale.Name = "labelscale";
            this.labelscale.Size = new System.Drawing.Size(43, 16);
            this.labelscale.Text = "Scale";
            // 
            // txtActualWeight
            // 
            this.txtActualWeight.Location = new System.Drawing.Point(135, 161);
            this.txtActualWeight.Name = "txtActualWeight";
            this.txtActualWeight.Size = new System.Drawing.Size(116, 23);
            this.txtActualWeight.TabIndex = 71;
            // 
            // labelActualWeight
            // 
            this.labelActualWeight.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelActualWeight.Location = new System.Drawing.Point(13, 161);
            this.labelActualWeight.Name = "labelActualWeight";
            this.labelActualWeight.Size = new System.Drawing.Size(107, 31);
            this.labelActualWeight.Text = "Actual Weight";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(135, 108);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(116, 23);
            this.txtWeight.TabIndex = 70;
            // 
            // labelweight
            // 
            this.labelweight.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelweight.Location = new System.Drawing.Point(12, 108);
            this.labelweight.Name = "labelweight";
            this.labelweight.Size = new System.Drawing.Size(119, 23);
            this.labelweight.Text = "Weight  (with unit)";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(135, 60);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(116, 23);
            this.txtProductName.TabIndex = 69;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(13, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 23);
            this.label3.Text = "Product Name";
            // 
            // txtProductNo
            // 
            this.txtProductNo.Location = new System.Drawing.Point(135, 13);
            this.txtProductNo.Name = "txtProductNo";
            this.txtProductNo.Size = new System.Drawing.Size(116, 23);
            this.txtProductNo.TabIndex = 68;
            // 
            // labelNumber
            // 
            this.labelNumber.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelNumber.Location = new System.Drawing.Point(13, 13);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(118, 23);
            this.labelNumber.Text = "Product Number";
            // 
            // frmProductDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(457, 295);
            this.Controls.Add(this.btnUpdateProduct);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.cmbScaleId);
            this.Controls.Add(this.labelscale);
            this.Controls.Add(this.txtActualWeight);
            this.Controls.Add(this.labelActualWeight);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.labelweight);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProductNo);
            this.Controls.Add(this.labelNumber);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProductDetail";
            this.Text = "ProductDetail";
            this.Load += new System.EventHandler(this.ProductDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnUpdateProduct;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.ComboBox cmbScaleId;
        private System.Windows.Forms.Label labelscale;
        private System.Windows.Forms.TextBox txtActualWeight;
        private System.Windows.Forms.Label labelActualWeight;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label labelweight;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProductNo;
        private System.Windows.Forms.Label labelNumber;
    }
}