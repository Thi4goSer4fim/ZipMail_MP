using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;

namespace Servico
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private EventLog _eventLog;

        public ProjectInstaller()
        {
            InitializeComponent();

            // Inicializa e configura o EventLog apenas se não existir
            if (!EventLog.SourceExists("ZipMailService"))
            {
                EventLog.CreateEventSource("ZipMailService", "Application");
            }
            _eventLog = new EventLog("Application")
            {
                Source = "ZipMailService"
            };

            // Configura o instalador do serviço e a conta
            serviceInstaller1.StartType = ServiceStartMode.Automatic;
            serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;

            // Associa o evento AfterInstall
            serviceInstaller1.AfterInstall += serviceInstaller1_AfterInstall;
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            using (var sc = new ServiceController(serviceInstaller1.ServiceName))
            {
                try
                {
                    _eventLog.WriteEntry("Tentando iniciar o serviço após a instalação.");
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                    _eventLog.WriteEntry("Serviço iniciado com sucesso.");
                }
                catch (Exception ex)
                {
                    _eventLog.WriteEntry($"Erro ao iniciar o serviço: {ex.Message}", EventLogEntryType.Error);
                }
            }
        }
    }
}