namespace ZeitRechnen
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbVonStunden = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbVonMinuten = new System.Windows.Forms.TextBox();
            this.btnZuruecksetzen = new System.Windows.Forms.Button();
            this.chkFeiertageMitberechnen = new System.Windows.Forms.CheckBox();
            this.tbFeiertage = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBisMinuten = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBisStunden = new System.Windows.Forms.TextBox();
            this.btnGesamtBerechnen = new System.Windows.Forms.Button();
            this.lblberechnetesWert = new System.Windows.Forms.Label();
            this.lblAusWochenArbZeit = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tbWocheStart = new System.Windows.Forms.TextBox();
            this.tbWocheEnd = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tbVonStunden
            // 
            this.tbVonStunden.Location = new System.Drawing.Point(85, 38);
            this.tbVonStunden.Name = "tbVonStunden";
            this.tbVonStunden.Size = new System.Drawing.Size(56, 20);
            this.tbVonStunden.TabIndex = 0;
            this.tbVonStunden.Leave += new System.EventHandler(this.TbVonStunden_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Von Stunden:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Von Minuten:";
            // 
            // tbVonMinuten
            // 
            this.tbVonMinuten.Location = new System.Drawing.Point(241, 38);
            this.tbVonMinuten.Name = "tbVonMinuten";
            this.tbVonMinuten.Size = new System.Drawing.Size(56, 20);
            this.tbVonMinuten.TabIndex = 2;
            // 
            // btnZuruecksetzen
            // 
            this.btnZuruecksetzen.Location = new System.Drawing.Point(664, 98);
            this.btnZuruecksetzen.Name = "btnZuruecksetzen";
            this.btnZuruecksetzen.Size = new System.Drawing.Size(124, 37);
            this.btnZuruecksetzen.TabIndex = 4;
            this.btnZuruecksetzen.Text = "Zurücksetzen";
            this.btnZuruecksetzen.UseVisualStyleBackColor = true;
            this.btnZuruecksetzen.Click += new System.EventHandler(this.btnZuruecksetzen_Click);
            // 
            // chkFeiertageMitberechnen
            // 
            this.chkFeiertageMitberechnen.AutoSize = true;
            this.chkFeiertageMitberechnen.Checked = true;
            this.chkFeiertageMitberechnen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFeiertageMitberechnen.Location = new System.Drawing.Point(13, 118);
            this.chkFeiertageMitberechnen.Name = "chkFeiertageMitberechnen";
            this.chkFeiertageMitberechnen.Size = new System.Drawing.Size(137, 17);
            this.chkFeiertageMitberechnen.TabIndex = 5;
            this.chkFeiertageMitberechnen.Text = "Feiertage in der Woche";
            this.chkFeiertageMitberechnen.UseVisualStyleBackColor = true;
            // 
            // tbFeiertage
            // 
            this.tbFeiertage.Location = new System.Drawing.Point(156, 116);
            this.tbFeiertage.Name = "tbFeiertage";
            this.tbFeiertage.Size = new System.Drawing.Size(33, 20);
            this.tbFeiertage.TabIndex = 6;
            this.tbFeiertage.Text = "0";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 154);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(396, 284);
            this.dataGridView1.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(664, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(124, 37);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Bis Minuten:";
            // 
            // tbBisMinuten
            // 
            this.tbBisMinuten.Location = new System.Drawing.Point(241, 76);
            this.tbBisMinuten.Name = "tbBisMinuten";
            this.tbBisMinuten.Size = new System.Drawing.Size(56, 20);
            this.tbBisMinuten.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Bis Stunden:";
            // 
            // tbBisStunden
            // 
            this.tbBisStunden.Location = new System.Drawing.Point(85, 76);
            this.tbBisStunden.Name = "tbBisStunden";
            this.tbBisStunden.Size = new System.Drawing.Size(56, 20);
            this.tbBisStunden.TabIndex = 3;
            // 
            // btnGesamtBerechnen
            // 
            this.btnGesamtBerechnen.Location = new System.Drawing.Point(664, 55);
            this.btnGesamtBerechnen.Name = "btnGesamtBerechnen";
            this.btnGesamtBerechnen.Size = new System.Drawing.Size(124, 37);
            this.btnGesamtBerechnen.TabIndex = 13;
            this.btnGesamtBerechnen.Text = "Gesamt berechnen";
            this.btnGesamtBerechnen.UseVisualStyleBackColor = true;
            this.btnGesamtBerechnen.Click += new System.EventHandler(this.btnGesamtBerechnen_Click);
            // 
            // lblberechnetesWert
            // 
            this.lblberechnetesWert.AutoSize = true;
            this.lblberechnetesWert.Location = new System.Drawing.Point(331, 44);
            this.lblberechnetesWert.Name = "lblberechnetesWert";
            this.lblberechnetesWert.Size = new System.Drawing.Size(43, 13);
            this.lblberechnetesWert.TabIndex = 14;
            this.lblberechnetesWert.Text = "000000";
            // 
            // lblAusWochenArbZeit
            // 
            this.lblAusWochenArbZeit.AutoSize = true;
            this.lblAusWochenArbZeit.Location = new System.Drawing.Point(331, 67);
            this.lblAusWochenArbZeit.Name = "lblAusWochenArbZeit";
            this.lblAusWochenArbZeit.Size = new System.Drawing.Size(62, 13);
            this.lblAusWochenArbZeit.TabIndex = 15;
            this.lblAusWochenArbZeit.Text = "xxxxxxxxxxx";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(425, 154);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(363, 284);
            this.dataGridView2.TabIndex = 16;
            // 
            // tbWocheStart
            // 
            this.tbWocheStart.Location = new System.Drawing.Point(470, 37);
            this.tbWocheStart.Name = "tbWocheStart";
            this.tbWocheStart.Size = new System.Drawing.Size(100, 20);
            this.tbWocheStart.TabIndex = 17;
            this.tbWocheStart.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // tbWocheEnd
            // 
            this.tbWocheEnd.Location = new System.Drawing.Point(470, 72);
            this.tbWocheEnd.Name = "tbWocheEnd";
            this.tbWocheEnd.Size = new System.Drawing.Size(100, 20);
            this.tbWocheEnd.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbWocheEnd);
            this.Controls.Add(this.tbWocheStart);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.lblAusWochenArbZeit);
            this.Controls.Add(this.lblberechnetesWert);
            this.Controls.Add(this.btnGesamtBerechnen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbBisMinuten);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbBisStunden);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tbFeiertage);
            this.Controls.Add(this.chkFeiertageMitberechnen);
            this.Controls.Add(this.btnZuruecksetzen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbVonMinuten);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbVonStunden);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbVonStunden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbVonMinuten;
        private System.Windows.Forms.Button btnZuruecksetzen;
        private System.Windows.Forms.CheckBox chkFeiertageMitberechnen;
        private System.Windows.Forms.TextBox tbFeiertage;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBisMinuten;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBisStunden;
        private System.Windows.Forms.Button btnGesamtBerechnen;
        private System.Windows.Forms.Label lblberechnetesWert;
        private System.Windows.Forms.Label lblAusWochenArbZeit;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox tbWocheStart;
        private System.Windows.Forms.TextBox tbWocheEnd;
    }
}

