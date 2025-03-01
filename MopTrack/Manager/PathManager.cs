using System;

namespace MopTrack.Manager;

public class PathManager
{
    private static string dbName = "mt.db";
    public static string OnGetDbPath()
    {
        string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        dbPath = Path.Combine(dbPath, dbName);
        dbPath = Path.Combine($"Filename={dbPath}");
        return dbPath;
    }
}
