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
        public int Item_id { get; set; }

        [MaxLength(255)]
        public string Item_Explanation { get; set; }

        [ForeignKey(typeof(Recipe))]
        public int Recipe_Id { get; set; }
    }
}
