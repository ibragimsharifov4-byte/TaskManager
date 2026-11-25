using Application.Responses;
using Application.DTOs.ProjectDtos;
namespace Application.Interfaces;
public interface IProjectService { Task<ApiResponse<List<Dto>>> GetAll();
 Task<ApiResponse<Dto?>> GetById(int id);
 Task<ApiResponse<bool>> Add(CreateDto dto);
 Task<ApiResponse<bool>> Update(int id, UpdateDto dto);
 Task<ApiResponse<bool>> Delete(int id); Task<ApiResponse<List<Dto>>> GetProjectsWithMostTasks(); }