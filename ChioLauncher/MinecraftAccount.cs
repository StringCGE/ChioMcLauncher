using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChioLauncher;

public sealed class MinecraftAccount
{
    public string? PlayerName { get; set; }
    public string? PlayerUuid { get; set; }

    // Se obtendrán mediante autenticación oficial.
    public string? AccessToken { get; set; }
    public string? ClientId { get; set; }

    public string? UserType { get; set; }
}

