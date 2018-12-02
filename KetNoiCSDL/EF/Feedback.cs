namespace KetNoiCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("Tên")]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Điện thoại")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Email { get; set; }

        [StringLength(50)]
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Address { get; set; }

        [StringLength(250)]
        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }
    }
}
