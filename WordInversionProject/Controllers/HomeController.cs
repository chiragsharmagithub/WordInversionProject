using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WordInversionProject.DTOs;
using WordInversionProject.Interfaces;
using WordInversionProject.Models;

namespace WordInversionProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly IWordInversionService _service;
		private readonly ILogger<HomeController> _logger;

		public HomeController(
		    IWordInversionService service,
		    ILogger<HomeController> logger)
		{
			_service = service;
			_logger = logger;
		}

		// GET: Home page with all records or search results
		[HttpGet]
		public async Task<IActionResult> Index(string searchWord = "")
		{
			try
			{
				List<WordInversionResponseDto> records;

				if (!string.IsNullOrWhiteSpace(searchWord))
				{
					var searchResults = await _service.SearchByWordAsync(searchWord);
					records = searchResults.ToList();
					ViewBag.SearchWord = searchWord;
					ViewBag.SearchPerformed = true;
					ViewBag.ResultCount = records.Count;
				}
				else
				{
					var allRecords = await _service.GetAllRecordsAsync();
					records = allRecords.ToList();
					ViewBag.SearchPerformed = false;
				}

				ViewBag.TotalRecords = records.Count;
				return View(records);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error on Index page");
				TempData["Error"] = "An error occurred while loading the page.";
				return View(new List<WordInversionResponseDto>());
			}
		}

		// POST: Invert sentence
		[HttpPost]
		public async Task<IActionResult> Invert(string sentence)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(sentence))
				{
					TempData["Error"] = "Please enter a sentence.";
					return RedirectToAction("Index");
				}

				var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
				var result = await _service.InvertSentenceAsync(sentence, ipAddress);

				TempData["Success"] = $"Inverted sentence: \"{result.InvertedSentence}\"";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error inverting sentence");
				TempData["Error"] = "Error inverting sentence. Please try again.";
				return RedirectToAction("Index");
			}
		}

		// GET: Search records
		[HttpGet]
		public async Task<IActionResult> Search(string word)
		{
			if (string.IsNullOrWhiteSpace(word))
				return RedirectToAction("Index");

			try
			{
				var results = await _service.SearchByWordAsync(word);
				var recordList = results.ToList();

				ViewBag.SearchWord = word;
				ViewBag.SearchPerformed = true;
				ViewBag.ResultCount = recordList.Count;
				ViewBag.TotalRecords = recordList.Count;

				return View("Index", recordList);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error searching records");
				TempData["Error"] = "Search failed. Please try again.";
				return RedirectToAction("Index");
			}
		}

		// POST: Clear search
		[HttpPost]
		public IActionResult ClearSearch()
		{
			return RedirectToAction("Index");
		}
	}
}
