using System;
using System.Reflection;
using OtiiTcpClient.Types.Attributes;

namespace OtiiTcpClient.Types.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Channel"/>.
    /// </summary>
    public static class ChannelExtensions
    {
        /// <summary>
        /// Gets the <see cref="DataType"/> of the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>The <see cref="DataType"/> of the channel.</returns>
        public static DataType GetDataType(this Channel channel)
        {
            return GetEnumValueAttribute<ChannelTypeAttribute>(channel)?.DataType ?? default;
        }

        /// <summary>
        /// Returns a value indicating whether the channel value can be retrieved using <see cref="Arc.GetValue"/>.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns><see langword="true"/> if the channel value can be retrieved; otherwise, <see langword="false"/>.</returns>
        public static bool CanGetValue(this Channel channel)
        {
            return GetEnumValueAttribute<ChannelTypeAttribute>(channel)?.CanGetValue ?? true;
        }

        public static T GetEnumValueAttribute<T>(Enum value)
            where T : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value) ?? throw new ArgumentException("The value is not defined in the enum.", nameof(value));
            return type.GetMember(name)[0].GetCustomAttribute<T>();
        }
    }
}