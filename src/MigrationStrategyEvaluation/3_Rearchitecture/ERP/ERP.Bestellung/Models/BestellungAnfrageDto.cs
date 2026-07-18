namespace ERP.Bestellung.Models
{
    public class BestellungAnfrageDto
    {
        public int KundeId { get; set; }
        public List<BestellpositionAnfrageDto> Positionen { get; set; } = [];
    }

    public class BestellpositionAnfrageDto
    {
        public int ArtikelId { get; set; }
        public int Menge { get; set; }
    }
}
