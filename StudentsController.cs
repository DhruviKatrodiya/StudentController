using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEntityFrameAppCS.Models;

namespace MVCEntityFrameAppCS.Controllers
{
    public class StudentsController : Controller
    {
        private StudentDBEntities1 db = new StudentDBEntities1();

        // GET: Students

        public ActionResult Index(string Sorting_Order, string Search_Data)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "StudentName" : "";
            ViewBag.SortingDate = Sorting_Order == "Birth Date" ? "BirthDate" : "Date";
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "City" : "";
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Gender" : "";
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "PhoneNumber" : "";
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Grade" : "";
           
            var students = from stud in db.Students select stud;

            switch(Sorting_Order)
            {
                case "StudentName":
                    students = students.OrderByDescending(stud => stud.StudentName);
                    break;

                case "Birth Date":
                    students = students.OrderBy(stud => stud.BirthDate);
                    break;

                case "BirthDate":
                    students = students.OrderByDescending(stud => stud.BirthDate);
                    break;

                case "City":
                    students = students.OrderByDescending(stud => stud.City);
                    break;

                case "Gender":
                    students = students.OrderByDescending(stud => stud.Gender);
                    break;

                case "PhoneNumber":
                    students = students.OrderByDescending(stud => stud.PhoneNumber);
                    break;

                case "Grade":
                    students = students.OrderByDescending(stud => stud.Grade);
                    break;

                default:
                    students = students.OrderBy(stud => stud.StudentName);
                    break;
            }

            return View(students.ToList());
            //if a user choose the radio button option as Subject  
            //if (option == "StudentId")
            //{
            //    int value;
            //    if (search == null)
            //    {
            //        value = 0;
            //    }
            //    else
            //    {
            //        value = Convert.ToInt32(search);
            //    }
            //    var result = db.Students.Where(s => s.StudentId.ToString().
            //    Contains(value.ToString()) || value == 0);
            //    return View(result.ToList());
            //}
            //else if (option == "StudentName")
            //{
            //    var result = db.Students.Where(s => s.StudentName.Contains(search) || search == null);
            //    return View(result.ToList());
            //}
            //else if (option == "City")
            //{
            //    var result = db.Students.Where(s => s.City.Contains(search) || search == null);
            //    return View(result.ToList());
            //}
            //else if (option == "Gender")
            //{
            //    var result = db.Students.Where(s => s.Gender.Equals(search) || search == null);
            //    return View(result.ToList());
            //}
            //else if (option == "PhoneNumber")
            //{
            //    var result = db.Students.Where(s => s.PhoneNumber.Contains(search) || search == null);
            //    return View(result.ToList());
            //}
            //else if (option == "Grade")
            //{
            //    var result = db.Students.Where(s => s.Grade.Contains(search) || search == null);
            //    return View(result.ToList());
            //}




        }



        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,CourseId,StudentName,BirthDate,City,Gender,PhoneNumber,Grade")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,CourseId,StudentName,BirthDate,City,Gender,PhoneNumber,Grade")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SearchList()
        {
            List<CourseInStudent> studentList = (List<CourseInStudent>)TempData["StudList"];
            return View(studentList);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "StudentId,CourseId,StudentName,BirthDate,City,Gender,PhoneNumber,Grade")] Student student)
        {
            var searchList = (from s in db.Students
                              join c in db.Courses on s.CourseId equals c.CourseId
                              where s.CourseId == student.CourseId
                              select new CourseInStudent
                              {
                                  StudentId = s.StudentId,
                                  StudentName = s.StudentName,
                                  Coursename = c.CourseName
                              }).ToList<CourseInStudent>();
            TempData["StudList"] = searchList;
            return RedirectToAction("SearchList");
        }
    }
}
