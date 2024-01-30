using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Persistence
{
    using Application.Contracts.Repositories;
    using Domain.Entities;
    using Infrastructure.Persistence;
    using Infrastructure.Repositories.Persistence.Common;
    using Microsoft.EntityFrameworkCore;

    public class StateRepository : RepositoryBase<StateEntity>, IStateRepository
    {
        private readonly ApplicationDbContext _context;

        public StateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<StateEntity>> ObtenerTodos()
        {
            var states = await _context.Set<StateEntity>().ToListAsync();
             
            return states;
        }
         
    }
}
