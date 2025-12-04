using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace WorkflowManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    public void DragWindow(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }

}