using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using WorkflowCore.Interface;
using WorkflowCoreWebsite.Models;
using WorkflowCoreWebsite.Workflows;

namespace WorkflowCoreWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkflowHost workflowHost;

        public HomeController(IWorkflowHost workflowHost)
        {
            this.workflowHost = workflowHost;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                workflowHost.StartWorkflow(nameof(ContactFormWorkflow), model);

                TempData["Message"] = @"Thanks for reaching out! The ContactFormWorkflow workflow will drop an email in C:\Development\SmtpPickupDirectory every 30 seconds until you click the link in one of them or stop/restart the web application.";
                return RedirectToAction(nameof(Contact));
            }

            return View(model);
        }

        public IActionResult Ack(Guid id)
        {
            // You could of course do a lot more here like ask for more information
            // and publish that data as part of the event.

            workflowHost.PublishEvent("ContactFormAcknowledged", id.ToString(), null);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
