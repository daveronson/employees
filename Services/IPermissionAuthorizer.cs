﻿namespace EmployeeDirectory.Services
{
    using System.Threading.Tasks;
    using Domain;

    public interface IPermissionAuthorizer
    {
        Task<bool> HasPermission(string username, Permission permission);
    }
}