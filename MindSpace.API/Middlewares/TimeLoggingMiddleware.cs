﻿
using System.Diagnostics;

namespace MindSpace.API.Middlewares
{
    public class TimeLoggingMiddleware : IMiddleware
    {
        // =======================================
        // === Fields & Props
        // =======================================

        private readonly ILogger<TimeLoggingMiddleware> _logger;
        private Stopwatch _stopwatch;

        // =======================================
        // === Constructors
        // =======================================

        public TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        // =======================================
        // === Methods
        // =======================================

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            // Start the timer
            _stopwatch = Stopwatch.StartNew();

            await next.Invoke(context);

            // Stop the timer
            _stopwatch.Stop();

            if (_stopwatch.ElapsedMilliseconds > 0)
            {
                var elapsedTime = _stopwatch.ElapsedMilliseconds;
                var httpRequestVerb = context.Request.Method;
                var httpRequestPath = context.Request.Path;

                _logger.LogInformation("Request [{HttpVerb}] at {HttpPath} took {ElapsedTime} ms", httpRequestVerb, httpRequestPath, elapsedTime);
            }
        }
    }
}
