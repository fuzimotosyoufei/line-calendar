using System;
using System.ComponentModel.DataAnnotations;

namespace ShiftManagerApi.Models
{
    public class ShiftEntity
    {
        [Key]
        public int Id { get; set; } // 自動で増えるID（主キー）

        [Required]
        public string Name { get; set; } = string.Empty; // 氏名

        [Required]
        public string DateString { get; set; } = string.Empty; // 📅 「2026-06-15」 などの日付文字列

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 登録日時
    }
}
