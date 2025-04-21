﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IKEA.DAL.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public bool IsAgree { get; set; }
    }
}
