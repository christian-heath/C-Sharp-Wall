using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace wall.Models
{
        public class WallViewModel
        {
        public Message Message{get; set;}
        public Comment Comment{get; set;}
        }
}