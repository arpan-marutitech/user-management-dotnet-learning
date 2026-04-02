namespace DesignPatterns.Creational.AbstractFactory;

// ── Windows family ──────────────────────────────────────────
public class WindowsButton : IButton
{
    public string Render() => "[Windows Button]";
}

public class WindowsCheckbox : ICheckbox
{
    public string Render() => "[Windows Checkbox]";
}

public class WindowsFactory : IUIFactory
{
    public IButton   CreateButton()   => new WindowsButton();
    public ICheckbox CreateCheckbox() => new WindowsCheckbox();
}

// ── Mac family ───────────────────────────────────────────────
public class MacButton : IButton
{
    public string Render() => "[Mac Button]";
}

public class MacCheckbox : ICheckbox
{
    public string Render() => "[Mac Checkbox]";
}

public class MacFactory : IUIFactory
{
    public IButton   CreateButton()   => new MacButton();
    public ICheckbox CreateCheckbox() => new MacCheckbox();
}
