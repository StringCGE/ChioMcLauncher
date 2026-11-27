using ChioLauncher.ItemSelected;
using System.Diagnostics;

namespace ChioLauncher;


public partial class Form1 : Form
{
    private readonly McProcess mc = new McProcess();
    private readonly LlmProcess _llmProcess = new LlmProcess();
    private readonly MinecraftLauncher launcher;

    private readonly ChioLauncherLlmConfig _llmConfig;
    private void UpdateInferenceEngines()
    {
        flp_InferenceEngines.Controls.Clear();

        string folder = tb_InferenceEnginesFolder.Text;

        if (!Directory.Exists(folder))
            return;

        foreach (string directory in Directory.GetDirectories(folder))
        {
            UC_ItemFolder item = new UC_ItemFolder
            {
                Type = FolderType.InferenceEngine,
                Path = directory,

                Selected = _llmConfig != null &&
                    !string.IsNullOrEmpty(_llmConfig.InferenceEnginePath) &&
                    string.Equals(
                        Path.GetFullPath(directory).TrimEnd('\\'),
                        Path.GetFullPath(_llmConfig.InferenceEnginePath).TrimEnd('\\'),
                        StringComparison.OrdinalIgnoreCase
                    )
            };

            item.SelectedChanged += (sender, path) =>
            {
                _llmConfig!.InferenceEnginePath = path;
                AppendConsole($"Inference Engine seleccionado: {path}");
            };

            flp_InferenceEngines.Controls.Add(item);
        }
    }

    private void UpdateModels()
    {
        flp_Models.Controls.Clear();

        string folder = tb_ModelsFolder.Text;

        if (!Directory.Exists(folder))
            return;

        string[] models = Directory.GetFiles(
            folder,
            "*.gguf",
            SearchOption.AllDirectories
        );

        foreach (string modelPath in models)
        {
            UC_ItemFolder item = new UC_ItemFolder
            {
                Type = FolderType.Model,
                Path = modelPath,

                Selected = _llmConfig != null &&
                    !string.IsNullOrEmpty(_llmConfig.ModelSelectedPath) &&
                    string.Equals(
                        Path.GetFullPath(modelPath),
                        Path.GetFullPath(_llmConfig.ModelSelectedPath),
                        StringComparison.OrdinalIgnoreCase
                    )
            };

            item.SelectedChanged += (sender, path) =>
            {
                _llmConfig!.ModelSelectedPath = path;
                AppendConsole($"Modelo seleccionado: {path}");
            };

            flp_Models.Controls.Add(item);
        }
    }


    private void ResizeFolderItems(FlowLayoutPanel flp)
    {
        int itemWidth = flp.ClientSize.Width - 20;

        foreach (Control control in flp.Controls)
        {
            if (control is UC_ItemFolder item)
            {
                item.Width = Math.Max(0, itemWidth);
            }
        }
    }

    public Form1()
    {
        InitializeComponent();
        CleanInferencePort();
        launcher = new MinecraftLauncher(mc);

        _llmConfig = new ChioLauncherLlmConfig
        {
            ModelsPath = @"H:\llm\models",
            InferenceEnginesPath = @"H:\InferenceEngines",

            ModelSelectedPath = @"H:\llm\models\xFanexx\Qwen3-8B-heretic-GGUF\qwen3-8b-heretic-Q8_0.gguf",

            InferenceEnginePath = @"C:\InferenceEngines\llama-b12780-bin-win-cpu-x64",

            Engine = new InferenceEngineSettings
            {
                Type = InferenceEngineType.LlamaCpp,
                ExecutionMode = InferenceExecutionMode.Server,
                ExecutableName = "llama-server.exe"
            },

            Parameters = new InferenceParameters
            {
                ContextSize = 2048,
                Threads = 4,
                BatchSize = 512,
                UBatchSize = 512,
                Host = "127.0.0.1",
                Port = 8080,
                GpuLayers = 0,
                Temperature = 0.8f,
                TopP = 0.95f,
                TopK = 40
            }
        };

        tb_LlmUrl.Text = _llmConfig.GetInferenceUrl();


        UpdateInferenceEngines();
        UpdateModels();

        flp_InferenceEngines.Resize += (s, e) => ResizeFolderItems(flp_InferenceEngines);
        flp_Models.Resize += (s, e) => ResizeFolderItems(flp_Models);
    }


    private Process _inferenceProcess;

    private async Task StartInference()
    {
        try
        {
            btnn_StartModel.Text = "Stop";
            btnn_StartModel.Enabled = true;

            AppendConsole("[PROCESS] Iniciando Inference Engine...");

            await _llmProcess.LaunchAsync(
                _llmConfig,

                onOutput: line =>
                {
                    AppendConsole(line);
                },

                onError: line =>
                {
                    AppendConsole(line);
                },

                onStarted: () =>
                {
                    AppendConsole("[PROCESS] Proceso iniciado.");
                },

                onExited: exitCode =>
                {
                    AppendConsole(
                        $"[PROCESS] Inference Engine finalizado. ExitCode: {exitCode}");
                }
            );
        }
        catch (Exception ex)
        {
            AppendConsole("[FATAL] " + ex.Message);
        }
        finally
        {
            btnn_StartModel.Text = "Start";
        }
    }
    private void AppendConsole(string message)
    {
        if (rtb_OutConsole.IsDisposed || !rtb_OutConsole.IsHandleCreated)
            return;

        if (rtb_OutConsole.InvokeRequired)
        {
            rtb_OutConsole.BeginInvoke(new Action(() =>
                AppendConsole(message)));

            return;
        }

        rtb_OutConsole.SelectionStart = rtb_OutConsole.TextLength;
        rtb_OutConsole.SelectionLength = 0;

        rtb_OutConsole.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");

        rtb_OutConsole.ScrollToCaret();
    }
    private async void btnIniciar_Click(object sender, EventArgs e)
    {
        string minecraftDirectory = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
            ".minecraft");

        string versionDirectory = Path.Combine(
            minecraftDirectory, "versions", "26.3");

        string manifestPath = Path.Combine(
            versionDirectory, "26.3.json");

        string runtimeRoot = Path.Combine(
            minecraftDirectory, "runtime");

        btnIniciar.Enabled = false;
        rtb_OutConsole.Clear();

        try
        {
            WriteLog("========== CHIOLAUNCHER DEBUG ==========");

            // 1. Mostrar rutas
            WriteLog("[PATH] Directorio Minecraft:");
            WriteLog(minecraftDirectory);

            WriteLog("[PATH] Directorio de versión:");
            WriteLog(versionDirectory);

            WriteLog("[PATH] Manifiesto:");
            WriteLog(manifestPath);

            WriteLog("[PATH] Directorio de runtimes:");
            WriteLog(runtimeRoot);

            // 2. Comprobar existencia
            WriteLog("\n========== VALIDACIÓN ==========");

            WriteLog($"Minecraft existe: {Directory.Exists(minecraftDirectory)}");
            WriteLog($"Versión existe: {Directory.Exists(versionDirectory)}");
            WriteLog($"Manifiesto existe: {File.Exists(manifestPath)}");
            WriteLog($"Runtime existe: {Directory.Exists(runtimeRoot)}");

            if (!File.Exists(manifestPath))
                throw new FileNotFoundException(
                    "No se encontró el manifiesto.", manifestPath);

            // 3. Leer manifiesto
            WriteLog("\n========== DETECCIÓN DE JAVA ==========");
            WriteLog($"Leyendo: {manifestPath}");

            JavaRuntimeInfo runtime =
                JavaRuntimeResolver.GetRequiredJavaComponent(
                    manifestPath);

            WriteLog($"Componente requerido: {runtime.Component}");
            WriteLog($"Versión principal: {runtime.MajorVersion}");

            // 4. Buscar ejecutable
            WriteLog("\n========== BÚSQUEDA DE JAVA ==========");
            WriteLog($"Buscando dentro de: {Path.Combine(runtimeRoot, runtime.Component)}");

            string javaPath =
                JavaRuntimeResolver.ResolveJavaPath(
                    runtimeRoot,
                    runtime.Component);

            WriteLog($"[FOUND] Java localizado:");
            WriteLog(javaPath);

            // 5. Ejecutar Java
            WriteLog("\n========== EJECUCIÓN ==========");
            WriteLog("Ejecutando Java -version...");

            var account = new MinecraftAccount
            {
                PlayerName = "Player",
                PlayerUuid = "",
                AccessToken = "",
                UserType = "msa"
            };

            WriteLog("========== INICIANDO MINECRAFT ==========");

            int exitCode = await launcher.LaunchAsync(
                javaPath,
                versionDirectory,
                minecraftDirectory,
                account,
                output => WriteLog("[MINECRAFT] " + output),
                error => WriteLog("[MINECRAFT ERROR] " + error)
            );

            WriteLog($"Minecraft finalizó. Código: {exitCode}");
            WriteLog($"Proceso finalizado. Código: {exitCode}");
        }
        catch (Exception ex)
        {
            WriteLog("\n========== ERROR ==========");
            WriteLog(ex.GetType().FullName ?? "Exception");
            WriteLog(ex.Message);
            WriteLog(ex.StackTrace ?? "Sin StackTrace");
        }
        finally
        {
            WriteLog("\n========== FIN DEBUG ==========");
            btnIniciar.Enabled = true;
        }
    }


    private void btnParar_Click(object sender, EventArgs e)
    {
        mc.Stop();
        WriteLog("Solicitud de detención enviada.");
    }

    private void WriteLog(string message)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => WriteLog(message)));
            return;
        }

        rtb_OutConsole.AppendText(
            message + Environment.NewLine);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        try
        {
            if (_inferenceProcess != null)
            {
                if (!_inferenceProcess.HasExited)
                {
                    _inferenceProcess.Kill(true);
                    _inferenceProcess.WaitForExit();
                }

                _inferenceProcess.Dispose();
                _inferenceProcess = null;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cerrar Inference Engine:\n{ex.Message}");
        }
        finally
        {
            mc.Dispose();
            base.OnFormClosing(e);
        }
    }
    private void btnFindModelsFolder_Click(object sender, EventArgs e)
    {
        fbd_.SelectedPath = tb_ModelsFolder.Text;
        if (fbd_.ShowDialog() == DialogResult.OK)
        {
            tb_ModelsFolder.Text = fbd_.SelectedPath;
        }
    }

    private void btnFindInferenceEnginesFolder_Click(object sender, EventArgs e)
    {
        fbd_.SelectedPath = tb_InferenceEnginesFolder.Text;
        if (fbd_.ShowDialog() == DialogResult.OK)
        {
            tb_InferenceEnginesFolder.Text = fbd_.SelectedPath;
        }
    }






    private void btn_GoToWeb_Click(object sender, EventArgs e)
    {
        OpenInferenceBrowser(_llmConfig);
    }
    private void OpenInferenceBrowser(ChioLauncherLlmConfig config)
    {
        try
        {
            string url = config.GetInferenceUrl();

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al abrir el navegador:\n{ex.Message}");
        }
    }

    private async void btnn_StartModel_Click(object sender, EventArgs e)
    {
        if (_llmProcess.IsRunning)
        {
            StopInference();
        }
        else
        {
            await StartInference();
        }
    }

    private async void StopInference()
    {
        try
        {
            AppendConsole("[PROCESS] Solicitando detención...");

            await _llmProcess.StopAsync();

            AppendConsole("[PROCESS] Proceso detenido.");
            btnn_StartModel.Text = "Start";
        }
        catch (Exception ex)
        {
            AppendConsole("[FATAL] " + ex.Message);
        }
    }

    private async Task CleanInferencePort()
    {
        const int port = 8080;

        try
        {
            AppendConsole($"[CLEANUP] Buscando procesos en el puerto {port}...");

            using Process netstat = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netstat.exe",
                    Arguments = "-ano",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            netstat.Start();

            string output = await netstat.StandardOutput.ReadToEndAsync();
            await netstat.WaitForExitAsync();

            HashSet<int> processIds = new HashSet<int>();

            foreach (string line in output.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries))
            {
                string[] parts = line.Split(
                    (char[])null,
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 5)
                    continue;

                string localAddress = parts[1];
                string state = parts[3];
                string pidText = parts[4];

                // Solo conexiones que escuchan en el puerto.
                if (!localAddress.EndsWith($":{port}"))
                    continue;

                if (!state.Equals(
                    "LISTENING",
                    StringComparison.OrdinalIgnoreCase))
                    continue;

                if (int.TryParse(pidText, out int pid))
                {
                    if (pid != Environment.ProcessId)
                        processIds.Add(pid);
                }
            }

            if (processIds.Count == 0)
            {
                AppendConsole("[CLEANUP] No hay procesos escuchando en el puerto.");
                return;
            }

            foreach (int pid in processIds)
            {
                AppendConsole($"[CLEANUP] Terminando PID: {pid}");

                using Process taskkill = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "taskkill.exe",
                        Arguments = $"/PID {pid} /T /F",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };

                taskkill.Start();

                string result = await taskkill.StandardOutput.ReadToEndAsync();
                string error = await taskkill.StandardError.ReadToEndAsync();

                await taskkill.WaitForExitAsync();

                if (taskkill.ExitCode == 0)
                {
                    AppendConsole($"[CLEANUP] PID {pid} eliminado correctamente.");
                }
                else
                {
                    AppendConsole($"[CLEANUP] No se pudo eliminar PID {pid}: {error}");
                }
            }

            AppendConsole("[CLEANUP] Limpieza finalizada.");
        }
        catch (Exception ex)
        {
            AppendConsole("[CLEANUP ERROR] " + ex.Message);
        }
    }

    private void btn_ForceStop_MouseClick(object sender, MouseEventArgs e)
    {
        CleanInferencePort();
    }
}






