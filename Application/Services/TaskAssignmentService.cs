using System.Net;
using Application.Interfaces;
using Application.Responses;
using Application.DTOs.TaskAssignmentDtos;
using Domain.Interfaces;
using Domain.Models;
namespace Application.Services;
public class TaskAssignmentService(ITaskAssignmentRepository repository) : ITaskAssignmentService
{
 private readonly ITaskAssignmentRepository _repo=repository;
 private static ApiResponse<T> Ok<T>(T data,string message) => new(HttpStatusCode.OK,message,data);
 private static ApiResponse<T> Bad<T>(string message) => new(HttpStatusCode.BadRequest,message,default);
 private static Dto Map(TaskAssignment x) => new() { Id=x.Id, TaskId=x.TaskId, UserId=x.UserId, AssignedDate=x.AssignedDate };
 public async Task<ApiResponse<List<Dto>>> GetAll() => Ok((await _repo.GetAll()).Select(Map).ToList(),"List received");
 public async Task<ApiResponse<Dto?>> GetById(int id)
 {
  if(id<=0)return Bad<Dto?>("Id must be greater than zero");
  var x=await _repo.GetById(id); return x==null ? new(HttpStatusCode.NotFound,"Not found",null) : Ok<Dto?>(Map(x),"Found");
 }
 public async Task<ApiResponse<bool>> Add(CreateDto dto)
 {
  if(dto==null)return Bad<bool>("Request body is required");
  var x=new TaskAssignment { TaskId=dto.TaskId, UserId=dto.UserId, AssignedDate=dto.AssignedDate==default ? DateTime.UtcNow : dto.AssignedDate };
  return await _repo.Add(x) ? Ok(true,"Created") : new(HttpStatusCode.InternalServerError,"Not created",false);
 }
 public async Task<ApiResponse<bool>> Update(int id,UpdateDto dto)
 {
  if(id<=0)return Bad<bool>("Id must be greater than zero");
  var current=await _repo.GetById(id); if(current==null)return new(HttpStatusCode.NotFound,"Not found",false);
  var x=new TaskAssignment { Id=id, TaskId=dto.TaskId, UserId=dto.UserId, AssignedDate=dto.AssignedDate };
  return await _repo.Update(x) ? Ok(true,"Updated") : new(HttpStatusCode.InternalServerError,"Not updated",false);
 }
 public async Task<ApiResponse<bool>> Delete(int id)
 {
  if(id<=0)return Bad<bool>("Id must be greater than zero");
  var result=await _repo.Delete(id); return result ? Ok(true,"Deleted") : new(HttpStatusCode.NotFound,"Not found",false);
 }
 public Task<ApiResponse<bool>> AssignTask(CreateDto dto) => Add(dto);
 public async Task<ApiResponse<List<Dto>>> GetAssignmentsByUser(int id) { if(id<=0)return Bad<List<Dto>>("Id must be greater than zero"); var data=(await _repo.GetAssignmentsByUser(id)).Select(Map).ToList(); return Ok(data,"Assignments by user"); }
 public async Task<ApiResponse<List<Dto>>> GetAssignmentsByTask(int id) { if(id<=0)return Bad<List<Dto>>("Id must be greater than zero"); var data=(await _repo.GetAssignmentsByTask(id)).Select(Map).ToList(); return Ok(data,"Assignments by task"); }
}
