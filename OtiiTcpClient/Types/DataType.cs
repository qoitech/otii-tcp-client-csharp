using System.Runtime.Serialization;

namespace OtiiTcpClient.Types
{
	/// <summary>
	/// Defines the possible channel data types.
	/// </summary>
	public enum DataType
	{
		/// <summary>
		/// Analog data. Used for measurements.
		/// </summary>
		[EnumMember(Value = "analog")]
		Analog,

		/// <summary>
		/// Digital data. Used for GPI channels.
		/// </summary>
		[EnumMember(Value = "digital")]
		Digital,

		/// <summary>
		/// Log data. Used for <see cref="Channel.UartLogs"/>.
		/// </summary>
		[EnumMember(Value = "log")]
		Log,
	}
}