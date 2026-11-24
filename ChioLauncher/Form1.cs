namespace ChioLauncher;


public partial class Form1 : Form
{
    private readonly McProcess mc = new McProcess();
    private readonly MinecraftLauncher launcher;


    public Form1()
    {
        InitializeComponent();
        launcher = new MinecraftLauncher(mc);
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
        rtbOutConsole.Clear();

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

        rtbOutConsole.AppendText(
            message + Environment.NewLine);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        mc.Dispose();
        base.OnFormClosing(e);
    }
}






