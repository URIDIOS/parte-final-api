﻿using CREAR_API.Data.Excepcions;
using CREAR_API.Data.Services;
using CREAR_API.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CREAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
				var newPublisher = _publishersService.AddPublisher(publisher);
				return Created(nameof(AddPublisher), newPublisher);
			}
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Nombre de la editoria:{ex.PublisherName}");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

         [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publishersService.GetPublisherByID(id);
            if (_response == null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }           
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {   
				_publishersService.DeletePublisherById(id);
				return Ok();
			}
          
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
          
		}




    }
}
