using System.Diagnostics;
using System.Text;
using Aspose.Email;
using Aspose.Email.Clients;
using Aspose.Email.Clients.Smtp;

namespace OfficeTechRepairSystem.Data;

public class EmailSender
{

    public async Task SendAdminEmail(string userMail, string phoneNumber, string message, string userName)
    {
        MailMessage message1 = new MailMessage();

        // Установите тему сообщения, тело HTML, информацию об отправителе и получателе.
        message1.Subject = "ООО 'Ремонтер' уведомление о принятии заявки";
        message1.HtmlBody =  $@"
<!DOCTYPE html>
<html lang='ru'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Подтверждение заявки</title>
    <style>
        body {{
            background-color: #f4f4f4;
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 50px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background-color: #007bff;
            color: #ffffff;
            padding: 10px;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
            text-align: center;
        }}
        .content {{
            padding: 20px;
            color: #333333;
        }}
        .footer {{
            background-color: #f1f1f1;
            padding: 10px;
            text-align: center;
            border-bottom-left-radius: 10px;
            border-bottom-right-radius: 10px;
            color: #777777;
        }}
        .button {{
            display: inline-block;
            background-color: #007bff;
            color: #ffffff;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }}
        .button:hover {{
            background-color: #0056b3;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Пришла новая заявка</h1>
        </div>
        <div class='content'>
            <p>Новая заявка</p>
            <p>На сайте в {DateTime.Now} была оставлена новая заявка</p>
            <p><strong>Детали заявки:</strong></p>
            <p>Имя: {userName}</p>
            <p>Телефон: {phoneNumber}</p>
            <p>Email: {userMail}</p>
            <p>Сообщение заявки: {message}</p>
            <p>Если у вас возникнут вопросы, не стесняйтесь <a href='mailto:support@example.com'>связаться с нашей службой поддержки</a>.</p>
            <p>С уважением,<br>Команда поддержки</p>
            <a href='https://www.example.com' class='button'>Посетить наш сайт</a>
        </div>
        <div class='footer'>
            <p>&copy; 2024 ООО Ремонтер. Все права защищены.</p>
        </div>
    </div>
</body>
</html>";
        message1.From = new MailAddress("bananjekrd@gmail.com", "OOO Remonter", false);

        // TO-DO указать почту актуальную
        message1.To.Add(new MailAddress("faustkill@bk.ru", "OOO Remonter", false));

        // Укажите кодировку 
        message1.BodyEncoding = Encoding.ASCII;

        // Создайте экземпляр класса SmtpClient.
        SmtpClient client = new SmtpClient();

        // Укажите свой почтовый хост, имя пользователя, пароль, номер порта и параметр безопасности.
        client.Host = "smtp.gmail.com";
        client.Username = "bananjekrd@gmail.com";
        client.Password = "njlc jlfo bkbb qovl";
        client.Port = 587;
        client.SecurityOptions = SecurityOptions.SSLExplicit;

        try
        {
            // Отправить это письмо
            await client.SendAsync(message1);
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.ToString());
        }
    }

    public async Task SendChangeEmailAsync(string email, string html)
    {
        MailMessage message = new MailMessage();

        // Установите тему сообщения, тело HTML, информацию об отправителе и получателе.
        message.Subject = "ООО 'Ремонтер' уведомление о принятии заявки";
        message.HtmlBody = html;

        message.From = new MailAddress("bananjekrd@gmail.com", "OOO Remonter", false);
        message.To.Add(new MailAddress(email, "OOO Remonter", false));

        // Укажите кодировку 
        message.BodyEncoding = Encoding.ASCII;

        // Создайте экземпляр класса SmtpClient.
        SmtpClient client = new SmtpClient();

        // Укажите свой почтовый хост, имя пользователя, пароль, номер порта и параметр безопасности.
        client.Host = "smtp.gmail.com";
        client.Username = "bananjekrd@gmail.com";
        client.Password = "njlc jlfo bkbb qovl";
        client.Port = 587;
        client.SecurityOptions = SecurityOptions.SSLExplicit;

        try
        {
            // Отправить это письмо
            await client.SendAsync(message);
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.ToString());
        }
    }

    public async Task SendUserEmail(string userMail)
    {
        MailMessage message = new MailMessage();

        // Установите тему сообщения, тело HTML, информацию об отправителе и получателе.
        message.Subject = "ООО 'Ремонтер' уведомление о принятии заявки";
        message.HtmlBody = @"
<!DOCTYPE html>
<html lang='ru'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Подтверждение заявки</title>
    <style>
        body {
            background-color: #f4f4f4;
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 600px;
            margin: 50px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .header {
            background-color: #007bff;
            color: #ffffff;
            padding: 10px;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
            text-align: center;
        }
        .content {
            padding: 20px;
            color: #333333;
        }
        .footer {
            background-color: #f1f1f1;
            padding: 10px;
            text-align: center;
            border-bottom-left-radius: 10px;
            border-bottom-right-radius: 10px;
            color: #777777;
        }
        .button {
            display: inline-block;
            background-color: #007bff;
            color: #ffffff;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }
        .button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Заявка принята</h1>
        </div>
        <div class='content'>
            <p>Уважаемый пользователь,</p>
            <p>Спасибо за вашу заявку, оставленную на нашем сайте. Мы рады сообщить вам, что ваша заявка принята в работу и будет обработана в ближайшее время.</p>
            <p>Если у вас возникнут вопросы, не стесняйтесь <a href='mailto:support@example.com'>связаться с нашей службой поддержки</a>.</p>
            <p>С уважением,<br>Команда поддержки</p>
            <a href='https://www.example.com' class='button'>Посетить наш сайт</a>
        </div>
        <div class='footer'>
            <p>&copy; 2024 ООО Ремонтер. Все права защищены.</p>
        </div>
    </div>
</body>
</html>";
        message.From = new MailAddress("bananjekrd@gmail.com", "OOO Remonter", false);
        message.To.Add(new MailAddress(userMail, "OOO Remonter", false));

        // Укажите кодировку 
        message.BodyEncoding = Encoding.ASCII;

        // Создайте экземпляр класса SmtpClient.
        SmtpClient client = new SmtpClient();

        // Укажите свой почтовый хост, имя пользователя, пароль, номер порта и параметр безопасности.
        client.Host = "smtp.gmail.com";
        client.Username = "bananjekrd@gmail.com";
        client.Password = "njlc jlfo bkbb qovl";
        client.Port = 587;
        client.SecurityOptions = SecurityOptions.SSLExplicit;

        try
        {
            // Отправить это письмо
            await client.SendAsync(message);
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.ToString());
        }
    }       
}
