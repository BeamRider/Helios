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

namespace GadrocsWorkshop.Helios.Gauges.AV8B.Accululator
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;
    using System.Windows.Media;

    [HeliosControl("Helios.AV8B.Accululator", "Accululator", "AV-8B Gauges", typeof(GaugeRenderer))]
    public class Accululator : BaseGauge
    {
        private HeliosValue _accululatorPressure;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _needleCalibration;
        
        public Accululator()
            : base("Accululator", new Size(364, 376))
        {

            //Components.Add(new GaugeImage("{Helios}/Gauges/AV-8B/Accululator/Accululator_faceplate.xaml", new Rect(32d, 38d, 300, 300)));

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 3600d, 90d);
            //_needleCalibration.Add(new CalibrationPointDouble(100d, 18d));
            //_needleCalibration.Add(new CalibrationPointDouble(500d, 180d));
            _needle = new GaugeNeedle("{Helios}/Gauges/AV-8B/Common/needle_a.xaml", new Point(182d, 188d), new Size(44, 165), new Point(22, 130), -45d);
            Components.Add(_needle);

            //Components.Add(new GaugeImage("{Helios}/Gauges/A-10/Common/gauge_bezel.png", new Rect(0d, 0d, 364d, 376d)));

            _accululatorPressure = new HeliosValue(this, new BindingValue(0d), "", "Accumulator", "Current brake accumulator pressure", "(0 - 3600)", BindingValueUnits.PoundsPerSquareInch);
            _accululatorPressure.Execute += new HeliosActionHandler(accululatorPressure_Execute);
            Actions.Add(_accululatorPressure);
        }

        void accululatorPressure_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }

    }
}
