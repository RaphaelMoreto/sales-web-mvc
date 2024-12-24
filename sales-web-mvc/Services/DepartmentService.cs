using sales_web_mvc.Data;
using sales_web_mvc.Models;

namespace sales_web_mvc.Services
{
    public class DepartmentService
    {
        private readonly sales_web_mvcContext _context;

        public DepartmentService(sales_web_mvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            //ACESSANDO O BANCO DE DADOS E RETORNANDO OS DEPARTAMENTOS ORDENADOS POR NOME
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
