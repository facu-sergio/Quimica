namespace FormulasApi.DTOS
{
    public class AuditableDto 
    {
        public DateTime? Fecha { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
