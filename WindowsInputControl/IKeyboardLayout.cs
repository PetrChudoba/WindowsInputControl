namespace WindowsInputControl.Hooks
{
    public interface IKeyboardLayout
    {
        string Identifier { get; }

        string Name { get;  }

    
        ushort GetVirtualKey(ushort scanCode);
    }
}