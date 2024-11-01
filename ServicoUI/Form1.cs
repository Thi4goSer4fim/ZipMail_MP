using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ServiceProcess;

namespace ServicoUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cteBox.Click += cteBox_Click;
            CfeBox.Click += cfeBox_Click;
            nfseBox.Click += nfseBox_Click;
            nfseNacionalBox.Click += nfsenacionalBox_Click;
            nfeBox.Click += nfeBox_Click;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is MaskedTextBox maskedTextBox) maskedTextBox.Clear();
                else if (control is System.Windows.Forms.TextBox textBox) textBox.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string clientCNPJ = cnpjclient.Text.Trim();
            string accountantCNPJ = cnpjaccountant.Text.Trim();
            string emailAccountant = emailaccountant.Text.Trim();
            string ctePath = cteBox.Text.Trim();
            string cfePath = CfeBox.Text.Trim();
            string nfsePath = nfseBox.Text.Trim();
            string nfsenacionalPath = nfseNacionalBox.Text.Trim();
            string nfePath = nfeBox.Text.Trim();

            var directoryPaths = new Dictionary<string, string>
            {
                { "CTE", ctePath },
                { "CFE", cfePath },
                { "NFSE", nfsePath },
                { "NFSENACIONAL", nfsenacionalPath },
                { "NFE", nfePath }
            };

            // Validar os campos e salvar as variáveis de ambiente se tudo estiver correto
            if (ValidateInputs(clientCNPJ, accountantCNPJ, emailAccountant, directoryPaths))
            {
                EnvironmentManager.SaveEnvironmentVariables(clientCNPJ, accountantCNPJ, emailAccountant, directoryPaths);
                MessageBox.Show("O monitoramento foi iniciado em segundo plano.", "Serviço Iniciado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }


        private bool ValidateInputs(string clientCNPJ, string accountantCNPJ, string emailAccountant, Dictionary<string, string> directoryPaths)
        {
            if (!Regex.IsMatch(clientCNPJ, @"^\d{14}$"))
            {
                MessageBox.Show("CNPJ do cliente inválido. Insira um CNPJ com 14 dígitos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!Regex.IsMatch(accountantCNPJ, @"^\d{14}$"))
            {
                MessageBox.Show("CNPJ do contador inválido. Insira um CNPJ com 14 dígitos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!Regex.IsMatch(emailAccountant, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("E-mail do contador inválido. Insira um e-mail válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            foreach (var path in directoryPaths)
            {
                if (string.IsNullOrEmpty(path.Value))
                {
                    MessageBox.Show($"O campo de diretório para {path.Key} não pode estar vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        private void SelectFolder(System.Windows.Forms.TextBox textBox)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Selecione o diretório",
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                textBox.Text = dialog.SelectedPath;
            }
        }
        private void cteBox_Click(object sender, EventArgs e) => SelectFolder(cteBox);
        private void cfeBox_Click(object sender, EventArgs e) => SelectFolder(CfeBox);
        private void nfseBox_Click(object sender, EventArgs e) => SelectFolder(nfseBox);
        private void nfsenacionalBox_Click(object sender, EventArgs e) => SelectFolder(nfseNacionalBox);
        private void nfeBox_Click(object sender, EventArgs e) => SelectFolder(nfeBox);
    }
}