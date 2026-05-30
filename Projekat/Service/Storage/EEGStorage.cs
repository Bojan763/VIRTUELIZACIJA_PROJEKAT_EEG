using Common;
using System.IO;
using System;

public class EEGStorage : IDisposable
{
    private string folder;
    private StreamWriter sessionWriter;

    public void Start(EegMeta meta)
    {
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        folder = Path.Combine("Data", meta.ParticipantId, date);

        Directory.CreateDirectory(folder);

        string sessionPath = Path.Combine(folder, "session.csv");

        sessionWriter = new StreamWriter(sessionPath, true);
    }

    public void SaveSample(EegSample s)
    {
        sessionWriter.WriteLine(
            $"{s.RowIndex},{s.TimeStamp},{s.AF3},{s.T7},{s.Pz}");
    }

    public void Dispose()
    {
        sessionWriter?.Flush();
        sessionWriter?.Dispose();
    }
}