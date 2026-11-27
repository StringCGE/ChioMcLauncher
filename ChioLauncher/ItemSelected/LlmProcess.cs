using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChioLauncher.ItemSelected;


public sealed class LlmProcess : IDisposable
{
    private Process? _process;
    private bool _disposed;

    public bool IsRunning
    {
        get
        {
            try
            {
                return _process != null && !_process.HasExited;
            }
            catch
            {
                return false;
            }
        }
    }

    public int? ExitCode { get; private set; }

    public int? ProcessId => IsRunning ? _process!.Id : null;

    public async Task<int> LaunchAsync(
        ChioLauncherLlmConfig config,
        Action<string>? onOutput = null,
        Action<string>? onError = null,
        Action? onStarted = null,
        Action<int>? onExited = null)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(LlmProcess));

        if (IsRunning)
            throw new InvalidOperationException(
                "El servidor de inferencia ya está ejecutándose.");

        string executable = config.GetInferenceExecutable();
        onOutput?.Invoke("[COMMAND] " + executable);
        if (!File.Exists(executable))
            throw new FileNotFoundException(
                "Inference Engine no encontrado.", executable);

        if (!File.Exists(config.ModelSelectedPath))
            throw new FileNotFoundException(
                "Modelo no encontrado.", config.ModelSelectedPath);

        ProcessStartInfo startInfo = config.BuildProcessStartInfo();

        var process = new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true
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

        try
        {
            ExitCode = null;

            process.Start();

            _process = process;

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            onStarted?.Invoke();

            await process.WaitForExitAsync().ConfigureAwait(false);

            // Asegurar que se procesen las salidas pendientes.
            process.WaitForExit();

            ExitCode = process.ExitCode;

            onExited?.Invoke(process.ExitCode);

            return process.ExitCode;
        }
        finally
        {
            if (ReferenceEquals(_process, process))
                _process = null;

            process.Dispose();
        }
    }

    public async Task StopAsync()
    {
        Process? process = _process;

        if (process == null)
            return;

        try
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);

                // Esperar a que el proceso termine realmente.
                await process.WaitForExitAsync().ConfigureAwait(false);
            }

            process.WaitForExit();
        }
        catch (InvalidOperationException)
        {
            // El proceso ya terminó.
        }
        catch (System.ComponentModel.Win32Exception)
        {
            // Error de permisos o terminación.
        }
        finally
        {
            if (ReferenceEquals(_process, process))
                _process = null;
        }
    }

    public void Stop()
    {
        StopAsync().GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        Stop();

        _disposed = true;
    }
}
