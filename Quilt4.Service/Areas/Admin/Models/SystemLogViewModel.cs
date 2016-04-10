using System.Collections.Generic;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Areas.Admin.Models
{
    public class SystemLogViewModel
    {
        public List<IServiceLogItem> Entries { get; set; }
    }
}