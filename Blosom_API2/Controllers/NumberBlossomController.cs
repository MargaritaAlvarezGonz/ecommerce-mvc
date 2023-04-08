using AutoMapper;
using Blosom_API2.Data;
using Blosom_API2.Models;
using Blosom_API2.Repository.IRepository;
using Blossom_API.Data;
using Blossom_API.Models;
using Blossom_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Blossom_API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BlossomController : ControllerBase
    {
        private readonly ILogger<BlossomController> _logger;
        private readonly IBlossomRepository _blossomRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public BlossomController(ILogger<BlossomController> logger,IBlossomRepository blossomRepo, IMapper mapper ) 
        {
            _logger = logger;
            _blossomRepo = blossomRepo;
            _mapper = mapper;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetBlossom()
        {
            try
            {
                _logger.LogInformation("See all products");

                IEnumerable<Blossom> blossomList = await _blossomRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<BlossomDto>>(blossomList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetNumberBlossomProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBlossom(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("error when bringing the product in with the id" + id);
                    _response.statusCode=HttpStatusCode.BadRequest;
                    _response.IsExitoso=false;
                    return BadRequest(_response);
                }
                //var blossom = BlossomStore.blossomList.FirstOrDefault(v => v.Id == id);
                var blossom = await _blossomRepo.Get(v => v.Id == id);

                if (blossom == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso=false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<BlossomDto>(blossom);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso=false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;         

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> PostProduct([FromBody] BlossomCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _blossomRepo.Get(v => v.Name.ToLower() == createDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("NameExist", "The product with that name already exists");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Blossom model = _mapper.Map<Blossom>(createDto);

                model.DateCreated = DateTime.Now;
                model.DateUpdated = DateTime.Now;
                await _blossomRepo.Post(model);
                _response.Result = model;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetBlossomProduct", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() {ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var blossom = await _blossomRepo.Get(v => v.Id == id);
                if (blossom == null)
                {
                    _response.IsExitoso=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _blossomRepo.Delete(blossom);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                
            }
            return BadRequest(_response);
            
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] BlossomUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.Id)
            {
                _response.IsExitoso=!false;
                _response.statusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            
            Blossom model = _mapper.Map<Blossom>(updateDto);
            
            await _blossomRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;
            
            return Ok(_response);
        }

       
    }
}
