using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoverallsWeb.Models
{
    public class Coveralls
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }
        public CoverallsType Type { get; set; }
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(20, 70, ErrorMessage = "Недопустимое значение")]
        public int Size { get; set; }
        public int? Height { get; set; }
    }
}
