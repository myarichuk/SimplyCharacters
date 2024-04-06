// Ignore Spelling: yaml

using System.Collections;
using YamlDotNet.Serialization;
using SimplyCharacters.Abstract.Components;
using SimplyCharacters.Abstract.Notifications;

public class ValueContainer : IEnumerable<ObservableNamedValue>
{
    private readonly Dictionary<string, ObservableNamedValue> _attributes = new(StringComparer.InvariantCultureIgnoreCase);

    public event Action<(ObservableNamedValue Origin, ValueChangeNotification Event)>? ValueChanged;

    /// <summary>
    /// Initializes a new instance of the CharacterAttributeContainer class by loading attribute definitions from the given YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string containing attribute definitions.</param>
    public ValueContainer(string yaml) => LoadAttributesFromYaml(yaml);

    /// <summary>
    /// Initializes a new instance of the CharacterAttributeContainer class by copying from another dictionary-like structure.
    /// </summary>
    /// <param name="yaml">Array containing attribute definitions as key/value pairs.</param>
    public ValueContainer(params KeyValuePair<string, ObservableNamedValue>[] attributes)
    {
        foreach(var item in attributes)
        {
            var value = item.Value;
            _attributes.Add(item.Key, value);
            item.Value.ValueChanged += @event => ValueChanged?.Invoke((value, @event));
        }
    }

    /// <summary>
    /// Provides indexed access to the character attributes by name.
    /// </summary>
    /// <param name="attributeName">The name of the attribute to access.</param>
    /// <returns>The ObservableNamedValue associated with the given attribute name.</returns>
    public uint this[string attributeName]
    {
        get
        {
            if(_attributes.ContainsKey(attributeName))
            {
                return _attributes[attributeName].Value;
            }

            throw new KeyNotFoundException(attributeName);
        }

        set
        {
            if (_attributes.ContainsKey(attributeName))
            {
                _attributes[attributeName].Value = value;
            }
            else
            {
                throw new KeyNotFoundException(attributeName);
            }
        }
    }

    public bool TryGetAttribute(string attributeName, out ObservableNamedValue? value) =>
        _attributes.TryGetValue(attributeName, out value);

    /// <summary>
    /// Loads character attributes from a YAML definition.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    private void LoadAttributesFromYaml(string yaml)
    {
        //TODO: improve error handling
        var deserializer = new DeserializerBuilder().Build();
        var yamlAttributes = deserializer.Deserialize<Dictionary<string, ValueWithDescription>>(yaml);
        
        foreach (var attr in yamlAttributes)
        {
            _attributes.Add(attr.Key, new ObservableNamedValue
            {
                Name = attr.Key,
                Description = attr.Value.Description,
                Value = attr.Value.Value
            });
        }
    }

    private record struct ValueWithDescription(string? Description, uint Value);

    public IEnumerator<ObservableNamedValue> GetEnumerator() => _attributes.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
