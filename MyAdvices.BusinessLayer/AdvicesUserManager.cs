using MyAdvices.BusinessLayer.Abstract;
using MyAdvices.BusinessLayer.Result;
using MyAdvices.Common.Helpers;
using MyAdvices.DataAccessLayer.EntityFramework;
using MyAdvices.Entities;
using MyAdvices.Entities.Messages;
using MyAdvices.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.BusinessLayer
{
    public class AdvicesUserManager:ManagerBase<AdvicesUser>
    {
       
        public BusinessLayerResult<AdvicesUser> RegisterUser(RegisterViewModel data)
        {
            AdvicesUser user = Find(x => x.Username == data.Username || x.Email == data.EMail);
            BusinessLayerResult<AdvicesUser> res = new BusinessLayerResult<AdvicesUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");

                }

                if (user.Email == data.EMail)
                {
                    res.AddError(ErrorMessagesCode.EmailAlreadyExists, "E-Mail adresi kayıtlı."); ;
                }
            }
            else
            {
                int dbResult = base.Insert(new AdvicesUser()
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    Username = data.Username,
                    Email = data.EMail,
                    ProfileImageFilename = "user.png",
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                });

                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Email == data.EMail && x.Username == data.Username);

                    //TODO : aktivasyon mail'i atılacak..
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.Username};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız.</a>";
                    MailHelper.SendMail(body,res.Result.Email,"MyAdvices Hesap Aktifleştirme");
                }
            }


            return res;

        }
        public BusinessLayerResult<AdvicesUser> LoginUser(LoginViewModel data)
        {
            //Giriş kontrolü
            //kullanıcı altif edilmiş mi

            BusinessLayerResult<AdvicesUser> res = new BusinessLayerResult<AdvicesUser>();
            res.Result =Find(x => x.Username == data.Username && x.Password == data.Password);



            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessagesCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessagesCode.CheckYourEmail, "Lütfen E-Mail adresinizi kontrol ediniz.");
                }


            }
            else
            {
                res.AddError(ErrorMessagesCode.UsernameOrPassWrong, "Kullanıcı adı veya parola yanlış.");
                
            }
            return res;
        }
        public BusinessLayerResult<AdvicesUser> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<AdvicesUser> res = new BusinessLayerResult<AdvicesUser>();
            res.Result = Find(x => x.ActivateGuid==activateId);

            if (res.Result!=null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessagesCode.UserAllreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }
               
                     res.Result.IsActive = true;
                     Update(res.Result);
                
                
            }
            else
            {
                res.AddError(ErrorMessagesCode.ActivateIdDoesNotExist, "Aktifleştirilecek kullanıcı bulunamadı.");
            }
            return res;
        }

        public BusinessLayerResult<AdvicesUser> GetUserById(int id)
        {
            BusinessLayerResult<AdvicesUser> res = new BusinessLayerResult<AdvicesUser>();
            res.Result = Find(x => x.Id == id);
            if (res.Result==null)
            {
                res.AddError(ErrorMessagesCode.UserNotFound, "Kullanıcı Bulunamadı.");
            }
            return res;
            
        }

        public BusinessLayerResult<AdvicesUser> UpdateProfile(AdvicesUser data)
        {
          // AdvicesUser db_user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);
           AdvicesUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
       
            BusinessLayerResult<AdvicesUser> res = new BusinessLayerResult<AdvicesUser>();

            if (db_user != null && db_user.Id!=data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");

                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessagesCode.EmailAlreadyExists, "E-Mail adresi kayıtlı."); ;
                }
                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            if (string.IsNullOrEmpty(data.ProfileImageFilename)==false)
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }
            if (base.Update(res.Result)==0)
            {
                res.AddError(ErrorMessagesCode.ProfileCouldNotUpdated, "PRofil Güncellenemedi.");
            }
            return res;
        }

        public BusinessLayerResult<AdvicesUser> RemoveUserById(int id)
        {
            BusinessLayerResult<AdvicesUser> res = new BusinessLayerResult<AdvicesUser>();
            AdvicesUser user = Find(x => x.Id == id);
            if (user!= null)
            {
                if (Delete(user)==0)
                {
                    res.AddError(ErrorMessagesCode.UserCouldNotRemove, "Kullanıcı Silinemedi.");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessagesCode.UserCouldNotFind, "Kullanıcı Bulunamadı.");
            }
            return res;
        }

        //Method hiding...
        public new BusinessLayerResult<AdvicesUser> Insert(AdvicesUser data)
        {
            AdvicesUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<AdvicesUser> res = new BusinessLayerResult<AdvicesUser>();

            res.Result = data;

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");

                }

                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessagesCode.EmailAlreadyExists, "E-Mail adresi kayıtlı."); ;
                }
            }
            else
            {
                res.Result.ProfileImageFilename = "user.png";
                res.Result.ActivateGuid = Guid.NewGuid();

                if (base.Insert(res.Result)==0)
                {
                    res.AddError(ErrorMessagesCode.UserCouldNotInserted, "Kullanıcı eklenemedi."); ;
                }
            }


            return res;
        }
        public new BusinessLayerResult<AdvicesUser> Update(AdvicesUser data)
        {
            AdvicesUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));

            BusinessLayerResult<AdvicesUser> res = new BusinessLayerResult<AdvicesUser>();
            res.Result = data;

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");

                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessagesCode.EmailAlreadyExists, "E-Mail adresi kayıtlı."); ;
                }
                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;

           
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessagesCode.UserCouldNotUpdated, "Kullanıcı Güncellenemedi.");
            }
            return res;
        }

    }
}
