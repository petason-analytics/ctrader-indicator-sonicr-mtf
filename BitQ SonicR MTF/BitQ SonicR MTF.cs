using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;
using System.Collections.Generic;
using cAlgoBitQ.Util;

namespace cAlgo.BitQ.Indicators
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


        private Bars barsM5;
        private Bars barsM15;
        private Bars barsH1;
        private Bars barsH4;
        private Bars barsD1;

        //protected override void InitializeWrong()
        protected void InitializeWrong()
        {
            // Initialize and create nested indicators
            Bars barsM5 = MarketData.GetBars(TimeFrame.Minute5);

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


        protected override void Initialize()
        {
			// Initialize and create nested indicators
			// MarketData.GetSeries(TimeFrame.Minute5) is obsolete, use GetBars instead
			barsM5 = MarketData.GetBars(TimeFrame.Minute5);
			barsM15 = MarketData.GetBars(TimeFrame.Minute15);
			barsH1 = MarketData.GetBars(TimeFrame.Hour);
			barsH4 = MarketData.GetBars(TimeFrame.Hour4);
			barsD1 = MarketData.GetBars(TimeFrame.Daily);

			M5_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM5.HighPrices, 34);
            M5_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM5.LowPrices, 34);
            M5_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 34);
            M5_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM5.ClosePrices, 89);

            M15_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsM15.HighPrices, 34);
            M15_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsM15.LowPrices, 34);
            M15_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsM15.ClosePrices, 34);
            M15_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsM15.ClosePrices, 89);

            H1_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsH1.HighPrices, 34);
            H1_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsH1.LowPrices, 34);
            H1_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsH1.ClosePrices, 34);
            H1_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsH1.ClosePrices, 89);

            H4_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsH4.HighPrices, 34);
            H4_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsH4.LowPrices, 34);
            H4_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsH4.ClosePrices, 34);
            H4_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsH4.ClosePrices, 89);

            D1_EMA34_H_indicator = Indicators.ExponentialMovingAverage(barsD1.HighPrices, 34);
            D1_EMA34_L_indicator = Indicators.ExponentialMovingAverage(barsD1.LowPrices, 34);
            D1_EMA34_C_indicator = Indicators.ExponentialMovingAverage(barsD1.ClosePrices, 34);
            D1_EMA89_C_indicator = Indicators.ExponentialMovingAverage(barsD1.ClosePrices, 89);
        }

        //public override void CalculateWrong(int index)
        public void CalculateWrong(int index)
        {
            // Calculate value at specified index
            // Result[index] = ...

            // Check current timeframe
            if (TimeFrame == TimeFrame.Minute5)
            {

            }
            else
            {
                throw new Exception("Invalid TimeFrame: " + TimeFrame.ToString() + " | This indicator support M5 chart only");
            }


            if (enabled_m5)
            {
                M5_ema_34_High[index] = M5_EMA34_H_indicator.Result[index];
                M5_ema_34_Low[index] = M5_EMA34_L_indicator.Result[index];
                M5_ema_34_Close[index] = M5_EMA34_C_indicator.Result[index];
                M5_ema_89_Close[index] = M5_EMA89_C_indicator.Result[index];
            }

            if (enabled_m15)
            {
                M15_ema_34_High[index] = M15_EMA34_H_indicator.Result[index];
                M15_ema_34_Low[index] = M15_EMA34_L_indicator.Result[index];
                M15_ema_34_Close[index] = M15_EMA34_C_indicator.Result[index];
                M15_ema_89_Close[index] = M15_EMA89_C_indicator.Result[index];
            }

            if (enabled_H1)
            {
                H1_ema_34_High[index] = H1_EMA34_H_indicator.Result[index];
                H1_ema_34_Low[index] = H1_EMA34_L_indicator.Result[index];
                H1_ema_34_Close[index] = H1_EMA34_C_indicator.Result[index];
                H1_ema_89_Close[index] = H1_EMA89_C_indicator.Result[index];
            }

            if (enabled_H4)
            {
                H4_ema_34_High[index] = H4_EMA34_H_indicator.Result[index];
                H4_ema_34_Low[index] = H4_EMA34_L_indicator.Result[index];
                H4_ema_34_Close[index] = H4_EMA34_C_indicator.Result[index];
                H4_ema_89_Close[index] = H4_EMA89_C_indicator.Result[index];
            }

            if (enabled_D1)
            {
                D1_ema_34_High[index] = D1_EMA34_H_indicator.Result[index];
                D1_ema_34_Low[index] = D1_EMA34_L_indicator.Result[index];
                D1_ema_34_Close[index] = D1_EMA34_C_indicator.Result[index];
                D1_ema_89_Close[index] = D1_EMA89_C_indicator.Result[index];
            }
        }

        public void CalculateWrong2(int index)
        {
    //        /*
    //         How index was populated: 
    //         index = 0..barCount
    //         The chart is infinite scroll list items, initial: let say 100 bars on the screen, we call it page 1, barCount = 100
    //         When you scroll left to load more bar in the past, let say 150 bars was loaded and append to the left, we have 100 + 150 = 250 bars
    //        So now index = 0..250,   0 is the left most bar, 250 is LastValue, the last bar on the right of the chart

    //         */
    //        // Calculate value at specified index
    //        // Result[index] = ...

    //        // Check current timeframe
    //        int
    //          m5BarCount
    //         , m15BarCount
    //         , H1BarCount
    //         , H4BarCount
    //         , D1BarCount
    //        ;
    //        if (TimeFrame == TimeFrame.Minute5)
    //        {
    //            /*
				//IndicatorDataSeries order:
				//0 1 2 .................... LastValue
				//IndicatorDataSeries.LastValue === current
				//series.Last(2) will get the index at 2 bars ago
				// */

    //            /*
    //                 0           1           2           3           4           5
    //            -----0---5---10--15--20--25--30--35--40--45--50--55--0---5---10--15---
    //            -----0---5---10--15--20--25--30--35--40--45--50--55--0---5---10--15---
    //                                 ^                                   ^
    //                                 -------------------------------------
    //             m5_index =          0   1   2   3   4   5   6   7   8   9   10
    //            m15_index =  0   1   1   1   2   2   2   3   3   3   4   4   4   5 
    //             h1_index =  0   0   0   0   0   0   0   0   0   0   0   0   0   0   
                                       
    //            */
    //            // index = current m5 bar
    //            // im15 = M15.LastIndex - (bars of m15 count)
    //            //m5BarCount = index; // NOTE: this index is not the begining of the days
    //            //m15BarCount = (int)Math.Ceiling((double)(Math.Max(0, index - m5BarCount_from_M15_begining)) / (3));
    //            //H1BarCount = (int)Math.Ceiling((double)(Math.Max(0, index - m5BarCount_from_H1_begining)) / (3 * 4));
    //            //H4BarCount = (int)Math.Ceiling((double)(Math.Max(0, index - m5BarCount_from_H4_begining)) / (3 * 4 * 4));
    //            //D1BarCount = (int)Math.Ceiling((double)(Math.Max(0, index - m5BarCount_from_D1_begining - 1 * 12)) / (3 * 4 * 4 * 6));
    //        }
    //        else
    //        {
    //            throw new Exception("Invalid TimeFrame: " + TimeFrame.ToString() + " | This indicator support M5 chart only");
    //        }


    //        if (enabled_m5)
    //        {
    //            M5_ema_34_High[index] = M5_EMA34_H_indicator.Result[m5BarCount];
    //            M5_ema_34_Low[index] = M5_EMA34_L_indicator.Result[m5BarCount];
    //            M5_ema_34_Close[index] = M5_EMA34_C_indicator.Result[m5BarCount];
    //            M5_ema_89_Close[index] = M5_EMA89_C_indicator.Result[m5BarCount];
    //        }

    //        if (enabled_m15)
    //        {
    //            M15_ema_34_High[index] = M15_EMA34_H_indicator.Result[m15BarCount];
    //            M15_ema_34_Low[index] = M15_EMA34_L_indicator.Result[m15BarCount];
    //            M15_ema_34_Close[index] = M15_EMA34_C_indicator.Result[m15BarCount];
    //            M15_ema_89_Close[index] = M15_EMA89_C_indicator.Result[m15BarCount];
    //        }

    //        if (enabled_H1)
    //        {
    //            H1_ema_34_High[index] = H1_EMA34_H_indicator.Result[H1BarCount];
    //            H1_ema_34_Low[index] = H1_EMA34_L_indicator.Result[H1BarCount];
    //            H1_ema_34_Close[index] = H1_EMA34_C_indicator.Result[H1BarCount];
    //            H1_ema_89_Close[index] = H1_EMA89_C_indicator.Result[H1BarCount];
    //        }

    //        if (enabled_H4)
    //        {
    //            H4_ema_34_High[index] = H4_EMA34_H_indicator.Result[H4BarCount];
    //            H4_ema_34_Low[index] = H4_EMA34_L_indicator.Result[H4BarCount];
    //            H4_ema_34_Close[index] = H4_EMA34_C_indicator.Result[H4BarCount];
    //            H4_ema_89_Close[index] = H4_EMA89_C_indicator.Result[H4BarCount];
    //        }

    //        if (enabled_D1)
    //        {
    //            D1_ema_34_High[index] = D1_EMA34_H_indicator.Result[D1BarCount];
    //            D1_ema_34_Low[index] = D1_EMA34_L_indicator.Result[D1BarCount];
    //            D1_ema_34_Close[index] = D1_EMA34_C_indicator.Result[D1BarCount];
    //            D1_ema_89_Close[index] = D1_EMA89_C_indicator.Result[D1BarCount];
    //        }
        }

        public override void Calculate(int index) {
            if (TimeFrame == TimeFrame.Minute5)
            {

            }
            else
            {
                throw new Exception("Invalid TimeFrame: " + TimeFrame.ToString() + " | This indicator support M5 chart only");
            }


            if (enabled_m5)
            {
                int m5Index = index;
                M5_ema_34_High[index] = M5_EMA34_H_indicator.Result[m5Index];
                M5_ema_34_Low[index] = M5_EMA34_L_indicator.Result[m5Index];
                M5_ema_34_Close[index] = M5_EMA34_C_indicator.Result[m5Index];
                M5_ema_89_Close[index] = M5_EMA89_C_indicator.Result[m5Index];
            }

            DateTime barOpenTime = barsM5.OpenTimes[index];
            if (enabled_m15)
            {
                int m15Index = GetIndexByDate(barsM15, barOpenTime);
                if (m15Index != -1)
                {
                    M15_ema_34_High[index] = M15_EMA34_H_indicator.Result[m15Index];
                    M15_ema_34_Low[index] = M15_EMA34_L_indicator.Result[m15Index];
                    M15_ema_34_Close[index] = M15_EMA34_C_indicator.Result[m15Index];
                    M15_ema_89_Close[index] = M15_EMA89_C_indicator.Result[m15Index];
                }
            }

            if (enabled_H1)
            {
                int H1Index = GetIndexByDate(barsH1, barOpenTime);
                if (H1Index != -1)
                {
                    H1_ema_34_High[index] = H1_EMA34_H_indicator.Result[H1Index];
                    H1_ema_34_Low[index] = H1_EMA34_L_indicator.Result[H1Index];
                    H1_ema_34_Close[index] = H1_EMA34_C_indicator.Result[H1Index];
                    H1_ema_89_Close[index] = H1_EMA89_C_indicator.Result[H1Index];
                }
            }

            if (enabled_H4)
            {
                int H4Index = GetIndexByDate(barsH4, barOpenTime);
                if (H4Index != -1)
                {
                    H4_ema_34_High[index] = H4_EMA34_H_indicator.Result[H4Index];
                    H4_ema_34_Low[index] = H4_EMA34_L_indicator.Result[H4Index];
                    H4_ema_34_Close[index] = H4_EMA34_C_indicator.Result[H4Index];
                    H4_ema_89_Close[index] = H4_EMA89_C_indicator.Result[H4Index];
                }
            }

            if (enabled_D1)
            {
                int D1Index = GetIndexByDate(barsD1, barOpenTime);
                if (D1Index != -1)
                {
                    D1_ema_34_High[index] = D1_EMA34_H_indicator.Result[D1Index];
                    D1_ema_34_Low[index] = D1_EMA34_L_indicator.Result[D1Index];
                    D1_ema_34_Close[index] = D1_EMA34_C_indicator.Result[D1Index];
                    D1_ema_89_Close[index] = D1_EMA89_C_indicator.Result[D1Index];
                }
            }
        }

        private int GetIndexByDate(Bars series, DateTime time)
        {
            return series.OpenTimes.GetIndexByTime(time);
            //return -1;//
        }
    }
}
