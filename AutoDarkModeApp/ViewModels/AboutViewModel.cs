using System.Diagnostics;
using System.Runtime.InteropServices;
using AutoDarkModeApp.Handlers;
using CommunityToolkit.Mvvm.ComponentModel;
using AdmExtensions = AutoDarkModeLib.Helper;

namespace AutoDarkModeApp.ViewModels;

public partial class AboutViewModel : ObservableRecipient
{
    [ObservableProperty]
    public partial string? CommitHashText { get; set; }

    [ObservableProperty]
    public partial string? SvcVersionText { get; set; }

    [ObservableProperty]
    public partial string? UpdaterVersionText { get; set; }

    [ObservableProperty]
    public partial string? ShellVersionText { get; set; }

    [ObservableProperty]
    public partial string? DotNetVersionText { get; set; }

    [ObservableProperty]
    public partial string? WindowsVersionText { get; set; }

    [ObservableProperty]
    public partial string? ArchText { get; set; }

    public AboutViewModel()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        var versionInfo = new VersionInfo();
        CommitHashText = versionInfo.Commit;
        SvcVersionText = versionInfo.Svc;
        UpdaterVersionText = versionInfo.Updater;
        ShellVersionText = versionInfo.Shell;
        DotNetVersionText = versionInfo.NetCore;
        WindowsVersionText = versionInfo.WindowsVersion;
        ArchText = versionInfo.Arch;
    }

    private class VersionInfo
    {
        public VersionInfo()
        {
            var currentDirectory = AdmExtensions.ExecutionDir;

            Commit = AdmExtensions.CommitHash();
            Svc = ValueOrNotFound(() =>
                FileVersionInfo.GetVersionInfo(currentDirectory + @"\AutoDarkModeSvc.exe").FileVersion);
            Updater = ValueOrNotFound(() =>
                FileVersionInfo.GetVersionInfo(AdmExtensions.ExecutionPathUpdater).FileVersion);
            Shell = ValueOrNotFound(() =>
                FileVersionInfo.GetVersionInfo(currentDirectory + @"\AutoDarkModeShell.exe").FileVersion);
            NetCore = ValueOrNotFound(() => Environment.Version.ToString());
            WindowsVersion = ValueOrNotFound(() => $"{Environment.OSVersion.Version.Build}.{RegistryHandler.GetUbr()}");
            Arch = RuntimeInformation.ProcessArchitecture.ToString();

            static string ValueOrNotFound(Func<string> value)
            {
                try
                {
                    return value();
                }
                catch
                {
                    return "not found";
                }
            }
        }

        public string Commit { get; }
        public string Svc { get; }
        public string Updater { get; }
        public string Shell { get; }
        public string NetCore { get; }
        public string WindowsVersion { get; }
        public string Arch { get; }
    }
}
