using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using OtiiTcpClient.Types.Attributes;


namespace OtiiTcpClient.Types{
	/// <summary>
	/// Defines the available channels.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Channel{

		/// <summary>
		/// Main current. Unit is amperes.
		/// </summary>
		[EnumMember(Value = "mc")]
		[ChannelType(DataType.Analog)]
		MainCurrent,

		/// <summary>
		/// Main energy. Unit is joules.
		/// </summary>
		[EnumMember(Value = "me")]
		[ChannelType(DataType.Analog, CanGetValue = false)]
		MainEnergy,

		/// <summary>
		/// Main voltage. Unit is volts.
		/// </summary>
		[EnumMember(Value = "mv")]
		[ChannelType(DataType.Analog)]
		MainVoltage,

		/// <summary>
		/// ADC current. Unit is amperes.
		/// </summary>
		[EnumMember(Value = "ac")]
		[ChannelType(DataType.Analog)]
		AdcCurrent,

		/// <summary>
		/// ADC energy. Unit is joules.
		/// </summary>
		[EnumMember(Value = "ae")]
		[ChannelType(DataType.Analog, CanGetValue = false)]
		AdcEnergy,

		/// <summary>
		/// ADC voltage. Unit is volts.
		/// </summary>
		[EnumMember(Value = "av")]
		[ChannelType(DataType.Analog)]
		AdcVoltage,

		/// <summary>
		/// Sense+ voltage. Unit is volts.
		/// </summary>
		[EnumMember(Value = "sp")]
		[ChannelType(DataType.Analog)]
		SensePositiveVoltage,

		/// <summary>
		/// Sense- voltage. Unit is volts.
		/// </summary>
		[EnumMember(Value = "sn")]
		[ChannelType(DataType.Analog)]
		SenseNegativeVoltage,

		/// <summary>
		/// VBUS. Unit is volts.
		/// </summary>
		[EnumMember(Value = "vb")]
		[ChannelType(DataType.Analog)]
		VBus,

		/// <summary>
		/// DC jack. Unit is volts.
		/// </summary>
		[EnumMember(Value = "vj")]
		[ChannelType(DataType.Analog)]
		DcJack,

		/// <summary>
		/// Temperature. Unit is degrees Celsius.
		/// </summary>
		[EnumMember(Value = "tp")]
		[ChannelType(DataType.Analog)]
		Temperature,

		/// <summary>
		/// GPI1.
		/// </summary>
		[EnumMember(Value = "i1")]
		[ChannelType(DataType.Digital)]
		Gpi1,

		/// <summary>
		/// GPI2.
		/// </summary>
		[EnumMember(Value = "i2")]
		[ChannelType(DataType.Digital)]
		Gpi2,

		/// <summary>
		/// UART logs.
		/// </summary>
		[EnumMember(Value = "rx")]
		[ChannelType(DataType.Log, CanGetValue = false)]
		UartLogs,
	}
}