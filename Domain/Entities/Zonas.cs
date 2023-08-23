namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("zonas")]
    public class Zonas
    {
        [Column("zon_id", TypeName = "int")]
        public int ZonId { get; set; }

        [Column("zon_des", TypeName = "varchar")]
        public string ZonDes { get; set; }
    }
}
