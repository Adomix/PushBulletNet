using System.Collections.Generic;
using System.Threading.Tasks;
using PushBulletNet.PushBullet;
using PushBulletNet.PushBullet.Models;
using PushBulletNet.PushBullet.Models.PostModels;

namespace PushBulletNet
{
    public interface IPushBulletClient
    {
        /// <summary>
        /// Gets user data from PushBullet
        /// </summary>
        /// <returns></returns>
        Task<PushBulletUser> GetUserDataAsync();

        /// <summary>
        /// Gets devices registered to PushBullet
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PushBulletDevice>> GetDevicesAsync();

        /// <summary>
        /// Gets all pushes by the client on PushBullet
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PushBulletPush>> GetPushesAsync();

        /// <summary>
        /// Gets all chats by the client on PushBullet
        /// </summary>
        Task<IEnumerable<PushBulletChat>> GetChatsAsync();

        /// <summary>
        /// Pushes a new request to the PushBullet service
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task PushAsync(string title, string content, string targetDeviceId);

        /// <summary>
        /// Creates a new chat
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task CreateDeviceAsync(NewDeviceModel model);
    }

    public class PushBulletClient : IPushBulletClient
    {
        private readonly string _token;

        private readonly IPushBulletService _pushBulletService;

        public PushBulletClient(string token)
        {
            _pushBulletService = new PushBulletService();
            _token = token;
        }

        /// <inheritdoc />
        public Task<PushBulletUser> GetUserDataAsync()
        {
            return _pushBulletService.GetClientData(_token);
        }

        /// <inheritdoc />
        public Task<IEnumerable<PushBulletDevice>> GetDevicesAsync()
        {
            return _pushBulletService.GetDevices(_token);
        }

        /// <inheritdoc />
        public Task<IEnumerable<PushBulletPush>> GetPushesAsync()
        {
            return _pushBulletService.GetPushes(_token);
        }

        /// <inheritdoc />
        public Task<IEnumerable<PushBulletChat>> GetChatsAsync()
        {
            return _pushBulletService.GetChats(_token);
        }

        /// <inheritdoc />
        public Task PushAsync(string title, string content, string targetDeviceId)
        {
            var request = new NotificationPostModel()
            {
                Title = title,
                Content = content,
                TargetDeviceIdentity = targetDeviceId,
                PushType = "note"
            };
            return _pushBulletService.PushNotification(_token, request);
        }

        /// <inheritdoc />
        public Task CreateDeviceAsync(NewDeviceModel model)
        {
            return _pushBulletService.CreateDevice(_token, model);
        }
    }
}