using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Servico
{
    public partial class Service1 : ServiceBase
    {
        private FileManager _folderWatch;

        private const string ZIP_FILE_NAME = "files.zip";

        private EventLog _eventLog;

        private readonly string recipient = Environment.GetEnvironmentVariable("ZIP_MAIL_ACCOUNTANT_EMAIL");
        private readonly string ctepath = Environment.GetEnvironmentVariable("ZIP_MAIL_CTE_WATCHER_PATH");
        private readonly string nfepath = Environment.GetEnvironmentVariable("ZIP_MAIL_NFE_WATCHER_PATH");
        private readonly string nfsepath = Environment.GetEnvironmentVariable("ZIP_MAIL_NFSE_WATCHER_PATH");
        private readonly string cfepath = Environment.GetEnvironmentVariable("ZIP_MAIL_CFE_WATCHER_PATH");
        private readonly string nfsenacionalpath = Environment.GetEnvironmentVariable("ZIP_MAIL_NFSENACIONAL_WATCHER_PATH");

        public Service1()
        {
            InitializeComponent();
            ConfigWatcher();

            _eventLog = new EventLog("Application")
            {
                Source = ServiceName
            };

            if (!EventLog.SourceExists(ServiceName))
            {
                EventLog.CreateEventSource(ServiceName, "Application");
            }
        }


        private void ConfigWatcher() 
        {
            _folderWatch = new FileManager(ServiceName, ZIP_FILE_NAME);
            _folderWatch.CompressionCompleted += OnZipCreated;
        }

        protected override void OnStart(string[] args)
        {
            _eventLog.WriteEntry("Serviço iniciado.", EventLogEntryType.Information);

            //SUBSTITUIR PELOS VALORES DAS VARIÁVEIS DE AMBIENTE
            //EXEMPLO:
            //Environment.GetEnvironmentVariable("SERVICO_CTE_OBSERVE_DIRECTORY");
            List<string> path = new List<string>() 
            {
                ctepath,
                nfsepath,
                nfsenacionalpath,
                cfepath,
                nfepath
            };

            _folderWatch.Start(path);
        }

        private async void OnZipCreated(string file, string type) 
        {
            await CreateEmail(file, type);            
        }

        private async Task CreateEmail(string file, string type)
{
    EmailService emailService = new EmailService();

    emailService.AddAttachments(file);

    DateTime date = DateTime.Now;
    string body = $"{type} - Arquivos em Anexo data {date:dd/MM/yyyy}";
    string subject = $"Arquivos - {type}";

    string[] recipients = new[] { recipient };

    (string status, string message) response = await emailService.SendEmailAsync(subject, body, recipients);

    EventLogEntryType logEntryType = response.status == "Error" ? EventLogEntryType.Error : EventLogEntryType.Information;
    _eventLog.WriteEntry($"Email Status: {response.status}, Message: {response.message}", logEntryType);

    if (response.status == "Success")
        DeleteZip(Path.GetDirectoryName(file));
}

        private void DeleteZip(string path) 
        {
            _folderWatch.DeleteFilesInDirectory(path);
        }

        protected override void OnStop()
        {
            _folderWatch.Stop();
        }
    }
}
