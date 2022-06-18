using Efa.Feedback.Domain.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Efa.Feedback.Api.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeedbackController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/{idUser}")]
    public async Task<IActionResult> FeedbackRecebido(int idUser)
    {
        await _mediator.Send(new FeedbackRecebidoRequest() { IdUser = idUser}); 
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> EnviarFeedback(EnviarFeedbackRequest request)
    {
        await _mediator.Send(request);
        return Accepted();
    }


}
