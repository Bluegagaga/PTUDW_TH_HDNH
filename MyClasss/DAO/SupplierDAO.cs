﻿using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasss.DAO
{
    public class SupplierDAO
    {
        private MyDBContext db = new MyDBContext();

        //SELECT *FROM
        public List<Suppliers> getList()
        {
            return db.Suppliers.ToList();

        }
        //SELECT * FROM cho Index chi voi status 1 va 2
        public List<Suppliers> getList(string status = "ALL")//status 0,1,2
        {
            List<Suppliers> list = null;
            switch (status)
            {
                case "Index"://1,2
                    {
                        list = db.Suppliers.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash"://0
                    {
                        list = db.Suppliers.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Suppliers.ToList();
                        break;
                    }
            }
            return list;
        }
        public Suppliers getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Suppliers category = null;
            return category;
        }


        //tao moi
        public int Insert(Suppliers row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }

        //cap nhat mau tin
        public int Update(Suppliers row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();

        }

        //xoa mau tin
        public int Delete(Suppliers row)
        {
            db.Suppliers.Remove(row);
            return db.SaveChanges();
        }
    }
}
