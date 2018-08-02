using System.Threading.Tasks;
using System.Collections.Generic;
using DatingApp.API.Models;
using DatingApp.API.Helpers;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        // CRUD Utilities
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();

         // User
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         // Photo
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser (int userId);
         // Like
         Task<Like> GetLike (int userId, int recipientId);
         // Message
         Task<Message> GetMessage (int id);
         Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
         Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
    }
}