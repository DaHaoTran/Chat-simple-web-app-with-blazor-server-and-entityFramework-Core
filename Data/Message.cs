using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatSimple.Data
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime time { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string email { get; set; }
        [ForeignKey("email")]
        public Account account { get; set; }
    }
}
