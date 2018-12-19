using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace wall.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int UserId {get; set;}
        public User User{get; set;}
        public int MessageId {get; set;}
        public Message Message{get; set;}        

        [Required]
        [MaxLength(255)]
        [Display(Name = "Comment:")]
        public string comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}