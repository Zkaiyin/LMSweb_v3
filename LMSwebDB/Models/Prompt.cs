namespace LMSwebDB.Models
{
    public class Prompt
    {
        public int Id { get; set; }
        public required string Name { get; set; } // 这个字段用于存储提示语模板名称
    }
}
