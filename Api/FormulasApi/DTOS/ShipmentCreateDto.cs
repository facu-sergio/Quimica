namespace FormulasApi.DTOS
{
    public class ShipmentCreateDto
    {
        public string ClientName { get; set; }
        public float Price { get; set; }
        public string Note { get; set; }
        public int LocationId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        
        public int? State { get; set; }
        public DateTime Date { get; set; }
        public List<ProductShipmentDto> Products { get; set; } = new List<ProductShipmentDto>();
    }
}
