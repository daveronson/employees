namespace EmployeeDirectory.ViewModels
{
    using System;
    using Domain;

    public class EmployeeIndexModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public Office Office { get; set; }
    }
}