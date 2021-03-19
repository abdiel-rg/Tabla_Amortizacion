using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabla_Amortizacion.Data;

namespace Tabla_Amortizacion.Pages
{
    public partial class TablaAmortizacion
    {
        public TablaAmortizacion(double Monto, double TasaAnual, int PlazoAnios)
        {
            this.Monto = Monto;
            this.TasaAnual = TasaAnual;
            this.PlazoAnios = PlazoAnios;
        }

        public TablaAmortizacion() { }

        public double Monto { get; set; }
        public double TasaAnual { get; set; }
        public int PlazoAnios { get; set; }

        public double TasaMensual => TasaAnual / 100 / 12;
        public int PlazoMeses => PlazoAnios * 12;

        public double CuotaProg => Monto * (TasaMensual * Math.Pow(1 + TasaMensual, PlazoMeses) / (Math.Pow(1 + TasaMensual, PlazoMeses) - 1));

        public IEnumerable<Pago> Pagos()
        {
            Pago pago = new() { BalanceInicial = Monto };

            for (int i = 1; i <= PlazoMeses; i++)
            {
                pago.Numero = i;
                pago.Intereses = pago.BalanceInicial * TasaMensual;
                pago.AbonoCapital = CuotaProg - pago.Intereses;
                pago.BalanceFinal = pago.BalanceInicial - pago.AbonoCapital;

                yield return pago;

                pago.BalanceInicial = pago.BalanceFinal;
            }
        }
    }
}
