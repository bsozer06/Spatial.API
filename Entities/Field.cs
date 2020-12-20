using System;
using NetTopologySuite.Geometries;

namespace Spatial.API.Entities
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public Geometry Geom { get; set; }
        public string Wkt { get; set; }
        public int CityId { get; set; }
        public int NeighId { get; set; }
        public bool IsActive { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}