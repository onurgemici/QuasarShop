﻿using System.ComponentModel.DataAnnotations;

namespace QuasarShop.Models
{
    public class CarouselImageViewModel
    {
        [Display(Name = "Görsel Dosyası")]

        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Aktif")]
        public bool Enabled { get; set; }

        [Display(Name = "İlk Tarih")]
        [DataType(DataType.Date)]
        public DateTime? DateFirst { get; set; }

        [Display(Name = "Son Tarih")]
        [DataType(DataType.Date)]
        public DateTime? DateEnd { get; set; }

    }
}
