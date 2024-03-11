﻿// <summary>
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


double[] sales = new double[logicalSize];
string[] dates = new string[logicalSize];

//Prompt method
string Prompt(string promptString)
{
  string response = "";
  Console.Write(promptString);
  response = Console.ReadLine();
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


// Displays the Program intro.
void DisplayProgramIntro()
{
	Console.WriteLine("========================================");
	Console.WriteLine("=                                      =");
	Console.WriteLine("=            Monthly  Sales            =");
	Console.WriteLine("=                                      =");
	Console.WriteLine("========================================");
	Console.WriteLine();
}


void displayMainMenu()
{
    Console.WriteLine(" [N] New Daily Sales Entry");
    Console.WriteLine(" [S] Save Entries to File");
    Console.WriteLine(" [E] Edit Sales Entries");
    Console.WriteLine(" [L] Load Sales File");
    Console.WriteLine(" [V] View Entered/Loaded Sales");
    Console.WriteLine(" [M] Montonly Statistics");
    Console.WriteLine(" [D] Display Main Menu");
    Console.WriteLine(" [Q] Quit Program");  
}

void displayAnalysisMenu()
{
    Console.WriteLine(" [A] Average Sales");
    Console.WriteLine(" [H] Highest Sales");
    Console.WriteLine(" [L] Lowest Sales");
    Console.WriteLine(" [G] Graph Sales");
    Console.WriteLine(" [R] Return to MAIN MENU");
}


// Finds the highest values in the sales array then return the index of the value in the array
int highestSales(double[] sales, int countOfEntries)
{   
    int index = 0;
    double max = sales[0];
    for (int i = 0; i < countOfEntries; i++)
    {
        if (sales[i] > max)
            index = i;
    }
    
    return index;
}


// Finds the lowest value in the sales array then return the index of the value in the array
int lowestSales(double[] sales, int countOfEntries)
{   
    int index = 0;
    double min = sales[0];
    for (int i = 0; i < countOfEntries; i++)
    {
        if (sales[i] < min)
            index = i;
    }
    
    return index;
}


// Calulates the Average sales of the month
double meanAverageSales(double[] sales, int countOfEntries)
{
    double sum = 0;
    for (int i = 0; i < countOfEntries; i++)
        sum += sales[i];
    
    return sum/countOfEntries;

}



// allow user to input new sales
int enterSales(double[] sales, string[] dates)                      //ERROR: Sales are not in two decimal place
{   int day = 1;
    string month = Prompt($"Enter the month (e.g. 03): ");
    string year = Prompt($"Enter the year (yyyy): ");
    double quit = -1.0;                                             
    int i = 0;
    while (contin)
    {   
        string formatedDay = day.ToString("00");
        dates[i] = $"{month}-{formatedDay}-{year}";
        
        sales[i] = PromptDouble($"Enter day {day} sales: ");
        Console.WriteLine($"Hint: Enter -1 to cancel and exit.");
        if (sales[i] == quit)
        {
            contin = false;
        }
        else
        {
            
            i += 1;
            day +=1;
        }
        
    }

    int logicalSize = i;

    return logicalSize;
}

// Method to get the file name
string GetFileName()
{
	string fileName = "";
	do
	{
		fileName = Prompt("Enter file name including .csv or .txt: ");
	} while (string.IsNullOrWhiteSpace(fileName));
	return fileName;
}


// allow the user to load the file data
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
