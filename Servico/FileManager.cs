using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;

namespace Servico
{
    internal class FileManager
    {
        private readonly List<FileSystemWatcher> _watchers;
        private readonly EventLog _eventLog;

        private Timer _debounceTimer;

        private bool _isZipping;

        public delegate void CompressionCompletedHandler(string file, string type);
        public event CompressionCompletedHandler CompressionCompleted;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="FileManager"/>.
        /// </summary>
        /// <param name="serviceName">Nome do serviço para o log de eventos.</param>
        /// <param name="zipFileName">Nome do arquivo zip a ser criado.</param>
        public FileManager(string serviceName, string zipFileName)
        {
            _eventLog = new EventLog("Application")
            {
                Source = serviceName
            };

            if (!EventLog.SourceExists(serviceName))
            {
                EventLog.CreateEventSource(serviceName, "Application");
            }

            _watchers = new List<FileSystemWatcher>();
        }

        /// <summary>
        /// Adiciona um observador de arquivos para um diretório específico.
        /// </summary>
        /// <param name="path">Caminho do diretório a ser observado.</param>
        private void AddWatcher(string path)
        {
            if (!Directory.Exists(path))
            {
                _eventLog.WriteEntry($"O diretório {path} não existe.", EventLogEntryType.Error);
                return;
            }

            var watcher = new FileSystemWatcher(path)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.*"
            };

            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;

            _watchers.Add(watcher);
        }

        /// <summary>
        /// Inicia a observação de uma lista de diretórios e zipa os arquivos existentes.
        /// </summary>
        /// <param name="paths">Lista de caminhos de diretórios a serem observados.</param>
        public void Start(List<string> paths)
        {
            foreach (var path in paths)
            {
                AddWatcher(path);
            }
        }

        /// <summary>
        /// Para a observação dos diretórios monitorados.
        /// </summary>
        public void Stop()
        {
            foreach (var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Created -= OnCreated;
                watcher.Dispose();
            }

            _watchers.Clear();
        }

        /// <summary>
        /// Manipula eventos de alteração de arquivos no diretório observado.
        /// </summary>
        /// <param name="sender">Origem do evento.</param>
        /// <param name="e">Informações sobre o evento de sistema de arquivos.</param>
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if (_isZipping) return;

            string directoryPath = Path.GetDirectoryName(e.FullPath);
            string type = DetermineDocumentType(directoryPath);  // Determina o tipo antes do zip

            _debounceTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            _debounceTimer = new Timer(ZipFilesInDirectory, Path.GetDirectoryName(e.FullPath), 500, Timeout.Infinite);
        }


        private string DetermineDocumentType(string directoryPath)
        {
            var pathTypeMap = new Dictionary<string, string>
    {
        { Environment.GetEnvironmentVariable("ZIP_MAIL_CTE_WATCHER_PATH"), "CT-e" },
        { Environment.GetEnvironmentVariable("ZIP_MAIL_NFE_WATCHER_PATH"), "NF-e" },
        { Environment.GetEnvironmentVariable("ZIP_MAIL_NFSE_WATCHER_PATH"), "NFS-e" },
        { Environment.GetEnvironmentVariable("ZIP_MAIL_CFE_WATCHER_PATH"), "CF-e" },
        { Environment.GetEnvironmentVariable("ZIP_MAIL_NFSENACIONAL_WATCHER_PATH"), "NFS-e Nacional" }
    };

            foreach (var kvp in pathTypeMap)
            {
                if (string.Equals(directoryPath, kvp.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return kvp.Value;
                }
            }

            return "Desconhecido";
        }

        /// <summary>
        /// Zipa os arquivos em um diretório específico.
        /// </summary>
        /// <param name="path">Caminho do diretório cujos arquivos devem ser zipados.</param>
        private void ZipFilesInDirectory(object state)
        {
            string path = (string)state;

            if (!Directory.Exists(path))
            {
                _eventLog.WriteEntry($"O diretório não existe: {path}", EventLogEntryType.Error);
                return;
            }

            var allFiles = Directory.GetFiles(path);

            if (allFiles.Length == 0)
            {
                _eventLog.WriteEntry("Nenhum arquivo encontrado.", EventLogEntryType.Warning);
                return;
            }

            string fileToSend = allFiles.Length > 1 ? CreateZip(allFiles.ToList()) : allFiles.FirstOrDefault();

            // Identifica o tipo do documento
            string type = DetermineDocumentType(path);

            if (fileToSend != null)
            {
                CompressionCompleted?.Invoke(fileToSend, type);
                DeleteFilesInDirectory(path);
            }
            else
            {
                _eventLog.WriteEntry("Nenhum arquivo zipado foi criado.", EventLogEntryType.Warning);
            }
        }


        /// <summary>
        /// Zipa os arquivos fornecidos em um diretório específico e retorna o caminho do arquivo zipado.
        /// </summary>
        /// <param name="filesToZip">Lista de arquivos que devem ser zipados.</param>
        /// <param name="path">Caminho do diretório onde o arquivo zipado será criado.</param>
        /// <returns>O caminho do arquivo zipado se a operação for bem-sucedida; caso contrário, retorna null.</returns>
        private string CreateZip(List<string> filesToZip)
        {
            // Cria um caminho temporário para o diretório de anexos
            string destinationPath = Path.Combine(Path.GetTempPath(), "MeusAnexos");

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            string zipPath = Path.Combine(destinationPath, $"{Guid.NewGuid()}.zip");

            int maxAttempts = 3;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                try
                {
                    _isZipping = true;
                    using (var zipStream = new FileStream(zipPath, FileMode.Create))
                    using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                    {
                        foreach (var file in filesToZip)
                        {
                            try
                            {
                                archive.CreateEntryFromFile(file, Path.GetFileName(file));
                            }
                            catch (Exception ex)
                            {
                                _eventLog.WriteEntry($"Erro ao zipar o arquivo {file}: {ex.Message}", EventLogEntryType.Error);
                            }
                        }
                    }

                    _eventLog.WriteEntry($"Arquivo criado com sucesso: {zipPath}", EventLogEntryType.Information);

                    return zipPath;
                }
                catch (IOException ex)
                {
                    _eventLog.WriteEntry($"Tentativa {attempt + 1} de zipar falhou: {ex.Message}", EventLogEntryType.Error);
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    _eventLog.WriteEntry($"Erro ao zipar arquivos: {ex.Message}\n{ex.StackTrace}", EventLogEntryType.Error);
                    return null;
                }
                finally
                {
                    _isZipping = false;
                }
            }

            _eventLog.WriteEntry("Falha ao zipar o arquivo após várias tentativas.", EventLogEntryType.Error);
            return null;
        }



        /// <summary>
        /// Deleta arquivos em um diretório específico.
        /// </summary>
        /// <param name="path">Caminho do diretório cujos arquivos devem ser deletados.</param>
        /// <param name="isAll">Indica se todos os arquivos devem ser deletados.</param>
        public void DeleteFilesInDirectory(string path, bool isAll = false)
        {
            string[] files = Directory.GetFiles(path);

            try
            {
                foreach (string file in files)
                {
                    File.Delete(file);                                 
                }

                _eventLog.WriteEntry("Arquivos deletados.");
            }
            catch (Exception ex)
            {
                _eventLog.WriteEntry($"Erro ao deletar arquivos: {ex.Message}", EventLogEntryType.Error);
            }
        }
    }
}
