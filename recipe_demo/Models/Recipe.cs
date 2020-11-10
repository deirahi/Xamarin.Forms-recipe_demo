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

        [MaxLength(2000)]
        public string Explanation { get; set; }

        [MaxLength(255)]
        public string  SetDate { get; set; }

        //todo: 写真はあとで別のクラスに分けて、たくさん追加できるようにする。ひとまずは１つだけ。
        [MaxLength(255)]
        public string PhotoFilePath { get; set; }

        public byte PhotoByte { get; set; }

        // CascadeOperationsで関連データも同時に操作するか、どの操作を同時に操作するかを決められる
        // https://bitbucket.org/twincoders/sqlite-net-extensions/src/master/SQLiteNetExtensions/Attributes/CascadeOperation.cs
        [OneToMany(CascadeOperations =CascadeOperation.All)]
        public List<Item> Items { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Step> Steps { get; set; }
    }
}
