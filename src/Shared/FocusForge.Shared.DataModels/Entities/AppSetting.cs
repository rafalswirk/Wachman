using System;
using System.ComponentModel.DataAnnotations;

namespace FocusForge.Shared.DataModels.Entities;

public class AppSetting
{
    [Key]
    public int Id { get; set; }
    public string SettingsKey { get; set; }
    public string SettingsValue { get; set; }
}

