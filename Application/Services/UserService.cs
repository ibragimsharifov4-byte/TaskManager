using System.Net;
using Application.Interfaces;
using Application.Responses;
using Application.DTOs.UserDtos;
using Domain.Interfaces;
using Domain.Models;
namespace Application.Services;
public class UserService(IUserRepository repository) : IUserService
{
 private readonly IUserRepository _repo=repository;
 private static ApiResponse<T> Ok<T>(T data,string message) => new(HttpStatusCode.OK,message,data);
 private static ApiResponse<T> Bad<T>(string message) => new(HttpStatusCode.BadRequest,message,default);
 private static Dto Map(User x) => new() { Id=x.Id, Name=x.Name, Email=x.Email, RegistrationDate=x.RegistrationDate };
 public async Task<ApiResponse<List<Dto>>> GetAll() => Ok((await _repo.GetAll()).Select(Map).ToList(),"List received");
 public async Task<ApiResponse<Dto?>> GetById(int id)
 {
  if(id<=0)return Bad<Dto?>("Id must be greater than zero");
  var x=await _repo.GetById(id); return x==null ? new(HttpStatusCode.NotFound,"Not found",null) : Ok<Dto?>(Map(x),"Found");
 }
 public async Task<ApiResponse<bool>> Add(CreateDto dto)
 {
  if(dto==null)return Bad<bool>("Request body is required");
  var x=new User { Name=dto.Name, Email=dto.Email, RegistrationDate=dto.RegistrationDate };
  return await _repo.Add(x) ? Ok(true,"Created") : new(HttpStatusCode.InternalServerError,"Not created",false);
 }
 public async Task<ApiResponse<bool>> Update(int id,UpdateDto dto)
 {
  if(id<=0)return Bad<bool>("Id must be greater than zero");
  var current=await _repo.GetById(id); if(current==null)return new(HttpStatusCode.NotFound,"Not found",false);
  var x=new User { Id=id, Name=dto.Name, Email=dto.Email, RegistrationDate=dto.RegistrationDate };
  return await _repo.Update(x) ? Ok(true,"Updated") : new(HttpStatusCode.InternalServerError,"Not updated",false);
 }
 public async Task<ApiResponse<bool>> Delete(int id)
 {
  if(id<=0)return Bad<bool>("Id must be greater than zero");
  var result=await _repo.Delete(id); return result ? Ok(true,"Deleted") : new(HttpStatusCode.NotFound,"Not found",false);
 }
 public async Task<ApiResponse<List<Dto>>> GetUsersWithMostTasks() { var data=(await _repo.GetUsersWithMostTasks()).Select(Map).ToList(); return Ok(data,"Users ordered by task count"); }
}
