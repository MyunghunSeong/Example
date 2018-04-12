namespace SocketClient_Example
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_port = new System.Windows.Forms.Label();
            this.tx_Ip = new System.Windows.Forms.TextBox();
            this.tx_Port = new System.Windows.Forms.TextBox();
            this.lb_Ip = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_Msg = new System.Windows.Forms.Label();
            this.tx_msg = new System.Windows.Forms.TextBox();
            this.tx_Recv = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.btn_connect = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lb_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel1.Controls.Add(this.lb_port, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tx_Ip, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tx_Port, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_Ip, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(412, 30);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lb_port
            // 
            this.lb_port.AutoSize = true;
            this.lb_port.Font = new System.Drawing.Font("굴림", 10F);
            this.lb_port.Location = new System.Drawing.Point(203, 0);
            this.lb_port.Name = "lb_port";
            this.lb_port.Size = new System.Drawing.Size(34, 14);
            this.lb_port.TabIndex = 3;
            this.lb_port.Text = "Port";
            this.lb_port.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tx_Ip
            // 
            this.tx_Ip.Location = new System.Drawing.Point(53, 3);
            this.tx_Ip.Name = "tx_Ip";
            this.tx_Ip.Size = new System.Drawing.Size(144, 21);
            this.tx_Ip.TabIndex = 0;
            // 
            // tx_Port
            // 
            this.tx_Port.Location = new System.Drawing.Point(253, 3);
            this.tx_Port.Name = "tx_Port";
            this.tx_Port.Size = new System.Drawing.Size(156, 21);
            this.tx_Port.TabIndex = 1;
            // 
            // lb_Ip
            // 
            this.lb_Ip.AutoSize = true;
            this.lb_Ip.Font = new System.Drawing.Font("굴림", 10F);
            this.lb_Ip.Location = new System.Drawing.Point(3, 0);
            this.lb_Ip.Name = "lb_Ip";
            this.lb_Ip.Size = new System.Drawing.Size(20, 14);
            this.lb_Ip.TabIndex = 2;
            this.lb_Ip.Text = "IP";
            this.lb_Ip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lb_Msg, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tx_msg, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 48);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(412, 28);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lb_Msg
            // 
            this.lb_Msg.AutoSize = true;
            this.lb_Msg.Location = new System.Drawing.Point(3, 0);
            this.lb_Msg.Name = "lb_Msg";
            this.lb_Msg.Size = new System.Drawing.Size(58, 12);
            this.lb_Msg.TabIndex = 0;
            this.lb_Msg.Text = "Message";
            this.lb_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tx_msg
            // 
            this.tx_msg.Location = new System.Drawing.Point(73, 3);
            this.tx_msg.Name = "tx_msg";
            this.tx_msg.Size = new System.Drawing.Size(336, 21);
            this.tx_msg.TabIndex = 1;
            // 
            // tx_Recv
            // 
            this.tx_Recv.Location = new System.Drawing.Point(17, 111);
            this.tx_Recv.Multiline = true;
            this.tx_Recv.Name = "tx_Recv";
            this.tx_Recv.ReadOnly = true;
            this.tx_Recv.Size = new System.Drawing.Size(403, 140);
            this.tx_Recv.TabIndex = 2;
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(345, 82);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 3;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(18, 82);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_connect.TabIndex = 4;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lb_status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 265);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(436, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lb_status
            // 
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(57, 17);
            this.lb_status.Text = "Waiting...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 287);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.tx_Recv);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lb_port;
        private System.Windows.Forms.TextBox tx_Ip;
        private System.Windows.Forms.TextBox tx_Port;
        private System.Windows.Forms.Label lb_Ip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lb_Msg;
        private System.Windows.Forms.TextBox tx_msg;
        private System.Windows.Forms.TextBox tx_Recv;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lb_status;
    }
}

