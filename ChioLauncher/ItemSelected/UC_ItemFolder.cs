using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChioLauncher.ItemSelected;


public partial class UC_ItemFolder : UserControl
{
    private string _path = "";

    private Color _baseColor;
    private Color _selectedColor = Color.FromArgb(190, 220, 190);

    private bool _selected;

    public event EventHandler<string>? SelectedChanged;

    public FolderType Type { get; set; }

    public string Path
    {
        get => _path;
        set
        {
            _path = value;
            UpdateName();
        }
    }

    public bool Selected
    {
        get => _selected;
        set
        {
            if (_selected == value)
                return;

            _selected = value;
            UpdateAppearance();
        }
    }

    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            _selectedColor = value;
            UpdateAppearance();
        }
    }

    public UC_ItemFolder()
    {
        InitializeComponent();

        // Capturar color original.
        _baseColor = BackColor;

        // Comportamiento de botón.
        Cursor = Cursors.Hand;

        MouseClick += UC_ItemFolder_MouseClick;
        lb_Name.MouseClick += UC_ItemFolder_MouseClick;
    }

    private void UpdateName()
    {
        switch (Type)
        {
            case FolderType.InferenceEngine:
                {
                    lb_Name.Text = System.IO.Path.GetFileName(_path);
                    break;
                }

            case FolderType.Model:
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(_path);

                    string modelFolder = System.IO.Path.GetFileName(
                        System.IO.Path.GetDirectoryName(_path)
                    );

                    string authorFolder = System.IO.Path.GetFileName(
                        System.IO.Path.GetDirectoryName(
                            System.IO.Path.GetDirectoryName(_path)
                        )
                    );

                    lb_Name.Text = $"{authorFolder} - {modelFolder} - {fileName}";
                    break;
                }
        }
    }

    private void UpdateAppearance()
    {
        BackColor = _selected ? _selectedColor : _baseColor;
        lb_Name.BackColor = BackColor;
    }

    private void UC_ItemFolder_MouseClick(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
            return;

        // Deseleccionar los hermanos.
        if (Parent != null)
        {
            foreach (Control control in Parent.Controls)
            {
                if (control is UC_ItemFolder item && item != this)
                {
                    item.Selected = false;
                }
            }
        }

        // Seleccionar este elemento.
        Selected = true;

        // Notificar selección.
        SelectedChanged?.Invoke(this, _path);
    }

    private void btn_Get_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Path))
        {
            Clipboard.SetText(Path);
        }
    }

}

public enum FolderType
{
    InferenceEngine,
    Model
}
