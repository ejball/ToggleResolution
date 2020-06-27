using System;
using System.Windows.Forms;

namespace ToggleResolution
{
	internal static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			try
			{
				Run();
			}
			catch (Exception exception)
			{
				ShowMessageBox($"Error: {exception.Message}");
			}
		}

		private static void Run()
		{
			throw new NotImplementedException();
		}

		private static void ShowMessageBox(params string[] lines)
		{
			MessageBox.Show(
				text: string.Join(Environment.NewLine, lines),
				caption: c_appCaption);
		}

		private const string c_appCaption = "ToggleResolution";
	}
}
