
namespace Task1 {
    class Program
    {
        static void Main(string[] args)
        {
            FilesWork.CreateFiles();
            Console.WriteLine("count of del raws:" + FilesWork.CombineFilesWithDeleted("abc"));
            ImportFileToDB.Import("file2.txt");
        }
    }

}