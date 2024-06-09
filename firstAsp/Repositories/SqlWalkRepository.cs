using firstAsp.Data;
using firstAsp.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstAsp.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly FirstDbContext firstDbContext;
        public SqlWalkRepository(FirstDbContext firstDbContext) 
        {
            this.firstDbContext = firstDbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await firstDbContext.Walks.AddAsync(walk);
            await firstDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
           
            var existWalk = await firstDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existWalk == null)
            {
                return null;
            }
            firstDbContext.Walks.Remove(existWalk);
            await firstDbContext.SaveChangesAsync();
            return existWalk;
        }

        public async Task<List<Walk>> GetAllAsync( string? filterOn=null, string? filterQuery=null,
            string? sortBy=null,bool?isAscending=true,
            int pageNumber=1,int pageSize=1000)
        {
           
            var walks=firstDbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                
            }
            //sorting
            if (string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if (sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending.GetValueOrDefault()?walks.OrderBy(x => x.Name):walks.OrderByDescending(x=>x.Name);
                   

                }


            }
            //pagination
            var skipResults=(pageNumber-1)* pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            // return await firstDbContext.Walks.Include(d=>d.Difficulty).Include(r=>r.Difficulty).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await firstDbContext.Walks
            .Include(w => w.Difficulty)
            .Include(w => w.Region)
            .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingModel=await firstDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingModel == null)
            {
                return null;
            }
            existingModel.Name= walk.Name;
            existingModel.Description= walk.Description;
            existingModel.WalkImageUrl= walk.WalkImageUrl;
            existingModel.RegionId = walk.RegionId;
            existingModel.DifficultyId = walk.DifficultyId;
            await firstDbContext.SaveChangesAsync();
            return existingModel;
        }

       
    }
}
 