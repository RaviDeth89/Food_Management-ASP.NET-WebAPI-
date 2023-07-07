using Ravi_MscIT_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ravi_MscIT_WebAPI.Controllers
{
    public class FoodController : ApiController
    {
        dbFoodWebApiEntities context = new dbFoodWebApiEntities();
        public IHttpActionResult Get()
        {
            return Ok(context.TBLFoodOrders.ToList());
        }
        public IHttpActionResult Post(TBLFoodOrder p)
        {
            try
            {
                if (p.Name == "" || p.Cuisine == "")
                {
                    throw new NullException("Please fill all the details !!");
                }
                else if (p.Quantity < 0)
                {
                    throw new QuantityException("Quantity must be greater than or equal to zero !!");
                }
                else if (p.Price <= 0)
                {
                    throw new PriceException("Price must be greater than zero !!");
                }

                else if (context.TBLFoodOrders.Where(a => a.Name == p.Name).SingleOrDefault() != null)
                {
                    throw new NameException("Name is Already exist !!!");
                }

            }
            catch (ExtraException ex)
            {
                return InternalServerError(ex);
            }
            context.TBLFoodOrders.Add(p);
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var food = context.TBLFoodOrders.Find(id);
                if (food == null)
                    return NotFound();
                context.TBLFoodOrders.Remove(food);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public class NullException : Exception
        {
            public NullException(String msg) : base(msg) { }

        }
        public class PriceException : Exception
        {
            public PriceException(String msg) : base(msg) { }

        }
        public class NameException : Exception
        {
            public NameException(String msg) : base(msg) { }

        }
        public class QuantityException : Exception
        {
            public QuantityException(String msg) : base(msg) { }

        }

        public class ExtraException : Exception
        {
            public ExtraException(String msg) : base(msg) { }

        }
    }
}