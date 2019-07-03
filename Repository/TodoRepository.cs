using System.Collections.Generic;
using System.Linq;
using TaskProjectApi.Models;

namespace TaskProjectApi.Repository
{
    public class TodoRepository : ITodoRepository<TodoItem>
    {
        private readonly TodoContext _todoContext;

        public TodoRepository(TodoContext context)
        {
            _todoContext = context;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todoContext.TodoItems.ToList();
        }

        public TodoItem Get(long id)
        {
            return _todoContext.TodoItems
                .FirstOrDefault(e => e.Id == id);
        }

        public void Add(TodoItem entity)
        {
            _todoContext.TodoItems.Add(entity);
            _todoContext.SaveChanges();
        }

        public void Update(TodoItem dbEntity, TodoItem entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.IsComplete = entity.IsComplete;
            _todoContext.SaveChanges();
        }

        public void Delete(TodoItem entity)
        {
            _todoContext.TodoItems.Remove(entity);
            _todoContext.SaveChanges();
        }
    }
}