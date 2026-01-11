using System;
using System.Collections.Generic;

namespace Flow.Launcher.Plugin.ChatGPT;

public class ChatGPT : IPlugin, IPluginI18n, IResultUpdated
{
    private PluginInitContext _context;
    
    private readonly string _chatGptUrl = "https://chatgpt.com/?prompt=";

    public void Init(PluginInitContext context)
    {
        _context = context;
    }

    public List<Result> Query(Query query)
    {
        if (string.IsNullOrWhiteSpace(query.Search) || string.IsNullOrWhiteSpace(query.SecondToEndSearch))
            return _GetDefaultHotKeys();

        var postArgs = query.FirstSearch.ToLower() switch
        {
            Settings.TemporaryCommand =>  "&temporary-chat=true",
            _ => ""
        };
        
        return new List<Result>()
        {
            new ()
            {
                Title = query.FirstSearch.ToLower(),
                IcoPath = "Images/app.ico",
                Action = _ =>
                {
                    _context.API.OpenUrl($"{_chatGptUrl}{Uri.EscapeDataString(query.SecondToEndSearch)}{postArgs}");
                    return false;
                }
            }
        };
    }

    private List<Result> _GetDefaultHotKeys()
    {
        return new List<Result>()
        {
            new ()
            {
                Title = Settings.NormalCommand,
                IcoPath = "Images/app.ico",
                AutoCompleteText = $"{_context.CurrentPluginMetadata.ActionKeyword} {Settings.NormalCommand} ",
                Action = _ =>
                {
                    _context.API.ChangeQuery(
                        $"{_context.CurrentPluginMetadata.ActionKeyword} {Settings.NormalCommand} ");
                    return false;
                }
            },
            new ()
            {
                Title = Settings.TemporaryCommand,
                SubTitle = _context.API.GetTranslation("plugin_chatgpt_temporary_chat"),
                IcoPath = "Images/app.ico",
                AutoCompleteText = $"{_context.CurrentPluginMetadata.ActionKeyword} {Settings.TemporaryCommand} ",
                Action = _ =>
                {
                    _context.API.ChangeQuery(
                        $"{_context.CurrentPluginMetadata.ActionKeyword} {Settings.TemporaryCommand} ");
                    return false;
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

    public event ResultUpdatedEventHandler ResultsUpdated;
}