using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagementWPF.View.UserControls;

public partial class CustomTextBox : UserControl, INotifyPropertyChanged
{
    public CustomTextBox()
    {
        DataContext = this;
        InitializeComponent();
    }

    private string? _label;
    private bool _isPassword;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Text => _isPassword ? passwordBox.Password : textBox.Text;

    public string? Label
    {
        get { return _label; }
        set 
        { 
            _label = value;
            OnPropertyChanged();
        }
    }

    public bool IsPassword
    {
        get { return _isPassword; }
        set 
        {
            _isPassword = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? caller = null)
    {
        if (caller is null)
            return;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}
