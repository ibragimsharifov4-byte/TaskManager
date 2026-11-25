using Application.Responses;
using Application.DTOs.WorkTaskDtos;
namespace Application.Interfaces;
public interface IWorkTaskService { Task<ApiResponse<List<Dto>>> GetAll();
 Task<ApiResponse<Dto?>> GetById(int id);
 Task<ApiResponse<bool>> Add(CreateDto dto);
 Task<ApiResponse<bool>> Update(int id, UpdateDto dto);
 Task<ApiResponse<bool>> Delete(int id); Task<ApiResponse<List<Dto>>> GetTasksByProject(int id); Task<ApiResponse<List<Dto>>> GetTasksByUser(int id); Task<ApiResponse<List<Dto>>> GetTasksDueSoon(int days); }