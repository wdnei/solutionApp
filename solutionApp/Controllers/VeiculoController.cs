using solutionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;
using Newtonsoft.Json;
using static solutionApp.Controllers.BasePaged;
using System.Net.Http;
using System.Net;

namespace solutionApp.Controllers
{
    [RoutePrefix("api/veiculo")]
    [EnableCors(origins: "http://localhost:50542", headers: "*", methods: "*")]
    public class VeiculoController : ApiController
    {
        List<Veiculo> list = new List<Veiculo>();
        solutionAppEntities _db = new solutionAppEntities();

        public VeiculoController()
        {
            _db.Database.CreateIfNotExists();
           
        }


        [Route("getVeiculos")]       
        public List<Veiculo> getVeiculos() {
            todosVeiculos();
            return list;
            
        }

        [Route("getVeiculosCor/{cor}")]
        [HttpGet]
        public List<Veiculo>  getVeiculosCor(string cor) {

            List<Veiculo> list;
            using (solutionAppEntities context = new solutionAppEntities())
            {
                var query =
                    from veiculo in context.Veiculo
                    where veiculo.cor == cor
                    select veiculo;

                list = query.ToList();
            }          
            
            return list;
        }

        [Route("addVeiculo")]
        [HttpPost]
        public List<Veiculo> addVeiculo(Veiculo veiculo)
        {
            Console.Write(veiculo);
           _db.Veiculo.Add(veiculo);
           _db.SaveChanges();

            todosVeiculos();

            return list;
        }

        [Route("updateVeiculo")]
        [HttpPut]
        public List<Veiculo> updateVeiculo(Veiculo veiculo)
        {

            
            var original = _db.Veiculo.Find(veiculo.id);

            if (original != null)
            {
                _db.Entry(original).CurrentValues.SetValues(veiculo);
                _db.SaveChanges();
            }

            Console.Write(veiculo);
           // veiculo  = _db.Veiculo.Attach(veiculo);
            //_db.Entry(veiculo).State = EntityState.Modified;
            //_db.SaveChanges();
                       

            todosVeiculos();

            return list;
        }

        [Route("deleteVeiculo")]
        [HttpDelete]
        public IHttpActionResult deleteVeiculo(int id)
        {

            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new solutionAppEntities())
            {
                var veiculo = ctx.Veiculo
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctx.Entry(veiculo).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
           
        }

        [Route("addMultaVeiculo")]
        [HttpPost]
        public List<Veiculo> addMultaVeiculo(int idmulta, int idveiculo)
        {
            Veiculo veiculo = _db.Veiculo.Find(idveiculo);

            Multa multa = _db.Multa.Find(idmulta);

            veiculo.Multa.Add(multa);

            _db.SaveChanges();

            todosVeiculos();

            return list;
        }

        [Route("addMulta")]
        [HttpPost]
        public List<Multa> addMulta(Multa multa)
        {
            _db.Multa.Add(multa);
            
            _db.SaveChanges();            

            return _db.Multa.ToList();
        }

        [Route("getVeiculosTable")]
        [HttpGet]
        public PagedResults<Veiculo> getVeiculosTable([FromUri]PagingParameterModel pagingparametermodel)
        {


            BasePaged.BasePagedResults<Veiculo> paging = new BasePaged.BasePagedResults<Veiculo>();
            
            PagedResults<Veiculo> pagedResults =  paging.CreatePagedResults(_db.Veiculo.AsQueryable(), pagingparametermodel, this);
            return pagedResults;

        }




        public List<Veiculo> todosVeiculos() {
            list = _db.Veiculo.ToList();
            return list;

        }
   

    }
}