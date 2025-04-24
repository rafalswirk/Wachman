using System;

namespace Wachman.Models;

public class PomodoroConfiguration
{
    public int WorkSessionDuration { get; set; } = 30;
    public int BreakTimeDuration { get; set; } = 5;   
    public bool DisableBreaks { get; set; } = false;
}
