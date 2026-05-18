using Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Client.CSV
{
    public class EEGCsvLoader
    {
        public List<(string participantId, string path)> FindFiles(string root)
        {
            List<(string participantId, string path)>files = new List<(string participantId, string path)>();

            string[] csvFiles = Directory.GetFiles(root, "subject_*_results.csv", SearchOption.AllDirectories);
            
            foreach (string file in csvFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                string[] parts = fileName.Split('_');

                string participantId = parts[1];
                files.Add((participantId, file));
            }
            files = files.OrderBy(x => int.Parse(x.participantId)).ToList();
            return files;
        }
    }
}
