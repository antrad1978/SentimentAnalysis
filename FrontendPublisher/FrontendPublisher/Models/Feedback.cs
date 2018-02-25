using System;
using System.ComponentModel.DataAnnotations;

namespace FrontendPublisher.Models
{
    public class Feedback
    {
        public Feedback()
        {
            
        }

        public Guid ID { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(128)]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime FeedbackDate { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(1024)]
        public string Text { get; set; }
    }
}
