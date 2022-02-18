using EntepriseArchitecture.Data.GenericRepository;
using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.Core;
using EnterpriseArchitecture.Core.Constants;
using EnterpriseArchitecture.DTO.EEntity;
using EnterpriseArchitecture.Services.Abstract;
using EnterpriseArchitecture.Utilities.PasswordOperations;
using System.Linq;

namespace EnterpriseArchitecture.Services.Concrete
{
    /// <summary>
    /// Business logic is implemented in the Service Layer, also called the Business Logic Layer.
    /// External Libraries Used: AutoMapper
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;
        private EUserDTO _userDTO;

        public UserService(UnitOfWork uow)
        {
            _uow = uow;
            _userRepository = _uow.GetRepository<User>();
            _userDTO = new EUserDTO();
        }

        public void Update(EUserDTO user)
        {
            var entity = _userRepository.Find(user.ID);

            if (user.WhichUpdate == "UP")
            {
                entity.UserName = user.UserName;
                entity.Password = user.Password;
            }
            if (user.WhichUpdate == "FJ")
            {
                entity.FullName = user.FullName;
                entity.Job = user.Job;
            }
            if (user.WhichUpdate == "I")
            {
                entity.Image = user.Value;
            }

            _userRepository.Update(entity);
        }

        public byte[] GetUserImage(int Id)
        {
            var result = _userRepository.GetAll().Where(p => p.ID == Id).SingleOrDefault();

            return (result == null) ? null : result.Image;
        }

        public EUserDTO Find(int Id)
        {
            var user = (from u in _userRepository.GetAll()
                        where u.ID == Id
                        select new EUserDTO
                        {
                            FullName = u.FullName,
                            ID = u.ID,
                            ImageURL = (u.Image != null) ? "/ImageView/" + u.ID : ConstantTypes.profileImage,
                            Job = u.Job,
                            Password = u.Password,
                            UserName = u.UserName
                        }).SingleOrDefault();
            user.Password = CryptoManager.Base64Decrypt(user.Password);
            return user;
        }

        public EUserDTO GetUserByUserName(string userName, string password)
        {
            var control = (from u in _userRepository.GetAll()
                           where u.UserName == userName && u.Password == password
                           select new EUserDTO
                           {
                               FullName = u.FullName,
                               ID = u.ID,
                               ImageURL = (u.Image != null) ? "/ImageView/" + u.ID : ConstantTypes.profileImage,
                               Job = u.Job,
                           }).SingleOrDefault();
            return control;
        }
    }
}