using System.ComponentModel.DataAnnotations;

namespace FormulasApi.DTOS
{
    public class ShipmentUpdateDTO
    {
        [Required(ErrorMessage = "El campo id es obligatorio.")]
        public int Id { get; set; }
        public string ClientName { get; set; }
        public float Price { get; set; }
        public string Note { get; set; } 

        public int AddressId { get; set; }  
        public int LocationId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        public int? State { get; set; }
        public DateTime Date { get; set; }
        public List<ProductShipmentDto> Products { get; set; } = new List<ProductShipmentDto>();
    }
}
