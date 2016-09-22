namespace EmployeeDirectory.ViewModels
{
    using System.Collections.Generic;
    using Domain;

    public class EmployeeDetailsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public Office Office { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<EmployeeRoleModel> EmployeeRoles { get; set; }

        public class EmployeeRoleModel
        {
            public string RoleName { get; set; }
        }
    }
}