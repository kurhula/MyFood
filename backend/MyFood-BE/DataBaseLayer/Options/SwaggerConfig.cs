using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Options
{
    public class SwaggerConfig
    {
        public string RouteDev { get; set; }
        public string RouteProd { get; set; }
        public bool IsDev { get; set; }
        public string Route => IsDev ? RouteDev : RouteProd;
        public string Version { get; set; }
        public string Title { get; set; }
        public string Prefix { get; set; }
        public string RouteTemplate { get; set; }
    }
}
