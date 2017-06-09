using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLTE.Models
{
    public class Login
    {
        [DisplayName("登录名")]
        [Required(ErrorMessage = "请输入登录名")]
        public string LoginName { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "请输入密码")]
        public string LoginPassword { get; set; }
        [DisplayName("验证码")]
        [Required(ErrorMessage = "请输入验证码")]
        [StringLength(4,MinimumLength = 4,ErrorMessage = "请输入6位验证码")]
        public string ValidateCode { get; set; }
        
    }
}