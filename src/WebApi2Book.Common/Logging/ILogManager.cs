// ILogManager.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using log4net;

namespace WebApi2Book.Common.Logging
{
    public interface ILogManager
    {
        ILog GetLog(Type typeAssociatedWithRequestedLog);
    }
}