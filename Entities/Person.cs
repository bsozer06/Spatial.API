using System.Collections.Generic;

namespace Spatial.API.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }

        // public int FieldId { get; set; }
        public List<Field> Fields { get; set; }
    }
}