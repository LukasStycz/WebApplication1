using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApplication1.Application.Models;
using WebApplication1.Application.Constants;
using WebApplication1.Data.Context;
using WebApplication1.Data.Logic;
using WebApplication1.Domain.Logic;
using WebApplication1.Domain.Models;
using WebApplication1.Mappings;

// Initial application setup 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseInMemoryDatabase("ToDoDatabase"));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ToDoRepository>();
builder.Services.AddScoped<ToDoUtilities>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
     var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
     dbContext.Seed();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Post

app.MapPost("/ToDo/Create", async (ToDoRepository repository, ToDoUtilities utilities, CreateToDoDto createToDoDto, IMapper mapper) =>
{
    try
    {
        var toDoValidationResults = utilities.ValidateToDoTask(createToDoDto);
        if (toDoValidationResults.Count != 0)
        {
            return Results.BadRequest(toDoValidationResults);
        } 
        else 
        {
            var toDo = mapper.Map<ToDo>(createToDoDto);
            var insertedToDo = await repository.InsertToDoAsync(toDo);
            return Results.Ok(insertedToDo);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});

#endregion




#region Put

app.MapPut("/ToDo/Update", async (ToDoRepository repository, ToDoUtilities utilities, UpdateTodoDto updateToDoDto, IMapper mapper) =>
{
    try
    {
        var toDoValidationResults = utilities.ValidateToDoTask(updateToDoDto);
        if (toDoValidationResults.Count != 0)
        {
            return Results.BadRequest(toDoValidationResults);
        } 
        else 
        {
            var toDo = mapper.Map<ToDo>(updateToDoDto);
            var updatedToDo = await repository.UpdateToDoAsync(toDo);
            if (updatedToDo != null)
            {
                return Results.Ok(updatedToDo);
            }
            else
            {
                return Results.BadRequest(ToDoConstantValues.NoSuchTask);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});


app.MapPut("/ToDo/Update/Percentage", async (ToDoRepository repository, ToDoUtilities utilities, UpdateTodoDto updateToDoDto, IMapper mapper) =>
{
    try
    {
        var toDoValidationResults = utilities.ValidateToDoTask(updateToDoDto);
        if (toDoValidationResults.Count != 0)
        {
            return Results.BadRequest(toDoValidationResults);
        } 
        else 
        {
            var toDo = mapper.Map<ToDo>(updateToDoDto);
            var updatedToDo = await repository.UpdateToDoPrecentageAsync(toDo.Id,toDo.CompletionPercentage);
            if (updatedToDo != null)
            {
                return Results.Ok(updatedToDo);
            }
            else
            {
                return Results.BadRequest(ToDoConstantValues.NoSuchTask);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});


app.MapPut("/ToDo/Update/Done", async (ToDoRepository repository, ToDoUtilities utilities, UpdateTodoDto updateToDoDto, IMapper mapper) =>
{
    try
    {
        var toDoValidationResults = utilities.ValidateToDoTask(updateToDoDto);
        if (toDoValidationResults.Count != 0)
        {
            return Results.BadRequest(toDoValidationResults);
        } 
        else 
        {
            var toDo = mapper.Map<ToDo>(updateToDoDto);
            var updatedToDo = await repository.UpdateToDoPrecentageAsync(toDo.Id, ToDoConstantValues.TaskDone);
            if (updatedToDo != null)
            {
                return Results.Ok(updatedToDo);
            }
            else
            {
                return Results.BadRequest(ToDoConstantValues.NoSuchTask);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});

#endregion




#region Get

app.MapGet("/ToDo/Get", async (ToDoRepository repository) =>
{
    try
    {
        var toDos = await repository.GetAllToDosAsync();
        return Results.Ok(toDos.Count == 0 ? ToDoConstantValues.NoAvailbleTask : toDos);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});


app.MapGet("/ToDo/Get/{id:int}", async (int id, ToDoRepository repository) =>
{
    try
    {
        var toDo = await repository.GetToDoByIdAsync(id);
        return Results.Ok(toDo == null ? ToDoConstantValues.NoSuchTask : toDo);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});


app.MapGet("/ToDo/Get/Title/{title}", async (string title, ToDoRepository repository) =>
{
    try
    {
        var toDos = await repository.GetToDoByTitleAsync(title);
        return Results.Ok(toDos.Count == 0 ? ToDoConstantValues.NoAvailbleTask : toDos);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});


app.MapGet("/ToDo/Get/Date/OneDay/{date}", async (string date, ToDoRepository repository, ToDoUtilities utilities) =>
{
    try
    {
        var parsedDate = utilities.ParseToDoExpiry(date);
        if (parsedDate == null)
        {
            return Results.BadRequest(ToDoConstantValues.WrongDate);
        }
        else
        {
            var toDos = await repository.GetToDoFromOneDayAsync(parsedDate);
            return Results.Ok(toDos.Count == 0 ? ToDoConstantValues.NoAvailbleTask : toDos);
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});


app.MapGet("/ToDo/Get/Date/Week/{date}", async (string date, ToDoRepository repository, ToDoUtilities utilities) =>
{
    try
    {
        var parsedDate = utilities.ParseToDoExpiry(date);
        if (parsedDate == null)
        {
            return Results.BadRequest(ToDoConstantValues.WrongDate);
        }
        else
        {
            var weekLimits = utilities.GetStartandEndOfWeek(parsedDate.Value);
            var toDos = await repository.GetToDoFromWeekAsync(weekLimits);
            return Results.Ok(toDos.Count == 0 ? ToDoConstantValues.NoAvailbleTask : toDos);
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});

#endregion




#region Delete

app.MapDelete("/ToDo/Delete/{id:int}", async (int id, ToDoRepository repository) =>
{
    try
    {
        var removal = await repository.DeleteToDoByIdAsync(id);
         return removal ? Results.Ok(ToDoConstantValues.TaskRemoval) : Results.BadRequest(ToDoConstantValues.NoSuchTask);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ToDoConstantValues.UnexpectedError, statusCode: 500);
    }
});

#endregion




app.Run();
