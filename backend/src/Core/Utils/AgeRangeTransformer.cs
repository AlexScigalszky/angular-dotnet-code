using System;

namespace Core.Utils
{
    public static class AgeRangeTransformer
    {

        private const string UNDER_20 = "Under 20";
        private const string BETWEEN_20_34 = "20 to 34";
        private const string BETWEEN_35_49 = "35 to 49";
        private const string BETWEEN_50_65 = "50 to 65";
        private const string PLUS_65 = "65+";

        public static string ToRange(this int age)
        {
            if (age < 20)
            {
                return UNDER_20;
            }
            else if (20 <= age && age <= 34)
            {
                return BETWEEN_20_34;
            }
            else if (35 <= age && age <= 49)
            {
                return BETWEEN_35_49;
            }
            else if (50 <= age && age <= 65)
            {
                return BETWEEN_50_65;
            }
            else
            {
                return PLUS_65;
            }
        }

        public static string ToRange(this DateTime birthdate)
        {
            return ToRange(Age(birthdate));
        }

        /// <summary>
        /// Calculates the age in years of the current System.DateTime object today.
        /// </summary>
        /// <param name="birthDate">The date of birth</param>
        /// <returns>Age in years today. 0 is returned for a future date of birth.</returns>
        public static int Age(this DateTime birthDate)
        {
            return Age(birthDate, DateTime.Today);
        }

        /// <summary>
        /// Calculates the age in years of the current System.DateTime object on a later date.
        /// </summary>
        /// <param name="birthDate">The date of birth</param>
        /// <param name="laterDate">The date on which to calculate the age.</param>
        /// <returns>Age in years on a later day. 0 is returned as minimum.</returns>
        public static int Age(this DateTime birthDate, DateTime laterDate)
        {
            int age;
            age = laterDate.Year - birthDate.Year;

            if (age > 0)
            {
                age -= Convert.ToInt32(laterDate.Date < birthDate.Date.AddYears(age));
            }
            else
            {
                age = 0;
            }

            return age;
        }
    }
}
