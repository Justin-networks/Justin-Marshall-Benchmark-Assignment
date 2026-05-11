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
        {//allows user to pick file filtered by json and desearializes
            OpenFileDialog openDialog = new OpenFileDialog();
            //narrow files shown to json only
            openDialog.Filter = "JSON files (*.json)|*.json";
            openDialog.Title = "Import Movies";
            //
            if (openDialog.ShowDialog() == true)
            {
                string path = openDialog.FileName;
                string json = File.ReadAllText(path);
                //desearialize searelized json array into <List<Movie>>
                return JsonSerializer.Deserialize<List<Movie>>(json);
            }
            //returns null if user cancels
            return null;
        }
        public static void ExportList(List<Movie> movies)
        {//alows user to pick filepath and searilizes current movie list to json
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JSON files (*.json)|*.json";
            saveDialog.Title = "Export Movies";
            if (saveDialog.ShowDialog() == true)
            {
                string path = saveDialog.FileName;
                //searilize <List<Movie>> to json string
                string json = JsonSerializer.Serialize(movies);
                File.WriteAllText(path, json);
            }
        }
    }
}
