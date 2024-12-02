namespace ClothesBadmintonManagent
{
    partial class Form7
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.dTP_start = new System.Windows.Forms.DateTimePicker();
            this.dTP_end = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dGv_Statistic = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_dele = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGv_Statistic)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LawnGreen;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(273, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 38);
            this.label1.TabIndex = 46;
            this.label1.Text = "STATISTIC";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.LawnGreen;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(-2, -1);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1108, 90);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // dTP_start
            // 
            this.dTP_start.Location = new System.Drawing.Point(205, 124);
            this.dTP_start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dTP_start.Name = "dTP_start";
            this.dTP_start.Size = new System.Drawing.Size(307, 26);
            this.dTP_start.TabIndex = 48;
            // 
            // dTP_end
            // 
            this.dTP_end.Location = new System.Drawing.Point(205, 215);
            this.dTP_end.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dTP_end.Name = "dTP_end";
            this.dTP_end.Size = new System.Drawing.Size(307, 26);
            this.dTP_end.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 25);
            this.label2.TabIndex = 50;
            this.label2.Text = "START:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 25);
            this.label3.TabIndex = 51;
            this.label3.Text = "END:";
            // 
            // dGv_Statistic
            // 
            this.dGv_Statistic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGv_Statistic.Location = new System.Drawing.Point(-2, 315);
            this.dGv_Statistic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dGv_Statistic.Name = "dGv_Statistic";
            this.dGv_Statistic.ReadOnly = true;
            this.dGv_Statistic.RowHeadersWidth = 51;
            this.dGv_Statistic.RowTemplate.Height = 24;
            this.dGv_Statistic.Size = new System.Drawing.Size(682, 549);
            this.dGv_Statistic.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 25);
            this.label4.TabIndex = 54;
            this.label4.Text = "STATISTIC:";
            // 
            // btn_load
            // 
            this.btn_load.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_load.Location = new System.Drawing.Point(518, 124);
            this.btn_load.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(168, 52);
            this.btn_load.TabIndex = 57;
            this.btn_load.Text = "LOAD STATISTIC:";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_dele
            // 
            this.btn_dele.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dele.Location = new System.Drawing.Point(518, 184);
            this.btn_dele.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_dele.Name = "btn_dele";
            this.btn_dele.Size = new System.Drawing.Size(168, 52);
            this.btn_dele.TabIndex = 58;
            this.btn_dele.Text = "DELETE:";
            this.btn_dele.UseVisualStyleBackColor = true;
            this.btn_dele.Click += new System.EventHandler(this.btn_dele_Click);
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(682, 861);
            this.Controls.Add(this.btn_dele);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dGv_Statistic);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dTP_end);
            this.Controls.Add(this.dTP_start);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form7";
            this.Load += new System.EventHandler(this.Form7_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGv_Statistic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dTP_start;
        private System.Windows.Forms.DateTimePicker dTP_end;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dGv_Statistic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_dele;
    }
}