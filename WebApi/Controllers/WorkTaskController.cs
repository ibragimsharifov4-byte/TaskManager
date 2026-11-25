using Application.Interfaces;
using Application.Responses;
using Application.DTOs.WorkTaskDtos;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[ApiController]
[Route("api/tasks")]
public class WorkTaskController(IWorkTaskService service) : ControllerBase
{
 private readonly IWorkTaskService _service=service;
 [HttpGet] public Task<ApiResponse<List<Dto>>> GetAll()=>_service.GetAll();
 [HttpGet("{id:int}")] public Task<ApiResponse<Dto?>> GetById(int id)=>_service.GetById(id);
 [HttpPost] public Task<ApiResponse<bool>> Add(CreateDto dto)=>_service.Add(dto);
 [HttpPut("{id:int}")] public Task<ApiResponse<bool>> Update(int id,UpdateDto dto)=>_service.Update(id,dto);
 [HttpDelete("{id:int}")] public Task<ApiResponse<bool>> Delete(int id)=>_service.Delete(id);
 [HttpGet("project/{id:int}")] public Task<ApiResponse<List<Dto>>> ByProject(int id)=>_service.GetTasksByProject(id);
 [HttpGet("user/{id:int}")] public Task<ApiResponse<List<Dto>>> ByUser(int id)=>_service.GetTasksByUser(id);
 [HttpGet("due-soon/{days:int}")] public Task<ApiResponse<List<Dto>>> DueSoon(int days)=>_service.GetTasksDueSoon(days);
}