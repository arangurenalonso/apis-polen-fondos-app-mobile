namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("lineas")]
    public class Lineas
    {
        [Column("nlin_id", TypeName = "int")]
        public int NlinId { get; set; }

        [Column("nlin_des", TypeName = "varchar")]
        public string NlinDes { get; set; }

        [Column("nlin_skey", TypeName = "varchar")]
        public string NlinSkey { get; set; }
    }
}
