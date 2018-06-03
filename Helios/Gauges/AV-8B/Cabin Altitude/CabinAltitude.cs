﻿//  Copyright 2014 Craig Courtney
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace GadrocsWorkshop.Helios.Gauges.AV8B.cabinAltitude
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;

    [HeliosControl("Helios.AV8B.cabinAltitude", "Cabin Altitude", "AV-8B Gauges (Deprecated)", typeof(GaugeRenderer))]
    public class cabinAltitude : BaseGauge
    {
        private HeliosValue _cabinAltitude;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _calibrationPoints;

        public cabinAltitude()
            : base("cabinAltitude", new Size(182, 188))
        {
            //Components.Add(new GaugeImage("{Helios}/Gauges/AV-8B/VVI/vvi_faceplate.xaml", new Rect(32d, 38d, 300d, 300d)));
            _needle = new GaugeNeedle("{Helios}/Gauges/AV-8B/Common/needle_a.xaml", new Point(91d, 94d), new Size(11, 82), new Point(5, 65), 180d);
            Components.Add(_needle);

            //Components.Add(new GaugeImage("{Helios}/Gauges/A-10/Common/gauge_bezel.png", new Rect(0d, 0d, 364d, 376d)));

            _cabinAltitude = new HeliosValue(this, new BindingValue(0d), "", "Cabin Altitude", "This is the pressurisation of the aircraft cabin in feet", "(0 to 50,000)", BindingValueUnits.Feet);
            _cabinAltitude.Execute += new HeliosActionHandler(cabinAltitude_Execute);
            Actions.Add(_cabinAltitude);

            _calibrationPoints = new CalibrationPointCollectionDouble(0d, 0d, 50000d, 315d);
            //_calibrationPoints.Add(new CalibrationPointDouble(-5000d, -153d));

        }

        void cabinAltitude_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _calibrationPoints.Interpolate(e.Value.DoubleValue);
        }
    }
}
