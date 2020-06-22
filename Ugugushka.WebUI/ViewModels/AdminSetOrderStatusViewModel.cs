using System;
using System.ComponentModel.DataAnnotations;
using Ugugushka.Common.Concretes;

namespace Ugugushka.WebUI.ViewModels
{
    public class AdminSetOrderStatusViewModel
    {
        [Required]
        public Guid OrderId { get; set; }
        
        [Range(0, 2)]
        [Required]
        public OrderStatus Status { get; set; }
    }
}
