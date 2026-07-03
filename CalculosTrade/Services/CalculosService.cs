using CalculosTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;


namespace CalculosTrade.Services
{
    public class CalculosService
    {
        private readonly string _connectionString;

        public CalculosService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
       

        public CalculoResultadoDto Ejecutar(CalculosDto dto)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand("[dbo].[Calculos_Trade]", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pOrigen", dto.POrigen);
                cmd.Parameters.AddWithValue("@pTipoDcto", dto.PTipoDcto);
                cmd.Parameters.AddWithValue("@pNroDcto", dto.PNroDcto);

                conn.Open();
                cmd.ExecuteNonQuery();

                return new CalculoResultadoDto
                {
                    POrigen = dto.POrigen,
                    PTipoDcto = dto.PTipoDcto,
                    PNroDcto = dto.PNroDcto,
                    Exitoso = true,
                    Mensaje = "Cargado correctamente"
                };
            }
            catch (SqlException ex)
            {
                return new CalculoResultadoDto
                {
                    POrigen = dto.POrigen,
                    PTipoDcto = dto.PTipoDcto,
                    PNroDcto = dto.PNroDcto,
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }
    }

}
