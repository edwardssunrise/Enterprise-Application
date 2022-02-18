using EnterpriseArchitecture.DTO.EEntity;

namespace EnterpriseArchitecture.Services.Abstract
{
    /// <summary>
    /// Business logic is implemented in the Service Layer, also called the Business Logic Layer.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Update user information in database.
        /// </summary>
        /// <param name="user"></param>
        void Update(EUserDTO user);

        /// <summary>
        /// Fetch the user image from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        byte[] GetUserImage(int id);

        /// <summary>
        /// Search for user in database and fetch user data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EUserDTO Find(int id);

        /// <summary>
        /// Fetch user data from database using username and user password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        EUserDTO GetUserByUserName(string userName, string password);
    }
}