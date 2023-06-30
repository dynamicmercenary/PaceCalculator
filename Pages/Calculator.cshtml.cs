using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RaceCalculator.Pages
{
    public class CalculatorModel : PageModel
    {
        [BindProperty]
        public decimal Distance { get; set; }

        [BindProperty]
        public decimal OtherDistance { get; set; }

        [BindProperty]
        public required string Time { get; set; }

        [BindProperty]
        public required string Result { get; set; }

        [BindProperty]
        public required string Splits { get; set; }

        public void OnGet()
        {
            // Initialize the properties with default values
            Distance = 0;
            OtherDistance = 0;
            Time = "";
            Splits = "neutral";
            Result = "";
        }

        public void OnPostCalculatePace()
        {
            // Parse the time input to extract hours, minutes, and seconds
            string[] timeParts = Time.Split(':');
            if (timeParts.Length != 3)
            {
                Result = "Invalid time format. Please enter the time in HH:mm:ss format.";
                return;
            }

            if (!TimeSpan.TryParse(Time, out TimeSpan totalTime))
            {
                Result = "Invalid time format. Please enter valid numeric values for hours, minutes, and seconds.";
                return;
            }

            if (Distance <= 0 && OtherDistance <= 0)
            {
                Result = "Please enter a valid distance.";
                return;
            }

            decimal distance = Distance;
            if (Distance == -1)
            {
                distance = OtherDistance;
            }

            decimal paceInSecondsPerKm = (decimal)totalTime.TotalSeconds / distance;

            bool isPositiveSplits = Splits == "positive";
            bool isNegativeSplits = Splits == "negative";
            bool isNeutralSplits = Splits == "neutral";

            decimal adjustedPaceFirstHalf;
            decimal adjustedPaceSecondHalf;
            TimeSpan adjustedPaceFirstHalfTime;
            TimeSpan adjustedPaceSecondHalfTime;
            TimeSpan adjustedPaceHalfTime;

            if (isPositiveSplits)
            {
                adjustedPaceFirstHalf = paceInSecondsPerKm - 10; // Pace for the first half remains the same
                adjustedPaceSecondHalf = paceInSecondsPerKm + 10; // Increase pace by 10% for the second half

                adjustedPaceFirstHalfTime = TimeSpan.FromSeconds((double)adjustedPaceFirstHalf);
                adjustedPaceSecondHalfTime = TimeSpan.FromSeconds((double)adjustedPaceSecondHalf);

                Result = $"First Half Pace: {adjustedPaceFirstHalfTime.Minutes:00}:{adjustedPaceFirstHalfTime.Seconds:00} min/km\n" +
                     $"Second Half Pace: {adjustedPaceSecondHalfTime.Minutes:00}:{adjustedPaceSecondHalfTime.Seconds:00} min/km";
            }
            else if (isNegativeSplits)
            {
                adjustedPaceFirstHalf = paceInSecondsPerKm + 10; // Pace for the first half remains the same
                adjustedPaceSecondHalf = paceInSecondsPerKm - 10; // Decrease pace by 10% for the first half

                adjustedPaceFirstHalfTime = TimeSpan.FromSeconds((double)adjustedPaceFirstHalf);
                adjustedPaceSecondHalfTime = TimeSpan.FromSeconds((double)adjustedPaceSecondHalf);

                Result = $"First Half Pace: {adjustedPaceFirstHalfTime.Minutes:00}:{adjustedPaceFirstHalfTime.Seconds:00} min/km\n" +
                     $"Second Half Pace: {adjustedPaceSecondHalfTime.Minutes:00}:{adjustedPaceSecondHalfTime.Seconds:00} min/km";
            }
            else
            {
                adjustedPaceHalfTime = TimeSpan.FromSeconds((double)paceInSecondsPerKm);
                Result = $"{adjustedPaceHalfTime.Minutes:00}:{adjustedPaceHalfTime.Seconds:00} min/km";
            }
        }

        public void OnPostCalculateFinishingTime()
        {
            // Parse the time input to extract hours, minutes, and seconds
            string[] timeParts = Time.Split(':');
            if (timeParts.Length != 3)
            {
                Result = "Invalid time format. Please enter the time in HH:mm:ss format.";
                return;
            }

            if (!TimeSpan.TryParse(Time, out TimeSpan totalTime))
            {
                Result = "Please enter a valid time in HH:mm:ss format.";
                return;
            }

            if (Distance <= 0 && OtherDistance <= 0)
            {
                Result = "Please enter a valid distance.";
                return;
            }

            decimal distance = Distance;
            if (Distance == -1)
            {
                distance = OtherDistance;
            }

            decimal paceInSecondsPerKm = (decimal)totalTime.TotalSeconds / distance;

            bool isPositiveSplits = Splits == "positive";
            bool isNegativeSplits = Splits == "negative";

            decimal adjustedPaceFirstHalf;
            decimal adjustedPaceSecondHalf;
            TimeSpan adjustedPaceFirstHalfTime;
            TimeSpan adjustedPaceSecondHalfTime;
            TimeSpan adjustedPaceHalfTime;

            if (isPositiveSplits)
            {
                adjustedPaceFirstHalf = paceInSecondsPerKm - 10; // Pace for the first half remains the same
                adjustedPaceSecondHalf = paceInSecondsPerKm + 10; // Increase pace by 10% for the second half

                adjustedPaceFirstHalfTime = TimeSpan.FromSeconds((double)adjustedPaceFirstHalf);
                adjustedPaceSecondHalfTime = TimeSpan.FromSeconds((double)adjustedPaceSecondHalf);

                Result = $"First Half Pace: {adjustedPaceFirstHalfTime.Minutes:00}:{adjustedPaceFirstHalfTime.Seconds:00} min/km\n" +
                     $"Second Half Pace: {adjustedPaceSecondHalfTime.Minutes:00}:{adjustedPaceSecondHalfTime.Seconds:00} min/km";
            }
            else if(isNegativeSplits)
            {
                adjustedPaceFirstHalf = paceInSecondsPerKm + 10; // Pace for the first half remains the same
                adjustedPaceSecondHalf = paceInSecondsPerKm - 10; // Decrease pace by 10% for the first half

                adjustedPaceFirstHalfTime = TimeSpan.FromSeconds((double)adjustedPaceFirstHalf);
                adjustedPaceSecondHalfTime = TimeSpan.FromSeconds((double)adjustedPaceSecondHalf);

                Result = $"First Half Pace: {adjustedPaceFirstHalfTime.Minutes:00}:{adjustedPaceFirstHalfTime.Seconds:00} min/km\n" +
                     $"Second Half Pace: {adjustedPaceSecondHalfTime.Minutes:00}:{adjustedPaceSecondHalfTime.Seconds:00} min/km";
            }
            else {
                adjustedPaceHalfTime = TimeSpan.FromSeconds((double)paceInSecondsPerKm);
                Result = $"{adjustedPaceHalfTime.Minutes:00}:{adjustedPaceHalfTime.Seconds:00} min/km";
            }

            
        }
    }
}
