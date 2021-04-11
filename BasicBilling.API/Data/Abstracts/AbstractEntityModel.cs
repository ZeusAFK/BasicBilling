using System;

namespace BasicBilling.Data.Abstracts
{

  public abstract class AbstractEntityModel
  {
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public AbstractEntityModel(int Id)
    {
      this.Id = Id;
    }

    public AbstractEntityModel() : base()
    {

    }
  }
}