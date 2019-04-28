using System.Collections.Generic;
using System.Threading.Tasks;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using Library.API.Helpers;
using AutoMapper;
using System;

namespace Library.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private ILibraryRepository _libraryRepository;
        public AuthorsController(ILibraryRepository services)
        {
            _libraryRepository = services;
        }

        [HttpGet()]
        public ActionResult GetAuthors()
        {
            //throw new Exception("Throw exception for testing purpose.");
            var authorFromRepo = _libraryRepository.GetAuthors();
            //var authors = new List<AuthorDto>();
            // foreach (var author in authorFromRepo)
            // {
            //     authors.Add(new AuthorDto(){
            //         Id = author.Id,
            //         Name = $"{author.FirstName} {author.LastName}",
            //         Genre = author.Genre,
            //         Age = author.DateOfBirth.GetCurrentAge()
            //     });
            // }
            var authors = Mapper.Map<IEnumerable<AuthorDto>>(authorFromRepo);
            return Ok(authors);          
        }

        [HttpGet("{id}")]
        public ActionResult GetAuthor(Guid id)
        {
            var authorFromRepo = _libraryRepository.GetAuthor(id);
            if (authorFromRepo == null)
            {
                return NotFound();
            }
            var author = Mapper.Map<AuthorDto>(authorFromRepo);
            return Ok(author);
        }
    }
}