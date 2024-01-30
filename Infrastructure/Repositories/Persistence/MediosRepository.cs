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

    public class MediosRepository : RepositoryBase<Medios>, IMediosRepository
    {
        private readonly ApplicationDbContext _context;

        public MediosRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Medios>> ObtenerTodos()
        {
            var medios = await _context.Set<Medios>().ToListAsync();

            return medios;
        }

    }
}
