using System;
using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace ClassTechMenager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program Start.");
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            Console.WriteLine("Current date and time: " + formattedDateTime);
            Console.WriteLine("Write class name.");
            try
            {
                string className = Console.ReadLine().ToString();
                Console.WriteLine("Selected class name: " + className + ".");
                string folderDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                folderDirectory = Directory.GetParent(folderDirectory).Parent.Parent.FullName;
                Console.WriteLine("Directory: " + folderDirectory);
                folderDirectory += "\\Data";
                if (!Directory.Exists(folderDirectory)) Directory.CreateDirectory(folderDirectory);
                string filePath = folderDirectory + "\\" + className + ".txt";
                Console.WriteLine("Write Lesson Number: ");
                int lessonNumber = int.Parse(Console.ReadLine() + "");


                Console.WriteLine("Write number of work station: ");
                int numberOfWorkStation = int.Parse(Console.ReadLine() + "");
                Console.WriteLine("!(1) Number of work station: " + numberOfWorkStation);
                while (numberOfWorkStation <= 0)
                {
                    Console.WriteLine("Write number of work station.");
                    numberOfWorkStation = int.Parse(Console.ReadLine() + "");
                    Console.WriteLine("Number of work station: " + numberOfWorkStation);
                }
                // IS All working 
                Dictionary<int, string> notWorkingWorkStation = new();
                //bool[] workingWorkStation = new bool[numberOfWorkStation];
                int isWorking;

                Console.WriteLine("!(2) Is any workstation not OK? If not working write 0. If all working 1");

                int isAllWorkStation = int.Parse(Console.ReadLine());

                if (isAllWorkStation == 0)
                {
                    while (true)
                    {
                        Console.WriteLine("Select work station. (End on -1) ");
                        int idOfNotWorkingStation = int.Parse(Console.ReadLine());
                        if (idOfNotWorkingStation - 1 > numberOfWorkStation || idOfNotWorkingStation < 0) break;
                        Console.WriteLine("What is not working: ");
                        string whatIsNotWorking = Console.ReadLine();
                        notWorkingWorkStation.Add(idOfNotWorkingStation, whatIsNotWorking);
                        Console.WriteLine();
                    }
                }
                ObjectOfDataToSave objectOfDataToSave = new();
                String objectSerializeAsString = objectOfDataToSave.SerializeData(currentDateTime, className,
                    lessonNumber, notWorkingWorkStation);
                Console.WriteLine("objectSerializeAsString: " + objectSerializeAsString);
                Console.WriteLine("path: " + filePath);

                if (!File.Exists(filePath))
                {
                    using (new FileStream(filePath, FileMode.Create)) { }
                }
                
                using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var writer = new StreamWriter(fileStream))
                    {
                        writer.WriteLine(objectSerializeAsString);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            
        }
    }
}
