using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Date
{
    private int day;
    private int month;
    private int year;

    public readonly int ID;

    private static int objectCounter;

    private const int minYear = 1900;

    public static void PrintClassInfo()
    {
        Console.WriteLine($"Object count: {objectCounter}");
    }
    private string GetMonthName()
    {
        return new DateTime(year, month, day).ToString("MMMM");
    }

    static Date()
    {
        objectCounter = 0;
    }

    private Date()
    {
        ID = GetHashCode();
    }

    public Date(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;
        objectCounter++;
    }

    public Date(int day = 1, int month = 12) : this(day, month, minYear) { }

    public int Day
    {
        get { return day; }
        set
        {
            if (value < 1 || value > DateTime.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Неверно указан день.");
            }
            day = value;

        }
    }
    public int Month
    {
        get { return month; }
        set
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Неверно указан месяц.");
            }
            month = value;
        }
    }

    public int Year
    {
        get { return year; }
        set
        {
            if (year < minYear || year > 2024)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Неверно указан год.");
            }
            year = value;
        }
    }

    public void PrintDate()
    {
        Console.WriteLine($"{day}/{month}/{year}");
        Console.WriteLine($"{day} {GetMonthName()} {year} года");
    }

    public void UpdateDate(ref int newDay, ref int newMonth, out int updatedYear)
    {
        if (newDay < 1 || newDay > DateTime.DaysInMonth(year, newMonth))
        {
            throw new ArgumentOutOfRangeException(nameof(newDay), "Неверно указан день.");
        }

        if (newMonth < 1 || newMonth > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(newMonth), "Неверно указан месяц.");
        }

        day = newDay;
        month = newMonth;

        updatedYear = year;
    }

    public override bool Equals(object obj)
    {
        return obj is Date other && day == other.day && month == other.month && year == other.year;
    }
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + day.GetHashCode();
        hash = hash * 23 + month.GetHashCode();
        return hash;
    }

    public override string ToString()
    {
        return $"Date [day: {day}, month: {month}, year: {year}";
    }

}

public class Holiday
{
    public string Name { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }

    public Holiday(string name, int day, int month)
    {
        Name = name;
        Day = day;
        Month = month;
    }

    public override string ToString()
    {
        return $"Holiday: {Name} on {Day}/{Month}";
    }
}


namespace OOP10
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // 1
            string[] months = { "June", "July", "May", "December", "January", "February", "March", "April", "August", "September", "October", "November" };

            int n = 4;

            var monthsWithLengthN = from month in months 
                                    where month.Length == n
                                    select month;

            foreach (var month in monthsWithLengthN) 
            { 
                Console.WriteLine(month);
            }

            Console.WriteLine("===========================");

            var summerAndWinterMonths = from month in months
                                        where month == "June" || month == "July" || month == "August" ||
                                              month == "December" || month == "January" || month == "February"
                                        select month;

            foreach (var month in summerAndWinterMonths)
            {
                Console.WriteLine(month);
            }

            Console.WriteLine("===========================");

            var monthAsc = from month in months
                           orderby month
                           select month;

            foreach (var month in monthAsc)
            {
                Console.WriteLine(month);
            }

            Console.WriteLine("===========================");

            var monthsWithUAndMinLength4 = from month in months
                                           where month.Contains("u") && month.Length >= 4
                                           select month;

            foreach (var month in monthsWithUAndMinLength4)
            {
                Console.WriteLine(month);
            }

            Console.WriteLine("============= 2 ==============");

            // 2

            List<Date> dateList = new List<Date>()
            {
                new Date(1, 1, 2023),
                new Date(15, 2, 2022),
                new Date(10, 3, 2021),
                new Date(25, 4, 2022),
                new Date(5, 5, 2019),
                new Date(30, 6, 2018),
                new Date(12, 7, 2017),
                new Date(9, 4, 2016),
                new Date(22, 5, 2015),
                new Date(30, 6, 2018),
                new Date(3, 10, 2014),
                new Date(14, 2, 2018),
            };

            int yearForSelect = 2022;

            var targetYear = from date in dateList
                             where date.Year == yearForSelect
                             select date;

            foreach (var date in targetYear)
            {
                Console.WriteLine($"Day: {date.Day}, Month: {date.Month}, Year: {date.Year}");
            }

            Console.WriteLine("===========================");

            int monthForSelect = 4;

            var targetMonth = from date in dateList
                              where date.Month == monthForSelect
                              select date;

            foreach (var date in targetMonth)
            {
                Console.WriteLine($"Day: {date.Day}, Month: {date.Month}, Year: {date.Year}");
            }

            Console.WriteLine("===========================");

            int startYear = 2014, endYear = 2016;

            var countOfDatesInRange = dateList.Count(date => date.Year >= startYear && date.Year <= endYear);

            Console.WriteLine($"Count of dates in range: {countOfDatesInRange}");

            var maxDate = dateList.OrderByDescending(d => d.Year).ThenByDescending(d => d.Month).ThenByDescending(d => d.Day).FirstOrDefault();

            Console.WriteLine($"Max date: {maxDate.Day}.{maxDate.Month}.{maxDate.Year}");

            int targetDay = 10;

            var firstTargetDay = dateList.FirstOrDefault(date => date.Day == targetDay);

            Console.WriteLine($"First target day: {firstTargetDay}");

            var orderedDates = dateList.OrderBy(d => d.Year).ThenBy(d => d.Month).ThenBy(d => d.Day);

            foreach (var date in orderedDates)
            {
                Console.WriteLine($"Day: {date.Day}, Month: {date.Month}, Year: {date.Year}");
            }

            Console.WriteLine("===========================");

            // 3

            var complexQuery = dateList
                .Where(d => d.Year >= 2015 && d.Year <= 2023)
                .Select(d => new { d.Month, d.Year })
                .GroupBy(d => d.Month)
                .OrderByDescending(g => g.Count())
                .Take(2);

            foreach (var group in complexQuery)
            {
                Console.WriteLine($"Month: {group.Key}, Count: {group.Count()}");

                foreach (var date in group)
                {
                    Console.WriteLine($"Year: {date.Year}");
                }
            }

            Console.WriteLine("===========================");

            // 4

            List<Holiday> holidayList = new List<Holiday>
            {
                new Holiday("New Year", 1, 1),
                new Holiday("Christmas", 25, 12),
                new Holiday("Labor Day", 1, 5),
                new Holiday("Halloween", 31, 10),
                new Holiday("Valentine's Day", 14, 2)
            };



            var joinQuery = from date1 in dateList
                            join date2 in holidayList
                            on new { date1.Day, date1.Month } equals new { date2.Day, date2.Month }
                            select new { Date = date1, Holiday = date2.Name };

            foreach (var item in joinQuery)
            {
                Console.WriteLine($"{item.Date.Day}.{item.Date.Month}.{item.Date.Year} - {item.Holiday}");
            }

        }
    }
}
