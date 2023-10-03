namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("states_contactos", Schema = "appasrdir")]
    public class StatesContactosEntity
    {

        [Key]
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("title", TypeName = "varchar")]
        public string Title { get; set; }

    }
}
