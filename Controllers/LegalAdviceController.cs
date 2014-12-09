using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using PremKaushal.Models;

namespace PremKaushal.Controllers
{
    public class LegalAdviceController : Controller
    {
        //
        // GET: /LegalAdvice/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult iQuestion(FormCollection form)
        {
            PremKaushalEntities PEntity = new PremKaushalEntities();
            string name, subject, question, email, contact;
            name = form["InputName"].Trim().ToString();
            subject = form["InputSubject"].Trim().ToString();
            question = form["InputQuestion"].Trim().ToString();
            email = form["InputEmail"].Trim().ToString();
            contact = form["InputContact"].Trim().ToString();
            try
            {
                PEntity.sp_insertQuestion(name, subject, question, email, contact);
                PEntity.sp_insertLog("Info", "Question Inserted: " + subject + ", " + email);
                string body = null;
                body = "<h2>" + subject + "</h2>";
                body += "<p>" + question + "</p>";
                body += "<hr/><h3>You will receive a mail alert once answer of your question is available.Thank You.</h3>";
                Mail.sendInfo(email,"Your Question is generated", body);
                Mail.sendAlert("Alert: Question asked by"+name+"<"+email+">"+"<"+contact+">",body);
            }
            catch (Exception ex)
            {

                PEntity.sp_insertLog("Error", "Error inserting Question: " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
            }
            
            return View();
        }

        
        public JsonResult getAnswerbyEmail(string email)
        {
            //Email = Request["email"].ToString();
            //Dictionary<Question, Answer> dQA = new Dictionary<Question, Answer>();
            //Question qu = new Question();
            var lstAns = new List<Answer>();
            //List<Question> lstQues = new List<Question>();

            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    var contacts = PEntity.Contacts.Where(c => c.Email == email).ToList();
                    foreach (var contact in contacts)
                    {
                        var questions = PEntity.Questions.Where(q => q.CId == contact.Id).ToList();
                        foreach (var question in questions)
                        {
                            //lstQues.Add(question);
                            var answers = PEntity.Answers.Where(a => a.Qid == question.Id).ToList();
                            if (answers.Any())
                            {
                                foreach (var answer in answers)
                                {
                                    //dQA.Add(question,answer);
                                    //qu = question;
                                    //lstAns.Add(answer);
                                    lstAns.Add(new Answer()
                                    {
                                        Id = answer.Id,
                                        Answer1 = answer.Answer1,
                                        CreatedDate = answer.CreatedDate,
                                        Qid = answer.Qid,
                                        Question =
                                            new Question()
                                            {
                                                Id = question.Id,
                                                Question1 = question.Question1,
                                                Subject = question.Subject,
                                                CreatedDate = question.CreatedDate,
                                                CId = question.CId,
                                                Contact =
                                                    new Contact()
                                                    {
                                                        Id = contact.Id,
                                                        Name = contact.Name,
                                                        ContactNo = contact.ContactNo,
                                                        Email = contact.Email
                                                    }
                                            },
                                    });
                                }
                            }
                            else
                            {
                                lstAns.Add(new Answer()
                                {
                                    Id = 0,
                                    Answer1 = "Your Question is still Pending for Answer.",
                                    CreatedDate = DateTime.Now,
                                    Qid = question.Id,
                                    Question =
                                        new Question()
                                        {
                                            Id = question.Id,
                                            Question1 = question.Question1,
                                            Subject = question.Subject,
                                            CreatedDate = DateTime.Now,
                                            CId = contact.Id,
                                            Contact =
                                                new Contact()
                                                {
                                                    Id = contact.Id,
                                                    Name = contact.Name,
                                                    ContactNo = contact.ContactNo,
                                                    Email = contact.Email
                                                }
                                        },
                                });
                            }
                        }
                    }
                    PEntity.sp_insertLog("Info", "GetAnswerbyEmail: " + lstAns[0].Question.Subject + ", " + email);
                }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error", "GetAnswerbyEmail: "+ email +", " + ex.Message +", " + ex.InnerException +", " + ex.StackTrace);
                }
            }

            //return Json(dQA);
            //string myJsonString;
            //myJsonString = JsonConvert.SerializeObject(lstAns, Formatting.Indented,
            //    new JsonSerializerSettings()
            //    {
            //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //    });
            //return Json(myJsonString);
            return Json(JsonHelper.Serialize(lstAns));
        }
    }
}