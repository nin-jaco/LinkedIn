using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelVsNormal
{
    class Program
    {


        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(path + @"\Pictures", "*.jpg");
            var alteredPath = path + @"\alteredPath";
            Directory.CreateDirectory(alteredPath);
            
            ParallelExecution(files, alteredPath);

            NormalExecution(files, alteredPath);
        }


        private static void ParallelExecution(string[] files, string alteredPath)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Parallel.ForEach(files, currentFile =>
            {
                var file = Path.GetFileName(currentFile);
                using (var fileBitmap = new Bitmap(currentFile))
                {
                    fileBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                    fileBitmap.Save(Path.Combine(alteredPath, file));
                    Console.WriteLine($@"Thread {Thread.CurrentThread.ManagedThreadId}");
                }
            });
            Console.WriteLine($@"Parallel Execution Elapsed time: {stopWatch.ElapsedMilliseconds}");
            stopWatch.Stop();
        }

        private static void NormalExecution(string[] files, string alteredPath)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var currentFile in files)
            {
                var file = Path.GetFileName(currentFile);
                using (var fileBitmap = new Bitmap(currentFile))
                {
                    fileBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                    fileBitmap.Save(Path.Combine(alteredPath, file));
                    Console.WriteLine($@"Thread {Thread.CurrentThread.ManagedThreadId}");
                } 
            }
            Console.WriteLine($@"Normal Execution Elapsed time: {stopWatch.ElapsedMilliseconds}");
            stopWatch.Stop();
        }
    }
}
