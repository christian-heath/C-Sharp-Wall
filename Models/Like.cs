using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace wall.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int UserId {get; set;}
        public User User{get; set;}

        public int MessageId{get; set;}
        public Message Message{get; set;}
    }
}