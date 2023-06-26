using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperTest.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Infrastructure.Persistence
{
    public class DeveloperTestContext : DbContext, IDeveloperTestContext
    {
        public DeveloperTestContext() : base() { }

        public DeveloperTestContext(DbContextOptions<DeveloperTestContext>  options) : base(options) { }
    }
}
