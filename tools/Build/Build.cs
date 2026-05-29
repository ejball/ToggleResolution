return BuildRunner.Execute(args, build =>
{
	var buildOptions = new DotNetBuildOptions();

	build.AddDotNetTargets(
		new DotNetBuildSettings
		{
			BuildOptions = buildOptions,
		});

	build.Target("package")
		.Describe("Creates a standalone executable")
		.ClearActions()
		.Does(() =>
		{
			RunDotNet("publish",
				Path.Combine("src", "ToggleResolution", "ToggleResolution.csproj"),
				"-c", buildOptions.ConfigurationOption!.Value,
				"-r", "win-x86",
				"--self-contained", "true",
				"-p:PublishSingleFile=true");
		});
});
