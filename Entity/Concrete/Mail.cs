using System.Globalization;
using Entity.Complex;

namespace Entity.Concrete;

public class Mail : BaseEntity
{
    public string ToAddress { get; set; }
    public string Header { get; set; }
    public string Text { get; set; }
    public string? Sign { get; set; }
}