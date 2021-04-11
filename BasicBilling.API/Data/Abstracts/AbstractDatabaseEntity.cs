using System;
using System.ComponentModel.DataAnnotations;

namespace BasicBilling.Data.Abstracts
{

  public abstract class AbstractDatabaseEntity
  {
    [Key]
    [Required]
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public AbstractDatabaseEntity(int Id)
    {
      this.Id = Id;
    }

    public AbstractDatabaseEntity() : base()
    {

    }
  }
}