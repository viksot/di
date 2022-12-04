using System;
using System.Windows.Forms;
using Ninject;
using Ninject.Extensions.Factory;
using FractalPainting.Infrastructure.UiActions;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.App.Fractals;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var container = new StandardKernel();

                // start here
                // container.Bind<TService>().To<TImplementation>();
                #region IUiAction
                container.Bind<IUiAction>().To<SaveImageAction>();
                container.Bind<IUiAction>().To<DragonFractalAction>();
                container.Bind<IUiAction>().To<KochFractalAction>();
                container.Bind<IUiAction>().To<ImageSettingsAction>();
                container.Bind<IUiAction>().To<PaletteSettingsAction>();
                #endregion

                container.Bind<IDragonPainterFactory>().ToFactory();
                container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<KochPainter>().ToSelf().InSingletonScope();


                container.Bind<Form>().To<MainForm>();         
                var form = container.Get<Form>();
                Application.Run(form);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}