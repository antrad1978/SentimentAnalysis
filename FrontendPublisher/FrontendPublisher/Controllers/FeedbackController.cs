using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using FrontendPublisher.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontendPublisher.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Name,Email,Text")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                await FeedbackSent(feedback);

                return RedirectToAction("/Home/Index");
            }
            return View(feedback);
        }

        private static async Task FeedbackSent(Feedback feedback)
        {
            feedback.ID = Guid.NewGuid();
            feedback.FeedbackDate = DateTime.Now;

            string payload = JsonConvert.SerializeObject(feedback); ;
            string topic = "DataAnalysis";
            var config = new Dictionary<string, object> { { "bootstrap.servers", "127.0.0.1" } };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                var deliveryReport = producer.ProduceAsync(topic, null, payload);
                await deliveryReport.ContinueWith(task =>
                {
                    Console.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
                });
                // Tasks are not waited on synchronously (ContinueWith is not synchronous),
                // so it's possible they may still in progress here.
                producer.Flush(TimeSpan.FromSeconds(1));
            }
        }
    }
}
