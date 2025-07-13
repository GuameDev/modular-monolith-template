namespace ModularMonolith.Template.SharedKernel.Configuration
{
    /// <summary>
    /// Represents a configuration option for an application, providing a mechanism to define and access a specific
    /// configuration section.
    /// </summary>
    /// <remarks>Implementing types must define the <see cref="SectionName"/> property to specify the name of
    /// the configuration section associated with the option. This is typically used to map configuration settings to
    /// strongly-typed objects.</remarks>
    public interface IAppOption
    {
        /// <summary>
        /// Gets the name of the configuration section associated with this type.
        /// </summary>
        public static abstract string SectionName { get; }
    }
}
