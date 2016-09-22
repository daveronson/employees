namespace EmployeeDirectory.Domain
{
    public class EmployeeRole : Entity
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public Employee Employee { get; set; }
        public Role Role { get; set; }
    }
}