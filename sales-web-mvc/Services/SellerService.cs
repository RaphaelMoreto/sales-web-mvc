﻿using sales_web_mvc.Data;
using sales_web_mvc.Models;

namespace sales_web_mvc.Services
{
    public class SellerService
    {
        private readonly sales_web_mvcContext _context;

        public SellerService(sales_web_mvcContext context)
        {
            _context = context;
        }

        //OPERAÇÃO PARA RETORNAR UMA LISTA COM TODOS OS VENDEDORES DO BANCO BANCO DE DADOS
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
