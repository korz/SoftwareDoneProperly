<Query Kind="Program" />

void Main()
{
	var versions = new List<string>
	{
		//"5",
		//"2.1.0",
		//"1.1.0",
		"1.0.0",
		//"1.1.1",
		"1.0.1",
		//"2.0"		
	}.Select(ToVersion).ToList();

	versions.Dump();
	//versions.Sort(new VersionComparer());
	var latestVersion = versions.OrderByDescending(x => x, new VersionComparer()).FirstOrDefault();
	latestVersion.Dump();
	
	var incrementedVersion = VersionIncrementer.IncrementNightly(latestVersion);
	incrementedVersion.Dump();
}

public static Version ToVersion(string versionText)
{
	var splits = versionText.Split('.');

	return new Version
	{
		Major = int.Parse(splits.ElementAtOrDefault(0) ?? "0"),
		Minor = int.Parse(splits.ElementAtOrDefault(1) ?? "0"),
		Nightly = int.Parse(splits.ElementAtOrDefault(2) ?? "0")
	};
}

public class Version
{
	public int Major { get; set; }
	public int Minor { get; set; }
	public int Nightly { get; set; }

	public override string ToString()
	{
		return $"{this.Major}.{this.Minor}.{this.Nightly}";
	}
}

public class VersionComparer : IComparer<Version>
{
	//0 if equal, 1 if x > y, -1 if x < y
	public int Compare(Version x, Version y)
	{
		if (x.Major == y.Major && x.Minor == y.Minor && x.Nightly == y.Nightly)
		{
			return 0;
		}

		if (x.Major > y.Major) { return 1; }
		else if (x.Major >= y.Major && x.Minor > y.Minor) { return 1; }
		else if (x.Major >= y.Major && x.Minor >= y.Minor && x.Nightly > y.Nightly) { return 1; }

		return -1;
	}
}

public static class VersionIncrementer
{
	public static Version IncrementMajor(Version version, bool resetMinor = true, bool resetNightly = true)
	{
		return new Version
		{
			Major = version.Major + 1,
			Minor = resetMinor ? 0 : version.Minor,
			Nightly = resetNightly ? 0 : version.Nightly
		};
	}

	public static Version IncrementMinor(Version version, bool resetNightly = true)
	{
		return new Version
		{
			Major = version.Major,
			Minor = version.Minor + 1,
			Nightly = resetNightly ? 0 : version.Nightly
		};
	}

	public static Version IncrementNightly(Version version)
	{
		return new Version
		{
			Major = version.Major,
			Minor = version.Minor,
			Nightly = version.Nightly + 1
		};
	}
}
