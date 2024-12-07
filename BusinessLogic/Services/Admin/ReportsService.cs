using Restaurant.DataAccess.Repositories;
using System.Data;
using System;

namespace Restaurant.BusinessLogic.Services
{
    public class ReportService
    {
        private readonly AdminRepository _adminRepository;

        public ReportService()
        {
            _adminRepository = new AdminRepository();
        }

        // Método para obtener reporte semanal
        public DataTable ObtenerReporteSemanal(DateTime fechaInicio, DateTime fechaFin)
        {
            return _adminRepository.ReporteSemanal(fechaInicio, fechaFin);
        }

        // Método para obtener reporte mensual
        public DataTable ObtenerReporteMensual(DateTime mes, DateTime year)
        {
            return _adminRepository.ReporteMensual(mes, year);
        }
    }
}
