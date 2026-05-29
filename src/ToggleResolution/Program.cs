using System.Runtime.InteropServices;

namespace ToggleResolution
{
	internal static class Program
	{
		[STAThread]
		public static int Main()
		{
			try
			{
				return Run() ? 0 : 1;
			}
			catch (Exception exception)
			{
				ShowMessageBox($"Error: {exception.Message}");
				return 2;
			}
		}

		private static bool Run()
		{
			var currentDevMode = default(NativeMethods.DEVMODE);
			currentDevMode.dmSize = (ushort) Marshal.SizeOf(currentDevMode);
			NativeMethods.EnumDisplaySettings(null, NativeMethods.ENUM_CURRENT_SETTINGS, ref currentDevMode);

			var availableDevMode = default(NativeMethods.DEVMODE);
			availableDevMode.dmSize = (ushort) Marshal.SizeOf(availableDevMode);

			var modeIndex = 0;
			while (NativeMethods.EnumDisplaySettings(null, modeIndex, ref availableDevMode))
			{
				if (availableDevMode.dmPelsWidth == currentDevMode.dmPelsWidth * 2 &&
					availableDevMode.dmPelsHeight == currentDevMode.dmPelsHeight * 2 &&
					availableDevMode.dmBitsPerPel == currentDevMode.dmBitsPerPel &&
					availableDevMode.dmDisplayFlags == currentDevMode.dmDisplayFlags &&
					availableDevMode.dmDisplayFrequency == currentDevMode.dmDisplayFrequency)
				{
					ChangeDisplaySettings(ref availableDevMode);
					return true;
				}
				modeIndex++;
			}

			modeIndex = 0;
			while (NativeMethods.EnumDisplaySettings(null, modeIndex, ref availableDevMode))
			{
				if (availableDevMode.dmPelsWidth >= 800 &&
					availableDevMode.dmPelsWidth == currentDevMode.dmPelsWidth / 2 &&
					availableDevMode.dmPelsHeight == currentDevMode.dmPelsHeight / 2 &&
					availableDevMode.dmBitsPerPel == currentDevMode.dmBitsPerPel &&
					availableDevMode.dmDisplayFlags == currentDevMode.dmDisplayFlags &&
					availableDevMode.dmDisplayFrequency == currentDevMode.dmDisplayFrequency)
				{
					ChangeDisplaySettings(ref availableDevMode);
					return true;
				}
				modeIndex++;
			}

			ShowMessageBox($"Could not find double or half resolution of {currentDevMode.dmPelsWidth}x{currentDevMode.dmPelsHeight}.");
			return false;
		}

		private static void ShowMessageBox(params string[] lines)
		{
			MessageBox.Show(
				text: string.Join(Environment.NewLine, lines),
				caption: c_appCaption);
		}

		private static void ChangeDisplaySettings(ref NativeMethods.DEVMODE devMode)
		{
			var result = NativeMethods.ChangeDisplaySettings(ref devMode, NativeMethods.CDS_UPDATEREGISTRY);
			if (result != NativeMethods.DISP_CHANGE_SUCCESSFUL)
				throw new InvalidOperationException($"ChangeDisplaySettings failed with result {result}.");
		}

		private const string c_appCaption = "ToggleResolution";
	}
}
