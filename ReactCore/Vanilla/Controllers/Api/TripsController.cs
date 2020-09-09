using Microsoft.AspNetCore.Mvc;
using ReactCore.Data;
using System;

namespace ReactCore.Data
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private ITripService _service;
        public TripsController(ITripService service)
        {
            this._service = service;
        }

        [HttpPost("AddTrip")]
        public IActionResult AddTrip([FromBody]Trip trip)
        {
            if(trip != null)
            {
                _service.AddTrip(trip);
            }
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult GetAllTrips()
        {
            try{
                var trips = _service.GetAllTrips();
            return Ok(trips);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
       }

       [HttpGet("[action]/{id}")]
        public IActionResult GetTripById(int id)
        {
            try{
                var trip = _service.GetTripById(id);
            return Ok(trip);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
       }

       [HttpPut("[action]/{id}")]
       public IActionResult UpdateTrip(int id, [FromBody]Trip trip)
       {
           try{
               _service.UpdateTrip(id, trip);
           return Ok(trip);
           }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
       }

       [HttpDelete("[action]/{id}")]
       public IActionResult DeleteTrip(int id)
       {
            try{
                _service.DeleteTrip(id);
            return Ok();
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
       }
    }
}