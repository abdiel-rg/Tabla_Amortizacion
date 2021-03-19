using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tabla_Amortizacion.Data
{
    public class Pago
    {
        public int Numero { get; set; }

        public double BalanceInicial { get; set; }

        public double Intereses { get; set; }

        public double AbonoCapital { get; set; }

        public double BalanceFinal { get; set; }
    }
}
