using ClassDemoCakesLib.collection;
using ClassDemoCakesLib.model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassDemoCakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KagerController : ControllerBase
    {
        private CakeRepositoryService _data;

        public KagerController(CakeRepositoryService repo)
        {
            _data = repo;
        }



        // GET: api/<KagerController>
        [HttpGet]
        public IEnumerable<Cake> Get()
        {
            return _data.GetAll();
        }

        // GET api/<KagerController>/5
        [HttpGet("{id}")]
        public Cake Get(int id)
        {
            return _data.GetById(id);
        }

        // POST api/<KagerController>
        [HttpPost]
        public void Post([FromBody] Cake value)
        {
            _data.Add(value);
        }

        // PUT api/<KagerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cake value)
        {
            _data.Edit(id,value);
        }

        // DELETE api/<KagerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _data.Delete(id);
        }
    }
}
