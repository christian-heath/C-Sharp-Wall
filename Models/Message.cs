using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace wall.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public int UserId {get; set;}
        public User User{get; set;}
        public List<Comment> Comments {get; set;}

        [Required]
        [MaxLength(255)]
        [Display(Name = "Message:")]
        public string message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}