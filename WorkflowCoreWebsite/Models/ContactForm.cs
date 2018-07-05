using System;
using System.ComponentModel.DataAnnotations;

namespace WorkflowCoreWebsite.Models
{
    public class ContactForm
    {
        [Required]
        [EmailAddress]
        [StringLength(254)]
        public string From { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public Guid Key { get; set; } = Guid.NewGuid();

        public bool Acknowledged { get; set; }
    }
}