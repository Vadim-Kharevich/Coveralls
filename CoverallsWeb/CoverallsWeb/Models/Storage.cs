using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoverallsWeb.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public Coveralls Coveralls { get; set; }
        public int CoverallsId { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, 10000, ErrorMessage = "Недопустимое значение")]
        public int Count { get; set; }
    }
}
