using System;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.FluentIcons;

namespace CurtainLauncher.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    /*
    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "Home"),
        new ListItemTemplate(typeof(InstancesPageViewModel), "Instances"),
        new ListItemTemplate(typeof(ConsolePageViewModel), "Console"),
        new ListItemTemplate(typeof(SettingsPageViewModel), "Settings"),
        new ListItemTemplate(typeof(HelpPageViewModel), "Help"),
        new ListItemTemplate(typeof(AboutPageViewModel), "About")
    };

    [RelayCommand]
    public void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
    */

    [ObservableProperty] 
    private ViewModelBase _currentPage = new HomePageViewModel();

    [ObservableProperty] 
    private SidebarItemListTemplate? _selectedPage;
    
    partial void OnSelectedPageChanged(SidebarItemListTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ViewModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    public ObservableCollection<SidebarItemListTemplate> Pages { get; } = new()
    {
        new SidebarItemListTemplate(typeof(HomePageViewModel), new StreamGeometry())
    };
}

public class SidebarItemListTemplate(Type viewModelType, StreamGeometry icon)
{
    public Type ViewModelType { get; } = viewModelType;
    public StreamGeometry Icon { get; } = icon;
    public string Label { get; } = viewModelType.Name.Replace("PageViewModel", "");
}
