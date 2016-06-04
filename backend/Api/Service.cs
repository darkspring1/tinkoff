﻿using System;
using Microsoft.Owin.Hosting;

namespace Api
{
    public class Service
    {
        IDisposable _app;
        
        public void Start(string url, int port) {
            _app = WebApp.Start<App>(string.Format("{0}:{1}", url, port));
        }

        public void Stop()
        {
            App.Container.Dispose();
            _app.Dispose();
        }
    }
}
