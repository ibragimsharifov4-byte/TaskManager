using Application.Interfaces;
using Application.Responses;
using Application.DTOs.ProjectDtos;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[ApiController]
[Route("api/projects")]
public class ProjectController(IProjectService service) : ControllerBase
{
 private readonly IProjectService _service=service;
 [HttpGet] public Task<ApiResponse<List<Dto>>> GetAll()=>_service.GetAll();
 [HttpGet("{id:int}")] public Task<ApiResponse<Dto?>> GetById(int id)=>_service.GetById(id);
 [HttpPost] public Task<ApiResponse<bool>> Add(CreateDto dto)=>_service.Add(dto);
 [HttpPut("{id:int}")] public Task<ApiResponse<bool>> Update(int id,UpdateDto dto)=>_service.Update(id,dto);
 [HttpDelete("{id:int}")] public Task<ApiResponse<bool>> Delete(int id)=>_service.Delete(id);
 [HttpGet("most-tasks")] public Task<ApiResponse<List<Dto>>> MostTasks()=>_service.GetProjectsWithMostTasks();
}