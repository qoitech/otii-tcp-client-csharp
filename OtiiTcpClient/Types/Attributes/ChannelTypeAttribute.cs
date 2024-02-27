using System;

namespace OtiiTcpClient.Types.Attributes
{
    /// <summary>
    /// Specifies the data type for a channel.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ChannelTypeAttribute : Attribute
    {
        /// <summary>
        /// Gets the channel data type.
        /// </summary>
        public DataType DataType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the channel value can be retrieved using <see cref="Arc.GetValue"/>.
        /// </summary>
        public bool CanGetValue { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelTypeAttribute"/> class.
        /// </summary>
        /// <param name="dataType">The channel data type.</param>
        public ChannelTypeAttribute(DataType dataType)
        {
            DataType = dataType;
        }
    }
}