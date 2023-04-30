using System.Text.RegularExpressions;

namespace BlogPlatform.API.Helpers;

public class GenerateBlogPostSlug
{
    public static string GenerateSlug(string phrase) 
    { 
        string str = phrase.ToLower(); 
        // invalid chars           
        str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); 
        // convert multiple spaces into one space   
        str = Regex.Replace(str, @"\s+", " ").Trim(); 
        // cut and trim 
        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();   
        str = Regex.Replace(str, @"\s", "-"); // hyphens   
        return str; 
    }
}