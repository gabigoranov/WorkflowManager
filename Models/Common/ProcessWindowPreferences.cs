namespace WorkflowManager.Models.Common;

public class ProcessWindowPreferences
{
    public bool IsMaximized { get; set; } = false;

    public string Monitor { get; set; } = "1";

    public int CoordX { get; set; } = 100;
    public int CoordY { get; set; } = 100;

    public int Width { get; set; } = 1280;
    public int Height { get; set; } = 900;
}