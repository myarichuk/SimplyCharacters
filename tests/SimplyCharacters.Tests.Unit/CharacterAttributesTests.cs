using System;
using System.Collections.Generic;
using Xunit;
using SimplyCharacters.Abstract.Components;
using SimplyCharacters.Abstract.Notifications;

namespace SimplyCharacters.Tests
{
    public class CharacterAttributesTests
    {
        private const string TestYaml = @"
            Strength:
                Description: Physical power
                Value: 10
            Intelligence:
                Description: Mental acuity
                Value: 15
            ";

        [Fact]
        public void Constructor_YamlString_CorrectlyInitializesAttributes()
        {
            var container = new ValueContainer(TestYaml);
            
            Assert.True(container.TryGetAttribute("Strength", out var strength));
            Assert.Equal(10u, strength?.Value);
            Assert.Equal("Physical power", strength?.Description);

            Assert.True(container.TryGetAttribute("Intelligence", out var intelligence));
            Assert.Equal(15u, intelligence?.Value);
            Assert.Equal("Mental acuity", intelligence?.Description);
        }

        [Fact]
        public void Constructor_KeyValuePairs_CorrectlyInitializesAttributes()
        {
            var strength = new ObservableNamedValue { Name = "Strength", Description = "Physical power", Value = 10 };
            var intelligence = new ObservableNamedValue { Name = "Intelligence", Description = "Mental acuity", Value = 15 };
            var container = new ValueContainer(
                new KeyValuePair<string, ObservableNamedValue>("Strength", strength),
                new KeyValuePair<string, ObservableNamedValue>("Intelligence", intelligence)
            );

            Assert.Equal(10u, container["Strength"]);
            Assert.Equal(15u, container["Intelligence"]);
        }

        [Fact]
        public void Indexer_GetSetWorksCorrectly()
        {
            var container = new ValueContainer(TestYaml);
            Assert.Equal(10u, container["Strength"]);
            
            container["Strength"] = 12;
            Assert.Equal(12u, container["Strength"]);
        }

        [Fact]
        public void Indexer_ThrowsOnUndefinedAttribute()
        {
            var container = new ValueContainer(TestYaml);
            Assert.Throws<KeyNotFoundException>(() => container["Agility"]);
        }

        [Fact]
        public void TryGetAttribute_ReturnsCorrectly()
        {
            var container = new ValueContainer(TestYaml);
            Assert.True(container.TryGetAttribute("Strength", out _));
            Assert.False(container.TryGetAttribute("Agility", out _));
        }

        [Fact]
        public void GetEnumerator_IteratesOverAllAttributes()
        {
            var container = new ValueContainer(TestYaml);
            var attributeNames = new List<string>();

            foreach (var attr in container)
            {
                attributeNames.Add(attr.Name);
            }

            Assert.Contains("Strength", attributeNames);
            Assert.Contains("Intelligence", attributeNames);
            Assert.Equal(2, attributeNames.Count);
        }
    }
}
