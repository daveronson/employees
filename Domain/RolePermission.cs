namespace EmployeeDirectory.Domain
{
    public class RolePermission : Entity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}