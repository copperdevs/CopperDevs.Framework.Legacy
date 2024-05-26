namespace CopperDevs.Core;

public static class Log
{
    public static void Info(object message) => CopperLogger.LogInfo(message);

    public static void Warning(object message) => CopperLogger.LogWarning(message);

    public static void Error(object message) => CopperLogger.LogError(message);

    public static void Error(Exception e) => CopperLogger.LogError($"[{e.GetType()}] {e.Message} \n {e.StackTrace}");

    public static void Assert(bool condition, object message)
    {
        if (condition) 
            return;
        
        CopperLogger.LogError(message);
    }
}

public static class CopperLogger
{
    public delegate void BaseLog(object message);

    private static BaseLog? info;
    private static BaseLog? warning;
    private static BaseLog? error;

    private static bool initialized;

    private static bool includeTimestamps;


    /// <summary>
    /// Initialize logger with internal log functions
    /// </summary>
    /// <param name="timestamps">Log messages with timestamp</param>
    public static void Initialize(bool timestamps = true)
    {
        Initialize(null!, null!, null!, timestamps);
    }

    /// <summary>
    /// Initialize logger with custom log functions
    /// </summary>
    /// <param name="infoLog"></param>
    /// <param name="warningLog"></param>
    /// <param name="errorLog"></param>
    /// <param name="timestamps">Log messages with timestamp</param>
    public static void Initialize(BaseLog infoLog, BaseLog warningLog, BaseLog errorLog, bool timestamps = true)
    {
        if (initialized)
            return;
        initialized = true;

        info = infoLog;
        warning = warningLog;
        error = errorLog;

        includeTimestamps = timestamps;
    }

    public static void LogInfo(object message)
    {
        if (info is not null)
            info.Invoke(message);
        else
            InternalLog("INFO", message, ConsoleColor.DarkGray);
    }

    public static void LogWarning(object message)
    {
        if (warning is not null)
            warning.Invoke(message);
        else
            InternalLog("WARN", message, ConsoleColor.DarkYellow);
    }

    public static void LogError(object message)
    {
        if (error is not null)
            error.Invoke(message);
        else
            InternalLog("ERROR", message, ConsoleColor.DarkRed);
    }
    
    private static void InternalLog(string prefix, object message, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        var timeStampLog = includeTimestamps ? $"[{DateTime.Now:HH:mm:ss}]" : "";
        var prefixLog = $"[{prefix}]";

        var logString = $"{prefixLog} {timeStampLog} {message}";
        
        Console.WriteLine(logString);
        Console.ResetColor();
    }
}