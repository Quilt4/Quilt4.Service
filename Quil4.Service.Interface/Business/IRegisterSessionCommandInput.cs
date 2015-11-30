using System;
using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IRegisterSessionCommandInput
    {
        Guid SessionKey { get; }
        string ProjectApiKey { get; }
        DateTime ClientStartTime { get; }
        string Environment { get; }
        IApplication Application { get; }
        IUser User { get; }
        IMachine Machine { get; }
        string CallerIp { get; }
    }

    public interface IMachine
    {
        string Fingerprint { get; }
        string Name { get; }
        IDictionary<string, string> Data { get; }
    }

    public interface IUser
    {
        string Fingerprint { get; }
        string UserName { get; }
    }

    public interface IApplication
    {
        string Name { get; }
        string Version { get; }
        string SupportToolkitNameVersion { get; }
    }
}