using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace OtiiTcpClient.Types{
	/// <summary>
	/// Defines the possible 4-wire measurement states.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Arc4WireState{
		/// <summary>
		/// Calibration invalid.
		/// </summary>
		[EnumMember(Value = "cal_invalid")]
		CalibrationInvalid,

		/// <summary>
		/// Disabled.
		/// </summary>
		[EnumMember(Value = "disabled")]
		Disabled,

		/// <summary>
		/// Inactive.
		/// </summary>
		[EnumMember(Value = "inactive")]
		Inactive,

		/// <summary>
		/// Active.
		/// </summary>
		[EnumMember(Value = "active")]
		Active,
	}
}