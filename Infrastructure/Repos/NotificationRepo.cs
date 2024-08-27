using Application.IRepos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class NotificationRepo : GenericRepo<Notification>, INotificationRepo
    {
        public NotificationRepo(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Notification>> GetByUserId(int userId)
        {
            var item = await _dbSet.Where(x => 
                    x.UserId == userId 
                    && x.IsDeleted == false)
                .ToListAsync();

            return item;
        }

        public async Task<List<Notification>> GetReadNotificationByUserId(int userId)
        {
            var item = await _dbSet.Where(x =>
                    x.UserId == userId
                    && x.Status == NotificationStatusEnum.Read
                    && x.IsDeleted == false)
                .ToListAsync();

            return item;
        }

        public async Task<List<Notification>> GetUnreadNotificationByUserId(int userId)
        {
            var item = await _dbSet.Where(x =>
                    x.UserId == userId
                    && x.Status == NotificationStatusEnum.Read
                    && x.IsDeleted == false)
                .ToListAsync();

            return item;
        }
    }
}
