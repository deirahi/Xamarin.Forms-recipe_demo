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
        public int StepId { get; set; }

        public int StepOrder { get; set; }
        [MaxLength(512)]
        public string StepDetails { get; set; }

        [ForeignKey(typeof(Recipe))]
        public int RecipeId { get; set; }

        [ManyToOne]
        public Recipe Recipe { get; set; }
    }
}
