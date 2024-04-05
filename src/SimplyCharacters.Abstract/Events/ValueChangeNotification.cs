using SimplyCharacters.Abstract.Components;

namespace SimplyCharacters.Abstract.Notifications;

/// <summary>
/// Provides details about changes to an ObservableValue, including the original value and the new value
/// </summary>
public record struct ValueChangeNotification(uint ValueBefore, uint ValueAfter);
