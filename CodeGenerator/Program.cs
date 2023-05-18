using System.Text.RegularExpressions;


Console.WriteLine("Hello, World!");

string contents = "";

var printDomain = true;
var printApplication = true;
var printInfrastructure = true;
var printFrontEnd = false;

if (printDomain)
{
    string path = @"C:\development\ProjectManager\Services\Estimator\Estimator.Domain\";
    string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories)
        .Where(file => !file.Contains("\\obj\\") && !file.Contains("\\bin\\"))
        .ToArray();

    foreach (string file in files)
    {
        var temp = File.ReadAllText(file);
        temp = temp.Replace("\t", " ");
        while (temp.IndexOf("  ") >= 0)
        {
            temp = temp.Replace("  ", " ");
        }
        contents += "\n" + temp + "\n";

    }
    path = @"C:\development\ProjectManager\";
    string outputPath = path + "domain.txt";
    File.WriteAllText(outputPath, contents);

}
if (printApplication)
{
    contents = "";
    string path = @"C:\development\ProjectManager\Services\Estimator\Estimator.Api\Application";
    string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories)
        .Where(file => !file.Contains("\\obj\\") && !file.Contains("\\bin\\"))
        .ToArray();

    foreach (string file in files)
    {
        var temp = File.ReadAllText(file);
        temp = temp.Replace("\t", " ");
        while (temp.IndexOf("  ") >= 0)
        {
            temp = temp.Replace("  ", " ");
        }
        contents += "\n" + MoveBrackets(RemoveEmptyLines(RemoveComments(temp))) + "\n";
    }

    path = @"C:\development\ProjectManager\";
    string outputPath = path + "api-application.txt";
    File.WriteAllText(outputPath, contents);

    // Now do the rest of the API

    contents = "";
    path = @"C:\development\ProjectManager\Services\Estimator\Estimator.Api";
    files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories)
        .Where(file => !file.Contains("\\obj\\") && !file.Contains("\\bin\\") && !file.Contains("\\Application\\"))
        .ToArray();

    foreach (string file in files)
    {
        var temp = File.ReadAllText(file);
        temp = temp.Replace("\t", " ");
        while (temp.IndexOf("  ") >= 0)
        {
            temp = temp.Replace("  ", " ");
        }
        contents += "\n" + MoveBrackets(RemoveEmptyLines(RemoveComments(temp))) + "\n";
    }

    path = @"C:\development\ProjectManager\";
    outputPath = path + "api-other.txt";
    File.WriteAllText(outputPath, contents);





}
if (printInfrastructure)
{
    contents = "";
    string path = @"C:\development\ProjectManager\Services\Estimator\Estimator.Infrastructure\";
    string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories)
        .Where(file => !file.Contains("\\obj\\") && !file.Contains("\\bin\\"))
        .ToArray();

    foreach (string file in files)
    {
        var temp = File.ReadAllText(file);
        temp = temp.Replace("\t", " ");
        while (temp.IndexOf("  ") >= 0)
        {
            temp = temp.Replace("  ", " ");
        }
        contents += "\n" + temp + "\n";
    }
    contents = RemoveComments(contents);

    path = @"C:\development\ProjectManager\";
    string outputPath = path + "infrastructure.txt";
    File.WriteAllText(outputPath, contents);

}
if (printFrontEnd)
{
    contents = "";
    string path = @"C:\development\ProjectManager\FrontEnd\";
    string[] files = Directory.GetFiles(path, "*.ts", SearchOption.AllDirectories)
        .Where(file => !file.Contains("\\node_modules\\") && !file.Contains("\\dist\\"))
        .ToArray();
    foreach (string file in files)
    {
        contents += "\n" + File.ReadAllText(file) + "\n";
    }
    path = @"C:\development\ProjectManager\";
    string outputPath = path + "frontend.txt";
    File.WriteAllText(outputPath, contents);
}
Console.WriteLine("OK we're done!");





static string RemoveComments(string sourceCode)
{
    var blockComments = @"/\*(.*?)\*/";
    var lineComments = @"//(.*?)\r?\n";
    var strings = @"""((\\[^\n]|[^""\n])*)""";
    var verbatimStrings = @"@(""[^""]*"")+";

    string noComments = Regex.Replace(sourceCode,
        blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
        me => {
            if (me.Value.StartsWith("/*") && me.Value.EndsWith("*/"))
                return "";
            if (me.Value.StartsWith("//"))
                return Environment.NewLine;
            return me.Value;
        },
        RegexOptions.Singleline);

    return noComments;
}


static string RemoveEmptyLines(string sourceCode)
{
    return Regex.Replace(sourceCode, @"^\s*$\n|\r", "", RegexOptions.Multiline);
}

static string MoveBrackets(string sourceCode)
{
    // Move opening brackets to the end of the previous line
    string noOpeningBracketLines = Regex.Replace(sourceCode, @"(\s*)\{\s*\n", " {");

    // Move closing brackets to the start of the next line
    string noClosingBracketLines = Regex.Replace(noOpeningBracketLines, @"\s*\}\s*\n", "} ");

    return noClosingBracketLines;
}