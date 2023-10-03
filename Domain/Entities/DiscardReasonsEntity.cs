namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("discard_reasons", Schema = "appasrdir")]
    public class DiscardReasonsEntity
    {

        [Key]
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("title", TypeName = "varchar")]
        public string? Title { get; set; }
    }
}
