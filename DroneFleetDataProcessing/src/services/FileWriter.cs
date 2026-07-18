using System;
using System.Collections.Generic;
using System.Text;
using DroneFleetDataProcessing.src.exceptions;

namespace DroneFleetDataProcessing.src.services
{
    class FileWriter
    {
        public void Write(string filename, string context)
        {
            if (String.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException();
            }
            if (String.IsNullOrWhiteSpace(filename))
            {
                throw new InvalidPathException();
            }

            string filepath = GetOutputPath(filename);
            File.WriteAllText(filepath, context);
        }
        private string GetOutputPath(string filename)
        {
            string outputDirectory = Path.Combine(
                AppContext.BaseDirectory,
                "output");

            Directory.CreateDirectory(outputDirectory);

            string outputPath = Path.Combine(
            outputDirectory,
            filename);

            return outputPath;
        }
    }
}
