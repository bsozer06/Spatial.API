namespace Calismam1.Contracts
{
    public interface IKisiler
    {
        int Id { get; set; }
        string KisiAdi { get; set; }
        string Meslek { get; set; }
        string Cinsiyet { get; set; }
        int Yas { get; set; }
    }
}