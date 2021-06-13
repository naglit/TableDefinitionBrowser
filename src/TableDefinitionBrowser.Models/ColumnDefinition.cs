using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableDefinitionBrowser.Models
{
	public class ColumnDefinition
    {
        [Key]
        [Column(Order = 1)]
        [Display(Name = "テーブル物理名")]
        public string TableName { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(Name = "列物理名")]
        public string PhysicalColumnName { get; set; }
        [Required]
        [Display(Name = "列論理名")]
        public string LogicalColumnName { get; set; }
        [Required]
        [Display(Name = "データ型")]
        public string DataType { get; set; }
        [Display(Name = "サイズ")]
        public string Size { get; set; }
        [Required]
        [Display(Name = "Nullを許容しない")]
        public bool IsNotNull { get; set; }
        [Display(Name = "初期値")]
        public string Default { get; set; }
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
