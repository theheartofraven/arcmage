﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Arcmage.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Arcmage.DAL.Utils
{
    public static class SearchExtensions
    {
        public static T FindByGuid<T>(this DbSet<T> set, Guid guid) where T : ModelBase
        {
            return set?.SingleOrDefault(x => x.Guid == guid);
        }

        public static async Task<T> FindByGuidAsync<T>(this DbSet<T> set, Guid? guid) where T : ModelBase
        {
            if (guid == null) return null;
            if (set == null) return null;
            return await set.SingleOrDefaultAsync(x => x.Guid == guid.Value);
        }
    }
}
