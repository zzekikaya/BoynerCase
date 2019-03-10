using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boyner.Web.UI.Models
{
    public class ConfigModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Value { get; set; }
        [DisplayName("Status")]
        [Required]
        public bool IsActive { get; set; }
        [DisplayName("Application Name")]
        [Required]
        public string ApplicationName { get; set; }
    }
}
