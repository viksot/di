using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<DragonSettings> settingsFactory;

        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, Func<DragonSettings> settingsFactory)
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.settingsFactory = settingsFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = settingsFactory();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            var container = new StandardKernel();
            // container.Bind<IImageHolder>().ToConstant(imageHolder);
            // container.Bind<DragonSettings>().ToConstant(dragonSettings);
            // container.Get<DragonPainter>().Paint();
            var dragonPainter = dragonPainterFactory.CreateDragonPainter(dragonSettings);
            dragonPainter.Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
}