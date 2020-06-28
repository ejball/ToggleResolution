using System.Runtime.InteropServices;

namespace ToggleResolution
{
	internal static class NativeMethods
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct Pointl
		{
			[MarshalAs(UnmanagedType.I4)]
			public int x;

			[MarshalAs(UnmanagedType.I4)]
			public int y;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct DEVMODE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			[MarshalAs(UnmanagedType.U2)]
			public ushort dmSpecVersion;

			[MarshalAs(UnmanagedType.U2)]
			public ushort dmDriverVersion;

			[MarshalAs(UnmanagedType.U2)]
			public ushort dmSize;

			[MarshalAs(UnmanagedType.U2)]
			public ushort dmDriverExtra;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmFields;

			public Pointl dmPosition;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmDisplayOrientation;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmDisplayFixedOutput;

			[MarshalAs(UnmanagedType.I2)]
			public short dmColor;

			[MarshalAs(UnmanagedType.I2)]
			public short dmDuplex;

			[MarshalAs(UnmanagedType.I2)]
			public short dmYResolution;

			[MarshalAs(UnmanagedType.I2)]
			public short dmTTOption;

			[MarshalAs(UnmanagedType.I2)]
			public short dmCollate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			[MarshalAs(UnmanagedType.U2)]
			public ushort dmLogPixels;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmBitsPerPel;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmPelsWidth;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmPelsHeight;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmDisplayFlags;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmDisplayFrequency;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmICMMethod;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmICMIntent;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmMediaType;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmDitherType;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmReserved1;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmReserved2;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmPanningWidth;

			[MarshalAs(UnmanagedType.U4)]
			public uint dmPanningHeight;
		}

		public const int ENUM_CURRENT_SETTINGS = -1;     // Retrieves the current display mode.
		public const uint CDS_UPDATEREGISTRY = 1;

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EnumDisplaySettings(
			[param: MarshalAs(UnmanagedType.LPTStr)] string? lpszDeviceName,
			[param: MarshalAs(UnmanagedType.U4)] int iModeNum,
			[In, Out] ref DEVMODE lpDevMode);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.I4)]
		public static extern int ChangeDisplaySettings(
			[In, Out] ref DEVMODE lpDevMode,
			[param: MarshalAs(UnmanagedType.U4)] uint dwflags);
	}
}
