﻿using AutoMapper;
using Blossom_Utility;
using Blossom_Web.Models.Dto;
using Blossom_Web.Models.Models;
using Blossom_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Blossom_Web.Controllers
{
    public class BlossomController : Controller
    {
        private readonly IBlossomService _blossomService;
        private readonly IMapper _mapper;
        public BlossomController(IBlossomService blossomService, IMapper mapper)
        {
            _blossomService = blossomService;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult>IndexBlossom()
        {
            List<BlossomDto> blossomList = new();

            var response = await _blossomService.GetAll<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if(response != null && response.IsExitoso) {
                blossomList = JsonConvert.DeserializeObject<List<BlossomDto>>(Convert.ToString(response.Result));
            }
            return View(blossomList);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PostBlossomProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>PostBlossomProduct(BlossomCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var response =await _blossomService.Ceate<APIResponse>(model, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null && response.IsExitoso) {
                    TempData["successful"] = "Product created successfully";
                    return RedirectToAction(nameof(IndexBlossom));
                }
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBlossomProduct(int blossomId)
        {
            var response = await _blossomService.Get<APIResponse>(blossomId, HttpContext.Session.GetString(DS.SessionToken));
            {
                if (response != null && response.IsExitoso)
                {
                   
                    BlossomDto modelo = JsonConvert.DeserializeObject<BlossomDto>(Convert.ToString(response.Result));
                    return View(_mapper.Map<BlossomUpdateDto>(modelo));
                }
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> UpdateBlossomProduct(BlossomUpdateDto model)
         {
                if (ModelState.IsValid)
                {
                    var response = await _blossomService.Update<APIResponse>(model, HttpContext.Session.GetString(DS.SessionToken));
                    if (response != null && response.IsExitoso)

                    {
                        TempData["successful"] = "Product update successfully";
                        return RedirectToAction(nameof(IndexBlossom));
                    }
                }
                return View(model);
         }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBlossomProduct(int blossomId)
        {
            var response = await _blossomService.Get<APIResponse>(blossomId, HttpContext.Session.GetString(DS.SessionToken));
            {
                if (response != null && response.IsExitoso)
                {
                    BlossomDto modelo = JsonConvert.DeserializeObject<BlossomDto>(Convert.ToString(response.Result));
                    return View(modelo);
                }
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteBlossomProduct(BlossomDto model)
        {          
            
                var response = await _blossomService.Delete<APIResponse>(model.Id, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null && response.IsExitoso)
                {
                TempData["successful"] = "Product delete successfully";
                return RedirectToAction(nameof(IndexBlossom));
                }
                TempData["error"] = "Error occurred";

            return View(model);
        }
    }
}
