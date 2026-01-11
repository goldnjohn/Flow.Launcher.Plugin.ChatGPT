using System.Windows.Controls;
using Flow.Launcher.Plugin.ChatGPT.ViewModels;

namespace Flow.Launcher.Plugin.ChatGPT.Views;

public partial class SettingsControl : UserControl
{
    public SettingsControl(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}