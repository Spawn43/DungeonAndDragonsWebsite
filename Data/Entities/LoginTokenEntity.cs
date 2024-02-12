using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{ 
    public class LoginTokenEntity
    {
        [Key]
        public string Id { get; set; }
        public UserEntity User { get; set; }
        public string Token { get; set; }
        public int TTL { get; set; } = 24;
        public DateTime LoginDateTime { get; set; }
    }
}
