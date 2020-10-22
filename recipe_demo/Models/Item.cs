using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace recipe_demo.Models
{
    [Table("Items")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }

        [MaxLength(255)]
        public string ItemExplanation { get; set; }

        [ForeignKey(typeof(Recipe))]
        public int RecipeId { get; set; }
    }
}
