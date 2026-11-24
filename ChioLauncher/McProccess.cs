using System.Diagnostics;

namespace ChioLauncher;




public sealed class McProcess : IDisposable
{
    private Process? _process;

    public bool IsRunning =>
        _process != null && !_process.HasExited;

    public async Task<int> LaunchAsync(
        string javaPath,
        string workingDirectory,
        string arguments,
        Action<string>? onOutput = null,
        Action<string>? onError = null)
    {
        if (IsRunning)
            throw new InvalidOperationException(
                "Minecraft ya está ejecutándose.");

        var startInfo = new ProcessStartInfo
        {
            FileName = javaPath,
            WorkingDirectory = workingDirectory,
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        var process = new Process
        {
            StartInfo = startInfo
        };

        process.OutputDataReceived += (_, e) =>
        {
            if (e.Data != null)
                onOutput?.Invoke(e.Data);
        };

        process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data != null)
                onError?.Invoke(e.Data);
        };

        _process = process;

        try
        {
            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();

            // Esperar a que termine la lectura de salida.
            process.WaitForExit();

            return process.ExitCode;
        }
        finally
        {
            if (ReferenceEquals(_process, process))
                _process = null;

            process.Dispose();
        }
    }

    public void Stop()
    {
        if (_process == null)
            return;

        try
        {
            if (!_process.HasExited)
                _process.Kill(entireProcessTree: true);
        }
        catch (InvalidOperationException)
        {
            // El proceso ya terminó.
        }
    }

    public void Dispose()
    {
        Stop();
    }
}