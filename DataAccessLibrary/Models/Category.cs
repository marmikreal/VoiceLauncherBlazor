﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLibrary.Models
{
    /// <summary>
    /// Category is used to identify certain launch or custom IntelliSense items :-)
    /// </summary>
    public partial class Category
    {
        public Category()
        {
            CustomIntelliSense = new HashSet<CustomIntelliSense>();
            Launchers = new HashSet<Launcher>();
        }
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(30)]
        [Column("Category")]
        [Required(ErrorMessage = "The Category Name is required!")]
        public string CategoryName { get; set; }
        [Column("Category_Type")]
        [StringLength(255)]
        [Required(ErrorMessage = "The Category Type is required!")]
        public string CategoryType { get; set; }
        public bool Sensitive { get; set; } = false;
        [StringLength(40)]
        public string Colour { get; set; }
        [StringLength(50)]
        public string Icon { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<CustomIntelliSense> CustomIntelliSense { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<Launcher> Launchers { get; set; }
    }
}
