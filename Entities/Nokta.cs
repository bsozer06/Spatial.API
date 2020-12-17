using System;

namespace Calismam1.Entities
{
    public class Nokta
    {
        int Id { get; set; }
        string Ad { get; set; }
        DateTime CreatedTime { get; set; }
        int IlId { get; set; }
        int IlceId { get; set; }
        int MahalleId { get; set; }
        bool isActive { get; set; }
        string Aciklama { get; set; } 

        public Kisi KisiId { get; set; }
    }
}