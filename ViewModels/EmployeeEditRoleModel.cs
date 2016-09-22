namespace EmployeeDirectory.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class EmployeeEditRoleModel : IValidatableObject
    {
        public int Id { get; set; }

        public List<int> SelectedRoles { get; set; } = new List<int>();
        public List<EmployeeRoleModel> Roles { get; set; }

        public class EmployeeRoleModel
        {
            public bool IsSelected { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!SelectedRoles.Any())
                yield return new ValidationResult("An employee must belong to at least one role.");
        }
    }
}