using System.Text.Json.Serialization;

namespace DamnTv.Api.Client.Entities
{
    public enum AgeLimit : byte
    {
        NoRestrictions = 0b0000, // 0+
        OverSixYearsOld = 0b0001, // 6+
        OverTwelveYearsOld = 0b0010, // 12+
        OverSixteenYearsOld = 0b0011, // 16+
        OverEighteenYearsOld = 0b0100, // 18+

        GeneralAudiences = 0b1000, // 0+ G
        ParentalGuidanceSuggested = 0b1001, // PG Детям рекомендуется смотреть фильм с родителями
        ParentsStronglyCautioned = 0b1010, // PG-13 Просмотр не желателен детям до 13 лет.
        Restricted = 0b1011,  // R Лица, не достигшие 17-летнего возраста, допускаются на фильм только в сопровождении одного из родителей
        NoOne17AndUnderAdmitted = 0b1100 // NC-17 Лица 17-летнего возраста и младше на фильм не допускаются;
    }

    public static class AgeLimitExtensions
    {
        public static string ToString(this AgeLimit ageLimit)
        {
            return ageLimit switch
            {
                AgeLimit.NoRestrictions => "0+",
                AgeLimit.OverSixYearsOld => "6+",
                AgeLimit.OverTwelveYearsOld => "12+",
                AgeLimit.OverSixteenYearsOld => "16+",
                AgeLimit.OverEighteenYearsOld => "18+",

                AgeLimit.GeneralAudiences => "G",
                AgeLimit.ParentalGuidanceSuggested => "PG",
                AgeLimit.ParentsStronglyCautioned => "PG-13",
                AgeLimit.Restricted => "R",
                AgeLimit.NoOne17AndUnderAdmitted => "NC-17",
                _ => throw new NotImplementedException()
            };
        }
    }
}