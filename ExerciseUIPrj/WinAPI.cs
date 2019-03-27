using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace ExerciseUIPrj
{
    internal class WinAPI
    {
        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("6d5140c1-7436-11ce-8034-00aa006009fa"), SuppressUnmanagedCodeSecurity]
        public interface _IServiceProvider
        {
            void QueryService(
                    [In, MarshalAs(UnmanagedType.LPStruct)] Guid guid,
                    [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
                    [MarshalAs(UnmanagedType.Interface)] out object Obj);
        }


        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
        internal interface IServiceProvider
        {
            void QueryService([MarshalAs(UnmanagedType.LPStruct)] Guid guidService, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject);
        }

        internal static readonly Guid SID_STopLevelBrowser = new Guid(0x4C96BE40, 0x915C, 0x11CF, 0x99, 0xD3, 0x00, 0xAA, 0x00, 0x4A, 0xE8, 0x37);

        public abstract class ExplorerGUIDs
        {
            public static readonly Guid CGID_DeskBand = new Guid("{EB0FE172-1A3A-11D0-89B3-00A0C90A90AC}");
            public static readonly Guid CLSID_TaskbarList = new Guid("56FDF344-FD6D-11d0-958A-006097C9A090");
            public static readonly Guid IID_IContextMenu = new Guid("{000214e4-0000-0000-c000-000000000046}");
            public static readonly Guid IID_IDataObject = new Guid("{0000010e-0000-0000-C000-000000000046}");
            public static readonly Guid IID_IDropTarget = new Guid("00000122-0000-0000-C000-000000000046");
            public static readonly Guid IID_IDropTargetHelper = new Guid("4657278B-411B-11D2-839A-00C04FD918D0");
            public static readonly Guid IID_IEnumIDList = new Guid("{000214F2-0000-0000-C000-000000000046}");
            public static readonly Guid IID_IExtractImage = new Guid("BB2E617C-0920-11d1-9A0B-00C04FC2D6C1");
            public static readonly Guid IID_IPersistFolder2 = new Guid("{1AC3D9F0-175C-11d1-95BE-00609797EA4F}");
            public static readonly Guid IID_IQueryInfo = new Guid("00021500-0000-0000-c000-000000000046");
            public static readonly Guid IID_IShellBrowser = new Guid("{000214E2-0000-0000-C000-000000000046}");
            public static readonly Guid IID_IShellFolder = new Guid("{000214E6-0000-0000-C000-000000000046}");
            public static readonly Guid IID_ITaskbarList = new Guid("56FDF342-FD6D-11d0-958A-006097C9A090");
            public static readonly Guid IID_ITravelLogStg = new Guid("{7EBFDD80-AD18-11d3-A4C5-00C04F72D6B8}");
            public static readonly Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
            public static readonly Guid IID_IWebBrowserApp = new Guid("{0002DF05-0000-0000-C000-000000000046}");
        }

        public enum FOLDERVIEWMODE
        {
            FVM_DETAILS = 4,
            FVM_ICON = 1,
            FVM_LIST = 3,
            FVM_SMALLICON = 2,
            FVM_THUMBNAIL = 5,
            FVM_THUMBSTRIP = 7,
            FVM_TILE = 6
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        [Flags]
        public enum SBSP : uint
        {
            DEFBROWSER = 0x00000000,
            SAMEBROWSER = 0x00000001,
            NEWBROWSER = 0x00000002,
            DEFMODE = 0x00000000,
            OPENMODE = 0x00000010,
            EXPLOREMODE = 0x00000020,
            HELPMODE = 0x00000040,
            NOTRANSFERHIST = 0x00000080,
            AUTONAVIGATE = 0x00000100,
            RELATIVE = 0x00001000,
            PARENT = 0x00002000,
            NAVIGATEBACK = 0x00004000,
            NAVIGATEFORWARD = 0x00008000,
            ALLOW_AUTONAVIGATE = 0x00010000,
            KEEPSAMETEMPLATE = 0x00020000,
            KEEPWORDWHEELTEXT = 0x00040000,
            ACTIVATE_NOFOCUS = 0x00080000,
            CREATENOHISTORY = 0x00100000,
            PLAYNOSOUND = 0x00200000,
            CALLERUNTRUSTED = 0x00800000,
            TRUSTFIRSTDOWNLOAD = 0x01000000,
            UNTRUSTEDFORDOWNLOAD = 0x02000000,
            NOAUTOSELECT = 0x04000000,
            WRITENOHISTORY = 0x08000000,
            TRUSTEDFORACTIVEX = 0x10000000,
            FEEDNAVIGATION = 0x20000000,
            REDIRECT = 0x40000000,
            INITIATEDBYHLINKFRAME = 0x80000000,
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("1AC3D9F0-175C-11d1-95BE-00609797EA4F")]
        public interface IPersistFolder2
        {
            [PreserveSig]
            int GetClassID(out Guid pClassID);

            [PreserveSig]
            int Initialize(IntPtr pidl);

            [PreserveSig]
            int GetCurFolder(out IntPtr ppidl);
        }

        [ComImport, Guid("000214E2-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
        public interface IShellBrowser
        {
            [PreserveSig]
            int GetWindow(out IntPtr phwnd);

            [PreserveSig]
            int ContextSensitiveHelp(bool fEnterMode);

            [PreserveSig]
            int InsertMenusSB(IntPtr hmenuShared, IntPtr lpMenuWidths);

            [PreserveSig]
            int SetMenuSB(IntPtr hmenuShared, IntPtr holemenuRes, IntPtr hwndActiveObject);

            [PreserveSig]
            int RemoveMenusSB(IntPtr hmenuShared);

            [PreserveSig]
            int SetStatusTextSB([MarshalAs(UnmanagedType.BStr)] string pszStatusText);

            [PreserveSig]
            int EnableModelessSB(bool fEnable);

            [PreserveSig]
            int TranslateAcceleratorSB(MSG pmsg, ushort wID);

            [PreserveSig]
            int BrowseObject(IntPtr pidl, SBSP wFlags);

            [PreserveSig]
            int GetViewStateStream(uint grfMode, out IntPtr ppStrm);

            [PreserveSig]
            int GetControlWindow(uint id, out IntPtr phwnd);

            [PreserveSig]
            int SendControlMsg(uint id, uint uMsg, IntPtr wParam, IntPtr lParam, out IntPtr pret);

            [PreserveSig]
            int QueryActiveShellView(out IShellView ppshv);

            [PreserveSig]
            int OnViewWindowActive(IntPtr pshv);

            [PreserveSig]
            int SetToolbarItems(IntPtr lpButtons, uint nButtons, uint uFlags);
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("cde725b0-ccc9-4519-917e-325d72fab4ce")]
        public interface IFolderView
        {
            [PreserveSig]
            int GetCurrentViewMode(ref FOLDERVIEWMODE pViewMode);

            [PreserveSig]
            int SetCurrentViewMode(FOLDERVIEWMODE ViewMode);

            [PreserveSig]
            int GetFolder(ref Guid riid, out IPersistFolder2 ppv);

            [PreserveSig]
            int Item(int iItemIndex, out IntPtr ppidl);

            [PreserveSig]
            int ItemCount(uint uFlags, out int pcItems);

            [PreserveSig]
            int Items(uint uFlags, [In] ref Guid riid, out object ppv);

            [PreserveSig]
            int GetSelectionMarkedItem(out int piItem);

            [PreserveSig]
            int GetFocusedItem(out int piItem);

            [PreserveSig]
            int GetItemPosition(IntPtr pidl, out POINT ppt);

            [PreserveSig]
            int GetSpacing(ref POINT ppt);

            [PreserveSig]
            int GetDefaultSpacing(ref POINT ppt);

            [PreserveSig]
            int GetAutoArrange();

            [PreserveSig]
            int SelectItem(int iItem, int dwFlags);

            [PreserveSig]
            int SelectAndPositionItems(uint cidl, IntPtr apidl, IntPtr apt, int dwFlags);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FOLDERSETTINGS
        {
            public FOLDERVIEWMODE ViewMode;
            public int fFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(Rectangle rectangle)
            {
                left = rectangle.X;
                top = rectangle.Y;
                right = rectangle.Right;
                bottom = rectangle.Bottom;
            }

            public int Width
            {
                get
                {
                    return Math.Abs((right - left));
                }
            }

            public int Height
            {
                get
                {
                    return (bottom - top);
                }
            }

            public Rectangle ToRectangle()
            {
                return new Rectangle(left, top, Width, Height);
            }
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E3-0000-0000-C000-000000000046")]
        public interface IShellView
        {
            [PreserveSig]
            int GetWindow(out IntPtr phwnd);

            [PreserveSig]
            int ContextSensitiveHelp(bool fEnterMode);

            [PreserveSig]
            int TranslateAccelerator(ref MSG pmsg);

            [PreserveSig]
            int EnableModeless(bool fEnable);

            [PreserveSig]
            int UIActivate(uint uState);

            [PreserveSig]
            int Refresh();

            [PreserveSig]
            int CreateViewWindow(IShellView psvPrevious, ref FOLDERSETTINGS pfs, ref IShellBrowser psb, ref RECT prcView, out IntPtr phWnd);

            [PreserveSig]
            int DestroyViewWindow();

            [PreserveSig]
            int GetCurrentInfo(ref FOLDERSETTINGS lpfs);

            [PreserveSig]
            int AddPropertySheetPages(int dwReserved, IntPtr pfn, IntPtr lparam);

            [PreserveSig]
            int SaveViewState();

            [PreserveSig]
            int SelectItem(IntPtr pidlItem, uint uFlags);

            [PreserveSig]
            int GetItemObject(uint uItem, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
        }

    }
   
}
[StructLayout(LayoutKind.Sequential)]
public struct PIDLIST_ABSOLUTE
{
    public IntPtr Ptr;
}

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
public interface IShellItem
{
    void BindToHandler(
        IntPtr pbc,
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid bhid,
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

    void GetParent(out IShellItem ppsi);

    void GetDisplayName(int sigdnName, out IntPtr ppszName);

    void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);

    [PreserveSig]
    int Compare(IShellItem psi, uint hint, out int piOrder);
}

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("70629033-e363-4a28-a567-0db78006e6d7")]
public interface IEnumShellItems
{
    void Next(int celt, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IShellItem[] rgelt, out int pceltFetched);

    void Skip(int celt);

    void Reset();

    void Clone(out IEnumShellItems ppenum);
}

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("b63ea76d-1f85-456f-a19c-48159efa858b")]
public interface IShellItemArray
{
    void BindToHandler(
        IntPtr pbc,
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid bhid,
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppvOut);

    void GetPropertyStore(
        uint flags,
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

    void GetPropertyDescriptionList(
        IntPtr keyType,
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

    void GetAttributes(uint AttribFlags, uint sfgaoMask, out uint psfgaoAttribs);

    void GetCount(out int pdwNumItems);

    void GetItemAt(int dwIndex, out IShellItem ppsi);

    void EnumItems(out IEnumShellItems ppenumShellItems);
}

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("000214e4-0000-0000-c000-000000000046")]
public interface IContextMenu
{
    [PreserveSig]
    int QueryContextMenu(IntPtr hMenu, uint indexMenu, int idCmdFirst, int idCmdLast, uint uFlags);

    void InvokeCommand([In] ref CMINVOKECOMMANDINFOEX pici);

    [PreserveSig]
    int GetCommandString(UIntPtr idCmd, uint uType, IntPtr pReserved, IntPtr pszName, int cchMax);
}

[StructLayout(LayoutKind.Sequential)]
public struct CMINVOKECOMMANDINFOEX
{
    public int cbSize;
    public uint fMask;
    public IntPtr hwnd;

    // Non-unicode verbs (are there unicode verbs?)
    [MarshalAs(UnmanagedType.LPStr)]
    public string lpVerb;

    [MarshalAs(UnmanagedType.LPStr)]
    public string lpParameters;

    [MarshalAs(UnmanagedType.LPStr)]
    public string lpDirectory;

    public int nShow;
    public uint dwHotKey;
    public IntPtr hIcon;

    [MarshalAs(UnmanagedType.LPStr)]
    public string lpTitle;

    // Use CMIC_MASK_UNICODE
    [MarshalAs(UnmanagedType.LPWStr)]
    public string lpVerbW;

    [MarshalAs(UnmanagedType.LPWStr)]
    public string lpParametersW;

    [MarshalAs(UnmanagedType.LPWStr)]
    public string lpDirectoryW;

    [MarshalAs(UnmanagedType.LPWStr)]
    public string lpTitleW;

    public int ptInvokeX;
    public int ptInvokeY;
}

// Windows >= Vista
public static class ShellItemUtilities
{
    public static readonly Guid FOLDERID_RecycleBinFolder = new Guid("b7534046-3ecb-4c18-be4e-64cd4cb7d6ac");

    public static readonly Guid BHID_EnumItems = new Guid("94f60519-2850-4924-aa5a-d15e84868039");

    public static readonly Guid BHID_SFUIObject = new Guid("3981e225-f559-11d3-8e3a-00c04f6837d5");

    // From Windows 7
    [DllImport("Shell32.dll", ExactSpelling = true, SetLastError = false)]
    public static extern int SHGetKnownFolderItem(
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
        uint dwFlags,
        IntPtr hToken,
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
        out IShellItem ppv);

    // For Windows Vista
    [DllImport("Shell32.dll", ExactSpelling = true, SetLastError = false)]
    public static extern int SHCreateItemFromIDList(PIDLIST_ABSOLUTE pidl, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IShellItem ppv);

    // For Windows Vista
    [DllImport("Shell32.dll", ExactSpelling = true, SetLastError = false)]
    public static extern int SHGetKnownFolderIDList(
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
        uint dwFlags,
        IntPtr hToken,
        out PIDLIST_ABSOLUTE ppidl);

    // From Windows Vista
    [DllImport("Shell32.dll", ExactSpelling = true, SetLastError = false)]
    public static extern int SHGetIDListFromObject([MarshalAs(UnmanagedType.Interface)] object punk, out PIDLIST_ABSOLUTE ppidl);

    [DllImport("Shell32.dll", ExactSpelling = true, SetLastError = false)]
    public static extern int SHCreateShellItemArrayFromIDLists(int cidl, [In] PIDLIST_ABSOLUTE[] rgpidl, out IShellItemArray ppsiItemArray);

    public static IEnumerable<IShellItem> Enumerate(this IShellItem si)
    {
        object pesiTemp;
        si.BindToHandler(IntPtr.Zero, BHID_EnumItems, typeof(IEnumShellItems).GUID, out pesiTemp);
        var pesi = (IEnumShellItems)pesiTemp;

        var items = new IShellItem[10];

        while (true)
        {
            int fetched;

            pesi.Next(1, items, out fetched);

            if (fetched == 0)
            {
                break;
            }

            yield return items[0];
        }
    }

    public static class ContextMenuUtilities
    {
        [DllImport("User32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateMenu();

        [DllImport("User32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DestroyMenu(IntPtr hMenu);

        [DllImport("User32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("User32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern uint GetMenuItemID(IntPtr hMenu, int nPos);

        [DllImport("User32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetMenuStringW", ExactSpelling = true, SetLastError = true)]
        public static extern int GetMenuString(IntPtr hMenu, uint uIDItem, [Out] StringBuilder lpString, int nMaxCount, uint uFlag);

        public static string[] GetVerbs(IContextMenu cm, bool ansi = true)
        {
            IntPtr menu = IntPtr.Zero;

            try
            {
                menu = CreateMenu();

                // It isn't clear why short.MaxValue, but 0x7FFF is very used around the .NET!
                int res = cm.QueryContextMenu(menu, 0, 0, short.MaxValue, 0);

                if (res < 0)
                {
                    Marshal.ThrowExceptionForHR(res);
                }

                //var sb = new StringBuilder(128);

                int count = GetMenuItemCount(menu);

                var verbs = new List<string>(count);

                var handle = default(GCHandle);

                try
                {
                    var bytes = new byte[ansi ? 128 : 256];

                    handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    IntPtr ptr = handle.AddrOfPinnedObject();

                    for (int i = 0; i < count; i++)
                    {
                        uint id = GetMenuItemID(menu, i);

                        if (id == uint.MaxValue)
                        {
                            continue;
                        }

                        //GetMenuString(menu, (uint)i, sb, sb.Capacity, 0x00000400 /* MF_BYPOSITION */);
                        //string description = sb.ToString();
                        //sb.Clear();

                        res = cm.GetCommandString((UIntPtr)id, ansi ? (uint)0x00000002 /* GCS_VALIDATEA */ : 0x00000006 /* GCS_VALIDATEW */, IntPtr.Zero, ptr, bytes.Length);

                        if (res < 0)
                        {
                            continue;
                        }

                        if (res == 0)
                        {
                            res = cm.GetCommandString((UIntPtr)id, ansi ? (uint)0x00000000 /* GCS_VERBA */ : 0x00000004 /* GCS_VERBW */, IntPtr.Zero, ptr, bytes.Length);

                            if (res < 0)
                            {
                                Marshal.ThrowExceptionForHR(res);
                            }

                            verbs.Add(ansi ? Marshal.PtrToStringAnsi(ptr) : Marshal.PtrToStringUni(ptr));
                        }
                    }
                }
                finally
                {
                    if (handle.IsAllocated)
                    {
                        handle.Free();
                    }
                }

                return verbs.ToArray();
            }
            finally
            {
                if (menu != IntPtr.Zero)
                {
                    DestroyMenu(menu);
                }
            }
        }
    }
}