using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NexMed.Web.Helpers
{
    public static class CookieExtensions
    {
        public static string GetCookieValue(this HttpRequest httpRequest, string key)
        {
            var cookie = httpRequest.Cookies[key];
            if (cookie == null)
            {
                return null;
            }

            return cookie.Value;
        }

        public static void RemoveCookie(this HttpRequest httpRequest, HttpCookie cookie)
        {
            cookie.Expires = new DateTime(1999, 1, 1);
            cookie.Value = string.Empty;
        }

        public static void SetCookie(this HttpRequest httpRequest, HttpResponse httpResponse, string key, string value)
        {
            var cookie = httpResponse.Cookies[key];
            if (value == null)
            {
                if (cookie != null)
                {
                    RemoveCookie(httpRequest, cookie);
                }
            }
            else
            {
                if (cookie == null)
                {
                    cookie = new HttpCookie(key);
                    httpResponse.Cookies.Add(cookie);
                }

                cookie.Expires = DateTime.MinValue;
                cookie.Path = "/";
                cookie.Value = value;
            }
        }
    }
}