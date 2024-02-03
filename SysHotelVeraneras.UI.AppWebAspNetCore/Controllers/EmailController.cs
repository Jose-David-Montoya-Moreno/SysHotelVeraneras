using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;

public class EmailController : Controller
{

    // Método para mostrar la vista para enviar correo electrónico
    public IActionResult EnviarCorreo()
    {
        return View();
    }

    // Método para procesar el envío del correo electrónico
    [HttpPost]
    public IActionResult EnviarCorreo(string destinatario, string asunto, string mensaje, IFormFile archivoAdjunto)
    {
        try
        {
            // Configurar el cliente SMTP
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                String sCuentaCorreo = "hl23002@esfe.agape.edu.sv";
                String sPasswordCorreo = "ishjmoeltciyxnfu";
                smtpClient.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sPasswordCorreo);



                // Crear el correo electrónico
                using (MailMessage correo = new MailMessage())
                {
                    correo.From = new MailAddress("hl23002@esfe.agape.edu.sv");
                    correo.To.Add(destinatario);
                    correo.Subject = asunto;
                    correo.Body = mensaje;



                    // Guardar archivo adjunto si se proporcionó
                    if (archivoAdjunto != null && archivoAdjunto.Length > 0)
                    {
                        // Ruta donde se guardarán los archivos adjuntos
                        string rutaAlmacenamiento = Path.Combine(Directory.GetCurrentDirectory(), "Temporal");

                        // Verificar si la carpeta de almacenamiento no existe, crearla
                        if (!Directory.Exists(rutaAlmacenamiento))
                        {
                            Directory.CreateDirectory(rutaAlmacenamiento);
                        }

                        // Generar un nombre único para el archivo adjunto
                        string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivoAdjunto.FileName);

                        // Construir la ruta completa del archivo a guardar
                        string rutaArchivoGuardar = Path.Combine(rutaAlmacenamiento, nombreArchivo);

                        // Guardar el archivo adjunto en la carpeta de almacenamiento
                        using (var stream = new FileStream(rutaArchivoGuardar, FileMode.Create))
                        {
                            archivoAdjunto.CopyTo(stream);
                        }

                        // Adjuntar el archivo utilizando la ruta del archivo guardado en el servidor
                        correo.Attachments.Add(new Attachment(rutaArchivoGuardar));
                    }


                    // Enviar correo electrónico
                    smtpClient.Send(correo);
                }
            }

            ViewBag.Message = "Correo enviado correctamente.";
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Error al enviar el correo: " + ex.Message;
        }

        return View("EnviarCorreoResultado");
    }

}
