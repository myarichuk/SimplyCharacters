using SimplyCharacters.Abstract.Notifications;

namespace SimplyCharacters.Abstract.Components;

/// <summary>
/// Extends <see cref="ObservableValue"/> with a name and description, allowing for detailed character attributes like abilities or spell slots.
/// </summary>
public class ObservableNamedValue : ObservableValue
{
    /// <summary>
    /// Gets the name of the value, representing a specific character attribute or property.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the description of the value, providing additional context or details about the character attribute or property.
    /// </summary>
    public string? Description { get; init; }
}
