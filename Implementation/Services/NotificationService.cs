using Newtonsoft.Json;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Interfaces;
using NIHApp.Implementation.Presentation.RestModels;
using NIHApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Implementation.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEventRepository _eventRepository;
        //-----------------------------------------------------------------------------------------------------------    
        public NotificationService(INotificationRepository notificationRepository, IEventRepository eventRepository)
        {
            _notificationRepository = notificationRepository;
            _eventRepository = eventRepository;
        }

        public NotificationModel CreateNotification(NotificationModel notificationModel)
        {
            var _notification = new Notification
            {
                NotId = notificationModel.NotPersonId,
                NotMessage = notificationModel.NotMessage,
                NotIsSent = notificationModel.NotIsSent,
                NotPersonId = notificationModel.NotPersonId,
                NotEventId = notificationModel.NotEventId,
                NotTimeCreated = DateTime.Now
            };

            using (var transaction = _eventRepository.Session.BeginTransaction())
            {
                _notificationRepository.Save(_notification);
                transaction.Commit();
            }

            return new NotificationModel(_notification);
        }

        //-----------------------------------------------------------------------------------------------------------
        public IList<NotificationModel> GetNotificationsByParentId(long parentId)
        {
            var notifications = _notificationRepository.FindNotificationsByParentId(parentId);
            return notifications.Select(x => new NotificationModel(x)).ToList();
        }

        //-----------------------------------------------------------------------------------------------------------
        public async Task<bool> NotifyAsync(string to, string Title, string Body, EventModel eventModel)
        {
            try
            {
                // Get the server key from FCM console
                //var serverKey = string.Format("key={0}", "Your server key - use app config");
                var serverKey = string.Format("AAAAIMC5mH8:APA91bHUY7o-IypKL3lhFH95kkCflRk10DSloqhLrf5FjuT8Hy8GFUf1cfmyx2BmhKJHlWDGw3O--tCLTjjNBbQ_h50RVkKXGLB1hwvi9aZrCfXjXdDS2j0XAN6CJbR-u2VY9lj_J-l0");
                // Get the sender id from FCM console
                //var senderId = string.Format("id={0}", "Your sender id - use app config");
                var senderId = string.Format("140672342143");
                var data = new
                {
                    to, // Recipient device token
                    notification = new { Title, Body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);
                        var isSent = false;
                
                        if (result.IsSuccessStatusCode)
                        {
                            isSent = true;
                            return true;
                        }
                        else
                        {
                            isSent = false;
                        }

                        var notification = new NotificationModel
                        {
                            NotMessage = Body,
                            NotIsSent = isSent,
                            NotPersonId = eventModel.EvtParentId,
                            NotTimeCreated = DateTime.Now,
                            NotEventId = eventModel.EvtID
                        };

                        CreateNotification(notification);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return false;
        }
        //-----------------------------------------------------------------------------------------------------------
    }
}
