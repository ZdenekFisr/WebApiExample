using System.Text;
using WebApiExample.Enums;
using WebApiExample.Extensions;

namespace WebApiExample.Features.NumberInWords
{
    /// <inheritdoc cref="INumberInWordsCzechService"/>
    public class NumberInWordsCzechService : INumberInWordsCzechService
    {
        private readonly Dictionary<byte, string> units = new()
        {
            {3, "tři" },
            {4, "čtyři" },
            {5, "pět" },
            {6, "šest" },
            {7, "sedm" },
            {8, "osm" },
            {9, "devět" },
            {10, "deset" },
            {11, "jedenáct" },
            {12, "dvanáct" },
            {13, "třináct" },
            {14, "čtrnáct" },
            {15, "patnáct" },
            {16, "šestnáct" },
            {17, "sedmnáct" },
            {18, "osmnáct" },
            {19, "devatenáct" }
        };

        private readonly Dictionary<byte, string> tens = new()
        {
            {2, "dvacet" },
            {3, "třicet" },
            {4, "čtyřicet" },
            {5, "padesát" },
            {6, "šedesát" },
            {7, "sedmdesát" },
            {8, "osmdesát" },
            {9, "devadesát" }
        };

        private readonly Dictionary<byte, string> hundreds = new()
        {
            {1, "sto" },
            {2, "dvě stě" },
            {3, "tři sta" },
            {4, "čtyři sta" },
            {5, "pět set" },
            {6, "šest set" },
            {7, "sedm set" },
            {8, "osm set" },
            {9, "devět set" }
        };

        private readonly Dictionary<byte, string> hundredsNoSpaces = new()
        {
            {1, "sto" },
            {2, "dvěstě" },
            {3, "třista" },
            {4, "čtyřista" },
            {5, "pětset" },
            {6, "šestset" },
            {7, "sedmset" },
            {8, "osmset" },
            {9, "devětset" }
        };

        private readonly string[] multiplesOfThousandOne = ["trilion", "biliarda", "bilion", "miliarda", "milion", "tisíc"]; // 2, 3, ..., 7
        private readonly string[] multiplesOfThousandTwoToFour = ["triliony", "biliardy", "biliony", "miliardy", "miliony", "tisíce"];
        private readonly string[] multiplesOfThousandFiveOrMore = ["trilionů", "biliard", "bilionů", "miliard", "milionů", "tisíc"];

        /// <inheritdoc />
        public string NumberToWords(long number, GrammaticalGender gender, bool insertSpaces)
        {
            if (number == 0)
                return "nula";

            StringBuilder result = new();
            if (number < 0)
            {
                result.Append("minus");
                number = -number;
            }

            long divisor = 1000000000000000000;
            long numberOfEntities;
            for (byte i = 0; i < multiplesOfThousandOne.Length; i++)
            {
                numberOfEntities = number / divisor;

                if (numberOfEntities != 0)
                {
                    string thousandsOrMoreString = ThousandsOrMore(
                        i == 1 || i == 3 ? GrammaticalGender.Feminine : GrammaticalGender.Masculine,
                        insertSpaces,
                        numberOfEntities % 1000,
                        multiplesOfThousandOne[i],
                        multiplesOfThousandTwoToFour[i],
                        multiplesOfThousandFiveOrMore[i]);

                    if (thousandsOrMoreString != string.Empty)
                    {
                        if (result.ToString() != string.Empty && insertSpaces)
                            result.Append(' ');
                        result.Append(thousandsOrMoreString);
                    }
                }

                divisor /= 1000;
            }

            string oneToThousandString = OneToThousand(gender, number % 1000, insertSpaces);

            if (oneToThousandString != string.Empty)
            {
                if (result.ToString() != string.Empty && insertSpaces)
                    result.Append(' ');
                result.Append(oneToThousandString);
            }

            return result.ToString();
        }

        private string OneToThousand(GrammaticalGender gender, long oneToThousand, bool insertSpaces)
        {
            if (oneToThousand == 0)
                return string.Empty;

            // split the number
            byte oneToHundred = (byte)(oneToThousand % 100);
            byte hundred = (byte)(oneToThousand / 100);
            string oneToHundredString;
            string hundredString = string.Empty;

            // append hundreds
            if (hundred != 0)
            {
                hundredString = insertSpaces ? hundreds[hundred] : hundredsNoSpaces[hundred];
                if (oneToHundred == 0)
                    return hundredString;
            }

            // append 1 to 99
            oneToHundredString = OneToHundred(gender, oneToHundred, insertSpaces);
            if (hundred == 0)
                return oneToHundredString;

            if (insertSpaces)
                return hundredString + " " + oneToHundredString;

            return hundredString + oneToHundredString;
        }

        private string OneToHundred(GrammaticalGender gender, byte oneToHundred, bool insertSpace)
        {
            if (oneToHundred == 0)
                return string.Empty;

            StringBuilder resultBuilder = new();

            switch (gender)
            {
                case GrammaticalGender.Masculine:
                    units.AddOrReplaceValue<byte, string>(1, "jeden");
                    units.AddOrReplaceValue<byte, string>(2, "dva");
                    break;
                case GrammaticalGender.Feminine:
                    units.AddOrReplaceValue<byte, string>(1, "jedna");
                    units.AddOrReplaceValue<byte, string>(2, "dvě");
                    break;
                case GrammaticalGender.Neuter:
                    units.AddOrReplaceValue<byte, string>(1, "jedno");
                    units.AddOrReplaceValue<byte, string>(2, "dvě");
                    break;
            };

            if (oneToHundred <= 19)
            {
                resultBuilder.Append(units[oneToHundred]);
                return resultBuilder.ToString();
            }

            byte ten = (byte)(oneToHundred / 10);
            resultBuilder.Append(tens[ten]);
            if (oneToHundred % 10 != 0)
            {
                if (insertSpace)
                    resultBuilder.Append(' ');

                resultBuilder.Append(units[(byte)(oneToHundred - ten * 10)]);
            }
            return resultBuilder.ToString();
        }

        private string ThousandsOrMore(GrammaticalGender gender, bool insertSpace, long numberOfEntities, string oneEntity, string twoToFourEntities, string fiveOrMoreEntities)
        {
            if (numberOfEntities == 1)
                return oneEntity;

            StringBuilder resultBuilder = new();

            resultBuilder.Append(OneToThousand(gender, numberOfEntities, insertSpace));

            if (resultBuilder.ToString() == string.Empty)
                return string.Empty;

            if (insertSpace)
                resultBuilder.Append(' ');

            if (numberOfEntities % 10 == 1 && numberOfEntities % 100 != 11)
                resultBuilder.Append(oneEntity);
            else if (numberOfEntities % 10 >= 2 && numberOfEntities % 10 <= 4 && (numberOfEntities % 100 < 12 || numberOfEntities % 100 > 14))
                resultBuilder.Append(twoToFourEntities);
            else
                resultBuilder.Append(fiveOrMoreEntities);

            return resultBuilder.ToString();
        }
    }
}
