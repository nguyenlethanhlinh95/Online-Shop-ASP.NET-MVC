﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Models
{
    public class ContentModel
    {
        [StringLength(250)]
        [Required(ErrorMessage ="Bắt buộc *")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public long? CategoryID { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Bắt buộc *")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        [StringLength(2)]
        public string Language { get; set; }
    }
}