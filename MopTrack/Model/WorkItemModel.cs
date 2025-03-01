using System;
using System.ComponentModel.DataAnnotations;

namespace MopTrack.Model;

public class WorkItemModel
{
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// Property gibt an ->Name des ausgewählten Benutzers
    /// </summary>
    public required string User { get; set; }
    /// <summary>
    /// Property gibt an -> Beschreibung des WorkItems
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Property gibt an, welches Objekt ausgewählt wurde
    /// </summary>
    public string? Object { get; set; }
    /// <summary>
    /// Property gibt an -> welcher Typ das ist (Pause, Arbeitszeit, Fahrzeit, Urlaub, Feiertag)
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// Property gibt an -> welcher Reinigungstyp es ist (Unterhaltsreinigung oder Grundreinigung (UR/GR))
    /// </summary>
    public string? CleanType { get; set; }
    /// <summary>
    /// Property gibt an -> an welchem Datum das war (muss angegeben werden)
    /// </summary>
    public required DateTime Date {get; set; }
    /// <summary>
    /// Property gibt an -> Wann das Item anfing (pflichtangabe)
    /// </summary>
    public required TimeSpan From { get; set; }
    /// <summary>
    /// Property gibt an -> Wann das Item beendet wurde (pflichtangabe)
    /// </summary>
    public required TimeSpan To { get; set; }
    /// <summary>
    /// Property gibt an -> die Gesamtzeit (von - bis)
    /// </summary>
    public TimeSpan? Result { get; set; }
    /// <summary>
    /// Property gibt an -> ob Frei durch Feiertag
    /// </summary>
    public bool? Holiday { get; set; }
    /// <summary>
    /// Property gibt an -> ob frei durch Urlaub
    /// </summary>
    public bool? Vacation { get; set; }
    /// <summary>
    /// Property gibt an -> den Titel des Workitems
    /// </summary>
    public string? WorkItemTitle { get; set; }
    /// <summary>
    /// Property gibt an -> die Notiz die man bei jedem Workitem hinterlegen kann
    /// </summary>
    public string? Notice { get; set; }

}
