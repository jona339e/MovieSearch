using System.Text.RegularExpressions;

namespace SearchProgram
{
	class Program
	{
		static string[] movieTable = { "Good Morning Vietnam", "Good Night and Good Luck", "Good Luck Chuck", "The Good the Bad and the Ugly", "A few Good Men", "Good Men Good Women",
        "The Good Son", "The Good Shepherd", "A change of Seasons", "The Four Seasons", "Three Seasons", "Seasons", "Winnie the Pooh: Seasons of Giving", "Four Seasons Lodge",
        "A Man for All Seasons", "Seasons of Gray" }; // Array of movie names





        static void Main(string[] args)
		{
            //Brug evt. struct til at give film entries navn og genre
            // til genre kan man bruge et array så filmen kan have mere end 1 genre

            // initializes regex with the word season
            //Regex reg = new Regex("^(season)$");

            //bool resutl = reg.IsMatch("sea");//checks if sea matches the word of the reg instance
            //bool result = reg.IsMatch("season");//checks if season matches the word of the reg instance

            //         Console.WriteLine(resutl);//false
            //Console.WriteLine(result);//true


             object[,] movieTable2 =
                {
                    { "Good Morning Vietnam" , "Comedy" , 1 }, { "Good Night and Good Luck" , "War" , 2 },{ "Good Luck Chuck" , "Adventure" , 3 },{ "The Good the Bad and the Ugly" , "Comedy" , 0 },
                    { "A few Good Men" , "Comedy" , 0 },{ "Good Men Good Women" , "Comedy" , 0 },{ "The Good Son" , "Comedy" , 0 },{ "The Good Shepherd" , "Comedy" , 0 },{ "A change of Seasons" , "Comedy" , 0 },
                    { "The Four Seasons" , "Comedy" , 0 },{ "Three Seasons" , "Comedy" , 0 },{ "Seasons" , "Comedy" , 0 },{ "Winnie the Pooh: Seasons of Giving" , "Comedy" , 0 },{ "Four Seasons Lodge" , "Comedy" , 0 },
                    { "A Man for All Seasons" , "Comedy" , 0 },{ "Seasons of Gray " , "Comedy" , 0 }
                };
            //SortArray(Search());

            SortArray2(Search2(movieTable2));



		}
		static string[] Search()
		{
			
			int searchArrLength = 0;
            Console.WriteLine("Indtast Søgning Her:");
			Regex reg = new Regex("(" + Console.ReadLine() + ")"); //takes user input as regex pattern


			//Finds amount of matches there is which equals to the length of the array: searchArr
			for (int i = 0; i < movieTable.Length; i++)
			{
				if (reg.IsMatch(movieTable[i]))
				{
					searchArrLength++;
				}
			}

			string[] searchArr = new string[searchArrLength]; // Array of search results

			searchArrLength = 0; // searchArrLength is set to 0 for reuse purposes -- searArrLength is used as the index of the new array searchArr.
								 // If 'i' was used from the for loop it would result in an overflow wehre the index of searchArr and movieTable didn't match


            //Loop that adds regex matches to the array searchArr
            for (int i = 0; i < movieTable.Length; i++)
			{
                if (reg.IsMatch(movieTable[i]))
                {
                    searchArr[searchArrLength++] = movieTable[i];
                    
                }
            }
			return searchArr;
		}

		static void SortArray(string[] myArray) // sorts array alphabetically and prints it to console
		{
            Array.Sort(myArray);

            for (int i = 0; i < myArray.Length; i++)
            {
                Console.WriteLine(myArray[i]);
            }

        }


        static object[,] Search2(object[,] tableInput)
        {

            int searchArrLength = 0;
            Console.WriteLine("Indtast Søgning Her:");
            Regex reg = new Regex("(" + Console.ReadLine() + ")"); //takes user input as regex pattern


            //Finds amount of matches there is which equals to the length of the array: searchArr
            for (int i = 0; i < tableInput.GetLength(0); i++)
            {
                if (reg.IsMatch(tableInput[i,0].ToString()))
                {
                    searchArrLength++;
                }
            }

            object[,] searchArr = new object[searchArrLength,3]; // Array of search results

            searchArrLength = 0; // searchArrLength is set to 0 for reuse purposes -- searArrLength is used as the index of the new array searchArr.
                                 // If 'i' was used from the for loop it would result in an overflow wehre the index of searchArr and movieTable didn't match


            //Loop that adds regex matches to the array searchArr
            for (int i = 0; i < tableInput.GetLength(0); i++)
            {
                if (reg.IsMatch(tableInput[i, 0].ToString()))
                {
                    for (int x = 0; x < tableInput.GetLength(1); x++)
                    {
                        searchArr[searchArrLength, x] = tableInput[i,x];
                    }
                    searchArrLength++;
                }
            }
            return searchArr;
        }

        static void SortArray2(object[,] myArray) 
        {

            string[] tempArr = new string[myArray.GetLength(0)];

            for (int i = 0; i < tempArr.Length; i++)
            {
                tempArr[i] = myArray[i,0].ToString();
            }

            Array.Sort(tempArr);

            for (int i = 0; i < tempArr.Length; i++)
            {
                Console.WriteLine(tempArr[i]);
            }

        }


    }
}