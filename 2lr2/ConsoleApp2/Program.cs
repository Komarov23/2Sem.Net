using System.Xml.Xsl;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Buffers;
using System.Diagnostics;

class Program
{
    static void Main(String[] args)
    {
        Console.WriteLine("Lib 1 (System.Xml.Xsl): \n");
        Lib1();

        Console.WriteLine("Lib 2 (System.Xml.XPath): \n");
        Lib2();

        Console.WriteLine("Lib 3 (System.Collections): \n");
        Lib3();

        Console.WriteLine("Lib 4 (System.Buffers): \n");
        Lib4();

        Console.WriteLine("Lib 5 (System.Diagnostics): \n");
        Lib5();
    }

    static void Lib1 ()
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Books.xml");

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("Books.xslt");

            StringWriter transformedOutput = new StringWriter();

            xslt.Transform(xmlDoc, null, transformedOutput);

            Console.WriteLine("Transformed Output:");
            Console.WriteLine(transformedOutput.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void Lib2 ()
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Books.xml");

            XPathNavigator navigator = xmlDoc.CreateNavigator();

            XPathNodeIterator books = navigator.Select("/Books/Book");
            while (books.MoveNext())
            {
                string title = books.Current.SelectSingleNode("Title").Value;
                string author = books.Current.SelectSingleNode("Author").Value;
                Console.WriteLine($"Title: {title}, Author: {author}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void Lib3 ()
    {
        ArrayList namesList = new ArrayList();

        namesList.Add("John");
        namesList.Add("Jane");
        namesList.Add("Bob");

        Console.WriteLine("Original List:");
        DisplayNames(namesList);

        namesList.Insert(1, "Alice");

        Console.WriteLine("\nList After Insertion:");
        DisplayNames(namesList);

        namesList.Remove("Jane");

        Console.WriteLine("\nList After Removal:");
        DisplayNames(namesList);
    }

    static void Lib4 ()
    {
        MemoryPool<byte> memoryPool = MemoryPool<byte>.Shared;

        IMemoryOwner<byte> memoryOwner = memoryPool.Rent(10);

        try
        {
            Memory<byte> memory = memoryOwner.Memory;

            for (int i = 0; i < memory.Length; i++)
            {
                memory.Span[i] = (byte)(i + 65);
            }

            Console.WriteLine("Memory Content:");
            foreach (byte value in memory.Span)
            {
                Console.Write((char)value + " ");
            }
            Console.WriteLine();
        }
        finally
        {
            memoryOwner.Dispose();
        }
    }

    static void Lib5 ()
    {
        string processPath = "notepad.exe";
        ProcessStartInfo startInfo = new ProcessStartInfo(processPath);
        Process myProcess = Process.Start(startInfo);

        if (myProcess != null)
        {
            Console.WriteLine($"Process ID: {myProcess.Id}");
            Console.WriteLine($"Process Name: {myProcess.ProcessName}");
            Console.WriteLine($"Start Time: {myProcess.StartTime}");

            Console.WriteLine("\nPress Enter to close the process...");
            Console.ReadLine();

            myProcess.CloseMainWindow();
            myProcess.WaitForExit();
        }
        else
        {
            Console.WriteLine("Failed to start the process.");
        }
    }

    static void DisplayNames(ArrayList names)
    {
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
    }
}