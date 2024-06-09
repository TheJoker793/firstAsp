using firstAsp.Data;
using firstAsp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace firstAsp.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly FirstDbContext _firstDbContext;
        
        public SqlRegionRepository(FirstDbContext dbContext )
        {
            this._firstDbContext = dbContext;
          
        }

        public async Task<Region> CreateAsync(Region region)
        {
            //await FirstDbContext.Regions.AddAsync(region);
            await _firstDbContext.regions.AddAsync(region);
            await _firstDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion=await _firstDbContext.regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingRegion==null)
            {
                return null;
            }
            _firstDbContext.regions.Remove(existingRegion);
            await _firstDbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
        return  await _firstDbContext.regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _firstDbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion= await _firstDbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            
            existingRegion.Code=region.Code;
            existingRegion.Name=region.Name;
            existingRegion.RegionImageUrl=region.RegionImageUrl;
            await _firstDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}



