﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace edit20210325.Models
{
    public class DepositOrderLogModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("編號")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "請輸入會員帳號")]
        [DisplayName("會員帳號")]
        public string MemberId { get; set; }

        [Required(ErrorMessage = "請輸入金額")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "輸入金額錯誤")]
        [DisplayName("給付金額")]
        public int MemberCashInCash { get; set; }

        [Required(ErrorMessage = "在製品選擇")]
        [DisplayName("在製品選擇")]
        public string CashInId { get; set; }

        [DisplayName("客戶備註")]
        public string MemberCashInRemarks1 { get; set; }

        [DisplayName("商家備註")]
        public string MemberCashInRemarks2 { get; set; }

        [Required(ErrorMessage = "日期時間錯誤")]
        [DataType(DataType.DateTime, ErrorMessage = "日期時間錯誤")]
        [DisplayName("建立日期")]
        public DateTime MemberCashInCrtDate { get; set; }
    }
}
