namespace Domain.Entities
{
    public class Employee: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate {  get; set; }
        public string Designation {  get; set; }

    }
}
