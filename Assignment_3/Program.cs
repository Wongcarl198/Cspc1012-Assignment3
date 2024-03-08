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



//Prompt method
string Prompt(string promptString)
{
  string response = "";
  Console.Write(promptString);
  response = Console.ReadLine();
  return response.ToUpper();
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