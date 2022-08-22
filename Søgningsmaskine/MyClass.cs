using System.Text.RegularExpressions;

namespace Søgningsmaskine
{
    internal class MyClass
    {

        internal static object[,] Search2D(object[,] tableInput)
        {

            int searchArrLength = 0;
            bool noMatchInFirstArray = false;
            bool noMatchInSecondArray = false;
            Console.WriteLine("Du kan søge på Titel eller Genre\nIndtast Søgning Her:");
            string s = "";

            do
            {
                s = Console.ReadLine();
                if (string.IsNullOrEmpty(s))
                {
                    Console.Clear();
                    Console.WriteLine("Du kan søge på Titel eller Genre\nIndtastning må ikke være tom\nIndtast Søgning Her:");
                }
            } while (string.IsNullOrEmpty(s));
            Regex reg = new Regex("(" + s + ")", RegexOptions.IgnoreCase); //takes user input as regex pattern.
                                                                           //RegexOptions.IgnoreCase makes the regex case insensitive
            Console.Clear();
            //Finds amount of matches there is which equals to the length of the array: searchArr
            for (int i = 0; i < tableInput.GetLength(0); i++)
            {
                if (reg.IsMatch(tableInput[i, 0].ToString()))
                {
                    searchArrLength++;
                }
            }
            if (searchArrLength == 0) //If no match is found on the array with movie titles the program looks for a match on genre name.
            {
                noMatchInFirstArray = true;
                for (int i = 0; i < tableInput.GetLength(0); i++)
                {
                    if (reg.IsMatch(tableInput[i, 1].ToString()))
                    {
                        searchArrLength++;
                    }
                }
            }

            if (noMatchInFirstArray == true && searchArrLength == 0)
            {
                Console.WriteLine("Ingen resultater fundet");
                return null;
            }

            //this is an issue, since this array is given as output and therefore the addscore uses this to incease score instead of the original array
            //fix could be to overload selectmovie(); where it takes 1 array and 2 2DArrays
            object[,] searchArr = new object[searchArrLength, 3]; // Array of search results

            searchArrLength = 0; // searchArrLength is set to 0 for reuse purposes -- searArrLength is used as the index of the new array searchArr.
                                 // If 'i' was used from the for loop it would result in an overflow wehre the index of searchArr and movieTable didn't match


            //Loop that adds regex matches to the array searchArr on movietitles
            if (!noMatchInFirstArray)
            {
                Console.WriteLine("Søger på Titel\n");
                for (int i = 0; i < tableInput.GetLength(0); i++)
                {
                    if (reg.IsMatch(tableInput[i, 0].ToString()))
                    {
                        for (int x = 0; x < tableInput.GetLength(1); x++)
                        {
                            searchArr[searchArrLength, x] = tableInput[i, x];
                        }
                        searchArrLength++;
                    }
                }
            }
            else if (!noMatchInSecondArray)            //Loop that adds regex matches to the array searchArr on genre name
            {
                Console.WriteLine("Søger på genre\n");
                for (int i = 0; i < tableInput.GetLength(0); i++)
                {
                    if (reg.IsMatch(tableInput[i, 1].ToString()))
                    {
                        for (int x = 0; x < tableInput.GetLength(1); x++)
                        {
                            searchArr[searchArrLength, x] = tableInput[i, x];
                        }
                        searchArrLength++;
                    }
                }
            }


            return searchArr;
        }
    }
}
