using StudentWebApplication.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentWebApplication.Controllers
{
    public class StudentController : Controller
    {
        db_StudentEntities dbObj = new db_StudentEntities();
        // GET: Student
        public ActionResult Student(Table_Student obj)
        {
            var model = Get(obj.ID);
            if (model != null)
            {
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(HttpPostedFileBase file, Table_Student model)
        {
            try
            {
                if (model.ID == 0)
                {
                    dbObj.Table_Student.Add(model);
                    dbObj.SaveChanges();
                    TempData["Msg"] = "Record Added Successfully";
                }
                else
                {
                    dbObj.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    dbObj.SaveChanges();
                    TempData["Msg"] = "Record Updated Successfully";
                }
                ModelState.Clear();
                return View("Student");
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Record Addition Failed" + e.Message;
                return RedirectToAction("Student");
            }
        }

        public ActionResult StudentList()
        {
            try
            {
                var res = dbObj.Table_Student.ToList();
                return View(res);
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Record Display Failed" + e.Message;
                return RedirectToAction("Student");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var res = dbObj.Table_Student.Where(x => x.ID == id).First();
                dbObj.Table_Student.Remove(res);
                dbObj.SaveChanges();

                var list = dbObj.Table_Student.ToList();

                return View("StudentList", list);

            }
            catch (Exception e)
            {
                TempData["Msg"] = "Record Deletion Failed" + e.Message;
                return RedirectToAction("Student");
            }

        }


        public Table_Student Get(int id)
        {
            try
            {
                Table_Student res = dbObj.Table_Student.Where(x => x.ID == id).First();
                return res;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

    }
}