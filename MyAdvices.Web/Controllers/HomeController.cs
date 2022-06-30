using MyAdvices.BusinessLayer;
using MyAdvices.BusinessLayer.Result;
using MyAdvices.Entities;
using MyAdvices.Entities.Messages;
using MyAdvices.Entities.ValueObject;
using MyAdvices.Web.Models;
using MyAdvices.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyAdvices.Web.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private UserManager userManager = new UserManager();
      private  AdvicesUserManager advicesUserManager = new AdvicesUserManager();

        // GET: Home 

        public ActionResult Index()
        {
            //BusinessLayer.Test test = new BusinessLayer.Test();
            ////test.InserTest();
            ////test.UpdateTest();
            ////test.DeleteTest();
            //test.CommentTest();

            //CategoryController üzerinden gelen view talebi ve model
            //if (TempData["mm"]!=null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}

              return View(noteManager.ListQuaryable().OrderByDescending(x=>x.ModifiedOn).ToList());
            //  return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Category cat = categoryManager.Find(x=>x.Id==id.Value);

            if (cat == null)
            {
                return HttpNotFound();
                //return RedirectToAction("Index","Home");
            }

            return View("Index", cat.Notes.OrderByDescending(x=>x.ModifiedOn).ToList());
        }
        public ActionResult MostLiked()
        {
            

            return View("Index",noteManager.ListQuaryable().OrderByDescending(x => x.LikeCount).ToList());
        }
        public ActionResult MostActiveUser()
        {
            
            
            return View(userManager.GetAllUser().OrderByDescending(x => x.Notes.Count).ToList());
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //yönlendirme
            //session kontrolü
            if (ModelState.IsValid)
            {
                
                BusinessLayerResult<AdvicesUser> res = advicesUserManager.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    if (res.Errors.Find(x => x.Code == ErrorMessagesCode.UserIsNotActive)!=null)
                    {
                        ViewBag.SetLink = "E-Mail Gönder";
                    } 
                    return View(model);
                }

                CurrentSession.Set<AdvicesUser>("login", res.Result); //Session'a kullanıcı bilgisi saklama
                return RedirectToAction("Index"); ///yönlendirme
            }

            
            return View(model);
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
               
               
                BusinessLayerResult<AdvicesUser> res = advicesUserManager.RegisterUser(model);

                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x=>ModelState.AddModelError("",x.Message));
                    return View(model);
                }



                /*
                //try
                //{
                //    aum.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{

                //    ModelState.AddModelError("", ex.Message);
                //}


                //if (user==null)
                //{
                //    return View(model);
                //}
                

                //if (model.Username=="aa")
                //{
                //    ModelState.AddModelError("","Kullanıcı adı kullanılıyor.");
                //}
                //if (model.EMail=="aa@aa.com")
                //{
                //    ModelState.AddModelError("", "E-Mail adresi kullanılıyor.");
                //}

                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count>0)
                //    {
                //        return View(model);
                //    }
                //} */

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl="/Home/Login",
                };
                notifyObj.Items.Add("Lütfen E-Mail adresinize gelen aktivasyon bağlantısına tıklayarak hesabınızı aktif ediniz." +
                    "Hesabınızı aktif etmediğniz durumunda gönderi paylaşamazsınız.");
                return View("Ok",notifyObj);
            }
            return View(model);
        }
        public ActionResult UserActivate(Guid id)
        {
            
           BusinessLayerResult<AdvicesUser> res= advicesUserManager.ActivateUser(id);
            if (res.Errors.Count>0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotiftObj = new OkViewModel()
            {
                Title="Hesap Aktifleştirildi",
                RedirectingUrl="/Home/Login",
               
            };
            okNotiftObj.Items.Add(" Hesabınız aktifleştirildi. Artık gönderi paylaşabilirsiniz.");
            return View("Ok",okNotiftObj);
        }
        public ActionResult ShowProfile()
        {
            
           
            BusinessLayerResult<AdvicesUser> res = advicesUserManager.GetUserById(CurrentSession.User.Id);
            if (res.Errors.Count>0)
            {
                //Kullanıcıyı hata ekranına yönlendir.
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }
        public ActionResult EditProfile()
        {
           
           
            BusinessLayerResult<AdvicesUser> res = advicesUserManager.GetUserById(CurrentSession.User.Id);
            if (res.Errors.Count > 0)
            {
                //Kullanıcıyı hata ekranına yönlendir.
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }
        [HttpPost]
        public ActionResult EditProfile(AdvicesUser model,HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" ||
                ProfileImage.ContentType == "image/jpg" ||
                ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;
                }
                
                BusinessLayerResult<AdvicesUser> res = advicesUserManager.UpdateProfile(model);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Home/EditProfile"
                    };
                    return View("Error", errorNotifyObj);
                }
                CurrentSession.Set<AdvicesUser> ("login",res.Result); //Profil güncellendiği için session güncellendi.
                return RedirectToAction("ShowProfile");
            }
            return View(model);
        }
        public ActionResult DeleteProfile()
        {
           
            
            BusinessLayerResult<AdvicesUser> res = advicesUserManager.RemoveUserById(CurrentSession.User.Id);
            if (res.Errors.Count > 0)
            {
                //Kullanıcıyı hata ekranına yönlendir.
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors,
                    RedirectingUrl="/Home/ShowProfile"
                };
                return View("Error", errorNotifyObj);
            }

            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult TestNotify()
        {
            ErrorViewModel model = new ErrorViewModel()
            {
                Header = "Yönlendirme",
                Title = "Ok Test",
                RedirectingTimeout = 5000,
                Items = new List<ErrorMessageObj>() {
                    new ErrorMessageObj() {Message= "Test başarılı 1" },
                    new ErrorMessageObj(){Message="Test başarılı 2" } 

            }
            };


           return View("Error",model);
        }

    }
}