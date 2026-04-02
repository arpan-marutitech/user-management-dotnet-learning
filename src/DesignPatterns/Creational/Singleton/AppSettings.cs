namespace DesignPatterns.Creational.Singleton;

// Singleton – only one instance is ever created
public sealed class AppSettings
{
    // The single instance, created lazily on first access
    private static readonly Lazy<AppSettings> _instance =
        new(() => new AppSettings());

    // Hide the constructor so callers cannot use 'new'
    private AppSettings()
    {
        AppName    = "DesignPatterns Demo";
        MaxRetries = 3;
    }

    public static AppSettings Instance => _instance.Value;

    public string AppName    { get; }
    public int    MaxRetries { get; set; }
}
