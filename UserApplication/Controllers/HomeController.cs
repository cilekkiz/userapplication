using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserApplication.Models;
using UserApplication.ViewModels;

namespace UserApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        db01Entities db = new db01Entities();
        public ActionResult Index(UserModel user)
        {
            List<Records> allRecords = db.Records.Where(k => k.UserId == user.Id).OrderBy(k => k.Name).ToList();
            return View(allRecords);
        }
        public ActionResult newRecord()
        {
            return View();
        }
        [HttpPost]
        public ActionResult newRecord(RecordModel model)
        {
            Records record = new Records();
            record.Name = model.Name;
            record.SurName = model.SurName;
            record.Email = model.Email;
            record.BirthDate = model.BirthDate;
            record.Phone = model.Phone;
            record.Location = model.Location;
            db.Records.Add(record);
            db.SaveChanges();
            ViewBag.sonuc = "Record was successfully added";
            return View();
        }
        public ActionResult recordEdit(int ? Id)
        {
            Records record = db.Records.Where(k => k.Id == Id).SingleOrDefault();
            RecordModel recordModel = new RecordModel();
            recordModel.Id = record.Id;
            recordModel.Name = record.Name;
            recordModel.SurName = record.SurName;
            recordModel.Email = record.Email;
            recordModel.BirthDate = record.BirthDate;
            recordModel.Phone = record.Phone;
            recordModel.Location = record.Location;
            return View(recordModel);
        }
        public ActionResult recordDelete(int? Id)
        {
            Records record = db.Records.Where(k => k.Id == Id).SingleOrDefault();
            db.Records.Remove(record);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult recordEdit(RecordModel records)
        {

            Records record = db.Records.Where(k => k.Id == records.Id).SingleOrDefault();
            record.Name = records.Name;
            record.SurName = records.SurName;
            record.Email = records.Email;
            record.BirthDate = records.BirthDate;
            record.Phone = records.Phone;
            record.Location = records.Location;
            db.SaveChanges();
            ViewBag.sonuc = "Record was successfully updated";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRegisterDetails(Users registerDetails)
        {

            if (ModelState.IsValid)
            {
                using (var databaseContext = new db01Entities())
                {
                    Users user = new Users();
                    user.UserName = registerDetails.UserName;
                    user.Password = GetMD5(registerDetails.Password);
                    user.Name = registerDetails.Name;

                    databaseContext.Users.Add(user);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Details Saved";
                return View("Register");
            }
            else
            {
                return View("Register", registerDetails);
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var isValidUser = IsValidUser(model);

                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        public Users IsValidUser(LoginViewModel model)
        {
            var currentkPasswd = GetMD5(model.Password);
            using (var dataContext = new db01Entities())
            {
                Users user = dataContext.Users.Where(query => query.UserName.Equals(model.Username) && query.Password.Equals(currentkPasswd)).SingleOrDefault();
                if (user == null)
                    return null;
                else
                    return user;
            }
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}