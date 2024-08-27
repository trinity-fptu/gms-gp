using Quartz;
using System;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using static Domain.Enums.PurchasingTaskEnum;
using Application.IRepos;
using Infrastructure.Repos;
using Application;

namespace WebAPI.Services
{
    public class CronJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;

        public CronJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var purchasingPlans = await _unitOfWork.PurchasingPlanRepo.GetAllAsync();
            var purchasingTasks = await _unitOfWork.PurchasingTaskRepo.GetAllAsync();

            foreach (var plan in purchasingPlans)
            {
                if (plan.EndDate < DateTime.Now && plan.ProcessStatus != ProcessStatus.Finished && plan.ProcessStatus != ProcessStatus.Overdue)
                {
                    plan.ProcessStatus = ProcessStatus.Overdue;
                    _unitOfWork.PurchasingPlanRepo.Update(plan);
                }
            }

            foreach (var task in purchasingTasks)
            {
                if (task.TaskEndDate < DateTime.Now && task.TaskStatus != PurchasingTaskStatus.Finished && task.TaskStatus != PurchasingTaskStatus.Overdue)
                {
                    task.TaskStatus = PurchasingTaskStatus.Overdue;
                    _unitOfWork.PurchasingTaskRepo.Update(task);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
