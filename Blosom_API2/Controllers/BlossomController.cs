using AutoMapper;
using Blosom_API2.Data;
using Blosom_API2.Repository.IRepository;
using Blossom_API.Data;
using Blossom_API.Models;
using Blossom_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blossom_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlossomController : ControllerBase
    {
        private readonly ILogger<BlossomController> _logger;
        private readonly IBlossomRepository _blossomRepo;
        private readonly IMapper _mapper;
        public BlossomController(ILogger<BlossomController> logger,IBlossomRepository blossomRepo, IMapper mapper ) 
        {
            _logger = logger;
            _blossomRepo = blossomRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BlossomDto>>> GetBlossom()
        {
            _logger.LogInformation("See all products");

            IEnumerable<Blossom> blossomList = await _blossomRepo.GetAll();
            
            return Ok(_mapper.Map<IEnumerable<BlossomDto>>(blossomList));
        }

        [HttpGet("id", Name = "GetBlossomProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BlossomDto>> GetBlossom(int id)
        {
            if (id == 0)
            {
                _logger.LogError("error when bringing the product in with the id" + id);
                return BadRequest();
            }
            //var blossom = BlossomStore.blossomList.FirstOrDefault(v => v.Id == id);
            var blossom = await _blossomRepo.Get(v => v.Id == id);

            if (blossom == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BlossomDto>(blossom));

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<BlossomDto>> PostProduct([FromBody] BlossomCreateDto createDto)
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
            

            await _blossomRepo.Post(model);
            
            return CreatedAtRoute("GetBlossomProduct", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var blossom = await _blossomRepo.Get(v => v.Id == id);
            if (blossom == null)
            {
                return NotFound();
            }
            _blossomRepo.Delete(blossom);
            

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] BlossomUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.Id)
            {
                return BadRequest();
            }
            
            Blossom model = _mapper.Map<Blossom>(updateDto);
            
            _blossomRepo.Update(model);
            

            return NoContent();
        }

        [HttpPatch("{id:int}")]
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
            
            _blossomRepo.Update(model);
            
            return NoContent();

        }
    }
}
