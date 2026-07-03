using CalculosTrade.Models;
using CalculosTrade.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculosTrade.Controllers
{
    public class CalculosController : Controller
    {
        private readonly ExcelService _excelService;
        private readonly CalculosService _calculosService;

        public CalculosController(
            ExcelService excelService,
            CalculosService calculosService)
        {
            _excelService = excelService;
            _calculosService = calculosService;
        }

        // 👉 ESTA ACCIÓN ES OBLIGATORIA
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CargarExcel(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                ModelState.AddModelError("", "Debe seleccionar un archivo");
                return View("Index");
            }

            var registros = _excelService.LeerExcel(archivo);
            var resultados = new List<CalculoResultadoDto>();

            foreach (var item in registros)
            {
                resultados.Add(_calculosService.Ejecutar(item));
            }

            return View("Resultado", resultados);
        }
    }
}
