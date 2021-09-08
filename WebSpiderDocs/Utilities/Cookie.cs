using System;
using System.Web;
using Spider.Types;
using Spider.Security;

namespace WebSpiderDocs
{
    public class Cookie
    {
        Crypt crypt = new Crypt("!JustForCookie!");

        string Name;
        DateTime ExpireDate;
        HttpCookie RequestCookie = null;

        public Cookie(string Name, DateTime ExpireDate = new DateTime())
        {
            this.Name = Name;

            if (ExpireDate == new DateTime())
                ExpireDate = DateTime.Now.AddHours(4);

            this.ExpireDate = ExpireDate;

            RequestCookie = HttpContext.Current.Request.Cookies[Name];
        }

        public T GetValue<T>(string key)
        {
            object ans = null;

            if ((RequestCookie != null) && !String.IsNullOrEmpty(RequestCookie[key]))
                ans = Convert.ChangeType(RequestCookie[key], typeof(T));

            return TypeUtilities.ConvertFromObject<T>(ans);
        }

        public void SaveValue(string key, object val)
        {
            SetResponseCookie(key, val.ToString());
        }

        public T GetObject<T>(string key) where T : class
        {
            T ans = null;

            if ((RequestCookie != null) && !String.IsNullOrEmpty(RequestCookie[key]))
            {
                string val = crypt.Decrypt(RequestCookie[key]);

                if (!String.IsNullOrEmpty(val))
                    ans = (T)Spider.Utilities.DeSerializeAnObject<T>(val);
            }

            return ans;
        }

        public void SaveObject<T>(string key, object val)
        {
            string str = crypt.Encrypt(Spider.Utilities.SerializeAnObject(val));
            SetResponseCookie(key, str);
        }

        void SetResponseCookie(string key, string val)
        {
            HttpCookie Cookie = new HttpCookie(Name);
            Cookie.Expires = DateTime.Now.AddHours(4);

            if (RequestCookie != null)
            {
                for (int i = 0; i < RequestCookie.Values.Count; i++)
                {
                    string wrk = RequestCookie.Values.AllKeys[i];

                    if (!String.IsNullOrEmpty(RequestCookie.Values[wrk]))
                        Cookie[wrk] = RequestCookie.Values[wrk];
                }

            }
            else
            {
                HttpContext.Current.Request.Cookies.Add(new HttpCookie(Name));
                RequestCookie = HttpContext.Current.Request.Cookies[Name];
            }

            RequestCookie[key] = val;
            Cookie[key] = val;

            HttpContext.Current.Response.Cookies.Remove(Name);
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }
    }
}
