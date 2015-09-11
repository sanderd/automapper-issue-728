using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FailureMap.DbModel
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsPublic { get; set; }

        public string Title { get; set; }

        public virtual User Owner { get; set; }
    }
}