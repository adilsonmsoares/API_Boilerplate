using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Models.Example
{
    [Table(name: "Example", Schema = "ExampleSchema")]
    public class Example : Audit
    {
        [Column("IdExample")]
        [Key]
        public long IdExample { get; set; }

        [Column("Property")]
        public string Property { get; set; }
    }
}
