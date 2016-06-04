﻿using Owin;

namespace Api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IAppBuilder UseLogger(this IAppBuilder app)
        {
            return app.Use<NLogMiddleware>();
        }
        
    }
}
