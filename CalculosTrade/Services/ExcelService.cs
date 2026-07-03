using CalculosTrade.Models;
using ClosedXML.Excel;

namespace CalculosTrade.Services
{
    public class ExcelService
    {
        public List<CalculosDto> LeerExcel(IFormFile archivo)
        {
            var lista = new List<CalculosDto>();

            using var stream = new MemoryStream();
            archivo.CopyTo(stream);

            using var workbook = new XLWorkbook(stream);
            var hoja = workbook.Worksheet(1);

            foreach (var fila in hoja.RowsUsed().Skip(1))
            {
                lista.Add(new CalculosDto
                {
                    POrigen = fila.Cell(1).GetString().Trim(),
                    PNroDcto = fila.Cell(2).GetString().Trim(),
                    PTipoDcto = fila.Cell(3).GetString().Trim()
                });
            }

            return lista;
        }
    }

}
