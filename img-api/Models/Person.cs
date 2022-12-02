using System.ComponentModel;

namespace img_api.Models
{
    public class Person
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Adding Date")]
        public DateTime AddingDate { get; set; }
        [DisplayName("Picture")]
        public byte[] Image { get; set; }
    }
}
