using Shared.Interfaces;
using System.Diagnostics;

namespace Shared.Services;

public class FileService : IFileService
{
    /// <summary>
    /// saves provided content to a file specified on given filepath
    /// </summary>
    /// <param name="filePath">the path to the file where content will be saved</param>
    /// <param name="content">the data to be saved in the file</param>
    /// <returns>returns true is content successfully saved to file, else false</returns>
    public string GetContentFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
            return null!;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    /// <summary>
    /// gets the content from file based on filepath
    /// </summary>
    /// <param name="filePath">the path to the file that will get the data</param>
    /// <returns>returns the content from the file if it exists, else null</returns>
    public bool SaveToFile(string filePath, string content)
    {
        try
        {
            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine(content);
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }
}
