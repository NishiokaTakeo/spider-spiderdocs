using System;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
//using SpiderDocsApplication = SpiderDocsForms.SpiderDocsApplication;
using SpiderDocsModule;
using SpiderDocsForms;
using NLog;

//---------------------------------------------------------------------------------
namespace AddInModules
{
	public abstract class SpiderDocsRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
	{
		RibbonMenu menuSaveSpider;
		RibbonButton buttonSaveWorkspace;
		Timer timer = new Timer();
		bool PrevLoginState = false;
        bool inprocess = false;
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        //---------------------------------------------------------------------------------		
        public SpiderDocsRibbon(RibbonFactory factory) : base(factory)
		{
		}

//---------------------------------------------------------------------------------
		protected void initialize(RibbonMenu menu, RibbonButton button)
		{
            logger.Trace("Begin");
			menuSaveSpider = menu;
			buttonSaveWorkspace = button;

			timer.Tick += new EventHandler(TimerEventProcessor);
			timer.Interval = 2000;
			timer.Start();

            logger.Trace("End");
        }

//---------------------------------------------------------------------------------
		abstract public void SetMenuEnabled();
		public void SetMenuEnabled(string FullName, RibbonMenu menu = null, RibbonButton button = null)
		{
            if (inprocess) return;

            try
            {
                inprocess = true;

                if(menu == null)
                    menu = menuSaveSpider;

                if(button == null)
                    button = buttonSaveWorkspace;

                if(SpiderDocsApplication.IsLoggedIn)
                {
                    if(!PrevLoginState)
                        SpiderDocsApplication.LoadAllSettings();

                    if(menu != null)
                        menu.Enabled = true;

                    if(button != null)
                        button.Enabled = AddInModule.IsSaveWorkspaceEnabled(FullName);

                    PrevLoginState = true;

                }else
                {
                    SetMenuDisabled(menu, button);
                    PrevLoginState = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                inprocess = false;
            }
		}

//---------------------------------------------------------------------------------
		void SetMenuDisabled(RibbonMenu menu, RibbonButton button)
		{
			if(menu != null)
				menu.Enabled = false;

			if(button != null)
				button.Enabled = false;
		}
		
//---------------------------------------------------------------------------------
		private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
		{
            logger.Trace("Begin");
            timer.Stop();
			SetMenuEnabled();
	        timer.Enabled = true;
            logger.Trace("End");
        }

//---------------------------------------------------------------------------------
	}
}
