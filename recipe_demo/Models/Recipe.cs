using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace recipe_demo.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int RecipeId { get; set; }

        [MaxLength(255)]
        public string RecipeName { get; set; }

        [MaxLength(255)]
        public string Explanation { get; set; }

        [MaxLength(255)]
        public string  SetDate { get; set; }

        //todo: 写真はあとで別のクラスに分けて、たくさん追加できるようにする。ひとまずは１つだけ。
        [MaxLength(255)]
        public string Photo_filepath { get; set; }

        public byte Photo_byte { get; set; }

        [OneToMany]
        public List<Item> Items { get; set; }

        [OneToMany]
        public List<Step> Steps { get; set; }
    }
}
