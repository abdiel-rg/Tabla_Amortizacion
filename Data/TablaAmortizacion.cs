using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tabla_Amortizacion.Pages
{
    public partial class TablaAmortizacion
    {
        public double Monto { get; set; }
        public double TasaAnual { get; set; }
        public int PlazoAnios { get; set; }

        public double TasaMensual => TasaAnual / 100 / 12;
        public int PlazoMeses => PlazoAnios * 12;

        public double CuotaProg => Monto * (TasaMensual * Math.Pow(1 + TasaMensual, PlazoMeses) / (Math.Pow(1 + TasaMensual, PlazoMeses) - 1));
    }
}
