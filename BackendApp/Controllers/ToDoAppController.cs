using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoAppController : ControllerBase
    {
        private IConfiguration _configuration;

        public ToDoAppController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetNotes")]
        public JsonResult GetNotes() 
        {
            string query = "select * from notes";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            using var dataSource = NpgsqlDataSource.Create(sqlDatasource);

            // Retrieve all rows
            using (var cmd = dataSource.CreateCommand(query))
            using (var reader = cmd.ExecuteReader())
            {
                table.Load(reader);
            }

            return new JsonResult(table);
        }
        [HttpPost]
        [Route("AddNote")]
        public JsonResult AddNote([FromForm] string newNotes)
        {
            if(newNotes is null || newNotes == string.Empty)
                return new JsonResult("Empty note error");

            string query = $"insert into notes (description) values ('{newNotes}')";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            using var dataSource = NpgsqlDataSource.Create(sqlDatasource);

            using (var cmd = dataSource.CreateCommand(query))
            {
                cmd.ExecuteNonQuery();
            }

            return new JsonResult("Added Succesfully");
        }
        [HttpDelete]
        [Route("DeleteNote")]
        public JsonResult DeleteNote([FromForm] int id)
        {
            string query = $"DELETE FROM notes WHERE id = {id}";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            using var dataSource = NpgsqlDataSource.Create(sqlDatasource);

            using (var cmd = dataSource.CreateCommand(query))
            {
                cmd.ExecuteNonQuery();
            }

            return new JsonResult("Deleted Succesfully");
        }

    }
}
