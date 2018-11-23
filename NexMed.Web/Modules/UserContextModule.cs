using NexMed.Entities;
using NexMed.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NexMed.Web.Modules
{
    public class UserContextModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        protected virtual void OnBeginRequest(object sender, EventArgs e)
        {
            var user = CookieUser.GetUser();
            HttpContext.Current.Items.Add("User", user);
        }
    }
}