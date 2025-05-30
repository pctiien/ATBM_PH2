using System.Collections.Generic;
using ATBM_HTTT_PH2.Models;

namespace ATBM_HTTT_PH2.Services
{
    public interface INotificationService
    {
        List<Notification> GetNotificationsForUser(string username);
    }
}
