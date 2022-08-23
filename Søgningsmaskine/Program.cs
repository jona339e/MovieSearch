using Søgningsmaskine;

namespace SearchProgram
{
	class Program
	{
        static void Main(string[] args)
		{
            //my array of movies, genre and scores.
            object[,] movieTable2D =
                {
                    { "Good Morning Vietnam" , "Comedy" , 1 }, { "Good Night and Good Luck" , "War" , 2 },{ "Good Luck Chuck" , "Adventure" , 3 },{ "The Good the Bad and the Ugly" , "Sci-Fi" , 1 },
                    { "A few Good Men" , "Romance" , 0 },{ "Good Men Good Women" , "Fantasy" , 0 },{ "The Good Son" , "Comedy" , 0 },{ "The Good Shepherd" , "War" , 0 },{ "A change of Seasons" , "Adventure" , 0 },
                    { "The Four Seasons" , "Sci-Fi" , 0 },{ "Three Seasons" , "Sci-Fi" , 0 },{ "Seasons" , "Fantasy" , 0 },{ "Winnie the Pooh: Seasons of Giving" , "Fantasy" , 0 },{ "Four Seasons Lodge" , "Adventure" , 0 },
                    { "A Man for All Seasons" , "Comedy" , 0 },{ "Seasons of Gray " , "Romance" , 0 }
                };


            // Calls the MainSwitch method inside a never ending loop
            while (true)
            {
                MainSwitch(movieTable2D);
            }
        }

        // Method that displays a menu using a switch case. calls various methods.
        static void MainSwitch(object[,] movieTable2D)
        {
            Console.WriteLine("1. Normal Søgning\n2. Få recommended\n3. Se 'hemmelige' statistikker\n4. Afslut Program");

            // Switch is activated on key press in the range of 1-4
            switch (Console.ReadKey().Key)
            {
                // Key press works with both numpad and regular key
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    {
                        Console.Clear();

                        // Declares an object to be used as the do while loop condition. The loop runs while the object is null;
                        object[,] methodChecker;
                        do
                        {
                            // Calls the method Search2D and sets the return value equal to methodChecker
                            methodChecker = MyClass.Search2D(movieTable2D);

                            // If Search2D returned null error is written to the console
                            if (methodChecker == null)
                            {
                                Console.WriteLine("Søgning fandt ingen resultater. Prøv igen!");
                            }
                            // If Search2D returned an array
                            else
                            {
                                // Calls SortArray2D method with two 2D arrays
                                SortArray2D(methodChecker, movieTable2D);
                            }
                        } while (methodChecker == null);
                        break;
                    }
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    {
                        // First runs the method SearchScore with the movieTabel2D object and uses the return value to run the SelectMovie method, again with the movieTable2D object.
                        Console.Clear();
                        SelectMovie(SearchScore(movieTable2D), movieTable2D);
                        break;
                    }
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    {
                        // Prints out all the values stored in movieTable2D to the console.
                        Console.Clear();

                        for (int i = 0; i < movieTable2D.GetLength(0); i++)
                        {
                            Console.WriteLine(movieTable2D[i, 0] + " - " + movieTable2D[i, 1] + " - " + movieTable2D[i, 2]);
                        }
                        Console.WriteLine("tryk på en knap for at gå tilbage");
                        Console.ReadKey();
                        break;
                    }
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    {
                        // Exits the program
                        Environment.Exit(0);
                        break;
                    }
                default:
                    break;
            }


            Console.Clear();
        }

        static void SortArray2D(object[,] myArray, object[,] movieTable2D) 
        {

            string[] tempArr = new string[myArray.GetLength(0)];

            for (int i = 0; i < tempArr.Length; i++)
            {
                tempArr[i] = myArray[i,0].ToString();
            }

            Array.Sort(tempArr);

            SelectMovie(tempArr, movieTable2D);

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

            bool check = false;
            int choice;
            do
            {
                 check = int.TryParse(Console.ReadLine(), out choice);
                if (choice > namesArr.Length || choice <= 0)
                {
                    check = false;
                    Console.WriteLine("Vælg venligst en af de viste valgmuligheder");
                }
            } while (!check);
            


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