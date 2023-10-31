using ABC.DataAccess.Data;
using ABC.DataAccess.Repository.IRepository;
using ABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ABC.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private AppDBContext _db;
		public ProductRepository(AppDBContext db) : base(db)
		{
			_db = db;
		}


		public void Update(Product obj)
		{
			var objFromDB = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
			if (objFromDB != null)
			{
				objFromDB.Barcode = objFromDB.Barcode;
                objFromDB.SKU = objFromDB.SKU;
                objFromDB.productName = objFromDB.productName;
                objFromDB.Category = objFromDB.Category;
				objFromDB.subCategory = objFromDB.subCategory;
                objFromDB.Brand = objFromDB.Brand;
                objFromDB.Warehouse = objFromDB.Warehouse;
                objFromDB.Description = objFromDB.Description;
                objFromDB.CostPrice = objFromDB.CostPrice;
                objFromDB.RetailPrice = objFromDB.RetailPrice;
                objFromDB.StockQuantity = objFromDB.StockQuantity;
                objFromDB.MinimumStockQuantity = objFromDB.MinimumStockQuantity;
                objFromDB.Type = objFromDB.Type;
                objFromDB.Duration = objFromDB.Duration;
                objFromDB.Provider = objFromDB.Provider;
                objFromDB.SpecOne = objFromDB.SpecOne;
                objFromDB.SpecTwo = objFromDB.SpecTwo;
                objFromDB.SpecThree = objFromDB.SpecThree;
                objFromDB.addNotes = objFromDB.addNotes;
                objFromDB.SupplierId = objFromDB.SupplierId;

                if (obj.ImageUrl != null)
                {
                    objFromDB.ImageUrl = obj.ImageUrl;
                }
            }
		}
	}
}
