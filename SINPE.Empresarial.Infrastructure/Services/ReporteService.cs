using SINPE.Empresarial.Domain.ReporteDomain.Entities;
using SINPE.Empresarial.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SINPE.Empresarial.Infrastructure.Services
{
    public class ReporteService
    {
        // Instancia: Acceso a la base de datos para reportes mensuales.
        private readonly SINPE_Empresarial_DB _context;

        // Constructor: Inicializa el servicio con acceso a la base de datos.
        public ReporteService()
        {
            _context = new SINPE_Empresarial_DB();
        }

        // Método: Obtener todos los reportes mensuales ordenados por fecha descendente.
        public List<ReporteMensual> ObtenerTodos()
        {
            return _context.ReportesMensuales
                .Include("Comercio")
                .OrderByDescending(r => r.FechaDelReporte)
                .ToList();
        }

        // Método: Generar reportes mensuales para todos los comercios.
        public void GenerarReportesMensuales()
        {
            var fechaActual = DateTime.Now;
            var mes = fechaActual.Month;
            var anio = fechaActual.Year;

            var comercios = _context.Comercio
                .Include("Cajas")
                .Include("Configuraciones")
                .ToList();

            foreach (var comercio in comercios)
            {
                // Logica de calculo de comisión
                var cantidadCajas = comercio.Cajas?.Count(c => c.Estado) ?? 0;
                var config = comercio.Configuraciones.FirstOrDefault();
                decimal porcentajeComision = (config?.Comision ?? 0m) / 100m;

                // Obtener las cajas activas - dinero y telefono
                var telefonosCajas = comercio.Cajas.Select(c => c.TelefonoSINPE).ToList();
                var sinpesDelMes = _context.Sinpe
                    .Where(s => telefonosCajas.Contains(s.TelefonoDestinatario)
                                && s.FechaDeRegistro.Month == mes
                                && s.FechaDeRegistro.Year == anio)
                    .ToList();

                // Logica de calculo de los montos y cantidades
                var montoRecaudado = sinpesDelMes.Sum(s => s.Monto);
                var cantidadSinpes = sinpesDelMes.Count;
                var montoComision = montoRecaudado * porcentajeComision;

                // Validar si ya existe un reporte para el comercio en el mes y año actual
                var reporteExistente = _context.ReportesMensuales.FirstOrDefault(r =>
                    r.IdComercio == comercio.IdComercio &&
                    r.FechaDelReporte.Month == mes &&
                    r.FechaDelReporte.Year == anio);

                // Actualizar o crear el reporte mensual
                if (reporteExistente != null)
                {
                    reporteExistente.CantidadDeCajas = cantidadCajas;
                    reporteExistente.MontoTotalRecaudado = montoRecaudado;
                    reporteExistente.CantidadDeSINPES = cantidadSinpes;
                    reporteExistente.MontoTotalComision = montoComision;
                    reporteExistente.FechaDelReporte = fechaActual;
                }
                else
                {
                    _context.ReportesMensuales.Add(new ReporteMensual
                    {
                        IdComercio = comercio.IdComercio,
                        CantidadDeCajas = cantidadCajas,
                        MontoTotalRecaudado = montoRecaudado,
                        CantidadDeSINPES = cantidadSinpes,
                        MontoTotalComision = montoComision,
                        FechaDelReporte = fechaActual
                    });
                }
            }

            _context.SaveChanges();
        }
    }
}