using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChioLauncher.ItemSelected;

// =========================================================
// ENUMERATIONS
// =========================================================

public enum InferenceEngineType
{
    LlamaCpp,
    Ollama,
    OnnxRuntime,
    OpenVINO,
    Custom
}

public enum InferenceExecutionMode
{
    Server,
    Console
}

// =========================================================
// DEFAULT SETTINGS
// =========================================================

public static class LlmDefaults
{
    public const int ContextSize = 2048;
    public const int Threads = 4;
    public const int BatchSize = 512;
    public const int UBatchSize = 512;
    public const int Port = 8080;

    public const string Host = "127.0.0.1";
    public const string ModelArgument = "-m";
    public const string ContextArgument = "-c";
    public const string ThreadsArgument = "-t";
}

// =========================================================
// INFERENCE ENGINE SETTINGS
// =========================================================

public class InferenceEngineSettings
{
    public InferenceEngineType Type { get; set; }
        = InferenceEngineType.LlamaCpp;

    public InferenceExecutionMode ExecutionMode { get; set; }
        = InferenceExecutionMode.Server;

    public string ExecutableName { get; set; }
        = "llama-server.exe";

    public string AdditionalArguments { get; set; }
        = "";
}

// =========================================================
// INFERENCE PARAMETERS
// =========================================================

public class InferenceParameters
{
    // Model
    public string ModelPath { get; set; } = "";

    // Context
    public int ContextSize { get; set; }
        = LlmDefaults.ContextSize;

    public int BatchSize { get; set; }
        = LlmDefaults.BatchSize;

    public int UBatchSize { get; set; }
        = LlmDefaults.UBatchSize;

    // CPU
    public int Threads { get; set; }
        = LlmDefaults.Threads;

    public int ThreadsBatch { get; set; }
        = LlmDefaults.Threads;

    // Server
    public string Host { get; set; }
        = LlmDefaults.Host;

    public int Port { get; set; }
        = LlmDefaults.Port;

    // GPU
    public int GpuLayers { get; set; } = 0;

    // Sampling
    public float Temperature { get; set; } = 0.8f;
    public float TopP { get; set; } = 0.95f;
    public int TopK { get; set; } = 40;
    public float MinP { get; set; } = 0.05f;

    // Generation
    public int MaxTokens { get; set; } = -1;

    // Options
    public bool UseMmap { get; set; } = true;
    public bool UseMlock { get; set; } = false;
    public bool FlashAttention { get; set; } = false;
}

// =========================================================
// MAIN CONFIGURATION
// =========================================================

public class ChioLauncherLlmConfig
{
    // Folders
    public string ModelsPath { get; set; } = "Models";

    public string InferenceEnginesPath { get; set; }
        = "InferenceEngines";

    // Selected items
    public string ModelSelectedPath { get; set; } = "";

    public string InferenceEnginePath { get; set; } = "";

    // Engine
    public InferenceEngineSettings Engine { get; set; }
        = new InferenceEngineSettings();

    // Parameters
    public InferenceParameters Parameters { get; set; }
        = new InferenceParameters();

    // =====================================================
    // EXECUTABLE
    // =====================================================

    public string GetInferenceExecutable()
    {
        return Path.Combine(
            InferenceEnginePath,
            Engine.ExecutableName
        );
    }

    // =====================================================
    // ARGUMENT BUILDER
    // =====================================================

    public string GetInferenceArguments()
    {
        var args = new StringBuilder();

        switch (Engine.Type)
        {
            case InferenceEngineType.LlamaCpp:

                if (!string.IsNullOrWhiteSpace(ModelSelectedPath))
                    args.Append($"-m \"{ModelSelectedPath}\" ");

                args.Append($"-c {Parameters.ContextSize} ");
                args.Append($"-b {Parameters.BatchSize} ");
                args.Append($"-ub {Parameters.UBatchSize} ");
                args.Append($"-t {Parameters.Threads} ");
                args.Append($"-tb {Parameters.ThreadsBatch} ");
                args.Append($"-ngl {Parameters.GpuLayers} ");

                if (Engine.ExecutionMode == InferenceExecutionMode.Server)
                {
                    args.Append($"--host {Parameters.Host} ");
                    args.Append($"--port {Parameters.Port} ");
                }

                args.Append($"--temp {Parameters.Temperature} ");
                args.Append($"--top-p {Parameters.TopP} ");
                args.Append($"--top-k {Parameters.TopK} ");
                args.Append($"--min-p {Parameters.MinP} ");

                if (Parameters.MaxTokens > 0)
                    args.Append($"-n {Parameters.MaxTokens} ");

                if (Parameters.UseMmap)
                {
                    //args.Append("--mmap ");
                }
                else
                {
                    args.Append("--no-mmap ");
                }
                if (Parameters.UseMlock)
                    args.Append("--mlock ");

                if (Parameters.FlashAttention)
                    args.Append("-fa ");

                break;

            case InferenceEngineType.Custom:
                break;

            case InferenceEngineType.Ollama:
            case InferenceEngineType.OnnxRuntime:
            case InferenceEngineType.OpenVINO:
                break;
        }

        if (!string.IsNullOrWhiteSpace(Engine.AdditionalArguments))
            args.Append(Engine.AdditionalArguments);

        return args.ToString().Trim();
    }

    // =====================================================
    // COMPLETE COMMAND
    // =====================================================

    public string BuildCommand()
    {
        return $"\"{GetInferenceExecutable()}\" {GetInferenceArguments()}";
    }

    // =====================================================
    // PROCESS START
    // =====================================================

    public ProcessStartInfo BuildProcessStartInfo()
    {
        return new ProcessStartInfo
        {
            FileName = GetInferenceExecutable(),
            Arguments = GetInferenceArguments(),
            WorkingDirectory = InferenceEnginePath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
    }

    public string GetInferenceUrl()
    {
        return $"http://{Parameters.Host}:{Parameters.Port}";
    }

}

