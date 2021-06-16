using Application.CQRS.Commands;
using Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IMediator mediator;
        public StudentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet, Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new GetAllStudentQuery()));
        }
       
        [HttpGet, Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetStudentByIdQuery { Id = id }));
        }

        [HttpDelete, Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await mediator.Send(new DeleteStudentByIdCommand { Id = id }));
        }

        [HttpPut, Route("update")]
        public async Task<IActionResult> Update(int id, UpdateStudentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await mediator.Send(command));
        }
    }
}
