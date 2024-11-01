using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Servico
{
    internal class EmailService
    {
        private const string SMTP_HOST = "smtp.gmail.com";
        private const int SMTP_PORT = 587;
        private const string SMTP_USERNAME = "tt0521407@gmail.com";
        private const string SMTP_PASS = "fwsi aadi nwym fyvg";

        private List<Attachment> _attachments;

        public EmailService()
        {
            _attachments = new List<Attachment>();
        }

        // adiciona anexos ao eemail a partir dos caminhos de arquivo fornecidos
        public void AddAttachments(params string[] attachments)
        {
            if (attachments != null && attachments.Length > 0)
            {
                foreach (var attachment in attachments)
                {
                    if (File.Exists(attachment))
                    {
                        try
                        {
                            _attachments.Add(new Attachment(attachment));
                        }
                        catch (IOException ex)
                        {
                            Debug.WriteLine($"Erro ao adicionar anexo {attachment}: {ex.Message}");
                        }
                    }
                }
            }
        }

        // verifica se o e-mail fornecido é válido
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // envia o email de forma assíncrona com os anexos adicionados
        public async Task<(string, string)> SendEmailAsync(string subject, string body, params string[] emailsTo)
        {
            using (var message = new MailMessage
            {
                From = new MailAddress(SMTP_USERNAME),
                Subject = subject,
                Body = body,
            })
            {
                try
                {
                    // Adiciona destinatários validos
                    var validEmails = emailsTo.Where(IsValidEmail).ToArray();
                    if (validEmails.Length == 0)
                        return ("Error", "Nenhum endereço de e-mail válido fornecido.");

                    foreach (var email in validEmails)
                    {
                        message.To.Add(email);
                    }

                    // adiciona os anexo
                    foreach (var attachment in _attachments)
                    {
                        message.Attachments.Add(attachment);
                    }

                    // configura e envia o e-mail pelo cliente SMTP
                    using (var smtpClient = new SmtpClient(SMTP_HOST, SMTP_PORT))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASS);

                        await smtpClient.SendMailAsync(message);
                    }

                    return ("Success", "Email enviado com sucesso");
                }
                catch (SmtpException ex)
                {
                    return ("Error", $"Erro SMTP: {ex.StatusCode} - {ex.Message}");
                }
                catch (Exception ex)
                {
                    return ("Error", $"Erro ao enviar e-mail: {ex.Message}");
                }
                finally
                {
                    // libers os recursos dos anexos apos o envio
                    foreach (var attachment in _attachments)
                    {
                        attachment.Dispose();
                    }
                    _attachments.Clear();
                }
            }
        }
    }
}