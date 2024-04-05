using SimplyCharacters.Abstract.Notifications;

namespace SimplyCharacters.Abstract.Components;

/// <summary>
/// Represents a value that notifies listeners of changes, suitable for UI binding and tracking changes in character states, abilities, and other numerical properties.
/// </summary>
public class ObservableValue
{
    private uint _value;

    /// <summary>
    /// Gets or sets the numeric value and notifies listeners of changes.
    /// </summary>
    public virtual uint Value
    {
        get => _value;
        set
        {
            var changeNotification = new ValueChangeNotification(_value, value);
            _value = value;
            AttributeChanged?.Invoke(changeNotification);
        }
    }

    /// <summary>
    /// Occurs when the value changes, providing details of the change.
    /// </summary>
    public event Action<ValueChangeNotification>? AttributeChanged;
}