namespace SCSIApplication
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_deviceList = new System.Windows.Forms.ComboBox();
            this.button_refresh = new System.Windows.Forms.Button();
            this.btn_get_info = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.button_copy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_cid = new System.Windows.Forms.TextBox();
            this.textBox_detail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 36);
            this.label1.TabIndex = 14;
            this.label1.Text = "Disk :";
            // 
            // comboBox_deviceList
            // 
            this.comboBox_deviceList.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_deviceList.FormattingEnabled = true;
            this.comboBox_deviceList.Location = new System.Drawing.Point(169, 15);
            this.comboBox_deviceList.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox_deviceList.Name = "comboBox_deviceList";
            this.comboBox_deviceList.Size = new System.Drawing.Size(387, 37);
            this.comboBox_deviceList.TabIndex = 15;
            this.comboBox_deviceList.SelectedIndexChanged += new System.EventHandler(this.comboBox_deviceList_SelectedIndexChanged);
            // 
            // button_refresh
            // 
            this.button_refresh.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_refresh.Location = new System.Drawing.Point(632, 14);
            this.button_refresh.Margin = new System.Windows.Forms.Padding(5);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(160, 44);
            this.button_refresh.TabIndex = 16;
            this.button_refresh.Text = "Refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // btn_get_info
            // 
            this.btn_get_info.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_get_info.Location = new System.Drawing.Point(20, 76);
            this.btn_get_info.Margin = new System.Windows.Forms.Padding(0);
            this.btn_get_info.Name = "btn_get_info";
            this.btn_get_info.Size = new System.Drawing.Size(772, 62);
            this.btn_get_info.TabIndex = 17;
            this.btn_get_info.Text = "Get CID";
            this.btn_get_info.UseVisualStyleBackColor = true;
            this.btn_get_info.Click += new System.EventHandler(this.btn_get_info_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(805, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // button_copy
            // 
            this.button_copy.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_copy.Location = new System.Drawing.Point(685, 154);
            this.button_copy.Margin = new System.Windows.Forms.Padding(5);
            this.button_copy.Name = "button_copy";
            this.button_copy.Size = new System.Drawing.Size(107, 44);
            this.button_copy.TabIndex = 20;
            this.button_copy.Text = "Copy CID";
            this.button_copy.UseVisualStyleBackColor = true;
            this.button_copy.Visible = false;
            this.button_copy.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(330, 405);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.MinimumSize = new System.Drawing.Size(67, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(464, 38);
            this.label2.TabIndex = 22;
            this.label2.Text = "The displayed 15 byte digits are CID without CRC7+end bit";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_cid
            // 
            this.textBox_cid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_cid.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_cid.Location = new System.Drawing.Point(20, 159);
            this.textBox_cid.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_cid.MinimumSize = new System.Drawing.Size(67, 30);
            this.textBox_cid.Name = "textBox_cid";
            this.textBox_cid.ReadOnly = true;
            this.textBox_cid.Size = new System.Drawing.Size(659, 28);
            this.textBox_cid.TabIndex = 23;
            this.textBox_cid.TabStop = false;
            // 
            // textBox_detail
            // 
            this.textBox_detail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_detail.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_detail.Location = new System.Drawing.Point(27, 199);
            this.textBox_detail.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_detail.MinimumSize = new System.Drawing.Size(0, 25);
            this.textBox_detail.Multiline = true;
            this.textBox_detail.Name = "textBox_detail";
            this.textBox_detail.ReadOnly = true;
            this.textBox_detail.Size = new System.Drawing.Size(641, 211);
            this.textBox_detail.TabIndex = 24;
            this.textBox_detail.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 440);
            this.Controls.Add(this.textBox_detail);
            this.Controls.Add(this.textBox_cid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_copy);
            this.Controls.Add(this.btn_get_info);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.comboBox_deviceList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Transcend RDF5 SD Card CID Reader v1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_deviceList;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Button btn_get_info;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button_copy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_cid;
        private System.Windows.Forms.TextBox textBox_detail;
    }
}

