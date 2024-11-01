namespace ServicoUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            button1 = new Button();
            button2 = new Button();
            cteBox = new TextBox();
            cnpjclient = new MaskedTextBox();
            label6 = new Label();
            emailaccountant = new TextBox();
            cnpjaccountant = new MaskedTextBox();
            nfeBox = new TextBox();
            nfseBox = new TextBox();
            CfeBox = new TextBox();
            nfseNacionalBox = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = SystemColors.Highlight;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(9, 26);
            label1.Name = "label1";
            label1.Padding = new Padding(5, 2, 5, 0);
            label1.Size = new Size(421, 30);
            label1.TabIndex = 0;
            label1.Text = "Dados do Cliente";
            label1.UseWaitCursor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(11, 68);
            label2.Name = "label2";
            label2.Size = new Size(48, 20);
            label2.TabIndex = 2;
            label2.Text = "CNPJ:";
            label2.UseWaitCursor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(11, 185);
            label3.Name = "label3";
            label3.Size = new Size(48, 20);
            label3.TabIndex = 5;
            label3.Text = "CNPJ:";
            label3.UseWaitCursor = true;
            // 
            // label4
            // 
            label4.BackColor = SystemColors.Highlight;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(11, 148);
            label4.Name = "label4";
            label4.Padding = new Padding(5, 2, 5, 0);
            label4.Size = new Size(419, 30);
            label4.TabIndex = 3;
            label4.Text = "Dados do Contador";
            label4.UseWaitCursor = true;
            // 
            // label5
            // 
            label5.BackColor = SystemColors.Highlight;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(11, 340);
            label5.Name = "label5";
            label5.Padding = new Padding(5, 2, 5, 0);
            label5.Size = new Size(419, 30);
            label5.TabIndex = 6;
            label5.Text = "Escolher diretório de upload";
            label5.UseWaitCursor = true;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Highlight;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(12, 592);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(138, 38);
            button1.TabIndex = 14;
            button1.Text = "Salvar";
            button1.UseVisualStyleBackColor = false;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Window;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(156, 592);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(138, 38);
            button2.TabIndex = 15;
            button2.Text = "Limpar";
            button2.UseVisualStyleBackColor = false;
            button2.UseWaitCursor = true;
            button2.Click += button2_Click;
            // 
            // cteBox
            // 
            cteBox.AllowDrop = true;
            cteBox.BorderStyle = BorderStyle.FixedSingle;
            cteBox.Location = new Point(11, 412);
            cteBox.Margin = new Padding(3, 2, 3, 2);
            cteBox.Name = "cteBox";
            cteBox.Size = new Size(206, 27);
            cteBox.TabIndex = 16;
            cteBox.UseWaitCursor = true;
            // 
            // cnpjclient
            // 
            cnpjclient.AsciiOnly = true;
            cnpjclient.BorderStyle = BorderStyle.FixedSingle;
            cnpjclient.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            cnpjclient.HidePromptOnLeave = true;
            cnpjclient.InsertKeyMode = InsertKeyMode.Overwrite;
            cnpjclient.Location = new Point(11, 91);
            cnpjclient.Margin = new Padding(3, 2, 3, 2);
            cnpjclient.Mask = "00,000,000/0000-00";
            cnpjclient.Name = "cnpjclient";
            cnpjclient.Size = new Size(419, 27);
            cnpjclient.TabIndex = 17;
            cnpjclient.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            cnpjclient.UseWaitCursor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(11, 260);
            label6.Name = "label6";
            label6.Size = new Size(147, 20);
            label6.TabIndex = 19;
            label6.Text = "E-mail do Contador:";
            label6.UseWaitCursor = true;
            // 
            // emailaccountant
            // 
            emailaccountant.BorderStyle = BorderStyle.FixedSingle;
            emailaccountant.Location = new Point(11, 288);
            emailaccountant.Margin = new Padding(3, 2, 3, 2);
            emailaccountant.Name = "emailaccountant";
            emailaccountant.Size = new Size(419, 27);
            emailaccountant.TabIndex = 18;
            emailaccountant.UseWaitCursor = true;
            // 
            // cnpjaccountant
            // 
            cnpjaccountant.AsciiOnly = true;
            cnpjaccountant.BorderStyle = BorderStyle.FixedSingle;
            cnpjaccountant.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            cnpjaccountant.HidePromptOnLeave = true;
            cnpjaccountant.InsertKeyMode = InsertKeyMode.Overwrite;
            cnpjaccountant.Location = new Point(11, 212);
            cnpjaccountant.Margin = new Padding(3, 2, 3, 2);
            cnpjaccountant.Mask = "00,000,000/0000-00";
            cnpjaccountant.Name = "cnpjaccountant";
            cnpjaccountant.Size = new Size(419, 27);
            cnpjaccountant.TabIndex = 20;
            cnpjaccountant.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            cnpjaccountant.UseWaitCursor = true;
            // 
            // nfeBox
            // 
            nfeBox.AllowDrop = true;
            nfeBox.BorderStyle = BorderStyle.FixedSingle;
            nfeBox.Location = new Point(224, 412);
            nfeBox.Margin = new Padding(3, 2, 3, 2);
            nfeBox.Name = "nfeBox";
            nfeBox.Size = new Size(206, 27);
            nfeBox.TabIndex = 21;
            nfeBox.UseWaitCursor = true;
            // 
            // nfseBox
            // 
            nfseBox.AllowDrop = true;
            nfseBox.BorderStyle = BorderStyle.FixedSingle;
            nfseBox.Location = new Point(12, 482);
            nfseBox.Margin = new Padding(3, 2, 3, 2);
            nfseBox.Name = "nfseBox";
            nfseBox.Size = new Size(206, 27);
            nfseBox.TabIndex = 22;
            nfseBox.UseWaitCursor = true;
            // 
            // CfeBox
            // 
            CfeBox.AllowDrop = true;
            CfeBox.BorderStyle = BorderStyle.FixedSingle;
            CfeBox.Location = new Point(224, 482);
            CfeBox.Margin = new Padding(3, 2, 3, 2);
            CfeBox.Name = "CfeBox";
            CfeBox.Size = new Size(206, 27);
            CfeBox.TabIndex = 23;
            CfeBox.UseWaitCursor = true;
            // 
            // nfseNacionalBox
            // 
            nfseNacionalBox.AllowDrop = true;
            nfseNacionalBox.BorderStyle = BorderStyle.FixedSingle;
            nfseNacionalBox.Location = new Point(12, 548);
            nfseNacionalBox.Margin = new Padding(3, 2, 3, 2);
            nfseNacionalBox.Name = "nfseNacionalBox";
            nfseNacionalBox.Size = new Size(206, 27);
            nfseNacionalBox.TabIndex = 24;
            nfseNacionalBox.UseWaitCursor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label7.Location = new Point(12, 390);
            label7.Name = "label7";
            label7.Size = new Size(40, 20);
            label7.TabIndex = 25;
            label7.Text = "CT-e";
            label7.UseWaitCursor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label8.Location = new Point(224, 390);
            label8.Name = "label8";
            label8.Size = new Size(43, 20);
            label8.TabIndex = 26;
            label8.Text = "NF-e";
            label8.UseWaitCursor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label9.Location = new Point(12, 460);
            label9.Name = "label9";
            label9.Size = new Size(51, 20);
            label9.TabIndex = 27;
            label9.Text = "NFS-e";
            label9.UseWaitCursor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label10.Location = new Point(224, 460);
            label10.Name = "label10";
            label10.Size = new Size(40, 20);
            label10.TabIndex = 28;
            label10.Text = "CF-e";
            label10.UseWaitCursor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(12, 526);
            label11.Name = "label11";
            label11.Size = new Size(116, 20);
            label11.TabIndex = 29;
            label11.Text = "NFS-e Nacional";
            label11.UseWaitCursor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 641);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(nfseNacionalBox);
            Controls.Add(CfeBox);
            Controls.Add(nfseBox);
            Controls.Add(nfeBox);
            Controls.Add(cnpjaccountant);
            Controls.Add(label6);
            Controls.Add(emailaccountant);
            Controls.Add(cnpjclient);
            Controls.Add(cteBox);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Salvar Dados";
            UseWaitCursor = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label6;
        private TextBox emailaccountant;
        private Button button1;
        private Button button2;
        private TextBox cteBox;
        private MaskedTextBox cnpjclient;
        private MaskedTextBox cnpjaccountant;
        private TextBox nfeBox;
        private TextBox nfseBox;
        private TextBox CfeBox;
        private TextBox nfseNacionalBox;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
    }
}
