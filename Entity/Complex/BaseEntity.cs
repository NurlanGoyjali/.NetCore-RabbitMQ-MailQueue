using System.ComponentModel.DataAnnotations;

namespace Entity.Complex;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public int IsDeleted { get; set; } = 0;
    public int IsActive { get; set; } = 1;
    
    [DataType(DataType.Date)]
    public DateTime CreateAt { get; set; } = DateTime.Now;
    [DataType(DataType.Date)]
    public DateTime UpdateAt { get; set; } = DateTime.Now;
    
}