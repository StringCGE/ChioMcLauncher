using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChioLauncher;


public sealed class MinecraftLauncher
{
    private readonly McProcess _process;

    public MinecraftLauncher(McProcess process)
    {
        _process = process;
    }

    public async Task<int> LaunchAsync(
        string javaPath,
        string versionDirectory,
        string minecraftDirectory,
        MinecraftAccount account,
        Action<string>? output = null,
        Action<string>? error = null)
    {
        string manifestPath = Path.Combine(
            versionDirectory, "26.3.json");

        using JsonDocument document =
            JsonDocument.Parse(File.ReadAllText(manifestPath));

        JsonElement root = document.RootElement;

        string mainClass = root
            .GetProperty("mainClass")
            .GetString()!;

        // Aquí construiremos el classpath.
        string classPath = BuildClassPath(
            root,
            minecraftDirectory);

        // Argumentos JVM.
        var arguments = new List<string>
        {
            "-Djava.library.path=" +
            Path.Combine(versionDirectory, "natives"),

            "-cp",
            classPath,

            mainClass
        };

        // Argumentos básicos del juego.
        arguments.Add("--username");
        arguments.Add(account.PlayerName ?? "Player");

        arguments.Add("--version");
        arguments.Add("26.3");

        arguments.Add("--gameDir");
        arguments.Add(minecraftDirectory);

        arguments.Add("--assetsDir");
        arguments.Add(Path.Combine(minecraftDirectory, "assets"));

        arguments.Add("--assetIndex");
        arguments.Add(
            root.GetProperty("assetIndex")
                .GetProperty("id")
                .GetString()!);

        arguments.Add("--uuid");
        arguments.Add(account.PlayerUuid ?? "");

        arguments.Add("--accessToken");
        arguments.Add(account.AccessToken ?? "");

        arguments.Add("--userType");
        arguments.Add(account.UserType ?? "msa");

        string finalArguments = string.Join(
            " ",
            arguments.Select(QuoteArgument));

        return await _process.LaunchAsync(
            javaPath,
            minecraftDirectory,
            finalArguments,
            output,
            error);
    }

    private static string BuildClassPath(
        JsonElement root,
        string minecraftDirectory)
    {
        var libraries = new List<string>();

        if (root.TryGetProperty("libraries", out var libraryArray))
        {
            foreach (var library in libraryArray.EnumerateArray())
            {
                if (!library.TryGetProperty(
                    "downloads", out var downloads))
                    continue;

                if (!downloads.TryGetProperty(
                    "artifact", out var artifact))
                    continue;

                string? path = artifact
                    .GetProperty("path")
                    .GetString();

                if (string.IsNullOrWhiteSpace(path))
                    continue;

                string libraryPath = Path.Combine(
                    minecraftDirectory,
                    "libraries",
                    path.Replace('/', Path.DirectorySeparatorChar));

                if (File.Exists(libraryPath))
                    libraries.Add(libraryPath);
            }
        }

        string clientJar = Path.Combine(
            minecraftDirectory,
            "versions",
            "26.3",
            "26.3.jar");

        if (File.Exists(clientJar))
            libraries.Add(clientJar);

        return string.Join(
            Path.PathSeparator,
            libraries);
    }

    private static string QuoteArgument(string argument)
    {
        return "\"" + argument.Replace("\"", "\\\"") + "\"";
    }
}


