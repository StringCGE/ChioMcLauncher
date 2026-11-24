using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChioLauncher;

public sealed class MinecraftManifestReader
{
    public JsonDocument VersionManifest { get; private set; } = null!;
    public JsonDocument? TLauncherManifest { get; private set; }

    public void Load(string versionDirectory)
    {
        string versionJson = Path.Combine(
            versionDirectory, "26.3.json");

        string additionalJson = Path.Combine(
            versionDirectory, "tLauncherAddittional.json");

        if (!File.Exists(versionJson))
            throw new FileNotFoundException(
                "Minecraft manifest not found.", versionJson);

        VersionManifest = JsonDocument.Parse(
            File.ReadAllText(versionJson));

        if (File.Exists(additionalJson))
        {
            TLauncherManifest = JsonDocument.Parse(
                File.ReadAllText(additionalJson));
        }
        else
        {
            throw new FileNotFoundException(
                "TLauncher additional manifest not found.",
                additionalJson);
        }
    }

    public void Debug(Action<string> log)
    {
        log("===== VERSION MANIFEST =====");

        foreach (var property in
            VersionManifest.RootElement.EnumerateObject())
        {
            log($"[MC] {property.Name}");
        }

        log("===== TLAUNCHER MANIFEST =====");

        foreach (var property in
            TLauncherManifest!.RootElement.EnumerateObject())
        {
            log($"[TL] {property.Name}");
        }
    }

    public void Dispose()
    {
        VersionManifest?.Dispose();
        TLauncherManifest?.Dispose();
    }
}


