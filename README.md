# Task Manager API

REST API для управления проектами, задачами, пользователями и назначениями задач. Структура повторяет учебный проект Booknest: `Domain`, `Application`, `Infrastructure`, `WebApi`; используется Repository + Service + Controller.

## Технологии
- .NET 10 (структура и стиль исходного Booknest-проекта; при необходимости можно переключить TargetFramework на net8.0 и подобрать совместимые версии EF Core/Npgsql)
- Entity Framework Core 10
- PostgreSQL
- ASP.NET Core Controllers, Swagger/OpenAPI
- LINQ, DTO, Data Annotations

## Структура
- `Domain/Models` — Project, WorkTask, User, TaskAssignment и навигационные свойства.
- `Domain/Interfaces` — общий `IRepository<T>`.
- `Application/DTOs` — Create/Update/response DTO для каждой сущности.
- `Application/Interfaces` — контракты сервисов.
- `Application/Services` — бизнес-логика, маппинг DTO и LINQ-запросы.
- `Infrastructure/Data` — DbContext, конфигурация модели и начальное заполнение.
- `Infrastructure/Repositories` — реализация CRUD через EF Core.
- `Infrastructure/Migrations` — миграция схемы.
- `WebApi/Controllers` — REST endpoints; `Middleware` — единая обработка исключений.

## Запуск
1. Установите .NET 10 SDK и PostgreSQL.
2. Создайте PostgreSQL database `TaskManager`.
3. В `WebApi/appsettings.json` замените `CHANGE_ME` на пароль своего PostgreSQL-пользователя. Не публикуйте реальные пароли в GitHub.
4. Из корня проекта выполните:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project WebApi
   ```
5. Swagger: `http://localhost:5000/swagger` или адрес, который покажет консоль.
6. При запуске приложение применяет миграции и добавляет тестовые данные, если таблица проектов пуста.

### Миграции
В проекте есть стартовая миграция. Для следующих изменений модели:
```bash
dotnet ef migrations add MigrationName --project Infrastructure --startup-project WebApi
dotnet ef database update --project Infrastructure --startup-project WebApi
```
Если команда `dotnet ef` не найдена: `dotnet tool install --global dotnet-ef --version 10.0.4`.

## API

### Projects — `/api/projects`
| Method | Route | Назначение |
|---|---|---|
| GET | `/api/projects` | Все проекты |
| GET | `/api/projects/{id}` | Проект по ID |
| POST | `/api/projects` | Создать проект |
| PUT | `/api/projects/{id}` | Обновить проект |
| DELETE | `/api/projects/{id}` | Удалить проект |
| GET | `/api/projects/most-tasks` | Проекты по убыванию количества задач |

### Users — `/api/users`
CRUD: `GET /api/users`, `GET /api/users/{id}`, `POST /api/users`, `PUT /api/users/{id}`, `DELETE /api/users/{id}`.
Дополнительно: `GET /api/users/most-tasks` — пользователи по количеству задач.

### Tasks — `/api/tasks`
CRUD: `GET /api/tasks`, `GET /api/tasks/{id}`, `POST /api/tasks`, `PUT /api/tasks/{id}`, `DELETE /api/tasks/{id}`.
Дополнительно:
- `GET /api/tasks/project/{id}` — задачи проекта.
- `GET /api/tasks/user/{id}` — задачи пользователя.
- `GET /api/tasks/due-soon/{days}` — задачи со сроком от текущего момента до N дней.

### Task assignments — `/api/assignments`
CRUD: `GET /api/assignments`, `GET /api/assignments/{id}`, `POST /api/assignments`, `PUT /api/assignments/{id}`, `DELETE /api/assignments/{id}`.
Дополнительно:
- `POST /api/assignments/assign` — назначить задачу пользователю.
- `GET /api/assignments/user/{id}` — назначения пользователя.
- `GET /api/assignments/task/{id}` — назначения задачи.

## Примеры JSON

Создать проект:
```json
{ "name": "Mobile app", "description": "Build a mobile application", "startDate": "2026-11-25T00:00:00Z", "endDate": null }
```
Создать пользователя:
```json
{ "name": "Alex", "email": "alex2@example.com", "registrationDate": "2026-11-25T12:00:00Z" }
```
Создать задачу:
```json
{ "title": "Create login", "description": "Implement login endpoint", "dueDate": "2026-12-01T12:00:00Z", "projectId": 1, "userId": 1 }
```
Назначение:
```json
{ "taskId": 1, "userId": 2, "assignedDate": "2026-11-25T12:00:00Z" }
```

## Сервисные методы
Каждый сервис содержит `GetAll`, `GetById`, `Add`, `Update`, `Delete`.
- `WorkTaskService`: `GetTasksByProject`, `GetTasksByUser`, `GetTasksDueSoon`.
- `ProjectService`: `GetProjectsWithMostTasks`.
- `UserService`: `GetUsersWithMostTasks`.
- `TaskAssignmentService`: `AssignTask`, `GetAssignmentsByUser`, `GetAssignmentsByTask`.

## Обработка ошибок и целостность
- `[ApiController]` автоматически проверяет DTO с Data Annotations.
- Сервис проверяет положительный ID и возвращает `400`/`404` в объекте `ApiResponse`.
- Middleware логирует необработанные исключения и возвращает безопасное сообщение `500`.
- Внешние ключи обеспечивают целостность связей; email и пара TaskId/UserId уникальны.
- Удаление проекта каскадно удаляет его задачи; пользователь с задачами не удаляется из-за `Restrict`, чтобы не оставлять некорректные ссылки.

## Примечание
`ApiResponse.StatusCode` находится в JSON-ответе, как в исходном Booknest-проекте. Если требуется реальный HTTP status code, контроллеры можно перевести на `ActionResult` и `StatusCode((int)response.StatusCode, response)`.
