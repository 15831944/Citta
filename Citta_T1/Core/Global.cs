﻿using C2.Business.Model;
using C2.Business.Option;
using C2.Controls;
using C2.Controls.Bottom;
using C2.Controls.Flow;
using C2.Controls.Left;
using C2.Controls.Title;
using C2.Controls.Top;
using C2.Forms;

namespace C2.Core
{
    class Global
    {
        private static MainForm mainForm;
        private static ModelTitlePanel modelTitlePanel;
        private static NaviViewControl naviViewControl;
        private static CanvasPanel canvasPanel;
        private static ModelDocumentDao modelDocumentDao;   // 用户模型工具类
        private static FlowControl flowControl;
        private static MyModelControl myModelControl;
        private static RemarkControl remarkControl;
        private static BottomLogControl logView;
        private static OptionDao optionDao;                 // 模型配置工具类
        private static DataSourceControl dataSourceControl; // 左侧数据源面板
        private static BottomConsoleControl bottomPythonConsoleControl; //底层控制台面板
        private static TopToolBarControl topToolBarControl; // 顶层右侧工具栏



        public static MainForm GetMainForm() { return mainForm; }
        public static ModelTitlePanel GetModelTitlePanel() { return modelTitlePanel; }
        public static NaviViewControl GetNaviViewControl() { return naviViewControl; }
        public static CanvasPanel GetCanvasPanel() { return canvasPanel; }
        public static ModelDocumentDao GetModelDocumentDao() { return modelDocumentDao; }

        public static ModelDocument GetCurrentDocument()
        {
            ModelDocument ret = null;
            if (GetModelDocumentDao() != null)
                //ret = GetModelDocumentDao().CurrentDocument;
                ret = (mainForm.MdiClient.ActivedMdiForm as CanvasForm).Document;
            return ret;
        }
        public static FlowControl GetFlowControl() { return flowControl; }
        public static MyModelControl GetMyModelControl() { return myModelControl; }
        public static RemarkControl GetRemarkControl() { return remarkControl; }
        public static BottomLogControl GetLogView() { return logView; }
        public static OptionDao GetOptionDao() { return optionDao; }
        public static DataSourceControl GetDataSourceControl() { return dataSourceControl; }
        public static BottomConsoleControl GetBottomPythonConsoleControl() { return bottomPythonConsoleControl; }
        public static TopToolBarControl GetTopToolBarControl() { return topToolBarControl; }



        public static void SetMainForm(MainForm mf) { mainForm = mf; }
        public static void SetModelTitlePanel(ModelTitlePanel mtp) { modelTitlePanel = mtp; }
        public static void SetNaviViewControl(NaviViewControl nvc) { naviViewControl = nvc; }
        public static void SetCanvasPanel(CanvasPanel cp) { canvasPanel = cp; }
        public static void SetModelDocumentDao(ModelDocumentDao mdd) { modelDocumentDao = mdd; }
        public static void SetFlowControl(FlowControl fc) { flowControl = fc; }
        public static void SetMyModelControl(MyModelControl mmc) { myModelControl = mmc; }
        public static void SetRemarkControl(RemarkControl rc) { remarkControl = rc; }
        public static void SetLogView(BottomLogControl lv) { logView = lv; }
        public static void SetOptionDao(OptionDao od) { optionDao = od; }
        public static void SetDataSourceControl(DataSourceControl dsc) { dataSourceControl = dsc; }
        public static void SetBottomPythonConsoleControl(BottomConsoleControl bpcc) { bottomPythonConsoleControl = bpcc; }
        public static void SetTopToolBarControl(TopToolBarControl ttbc) { topToolBarControl = ttbc; }

        private static string workspaceDirectory;           // 用户模型工作目录
        public static string WorkspaceDirectory { get => workspaceDirectory; set => workspaceDirectory = value; }

        public const float Factor = 1.3F;
        private static string versionType;
        public static string VersionType { get => versionType; set => versionType = value; }
        public const string GreenVersion = "Green";
        public const string GreenPath = "source";
        public const string regPath = @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+.[\w]+)";
    }
}
