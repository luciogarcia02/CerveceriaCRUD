namespace CerveceriaCRUD.Models
{
    public class Beer
    {

        public Beer(int id, string name, string description)
        {
            this.Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
