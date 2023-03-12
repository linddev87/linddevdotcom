using Core.Application.Interfaces;
using Core.Application.Models;
using Core.Application.Services;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class JournalController : ControllerBase {
    private readonly IJournalService _journalService;

    public JournalController(IJournalService journalService)
    {
        _journalService = journalService;
    }

    [HttpGet]
    public async Task<IActionResult> List(){
        try{
            var res = await _journalService.List();
            return res != null ? Ok(res) : BadRequest();    
        }
        catch(Exception e){
            return Problem();
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id){
        try{
            var res = await _journalService.GetById(id);
            return res != null ? Ok(res) : BadRequest();    
        }
        catch(Exception e){
            return Problem();
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(int id){
        try{
            var res = await _journalService.DeleteById(id);
            return res == true ? Ok(res) : BadRequest();
        }
        catch(Exception e){
            return Problem();
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateById(int id, UpsertJournalEntryRequest req){
        try{
            var res = await _journalService.Update(id, req);
            return res != null ? Ok(res) : BadRequest();
        }
        catch(Exception e){
            return Problem();
        }        
    }

    [HttpPost]
    public async Task<IActionResult> Create(UpsertJournalEntryRequest req){
        try{
            var res = await _journalService.Create(req);
            return res != null ? Ok(res) : BadRequest();
        }
        catch(Exception e){
            return Problem();
        }
    }
}