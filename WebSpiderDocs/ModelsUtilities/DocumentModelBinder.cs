using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpiderDocsModule;
using NLog;

namespace WebSpiderDocs
{
	public class DocumentModelBinder : IModelBinder
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			Document ans = null;
            JObject json = null;
            
            string contentType = controllerContext.HttpContext.Request.ContentType;
            
            // Do nothing if  
            if (false == contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase)) return null;
            try
            {
                // Json to Instance of Document
                using (Stream stream = controllerContext.HttpContext.Request.InputStream)
                using (StreamReader reader = new StreamReader(stream))
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    string bodyText = reader.ReadToEnd();

                    json = JObject.Parse(bodyText);

                    if (logger.IsDebugEnabled) logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(json));

                    ans = JsonConvert.DeserializeObject<Document>(json["Document"].ToString());
                }

                if (0 < ans.Attrs.Count)
                {
                    List<DocumentAttribute> wrks = DocumentAttributeController.GetAttributesCache(attr_id: ans.Attrs.Select(a => a.id).ToArray());

                    for (int i = ans.Attrs.Count - 1; 0 <= i; i--)
                    {
                        DocumentAttribute attr = ans.Attrs[i];
                        DocumentAttribute wrk = wrks.FirstOrDefault(a => a.id == attr.id);

                        if (wrk != null)
                            attr.id_type = wrk.id_type;

                        // Avoid to check combo . it could be count zero.
                        if (!attr.IsValidValue() && !wrk.IsCombo())
                            ans.Attrs.RemoveAt(i);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(json));
            }
            finally
            {

                logger.Debug("Requested Json: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(json));
            }

            return ans;
		}
	}
}