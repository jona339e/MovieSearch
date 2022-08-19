using System.Text.RegularExpressions;
using System.Linq;

namespace SearchProgram
{
	class Program
	{
		//static string[] movieTable = { "Good Morning Vietnam", "Good Night and Good Luck", "Good Luck Chuck", "The Good the Bad and the Ugly", "A few Good Men", "Good Men Good Women",
  //      "The Good Son", "The Good Shepherd", "A change of Seasons", "The Four Seasons", "Three Seasons", "Seasons", "Winnie the Pooh: Seasons of Giving", "Four Seasons Lodge",
  //      "A Man for All Seasons", "Seasons of Gray" }; 
        // Array of movie names. Not used as I decided to use multidimensional arrays

        static void Main(string[] args)
		{
            #region
            //small tests i made before starting

            //Brug evt. struct til at give film entries navn og genre
            // til genre kan man bruge et array så filmen kan have mere end 1 genre

            // initializes regex with the word season
            //Regex reg = new Regex("^(season)$");

            //bool resutl = reg.IsMatch("sea");//checks if sea matches the word of the reg instance
            //bool result = reg.IsMatch("season");//checks if season matches the word of the reg instance

            //         Console.WriteLine(resutl);//false
            //Console.WriteLine(result);//true

            #endregion


            //my array of movies, genre and scores.
            object[,] movieTable2D =
                {
                    { "Good Morning Vietnam" , "Comedy" , 1 }, { "Good Night and Good Luck" , "War" , 2 },{ "Good Luck Chuck" , "Adventure" , 3 },{ "The Good the Bad and the Ugly" , "Sci-Fi" , 1 },
                    { "A few Good Men" , "Romance" , 0 },{ "Good Men Good Women" , "Fantasy" , 0 },{ "The Good Son" , "Comedy" , 0 },{ "The Good Shepherd" , "War" , 0 },{ "A change of Seasons" , "Adventure" , 0 },
                    { "The Four Seasons" , "Sci-Fi" , 0 },{ "Three Seasons" , "Sci-Fi" , 0 },{ "Seasons" , "Fantasy" , 0 },{ "Winnie the Pooh: Seasons of Giving" , "Fantasy" , 0 },{ "Four Seasons Lodge" , "Adventure" , 0 },
                    { "A Man for All Seasons" , "Comedy" , 0 },{ "Seasons of Gray " , "Romance" , 0 }
                };


            //SortArray(Search());

            while (true)
            {
                Console.WriteLine("1. Normal Søgning\n2. Få recommended\n3. Se 'hemmelige' statistikker\n4. Afslut Program");
                switch (Console.ReadKey().Key)
                {

                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        {
                            Console.Clear();
                            SortArray2D(Search2D(movieTable2D), movieTable2D);
                            break;
                        }
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        {
                            Console.Clear();
                            SelectMovie(SearchScore(movieTable2D), movieTable2D);
                            break;
                        }
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        {
                            Console.Clear();

                            for (int i = 0; i < movieTable2D.GetLength(0); i++)
                            {
                                Console.WriteLine(movieTable2D[i,0] + " - " + movieTable2D[i,1] + " - " + movieTable2D[i,2]);
                            }

                            Console.ReadLine();
                            break;
                        }
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        break;
                }
                
                
                Console.Clear();
            }
          

            
            //foreach (string s in SearchScore(movieTable2D)) //prints out search results based on score
            //{
            //    Console.WriteLine(s);
            //}

		}
        //gammel kode der ikke skal kigges så meget på
        #region 
        //code that was used initially before making use of multidimensional arrays
        //      static string[] Search()
        //{

        //	int searchArrLength = 0;
        //          Console.WriteLine("Indtast Søgning Her:");
        //	Regex reg = new Regex("(" + Console.ReadLine() + ")"); //takes user input as regex pattern


        //	//Finds amount of matches there is which equals to the length of the array: searchArr
        //	for (int i = 0; i < movieTable.Length; i++)
        //	{
        //		if (reg.IsMatch(movieTable[i]))
        //		{
        //			searchArrLength++;
        //		}
        //	}

        //	string[] searchArr = new string[searchArrLength]; // Array of search results

        //	searchArrLength = 0; // searchArrLength is set to 0 for reuse purposes -- searArrLength is used as the index of the new array searchArr.
        //						 // If 'i' was used from the for loop it would result in an overflow wehre the index of searchArr and movieTable didn't match


        //          //Loop that adds regex matches to the array searchArr
        //          for (int i = 0; i < movieTable.Length; i++)
        //	{
        //              if (reg.IsMatch(movieTable[i]))
        //              {
        //                  searchArr[searchArrLength++] = movieTable[i];

        //              }
        //          }
        //	return searchArr;
        //}

        //static void SortArray(string[] myArray) // sorts array alphabetically and prints it to console
        //{
        //          Array.Sort(myArray);

        //          for (int i = 0; i < myArray.Length; i++)
        //          {
        //              Console.WriteLine(myArray[i]);
        //          }

        //      }

        #endregion


        static object[,] Search2D(object[,] tableInput)
        {

            int searchArrLength = 0;
            bool noMatchInFirstArray = false;
            Console.WriteLine("Indtast Søgning Her:");
            Regex reg = new Regex("(" + Console.ReadLine() + ")", RegexOptions.IgnoreCase); //takes user input as regex pattern.
                                                                                            //RegexOptions.IgnoreCase makes the regex case insensitive

            //Finds amount of matches there is which equals to the length of the array: searchArr
            for (int i = 0; i < tableInput.GetLength(0); i++)
            {
                if (reg.IsMatch(tableInput[i,0].ToString()))
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

            //this is an issue, since this array is given as output and therefore the addscore uses this to incease score instead of the original array
            //fix could be to overload selectmovie(); where it takes 1 array and 2 2DArrays
            object[,] searchArr = new object[searchArrLength,3]; // Array of search results

            searchArrLength = 0; // searchArrLength is set to 0 for reuse purposes -- searArrLength is used as the index of the new array searchArr.
                                 // If 'i' was used from the for loop it would result in an overflow wehre the index of searchArr and movieTable didn't match


            //Loop that adds regex matches to the array searchArr on movietitles
            if (noMatchInFirstArray == false)
            {
                Console.WriteLine("Searching for Title\n");
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
            else            //Loop that adds regex matches to the array searchArr on genre name
            {
                Console.WriteLine("No Title Found, Searching for Genre:\n");
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

        static void SortArray2D(object[,] myArray, object[,] movieTable2D) 
        {

            string[] tempArr = new string[myArray.GetLength(0)];

            for (int i = 0; i < tempArr.Length; i++)
            {
                tempArr[i] = myArray[i,0].ToString();
            }

            Array.Sort(tempArr);

            SelectMovie(tempArr, myArray, movieTable2D);

        }


        static string[] SearchScore(object[,] scoreArr)
        {
            // Notes to self:
            // find where [i,2] is largest in movietable2
            // add the title, where the number is largest, to a new array
            // Display the array


            // lav nyt array med tallene fra movietable2, find index af største værdi
            // index af største værdi == movitable2 index af største værdi, som derfor skal sorteres først.

            int[] indexFinderArr = new int[scoreArr.GetLength(0)];
            

            for (int i = 0; i < scoreArr.GetLength(0); i++) //Finds score and stores them in an array
            {
                indexFinderArr[i] = (int)scoreArr[i, 2]; //typecasting object to int for this to work
            }
            int indexFinderLength = 0;

            for (int i = 0; i < indexFinderArr.Length; i++)
            {
                if (indexFinderArr[i] > 0) { indexFinderLength++; }
            }                                                                     
                                                                                  //finds the number of movies with a score greater than 0
                                                                                  //Which equals the length of the indexFinderArr array

            int[] indexedArr = new int[indexFinderLength];
            int count = 0;

            while (indexFinderArr.Max() > 0)            //creates a sorted array with the highest score first.
            {
                int m = indexFinderArr.Max();
                int pos = Array.IndexOf(indexFinderArr, m);
                indexFinderArr[pos] = 0;
                indexedArr[count++] = m;
            }
            // nu har jeg et array som er sorteret efter den højeste score 
            // jeg skal nu lave et array med filmnavne sorteret efter deres score

            string[] titleArr = new string[indexedArr.Length];
            count = 0;


            //check if s exists in titleArr
            bool checker = false;

            for (int i = 0; i < scoreArr.GetLength(0); i++)
            {
                if (indexedArr[count] == (int)scoreArr[i, 2]) //if indexedArr score == 2darray Score
                {

                    for (int x = 0; x < count; x++)
                    {

                        if ((string)scoreArr[i, 0] == titleArr[x])
                        {
                            checker = true;
                        }

                    }
                    if (!checker)
                    {
                        titleArr[count++] = (string)scoreArr[i, 0];
                        i = -1;
                    }
                    else
                    {
                        checker = false;
                    }
                    
                }
                if (count > titleArr.Length-1)      // if count is greater than the length of the array - 1 (meaning all empty spaces have been filled) the array is returned
                {
                    return titleArr;
                }
            }

            return titleArr; // er unreachable men visual studio brokker sig

        }

        static void SelectMovie(string[] namesArr, object[,] movieTable)
        {
            for (int i = 0; i < namesArr.Length; i++)
            {
                Console.WriteLine(i+1 + " " + namesArr[i]);
            }
            //TODO make this a loop to make sure user gives an input
            int.TryParse(Console.ReadLine(), out int choice);


            string s = namesArr[choice-1];
            int index = 0;
            for (int i = 0; i < movieTable.GetLength(0); i++)
            {
                if (movieTable[i,0] == s)
                {
                    Console.WriteLine(movieTable[i,0] + " - " + movieTable[i,1] + "\n1. Se Film\n2. Gå Tilbage");
                    index = i;
                }
            }

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    {
                        AddScore(index, movieTable);
                        break;
                    }
 
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    {
                        Console.Clear();
                        break;
                    }


                default:
                    break;
            }
        }

        static void SelectMovie(string[] namesArr, object[,] sortedArray,object[,] movieTable) //overloading fordi jeg ikke gider rette min kode
        {
            for (int i = 0; i < namesArr.Length; i++)
            {
                Console.WriteLine(i + 1 + " " + namesArr[i]);
            }
            //TODO make this a loop to make sure user gives an input
            int.TryParse(Console.ReadLine(), out int choice);


            string s = namesArr[choice - 1];
            int index = 0;
            for (int i = 0; i < movieTable.GetLength(0); i++)
            {
                if (movieTable[i, 0] == s)
                {
                    Console.WriteLine(movieTable[i, 0] + " - " + movieTable[i, 1] + "\n1. Se Film\n2. Gå Tilbage");
                    index = i;
                }
            }

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    {
                        AddScore(index, movieTable);
                        break;
                    }

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    {
                        Console.Clear();
                        break;
                    }


                default:
                    break;
            }
        }

        static void AddScore(int index, object [,] movieTable)
        {
            for (int i = 0; i < movieTable.GetLength(0); i++)
            {
                if (movieTable[index,1] == movieTable[i,1])
                {

                    movieTable[i, 2] = (int)movieTable[i,2] + 1;

                }
            }
        }
    }
}