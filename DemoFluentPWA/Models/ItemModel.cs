using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DemoFluentPWA.Models
{
    [BsonIgnoreExtraElements]
    public class ItemModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [Display(Name ="Titolo")]
        public string Title { get; set; }

        [Display(Name = "Descrizione")]
        public string Description { get; set; }

        [Display(Name = "Payoff")]
        public string Payoff { get; set; }

        [Display(Name ="Body")]
        public string Body { get; set; }

        public ImageModel Image { get; set; }
    }
}
