namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("medios")]
    public class Medios
    {
        [Column("med_id", TypeName = "int")]
        public int MedId { get; set; }

        [Column("med_des", TypeName = "varchar")]
        public string? MedDescription { get; set; }

        [Column("med_est", TypeName = "bit")]
        public bool MedStatus { get; set; }

        [Column("med_name", TypeName = "varchar")]
        public string MedName { get; set; }
    }
}
