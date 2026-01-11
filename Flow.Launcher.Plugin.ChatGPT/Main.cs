using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Flow.Launcher.Plugin.ChatGPT.ViewModels;
using Flow.Launcher.Plugin.ChatGPT.Views;

namespace Flow.Launcher.Plugin.ChatGPT;

public class ChatGPT : IPlugin, IPluginI18n, ISettingProvider, IContextMenu
{
    private PluginInitContext _context;
    
    private Settings _settings;
    
    private SettingsViewModel _viewModel;
    
    private static readonly List<Result> EmptyResults = new();

    private readonly string _chatGptUrl = "https://chatgpt.com/?prompt=";

    public void Init(PluginInitContext context)
    {
        _context = context;
        _settings = context.API.LoadSettingJsonStorage<Settings>();
        _viewModel = new SettingsViewModel(_settings);
    }

    public List<Result> Query(Query query)
    {
        if (string.IsNullOrWhiteSpace(query.Search))
        {
            return EmptyResults;
        }
        
        var postArgs = "";
        if (_settings.TemporaryChat)
        {
            postArgs = "&temporary-chat=true";
        }
        
        return new List<Result>
        {
            new ()
            {
                Title = query.FirstSearch.ToLower(),
                IcoPath = "Images/app.ico",
                Action = _ =>
                {
                    _context.API.OpenUrl($"{_chatGptUrl}{Uri.EscapeDataString(query.Search)}{postArgs}");
                    return true;
                }
            }
        };
    }

    public string GetTranslatedPluginTitle()
    {
        return _context.API.GetTranslation("plugin_chatgpt_plugin_name");
    }

    public string GetTranslatedPluginDescription()
    {
        return _context.API.GetTranslation("plugin_chatgpt_plugin_description");
    }

    public Control CreateSettingPanel()
    {
        return new SettingsControl(_viewModel);
    }

    public List<Result> LoadContextMenus(Result selectedResult)
    {
        var results = new List<Result>
        {
            Capacity = 0
        };
        return results;
    }
}