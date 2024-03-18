// <summary>
// Assignment 3
// 
// Author: Carl Wong
// Date: 2024-03-06
// Purpose: Allows user to enter/save/load/edit/view daily sales values
//          from a file. Allows and displays simple data analysis
//          (mean/max/min/graph) of sales values for a given month.
// </summary>
//

bool contin = true;
int physicalSize = 31;
int logicalSize = 0;


double[] sales = new double[physicalSize];
string[] dates = new string[physicalSize];

double minValue = 0.0;
double maxValue = 100.0;

// "Main Function"
bool goAgain = true;
  while (goAgain)
  {
    try
    {
      DisplayMainMenu();
      string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
      if (mainMenuChoice == "L")
        logicalSize = LoadFileValuesToMemory(dates, sales);
      if (mainMenuChoice == "S")
        SaveMemoryValuesToFile(dates, sales, logicalSize);
      if (mainMenuChoice == "D")
        DisplayMemoryValues(dates, sales, logicalSize);
      if (mainMenuChoice == "A")
        logicalSize = AddMemoryValues(dates, sales, logicalSize);
      if (mainMenuChoice == "E")
        EditMemoryValues(dates, sales, logicalSize);
      if (mainMenuChoice == "Q")
      {
        goAgain = false;
        throw new Exception("Bye, hope to see you again.");
      }
      if (mainMenuChoice == "R")
      {
        while (true)
        {
          if (logicalSize == 0)
					  throw new Exception("No entries loaded. Please load a file into memory");
          DisplayAnalysisMenu();
          string analysisMenuChoice = Prompt("\nEnter an Analysis Menu Choice: ").ToUpper();
          if (analysisMenuChoice == "A")
            FindAverageOfValuesInMemory(sales, logicalSize);
          if (analysisMenuChoice == "H")
            FindHighestValueInMemory(sales, logicalSize);
          if (analysisMenuChoice == "L")
            FindLowestValueInMemory(sales, logicalSize);
          if (analysisMenuChoice == "G")
            GraphValuesInMemory(dates, sales, logicalSize);
          if (analysisMenuChoice == "R")
            throw new Exception("Returning to Main Menu");
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }


//Function to Display the MainMenu
void DisplayMainMenu()
{
	Console.WriteLine("\nMain Menu");
	Console.WriteLine("L) Load Values from File to Memory");
	Console.WriteLine("S) Save Values from Memory to File");
	Console.WriteLine("D) Display Values in Memory");
	Console.WriteLine("A) Add Value in Memory");
	Console.WriteLine("E) Edit Value in Memory");
	Console.WriteLine("R) Analysis Menu");
	Console.WriteLine("Q) Quit");
}


// Function to Display AnalysisMenu
void DisplayAnalysisMenu()
{
	Console.WriteLine("\nAnalysis Menu");
	Console.WriteLine("A) Find Average of Values in Memory");
	Console.WriteLine("H) Find Highest Value in Memory");
	Console.WriteLine("L) Find Lowest Value in Memory");
	Console.WriteLine("G) Graph Values in Memory");
	Console.WriteLine("R) Return to Main Menu");
}

//A helper functioin for loading the files
string GetFileName()
{
	string fileName = "";
	do
	{
		fileName = Prompt("Enter file name including .csv or .txt: ");
	} while (string.IsNullOrWhiteSpace(fileName));
	return fileName;
}


// Function to load the data from a file
int LoadFileValuesToMemory(string[] dates, double[] sales)
{
	string fileName = GetFileName();
	int logicalSize = 0;
	string filePath = $"./data/{fileName}";
	if (!File.Exists(filePath))
		throw new Exception($"The file {fileName} does not exist.");
	string[] csvFileInput = File.ReadAllLines(filePath);
	for(int i = 0; i < csvFileInput.Length; i++)
	{
		string[] items = csvFileInput[i].Split(',');
		
        if(i != 0)
		{
            dates[logicalSize] = items[0];
            sales[logicalSize] = double.Parse(items[1]);
            logicalSize++;
		}
	}
  Console.WriteLine($"Load complete. {fileName} has {logicalSize} data entries");
	return logicalSize;
}


// Function to display the function loaded
void DisplayMemoryValues(string[] dates, double[] sales, int logicalSize)
{
	if(logicalSize == 0)
		throw new Exception($"No Entries loaded. Please load a file to memory or add a value in memory");
	Console.WriteLine($"\nCurrent Loaded Entries: {logicalSize}");
	Console.WriteLine($"   Date     Value");
	for (int i = 0; i < logicalSize; i++)
		Console.WriteLine($"{dates[i]}   {sales[i]}");
}
// Finds the highest values in the sales array then return the index of the value in the array
double FindHighestValueInMemory(double[] sales, int logicalSize)
{
	
    int index = 0;
    double max = sales[0];
    for (int i = 0; i < logicalSize; i++)
    {
        if (sales[i] > max)
        {
            max = sales[i];
            index = i;
        }
    
    }
    Console.WriteLine($"The Highest value is {max:n2}");
    return max;
}


// finds the lowest value in the sales array
double FindLowestValueInMemory(double[] sales, int logicalSize)
{
	int index = 0;
    double min = sales[0];
    for (int i = 0; i < logicalSize; i++)
    {
        if (sales[i] < min)
        {
            min = sales[i];
            index = i;
        }
    }
    Console.WriteLine($"The lowest value is {min:n2}");
    return min;
}


// Calulates the Average sales of the month
double FindAverageOfValuesInMemory(double[] sales, int logicalSize)
{
	double sum = 0;
    for (int i = 0; i < logicalSize; i++)
        sum += sales[i];
    
    sum = sum/logicalSize;
    Console.WriteLine($"The sales average is {sum:n2}");
    return sum;

}

// Function to save the data into a file
void SaveMemoryValuesToFile(string[] dates, double[] sales, int logicalSize)
{
  string fileName = GetFileName();
  string filePath = $"./data/{fileName}";
  if (!File.Exists(filePath))
    throw new Exception($"The file {fileName} does not exist.");
  if (logicalSize == 0)
    throw new Exception($"There are no entries to save. Please add entries!");
  string[] csvLine = new string[physicalSize];
  csvLine[0] = ($"Dates,Sales");
  for (int i = 0; i < logicalSize; i++)
  {
    csvLine[i + 1] = $"{dates[i]},{sales[i].ToString()}";
  }
  File.WriteAllLines(filePath, csvLine);

}

// helper function for the AddMemoryValues
double PromptDoubleBetweenMinMax(string Prompt, double min, double max)
{
  bool inValidInput = true;
  double num = 0;
  while (inValidInput)
  {
    try
    {
      Console.Write($"{Prompt} between {min:n2} and {max:n2}: ");
      num = double.Parse(Console.ReadLine());
      if (num > max || num < min)
        throw new Exception($"InValid. Must be between {min} and {max}.");
      inValidInput = false;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }
  return num;
}


// Helper function for the AddMemoryValues to get the date
string promptDate(string prompt)
{
  bool inValidInput = true;
  DateTime date = DateTime.Today;
  Console.WriteLine(date);
  while (inValidInput)
  {
    try
    {
      Console.Write(prompt);
      date = DateTime.Parse(Console.ReadLine());
      Console.WriteLine(date);
      inValidInput = false;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }
  return date.ToString("MM-dd-yyyy");
}


// Function to add data to a file
int AddMemoryValues(string[] dates, double[] sales, int logicalSize)
{
	double sale = 0.0;
  string dateString = "";

  dateString = promptDate("Enter date format mm--dd-yyyy (eg 11-23-2023): ");
  bool found = false;
  for (int i = 0; i < logicalSize; i++)
    if (dates[i].Equals(dateString))
      found = true;
  if(found == true)
    throw new Exception($"{dateString} is already in memory. Edit entry instead.");
  sale = PromptDoubleBetweenMinMax($"Enter a double value", minValue, maxValue);
  dates[logicalSize] = dateString;
  sales[logicalSize] = sale;
  logicalSize++;
  return logicalSize;
}

// Function to Edit the data in a file
void EditMemoryValues(string[] dates, double[] sales, int logicalSize)
{
	double sale = 0.0;
  string dateString = "";
  int day = 0;

  if (logicalSize == 0)
  {
    throw new Exception($"There are no entries loaded. Please load a file or add a value in memory first");
  }
  dateString = promptDate("Enter the day in format mm-dd-yyyy (eg. 11-23-2024): ");
  bool found = false;
  for (int i = 0; i < logicalSize; i++)
  {
    if (dates[i].Equals(dateString))
    {
      found = true;
      day = i;
    }
  }
  if (found == false)
    throw new Exception($"{dateString} is not in memory. Add entry instead.");
  sale = PromptDoubleBetweenMinMax($"Enter a double value", minValue, maxValue);
  sales[day] = sale;
}

//Function to create the graph the data
void GraphValuesInMemory(string[] dates, double[] sales, int logicalSize)
{
	Console.WriteLine("Not Implemented Yet");
	//TODO: Replace this code with yours to implement this function.
}


//Prompt method
string Prompt(string promptString)
{
  string response = "";
  while(true)
  {
    try
    {
      Console.Write(promptString);
      response = Console.ReadLine().Trim();
      if(string.IsNullOrWhiteSpace(response))
        throw new Exception($"Empty input: Please enter something.");
      break;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
    
  }    
  return response;
}


//PromptDouble method 
double PromptDouble(string promptString)
{
    bool inValidInput = true;
    double input = 0;
    while(inValidInput)
    {
        try
        {

            Console.Write(promptString);
            input = double.Parse(Console.ReadLine());
            inValidInput = false;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"InValid Input: {ex.Message}");
        }
    }
    return input;
}