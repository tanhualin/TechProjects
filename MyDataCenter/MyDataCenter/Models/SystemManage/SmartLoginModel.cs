using System.ComponentModel.DataAnnotations;

namespace MyDataCenter.Models.SystemManage
{
    public class SmartLoginModel
    {
        [Required(ErrorMessage = "用户不能为空")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "购买商品数量不得为空")]
        [Range(1, 999, ErrorMessage = "购买商品数量必须介于1~999之间")]
        public int Number { get; set; } = 0;
    }
}
