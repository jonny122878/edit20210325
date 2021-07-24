using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace edit20210325.Models
{
    public class MemberCashInModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("編號")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "請輸入會員帳號")]
        [DisplayName("會員帳號")]
        public string MemberCashInId { get; set; }

        [Required(ErrorMessage = "請輸入已付訂金")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "已付訂金欄位錯誤")]
        [DisplayName("已付訂金")]
        public int MemberCashInCash { get; set; }

        [Required(ErrorMessage = "請輸入訂製商品")]
        [DisplayName("訂製商品")]
        public string MemberCashInName { get; set; }

        [DisplayName("進度說明")]
        public string MemberCashInRemarks { get; set; }

        [DisplayName("給付原因")]
        public string MemberCashInSpace1 { get; set; }

        [DisplayName("客戶備註")]
        public string MemberCashInSpace2 { get; set; }

        [DisplayName("商家備註")]
        public string MemberCashInSpace3 { get; set; }

        [Required(ErrorMessage = "日期錯誤")]
        [DataType(DataType.Date, ErrorMessage = "日期錯誤")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("建立日期")]
        public DateTime MemberCashInCrtDate { get; set; }

        [Required(ErrorMessage = "日期錯誤")]
        [DataType(DataType.Date, ErrorMessage = "日期錯誤")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("最後變更日期")]
        public DateTime MemberCashInChgDate { get; set; }
    }
}
