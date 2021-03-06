﻿using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AccountGoWeb.Controllers
{
    public class QuotationsController : Controller
    {
        private readonly IConfiguration _config;

        public QuotationsController(IConfiguration config) {
            _config = config;
        }

        public IActionResult Index()
        {
            return RedirectToAction("quotations");
        }

        public async System.Threading.Tasks.Task<IActionResult> Quotations()
        {
            ViewBag.PageContentHeader = "Quotations";

            using (var client = new HttpClient())
            {
                var baseUri = _config["ApiUrl"];
                client.BaseAddress = new System.Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri + "sales/quotations");
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return View(model: responseJson);
                }
            }

            return View();
        }

        public IActionResult AddSalesQuotation()
        {
            ViewBag.PageContentHeader = "Add Sales Quotation";

            return View();
        }

        public IActionResult Quotation()
        {
            ViewBag.PageContentHeader = "Sales Quotation";

            return View();
        }
    }
}
