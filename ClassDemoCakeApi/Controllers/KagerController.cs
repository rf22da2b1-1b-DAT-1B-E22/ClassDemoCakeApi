using ClassDemoCakesLib.collection;
using ClassDemoCakesLib.model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        // til dokumentation i swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get()
        {
            IEnumerable<Cake> cakes = _data.GetAll();

            if (cakes.ToList().Count == 0)
            {
                return NoContent();
            }
            return Ok(cakes);
        }

        // GET api/<KagerController>/5
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                Cake cake = _data.GetById(id);
                return Ok(cake);
            }
            catch (KeyNotFoundException knfe) { 
                return NotFound();
            }
            
        }

        

        // GET: api/<KagerController>
        [HttpGet]
        [Route("Wienerbrød")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetWienerbroed(string kageType)
        {
            // kan også lave en metode i repository'et

            // Hack laver det bare her
            IEnumerable<Cake> cakes = _data.GetAll().ToList().FindAll(k => k.Description.Contains("wienerbrød"));

            if (cakes.ToList().Count == 0)
            {
                return NoContent();
            }
            return Ok(cakes);
        }

        // GET: api/<KagerController>
        [HttpGet]
        [Route("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetFilter([FromQuery] Filter2 filter)
        {
            // kan også lave en metode i repository'et

            // Hack laver det bare her

            List<Cake> cakes = _data.GetAll().ToList();

            if (filter.MaxPris is not null) { 
                cakes = cakes.FindAll(k => k.Price < filter.MaxPris);
            }

            if (cakes.ToList().Count == 0)
            {
                return NoContent();
            }
            return Ok(cakes);
        }



        // POST api/<KagerController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] Cake value)
        {
            Cake cake =_data.Add(value);
            return Created($"api/kager/{cake.Id}", cake);
        }

        // PUT api/<KagerController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Cake value)
        {
            try
            {
                Cake cake = _data.Edit(id, value);
                return Ok(cake);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound();
            }
        }

        // DELETE api/<KagerController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                Cake cake = _data.Delete(id);
                return Ok(cake);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound();
            }

        }
    }
}

