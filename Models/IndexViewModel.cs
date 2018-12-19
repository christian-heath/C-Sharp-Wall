using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace wall.Models
{
        public class IndexViewModel
        {
        public User User{get; set;}
        public LoginUser LoginUser{get; set;}
        }
}