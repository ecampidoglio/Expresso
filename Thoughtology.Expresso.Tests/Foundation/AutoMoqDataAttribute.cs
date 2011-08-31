using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;

namespace Thoughtology.Expresso.Tests.Foundation
{
    /// <summary>
    /// Provides auto-generated data specimens to xUnit Theories
    /// using AutoFixture for concrete types and Moq for interfaces and abstract classes.
    /// </summary>
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMoqDataAttribute"/> class.
        /// </summary>
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(
                new AutoMoqCustomization()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMoqDataAttribute"/> class.
        /// </summary>
        /// <param name="fixtureType">Type of the fixture.</param>
        public AutoMoqDataAttribute(Type fixtureType)
            : base(((IFixture)Activator.CreateInstance(fixtureType)).Customize(
                new AutoMoqCustomization()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMoqDataAttribute"/> class.
        /// </summary>
        /// <param name="fixture">The <see cref="IFixture"/> instance.</param>
        public AutoMoqDataAttribute(IFixture fixture)
            : base(fixture.Customize(
                new AutoMoqCustomization()))
        {
        }
    }
}