using System;
using System.IO;
using System.Diagnostics;

namespace ShadeExe
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                string sourceFile = args[0];

                if(File.Exists(sourceFile))
                {
                    // creating source file in c++ to compile
                    Console.WriteLine("creating source file..");
                    string code = "#include <stdio.h> \n" +
                        "int main() \n" +
                        "    { \n" +
                        "        system(\"" + File.ReadAllText(sourceFile) + "\"); \n" +
                        "        return 0; \n" +
                        "    } \n";
                    string csourceFile = Path.GetFileName(sourceFile) + ".c";
                    File.WriteAllText(csourceFile, code);

                    // run compiler
                    Console.WriteLine("compile file..");
                    Process process = new Process();
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = "tcc/tcc.exe",
                        Arguments = csourceFile,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                    };
                    process.Start();
                    process.WaitForExit();
                    File.Delete(csourceFile);
                    Console.WriteLine("done {0} is ready to use", Path.GetFileName(sourceFile) + ".exe");
                }
                else
                {
                    Console.WriteLine("file {0} does not exits.", sourceFile);
                }
            }
        }
    }
}
