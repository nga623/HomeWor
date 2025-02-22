﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Application.Contracts.Dtos 
{
  public  class UserCreateDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int UserTypeEnum { get; set; }
        public string UserType { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
    }
}
