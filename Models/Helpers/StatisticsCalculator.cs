using System;

namespace WePlayBall.Models.Helpers
{
    public static class StatisticsCalculator
    {
        /// <summary>
        /// W% - Calculate a teams win percentage to 3 decimal points
        /// </summary>
        /// <param name="wins"></param>
        /// <param name="games"></param>
        /// <returns></returns>
        public static decimal WinPercentage(int wins, int games)
        {
            var calculation = (wins / games) * 100;
            var result = decimal.Round(calculation, 3);
            return result;
        }

        /// <summary>
        /// L% - Calculate a teams loss percentages to 3 decimal places
        /// </summary>
        /// <param name="losses"></param>
        /// <param name="games"></param>
        /// <returns></returns>
        public static decimal LossPercentage(int losses, int games)
        {
            var calculation = (losses / games) * 100;
            var result = decimal.Round(calculation, 3);
            return result;
        }

        /// <summary>
        /// BPG - Baskets per game, expressed a decimal to 1 place
        /// </summary>
        /// <param name="basketsFor"></param>
        /// <param name="games"></param>
        /// <returns></returns>
        public static decimal BasketsPerGame(int basketsFor, int games)
        {
            var calculation = (basketsFor / games);
            var result = decimal.Round(calculation, 1);
            return result;
        }

        //  W-L% - Won - Loss percentage
        //  https://www.basketball-reference.com/about/glossary.html
        public static decimal WinLossPercentage(int wins, int losses)
        {
            var calculation = wins / (wins + losses);
            var result = decimal.Round(calculation, 3);
            return result;
        }

        /// <summary>
        /// .500 - Calculate Wins Over .500; the formula is (W - L) / 2.
        /// </summary>
        /// <param name="wins"></param>
        /// <param name="losses"></param>
        /// <returns></returns>
        public static decimal WinsOver50(int wins, int losses)
        {
            var calculation = (wins - losses) / 2;
            var result = decimal.Round(calculation, 3);
            return result;
        }

        //  Pythagorean Wins (W Pyth) - adapted it using baskets for as opposed to using
        //  points for / against. return expressed as a fraction to 2dp.
        //  https://captaincalculator.com/sports/basketball/pythagorean-win-percentage-calculator/
        public static decimal WPyth(int basketsFor, int basketsAgainst)
        {
            const double power = 13.91;
            var numerator = Math.Pow(basketsFor, power);
            var denominator = (Math.Pow(basketsFor, power) + Math.Pow(basketsAgainst, power));
            var calculation = (numerator / denominator) * 100;
            var calToDecimal = System.Convert.ToDecimal(calculation);
            return decimal.Round(calToDecimal, 2);
        }
    }
}
