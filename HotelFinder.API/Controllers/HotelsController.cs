using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {   
        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService= hotelService;
        }


        /// <summary>
        ///  Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels); // 200 + data

        }

        /// <summary>
        ///  Get Hotel By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")] // api/hotels/gethotelbyid/2
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if(hotel != null)
            {
                return Ok(hotel);// 200 + data
            }

            return NotFound(); // 404
        }

        /// <summary>
        ///  Get Hotel By Name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{name}")] // api/hotels/gethotelbyname/hotelName
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel = await _hotelService.GetHotelByName(name);
            if(hotel != null)
                return Ok(hotel); // 200 + data
            return NotFound(); // 400
        }

        

        /// <summary>
        ///  Create A Hotel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateHotel([FromBody]Hotel hotel)
        {
            #region "[ApiController] attribute önemi"
            // If kontrolü ve model valid değilse BadRequest gönder kodu yazmamıza gerek yok. [ApiController] sayesinde model valid değilse actiona hiç girilmez.

            //if (ModelState.IsValid)
            //{
            //    var createdHotel = _hotelService.CreateHotel(hotel);
            //    return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel); // 201 + data
            //}

            //return BadRequest(ModelState); // 400 + validation errors
            #endregion

            var createdHotel = await _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel); // 201 + data

        }

        /// <summary>
        ///  Update A Hotel
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if ( await _hotelService.GetHotelById(hotel.Id) != null)
                return Ok(await _hotelService.UpdateHotel(hotel)); // 200 + data

            return NotFound();


        }

        /// <summary>
        ///  Delete A Hotel
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {   
            if(_hotelService.GetHotelById(id) != null)
            {
                 await _hotelService.DeleteHotel(id);
                return Ok();
            }

            return NotFound(id);
        }
    } 
}
