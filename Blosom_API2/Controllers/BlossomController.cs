using AutoMapper;

using Blosom_API2.Models;
using Blosom_API2.Models.Dto;
using Blosom_API2.Repository.IRepository;
using Blossom_API.Models;
using Blossom_API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Blossom_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberBlossomController : ControllerBase
    {
        private readonly ILogger<NumberBlossomController> _logger;
        private readonly IBlossomRepository _blossomRepo;
        private readonly INumberBlossomRepository _numberRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumberBlossomController(ILogger<NumberBlossomController> logger,IBlossomRepository blossomRepo, 
                                                                               INumberBlossomRepository numberRepo, IMapper mapper ) 
        {
            _logger = logger;
            _blossomRepo = blossomRepo;
            _numberRepo = numberRepo;
            _mapper = mapper;
            _response = new ();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumberBlossom()
        {
            try
            {
                _logger.LogInformation("See the number of the products");

                IEnumerable<NumberBlossom> numberblossomList = await _numberRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<NumberBlossomDto>>(numberblossomList);
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

        [HttpGet("{id:int}", Name = "GetBlossomProduct")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumberBlossom(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("error when bringing the number product in with the id" + id);
                    _response.statusCode=HttpStatusCode.BadRequest;
                    _response.IsExitoso=false;
                    return BadRequest(_response);
                }
                //var blossom = BlossomStore.blossomList.FirstOrDefault(v => v.Id == id);
                var numberBlossom = await _numberRepo.Get(v => v.BlossomNo == id);

                if (numberBlossom == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso=false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<NumberBlossomDto>(numberBlossom);
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
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> PostNumberProduct([FromBody] NumberBlossomCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numberRepo.Get(v => v.BlossomNo == createDto.BlossomNo) != null)
                {
                    ModelState.AddModelError("NumberExist", "The product with that number already exists");
                    return BadRequest(ModelState);
                }

                if (await _blossomRepo.Get(v => v.Id == createDto.BlossomId) == null) 
                {
                    ModelState.AddModelError("Unidentified key", "Product Id does not exist!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                NumberBlossom model = _mapper.Map<NumberBlossom>(createDto);

                model.DateCreated = DateTime.Now;
                model.DateUpdate = DateTime.Now;
                await _numberRepo.Post(model);
                _response.Result = model;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumberBlossomProduct", new { id = model.BlossomNo }, _response);
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteNumberProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numberBlossom = await _numberRepo.Get(v => v.BlossomNo == id);
                if (numberBlossom == null)
                {
                    _response.IsExitoso=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numberRepo.Delete(numberBlossom);

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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumberProduct(int id, [FromBody] NumberBlossomUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.BlossomNo)
            {
                _response.IsExitoso=!false;
                _response.statusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _blossomRepo.Get(v => v.Id ==updateDto.BlossomId) == null)
            {
                ModelState.AddModelError("Unidentified key", "Product Id does not exist!");
                return BadRequest(ModelState);
            }
            NumberBlossom model = _mapper.Map<NumberBlossom>(updateDto);
            
            await _numberRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;
            
            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialProduct(int id, JsonPatchDocument <BlossomUpdateDto> patchDto)
        {
            if (patchDto == null || id ==0)
            {
                return BadRequest();
            }
            var blossom = await _blossomRepo.Get(v => v.Id == id, tracked:false);

            BlossomUpdateDto blossomDto = _mapper.Map<BlossomUpdateDto>(blossom);

            if (blossom == null) return BadRequest();

            patchDto.ApplyTo(blossomDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Blossom model = _mapper.Map<Blossom>(blossomDto);
            
            await _blossomRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            
            return Ok(_response);

        }
    }
}
