namespace Demo
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
            this.button1 = new System.Windows.Forms.Button();
            this.tbProduct = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tbQueueName = new System.Windows.Forms.TextBox();
            this.tbExchangeName = new System.Windows.Forms.TextBox();
            this.tbExchangeType = new System.Windows.Forms.TextBox();
            this.tbKeys = new System.Windows.Forms.TextBox();
            this.tbKeysC = new System.Windows.Forms.TextBox();
            this.tbExchangeTypeC = new System.Windows.Forms.TextBox();
            this.tbExchangeNameC = new System.Windows.Forms.TextBox();
            this.tbQueueNameC = new System.Windows.Forms.TextBox();
            this.tbId = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "生产者";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbProduct
            // 
            this.tbProduct.Location = new System.Drawing.Point(105, 12);
            this.tbProduct.Name = "tbProduct";
            this.tbProduct.Size = new System.Drawing.Size(75, 21);
            this.tbProduct.TabIndex = 1;
            this.tbProduct.Text = "1 2 3";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(24, 243);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(414, 195);
            this.txtLog.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "消费者1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(363, 101);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "全部停止";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbQueueName
            // 
            this.tbQueueName.Location = new System.Drawing.Point(186, 12);
            this.tbQueueName.Name = "tbQueueName";
            this.tbQueueName.Size = new System.Drawing.Size(75, 21);
            this.tbQueueName.TabIndex = 7;
            this.tbQueueName.Text = "queue";
            // 
            // tbExchangeName
            // 
            this.tbExchangeName.Location = new System.Drawing.Point(267, 12);
            this.tbExchangeName.Name = "tbExchangeName";
            this.tbExchangeName.Size = new System.Drawing.Size(75, 21);
            this.tbExchangeName.TabIndex = 8;
            this.tbExchangeName.Text = "ex";
            // 
            // tbExchangeType
            // 
            this.tbExchangeType.Location = new System.Drawing.Point(267, 39);
            this.tbExchangeType.Name = "tbExchangeType";
            this.tbExchangeType.Size = new System.Drawing.Size(75, 21);
            this.tbExchangeType.TabIndex = 9;
            this.tbExchangeType.Text = "fanout";
            // 
            // tbKeys
            // 
            this.tbKeys.Location = new System.Drawing.Point(267, 66);
            this.tbKeys.Name = "tbKeys";
            this.tbKeys.Size = new System.Drawing.Size(75, 21);
            this.tbKeys.TabIndex = 10;
            this.tbKeys.Text = "keys";
            // 
            // tbKeysC
            // 
            this.tbKeysC.Location = new System.Drawing.Point(267, 155);
            this.tbKeysC.Name = "tbKeysC";
            this.tbKeysC.Size = new System.Drawing.Size(75, 21);
            this.tbKeysC.TabIndex = 15;
            this.tbKeysC.Text = "keys";
            // 
            // tbExchangeTypeC
            // 
            this.tbExchangeTypeC.Location = new System.Drawing.Point(267, 128);
            this.tbExchangeTypeC.Name = "tbExchangeTypeC";
            this.tbExchangeTypeC.Size = new System.Drawing.Size(75, 21);
            this.tbExchangeTypeC.TabIndex = 14;
            this.tbExchangeTypeC.Text = "fanout";
            // 
            // tbExchangeNameC
            // 
            this.tbExchangeNameC.Location = new System.Drawing.Point(267, 101);
            this.tbExchangeNameC.Name = "tbExchangeNameC";
            this.tbExchangeNameC.Size = new System.Drawing.Size(75, 21);
            this.tbExchangeNameC.TabIndex = 13;
            this.tbExchangeNameC.Text = "ex";
            // 
            // tbQueueNameC
            // 
            this.tbQueueNameC.Location = new System.Drawing.Point(186, 101);
            this.tbQueueNameC.Name = "tbQueueNameC";
            this.tbQueueNameC.Size = new System.Drawing.Size(75, 21);
            this.tbQueueNameC.TabIndex = 12;
            this.tbQueueNameC.Text = "queue";
            // 
            // tbId
            // 
            this.tbId.Location = new System.Drawing.Point(105, 101);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(75, 21);
            this.tbId.TabIndex = 11;
            this.tbId.Text = "001";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(24, 190);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 47);
            this.button4.TabIndex = 16;
            this.button4.Text = "开启RPC服务";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(126, 190);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 47);
            this.button5.TabIndex = 17;
            this.button5.Text = "停止RPC服务";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(228, 190);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(96, 47);
            this.button6.TabIndex = 18;
            this.button6.Text = "RPC调用";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tbKeysC);
            this.Controls.Add(this.tbExchangeTypeC);
            this.Controls.Add(this.tbExchangeNameC);
            this.Controls.Add(this.tbQueueNameC);
            this.Controls.Add(this.tbId);
            this.Controls.Add(this.tbKeys);
            this.Controls.Add(this.tbExchangeType);
            this.Controls.Add(this.tbExchangeName);
            this.Controls.Add(this.tbQueueName);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.tbProduct);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbProduct;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbQueueName;
        private System.Windows.Forms.TextBox tbExchangeName;
        private System.Windows.Forms.TextBox tbExchangeType;
        private System.Windows.Forms.TextBox tbKeys;
        private System.Windows.Forms.TextBox tbKeysC;
        private System.Windows.Forms.TextBox tbExchangeTypeC;
        private System.Windows.Forms.TextBox tbExchangeNameC;
        private System.Windows.Forms.TextBox tbQueueNameC;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

