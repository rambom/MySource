using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Com.Dcjet.Plugin.Command;
using Com.Dcjet.Plugin.Entity;
using Com.Dcjet.Plugin.Helper;
using Com.Dcjet.Plugin.Manager;
using Com.Dcjet.Plugin.Menu;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace Com.Dcjet.Plugin.AddIn
{
    /// <summary>用于实现外接程序的对象。</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Main : IDTExtensibility2, IDTCommandTarget
    {

        /// <summary>实现外接程序对象的构造函数。请将您的初始化代码置于此方法内。</summary>
        public Main()
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnConnection 方法。接收正在加载外接程序的通知。</summary>
        /// <param term='application'>宿主应用程序的根对象。</param>
        /// <param term='connectMode'>描述外接程序的加载方式。</param>
        /// <param term='addInInst'>表示此外接程序的对象。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (EnvDTE.AddIn)addInInst;
            bool abc = _addInInstance.Connected;
            //初始化工具箱
            _dteHelper = new DTEHelper(_applicationObject, _addInInstance);

            if (!_initAlreadySetup && (ext_ConnectMode.ext_cm_UISetup == connectMode
                || ext_ConnectMode.ext_cm_Startup == connectMode
                || ext_ConnectMode.ext_cm_AfterStartup == connectMode))
            {
                CommandBars commandBars = (CommandBars)_applicationObject.CommandBars;

                try
                {
                    IDictionary<string, EnvDTE.Command> dicCmd = new Dictionary<string, EnvDTE.Command>(3);

                    //注册插件命令
                    foreach (var command in CommandManager.GetAllCommands())
                    {
                        if (!Attribute.IsDefined(command.GetType(), typeof(CommandAttribute)))
                        {
                            continue;
                        }
                        //命令基本信息
                        CommandAttribute cmdAttribute =
                            Attribute.GetCustomAttribute(command.GetType(), typeof(CommandAttribute)) as CommandAttribute;

                        if (null == cmdAttribute || dicCmd.ContainsKey(cmdAttribute.Key))
                        {
                            continue;
                        }

                        try
                        {
                            //注册命令
                            EnvDTE.Command cmd = _dteHelper.RegisterCommand(cmdAttribute.Key, cmdAttribute.Caption, cmdAttribute.Tooltip, cmdAttribute.UseMsoButton, cmdAttribute.IconResource, CommonHelper.Convert2VsCmdStatus(cmdAttribute.InitViewStatus));

                            dicCmd.Add(cmdAttribute.Key, cmd);
                        }
                        catch
                        {
                            // 可能是由于同名的命令已经存在了，忽略该异常
                        }

                    }

                    foreach (IMenuBar menuBar in MenuManager.GetAddinMenu())
                    {
                        if (!Attribute.IsDefined(menuBar.GetType(), typeof(MenuAttribute)))
                        {
                            continue;
                        }

                        //菜单信息
                        MenuAttribute menuAttribute =
                            Attribute.GetCustomAttribute(menuBar.GetType(), typeof(MenuAttribute)) as MenuAttribute;

                        //附加栏信息
                        AdditionalBarAttribute[] additionalBarAttribute =
                            Attribute.GetCustomAttributes(menuBar.GetType(), typeof(AdditionalBarAttribute)) as
                            AdditionalBarAttribute[];

                        if (null == menuAttribute || null == additionalBarAttribute)
                            continue;

                        //缓存菜单项，以便卸载
                        _dicMenu.Add(menuAttribute.Key, menuBar);

                        foreach (var s in additionalBarAttribute)
                        {
                            CommandBar menuBarCommandBar = commandBars[s.VsCommandBar];
                            //添加菜单
                            CommandBarControl menu = menuBarCommandBar.Controls.Add(menuAttribute.ControlType,
                                                                                    Type.Missing, Type.Missing,
                                                                                    menuAttribute.Position <= 0 ? menuBarCommandBar.Controls.Count : menuAttribute.Position,
                                                                                    true);
                            //菜单名
                            menu.Caption = menuAttribute.Caption;
                            //提示文本
                            menu.TooltipText = menuAttribute.Tooltip;

                            foreach (ICommand command in menuBar.CommandList)
                            {
                                if (!Attribute.IsDefined(command.GetType(), typeof(CommandAttribute)))
                                {
                                    continue;
                                }


                                //命令基本信息
                                CommandAttribute cmdAttribute =
                                    Attribute.GetCustomAttribute(command.GetType(), typeof(CommandAttribute)) as CommandAttribute;

                                if (null == cmdAttribute)
                                    continue;

                                string strCmdName = FormatCommandName(cmdAttribute.Key);

                                try
                                {
                                    //给菜单附加命令
                                    if (dicCmd.ContainsKey(cmdAttribute.Key))
                                    {
                                        if (menu is CommandBarPopup)
                                        {
                                            dicCmd[cmdAttribute.Key].AddControl(((CommandBarPopup)menu).CommandBar,
                                                                                     cmdAttribute.Position);
                                        }
                                        else
                                        {
                                            dicCmd[cmdAttribute.Key].AddControl(menu, cmdAttribute.Position);
                                        }

                                        if (!_dicCommand.ContainsKey(strCmdName))
                                        {
                                            command.DteHelper = _dteHelper;
                                            _dicCommand.Add(strCmdName, command);
                                        }
                                    }

                                }
                                catch
                                {
                                }
                                /*
                                CommandBarButton cbc = MsoControlType.msoControlPopup == menuBar.CommandMenu.ControlType
                                                           ? DTEHelper.GetInstance().AddButtonToPopup(
                                                               menu as CommandBarPopup, command.CommandButton.Position,
                                                               command.CommandButton.Caption,
                                                               command.CommandButton.Tooltip)
                                                           : DTEHelper.GetInstance().AddButtonToCmdBar(
                                                               menu as CommandBar, command.CommandButton.Position,
                                                               command.CommandButton.Caption,
                                                               command.CommandButton.Tooltip);

                                //参数区分命令来源
                                cbc.Parameter = string.Format("{0}.{1}.{2}", kvp.Key, menuBar.CommandMenu.Key,
                                                              command.CommandButton.Key);
                                //添加按钮事件
                                cbc.Click += (CommandBarButton ctrl, ref bool @default) =>
                                                 {
                                                     //第1位是窗口名,每2位是菜单名,每3位是命令名
                                                     string[] strPara = ctrl.Parameter.Split('.');
                                                     //取出菜单
                                                     IMenuBar customMenu = this._dicMenuBar[strPara[1]];

                                                     foreach (ICommand cmd in customMenu.CommandList)
                                                     {
                                                         if (strPara[2].Equals(cmd.CommandButton.Key))
                                                         {
                                                             CommandResult result = cmd.Exec(ctrl.Parameter);
                                                             if (0 != result.ErrCode)
                                                                 MessageBox.Show(result.ErrMsg);
                                                             break;
                                                         }
                                                     }
                                                 }; 
                                 */

                            }
                        }
                    }

                    //插件加载标识
                    _initAlreadySetup = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnDisconnection 方法。接收正在卸载外接程序的通知。</summary>
        /// <param term='disconnectMode'>描述外接程序的卸载方式。</param>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
            if ((disconnectMode == ext_DisconnectMode.ext_dm_HostShutdown || disconnectMode == ext_DisconnectMode.ext_dm_UserClosed))
            {
                try
                {
                    UninstallPlugin();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 移除插件
        /// </summary>
        private void UninstallPlugin()
        {
            //移除命令
            foreach (var command in _dicCommand)
            {
                EnvDTE.Command cmd = _applicationObject.Commands.Item(command.Key);
                cmd.Delete();
            }

            //移除菜单
            foreach (var menu in _dicMenu)
            {
                MenuAttribute menuAttribute =
                    Attribute.GetCustomAttribute(menu.Value.GetType(), typeof(MenuAttribute)) as MenuAttribute;

                AdditionalBarAttribute[] additionalBarAttributes =
                    Attribute.GetCustomAttributes(menu.Value.GetType(), typeof(AdditionalBarAttribute)) as
                    AdditionalBarAttribute[];

                foreach (var s in additionalBarAttributes)
                {
                    CommandBar cmdBar = ((CommandBars)_applicationObject.CommandBars)[s.VsCommandBar];
                    cmdBar.Controls[menuAttribute.Caption].Delete();
                }
            }
            //插件卸载
            _initAlreadySetup = false;
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnAddInsUpdate 方法。当外接程序集合已发生更改时接收通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnStartupComplete 方法。接收宿主应用程序已完成加载的通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnBeginShutdown 方法。接收正在卸载宿主应用程序的通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }

        /// <summary>实现 IDTCommandTarget 接口的 QueryStatus 方法。此方法在更新该命令的可用性时调用</summary>
        /// <param term='commandName'>要确定其状态的命令的名称。</param>
        /// <param term='neededText'>该命令所需的文本。</param>
        /// <param term='viewStatus'>该命令在用户界面中的状态。</param>
        /// <param term='commandText'>neededText 参数所要求的文本。</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText != vsCommandStatusTextWanted.vsCommandStatusTextWantedNone) return;

            status = vsCommandStatus.vsCommandStatusSupported;

            if (_dicCommand.ContainsKey(commandName))
            {
                CommandViewStatus cmdViewStatus = _dicCommand[commandName].GetStatus();

                status = CommonHelper.Convert2VsCmdStatus(cmdViewStatus);
            }
        }

        /// <summary>实现 IDTCommandTarget 接口的 Exec 方法。此方法在调用该命令时调用。</summary>
        /// <param term='commandName'>要执行的命令的名称。</param>
        /// <param term='executeOption'>描述该命令应如何运行。</param>
        /// <param term='varIn'>从调用方传递到命令处理程序的参数。</param>
        /// <param term='varOut'>从命令处理程序传递到调用方的参数。</param>
        /// <param term='handled'>通知调用方此命令是否已被处理。</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (_dicCommand.ContainsKey(commandName))
                {
                    CommandResult cmdResult = _dicCommand[commandName].Exec(commandName);
                    if (CommandExecStatus.Succeed != cmdResult.ErrCode)
                    {
                        MessageBox.Show(cmdResult.ErrMsg);
                    }
                }
                handled = true;
            }
        }

        /// <summary>
        ///格式化命令名
        /// </summary>
        /// <param name="cmdName">命令名</param>
        /// <returns></returns>
        private string FormatCommandName(string cmdName)
        {
            return string.Format("{0}.{1}", this.GetType().FullName, cmdName);
        }


        private DTE2 _applicationObject;
        private EnvDTE.AddIn _addInInstance;
        /// <summary>
        /// dte工具
        /// </summary>
        private DTEHelper _dteHelper;
        /// <summary>
        /// 插件安装标识
        /// </summary>
        private static bool _initAlreadySetup = false;
        /// <summary>
        /// 注册命令集合
        /// </summary>
        private static readonly IDictionary<string, ICommand> _dicCommand = new Dictionary<string, ICommand>(3);
        /// <summary>
        /// 注册菜单集合
        /// </summary>
        private static readonly IDictionary<string, IMenuBar> _dicMenu = new Dictionary<string, IMenuBar>(1);
    }
}