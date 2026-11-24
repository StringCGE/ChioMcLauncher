using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChioLauncher;


public sealed record JavaRuntimeInfo(
    string Component,
    int MajorVersion
);

public static class JavaRuntimeResolver
{
    // Lee el manifiesto y determina qué Java necesita Minecraft.
    public static JavaRuntimeInfo GetRequiredJavaComponent(
        string manifestPath)
    {
        if (!File.Exists(manifestPath))
            throw new FileNotFoundException(
                "No se encontró el manifiesto.", manifestPath);

        using JsonDocument document =
            JsonDocument.Parse(File.ReadAllText(manifestPath));

        JsonElement root = document.RootElement;

        if (!root.TryGetProperty("javaVersion", out var javaVersion))
            throw new InvalidDataException(
                "El manifiesto no contiene javaVersion.");

        string component = javaVersion
            .GetProperty("component")
            .GetString()
            ?? throw new InvalidDataException(
                "No se especificó el componente Java.");

        int majorVersion = javaVersion
            .GetProperty("majorVersion")
            .GetInt32();

        return new JavaRuntimeInfo(component, majorVersion);
    }

    // Busca el ejecutable Java correspondiente al componente.
    public static string ResolveJavaPath(
        string runtimeRoot,
        string component)
    {
        if (!Directory.Exists(runtimeRoot))
            throw new DirectoryNotFoundException(
                $"No existe la carpeta runtime: {runtimeRoot}");

        // Evitar nombres de directorio inesperados.
        if (string.IsNullOrWhiteSpace(component) ||
            component != Path.GetFileName(component))
        {
            throw new ArgumentException(
                "Componente Java no válido.", nameof(component));
        }

        string componentDirectory =
            Path.Combine(runtimeRoot, component);

        if (!Directory.Exists(componentDirectory))
            throw new DirectoryNotFoundException(
                $"No está instalado el runtime: {component}");

        string[] javaFiles = Directory.GetFiles(
            componentDirectory,
            "java.exe",
            SearchOption.AllDirectories);

        string? javaPath = javaFiles
            .FirstOrDefault(path =>
                Path.GetFileName(
                    Path.GetDirectoryName(path))!
                    .Equals("bin",
                        StringComparison.OrdinalIgnoreCase));

        if (javaPath == null)
            throw new FileNotFoundException(
                $"No se encontró java.exe para {component}.");

        return javaPath;
    }
}

