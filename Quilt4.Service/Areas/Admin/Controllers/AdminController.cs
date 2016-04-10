﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quilt4.Service.Areas.Admin.Models;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServiceLog _serviceLog;
        private readonly ISettingBusiness _settingBusiness;
        
        public AdminController(IServiceLog serviceLog, ISettingBusiness settingBusiness)
        {
            _serviceLog = serviceLog;
            _settingBusiness = settingBusiness;
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            var logEntries = _serviceLog.GetAllLogEntries();
            var systemLogViewModel = new SystemLogViewModel { Entries = logEntries.OrderByDescending(x => x.LogTime).ToList() };

            var settings = _settingBusiness.GetSettings();
            var settingsModel = new SettingsModel { Values = settings.ToDictionary(x => x.Name, x => x.Value) };

            return View(new IndexModel { SystemLog = systemLogViewModel, Settings = settingsModel });
        }
    }
}