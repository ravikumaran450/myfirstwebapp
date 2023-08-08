using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYWebApplication.DataAccess;
using MYWebApplication.Models;
using MYWebApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYWebApplication.Controllers
{
    public class MyEmployeeController : Controller
    {
       

        // GET: MyEmployeeController
      
        public ActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        
        }

        [HttpPost]
        public ActionResult Login(LoginUser u)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {

                if (ModelState.IsValid)
                {
                    ActionClass ac = new ActionClass();
                    LoginUser user=ac.Login(u);
                    if (user != null)
                    {
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetString("Role", user.Role);
                        if (user.Role.Equals("Project Manager"))
                        {
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            return RedirectToAction("List");
                        }
                    }
                    
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Signup()
        {
              return View();
                      
        }
                

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginUser loginUser)
        {
            ActionClass obj = new ActionClass();
            obj.RegEmployee(loginUser);

            return RedirectToAction("Login");
        }


        [Authentication]
        public ActionResult List()
        {
            ActionClass obj = new ActionClass();
            var Emplyee = obj.GetAllEmployee();

            return View(Emplyee);
        }

        // GET: MyEmployeeController/Details/5
        [Authentication]
        public ActionResult Details(int empid)
        {
            ActionClass obj = new ActionClass();
            var details = obj.GetAllEmployeeById(empid);
            return View(details);
        }

        // GET: MyEmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyEmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp)
        {
            try
            {
                ActionClass obj = new ActionClass();
                obj.AddEmployee(emp);
                return RedirectToAction(nameof(List));
            }
            catch(Exception e)
            {
                return View(e);
            }
        }

        // GET: MyEmployeeController/Edit/5
        [Authentication]
        public ActionResult Edit(int empid)
        {
            ActionClass obj = new ActionClass();
            var empDetails = obj.GetAllEmployeeById(empid);

            return View(empDetails);
        }

        // POST: MyEmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public ActionResult Edit( int empid,Employee employee)
        {

                try
                {
                    ActionClass obj = new ActionClass();
                    obj.UpdateEmployee(empid,employee);

                    return RedirectToAction(nameof(List));
                }
                catch (Exception e)
                {
                    return View(e);
                }
            }
      


        // GET: MyEmployeeController/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public ActionResult Delete(int empid)
        {
            //    Employee emp = ActionClass.GetEmpData(companyName);
            ActionClass obj = new ActionClass();
            var empDetails = obj.GetAllEmployeeById(empid);

            return View(empDetails);
        }

        // POST: MyEmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public ActionResult Delete(int empid,Employee employee)
        {
            try
            {
                ActionClass obj = new ActionClass();
                obj.DeleteEmployee(empid,employee);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("Login");
        }


    }
}
