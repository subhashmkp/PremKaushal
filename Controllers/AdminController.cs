using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PremKaushal.Models;

namespace PremKaushal.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Log()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }

        public ViewResult Inbox()
        {
            return View();
        }

        public ViewResult Question()
        {
            var lstQues = new List<Question>();
            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    var questions = PEntity.Questions.Where(q => q.Status == 0).ToList();
                    foreach (var question in questions)
                    {
                        lstQues.Add(new Question()
                        {
                            Id = question.Id,
                            Subject = question.Subject,
                            CId = question.CId,
                            Question1 = question.Question1,
                            CreatedDate = question.CreatedDate,
                            Status = question.Status
                        });
                    }
                }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error",
                        "Questions: " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
            return View(lstQues);
        }

        [HttpPost]
        public JsonResult getLog()
        {
            var lstLog = new List<Log>();
            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    var logs = PEntity.Logs.OrderByDescending(i => i.Id).ToList();
                    lstLog.AddRange(logs);
                    PEntity.sp_updateLog();
                }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error",
                        "getLog: " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
            return Json(JsonHelper.Serialize(lstLog));
        }

        public JsonResult getCount()
        {
            var dCount = new DCount();
            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    dCount.Contacts = PEntity.Contacts.Count(c => c.Status == 0);
                    dCount.Questions = PEntity.Questions.Count(q => q.Status == 0);
                    dCount.Messages = PEntity.Messages.Count(m => m.Status == 0);
                    dCount.Logs = PEntity.Logs.Count(l => l.Status == 0);
                    dCount.TContacts = PEntity.Contacts.Count();
                    dCount.TQuestions = PEntity.Questions.Count();
                    dCount.TMessages = PEntity.Messages.Count();
                    dCount.TAnswers = PEntity.Answers.Count();
                }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error",
                        "getCount: " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
            return Json(JsonHelper.Serialize(dCount));
        }

        public JsonResult getContacts()
        {
            var lstContact = new List<Contact>();
            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    var contacts = PEntity.Contacts.Where(c => c.Status == 0).ToList();
                    foreach (var contact in contacts)
                    {
                        lstContact.Add(new Contact()
                        {
                            Id = contact.Id,
                            Name = contact.Name,
                            Email = contact.Email,
                            ContactNo = contact.ContactNo,
                            Status = contact.Status

                        });
                    }

                }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error",
                        "getContacts: " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
            return Json(JsonHelper.Serialize(lstContact));
        }

        public JsonResult getAnswers(int cid)
        {
            var lstAns = new List<Answer>();
            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    var questions = PEntity.Questions.Where(c => c.CId == cid).ToList();
                    foreach (var question in questions)
                    {
                        var answers = PEntity.Answers.Where(a => a.Qid == question.Id);
                        if (answers.Any())
                        {
                            foreach (var answer in answers)
                            {
                                lstAns.Add(new Answer()
                                {
                                    Id = answer.Id,
                                    Answer1 = answer.Answer1,
                                    Qid = answer.Qid,
                                    CreatedDate = answer.CreatedDate,
                                    Question =
                                        new Question()
                                        {
                                            Id = question.Id,
                                            Subject = question.Subject,
                                            Question1 = question.Question1,
                                            CId = question.CId,
                                            CreatedDate = question.CreatedDate,
                                            Status = question.Status
                                        }
                                });
                            }
                        }
                        else
                        {
                            lstAns.Add(new Answer()
                            {
                                Id = 0,
                                Answer1 = "Pending",
                                Qid = question.Id,
                                CreatedDate = question.CreatedDate,
                                Question =
                                    new Question()
                                    {
                                        Id = question.Id,
                                        Subject = question.Subject,
                                        Question1 = question.Question1,
                                        CId = question.CId,
                                        CreatedDate = question.CreatedDate,
                                        Status = question.Status
                                    }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error",
                        "getAnswers(int cid): " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
            return Json(JsonHelper.Serialize(lstAns));
        }

        public JsonResult getMessages(int cid)
        {
            var lstMgs = new List<Message>();
            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    var messages = PEntity.Messages.Where(m => m.Cid == cid).ToList();
                    foreach (var message in messages)
                    {
                        lstMgs.Add(new Message()
                        {
                            Id = message.Id,
                            Msg = message.Msg,
                            Status = message.Status,
                            Cid = message.Cid
                        });
                    }
                }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error",
                        "getMessages(int cid): " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
            return Json(JsonHelper.Serialize(lstMgs));
        }

        public JsonResult getAnswersbyQid(int qid)
        {
            var ans = new Answer();
            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    var question = PEntity.Questions.FirstOrDefault(q => q.Id == qid);
                    var answer = PEntity.Answers.FirstOrDefault(a => a.Qid == question.Id);
                    var contact = PEntity.Contacts.FirstOrDefault(c => c.Id == question.CId);
                    if (answer != null)
                    {
                        ans = new Answer()
                        {
                            Id = answer.Id,
                            Answer1 = answer.Answer1,
                            Qid = answer.Qid,
                            CreatedDate = answer.CreatedDate,
                            Question = new Question() { Id = question.Id, Subject = question.Subject, Question1 = question.Question1, CId = question.CId, CreatedDate = question.CreatedDate, Status = question.Status, 
                                Contact = new Contact(){Id = contact.Id, Name = contact.Name, Email = contact.Email, ContactNo = contact.ContactNo, Status = contact.Status} }
                        };
                    }
                    else
                    {
                        ans = new Answer()
                        {
                            Id = 0,
                            Answer1 = "No Answer",
                            Qid = question.Id,
                            CreatedDate = DateTime.Now,
                            Question = new Question()
                            {
                                Id = question.Id,
                                Subject = question.Subject,
                                Question1 = question.Question1,
                                CId = question.CId,
                                CreatedDate = question.CreatedDate,
                                Status = question.Status,
                                Contact = new Contact() { Id = contact.Id, Name = contact.Name, Email = contact.Email, ContactNo = contact.ContactNo, Status = contact.Status }
                            }
                        };
                    }
                    
                 }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error",
                        "getAnswersbyQid(int qid): " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
            return Json(JsonHelper.Serialize(ans));
        }

        public ActionResult insertAnswer(FormCollection form)
        {
            //int r = 0;
            string answer = form["answer"].Trim();
            string cname = form["cname"].Trim();
            string qid = form["qid"].Trim();
            string email = form["email"].Trim();
            using (var pEntity = new PremKaushalEntities())
                {
                    try
                    {
                        pEntity.sp_insertAnswer(answer, Convert.ToInt32(qid));
                        pEntity.sp_insertLog("Info", "Answer inserted for Qid " + qid);
                        string body = "<h1>Hello " + cname+"</h1>";
                        body += "<h2>Prem Kaushal has given the answer of your question</h2>";
                        body += "<h3>Please <a href=\"www.premkaushal.com/LegalAdvice\">Click Here</a> to see your answer.</h3> ";
                        Mail.sendInfo(email,"You question is answered now",body);
                    }
                    catch (Exception ex)
                    {
                        pEntity.sp_insertLog("Error",
                         "insertAnswer(int qid, string answer): " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                    }
                    //return r;
                }
            return RedirectToAction("Question");
        }

        public JsonResult getContact(int cid)
        {
            var con = new Contact();
            using (var PEntity = new PremKaushalEntities())
            {
                try
                {
                    var contact = PEntity.Contacts.FirstOrDefault(c => c.Id == cid);
                    con = new Contact() { Id = contact.Id, Name = contact.Name, Email = contact.Email, ContactNo = contact.ContactNo, Status = contact.Status };
                }
                catch (Exception ex)
                {
                    PEntity.sp_insertLog("Error",
                         "getContact(int cid): " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
                
            }
            return Json(JsonHelper.Serialize(con));
        }
    }
}