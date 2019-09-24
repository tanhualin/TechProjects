namespace MyDataCenter.Models
{
    public class JwtTokenModel
    {
        public string Token { get; set; }
        public TokenUserModel User { get; set; }
    }

    public class TokenUserModel
    {
        /// <summary>用户名</summary>
        public virtual string Name { get; set; }
        /// <summary>[Email]</summary>
        public virtual string Email { get; set; }
        /// <summary>[Phone]</summary>
        public virtual string Phone { get; set; }
        /// <summary>图片地址</summary>
        public virtual string Avatar { get; set; }
    }
}
