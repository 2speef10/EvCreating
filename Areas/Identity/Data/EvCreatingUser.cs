﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EvCreating.Areas.Identity.Data;

// Add profile data for application users by adding properties to the EvCreatingUser class
public class EvCreatingUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

