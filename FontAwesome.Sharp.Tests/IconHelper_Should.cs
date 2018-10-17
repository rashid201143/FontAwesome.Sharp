using System;
using System.Linq;
using System.Windows.Media;
using FluentAssertions;
using NEdifis.Attributes;
using NUnit.Framework;

namespace FontAwesome.Sharp.Tests
{
    [TestFixtureFor(typeof(IconHelper))]
    // ReSharper disable once InconsistentNaming
    internal class IconHelper_Should
    {
        [Test]
        public void Convert_icon_enums_to_characters()
        {
            foreach (var icon in Enum.GetValues(typeof(IconChar)).Cast<IconChar>())
            {
                var expected = char.ConvertFromUtf32((int)icon).Single();
                icon.ToChar().Should().Be(expected);
            }
        }

        [Test]
        public void Generate_imageSources_for_icon_chars()
        {
            const int limit = -1;
            var icons = Enum.GetValues(typeof(IconChar)).Cast<IconChar>().Skip(1); // 1=None
            if (limit > 0) icons = icons.Take(limit);
            foreach (var icon in icons)
            {
                const int size = 16;
                var imageSource = icon.ToImageSource(size: size);
                imageSource.Should().NotBeNull($"an image should be generated for '{icon}'");
                imageSource.Should().BeOfType<DrawingImage>();
                imageSource.Width.Should().BeLessOrEqualTo(size);
                imageSource.Height.Should().BeLessOrEqualTo(size);
            }
        }

    }
}
