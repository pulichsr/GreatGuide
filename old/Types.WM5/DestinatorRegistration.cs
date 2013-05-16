using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Nucleo.GoodGuide.Types
{
  public class DestinatorRegistration
  {
    public const string DestIdArrayId = "{0C82A99D-78A0-4188-B807-224A6D84CCF6}";

    [DllImport("destsdk.dll")]
    public static extern void DllRegisterServer();

    public Boolean IsRegistered
    {
      get
      {
        rkCLSID = rkClassesRoot.OpenSubKey("CLSID");
        rkDestIdArray = rkCLSID.OpenSubKey(DestIdArrayId);
        if (rkDestIdArray == null)
          return false;

        return true;
      }
    }

    public void Register()
    {
      DllRegisterServer();
    }

    private readonly RegistryKey rkClassesRoot = Registry.ClassesRoot;
    private RegistryKey rkCLSID = null;
    private RegistryKey rkDestIdArray = null;
  }
}