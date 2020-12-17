using System;

namespace Calismam1.Contracts
{
    public interface INoktalar
    {
        int Id { get; set; }
        string Ad { get; set; }
        DateTime CreatedTime { get; set; }
        int IlId { get; set; }
        int IlceId { get; set; }
        int MahalleId { get; set; }
        bool isActive { get; set; }
        string Aciklama { get; set; } 
    }
}