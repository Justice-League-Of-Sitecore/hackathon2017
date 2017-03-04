using System.Collections.Generic;
using System.Linq;
using Ignition.Foundation.Core.Mvc;
using JLS.Feature.Heros.Areas.Heros.Models;

namespace JLS.Feature.Heros.Areas.Heros.ViewModels
{
    public class HeroBannerViewModel : IgnitionViewModel
    {
        public IEnumerable<IHeroCarousel> Carousel { get; set; } = Enumerable.Empty<IHeroCarousel>();
    }
}