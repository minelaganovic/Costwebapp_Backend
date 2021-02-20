using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CostApp.Domain
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonPropertyName("product_record")]
        [StringLength(200)]
        public string Name { get; set; }

        [JsonPropertyName("product_date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("product_category")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [JsonPropertyName("product_price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [JsonPropertyName("product_qty")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [NotMapped]
        public decimal Total { get; set; }

    }
}
