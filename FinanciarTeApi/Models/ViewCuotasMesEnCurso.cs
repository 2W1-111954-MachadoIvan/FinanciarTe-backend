using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewCuotasMesEnCurso
{
    public string Descripcion { get; set; } = null!;

    public int? Cantidad { get; set; }
}
