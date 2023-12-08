using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        try
        {
            string responseBody = await FetchDataFromUrl("https://raw.githubusercontent.com/microsoft/CopilotAdventures/main/Data/scrolls.txt");
            var matches = ExtractTextBetweenSpecialCharacters(responseBody);
            PrintMatches(matches);
        }
        catch(HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ",e.Message);
        }
    }

    static async Task<string> FetchDataFromUrl(string url)
    {
        return await client.GetStringAsync(url);
    }

    // This method extracts the text enclosed within {* and *} from the provided text
    static MatchCollection ExtractTextBetweenSpecialCharacters(string text)
    {
        // The regular expression pattern @"\{\*(.*?)\*\}" is used to match any text enclosed within {* and *}
        // RegexOptions.Singleline ensures that the . character in the pattern matches every character (including new lines)
        return Regex.Matches(text, @"\{\*(.*?)\*\}", RegexOptions.Singleline);
    }

    // This method prints the extracted matches to the console
    static void PrintMatches(MatchCollection matches)
    {
        try
        {

            // Check if matches is null
            if(matches == null)
            {
                throw new ArgumentNullException(nameof(matches), "The matches collection cannot be null.");
            }

            // Iterate over each match found
            foreach (Match match in matches)
            {
                // Check if match is null
                if(match == null)
                {
                    throw new ArgumentNullException(nameof(match), "A match in the collection cannot be null.");
                }

                // match.Groups[1].Value gets the captured substring from the input string
                // This is then printed to the console
                Console.WriteLine(match.Groups[1].Value);
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while printing matches: " + e.Message);
        }
    }
}