namespace EmployeeDirectory.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain;

    public class EmployeeEditModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public Office Office { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
    }
}