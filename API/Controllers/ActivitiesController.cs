using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;


public class ActivitiesController : BaseApiController
{
    
    private readonly IMediator _mediator;

    public ActivitiesController(IMediator mediator)
    {
           
       _mediator = mediator;
    }

    // Get all activities
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await _mediator.Send(new List.Query());
    }

    // Get specific activity
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
        //TODO: Get activity
        return Ok();
    }
}
