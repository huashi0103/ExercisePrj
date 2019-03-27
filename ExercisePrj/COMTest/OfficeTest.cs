
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading;

[InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00020400-0000-0000-c000-000000000046")]
interface IDispatch
{
    [PreserveSig]
    int GetTypeInfoCount(out int typeInfoCount);

    // Gets the Type information for an object if GetTypeInfoCount returned 1.
    void GetTypeInfo(int typeInfoIndex, int lcid, //out IntPtr hwd);
       [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef =typeof(System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler))]
        out Type typeInfo);

    // Gets the DISPID of the specified member name.
    [PreserveSig]
    int GetDispId(ref Guid riid, ref string name, int nameCount, int lcid,
        out int dispId);
    void GetIDsOfNames([In] ref Guid riid, 
        [In, MarshalAs(UnmanagedType.LPArray)] string[] rgszNames, 
        [In, MarshalAs(UnmanagedType.U4)] int cNames,
        [In, MarshalAs(UnmanagedType.U4)] int lcid,
        [Out, MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);
    void Invoke(int dispIdMember, ref Guid riid,
              uint lcid, ushort dwFlags,
              ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams,
              ref object pVarResult, ref IntPtr pExcepInfo, ref uint pArgErr);

}



class OfficeMonitor
{


    //public static readonly OfficeMonitor Instance = new OfficeMonitor();

    [DllImport("ole32.dll")]
    static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);
    [DllImport("ole32.dll")]
    static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

    public const string msWord = "Word.Application";
    public const string kiWord = "KWPS.Application";
    public const string msExcel = "Excel.Application";
    public const string kiExcel = "KET.Application";
    public const string msPPT = "PowerPoint.Application";
    public const string kiPPT = "KWPP.Application";

    const string ProgPPT = "Presentation ";
    public OfficeMonitor()
    {
    }
    bool print = false;
    #region   //主动获com对象搜索文件
    public List<string> GetFiles()
    {
        List<string> files = new List<string>();
        GetWords(files);
        GetExcels(files);
        GetPPT(files);
        return files;

    }
    void GetWords(List<string> files)
    {
        var app = GetActiveObject(msWord);
        if (app != null)
        {
            try
            {
                foreach (var doc in app.Documents)
                {
                    files.Add(doc.FullName.ToLowerInvariant());
                }
            }
            catch
            {
            }
        }
        app = GetActiveObject(kiWord);
        if (app != null)
        {
            try
            {
                foreach (var doc in app.Documents)
                {
                    files.Add(doc.FullName.ToLowerInvariant());
                }
            }
            catch
            {
            }
        }
    }
    void GetExcels(List<string> files)
    {
        var app = GetActiveObject(msExcel);
        if (app != null)
        {
            try
            {
                foreach (var book in app.Workbooks)
                {
                    files.Add(book.FullName.ToLowerInvariant());
                }
            }
            catch
            {
            }
        }
        app = GetActiveObject(kiExcel);
        if (app != null)
        {
            try
            {
                foreach (var book in app.Workbooks)
                {
                    files.Add(book.FullName.ToLowerInvariant());
                }
            }
            catch
            {
            }
        }
    }
    void GetPPT(List<string> files)
    {
        var app = GetActiveObject(msPPT);
        if (app != null)
        {
            try
            {
                foreach (var doc in app.Presentations)
                {
                    files.Add(doc.FullName.ToLowerInvariant());
                }
            }
            catch
            {
            }
        }
        app = GetActiveObject(kiPPT);
        if (app != null)
        {
            try
            {
                foreach (var doc in app.Presentations)
                {
                    files.Add(doc.FullName.ToLowerInvariant());
                }
            }
            catch
            {
            }
        }
    }
    dynamic GetActiveObject(string key)
    {
        try
        {
            dynamic app = System.Runtime.InteropServices.Marshal.GetActiveObject(key);
            if (app != null)
                return app;
        }
        catch
        { }
        return null;
    }
#endregion   

    List<dynamic> apps = new List<dynamic>();

    //被动扫描
    public Hashtable GetActiveObjectList(string filter)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Hashtable result = new Hashtable();
        IntPtr numFetched = IntPtr.Zero;
        IRunningObjectTable runningObjectTable;
        IEnumMoniker monikerEnumerator;
        IMoniker[] monikers = new IMoniker[1];

        GetRunningObjectTable(0, out runningObjectTable);
        l(string.Format("GetRunningObjectTable{0}", sw.ElapsedMilliseconds));

        runningObjectTable.EnumRunning(out monikerEnumerator);
        l(string.Format("EnumRunning{0}", sw.ElapsedMilliseconds));

        monikerEnumerator.Reset();
        IBindCtx ctx;
        CreateBindCtx(0, out ctx);
        l(string.Format("CreateBindCtx{0}", sw.ElapsedMilliseconds));
        while (monikerEnumerator.Next(1, monikers, numFetched) == 0)
        {
            try
            {
                string runningObjectName;
                monikers[0].GetDisplayName(ctx, null, out runningObjectName);
                l(string.Format("GetDisplayName{0}:{1}", runningObjectName, sw.ElapsedMilliseconds));

                object runningObjectVal = null;
                Action getobj = new Action(() =>
                {
                    runningObjectVal = GetComObject(runningObjectTable, monikers[0]);
                });
                CallWithTimeout(getobj, 1500);
                //var res = runningObjectTable.GetObject(monikers[0], out runningObjectVal);

                l(string.Format("GetObject{0}:{1}", runningObjectName, sw.ElapsedMilliseconds));
                if (runningObjectVal == null) continue;
                result[runningObjectName] = runningObjectVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (numFetched != IntPtr.Zero)
                    Marshal.Release(numFetched);
            }
        }
       // Marshal.ReleaseComObject(ctx);
        sw.Stop();
       Console.WriteLine("GetActiveObjectList:{0}", sw.ElapsedMilliseconds);
        return result;
    }
    object GetComObject(IRunningObjectTable table, IMoniker moniker)
    {
        object runningObjectVal;
        var res = table.GetObject(moniker, out runningObjectVal);
        return runningObjectVal;
    }
    void CallWithTimeout(Action action, int timeoutMilliseconds)
    {
        Action wrappedAction = () =>
        {
            action();
        };

        IAsyncResult result = wrappedAction.BeginInvoke(null, null);
        if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
        {
            wrappedAction.EndInvoke(result);
        }
        else
        {
            throw new TimeoutException();
        }
    }

    public bool EnumFiles( Action <string,object> Fund)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Hashtable result = new Hashtable();
        IntPtr numFetched = IntPtr.Zero;
        IRunningObjectTable runningObjectTable;
        IEnumMoniker monikerEnumerator;
        IMoniker[] monikers = new IMoniker[1];
        IBindCtx ctx;
        CreateBindCtx(0, out ctx);
        ctx.GetRunningObjectTable(out runningObjectTable);

        l(string.Format("GetRunningObjectTable{0}", sw.ElapsedMilliseconds));

        runningObjectTable.EnumRunning(out monikerEnumerator);
        l(string.Format("EnumRunning{0}", sw.ElapsedMilliseconds));

        monikerEnumerator.Reset();

        l(string.Format("CreateBindCtx{0}", sw.ElapsedMilliseconds));
        var ret = true;
        while (monikerEnumerator.Next(1, monikers, numFetched) == 0)
        {
            string runningObjectName;
            object runningObjectVal = null;
            try
            {
                monikers[0].GetDisplayName(ctx, null, out runningObjectName);
                l(string.Format("GetDisplayName{0}:{1}", runningObjectName, sw.ElapsedMilliseconds));
                Action getobj = new Action(() =>
                {
                    runningObjectVal = GetComObject(runningObjectTable, monikers[0]);
                    Fund?.Invoke(runningObjectName, runningObjectVal);
                });
                CallWithTimeout(getobj, 2000);
                l(string.Format("GetObject{0}:{1}", runningObjectName, sw.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                ret = false;
                ReleaseCom(runningObjectVal);
                break;
            }
            finally
            {
                ReleaseCom(runningObjectVal);
            }
        }
        ReleaseCom(monikerEnumerator);
        ReleaseCom(runningObjectTable);
        sw.Stop();
        Console.WriteLine("GetActiveObjectList:{0}", sw.ElapsedMilliseconds);
        return ret;
    }
    void ReleaseCom(object obj)
    {
        if (obj != null)
            Marshal.ReleaseComObject(obj);
    }
    public List<string> GetFilesEx()
    {
        List<string> list = new List<string>();
        Regex re = new Regex(@"^(([a-zA-Z]:\\)|(\\{2}\w+)\$?)((([^/\\\?\*])(\\?))*)$");
        var ret = EnumFiles((progid, obj) =>
        {
            CheckFile(progid, list);
            GetFileEx(list, progid, obj);
        });
        return ret ? list : null;
    }
    Regex re = new Regex(@"^(([a-zA-Z]:\\)|(\\{2}\w+)\$?)((([^/\\\?\*])(\\?))*)$");
    bool CheckFile(string progId,List<string>list)

    {
        progId = progId.ToLowerInvariant();
        if (!re.IsMatch(progId))
            return false;
        var path = System.IO.Path.GetFileName(progId);
        if (path == null) return false;
        var ext = System.IO.Path.GetExtension(path);
        if (IsSpecial(ext))
        {
            if (list.Contains(progId))
                return true;
            list.Add(progId);
        }
        return true;
    }

    void we(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
    }
    void l(string msg)
    {
        if (print)
            Console.WriteLine(msg);
    }
    public List<string> GetFileList(ref bool IsError)
    {
        List<string> list = new List<string>();
        Hashtable runningObjects = new Hashtable();
        Stopwatch sw = new Stopwatch();
        sw.Start();
        try
        {

            runningObjects = GetActiveObjectList(null);
            foreach (DictionaryEntry de in runningObjects)
            {
                string progId = de.Key.ToString().ToLowerInvariant();
                object getObj = de.Value;
                try
                {
                    if (getObj != null && !GetFileEx(list, progId, getObj))
                    {
                        if (!re.IsMatch(progId))
                            continue;
                        var path = System.IO.Path.GetFileName(progId);
                        if (path == null) continue;
                        var ext = System.IO.Path.GetExtension(path);
                        if (IsSpecial(ext))
                        {
                            if (list.Contains(progId))
                                continue;
                            list.Add(progId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    IsError = true;
                    we(ex);
                    break;
                }
            }
        }
        catch (Exception exx)
        {
            IsError = true;
            we(exx);

        }
        finally
        {
            foreach (DictionaryEntry de in runningObjects)
            {
                if (de.Value != null)
                    Marshal.FinalReleaseComObject(de.Value);
            }
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            sw.Stop();
            Console.WriteLine("stopwatch: {0}", sw.ElapsedMilliseconds);


        }
        return list;
    }
    public bool IsSpecial(string ext)
    {
        return IsWordExt(ext) | IsExcelExt(ext) | IsPPTExt(ext);
    }
    //bool GetFile(List<string> list, string progID, object obj)
    //{
    //    if (CheckWord(progID))
    //    {
    //        var ms = obj as MSWord.Application;
    //        if (ms != null)
    //        {
    //            foreach (MSWord.Document e in ms.Documents)
    //            {
    //                var fname = e.FullName;
    //                if (!list.Contains(fname))
    //                {
    //                    list.Add(fname);
    //                }
    //                Marshal.ReleaseComObject(e);
    //            }
    //        }
    //        else
    //        {
    //            var app = obj as KIWord;
    //            foreach (Word.Document e in app.Documents)
    //            {
    //                var fname = e.FullName;
    //                if (!list.Contains(fname))
    //                {
    //                    list.Add(fname);
    //                }
    //                Marshal.ReleaseComObject(e);
    //            }
    //        }
    //        return true;
    //    }
    //    else if (CheckExcel(progID))
    //    {
    //        var mse = obj as MSExcel.Application;
    //        if (mse != null)
    //        {
    //            foreach (MSExcel.Workbook e in mse.Workbooks)
    //            {
    //                var fname = e.FullName;
    //                if (!list.Contains(fname))
    //                {
    //                    list.Add(fname);
    //                }
    //                Marshal.ReleaseComObject(e);
    //            }
    //        }
    //        else
    //        {
    //            var app = obj as KIExcel;
    //            foreach (Excel.Workbook e in app.Workbooks)
    //            {
    //                var fname = e.FullName;
    //                if (!list.Contains(fname))
    //                {
    //                    list.Add(fname);
    //                }
    //                Marshal.ReleaseComObject(e);
    //            }
    //        }
    //        return true;
    //    }
    //    else if (CheckPPT(progID))
    //    {
    //        var msp = obj as MSPPT.Application;
    //        if (msp != null)
    //        {
    //            foreach (MSPPT.Presentation e in msp.Presentations)
    //            {
    //                var fname = e.FullName;
    //                if (!list.Contains(fname))
    //                {
    //                    list.Add(fname);
    //                }
    //                Marshal.ReleaseComObject(e);
    //            }

    //        }
    //        else
    //        {
    //            var app = obj as KIPPT;
    //            foreach (PowerPoint.Presentation e in app.Presentations)
    //            {
    //                var fname = e.FullName;
    //                if (!list.Contains(fname))
    //                {
    //                    list.Add(fname);
    //                }
    //                Marshal.ReleaseComObject(e);
    //            }
    //        }
    //        return true;
    //    }
    //    return false;
    //}
    object obj = new object();
    bool GetFileEx(List<string> list, string progID, object obj)
    {
        if (CheckWord(progID))
        {
            return GetWord(list, progID, obj);
        }
        else if (CheckExcel(progID))
        {
            return GetExcel(list, progID, obj);

        }
        else if (CheckPPT(progID))
        {
            return GetPPt(list, progID, obj);
        }
        return false;
    }

    bool GetWord(List<string> list, string progID, object obj)
    {
        dynamic app = obj;
        dynamic docs = app.Documents;
        try
        {
            //foreach (var dc in docs)
            //{
            //    var fname = dc.FullName.ToLowerInvariant();
            //    if (!list.Contains(fname))
            //    {
            //        list.Add(fname);
            //    }
            //}
            return true;
        }
        catch
        {
            throw;
        }
        finally
        {
            if (docs != null)
            {
                foreach (var p in docs)
                {
                    Marshal.ReleaseComObject(p);
                }
            }

        }
    }
    bool GetPPt(List<string> list, string progID, object obj)
    {
        dynamic app = obj;
        dynamic ppts = app.Presentations;
        try
        {
            foreach (var p in ppts)
            {
                var fname = p.FullName.ToLowerInvariant();
                if (!list.Contains(fname))
                {
                    list.Add(fname);
                }
            }

            return true;
        }
        catch
        {
            throw;
        }
        finally
        {
            if (ppts != null)
            {
                foreach (var p in ppts)
                {
                    Marshal.ReleaseComObject(p);
                }
            }

        }
    }
    bool GetExcel(List<string> list, string progID, object obj)
    {
        dynamic app = obj;
        dynamic books = app.Workbooks;
        try
        {
            foreach (var book in books)
            {
                var fname = book.FullName.ToLowerInvariant();
                if (!list.Contains(fname))
                {
                    list.Add(fname);
                }
            }
            return true;
        }
        catch
        {
            throw;
        }
        finally
        {
            if (books != null)
            {
                foreach (var p in books)
                {
                    Marshal.ReleaseComObject(p);
                }
            }

        }
    }
    string GetStr(string guid)
    {
        return string.Format("!{{{0}}}", guid);
    }
    bool CheckProg(string progid, params string[] appkey)
    {
        foreach (var k in appkey)
        {
            var type = Type.GetTypeFromProgID(k);
            if (type == null)
                continue;
            var chkstr = GetStr(type.GUID.ToString());
            if (string.Compare(progid, chkstr, true) == 0)
                return true;
        }
        return false;
    }
    //判断是否是wordcom对象
    bool CheckWord(string progid)
    {
        return CheckProg(progid, msWord, kiWord);
    }
    bool CheckExcel(string progid)
    {
        return CheckProg(progid, msExcel, kiExcel);
    }
    bool CheckPPT(string progid)
    {
        return CheckProg(progid, msPPT, kiPPT);
    }


    public static bool IsWordExt(string ext)
    {
        return ext == ".doc" || ext == ".docx" || ext == ".wps";
    }

    public static bool IsExcelExt(string ext)
    {
        return ext == ".xls" || ext == ".xlsx" || ext == ".et";
    }

    public static bool IsPPTExt(string ext)
    {
        return ext == ".ppt" || ext == ".pptx" || ext == ".dps";
    }

}