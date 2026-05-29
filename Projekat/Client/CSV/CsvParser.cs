using Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Client.CSV
{
    public class CsvParser
    {
        private readonly string logPath = Path.Combine("Logs", "client_errors.txt");

        public List<EegSample> ParseFile(string path)
        {
            List<EegSample> samples = new List<EegSample>();

            Directory.CreateDirectory("Logs");

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        sr.ReadLine();
                        int rowIndex = 0;
                        while(!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            try
                            {
                                EegSample sample = ParseLine(line, rowIndex);
                                samples.Add(sample);
                                rowIndex++;
                            }
                            catch(Exception ex)
                            {
                                LogBadRow(line, ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return samples;
        }
        private EegSample ParseLine(string line, int rowIndex)
        {
            
            string[] p = line.Split(',');

            return new EegSample
            {
                TimeStamp = DateTime.Parse(p[0], CultureInfo.InvariantCulture),

                AF3 = double.Parse(p[1], CultureInfo.InvariantCulture),

                T7 = double.Parse(p[2], CultureInfo.InvariantCulture),

                Pz = double.Parse(p[3], CultureInfo.InvariantCulture),

                T8 = double.Parse(p[4], CultureInfo.InvariantCulture),

                AF4 = double.Parse(p[5], CultureInfo.InvariantCulture),

                Attention = double.Parse(p[6], CultureInfo.InvariantCulture),

                Engagement = double.Parse(p[7], CultureInfo.InvariantCulture),

                Excitement = double.Parse(p[8], CultureInfo.InvariantCulture),

                Interest = double.Parse(p[9], CultureInfo.InvariantCulture),

                Relaxation = double.Parse(p[10], CultureInfo.InvariantCulture),

                Stress = double.Parse(p[11], CultureInfo.InvariantCulture),

                Battery = int.Parse(p[12]),

                ContactQuality = int.Parse(p[13]),

                SlideIndex = int.Parse(p[14]),

                SetIndex = int.Parse(p[15]),

                RowIndex = rowIndex
            };
        }
        private void LogBadRow(string line, string error)
        {
            using(StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine($"[{DateTime.Now}] " + $"ERROR: {error}");
                sw.WriteLine(line);
                sw.WriteLine();
            }
        }


    }
}
