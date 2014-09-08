namespace SquirrelsWorlds
{
    partial class SquirrelsWorldForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.listviewPredBoolMap = new System.Windows.Forms.ListView();
            this.colPredicate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtbEdgy = new System.Windows.Forms.RichTextBox();
            this.rtbWally = new System.Windows.Forms.RichTextBox();
            this.labelEdgy = new System.Windows.Forms.Label();
            this.labelWally = new System.Windows.Forms.Label();
            this.labelPredBoolMap = new System.Windows.Forms.Label();
            this.labelEventList = new System.Windows.Forms.Label();
            this.rtbEventList = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(20, 10);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // listviewPredBoolMap
            // 
            this.listviewPredBoolMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPredicate,
            this.colValue});
            this.listviewPredBoolMap.LabelEdit = true;
            this.listviewPredBoolMap.LabelWrap = false;
            this.listviewPredBoolMap.Location = new System.Drawing.Point(20, 212);
            this.listviewPredBoolMap.Name = "listviewPredBoolMap";
            this.listviewPredBoolMap.Size = new System.Drawing.Size(225, 500);
            this.listviewPredBoolMap.TabIndex = 2;
            this.listviewPredBoolMap.UseCompatibleStateImageBehavior = false;
            this.listviewPredBoolMap.View = System.Windows.Forms.View.Details;
            // 
            // colPredicate
            // 
            this.colPredicate.Text = "Predicate";
            this.colPredicate.Width = 135;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 55;
            // 
            // rtbEdgy
            // 
            this.rtbEdgy.Location = new System.Drawing.Point(485, 212);
            this.rtbEdgy.Name = "rtbEdgy";
            this.rtbEdgy.Size = new System.Drawing.Size(200, 500);
            this.rtbEdgy.TabIndex = 6;
            this.rtbEdgy.Text = "";
            // 
            // rtbWally
            // 
            this.rtbWally.Location = new System.Drawing.Point(705, 212);
            this.rtbWally.Name = "rtbWally";
            this.rtbWally.Size = new System.Drawing.Size(200, 500);
            this.rtbWally.TabIndex = 8;
            this.rtbWally.Text = "";
            // 
            // labelEdgy
            // 
            this.labelEdgy.AutoSize = true;
            this.labelEdgy.Location = new System.Drawing.Point(485, 185);
            this.labelEdgy.Name = "labelEdgy";
            this.labelEdgy.Size = new System.Drawing.Size(47, 15);
            this.labelEdgy.TabIndex = 5;
            this.labelEdgy.Text = "Edgy:";
            // 
            // labelWally
            // 
            this.labelWally.AutoSize = true;
            this.labelWally.Location = new System.Drawing.Point(705, 185);
            this.labelWally.Name = "labelWally";
            this.labelWally.Size = new System.Drawing.Size(55, 15);
            this.labelWally.TabIndex = 7;
            this.labelWally.Text = "Wally:";
            // 
            // labelPredBoolMap
            // 
            this.labelPredBoolMap.AutoSize = true;
            this.labelPredBoolMap.Location = new System.Drawing.Point(20, 185);
            this.labelPredBoolMap.Name = "labelPredBoolMap";
            this.labelPredBoolMap.Size = new System.Drawing.Size(167, 15);
            this.labelPredBoolMap.TabIndex = 1;
            this.labelPredBoolMap.Text = "PredicateBooleanMap:";
            // 
            // labelEventList
            // 
            this.labelEventList.AutoSize = true;
            this.labelEventList.Location = new System.Drawing.Point(265, 185);
            this.labelEventList.Name = "labelEventList";
            this.labelEventList.Size = new System.Drawing.Size(87, 15);
            this.labelEventList.TabIndex = 3;
            this.labelEventList.Text = "EventList:";
            // 
            // rtbEventList
            // 
            this.rtbEventList.Location = new System.Drawing.Point(265, 212);
            this.rtbEventList.Name = "rtbEventList";
            this.rtbEventList.Size = new System.Drawing.Size(200, 500);
            this.rtbEventList.TabIndex = 4;
            this.rtbEventList.Text = "";
            // 
            // SquirrelsWorldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 853);
            this.Controls.Add(this.labelEventList);
            this.Controls.Add(this.rtbEventList);
            this.Controls.Add(this.labelPredBoolMap);
            this.Controls.Add(this.labelWally);
            this.Controls.Add(this.labelEdgy);
            this.Controls.Add(this.rtbWally);
            this.Controls.Add(this.rtbEdgy);
            this.Controls.Add(this.listviewPredBoolMap);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SquirrelsWorldForm";
            this.Text = "SquirrelsWorld";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListView listviewPredBoolMap;
        private System.Windows.Forms.RichTextBox rtbEdgy;
        private System.Windows.Forms.RichTextBox rtbWally;
        private System.Windows.Forms.Label labelEdgy;
        private System.Windows.Forms.Label labelWally;
        private System.Windows.Forms.Label labelPredBoolMap;
        private System.Windows.Forms.Label labelEventList;
        private System.Windows.Forms.RichTextBox rtbEventList;
        private System.Windows.Forms.ColumnHeader colPredicate;
        private System.Windows.Forms.ColumnHeader colValue;
    }
}

