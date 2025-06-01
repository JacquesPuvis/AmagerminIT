using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AmagerminIT.Models;

public class CalendarViewComponent : ViewComponent
{
    public static List<CalendarEvent> EventList = CalendarModel.EventList; // share same list

    public IViewComponentResult Invoke(int? currentMonth, int? currentYear, DateTime? selectedDate)
    {
        var today = DateTime.Today;
        int month = currentMonth.HasValue && currentMonth.Value >= 1 && currentMonth.Value <= 12 ? currentMonth.Value : today.Month;
        int year = currentYear.HasValue && currentYear.Value >= 1 ? currentYear.Value : today.Year;

        var model = new CalendarViewModel
        {
            CurrentMonth = month,
            CurrentYear = year,
            SelectedDate = selectedDate,
            Events = EventList
        };

        return View(model);
    }
}

public class CalendarViewModel
{
    public int CurrentMonth { get; set; }
    public int CurrentYear { get; set; }
    public DateTime? SelectedDate { get; set; }
    public List<CalendarEvent> Events { get; set; }
}