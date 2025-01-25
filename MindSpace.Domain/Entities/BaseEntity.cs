namespace MindSpace.Domain.Entities;

/// <summary>
/// A base entity class
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}