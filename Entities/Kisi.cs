namespace Calismam1.Entities
{
    public class Kisi
    {
        public int Id { get; set; }
        public string KisiAdi { get; set; }
        public string Meslek { get; set; }
        public string Cinsiyet { get; set; }
        public int Yas { get; set; }

        public Nokta NoktaId { get; set; }
    }
}