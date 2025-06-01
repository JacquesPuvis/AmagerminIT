using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using AmagerminIT.Models;

public class CalendarModel : PageModel
{
    public static List<CalendarEvent> EventList = new();

    [BindProperty]
    public CalendarEvent NewEvent { get; set; }

    // Selected date for popup
    [BindProperty(SupportsGet = true)]
    public DateTime? SelectedDate { get; set; }

    // For displaying current month/year in calendar
    [BindProperty(SupportsGet = true)]
    public int CurrentMonth { get; set; }

    [BindProperty(SupportsGet = true)]
    public int CurrentYear { get; set; }

    public List<CalendarEvent> Events => EventList;

    public void OnGet()
    {
        if (CurrentMonth < 1 || CurrentMonth > 12 || CurrentYear < 1)
        {
            var today = DateTime.Today;
            CurrentMonth = today.Month;
            CurrentYear = today.Year;
        }
    }

    public IActionResult OnPostAddEvent()
    {
        if (ModelState.IsValid && NewEvent.Date != DateTime.MinValue)
        {
            NewEvent.Id = EventList.Count + 1;
            EventList.Add(NewEvent);
        }

        // Redirect without SelectedDate param — closes popup
        return RedirectToPage(new { CurrentMonth, CurrentYear });
    }

}