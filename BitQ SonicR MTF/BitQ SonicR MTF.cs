using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;
using System.Collections.Generic;

namespace cAlgo
{
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class BitQSonicRMTF : Indicator
    {
        //[Parameter("RunningMode", DefaultValue = RunningMode.VisualBacktesting, Group = "Operation")]
        //public RunningMode cBotRunningMode { get; set; }

        [Parameter("M5", DefaultValue = true, Group = "Operation")]
        public bool enabled_m5 { get; set; }


        [Parameter("M15", DefaultValue = true, Group = "Operation")]
        public bool enabled_m15 { get; set; }

        [Parameter("H1", DefaultValue = true, Group = "Operation")]
        public bool enabled_H1 { get; set; }

        [Parameter("H4", DefaultValue = true, Group = "Operation")]
        public bool enabled_H4 { get; set; }

        [Parameter("D1", DefaultValue = true, Group = "Operation")]
        public bool enabled_D1 { get; set; }


        [Output("M5 EMA34 H", LineColor = "#FF3F3F3F", LineStyle = LineStyle.Solid, Thickness = 1)]
        public IndicatorDataSeries M5_ema_34_High { get; set; }
        [Output("M5 EMA34 L", LineColor = "#FF3F3F3F", LineStyle = LineStyle.Solid, Thickness = 1)]
        public IndicatorDataSeries M5_ema_34_Low { get; set; }
        [Output("M5 EMA34 C", LineColor = "#FF3F3F3F", LineStyle = LineStyle.Solid, Thickness = 1)]
        public IndicatorDataSeries M5_ema_34_Close { get; set; }
        [Output("M5 EMA89 C", LineColor = "#FF3F3F3F", LineStyle = LineStyle.Solid, Thickness = 2)]
        public IndicatorDataSeries M5_ema_89_Close { get; set; }

        [Output("M15 EMA34 H", LineColor = "#FFFFCBCD", Thickness = 1)]
        public IndicatorDataSeries M15_ema_34_High { get; set; }
        [Output("M15 EMA34 L", LineColor = "#FFFFCBCD", Thickness = 1)]
        public IndicatorDataSeries M15_ema_34_Low { get; set; }
        [Output("M15 EMA34 C", LineColor = "#FFFFCBCD", Thickness = 1)]
        public IndicatorDataSeries M15_ema_34_Close { get; set; }
        [Output("M15 EMA89 C", LineColor = "#FFFFCBCD", Thickness = 2)]
        public IndicatorDataSeries M15_ema_89_Close { get; set; }

        [Output("H1 EMA34 H", LineColor = "#FF542478", Thickness = 1)]
        public IndicatorDataSeries H1_ema_34_High { get; set; }
        [Output("H1 EMA34 L", LineColor = "#FF542478", Thickness = 1)]
        public IndicatorDataSeries H1_ema_34_Low { get; set; }
        [Output("H1 EMA34 C", LineColor = "#FF542478", Thickness = 1)]
        public IndicatorDataSeries H1_ema_34_Close { get; set; }
        [Output("H1 EMA89 C", LineColor = "#FF542478", Thickness = 2)]
        public IndicatorDataSeries H1_ema_89_Close { get; set; }

        [Output("H4 EMA34 H", LineColor = "#FF805F00", Thickness = 1)]
        public IndicatorDataSeries H4_ema_34_High { get; set; }
        [Output("H4 EMA34 L", LineColor = "#FF805F00", Thickness = 1)]
        public IndicatorDataSeries H4_ema_34_Low { get; set; }
        [Output("H4 EMA34 C", LineColor = "#FF805F00", Thickness = 1)]
        public IndicatorDataSeries H4_ema_34_Close { get; set; }
        [Output("H4 EMA89 C", LineColor = "#FF805F00", Thickness = 2)]
        public IndicatorDataSeries H4_ema_89_Close { get; set; }

        [Output("D1 EMA34 H", LineColor = "#FF005727", Thickness = 1)]
        public IndicatorDataSeries D1_ema_34_High { get; set; }
        [Output("D1 EMA34 L", LineColor = "#FF005727", Thickness = 1)]
        public IndicatorDataSeries D1_ema_34_Low { get; set; }
        [Output("D1 EMA34 C", LineColor = "#FF005727", Thickness = 1)]
        public IndicatorDataSeries D1_ema_34_Close { get; set; }
        [Output("D1 EMA89 C", LineColor = "#FF005727", Thickness = 2)]
        public IndicatorDataSeries D1_ema_89_Close { get; set; }




        private ExponentialMovingAverage M5_EMA34_H_indicator { get; set; }
        private ExponentialMovingAverage M5_EMA34_L_indicator { get; set; }
        private ExponentialMovingAverage M5_EMA34_C_indicator { get; set; }
        private ExponentialMovingAverage M5_EMA89_C_indicator { get; set; }
        private ExponentialMovingAverage M15_EMA34_H_indicator { get; set; }
        private ExponentialMovingAverage M15_EMA34_L_indicator { get; set; }
        private ExponentialMovingAverage M15_EMA34_C_indicator { get; set; }
        private ExponentialMovingAverage M15_EMA89_C_indicator { get; set; }
        private ExponentialMovingAverage H1_EMA34_H_indicator { get; set; }
        private ExponentialMovingAverage H1_EMA34_L_indicator { get; set; }
        private ExponentialMovingAverage H1_EMA34_C_indicator { get; set; }
        private ExponentialMovingAverage H1_EMA89_C_indicator { get; set; }
        private ExponentialMovingAverage H4_EMA34_H_indicator { get; set; }
        private ExponentialMovingAverage H4_EMA34_L_indicator { get; set; }
        private ExponentialMovingAverage H4_EMA34_C_indicator { get; set; }
        private ExponentialMovingAverage H4_EMA89_C_indicator { get; set; }
        private ExponentialMovingAverage D1_EMA34_H_indicator { get; set; }
        private ExponentialMovingAverage D1_EMA34_L_indicator { get; set; }
        private ExponentialMovingAverage D1_EMA34_C_indicator { get; set; }
        private ExponentialMovingAverage D1_EMA89_C_indicator { get; set; }




        protected override void Initialize()
        {
            // Initialize and create nested indicators

            // m5: ......
            // m15: 
            // h1
            // h4
            // d1
            //Bars barsM1 = MarketData.GetBars(TimeFrame.Minute);
            Bars barsM5 = MarketData.GetBars(TimeFrame.Minute5);
            Bars barsM15 = MarketData.GetBars(TimeFrame.Minute15);
            //Bars barsM30 = MarketData.GetBars(TimeFrame.Minute30);
            Bars barsH1 = MarketData.GetBars(TimeFrame.Hour);
            Bars barsH4 = MarketData.GetBars(TimeFrame.Hour4);
            Bars barsD1 = MarketData.GetBars(TimeFrame.Daily);
            //Bars barsW1 = MarketData.GetBars(TimeFrame.Weekly);
            //Bars barsMN = MarketData.GetBars(TimeFrame.Monthly);

            //M5_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM5.HighPrices, 34);
            //M5_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM5.LowPrices, 34);
            //M5_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 34);
            //M5_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 89);

            //M15_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM15.HighPrices, 34);
            //M15_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM15.LowPrices, 34);
            //M15_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM15.ClosePrices, 34);
            //M15_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM15.ClosePrices, 89);

            //H1_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsH1.HighPrices, 34);
            //H1_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsH1.LowPrices, 34);
            //H1_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsH1.ClosePrices, 34);
            //H1_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsH1.ClosePrices, 89);

            //H4_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsH4.HighPrices, 34);
            //H4_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsH4.LowPrices, 34);
            //H4_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsH4.ClosePrices, 34);
            //H4_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsH4.ClosePrices, 89);

            //D1_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsD1.HighPrices, 34);
            //D1_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsD1.LowPrices, 34);
            //D1_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsD1.ClosePrices, 34);
            //D1_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsD1.ClosePrices, 89);

            int m = 1;
            M5_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM5.HighPrices, 34 * m);
            M5_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM5.LowPrices, 34 * m);
            M5_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 34 * m);
            M5_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 89 * m);
            m = 1 * 3;
            M15_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM5.HighPrices, 34 * m);
            M15_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM5.LowPrices, 34 * m);
            M15_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 34 * m);
            M15_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 89 * m);
            m = 1 * 3 * 4;
            H1_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM5.HighPrices, 34 * m);
            H1_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM5.LowPrices, 34 * m);
            H1_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 34 * m);
            H1_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 89 * m);
            m = 1 * 3 * 4 * 4;
            H4_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM5.HighPrices, 34 * m);
            H4_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM5.LowPrices, 34 * m);
            H4_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 34 * m);
            H4_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 89 * m);
            m = 1 * 3 * 4 * 4 * 6;
            D1_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM5.HighPrices, 34 * m);
            D1_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM5.LowPrices, 34 * m);
            D1_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 34 * m);
            D1_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 89 * m);
        }

        public override void Calculate(int index)
        {
			// Calculate value at specified index
			// Result[index] = ...

			// Check current timeframe
			//int
			//  m5BarCount
			// , m15BarCount
			// , H1BarCount
			// , H4BarCount
			// , D1BarCount
			//;
			if (TimeFrame == TimeFrame.Minute5)
			{
				//    /*
				//    IndicatorDataSeries order:
				//    0 1 2 .................... LastValue
				//    IndicatorDataSeries.LastValue === current
				//    series.Last(2) will get the index at 2 bars ago

				//    D1 -----|-----------------------------------------------|----*------------------
				//    H4 -----|-------|-------|-------|-------|-------|-------|----*|-------------
				//    H1 -|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|*|--------
				//    ...

				//    Hide all timeframe < current timeframe, because we cannot split index to smaller pieces
				//    */

				//    /*
				//    index belong to the first segment of higher timeframe: -> higherFrameIndex = 0

				//    index NOT belong to the first segment of higher timeframe: -> ceiling + 1
				//    m5  --------------------0---1---2---3---4---5---6---7---8---9---10--11--12--*(13)
				//    m15 ----x---.---.---1---.---.---2---.---.---3-----------4---.---.---5---.---*(5)
				//    m5 can start at any where in m15, im5=0 does not mean this is first m15 segment
				//     */
				//    // Bars barsM5 = MarketData.GetBars(TimeFrame.Minute5, Symbol.Name);

				//    // index = current m5 bar
				//    // im15 = M15.LastIndex - (bars of m15 count)
				//    //m5BarCount = 0; // NOTE: this index is not the begining of the days
				//    //m15BarCount = (int)Math.Ceiling((double)index / (3));
				//    //H1BarCount = (int)Math.Ceiling((double)index / (3 * 4));
				//    //H4BarCount = (int)Math.Ceiling((double)index / (3 * 4 * 4));
				//    //D1BarCount = (int)Math.Ceiling((double)index / (3 * 4 * 4 * 6));
			}
            else
            {
                throw new Exception("Invalid TimeFrame: " + TimeFrame.ToString() + " | This indicator support M5 chart only");
	        }


            if (enabled_m5)
            {
                //M5_ema_34_High[index] = M5_EMA34_H_indicator.Result.Last(m5BarCount);
                //M5_ema_34_Low[index] = M5_EMA34_L_indicator.Result.Last(m5BarCount);
                //M5_ema_34_Close[index] = M5_EMA34_C_indicator.Result.Last(m5BarCount);
                //M5_ema_89_Close[index] = M5_EMA89_C_indicator.Result.Last(m5BarCount);

                 M5_ema_34_High[index] = M5_EMA34_H_indicator.Result[index];
                  M5_ema_34_Low[index] = M5_EMA34_L_indicator.Result[index];
                M5_ema_34_Close[index] = M5_EMA34_C_indicator.Result[index];
                M5_ema_89_Close[index] = M5_EMA89_C_indicator.Result[index];
            }

            if (enabled_m15)
            {
                // M15_ema_34_High[index] = M15_EMA34_H_indicator.Result.Last(m15BarCount);
                //  M15_ema_34_Low[index] = M15_EMA34_L_indicator.Result.Last(m15BarCount);
                //M15_ema_34_Close[index] = M15_EMA34_C_indicator.Result.Last(m15BarCount);
                //M15_ema_89_Close[index] = M15_EMA89_C_indicator.Result.Last(m15BarCount);

                 M15_ema_34_High[index] = M15_EMA34_H_indicator.Result[index];
                  M15_ema_34_Low[index] = M15_EMA34_L_indicator.Result[index];
                M15_ema_34_Close[index] = M15_EMA34_C_indicator.Result[index];
                M15_ema_89_Close[index] = M15_EMA89_C_indicator.Result[index];
            }

            if (enabled_H1)
            {
                //H1_ema_34_High[index] = H1_EMA34_H_indicator.Result.Last(H1BarCount);
                //H1_ema_34_Low[index] = H1_EMA34_L_indicator.Result.Last(H1BarCount);
                //H1_ema_34_Close[index] = H1_EMA34_C_indicator.Result.Last(H1BarCount);
                //H1_ema_89_Close[index] = H1_EMA89_C_indicator.Result.Last(H1BarCount);
                 H1_ema_34_High[index] = H1_EMA34_H_indicator.Result[index];
                  H1_ema_34_Low[index] = H1_EMA34_L_indicator.Result[index];
                H1_ema_34_Close[index] = H1_EMA34_C_indicator.Result[index];
                H1_ema_89_Close[index] = H1_EMA89_C_indicator.Result[index];
            }

            if (enabled_H4)
            {
                //H4_ema_34_High[index] = H4_EMA34_H_indicator.Result.Last(H4BarCount);
                //H4_ema_34_Low[index] = H4_EMA34_L_indicator.Result.Last(H4BarCount);
                //H4_ema_34_Close[index] = H4_EMA34_C_indicator.Result.Last(H4BarCount);
                //H4_ema_89_Close[index] = H4_EMA89_C_indicator.Result.Last(H4BarCount);
                 H4_ema_34_High[index] = H4_EMA34_H_indicator.Result[index];
                  H4_ema_34_Low[index] = H4_EMA34_L_indicator.Result[index];
                H4_ema_34_Close[index] = H4_EMA34_C_indicator.Result[index];
                H4_ema_89_Close[index] = H4_EMA89_C_indicator.Result[index];
            }

            if (enabled_D1)
            {
                //D1_ema_34_High[index] = D1_EMA34_H_indicator.Result.Last(D1BarCount);
                //D1_ema_34_Low[index] = D1_EMA34_L_indicator.Result.Last(D1BarCount);
                //D1_ema_34_Close[index] = D1_EMA34_C_indicator.Result.Last(D1BarCount);
                //D1_ema_89_Close[index] = D1_EMA89_C_indicator.Result.Last(D1BarCount);
                 D1_ema_34_High[index] = D1_EMA34_H_indicator.Result[index];
                  D1_ema_34_Low[index] = D1_EMA34_L_indicator.Result[index];
                D1_ema_34_Close[index] = D1_EMA34_C_indicator.Result[index];
                D1_ema_89_Close[index] = D1_EMA89_C_indicator.Result[index];
            }
        }
    }
}
