using Application.Responses;
using Application.DTOs.TaskAssignmentDtos;
namespace Application.Interfaces;
public interface ITaskAssignmentService { Task<ApiResponse<List<Dto>>> GetAll();
 Task<ApiResponse<Dto?>> GetById(int id);
 Task<ApiResponse<bool>> Add(CreateDto dto);
 Task<ApiResponse<bool>> Update(int id, UpdateDto dto);
 Task<ApiResponse<bool>> Delete(int id); Task<ApiResponse<List<Dto>>> GetAssignmentsByUser(int id); Task<ApiResponse<List<Dto>>> GetAssignmentsByTask(int id); Task<ApiResponse<bool>> AssignTask(CreateDto dto); }