using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Entity.ViewModel
{
    public class MakaleAddViewModel
    {
        [Required(ErrorMessage = "Lütfen makale başlığı giriniz!")]
        [StringLength(100, ErrorMessage = "Başlık {0} karakterden uzun olmamalıdır!")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Summary { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; } //ckeditor den gelecek
        [StringLength(100)]
        public string Picture { get; set; } //görüntünün adı için
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public HttpPostedFileBase PictureUpload { get; set; } //controller a göndermemiz için gereken bir property. tek görüntü için bu, çoklu için List<HttpPostedFileBase> . eğer ki view içinde type ı file olan bir input kullanıyorsak, onun ismi,uzantsı,yolu bilgilerini çekebilmek için bu türden bir property kullanıyoruz. bir çok property geleceği için, bu şekilde bir property yaptık. sadece görüntü yüklemeye yarayan bir form olsaydı, model yazmazdık; post controller ında parametre olarak HttpPostedFileBase gelirdi.
    }
}
