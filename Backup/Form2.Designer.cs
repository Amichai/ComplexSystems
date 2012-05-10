namespace BindingTest
{
	partial class Form2
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
			if (disposing && (components != null)) {
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
			this.label2 = new System.Windows.Forms.Label();
			this.lstPassengers = new System.Windows.Forms.ListBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.grid = new System.Windows.Forms.DataGridView();
			this.txtAirplaneFilter = new System.Windows.Forms.TextBox();
			this.txtPassengerFilter = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(366, 192);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Name:";
			// 
			// lstPassengers
			// 
			this.lstPassengers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lstPassengers.FormattingEnabled = true;
			this.lstPassengers.Location = new System.Drawing.Point(369, 49);
			this.lstPassengers.Name = "lstPassengers";
			this.lstPassengers.Size = new System.Drawing.Size(146, 134);
			this.lstPassengers.TabIndex = 19;
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(407, 189);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(108, 20);
			this.txtName.TabIndex = 18;
			this.txtName.Text = "txtName";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 13);
			this.label4.TabIndex = 17;
			this.label4.Text = "Airplanes:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(366, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Passengers on selected plane:";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 192);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "Model:";
			// 
			// txtModel
			// 
			this.txtModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtModel.Location = new System.Drawing.Point(57, 189);
			this.txtModel.Name = "txtModel";
			this.txtModel.Size = new System.Drawing.Size(303, 20);
			this.txtModel.TabIndex = 14;
			this.txtModel.Text = "txtModel";
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Location = new System.Drawing.Point(15, 35);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(345, 148);
			this.grid.TabIndex = 13;
			// 
			// txtAirplaneFilter
			// 
			this.txtAirplaneFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAirplaneFilter.Location = new System.Drawing.Point(234, 9);
			this.txtAirplaneFilter.Name = "txtAirplaneFilter";
			this.txtAirplaneFilter.Size = new System.Drawing.Size(126, 20);
			this.txtAirplaneFilter.TabIndex = 21;
			this.txtAirplaneFilter.TextChanged += new System.EventHandler(this.txtAirplaneFilter_TextChanged);
			// 
			// txtPassengerFilter
			// 
			this.txtPassengerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPassengerFilter.Location = new System.Drawing.Point(404, 25);
			this.txtPassengerFilter.Name = "txtPassengerFilter";
			this.txtPassengerFilter.Size = new System.Drawing.Size(111, 20);
			this.txtPassengerFilter.TabIndex = 21;
			this.txtPassengerFilter.TextChanged += new System.EventHandler(this.txtPassengerFilter_TextChanged);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(196, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Filter:";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(366, 28);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(32, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Filter:";
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(527, 214);
			this.Controls.Add(this.txtPassengerFilter);
			this.Controls.Add(this.txtAirplaneFilter);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lstPassengers);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.grid);
			this.Name = "Form2";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lstPassengers;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.DataGridView grid;
		private System.Windows.Forms.TextBox txtAirplaneFilter;
		private System.Windows.Forms.TextBox txtPassengerFilter;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;


	}
}