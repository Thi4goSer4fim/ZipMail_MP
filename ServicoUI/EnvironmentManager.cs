using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ServicoUI
{
    public class EnvironmentManager
    {
        public static void SaveEnvironmentVariables(
            string clientCNPJ,
            string accountantCNPJ,
            string emailAccountant,
            Dictionary<string, string> directoryPaths)
        {
            try
            {
                if (!string.IsNullOrEmpty(clientCNPJ))
                {
                    Environment.SetEnvironmentVariable("ZIP_MAIL_CLIENT_CNPJ", clientCNPJ, EnvironmentVariableTarget.Machine);
                }
                if (!string.IsNullOrEmpty(accountantCNPJ))
                {
                    Environment.SetEnvironmentVariable("ZIP_MAIL_ACCOUNTANT_CNPJ", accountantCNPJ, EnvironmentVariableTarget.Machine);
                }
                if (!string.IsNullOrEmpty(emailAccountant))
                {
                    Environment.SetEnvironmentVariable("ZIP_MAIL_ACCOUNTANT_EMAIL", emailAccountant, EnvironmentVariableTarget.Machine);
                }

                foreach (var path in directoryPaths)
                {
                    if (!string.IsNullOrEmpty(path.Value))
                    {
                        Environment.SetEnvironmentVariable($"ZIP_MAIL_{path.Key}_WATCHER_PATH", path.Value, EnvironmentVariableTarget.Machine);
                    }
                }

                MessageBox.Show("Variáveis de ambiente salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar variáveis de ambiente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public static string LoadEnvironmentVariable(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Machine);
        }
    }
}