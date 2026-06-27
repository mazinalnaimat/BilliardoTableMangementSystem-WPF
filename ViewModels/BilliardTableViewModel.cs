using BilliardGameTablesManagement.Helpers;
using BilliardGameTablesManagement.Models;
using BilliardGameTablesManagement.ViewModels;
using System.Windows.Input;
using System.Windows.Threading; // DispatcherTimer still okay here

public class BilliardTableViewModel : BaseViewModel
{
    private readonly DispatcherTimer _timer;
    private TableSession _session;

    public BilliardTableViewModel()
    {
        _session = new TableSession();
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += OnTimerTick;

        StartCommand = new RelayCommand(Start, () => !_session.IsRunning);
        StopCommand = new RelayCommand(Stop, () => _session.IsRunning);
        ResetCommand = new RelayCommand(Reset);
    }

    // ---------- Bound Properties ----------
    public int TableNumber
    {
        get => _session.TableNumber;
        set
        {
            if (_session.TableNumber != value)
            {
                _session.TableNumber = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal RatePerHour
    {
        get => _session.RatePerHour;
        set
        {
            if (_session.RatePerHour != value)
            {
                _session.RatePerHour = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentCost));
            }
        }
    }

    public TimeSpan ElapsedTime => _session.ElapsedTime;
    public decimal CurrentCost => _session.CurrentCost;
    public bool IsRunning => _session.IsRunning;
    public bool IsNotRunning => !_session.IsRunning;

    // ---------- Commands ----------
    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand ResetCommand { get; }

    private void Start()
    {
        _session.StartTime = DateTime.Now;
        _session.IsRunning = true;
        _timer.Start();
        RaiseAllChanged();
    }

    private void Stop()
    {
        _session.AccumulatedTime += DateTime.Now - _session.StartTime;
        _session.IsRunning = false;
        _timer.Stop();
        RaiseAllChanged();
    }

    private void Reset()
    {
        bool wasRunning = _session.IsRunning;
        _timer.Stop();
        _session.IsRunning = false;
        _session.AccumulatedTime = TimeSpan.Zero;
        if (wasRunning)
            _session.StartTime = DateTime.Now; // consistent
        RaiseAllChanged();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        if (_session.IsRunning)
        {
            OnPropertyChanged(nameof(ElapsedTime));
            OnPropertyChanged(nameof(CurrentCost));
        }
    }

    private void RaiseAllChanged()
    {
        OnPropertyChanged(nameof(ElapsedTime));
        OnPropertyChanged(nameof(CurrentCost));
        OnPropertyChanged(nameof(IsRunning));
        OnPropertyChanged(nameof(IsNotRunning));
        // Commands will auto‑requery if you use CommandManager (RelayCommand below)
    }
}