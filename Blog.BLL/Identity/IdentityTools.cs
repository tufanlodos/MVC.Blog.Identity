﻿using Blog.DAL.Context;
using Blog.Entity.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Identity
{
    public static class IdentityTools //bir çok yerde user role işlemleri için bunlar lazım olduğunda çekebilecez.
    {
        public static UserStore<ApplicationUser> NewUserStore() => new UserStore<ApplicationUser>(new BlogContext());
        public static UserManager<ApplicationUser> NewUserManager() => new UserManager<ApplicationUser>(NewUserStore());
        public static RoleStore<ApplicationRole> NewRoleStore() => new RoleStore<ApplicationRole>(new BlogContext());
        public static RoleManager<ApplicationRole> NewRoleManager() => new RoleManager<ApplicationRole>(NewRoleStore());
    }
}
