using System.ComponentModel.DataAnnotations;
using System;

namespace TableDefinitionBrowser.Models
{
    public class TableDefinition
    {
        [Key]
        [Display(Name = "テーブル物理名")]
        public string PhysicalTableName { get; set; }
        [Required]
        [Display(Name = "テーブル論理名")]
        public string LogicalTableName { get; set; }
        [Display(Name = "カテゴリ")]
        public string Category { get; set; }
        [Display(Name = "オーナー")]
        public string Owner { get; set; }
        [Display(Name = "備考")]
        public string Remarks { get; set; }
        [Display(Name = "作成者")]
        public string CreatedBy { get; set; }
        [Display(Name = "作成日時")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "最終更新者")]
        public string UpdatedBy { get; set; }
        [Display(Name = "最終更新日時")]
        public DateTime UpdatedAt { get; set; }
    }
}
