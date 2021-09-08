﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
    public class SystemEmail
    {
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

	public class TaskNotification
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        ScheduleNotificationAmended _work;

        public SystemEmail GetEmailTemplate()
        {
            return new SystemEmail()
            {
                Subject = "Spiderdocs Document Amended:#id_doc# #title#",
                Body = 
@"Hi Guys

The following document has been amended:

Spiderdocs Id: #id_doc#

Document Name: #title#

New Version No: #version#

Date Amended: #amendedDate#

Amended By: #amendedBy#

Reason: #reason#

This email was generated by Spider Docs Win Service.
Please contact Takeo(takeo@spiderdevelopments.com.au) if you have any issue.
"
            };
        }

        public TaskNotification(ScheduleNotificationAmended work)
        {
            _work = work;
        }
        
        public void Run()
        {
            var emailtemplate = GetEmailTemplate();
            var recievers = ViewNotificationAmendedController.Get(_work.id_doc,_work.new_version);
            var body = recievers.FirstOrDefault() ?? new ViewNotificationAmended();

            if (recievers.Count == 0)
            {
                logger.Info("{0} has been unset notification gorup or deleted", _work.id_doc);
                return; //Could be removed or archived.
            }
            
            System.Collections.Generic.List<string> addres = recievers.Select(x => x.email).ToList();
            
            string subject = Replace(emailtemplate.Subject, body), mailBody = Replace(emailtemplate.Body, body);
            
            Notify(addres,subject,mailBody);
        }

        string Replace(string source, object myObject)
        {
            Type myType = myObject.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(myObject, null);

                source = source.Replace($"#{prop.Name}#", DateTime.Now.GetType() == propValue.GetType() ? ((DateTime)propValue).ToString("r", System.Globalization.CultureInfo.CreateSpecificCulture("en-AU")) : propValue.ToString());
            }         

            return source;   
        }

        void Notify(List<string> tos, string subject, string body)
        {            
            try
            { 
                MailSettingss MailSettingss = new MailSettingss();
                MailSettingss.Load();
                
                Spider.Net.Email email = new Spider.Net.Email(MailSettingss.server);
                email.to = tos;//tos
                email.subject = subject;
                email.body = body;
                email.MultiThread = false;
                email.IsBodyHtml = false;
                
                email.Send();
                
                logger.Info("An email is sent successfully, {0}, sent to {1}", subject, string.Join(",",tos));

                if (logger.IsDebugEnabled) logger.Debug("Email({0}) Body:{1}", subject,body);
            }
            catch (Exception ex)
            {
                logger.Info(ex,"Sending email failed");

            }
        }


    }
}
