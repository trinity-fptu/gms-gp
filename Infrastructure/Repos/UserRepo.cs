using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepos;
using Domain.Entities;
using Domain.Entities.UserRole;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<User?> Login(string email, string hashedPassword)
        {
            var user = await _dbSet.FirstOrDefaultAsync(
                x => x.Email == email
                && x.HashedPassword == hashedPassword
                && !x.IsDeleted
                && x.AccountStatus == Domain.Enums.User.UserStatusEnum.Active);
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var userList = await _dbSet
                .Include(x => x.Inspector)
                .Include(x => x.Manager)
                .Include(x => x.PurchasingManager)
                .Include(x => x.PurchasingStaff)
                .Include(x => x.Supplier)
                .Include(x => x.WarehouseStaff)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            return userList;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbSet
                .Include(x => x.Inspector)
                .Include(x => x.Manager)
                .Include(x => x.PurchasingManager)
                .Include(x => x.PurchasingStaff)
                .Include(x => x.Supplier)
                .Include(x => x.WarehouseStaff)
                .FirstOrDefaultAsync(x =>
                    x.Id == id && !x.IsDeleted);
            return user;
        }

        public async Task<User> GetByStaffCodeAsync(string code)
        {
            var user = await _dbSet
                .FirstOrDefaultAsync(x =>
                    x.StaffCode == code && !x.IsDeleted); 
            return user;
        }

        public async Task<User> GetBySupplierId(int supplierId)
        {
            var user = await _dbSet
                .Include(x => x.Supplier)
                .FirstOrDefaultAsync(x =>
                    x.SupplierId == supplierId && !x.IsDeleted);
            return user;
        }

        public async Task<User> GetByManagerId(int managerId)
        {
            var user = await _dbSet
                .Include(x => x.Manager)
                .FirstOrDefaultAsync(x =>
                    x.ManagerId == managerId && !x.IsDeleted);
            return user;
        }

        public async Task<User> GetByPurchasingManagerId(int purchasingManagerId)
        {
            var user = await _dbSet
                .Include(x => x.PurchasingManager)
                .FirstOrDefaultAsync(x =>
                    x.PurchasingManagerId == purchasingManagerId && !x.IsDeleted);
            return user;
        }

        public async Task<User> GetByPurchasingStaffId(int purchasingStaffId)
        {
            var user = await _dbSet
                .Include(x => x.PurchasingStaff)
                .FirstOrDefaultAsync(x =>
                    x.PurchasingStaffId == purchasingStaffId && !x.IsDeleted);
            return user;
        }

        public async Task<User> GetByWarehouseStaffId(int warehouseStaffId)
        {
            var user = await _dbSet
                .Include(x => x.WarehouseStaff)
                .FirstOrDefaultAsync(x =>
                    x.WarehouseStaffId == warehouseStaffId && !x.IsDeleted);
            return user;
        }

        public async Task<User> GetByInspectorId(int inspectorId)
        {
            var user = await _dbSet
                .Include(x => x.Inspector)
                .FirstOrDefaultAsync(x =>
                    x.InspectorId == inspectorId && !x.IsDeleted);
            return user;
        }

        public async Task<List<User>> GetAllByRoleId(int roleId)
        {
            var userList = await _dbSet
                .Include(x => x.Supplier)
                .Where(x =>
                    x.RoleId == roleId && !x.IsDeleted).ToListAsync();
            return userList;
        }
    }
}
