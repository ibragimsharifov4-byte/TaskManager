using Application.Interfaces;
using Application.Responses;
using Application.DTOs.TaskAssignmentDtos;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[ApiController]
[Route("api/assignments")]
public class TaskAssignmentController(ITaskAssignmentService service) : ControllerBase
{
 private readonly ITaskAssignmentService _service=service;
 [HttpGet] public Task<ApiResponse<List<Dto>>> GetAll()=>_service.GetAll();
 [HttpGet("{id:int}")] public Task<ApiResponse<Dto?>> GetById(int id)=>_service.GetById(id);
 [HttpPost] public Task<ApiResponse<bool>> Add(CreateDto dto)=>_service.Add(dto);
 [HttpPut("{id:int}")] public Task<ApiResponse<bool>> Update(int id,UpdateDto dto)=>_service.Update(id,dto);
 [HttpDelete("{id:int}")] public Task<ApiResponse<bool>> Delete(int id)=>_service.Delete(id);
 [HttpPost("assign")] public Task<ApiResponse<bool>> Assign(CreateDto dto)=>_service.AssignTask(dto);
 [HttpGet("user/{id:int}")] public Task<ApiResponse<List<Dto>>> ByUser(int id)=>_service.GetAssignmentsByUser(id);
 [HttpGet("task/{id:int}")] public Task<ApiResponse<List<Dto>>> ByTask(int id)=>_service.GetAssignmentsByTask(id);
}