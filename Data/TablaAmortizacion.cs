using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double Monto { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double TasaAnual { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public int PlazoAnios { get; set; }

        public double TasaMensual => TasaAnual / 100 / 12;

        public int PlazoMeses => PlazoAnios * 12;

        public (int Numero, double Diferencia) PuntoMedioPrestamo => Pagos()
                    .Select(p => (p.Numero, Diferencia: Math.Abs(p.Intereses - p.AbonoCapital)))
                    .OrderBy(p => p.Diferencia)
                    .First();

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
