using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Justin_Marshall___Benchmark_Assignment
{
    public static class FileHandler
    {
        public static List<Movie> ImportList()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            //narrow files shown to json only
            openDialog.Filter = "JSON files (*.json)|*.json";
            openDialog.Title = "Import Movies";
            //
            if (openDialog.ShowDialog() == true)
            {
                string path = openDialog.FileName;
                string json = File.ReadAllText(path);
                //desearialize searelized json file to dtg importable format
                return JsonSerializer.Deserialize<List<Movie>>(json);
            }
            //returns null if user cancels
            return null;
        }
        public static void ExportList(List<Movie> movies)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JSON files (*.json)|*.json";
            saveDialog.Title = "Export Movies";
            if (saveDialog.ShowDialog() == true)
            {
                string path = saveDialog.FileName;
                string json = JsonSerializer.Serialize(movies);
                File.WriteAllText(path, json);
            }
        }
    }
}
