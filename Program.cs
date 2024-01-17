using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        
        var startTime = DateTime.Now;
//
        var sumTask = CalculateSumAsync(1, 3000);

        var wordCountTask1 = CountWordsAsync("Eval_file1.txt");
        var wordCountTask2 = CountWordsAsync("Eval_file2.txt");
        var loremIpsumCountTask1 = CountOccurrencesAsync("Eval_file1.txt", "Lorem ipsum");
        var loremIpsumCountTask2 = CountOccurrencesAsync("Eval_file2.txt", "Lorem ipsum");

     
        var sum = await sumTask;
        Console.WriteLine($"Somme des nombres de 1 à 3000 : {sum}");


        var wordCount1 = await wordCountTask1;
        var wordCount2 = await wordCountTask2;
        var loremIpsumCount1 = await loremIpsumCountTask1;
        var loremIpsumCount2 = await loremIpsumCountTask2;

        Console.WriteLine($"Nombre de mots dans Eval_file1.txt : {wordCount1}");
        Console.WriteLine($"Nombre de mots dans Eval_file2.txt : {wordCount2}");
        Console.WriteLine($"Occurrences de 'Lorem ipsum' dans Eval_file1.txt : {loremIpsumCount1}");
        Console.WriteLine($"Occurrences de 'Lorem ipsum' dans Eval_file2.txt : {loremIpsumCount2}");

     
        long finalSum = sum + wordCount1 + wordCount2 + loremIpsumCount1 + loremIpsumCount2;
        Console.WriteLine($"Somme des 5 nombres résultants : {finalSum}");

       
        var endTime = DateTime.Now;
        var totalTime = endTime - startTime;
        Console.WriteLine($"Temps de traitement total : {totalTime.TotalMilliseconds} ms");
    }

    static async Task<long> CalculateSumAsync(int start, int end)
    {
        return await Task.Run(() =>
        {
            long sum = 0;
            for (int i = start; i <= end; i++)
            {
                sum += i;
            }
            return sum;
        });
    }

    static async Task<int> CountWordsAsync(string filePath)
    {
        var text = await File.ReadAllTextAsync(filePath);
        var words = text.Split(' ');
        return words.Length;
    }

    static async Task<int> CountOccurrencesAsync(string filePath, string searchTerm)
    {
        var text = await File.ReadAllTextAsync(filePath);
        int count = 0;
        int index = 0;

        while ((index = text.IndexOf(searchTerm, index)) != -1)
        {
            index += searchTerm.Length;
            count++;
        }

        return count;
    }
}
