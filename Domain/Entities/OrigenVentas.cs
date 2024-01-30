namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("origenventas")]
    public class OrigenVentas
    { 
        [Column("corivta", TypeName = "varchar")]
        public string Corivta { get; set; }

        [Column("cori_des", TypeName = "varchar")] 
        public string CoriDes { get; set; }

        [Column("cori_ref", TypeName = "bit")]
        public bool CoriRef { get; set; }

        [Column("coridat_id", TypeName = "varchar")]
        public string CoridatId { get; set; }
    }
}
