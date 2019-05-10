namespace Timor.HomeWork.AotuCreateClass
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.create_btn = new System.Windows.Forms.Button();
            this.selectSavePath_btn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.text_namespace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // create_btn
            // 
            this.create_btn.Location = new System.Drawing.Point(312, 62);
            this.create_btn.Name = "create_btn";
            this.create_btn.Size = new System.Drawing.Size(98, 23);
            this.create_btn.TabIndex = 0;
            this.create_btn.Text = "创建";
            this.create_btn.UseVisualStyleBackColor = true;
            this.create_btn.Click += new System.EventHandler(this.create_btn_Click);
            // 
            // selectSavePath_btn
            // 
            this.selectSavePath_btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectSavePath_btn.Location = new System.Drawing.Point(313, 29);
            this.selectSavePath_btn.Name = "selectSavePath_btn";
            this.selectSavePath_btn.Size = new System.Drawing.Size(97, 23);
            this.selectSavePath_btn.TabIndex = 0;
            this.selectSavePath_btn.Text = "选择保存路径";
            this.selectSavePath_btn.UseVisualStyleBackColor = true;
            this.selectSavePath_btn.Click += new System.EventHandler(this.selectSavePath_btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "保存路径：";
            // 
            // text_namespace
            // 
            this.text_namespace.Location = new System.Drawing.Point(97, 64);
            this.text_namespace.Name = "text_namespace";
            this.text_namespace.Size = new System.Drawing.Size(210, 21);
            this.text_namespace.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "命名空间名称：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 268);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_namespace);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.selectSavePath_btn);
            this.Controls.Add(this.create_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button create_btn;
        private System.Windows.Forms.Button selectSavePath_btn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_namespace;
        private System.Windows.Forms.Label label2;
    }
}

