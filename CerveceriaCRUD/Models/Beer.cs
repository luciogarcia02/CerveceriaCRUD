namespace CerveceriaCRUD.Models
{
    public class Beer
    {
        public Beer() {
            this.Name = "";
            this.Description = "";
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public float Graduation { get; set; }
    }
}
