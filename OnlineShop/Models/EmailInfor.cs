using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class EmailInfor
    {
        [DisplayName("Tên")]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Name { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Email { get; set; }

        [DisplayName("Điện thoại")]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Phone { get; set; }

        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Content { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
    }
}