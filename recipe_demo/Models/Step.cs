using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace recipe_demo.Models
{
    [Table("Steps")]
    public class Step
    {
        [PrimaryKey , AutoIncrement]
        public int Step_ID { get; set; }

        public string Step_Details { get; set; }

        [ForeignKey(typeof(Recipe))]
        public int Recipe_Id { get; set; }
    }
}
