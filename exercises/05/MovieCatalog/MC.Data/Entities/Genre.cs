namespace MC.Data.Entities
{
    public class Genre : BaseEntity
    {
        required public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
