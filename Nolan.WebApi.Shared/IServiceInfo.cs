﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.WebApi.Shared
{
    public interface IServiceInfo
    {
        public string Id { get; set; }
        public string CorsPolicy { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyFullName { get; set; }
    }
}
