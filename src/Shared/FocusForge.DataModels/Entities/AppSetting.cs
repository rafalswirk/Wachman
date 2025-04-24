using System;
using System.ComponentModel.DataAnnotations;

namespace Wachman.Entities;

public class AppSetting
{
    [Key]
    public int Id { get; set; }
    public string SettingsKey { get; set; }
    public string SettingsValue { get; set; }
}

