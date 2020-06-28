using System.IO;
using Faithlife.Build;
using static Faithlife.Build.DotNetRunner;

internal static class Build
{
	public static int Main(string[] args) => BuildRunner.Execute(args, build =>
	{
		var buildOptions = new DotNetBuildOptions();

		build.AddDotNetTargets(
			new DotNetBuildSettings
			{
				BuildOptions = buildOptions,
				Verbosity = DotNetBuildVerbosity.Minimal,
			});

		build.Target("package")
			.Describe("Create a standalone executable")
			.ClearActions()
			.Does(() =>
			{
				RunDotNet("publish",
					Path.Combine("src", "ToggleResolution", "ToggleResolution.csproj"),
					"-c", buildOptions.ConfigurationOption!.Value,
					"-r", "win-x86",
					"--self-contained", "true",
					"-p:PublishSingleFile=true",
					"-p:PublishTrimmed=true");
			});
	});
}
