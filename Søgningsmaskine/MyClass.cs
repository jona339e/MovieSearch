using System.Text.RegularExpressions;

namespace Søgningsmaskine
{
    internal class MyClass
    {

        internal static object[,] Search2D(object[,] movieTable2D)
        {
            //TODO make it so both titles and genres are searched for. atm you can have a title with a genre name and only return the title.
            int searchArrLength = 0;
            bool noMatchInFirstArray = false;
            bool noMatchInSecondArray = false;
            Console.WriteLine("Du kan søge på Titel eller Genre\nIndtast Søgning Her:");
            string s = "";

            // Takes user input and makes sure it isn't null or empty
            do
            {
                s = Console.ReadLine();
                if (string.IsNullOrEmpty(s))
                {
                    Console.Clear();
                    Console.WriteLine("Du kan søge på Titel eller Genre\nIndtastning må ikke være tom\nIndtast Søgning Her:");
                }
            } while (string.IsNullOrEmpty(s));

            // takes user input as regex pattern.
            // RegexOptions.IgnoreCase makes the regex case insensitive
            Regex reg = new Regex("(" + s + ")", RegexOptions.IgnoreCase); 

            Console.Clear();

            // Finds amount of matches there is which equals to the length of the array: searchArr
            for (int i = 0; i < movieTable2D.GetLength(0); i++)
            {
                if (reg.IsMatch(movieTable2D[i, 0].ToString()))
                {
                    searchArrLength++;
                }
            }
            // If no match is found on the array with movie titles the program looks for a match on genre name.
            if (searchArrLength == 0) 
            {
                noMatchInFirstArray = true;

                // Does the same as above for loop except this is on the genre placement in the 2D array.
                for (int i = 0; i < movieTable2D.GetLength(0); i++)
                {
                    if (reg.IsMatch(movieTable2D[i, 1].ToString()))
                    {
                        searchArrLength++;
                    }
                }
            }

            // Checks if there is any matches at all, if there is not null is returned
            if (searchArrLength == 0)
            {
                return null;
            }

            // Array of search results
            object[,] searchArr = new object[searchArrLength, 3];

            // searchArrLength is set to 0 for reuse purposes -- searArrLength is used as the index of the new array searchArr.
            searchArrLength = 0; 


            // Loop that adds regex matches to the array searchArr on movietitles
            if (!noMatchInFirstArray)
            {
                Console.WriteLine("Søger på Titel\n");
                for (int i = 0; i < movieTable2D.GetLength(0); i++)
                {
                    if (reg.IsMatch(movieTable2D[i, 0].ToString()))
                    {
                        for (int x = 0; x < movieTable2D.GetLength(1); x++)
                        {
                            searchArr[searchArrLength, x] = movieTable2D[i, x];
                        }
                        searchArrLength++;
                    }
                }
            }

            // Loop that adds regex matches to the array searchArr on genre name
            else if (!noMatchInSecondArray)            
            {
                Console.WriteLine("Søger på genre\n");
                for (int i = 0; i < movieTable2D.GetLength(0); i++)
                {
                    if (reg.IsMatch(movieTable2D[i, 1].ToString()))
                    {
                        for (int x = 0; x < movieTable2D.GetLength(1); x++)
                        {
                            searchArr[searchArrLength, x] = movieTable2D[i, x];
                        }
                        searchArrLength++;
                    }
                }
            }


            return searchArr;
        }
    }
}
